#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 4430

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MP.IPLocalizator.WebApi/MP.IPLocalizator.WebApi.csproj", "MP.IPLocalizator.WebApi/"]
COPY ["MP.IPLocalizator.Bootstrapper/MP.IPLocalizator.Bootstrapper.csproj", "MP.IPLocalizator.Bootstrapper/"]
COPY ["MP.IPLocalizator.Services/MP.IPLocalizator.Services.csproj", "MP.IPLocalizator.Services/"]
COPY ["MP.IPLocalizator.Business/MP.IPLocalizator.Business.csproj", "MP.IPLocalizator.Business/"]
COPY ["MP.IPLocalizator.IServices/MP.IPLocalizator.IServices.csproj", "MP.IPLocalizator.IServices/"]
COPY ["MP.IPLocalizator.Dto/MP.IPLocalizator.Dto.csproj", "MP.IPLocalizator.Dto/"]
COPY ["MP.IPLocalizator.Integration/MP.IPLocalizator.Integration.csproj", "MP.IPLocalizator.Integration/"]
COPY ["MP.IPLocalizator.Persistence/MP.IPLocalizator.Persistence.csproj", "MP.IPLocalizator.Persistence/"]
RUN dotnet restore "MP.IPLocalizator.WebApi/MP.IPLocalizator.WebApi.csproj"
COPY . .
WORKDIR "/src/MP.IPLocalizator.WebApi"
RUN dotnet build "MP.IPLocalizator.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MP.IPLocalizator.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MP.IPLocalizator.WebApi.dll"]

