using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;


string string_json_default = """
                             {
                               "target"        : "notepad.exe"
                              ,"verb"          : "runas"
                              ,"work_dir"      : "####special:Desktop####"
                              ,"arguments"     : [
                                                 ]
                              ,"is_no_window"  : false
                              ,"window_style"  : "Normal"
                              ,"priority"      : "Normal"
                              ,"env_variable"  : { "nAmE" : "vAlUe"
                                                  ,"-foo" : "-"
                                                 }
                             }
                             """;


string path_exe    = Assembly.GetEntryAssembly()!.Location;
string name_exe    = Path.GetFileNameWithoutExtension(path_exe);
string dir_exe     = Path.GetDirectoryName(path_exe)!;
string path_json   = Path.Combine(dir_exe, name_exe + ".json");

if (!File.Exists(path_json)) {
  File.WriteAllText(path_json,string_json_default, Encoding.UTF8);
  Console.Error.WriteLine("ERROR CODE 1: could not find " + path_json + " - dummy file was written for you.");
  Environment.Exit(1);
}


string replace_tokens_base64(string s){ //if given a string with any amount of ####base64:foo#### try to base64-decrypt foo from the string, returning the complete string with all the token replaced with the base64 part decrypted. if none is found, or if the base64 decryption has an error it simply returns the original string or skip to the next match. it is safe to pass any string to it.     
  if (string.IsNullOrEmpty(s)) { return s; }
  var regex = new Regex(@"####base64:(.*?)####", RegexOptions.IgnoreCase | RegexOptions.Singleline);
  return regex.Replace(s, m => {
    string base64_content = m.Groups[1].Value;
    try {
      byte[] bytes = Convert.FromBase64String(base64_content);
      return Encoding.UTF8.GetString(bytes);
    }
    catch (FormatException) {
      return m.Value;
    }
  });
}


string? path_from_special_folder_name(string name){
  if (string.IsNullOrWhiteSpace(name)) return null;
  var key = name.Trim().ToLowerInvariant();
  var map = Enum.GetValues<Environment.SpecialFolder>()
    .ToDictionary(sf => sf.ToString().ToLowerInvariant(), sf => sf);
  if (map.TryGetValue(key, out var sf)) {
    var path = Environment.GetFolderPath(sf);
    return string.IsNullOrEmpty(path) ? null : path;
  }
  return null;
}

string replace_tokens_special_folders (string s){     
  if (string.IsNullOrEmpty(s)) { return s; }
  var regex = new Regex(@"####special:(.*?)####", RegexOptions.IgnoreCase | RegexOptions.Singleline);
  return regex.Replace(s, m => {
    string name = m.Groups[1].Value.Trim().ToLowerInvariant();
    string path = path_from_special_folder_name(name);
    if (string.IsNullOrEmpty(path)) {
      return m.Value;
    }
    return path;
  });
}


string string_json = File.ReadAllText(path_json);
JsonNode? object_json = JsonNode.Parse(string_json, new JsonNodeOptions { PropertyNameCaseInsensitive = true });
if(object_json == null) {
  Console.Error.WriteLine("ERROR CODE 2: could not parse " + path_json + " with content: \r\n" + string_json);
  Environment.Exit(2);
}


string?   target              = object_json?["target"]?.GetValue<string?>()?.Trim()       ?? "";
if (string.IsNullOrEmpty(target)) {
  Console.Error.WriteLine("ERROR CODE 3: " + path_json + " should include target pointing to the file to open.");
  Environment.Exit(3);
}
target = replace_tokens_base64(target);
target = replace_tokens_special_folders(target);


string?   verb                = object_json?["verb"]?.GetValue<string?>()?.Trim()         ?? "open";
verb = replace_tokens_base64(verb);
verb = replace_tokens_special_folders(verb);

string?   work_dir            = object_json?["work_dir"]?.GetValue<string?>()?.Trim()     ?? "";
work_dir = replace_tokens_base64(work_dir);
work_dir = replace_tokens_special_folders(work_dir);

string[]? arguments           = ((object_json["arguments"] as JsonArray) ?? new JsonArray {}).Select(n => n?.ToString() ?? String.Empty).ToArray();
arguments = arguments.Select(s => {
  s = replace_tokens_base64(s);
  s = replace_tokens_special_folders(s);
  return s;
}).ToArray();


bool is_no_window = object_json?["is_no_window"]?.GetValue<bool?>() ?? false;


string?   string_window_style = object_json?["window_style"]?.GetValue<string?>()?.Trim() ?? "Normal";
string_window_style = replace_tokens_base64(string_window_style);
string_window_style = replace_tokens_special_folders(string_window_style);
ProcessWindowStyle string_to_enum_window_style()
  => Enum.TryParse(string_window_style, ignoreCase: true, out ProcessWindowStyle pws) ? pws : ProcessWindowStyle.Normal;
ProcessWindowStyle window_style = string_to_enum_window_style();


string?   string_priority     = object_json?["priority"]?.GetValue<string?>()?.Trim() ?? "Normal";
string_priority = replace_tokens_base64(string_priority);
string_priority = replace_tokens_special_folders(string_priority);
ProcessPriorityClass string_to_enum_process_priority()
  => Enum.TryParse(string_priority, ignoreCase: true, out ProcessPriorityClass ppc) ? ppc : ProcessPriorityClass.Normal;
ProcessPriorityClass priority = string_to_enum_process_priority();


var psi = new ProcessStartInfo();
psi.UseShellExecute        = true; //allow the OS to start non-executables with the associated opener.
psi.RedirectStandardInput  = false;
psi.RedirectStandardError  = false;
psi.RedirectStandardOutput = false;

psi.FileName = target;
if (!verb.Equals("open", StringComparison.CurrentCultureIgnoreCase)) {
  psi.Verb = verb;
}
if (!string.IsNullOrEmpty(work_dir)) {
  psi.WorkingDirectory = work_dir;
}

psi.CreateNoWindow = is_no_window;
if (window_style != ProcessWindowStyle.Normal) {
  psi.WindowStyle = window_style;
}

Array.ForEach(arguments, psi.ArgumentList.Add);

string[] additional_arguments = Environment.GetCommandLineArgs()
                                           .Skip(1)
                                           .Select(s => {
                                             s = replace_tokens_base64(s);
                                             s = replace_tokens_special_folders(s);
                                             return s;
                                           }).ToArray();
Array.ForEach(additional_arguments, psi.ArgumentList.Add);


var process = new Process();
if (priority != ProcessPriorityClass.Normal) {
  process.PriorityClass = priority;
}

process.StartInfo = psi;
process.Start();

Environment.Exit(0);//success
