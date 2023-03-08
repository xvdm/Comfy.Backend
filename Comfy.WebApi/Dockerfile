#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Comfy.WebApi/Comfy.WebApi.csproj", "Comfy.WebApi/"]
RUN dotnet restore "Comfy.WebApi/Comfy.WebApi.csproj"
COPY . .
WORKDIR "/src/Comfy.WebApi"
RUN dotnet build "Comfy.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Comfy.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comfy.WebApi.dll"]