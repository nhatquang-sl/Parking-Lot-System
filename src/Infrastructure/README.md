# NuGet Packages
- Microsoft.Extensions.Options.ConfigurationExtensions: enable read configuration
- Microsoft.EntityFrameworkCore.Design: for enable migration database 

For moving the appsettings.json from WebUI to Infrastructure
```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
```

## Serilog
- **Serilog.Settings.Configuration**: use to read configuration from json file.
```
dotnet add package Serilog
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.Loggly
```

# Database Migration
- Comment the constructor of the `ApplicationDbContext`
- Uncomment the OnConfiguring method to specify the SQL provider
- Run the following command
```
dotnet ef migrations add InitialCreate -o Persistence\Migrations
```