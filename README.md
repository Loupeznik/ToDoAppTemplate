# ToDo app template API

A template repository for a ToDo app API with .NET8 and PostgreSQL. REPR (FastEndpoints), CQRS (MediatR) and partially Clean Architecture patterns are used.
The API is easily extensible and can be used as a starting point for a new project.

Functionality:

- Create, read, update and delete ToDos
- User registration, login, option to reset user's password. Accounts are stored in the database, authentication is done using JWT.
- Email templates and sending emails via Smtp or SendGrid is supported.

## Prerequisites

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Dotnet ef](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- Optional: [Docker](https://docs.docker.com/get-docker/) and [Docker Compose](https://docs.docker.com/compose/install/)

## Applying the database migrations

The database migrations are located in the `ToDoAppTemplate.Data` project. The following commands can be run from the solution root directory:

```powershell
# Create a new migration
dotnet ef migrations add MigrationName --project ./ToDoAppTemplate.Data --startup-project ./ToDoAppTemplate.Api
# Apply a migration
dotnet ef database update --project ./ToDoAppTemplate.Data --startup-project ./ToDoAppTemplate.Api
```

## Running the API

Either run the API by running `docker-compose up` from the root of the solution. This will start the API and a PostgreSQL database in Docker containers.

Or run the API from Visual Studio or the command line. The connection string to the database can be configured in the `appsettings.json` file.
