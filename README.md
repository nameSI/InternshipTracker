# Internship Tracker

A web app for tracking internship applications, built to manage my own job search. Each user has a private account and sees only their own applications.

## Features
- Register, log in and log out (ASP.NET Core Identity)
- Private data: every user sees only their own applications
- Create, edit, delete and search applications
- Dashboard with counts per status and upcoming deadlines

## Tech stack
C#, ASP.NET Core MVC (.NET 10), Entity Framework Core, SQL Server, Bootstrap

## Screenshots

### Login and registration
![Login page](docs/login_page.png)
![Register page](docs/register_page.png)

### Dashboard
![Dashboard](docs/dashboard.png)

### Managing applications
![Applications list](docs/job_application.png)
![Create an application](docs/create_application.png)
![Edit an application](docs/edit_application.png)
![Delete an application](docs/delete_application.png)
![Search applications](docs/search_application.png)

## Run it locally
1. Install Visual Studio 2026 (ASP.NET and web development workload) and SQL Server.
2. Clone this repository and open `InternshipTracker.slnx`.
3. In `InternshipTracker/appsettings.json`, set `DefaultConnection` to your SQL Server. For a default local instance it is already `Server=localhost;Database=InternshipTracker;Trusted_Connection=True;TrustServerCertificate=True`.
4. In a terminal in the `InternshipTracker` project folder, run: `dotnet ef database update`
5. Press F5, register an account and add some applications.

## What I learned
I rebuilt my coding skills by designing this project end to end. I learned how ASP.NET Core Identity handles login and password hashing, how to make sure each user can only see their own data, and how to debug package, migration and database connection problems.

## Roadmap
- Email reminders for upcoming deadlines
- Automated tests
- Live deployment


