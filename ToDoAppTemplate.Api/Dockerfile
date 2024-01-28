FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ToDoAppTemplate.Api/ToDoAppTemplate.Api.csproj", "ToDoAppTemplate.Api/"]
COPY ["ToDoAppTemplate.Domain/ToDoAppTemplate.Domain.csproj", "ToDoAppTemplate.Domain/"]
COPY ["ToDoAppTemplate.Application/ToDoAppTemplate.Application.csproj", "ToDoAppTemplate.Application/"]
COPY ["ToDoAppTemplate.Data/ToDoAppTemplate.Data.csproj", "ToDoAppTemplate.Data/"]
COPY ["ToDoAppTemplate.Core/ToDoAppTemplate.Core.csproj", "ToDoAppTemplate.Core/"]
RUN dotnet restore "ToDoAppTemplate.Api/ToDoAppTemplate.Api.csproj"
COPY . .
WORKDIR "/src/ToDoAppTemplate.Api"
RUN dotnet build "ToDoAppTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ToDoAppTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoAppTemplate.Api.dll"]
