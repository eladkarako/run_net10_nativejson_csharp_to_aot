/* this is a launcher.
 * -----------------------------------------------------------------------------------
 * you can rename the executable filename to be anything you want.
 * place a .json file with the same name next to it.
 * -----------------------------------------------------------------------------------
 * the minimal content of the json file is:
 * {"target":"foo"}
 * -----------------------------------------------------------------------------------
 * here is an example json
 *  {
 *   "verb"          : "runas"
 *  ,"target"        : "notepad.exe"
 *  ,"work_dir"      : "!!!!special:Desktop!!!!"
 *  ,"arguments"     : ["@@@@base64:MSAoZmlsZSAnbmFtZScgd2l0aCBoYXJkIHRvIGVzY2FwZSBzeW1ib2xzKS50eHQ=@@@@"
                       ]
 *  ,"window_style"  : "Normal"
 *  ,"priority"      : "Normal"
 *  ,"env_variable"  : {"nAmE" : "vAlUe"
 *                     ,"-foo" : "-"
 *                     }
 *  }
 *
 * the working folder is set to a special folder desktop.
 * the value is taken from HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders
 * using the special location token !!!!special:foo!!!!
 *
 * the first argument is a file name
 * `1 (file 'name' with hard to escape symbols).txt`
 * that is passed to the launcher using @@@@base64:foo@@@@ token.
 * the launcher decodes it, and set it to the application argument.
 *
 * to the set of environment variables 'nAmE' with value 'vAlUe' is added.
 * and if an environment variable named 'foo' exists, it is deleted.
 * 
 * this is the filename that is passed as an argument (base64):
 *
 * the verb is set to "runas" which, if user is an administrator,
 * it will launch the target, with the highest available.
 * -----------------------------------------------------------------------------------
 * this launcher delegate the execution to shellexecute
 * it means you can specify as a target any file.
 * if it has associations. it will be launched using that association.
 * for example, excel spreadsheet as target will automatically launch excel.exe
 * with the file as its argument.
 * it should not be needing an additional escaping for the arguments.
 * ----------------------------------------------------------------------------------- 
 * you can use environment variables anywhere use ####foo####
 * where 'foo' is the name of the environment-variable.
 * it will be replaced with the value if one exists,
 * and the #### #### will be removed. if there is no such environment variable name.
 * it will replaced with empty string. 
 * ----------------------------------------------------------------------------------- 
 * you can set set/remove environment variables to the target.
 * use - as value and it will locally be removed for the launch.
 * -----------------------------------------------------------------------------------
 * you can use the special folders from 
 * HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders
 * by specifying (not case sensitive)
 * the value using !!!!foo!!!!
 * if there is a key in that name, the phrase will be replaced with the value.
 * -----------------------------------------------------------------------------------
 * you can include base64 of the content instead of the actual content.
 * use @@@base64:foo@@@ and the content will be replaced with the UTF-8 of the decoded value.
 * it can be very useful for when you don't want to include passwords as plain-text.
 * or don't want to escape a lot of weird characters in the JSON file.
 * use https://eladkarako.github.io/sort/ which has local base64 encode decode.
 * -----------------------------------------------------------------------------------
 * you can use https://eladkarako.github.io/sort/
 * to deep sort a json text.
 * then beautify it.
 * deep sort sorts keys of objects (preserving their values) and content of arrays
 * the result can be used to compare side by side two versions
 * using programs such as BeyondCompare.
 * -----------------------------------------------------------------------------------
 * 
 * allows you to compare side by side json files in programs such as
 * 
 * -----------------------------------------------------------------------------------
 * 
 * value exists as a key
 * 
 * for the target.
 * say the target is "notepad.exe"
 * it will launch from C:\Windows\System32\notepad.exe (using PATH)
 * if you'll set work_dir to 
 * as well as 
 * 
 * program assumes there is a json file,
 * with the same name as the execute,
 * in the same folder as the execute.
 *
 * the minimal json file should include a 'program' entry
 * which points to the file to execute.
 *
 * other optional entries can effect the window size,
 * process CPU priority, process work directory,
 * and a verb which effects how to run the file.
 * 
 * you can effect the environment variables that are passed to the process,
 * either add or remove (prefix '-' and '-' as value)
 *
 * you can also use special token of ####FOO#### to use any environment 
 * 
 * the main idea is to be able to launch another file,
 * without using cmd.exe, powershell.exe, or visual-basic script.
 */

