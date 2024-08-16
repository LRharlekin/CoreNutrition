

# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# copy csproj files and restore as distinct layers
COPY ["src/CoreNutrition.Api/CoreNutrition.Api.csproj", "CoreNutrition.Api/"]
COPY ["src/CoreNutrition.Contracts/CoreNutrition.Contracts.csproj", "CoreNutrition.Contracts/"]
COPY ["src/CoreNutrition.Infrastructure/CoreNutrition.Infrastructure.csproj", "CoreNutrition.Infrastructure/"]
COPY ["src/CoreNutrition.Application/CoreNutrition.Application.csproj", "CoreNutrition.Application/"]
COPY ["src/CoreNutrition.Domain/CoreNutrition.Domain.csproj", "CoreNutrition.Domain/"]

# restore all projects
RUN dotnet restore "CoreNutrition.Api/CoreNutrition.Api.csproj"

# copy rest of source code
COPY ["src/CoreNutrition.Api", "CoreNutrition.Api/"]
COPY ["src/CoreNutrition.Contracts", "CoreNutrition.Contracts/"]
COPY ["src/CoreNutrition.Infrastructure", "CoreNutrition.Infrastructure/"]
COPY ["src/CoreNutrition.Application", "CoreNutrition.Application/"]
COPY ["src/CoreNutrition.Domain", "CoreNutrition.Domain/"]

# build
WORKDIR /src/CoreNutrition.Api
RUN dotnet build "CoreNutrition.Api.csproj" -c Release -o /app/build

# Stage 2: Publish stage
FROM build AS publish
RUN dotnet publish "CoreNutrition.Api.csproj" -c Release -o /app/publish

###### publish folder is built

# Stage 3: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5089
ENV ASPNETCORE_HTTPS_PORTS=5090
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certs/aspnetapp.pfx

EXPOSE 5089 5090
WORKDIR /app
COPY --from=publish /app/publish .
# copy ssl cert
RUN mkdir -p /app/certs
COPY ["certs/aspnetapp.pfx", "app/certs/"]
# run artifacts from publish folder
ENTRYPOINT [ "dotnet", "CoreNutrition.Api.dll" ]