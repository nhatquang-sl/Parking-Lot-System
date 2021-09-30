# NuGet Packages
- Microsoft.Extensions.Options.ConfigurationExtensions: enable read configuration
- Microsoft.EntityFrameworkCore.Design: for enable migration database 

For moving the appsettings.json from WebUI to Infrastructure
```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
```

# Database Migration
- Comment the constructor of the `ApplicationDbContext`
- Uncomment the OnConfiguring method to specify the SQL provider
- Run the following command
```
dotnet ef migrations add InitialCreate -o Persistence\Migrations
```