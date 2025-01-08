# RoveComm C# #

RoveComm is the communication protocol used by the Mars Rover Design Team (MRDT) at Missouri University of Science and 
Technology (Missouri S&T). Several implementations exist for RoveComm. This implementation was made to be used with the 
[new BaseStation](https://github.com/MissouriMRDT/Basestation_Software_Blazor) written in Blazor.

# In This Repo

The repo conains a Visual Studio 2022 solution `RoveComm_CSharp.sln`. The solution contains two projects.

## RoveComm

This contains the source code for the ![](images/rovecomm.png) [RoveComm NuGet Package](https://github.com/MissouriMRDT/RoveComm_CSharp/pkgs/nuget/RoveComm).
For more information on RoveComm installation and usage, look [here](RoveComm/README.md).

## RoveComm.Test

This contains a quick and dirty sample app for testing RoveComm. To run it, open the solution and right click on the
`RoveComm.Test` project. Select "Set As Startup Project". Then click the run button at the top and select `http`.
