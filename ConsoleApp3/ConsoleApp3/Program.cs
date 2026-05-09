using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

string exePath = Assembly.GetEntryAssembly()!.Location;
if (string.IsNullOrEmpty(exePath)){
  Console.Error.WriteLine("Cannot determine executable path.");
  Environment.Exit(1);
}

string exeDir = Path.GetDirectoryName(exePath)!;
string exeName = Path.GetFileNameWithoutExtension(exePath);
string jsonPath = Path.Combine(exeDir, exeName + ".json");

if (!File.Exists(jsonPath))
{
  Console.Error.WriteLine($"JSON file not found: {jsonPath}");
  Environment.Exit(2);
}

string jsonText = File.ReadAllText(jsonPath);
JsonNode? root = JsonNode.Parse(jsonText);
if (root is null)
{
  Console.Error.WriteLine("Parsed JSON is null.");
  Environment.Exit(2);
}

// Example: print root as compact JSON
Console.WriteLine(
  root.ToJsonString(new JsonSerializerOptions { WriteIndented = true })
);


// Example: safely read nested properties without knowing schema
if (root["name"] is JsonNode nameNode)
{
  Console.WriteLine("name: " + nameNode.GetValue<string?>());
}

// Example: enumerate array if present
if (root["items"] is JsonArray arr)
{
  Console.WriteLine("items:");
  foreach (var item in arr)
    Console.WriteLine(" - " + (item?.ToJsonString() ?? "null"));
  
  Environment.Exit(0);//success
}

