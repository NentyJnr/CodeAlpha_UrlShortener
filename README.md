# Simple URL Shortener

A lightweight URL shortener service built with ASP.NET Core. It shortens long URLs and redirects users to the original ones using clean code architecture and Entity Framework Core.

## Features
- Shorten any valid long URL
- Redirect users from short URL to original
- Structured response model
- Clean code (controller + service + interface)
- EF Core database support
- Swagger API documentation

## Tech Stack
- ASP.NET Core
- Entity Framework Core (Code-First)
- SQL Server (or SQLite for local testing)
- Swagger for API UI

## How to Run
1. Clone the repo
2. Set your DB connection string in `appsettings.json`
3. Run migrations (`dotnet ef database update`)
4. Run the project (`dotnet run`)
5. Test via Swagger: `https://localhost:{port}/swagger`

## 🔗 Sample API Endpoints
- `POST /api/url/shorten` — Shorten a long URL
- `GET /api/url/{code}` — Redirect to original URL
- `GET /api/url/resolve/{code}` — Get original URL without redirect (for testing)

PRs and suggestions are welcome!
