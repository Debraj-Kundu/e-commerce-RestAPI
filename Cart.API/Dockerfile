#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cart.API/Cart.API.csproj", "Cart.API/"]
COPY ["JWTAuthManager/JWTAuthManager.csproj", "JWTAuthManager/"]
COPY ["MessageEvents/MessageEvents.csproj", "MessageEvents/"]
RUN dotnet restore "Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/Cart.API"
RUN dotnet build "Cart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]