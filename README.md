# A Windows scheduled Background Service example

Uses:
- `Microsoft.NET.Sdk.Worker` .Net SDK for worker services
- `Microsoft.Extensions.Hosting.WindowsServices` extension package for Windows Services
- `ncrontab` for scheduling with `cron` expressions

Examples:
- [`WinService.csproj`](./src/WinService/WinService.csproj) - SDK and NuGet package setup
- [`Program.cs`](./src/WinService/Program.cs) - dependency injection and module usage
- [`Worker.cs`](./src/WinService/Worker.cs) - service scheduling and runnning the process


