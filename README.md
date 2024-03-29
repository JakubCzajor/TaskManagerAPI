# TaskManagerAPI

TaskManagerAPI is a ASP.NET 8 Web API project designed to manage tasks, categories and users. It provides RESTful endpoints for performing CRUD operations on these entities. Users can interact with the application via Swagger or Postman.

## Features

- **Tech Stack**: 
  - .NET 8 Core
  - Entity Framework
  - SQL Server
  - FluentValidation
  - AutoMapper
  - NLog
  - Swagger

- **Database Tables**:
  - Tasks
  - Categories
  - Users
  - Roles

- **Authentication/Authorization**:
  - JWT Bearer Token

## Project setup

1. Update the database connection string in `appsettings.json`:
```
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=true;"
```

2. Update the private key for JWT token generation in `appsettings.json`:
```
"JwtKey": "_____PRIVATE_KEY_DONT_SHARE_____"
```

3. Run this command in `Packet Manager Console` to create the necessary database schema:
```
update-database
```

## Usage

Once the application is running, you can interact with it using either Swagger or Postman.

To access Swagger UI:

1. Navigate to `https://localhost:<port>/swagger` in your web browser.
2. Explore available endpoints and perform operations.

## Endpoints

The API exposes the following endpoints:

- **Tasks**: 
  - `GET /api/task`: Get all tasks.
  - `GET /api/task/active`: Get active tasks.
  - `GET /api/task/done`: Get done tasks.
  - `GET /api/task/{id}`: Get a specific task by ID.
  - `POST /api/task`: Create a new task.
  - `PUT /api/task/{id}`: Update an existing task.
  - `DELETE /api/task/{id}`: Delete a task.

- **Categories**:
  - `GET /api/category`: Get all categories.
  - `GET /api/category/{id}`: Get a specific category by ID.
  - `POST /api/category`: Create a new category.
  - `PUT /api/category/{id}`: Update an existing category.
  - `DELETE /api/category/{id}`: Delete a category.

- **Accounts**:
  - `POST /api/account/register`: Register a new user.
  - `POST /api/account/login`: Login to the application and obtain a JWT token.
  - `GET /api/account`: Get all users.
  - `GET /api/account/profile`: Get user profile.
  - `PUT /api/account/profile`: Update user profile.
  - `PUT /api/account/password`: Update user password.
  - `PUT /api/account/role/{userId}`: Update user role.

## Authentication/Authorization

TaskManagerAPI utilizes JWT Bearer Token for authentication and authorization. To access protected endpoints, include the JWT token in the Authorization header of your requests:
 ```
 Bearer YOUR_JWT_TOKEN
 ``` 

### Role Access Levels

- **Admin**: 
  - Full access to all endpoints.
- **Manager**
  - Can view, create, update and delete tasks.
  - Can view, create, update and delete categories.
  - Can view all users and update their roles.
- **User**: 
  - Can view active tasks and set them as done.
  - Can view categories.
  - Can view, update personal information and password.
