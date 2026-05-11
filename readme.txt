work in progress.

=-=-=-=-=-=  

a top-level, .net 10, `c#` application,  
published as a AOT, native app, using C++ so it does not need the .net depenencies.  

this application is a runner,  
or "launcher",  
it uses shellexecute to launch a file, using the Windows OS.  

=-=-=-=-=-=  

designed to be portable,  
the information is kept in a json file,  
with the same name as the exe.  
in the same folder.  

the json includes many process related switches such as arguments,  
working directory, environment variables add/remove,  
as well as window style, and process CPU priority.  

=-=-=-=-=-=  

in addition,  
the textual content can include a special token `####special:foo####`  
as a shortcut to known special directories to the user.  

every textual content,  
include the entries for arguments array,  
can be put as a base64. which will be decoded by the runner (launcher)  
before. using the token `####base64:foo####`,  
which will be transform to plain text before running the file.  

=-=-=-=-=-=  

all additional arguments to the runner (launcher)  
are appended to the arguments used to launch the file,  
so there is a level of flexibility on top of the hard-coded values in the JSON.  

=-=-=-=-=-=  

some of the features are half baked.  

- handling environment variables (add / remove).

=-=-=-=-=-=  

the runner (launcher)  
is my kind of functional "hello world" application.  

originally it was developed using VisualBasic 6sp6 (surprisingly I still use it)  
which used INI file and Windows API to read and write the values.  

the runner (launcher) is a continues effort to avoid cmd, powershell, visual-basic scripting,  
as well as provide Unicode compatible input and portability.  

I also built a similar application using Rust.  

.net, c# was always better at launching stuff,  
shellexecute as backend and a ton of functionality,  
but the dependencies are a pain..  
so I have published the application using AOT,  
which build a native exe file, to Windows x64, using the C++ build tools.  


*cd C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3
*delete .idea - it will be recreated, it stores previous project folders.
dotnet nuget locals all --clear
dotnet restore -v:detailed
dotnet publish -r win-x64 -c Release -p:PublishAot=true -v:detailed

=-=-=-=-=-=  


PS C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3> dotnet restore -v:detailed
Restore complete (2.8s)
    Determining projects to restore...
    Restored C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj (in 2.22 sec).

Build succeeded in 2.9s
PS C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3> dotnet publish -r win-x64 -c Release -p:PublishAot=true -v:detailed
Restore complete (15.3s)
    Determining projects to restore...
    Restored C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj (in 14.66 sec).
  ConsoleApp3 net10.0 win-x64 succeeded with 6 warning(s) (28.0s) → ConsoleApp3\bin\Release\net10.0\win-x64\publish\
    C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\Program.cs(13,7): warning CS0105: The using directive for 'System.Reflection' appeared previously in this namespace
    C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\Program.cs(15,7): warning CS0105: The using directive for 'System.Runtime.InteropServices' appeared previously in this namespace
    C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\Program.cs(114,35): warning CS8602: Dereference of a possibly null reference.
    C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\Program.cs(80,19): warning CS8600: Converting null literal or possible null value to non-nullable type.
    C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\Program.cs(35,22): warning IL3000: 'System.Reflection.Assembly.Location.get' always returns an empty string for assemblies embedded in a single-file app. If the path to the app directory is needed, consider calling 'System.AppContext.BaseDirectory'.
    Generating native code
    C:\Users\Elad\RiderProjects\run_net10_nativejson_csharp_to_aot\ConsoleApp3\ConsoleApp3\Program.cs(35): warning IL3000: Program.<Main>$(String[]): 'System.Reflection.Assembly.Location.get' always returns an empty string for assemblies embedded in a single-file app. If the path to the app directory is needed, consider calling 'System.AppContext.BaseDirectory'.

Build succeeded with 6 warning(s) in 43.9s



