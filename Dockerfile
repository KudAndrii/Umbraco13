FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source
COPY Umbraco13.csproj .
COPY . .

RUN dotnet restore
RUN dotnet publish Umbraco13.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_ENVIRONMENT=Production

WORKDIR /app

COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Umbraco13.dll"]