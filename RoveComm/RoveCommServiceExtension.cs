using Microsoft.Extensions.DependencyInjection;
using RoveComm;

public static class RoveCommServiceExtension
{
    /// <summary>
    /// Makes it so that you can call <c>builder.Services.AddRoveComm()</c>
    /// in Program.cs in your Blazor project
    /// </summary>
    public static void AddRoveComm(this IServiceCollection services)
    {
        services.AddSingleton<RoveCommService>();
        services.AddHostedService((sp) => sp.GetRequiredService<RoveCommService>());
    }
}
