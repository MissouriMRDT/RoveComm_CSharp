# RoveComm C# #

RoveComm is the communication protocol used by the Mars Rover Design Team (MRDT) at Missouri University of Science and 
Technology (Missouri S&T). Several implementations exist for RoveComm. This implementation was made to be used with the
[new BaseStation](https://github.com/MissouriMRDT/Basestation_Software_Blazor) written in Blazor, though it will work
with any .NET app.

## Installation

**To install through Visual Studio:**

In Visual Studio, make sure you have your project selected in Solution Explorer, then navigate to
`Project` > `Manage NuGet Packages...`. Go to `Browse` and search for "RoveComm". Click the down arrow next to the
package version number. Click `Apply`.

**To install with the dotnet CLI:**

Open terminal and `cd` into your project directory. Run the following command to install the latest version:

```cli
dotnet add package RoveComm
```

*If we need to migrate off the NuGet registry, we can also use [GitHub Packages](#using-with-github-packages)*

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

## Using With GitHub Packages

To access the GitHub package registry, you will need *Personal Access Token*. Follow the instructions below to create a PAT:

1. Navigate to [github.com](https://github.com)
1. Click on your profile picture in the upper corner.
1. Go to **Settings**.
1. On the side bar, click **Developer settings**.
1. On the side bar, expand **Personal access tokens** and select **Tokens (classic)**.
1. Click **Generate new token** > **Generate new token (classic)**.
    1. Find and check the box next to **read:packages**.
    1. Put whatever you want in notes and expiration date.
    1. You can ignore the other settings.
1. Scroll to the bottom and click **Generate token**.
1. ***Copy the token and save it somewhere!*** Otherwise you will need to generate a new one!

Because this package is hosted on GitHub instead of on [nuget.org](https://nuget.org), you must add an additional package
source. This is most easily done with the dotnet CLI.

Add GitHub as a source and name it "mrdt" (only do this once):

```cli
dotnet nuget add source --username <USERNAME> --password <TOKEN> --store-password-in-clear-text --name mrdt "https://nuget.pkg.github.com/MissouriMRDT/index.json"
```

If you are using Visual Studio, you can manage your sources in 
Tools > NuGet Package Manager > Package Manager Settings > Package Sources.

For more information about GitHub NuGet packages, see the [official documentation](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry).