# A .Net Core/.Net Windows Scheduled Background Service Example

Uses:
- `Microsoft.NET.Sdk.Worker` - .Net Core/.Net SDK for worker services
- `Microsoft.Extensions.Hosting.WindowsServices` - .Net Core/.Net hosting extension NuGet package for Windows Services
- `ncrontab` - NuGet package for `cron` expressions scheduling

Examples:
- [`WinService.csproj`](./src/WinService/WinService.csproj) - SDK and NuGet package setup
- [`Program.cs`](./src/WinService/Program.cs) - dependency injection and module usage
- [`Worker.cs`](./src/WinService/Worker.cs) - service scheduling and running the process


