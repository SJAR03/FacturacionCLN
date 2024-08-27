# FacturacionCLN

## BACKEND TEST

- ASP .NET Core Web API
- .NET 8.0
- Visual Studio 2022

### Packages:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

### Structure:

/Controllers
/Models
/Services
/Data

### Entity framework

- Enter to Package Manager Console
- Add-Migration InitialCreate
- Update-database

### Business rules

- Only 2 decimals on prices and totals
- Check price on products (cordobas and dollar conversion using of reference the cordobas price, if apply)

- Cant accept negative exhange rate

## Pending

- Validate celphone numbers on client
- Validate email on client
- Validate repeat codes on client

- In general, be more specific in the type of data for the database, check for nulls and column lengths