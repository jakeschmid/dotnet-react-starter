# dotnet-react-starter

A simple **Notes** full-stack sample application for onboarding intern/junior developers.  
It demonstrates the basic request flow: **React UI → ASP.NET Core API → SQL Server**.

## Tech stack

| Layer    | Technology |
|----------|-----------|
| Frontend | React 19, TypeScript 5.8, Vite 8, Tailwind CSS 4, TanStack React Query, Axios |
| Backend  | ASP.NET Core 10 Web API, EF Core 10, SQL Server |

---

## Repository structure

```
dotnet-react-starter/
├── api/
│   └── Sample.Api/          # ASP.NET Core 10 Web API
├── web/
│   └── sample-web/          # React + Vite SPA
├── README.md
└── .gitignore
```

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js LTS](https://nodejs.org/)
- SQL Server — one of:
  - **LocalDB** (Windows, easiest): installed with Visual Studio or the [SQL Server Express installer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  - **SQL Server Express** (Windows / Linux)
  - Any existing local SQL Server instance

---

## Backend setup

### 1. Configure the connection string

Open `api/Sample.Api/appsettings.Development.json` and update the connection string if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SampleAppDb;Trusted_Connection=True;"
  }
}
```

**SQL Server Express example:**
```
Server=localhost;Database=SampleAppDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### 2. Run the API

```bash
cd api/Sample.Api
dotnet restore
dotnet run
```

The API starts on:
- `https://localhost:5001`
- `http://localhost:5000`

Swagger UI is available at:
- `http://localhost:5000/swagger`

> The database is created automatically on first run and seeded with 5 sample notes.

---

## Frontend setup

### 1. Configure the API base URL

The file `web/sample-web/.env` already points to the default backend URL:

```
VITE_API_BASE_URL=http://localhost:5000
```

Change this if your API runs on a different port.

### 2. Run the frontend

```bash
cd web/sample-web
npm install
npm run dev
```

The frontend is available at:
- `http://localhost:5173`

> Make sure the API is running before opening the frontend.

---

## Features

- View all notes (newest first)
- Create a note
- Edit a note inline
- Delete a note

---

## Architecture notes

### Backend

```
NotesController  →  INotesService / NotesService  →  AppDbContext  →  SQL Server
```

- **Controllers** are thin — they only handle HTTP concerns.
- **Services** contain all business logic.
- **DTOs** (`CreateNoteRequest`, `UpdateNoteRequest`, `NoteResponse`) are used in place of EF entities.
- EF Core Code-First with `EnsureCreated()` on startup.

### Frontend

```
pages/Home  →  hooks/useNotes  →  services/notesApi (Axios)  →  API
```

- All API calls go through `services/notesApi.ts`.
- TanStack React Query handles data fetching, caching and mutation.
- Queries are invalidated after every mutation — no manual state updates.
