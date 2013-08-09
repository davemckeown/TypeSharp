Introduction
---
TypeSharp is a C# to TypeScript code generator and testing tool built as a Visual Studio 2012 Extension

Status Update
---
August 8th 2013 - The repository is now public, however the code has not been updated in several months due to changes in the TypeScript language spec. With the introduction of generics and the 0.9 release of TypeScript stabilizing I am preparing to make major updates to the project. I am welcoming feedback and contributions from the community, please feel free to send a pull request of file an issue. If you would like to collaborate on development or have any questions my email is davemckeown@outlook.com

To track progress as the project is updated for TypeScript 0.9 see the following issue:

https://github.com/davemckeown/TypeSharp/issues/1

Getting Started
---
This Project requires the Roslyn September 2012 CTP and the Visual Studio 2012 SDK. Roslyn will install the VS SDK if it is not already installed, it is avalaible from:

http://msdn.microsoft.com/en-us/vstudio/roslyn.aspx

Once installed the Visual Studio extension project can be opened from the TypeSharpManager solution directory.

Launching the TypeSharpVSIX project will launch the experimental instance of Visual Studio 2012. Ensure that the TypeSharpVSIX has the following options set in the project properties under Debug:

Start External Program: C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe

Command Line Arguments: /rootsuffix Exp

Once the extension launches in debug mode you can open the project TypeSharpSample in the root directory to experiment. You may need to specify an output project (TypeSharpModules in sample project) under Tools -> TypeSharp before building to see the TypeScript output.
