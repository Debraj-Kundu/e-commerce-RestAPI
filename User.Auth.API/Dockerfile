#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["User.Auth.API/User.Auth.API.csproj", "User.Auth.API/"]
RUN dotnet restore "User.Auth.API/User.Auth.API.csproj"
COPY . .
WORKDIR "/src/User.Auth.API"
RUN dotnet build "User.Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Auth.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Auth.API.dll"]