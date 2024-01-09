FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DZarsky.ToDoAppTemplate.Api/DZarsky.ToDoAppTemplate.Api.csproj", "DZarsky.ToDoAppTemplate.Api/"]
COPY ["DZarsky.ToDoAppTemplate.Domain/DZarsky.ToDoAppTemplate.Domain.csproj", "DZarsky.ToDoAppTemplate.Domain/"]
COPY ["DZarsky.ToDoAppTemplate.Application/DZarsky.ToDoAppTemplate.Application.csproj", "DZarsky.ToDoAppTemplate.Application/"]
COPY ["DZarsky.ToDoAppTemplate.Data/DZarsky.ToDoAppTemplate.Data.csproj", "DZarsky.ToDoAppTemplate.Data/"]
COPY ["DZarsky.ToDoAppTemplate.Core/DZarsky.ToDoAppTemplate.Core.csproj", "DZarsky.ToDoAppTemplate.Core/"]
RUN dotnet restore "DZarsky.ToDoAppTemplate.Api/DZarsky.ToDoAppTemplate.Api.csproj"
COPY . .
WORKDIR "/src/DZarsky.ToDoAppTemplate.Api"
RUN dotnet build "DZarsky.ToDoAppTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DZarsky.ToDoAppTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DZarsky.ToDoAppTemplate.Api.dll"]
