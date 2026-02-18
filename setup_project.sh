#!/bin/bash

# Crear proyectos
dotnet new webapi -n Game.Api
dotnet new classlib -n Game.Application
dotnet new classlib -n Game.Domain
dotnet new classlib -n Game.Infrastructure

# Añadir a solution
dotnet sln add Game.Api/Game.Api.csproj
dotnet sln add Game.Application/Game.Application.csproj
dotnet sln add Game.Domain/Game.Domain.csproj
dotnet sln add Game.Infrastructure/Game.Infrastructure.csproj

# Configurar referencias
dotnet add Game.Application reference Game.Domain
dotnet add Game.Infrastructure reference Game.Application
dotnet add Game.Infrastructure reference Game.Domain
dotnet add Game.Api reference Game.Application

