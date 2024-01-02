FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Eoss.Backend.Web.Host/Eoss.Backend.Web.Host.csproj", "src/Eoss.Backend.Web.Host/"]
COPY ["src/Eoss.Backend.Web.Core/Eoss.Backend.Web.Core.csproj", "src/Eoss.Backend.Web.Core/"]
COPY ["src/Eoss.Backend.Application/Eoss.Backend.Application.csproj", "src/Eoss.Backend.Application/"]
COPY ["src/Eoss.Backend.Core/Eoss.Backend.Core.csproj", "src/Eoss.Backend.Core/"]
COPY ["src/Eoss.Backend.EntityFrameworkCore/Eoss.Backend.EntityFrameworkCore.csproj", "src/Eoss.Backend.EntityFrameworkCore/"]
WORKDIR "/src/src/Eoss.Backend.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Eoss.Backend.Web.Host", "src/Eoss.Backend.Web.Host"]
COPY ["src/Eoss.Backend.Web.Core", "src/Eoss.Backend.Web.Core"]
COPY ["src/Eoss.Backend.Application", "src/Eoss.Backend.Application"]
COPY ["src/Eoss.Backend.Core", "src/Eoss.Backend.Core"]
COPY ["src/Eoss.Backend.EntityFrameworkCore", "src/Eoss.Backend.EntityFrameworkCore"]
WORKDIR "/src/src/Eoss.Backend.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Eoss.Backend.Web.Host.dll"]
