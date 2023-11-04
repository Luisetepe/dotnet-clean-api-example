# Dotnet Clean Api Example

## Description

Example of a clean REST Api build with ASP.NET Core, FastEndpoints, MediatR and EntityFramework.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Entity Framework Core tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

## Installation

A step by step series of examples that tell you how to get a development environment running.

```bash
git clone https://github.com/Luisetepe/dotnet-clean-api-example.git
cd dotnet-clean-api-example
dotnet build
```

## Usage

First make sure you have a PostgreSQL database available in your local machine with these settings (or change both `appSettings.json` from the Seeding.Tool and the Api projects):

```
host=127.0.0.1
user=postgres
pass=postgrespw
```

Then you can apply migrations to create the database schema with:
```bash
dotnet ef database update -s src/Web/Artema.Platform.Api
```

After that, you can seed test data running the Seeding.Tool project with:
```bash
dotnet run --project src/Tools/Artema.Platform.Seeding.Tool
```

Finally, you can just build and run the Api project and test it for yourself:

```bash
dotnet run --project src/Web/Artema.Platform.Api/Artema.Platform.Api.csproj
```

## Running the tests

```bash
dotnet test
```
