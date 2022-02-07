# Data Mapper for dotnet

[![Nuget](https://img.shields.io/nuget/v/Buhlergroup.DataMapper?color=blue)](https://www.nuget.org/packages/Buhlergroup.DataMapper/)
[![CI - Build & Test](https://github.com/buhlergroup/data-mapper-dotnet/actions/workflows/CI-build-test.yml/badge.svg)](https://github.com/buhlergroup/data-mapper-dotnet/actions/workflows/CI-build-test.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=buhlergroup_data-mapper-dotnet&metric=alert_status)](https://sonarcloud.io/dashboard?id=buhlergroup_data-mapper-dotnet)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=buhlergroup_data-mapper-dotnet&metric=coverage)](https://sonarcloud.io/dashboard?id=buhlergroup_data-mapper-dotnet)

The **data-mapper-dotnet** allows developers to map data from one schema to another by defining a mapping file that can be managed by non-technical staff.

![Data Mapper](./docs/assets/data-mapper.svg)

## Idea

The data mapper can be used as part of an interface between two IT systems.

![System Context](./docs/assets/l1-system-context.dio.svg)

The developer can focus on implementing the interface while the project manager can define the mapping in a json file.
This way the interface can easily be adjusted if by a project manager without the need of a developer.

## How to use

There are two parts to the library to use it. One is the technical implementation for the developer and one is the mapping for the non-technical staff.

### Library

1. Install the [package via NuGet](https://www.nuget.org/packages/Buhlergroup.DataMapper/)
2. If you're using dependency injection you can use the `AddDataMapper` extension method.

```csharp
using Buhlergroup.DataMapper;
...
var host = new HostBuilder()
    .ConfigureServices(s =>
    {
        ...
        s.AddDataMapper();
    })
    .Build();
```

3. Now an instance of `IMapper` can be injected into any class.

Check the [Development Docs](./docs/Development.md) to get an overview of how the library can be used and how it's structured.

### Mapping

Check the [Mappiong Docs](./docs/Mapping.md) to see how the mapping file works and what it can do for you.

## Contribute

Visit the [contribution page](https://github.com/buhlergroup/data-mapper-dotnet/contribute) to see how you can contribute.
