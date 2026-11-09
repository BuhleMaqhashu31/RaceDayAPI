# RaceDay

RaceDay is a full-stack web application built using ASP.NET Core Web API, Entity Framework Core, an ASP.NET Core MVC front-end, Azure Blob Storage, and Docker containerization[cite: 1, 2, 3].

## Features
* **Role-Based Access Control:** Distinguishes between Organisers and Participants using secure session management[cite: 1, 2, 11].
* **Event & Category Management:** Allows Organisers to create events and configure distance categories[cite: 1, 12, 13].
* **Participant Enrolment & Results:** Enables participants to register for events and view historical results[cite: 1, 14, 15, 16].
* **Azure Blob Storage:** Securely handles profile pictures and event banner uploads via the API[cite: 1, 17, 18].
* **Docker Containerisation:** Packages the MVC front-end inside a container communicating with the host API[cite: 2, 3, 23].

---

## Database Architecture (Part 1)
The system uses a normalized 3NF relational SQL Server database (`RaceDayDb`) with explicit foreign key constraints[cite: 4, 43].
* **Roles & Users:** Manages access levels and hashed user account profiles[cite: 9, 11].
* **Events & Categories:** Stores race details, locations, types, and sub-categories[cite: 13, 17].
* **Enrolments & Results:** Maps participant entries to categories and tracks finish times[cite: 14, 15].

---

## Backend Implementation (Part 2)
* **RaceDayContext.cs:** Configures Entity Framework Core mappings and primary key constraints.
* **AuthController.cs:** Manages user registration, password hashing, and session cookie validation[cite: 10].
* **EventsController.cs & EnrolmentsController.cs:** Handles event filtering, organiser restrictions, and participant registrations[cite: 12, 15].

---

## MVC Web Application & Docker (Part 3)
* **RaceDay.Mvc:** ASP.NET Core MVC front-end consuming the Part 2 RESTful API via `RaceDayApiClient`[cite: 1, 5, 7, 8].
* **Dockerfile:** Multi-stage build recipe packaging the MVC application for container deployment[cite: 24, 25].
* **Docker Execution:** Run via solution root build and run commands linking to host API (`host.docker.internal`)[cite: 23, 25, 37].

---

## Tech Stack
* **Framework:** .NET 8 / ASP.NET Core Web API & MVC[cite: 3, 5]
* **Database:** SQL Server Management Studio (SSMS) & EF Core[cite: 4, 43]
* **Storage & Containerisation:** Azure.Storage.Blobs & Docker Desktop[cite: 17, 21]
* **Testing:** xUnit testing framework[cite: 46]
