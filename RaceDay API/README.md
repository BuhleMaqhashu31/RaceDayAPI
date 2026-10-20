# RaceDay Web API

RaceDay is a full-stack RESTful Web API and relational database application built using ASP.NET Core and Entity Framework Core. The system manages event registrations, user roles, event categories, and participant race results with role-based authorization and SQL Server database persistence.

---

## Features

* **Role-Based Access Control:** Distinguishes between Organisers and Participants using secure session management.
* **Password Security:** Hashes user passwords securely using BCrypt.Net.
* **Event and Category Management:** Allows Organisers to create race events and configure associated distance categories.
* **Participant Enrollment:** Enables registered Participants to enroll in specific event categories.
* **Results Tracking:** Records and displays participant race completion times and final positions.
* **Database Resiliency:** Configured with Entity Framework Core retry logic to maintain stability during transient network failures.

---

## Database Architecture (Part 1)

The system uses a normalized 3NF relational SQL Server database (`RaceDayDb`) with explicit foreign key constraints and primary keys.

### Key Entities
* **Roles:** Stores user access levels (`Organiser` and `Participant`).
* **Users:** Stores account profiles, hashed credentials, and role associations.
* **Events:** Stores race event details including date, location, and organiser identification.
* **EventCategories:** Stores race sub-categories linked to specific events.
* **Enrolments:** Maps participants to their selected event categories.
* **Results:** Stores finishing times and rankings linked to valid enrolments.

---

## Backend Implementation (Part 2)

* **RaceDayContext.cs:** Configures Entity Framework Core mappings and explicitly defines primary key properties (`RoleId`, `UserId`, `EventId`, `CategoryId`, `EnrolmentId`, `ResultId`).
* **AuthController.cs:** Manages user account registration, password hashing, credential validation, and user sessions.
* **EventsController.cs:** Handles event listing and enforces Organiser-only restrictions for creating new events.
* **EnrolmentsController.cs:** Manages participant race registrations and retrieves personal enrolment history and results.

---

## Tech Stack

* **Framework:** .NET 8.0 / ASP.NET Core Web API
* **Database:** SQL Server Management Studio (SSMS)
* **ORM:** Entity Framework Core (`Microsoft.EntityFrameworkCore.SqlServer`)
* **Security:** BCrypt.Net-Next and ASP.NET Core In-Memory Sessions
* **Testing:** xUnit and EF Core In-Memory Database

---

## Getting Started

### Prerequisites
* Visual Studio 2022 with the .NET 8 SDK installed
* SQL Server Management Studio (SSMS)

