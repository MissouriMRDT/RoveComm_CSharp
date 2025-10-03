# RoveComm C# #

RoveComm is the communication protocol used by the Mars Rover Design Team (MRDT) at Missouri University of Science and 
Technology (Missouri S&T). Several implementations exist for RoveComm. This implementation was made to be used with 
[Basstation Software Blazor](https://github.com/MissouriMRDT/Basestation_Software_Blazor).

# In This Repo

The repo conains a Visual Studio 2022 solution `RoveComm_CSharp.sln`. The solution contains two projects: RoveComm and RoveComm.Test.

## RoveComm

This contains the source code for the ![](RoveComm/rovecomm.png) **RoveComm NuGet Package**.

The package is hosted on the [NuGet Registry](https://www.nuget.org/packages/RoveComm) and on
[GitHub Packages](https://github.com/MissouriMRDT/RoveComm_CSharp/pkgs/nuget/RoveComm), though extra steps are required
to use it from there. For more information on RoveComm installation and usage, look [here](RoveComm/README.md).

## RoveComm.Test

This contains a quick and dirty sample app for testing RoveComm. To start it, run `dotnet run --project Rovecomm.Test` in the RoveComm_CSharp root directory. The app can then be accessed at `http://127.0.0.1:5185/` within any web browser. 

# Updating to Lastest Manifest Version
The RoveComm Project can be updated to reflect the latest manifest version via the following steps:
1. Update the main RoveComm submodule to the latest commit: `git submodule update --recursive --remote`
2. Run the parser script to update the C# code: `python ./tools/parser.py`
3. Commit and push your changes to the submodule and C# code to git.
