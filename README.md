# Hotel Booking System

## Overview

This is a simple Hotel Booking System API built with ASP.NET Core Web API. It allows managing hotel rooms with basic CRUD operations.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/) (optional, by default connection string points to localhost)

## Getting Started

1.  **Clone the repository**
2.  **Configure Database**
    -   Update `appsettings.json` with your PostgreSQL connection string.
    -   Apply migrations:
        ```bash
        dotnet tool install --global dotnet-ef
        cd HotelBookingSystem.Api
        dotnet ef database update
        ```
3.  **Run the Application**
    ```bash
    dotnet run --project HotelBookingSystem.Api
    ```
    The API will be available at `http://localhost:5038` (or similar, check output).
    Swagger UI is available at `/swagger` in Development environment.

## Running Tests

To run the unit tests:

```bash
dotnet test
```

## Project Structure

-   `HotelBookingSystem.Api`: Main Web API project.
-   `HotelBookingSystem.Tests`: Unit tests project (xUnit).

## API Endpoints

-   `GET /api/rooms`: Get all rooms.
-   `GET /api/rooms/{id}`: Get a room by ID.
-   `POST /api/rooms`: Create a new room.
-   `PUT /api/rooms/{id}`: Update an existing room.
-   `DELETE /api/rooms/{id}`: Delete a room.
