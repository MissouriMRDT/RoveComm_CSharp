# RoveComm C# #

RoveComm is the communication protocol used by the Mars Rover Design Team (MRDT) at Missouri University of Science and 
Technology (Missouri S&T). Several implementations exist for RoveComm. This implementation was made to be used with the
[new BaseStation](https://github.com/MissouriMRDT/Basestation_Software_Blazor) written in Blazor, though it will work
with any .NET app.

## Installation

**TODO: instructions for adding github as a source**

**To install through Visual Studio:**

Right click on your project in Visual Studio and select `Manage NuGet Packages...`.
Go to `Browse` and search for "RoveComm". Click the down arrow next to the package version number, then click `Apply`.

**To install with the dotnet CLI:**

Open terminal and `cd` into your project directory. Run the following command to install the latest version:

```cli
dotnet add package RoveComm
```

## Adding RoveComm to your Blazor App

In `Program.cs`, add the following:

```cs
using RoveComm;
builder.Services.AddRoveComm();
```

You can inject RoveComm into your razor pages and use it like so:

```cshtml
@inject RoveCommService _RoveComm
@using RoveComm

<h1>My Super Cool Application</h1>
<p>@_driveSpeeds</p>

@code
{
    private string _driveSpeeds = "";

    // Subscribe to packets containing floats with DataID 3100
    _RoveComm.On<float>(
            "Core",
            "DriveSpeeds",
            async (packet) => {
                _driveSpeeds = _string.Join(", ", packet.Data);
                await InvokeAsync(StateHasChanged);
            }
        );
}
```

## Documentation

For further documentation of available methods, see [TODO: set up Doxygen for RoveComm_CSharp]().

You can see the list of boards and DataIDs in the [RoveComm_Base Repo](https://github.com/MissouriMRDT/RoveComm_Base).
If you need a new packet added for any reason, contact a software lead.
