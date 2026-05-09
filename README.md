# run_net10_nativejson_csharp_to_aot
top-level program, no class targeting .NET 10. that reads json file and prints it back. it uses aot meaning it needs desktop c++ development even-though you write c#, this project was developed with jetbrains rover which is a lot faster than visual studio. sadly you'll have to install the sdk for .net and c# anyway, and eventually c++ toolkit...

<hr/>

this program is named runner (launcher) since i have looked for a native, easy to maintain, running program that would be able me to avoid stdin stdout stderr, optionally subsystem windows and no use of console terminal cmd powershell.... batch files. while maintaining core functionality and configuration in a json file, modern and neat! this is like a "hello world!" project, just more useful, as for now, Rust language was the way to go, but .net has windows OS better integrated - and more fun to work with.

<hr/>

i will try eventually making both rover and .net for this development portable, hope i wouldn't have to use vmware thinapp...

<hr/>

this is what you'll normally get until you will install the c++ toolkit with visual studio bootstrap installer.  

if you're already "there",  

i advise installing all the .net instead of using jetbrains rover to install it,  
at least this way everything is managed by the visual studio installer.  

sadly this makes things hard to be portable,  
as the sdk for .net as well as the c++ toolkits are registered with plenty of registry entries,  
and simply copying all to one folder and setting the project, is probably only work as long the tools are installed on the machine properly...   

you can always ship the runtime for the .net with your project, but i hoped for AOT just for testing...

note, i also had to add nuget sources for the rover, since it was my first installation.  



<hr/>

```txt

dotnet publish -r win-x64 -c Release -p:PublishAot=true
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.DotNet.ILCompiler (>= 10.0.7)' for 'net10.0'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.NET.ILLink.Tasks (>= 10.0.7)' for 'net10.0'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.DotNet.ILCompiler (>= 10.0.7)' for 'net10.0/win-x64'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.NET.ILLink.Tasks (>= 10.0.7)' for 'net10.0/win-x64'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.NETCore.App.Runtime.win-x64 (= 10.0.7)' for 'net10.0'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.WindowsDesktop.App.Runtime.win-x64 (= 10.0.7)' for 'net10.0'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.AspNetCore.App.Runtime.win-x64 (= 10.0.7)' for 'net10.0'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'Microsoft.NETCore.App.Runtime.NativeAOT.win-x64 (= 10.0.7)' for 'net10.0'.
    C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj : error NU1100: Unable to resolve 'runtime.win-x64.Microsoft.DotNet.ILCompiler (= 10.0.7)' for 'net10.0'.

Restore failed with 9 error(s) in 0.9s


-----------------------------------------------------------------------------------

dotnet --list-sdks
8.0.420 [C:\Users\Elad\.dotnet\sdk]
9.0.313 [C:\Users\Elad\.dotnet\sdk]
10.0.203 [C:\Users\Elad\.dotnet\sdk]

-----------------------------------------------------------------------------------

dotnet --list-runtimes
Microsoft.AspNetCore.App 8.0.26 [C:\Users\Elad\.dotnet\shared\Microsoft.AspNetCore.App]
Microsoft.AspNetCore.App 9.0.15 [C:\Users\Elad\.dotnet\shared\Microsoft.AspNetCore.App]
Microsoft.AspNetCore.App 10.0.7 [C:\Users\Elad\.dotnet\shared\Microsoft.AspNetCore.App]
Microsoft.NETCore.App 8.0.26 [C:\Users\Elad\.dotnet\shared\Microsoft.NETCore.App]
Microsoft.NETCore.App 9.0.15 [C:\Users\Elad\.dotnet\shared\Microsoft.NETCore.App]
Microsoft.NETCore.App 10.0.7 [C:\Users\Elad\.dotnet\shared\Microsoft.NETCore.App]
Microsoft.WindowsDesktop.App 8.0.26 [C:\Users\Elad\.dotnet\shared\Microsoft.WindowsDesktop.App]
Microsoft.WindowsDesktop.App 9.0.15 [C:\Users\Elad\.dotnet\shared\Microsoft.WindowsDesktop.App]
Microsoft.WindowsDesktop.App 10.0.7 [C:\Users\Elad\.dotnet\shared\Microsoft.WindowsDesktop.App]

-----------------------------------------------------------------------------------

dotnet nuget list source 
No sources found.

-----------------------------------------------------------------------------------

dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
Package source with Name: nuget.org added successfully.

dotnet nuget add source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-public/nuget/v3/index.json -n dotnet-public
Package source with Name: dotnet-public added successfully.

-----------------------------------------------------------------------------------


dotnet nuget list source 

Registered Sources:
  1.  nuget.org [Enabled]
      https://api.nuget.org/v3/index.json
  2.  dotnet-public [Enabled]
      https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-public/nuget/v3/index.json




dotnet nuget locals all --clear
Clearing NuGet HTTP cache: C:\Users\Elad\AppData\Local\NuGet\v3-cache
Clearing NuGet global packages folder: C:\Users\Elad\.nuget\packages\
Clearing NuGet Temp cache: E:\temp\NuGetScratch
Clearing NuGet plugins cache: C:\Users\Elad\AppData\Local\NuGet\plugins-cache
Local resources cleared.


dotnet restore --no-cache --verbosity:diagnostic
Restore complete (0.7s)
    Determining projects to restore...
    Restored C:\Users\Elad\RiderProjects\ConsoleApp3\ConsoleApp3\ConsoleApp3.csproj (in 110 ms).

Build succeeded in 0.8s




PS C:\Users\Elad\RiderProjects\ConsoleApp3> dotnet publish -r win-x64 -c Release -p:PublishAot=true 
Restore complete (26.7s)
  ConsoleApp3 net10.0 win-x64 failed with 1 error(s) (21.6s) → ConsoleApp3\bin\Release\net10.0\win-x64\ConsoleApp3.dll
    C:\Users\Elad\.nuget\packages\microsoft.dotnet.ilcompiler\10.0.7\build\Microsoft.NETCore.Native.Windows.targets(142,5): error Platform linker not found. Ensure you have all the required prerequisites documented at https://aka.ms/nativeaot-prerequisites, in particular the Desktop Development for C++ workload in Visual Studio. For ARM64 development also install C++ ARM64 build tools.

Build failed with 1 error(s) in 49.5s
PS C:\Users\Elad\RiderProjects\ConsoleApp3> 



https://download.visualstudio.microsoft.com/download/pr/ef8a1da5-1f31-4995-8be6-cb9800f90516/c179f43574c18baa66a940b1112b4a2e2debc5d0e39cf730e8df3339649344e0/vs_BuildTools.exe


That error means the MSVC linker (link.exe) isn't available to the Native AOT toolchain. Fixes:
Install Visual C++ build tools, Desktop development with C++" workload (ensure the MSVC toolset is selected).

Verify link.exe is discoverable
Open a Developer Command Prompt for Visual Studio and run: where link.exe It should return a path under ...\VC\Tools\MSVC\\bin\Hostx64\x64\link.exe.

Restart shell/IDE


dotnet nuget locals all --clear
dotnet restore -v:detailed
dotnet publish -r win-x64 -c Release -p:PublishAot=true -v:detailed
```














