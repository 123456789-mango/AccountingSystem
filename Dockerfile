# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY AccountingSystem.sln ./

# Copy each project's .csproj first (enables Docker layer caching —
# restore only reruns if a .csproj actually changes)
COPY src/AccountingSystem.Api/*.csproj src/AccountingSystem.Api/
COPY src/AccountingSystem.Application/*.csproj src/AccountingSystem.Application/
COPY src/AccountingSystem.Domain/*.csproj src/AccountingSystem.Domain/
COPY src/AccountingSystem.Infrastructure.SQLRepo/*.csproj src/AccountingSystem.Infrastructure.SQLRepo/

RUN dotnet restore AccountingSystem.sln

# Copy the rest of the source and publish just the API project
COPY . ./
RUN dotnet publish src/AccountingSystem.Api/AccountingSystem.Api.csproj -c Release -o /app/publish

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AccountingSystem.Api.dll"]