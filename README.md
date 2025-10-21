# Blazor frontend til Domain Centric Backend

## Formål

Formålet med denne demo er at vise hvorledes elemeterne i arkitekturen "spiller sammen". 

Formålet er IKKE at vise korrekt implementering af de enkelte lag. Eksempelvis implementeres domain entities som en "Anemic domain model" Ligeledes er alle implementeringer af interfaces "tomme" (de kaster blot en exception) 

Fokus er:

- Blazor kobles sammen med en "Onion Core"
- Opsætning af IoC containeren via Blazor
- Opsætning af Entity Framework via Blazor
- Database migration og database update.

Hertil oprettes det nødvendige kodeskelet for at kunne opfylde formålet. 



## Forudsætninger



Der anvende .NET 10 og Visual Studio 2026 Insider til løsningen.

Tjek din .NET version inden du går igang:

```powershell
C:\Demo>dotnet --version
10.0.100-rc.2.25502.107
```

Du skal mindst være på version `10.0.100-rc.2`



## Overblik over arkitekturen

Nedenstående er vist et oversigtsdiagram for demo projektet



```mermaid
---
config:
  layout: fixed
---
flowchart TB
 subgraph UI["Blazor SSS Frontend UI"]
        UI_Component["UI Components / Pages"]
  end
 subgraph APP["Application (Use Cases)"]
        App_Services["Application Services"]
        DTOs["DTOs / Commands / Queries"]
        Repositories["Repository Interfaces"]
        CommandHandlers["Command Interfaces"]
        QueryHandlers["Query Interfaces"]
  end
 subgraph DOMAIN["Domain (Core)"]
        Entities["Entities / Aggregates"]
        ValueObjects["Value Objects"]
        DomainServices["Domain Services Interfaces"]
  end
 subgraph INFRA["Infrastructure (Adapters)"]
        RepoImpl["Repository Implementations (adapters)"]
        Integrations["External Integrations (e.g., Email, Payment, 3rd-party APIs)"]
        Mappers["ORM / Mappers"]
        DmServicesImpl["Domain Services Implementations"]
        QueryServiceImpl["Query Services  Implementations (adapters)"]
  end
 subgraph DB["Database"]
        DBMS["Relational/NoSQL DB"]
  end
    App_Services --> Entities & Repositories
    ValueObjects --> DomainServices
    App_Services -. implements .-> CommandHandlers
    RepoImpl -. implements .-> Repositories
    RepoImpl --> Mappers & DBMS
    DmServicesImpl -. implements .-> DomainServices
    QueryServiceImpl -. implements .-> QueryHandlers
    QueryServiceImpl --> DBMS
    Entities --> ValueObjects
    UI_Component --> DTOs
    DTOs --> CommandHandlers & QueryHandlers
     DTOs:::subtle
     Mappers:::subtle
    classDef core fill:#fff6cc,stroke:#b38f00,stroke-width:1.2px
    classDef app fill:#e6f7ff,stroke:#1f78b4,stroke-width:1.2px
    classDef ui fill:#f0f5ff,stroke:#2f54eb,stroke-width:1.2px
    classDef infra fill:#f6ffed,stroke:#389e0d,stroke-width:1.2px
    classDef db fill:#fff1f0,stroke:#cf1322,stroke-width:1.2px
    classDef subtle fill:#fafafa,stroke:#bbb,stroke-dasharray: 3 3

```

## Oprettelse af Visual Studio Solution samt projekter

Til oprettelse af Visual Studio projektet kan man med fordel anvende `KbrOnionTemplate-Core-latest.bat` (som er vist i Bilag 1).

I nedenstående eksempel vises oprettelsen af en solution med projekter for `BlazorOnionDemo` - de er de første tre linjer er input - resten er output fra scriptet (der vises kun et udsnit af output):

```powershell
C:\Demo>KbrOnionTemplate-Core-latest.bat
Enter application name: BlazorOnionDemo
Are You Sure? [Y,N]?Y
Creating BlazorOnionDemo
The template "Blazor Web App" was created successfully.
This template contains technologies from parties other than Microsoft, see https://aka.ms/aspnetcore/10.0-third-party-notices for details.

Processing post-creation actions...
Restoring C:\Demo\BlazorOnionDemo\BlazorOnionDemo.WebUi\BlazorOnionDemo.WebUi.csproj:
Restore succeeded.


The template "Class Library" was created successfully.

Processing post-creation actions...
Restoring C:\Demo\BlazorOnionDemo\BlazorOnionDemo.Application\BlazorOnionDemo.Application.csproj:
Restore succeeded.


The template "Class Library" was created successfully.

Processing post-creation actions...
Restoring C:\Demo\BlazorOnionDemo\BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj:
Restore succeeded.


The template "NUnit Test Project" was created successfully.

Processing post-creation actions...
Restoring C:\Demo\BlazorOnionDemo\BlazorOnionDemo.Domain.Test\BlazorOnionDemo.Domain.Test.csproj:
Restore succeeded.


The template "Class Library" was created successfully.

Processing post-creation actions...
Restoring C:\Demo\BlazorOnionDemo\BlazorOnionDemo.Infrastructure\BlazorOnionDemo.Infrastructure.csproj:
Restore succeeded.


Reference `..\BlazorOnionDemo.Application\BlazorOnionDemo.Application.csproj` added to the project.
Reference `..\BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj` added to the project.
Reference `..\BlazorOnionDemo.Infrastructure\BlazorOnionDemo.Infrastructure.csproj` added to the project.
Reference `..\BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj` added to the project.
Reference `..\BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj` added to the project.
Reference `..\BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj` added to the project.
Reference `..\BlazorOnionDemo.Application\BlazorOnionDemo.Application.csproj` added to the project.
The template "Solution File" was created successfully.

Project `BlazorOnionDemo.WebUi\BlazorOnionDemo.WebUi.csproj` added to the solution.
Project `BlazorOnionDemo.Application\BlazorOnionDemo.Application.csproj` added to the solution.
Project `BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj` added to the solution.
Project `BlazorOnionDemo.Infrastructure\BlazorOnionDemo.Infrastructure.csproj` added to the solution.

Update nuget packages
Tool 'dotnet-outdated-tool' is already installed.
Discovering projects...
Analyzing project(s)...
Analyzing dependencies...
» BlazorOnionDemo.Domain.Test
  [net10.0]
  Microsoft.NET.Test.Sdk  17.14.0 -> 18.0.0
  NUnit                   4.3.2   -> 4.4.0
  NUnit.Analyzers         4.7.0   -> 4.10.0
  NUnit3TestAdapter       5.0.0   -> 5.2.0

Version color legend:
<red>   : Major version update or pre-release version. Possible breaking changes.
<yellow>: Minor version update. Backwards-compatible features added.
<green> : Patch version update. Backwards-compatible bug fixes.

Upgrading package Microsoft.NET.Test.Sdk...
Project BlazorOnionDemo.Domain.Test [net10.0] upgraded successfully

Upgrading package NUnit...
Project BlazorOnionDemo.Domain.Test [net10.0] upgraded successfully

Upgrading package NUnit.Analyzers...
Project BlazorOnionDemo.Domain.Test [net10.0] upgraded successfully

Upgrading package NUnit3TestAdapter...
Project BlazorOnionDemo.Domain.Test [net10.0] upgraded successfully

Elapsed: 00:00:07.3917757
C:\Demo\BlazorOnionDemo>
```



## Oprettelse af demo kode

### Domain model

```c#
namespace BlazorOnionDemo.Domain.Entity;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Author Author { get; set; }
}
```



### Application layer

#### Kontrakter

`IAuthorCommand`

```c#
namespace BlazorOnionDemo.Application.Contracts.Commands;

public interface IAuthorCommand
{
    Task Create(AuthorCreateCommandDto dto);
    Task AddBookToAuthor(AddBookToAuthorCommandDto dto);
}

public class AddBookToAuthorCommandDto
{
    public int AuthorId { get; set; }
    public int BookId { get; set; }
}

public class AuthorCreateCommandDto
{
    public string Name { get; set; }
}
```



`IBookCommand`

```c#
namespace BlazorOnionDemo.Application.Contracts.Commands;

public interface IBookCommand
{
    Task Create(BookCreateCommandDto dto);
}

public class BookCreateCommandDto
{
    public string Title { get; set; }
    public string Description { get; set; }
}
```



`IAuthorQuery`

```c#
namespace BlazorOnionDemo.Application.Contracts.Queries;

public interface IAuthorQuery
{
    Task<AuthorDto> GetById(int id);
    Task<List<AuthorDto>> GetAll();
}

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<AuthorBookDto> Books { get; set; }
}

public class AuthorBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
```



`IBookQuery`

```c#
namespace BlazorOnionDemo.Application.Contracts.Queries;

public interface IBookQuery
{
    Task<BookDto> GetById(int id);
    Task<List<BookDto>> GetAll();
}

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public AuthorDto Author { get; set; }
}
```



`IAuthorRepository`

```c#
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.Repositories;

public interface IAuthorRepository
{
    Task<Author> Load(int id);
    Task Save();
}
```



`IBookRepository`

```c#
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.Repositories;

public interface IBookRepository
{
    Task<Book> Load(int id);
    Task Save();
}
```



#### CommandHandlers

`AuthorCommand`

```c#
using BlazorOnionDemo.Application.Contracts.Commands;

namespace BlazorOnionDemo.Application.CommandHandlers;

public class AuthorCommand : IAuthorCommand
{
    Task IAuthorCommand.AddBookToAuthor(AddBookToAuthorCommandDto dto)
    {
        throw new NotImplementedException();
    }

    Task IAuthorCommand.Create(AuthorCreateCommandDto dto)
    {
        throw new NotImplementedException();
    }
}
```



`BookCommand`

```c#
using BlazorOnionDemo.Application.Contracts.Commands;

namespace BlazorOnionDemo.Application.CommandHandlers;

public class BookCommand : IBookCommand
{
    Task IBookCommand.Create(BookCreateCommandDto dto)
    {
        throw new NotImplementedException();
    }
}
```





### Infrastructure layer

#### Repositories

`AuthorRepository`

```c#
using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    Task<Author> IAuthorRepository.Load(int id)
    {
        throw new NotImplementedException();
    }

    Task IAuthorRepository.Save()
    {
        throw new NotImplementedException();
    }
}
```



`BookRepository`

```c#
using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    Task<Book> IBookRepository.Load(int id)
    {
        throw new NotImplementedException();
    }

    Task IBookRepository.Save()
    {
        throw new NotImplementedException();
    }
}
```



#### Queries

`AuthorQuery`

```c#
using BlazorOnionDemo.Application.Contracts.Queries;

namespace BlazorOnionDemo.Infrastructure.Queries;

public class AuthorQuery : IAuthorQuery
{
    Task<List<AuthorDto>> IAuthorQuery.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<AuthorDto> IAuthorQuery.GetById(int id)
    {
        throw new NotImplementedException();
    }
}
```



`BookQuery`

```c#
using BlazorOnionDemo.Application.Contracts.Queries;

namespace BlazorOnionDemo.Infrastructure.Queries;

public class BookQuery : IBookQuery
{
    Task<List<BookDto>> IBookQuery.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<BookDto> IBookQuery.GetById(int id)
    {
        throw new NotImplementedException();
    }
}
```



#### Database

`OrmContext`

```c#
using BlazorOnionDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorOnionDemo.Infrastructure.Database;

public class OrmContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public OrmContext(DbContextOptions<OrmContext> options) : base(options)
    {
    }
}
```



# Integration af lag

Vi er nu klare til at implementere:

- Opsætning af IoC containeren via Blazor
- Opsætning af Entity Framework via Blazor
- Blazor kobles sammen med en "Onion Core"



## Opsætning af IoC containeren via Blazor

For at holde IoC opsætningen fokuseret tilføjes en hjælpeklasse i hver lag

### Application layer

Først tilføjes nuget pakkerne

-  `Microsoft.Extensions.DependencyInjection` 
-  `Microsoft.Extensions.Configuration.Abstractions`



`ApplicationServiceRegistration`

```c#
using BlazorOnionDemo.Application.CommandHandlers;
using BlazorOnionDemo.Application.Contracts.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorOnionDemo.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorCommand, AuthorCommand>();
        services.AddScoped<IBookCommand, BookCommand>();


        return services;
    }
}
```



### Infrastructure layer

Først tilføjes nuget pakkerne

-  `Microsoft.Extensions.DependencyInjection` 
-  `Microsoft.Extensions.Configuration.Abstractions`





`InfrastructureServiceRegistration`

```c#
using BlazorOnionDemo.Application.Contracts.Queries;
using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Infrastructure.Database;
using BlazorOnionDemo.Infrastructure.Queries;
using BlazorOnionDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorOnionDemo.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Query implementations
        services.AddScoped<IAuthorQuery, AuthorQuery>();
        services.AddScoped<IBookQuery, BookQuery>();

        // Register Repository implementations
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();

        services.AddDbContext<OrmContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });

        return services;
    }
}
```



Da IoC rodpunktet placeres i Blazor projektet, skal Blazor projektet kende alle de øvrige projekter.

Bemærk at `BlazorOnionDemo.WebUi.csproj` indeholder `ProjectReference` til de øvrige projekter (bortset fra testprojektet).

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <ItemGroup>
    <ProjectReference Include="..\BlazorOnionDemo.Application\BlazorOnionDemo.Application.csproj" />
    <ProjectReference Include="..\BlazorOnionDemo.Domain\BlazorOnionDemo.Domain.csproj" />
    <ProjectReference Include="..\BlazorOnionDemo.Infrastructure\BlazorOnionDemo.Infrastructure.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.10" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.10" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.10">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <BlazorDisableThrowNavigationException>true</BlazorDisableThrowNavigationException>
  </PropertyGroup>

</Project>

```



Derfor kan vi nu indsætte en IoC blok i `Program.cs`

```c#
using BlazorOnionDemo.Application;
using BlazorOnionDemo.Infrastructure;
using BlazorOnionDemo.WebUi.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// OBS !!! Blazor frontend til Domain Centric Backend - IoC setup
builder.Services.AddApplicationServices(); // from BlazorOnionDemo.Application
builder.Services.AddInfrastructureServices(builder.Configuration); // from BlazorOnionDemo.Infrastructure


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

```





## Opsætning af Entity Framework via Blazor

### Connectionstring

I `appsettings.json` tilføjes "`ConnectionStrings`":

```json
{
  "ConnectionStrings": {
    "DbConnection": "Server=localhost;Database=BlazorOnionDemoDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true "
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Hvis din SQL server IKKE er installeret lokalt (localhost), skal du tilrette "`localhost`"  i `appsettings.json`

### Database migration & Database update

Åben et "`Packet Manager Console`" vindue og kør kommandoen

```powershell
Add-Migration InitialMigration -Context OrmContext -Project BlazorOnionDemo.Infrastructure
```

Tjek at der oprettes `Migrations` i `BlazorOnionDemo.Infrastructure`

Dernæst - kør kommandoen

```powershell
Update-Database -Context OrmContext -Project BlazorOnionDemo.Infrastructure
```





## Blazor kobles sammen med en "Onion Core"





# Bilag



## Bilag 1 - Script til projektoprettelse

KbrOnionTemplate-Core-latest.bat

```bat
@echo off
set /p "app=Enter application name: "
rem echo Are You Sure?
choice /c YN /M "Are You Sure?"
if %errorlevel%==1 goto yes
if %errorlevel%==2 goto no
:yes
echo Creating %app%

if not exist %app% mkdir %app%
cd %app%

rem dotnet new mvc -au Individual -uld -o %app%.Mvc
dotnet new blazor -au None -o %app%.WebUi
dotnet new classlib -o %app%.Application
dotnet new classlib -o %app%.Domain
dotnet new nunit -o %app%.Domain.Test
dotnet new classlib -o %app%.Infrastructure

dotnet add %app%.WebUi reference %app%.Application
dotnet add %app%.WebUi reference %app%.Domain
dotnet add %app%.WebUi reference %app%.Infrastructure
dotnet add %app%.Domain.Test reference %app%.Domain
dotnet add %app%.Application reference %app%.Domain
dotnet add %app%.Infrastructure reference %app%.Domain
dotnet add %app%.Infrastructure reference %app%.Application

dotnet new sln -n %app%
dotnet sln %app%.sln add %app%.WebUi/%app%.WebUi.csproj
dotnet sln %app%.sln add %app%.Application/%app%.Application.csproj
dotnet sln %app%.sln add %app%.Domain/%app%.Domain.csproj
dotnet sln %app%.sln add %app%.Domain.Test/%app%.Domain.Test.csproj
dotnet sln %app%.sln add %app%.Infrastructure/%app%.Infrastructure.csproj

dotnet add %app%.WebUi/%app%.WebUi.csproj package Microsoft.EntityFrameworkCore
dotnet add %app%.WebUi/%app%.WebUi.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add %app%.WebUi/%app%.WebUi.csproj package Microsoft.EntityFrameworkCore.Tools

dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package Microsoft.EntityFrameworkCore
dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Tools


dotnet add %app%.Domain.Test/%app%.Domain.Test.csproj package Moq
@echo off
echo Update nuget packages
rem "https://www.reddit.com/r/dotnet/comments/1757s1o/upgrading_multiple_nuget_packages_in_jetbrains/?rdt=62769"
dotnet tool install --global dotnet-outdated-tool 
dotnet outdated --upgrade
:no
```

