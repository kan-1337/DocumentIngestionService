FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY DocumentIngestionService.sln ./

COPY DocumentIngestion.Api/DocumentIngestion.Api.csproj DocumentIngestion.Api/
COPY Shared.Common/Shared.Common.csproj Shared.Common/

RUN dotnet restore DocumentIngestion.Api/DocumentIngestion.Api.csproj

COPY . .

WORKDIR /src/DocumentIngestion.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN mkdir -p /app/data

COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "DocumentIngestion.Api.dll"]
