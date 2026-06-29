# dotnet-react-starter

A simple **Notes** full-stack sample application for onboarding intern/junior developers.  
It demonstrates the basic request flow: **React UI → ASP.NET Core API → database**.

## Tech stack

| Layer    | Technology |
|----------|-----------|
| Frontend | React 19, TypeScript 5.8, Vite 8, Tailwind CSS 4, TanStack React Query, Axios |
| Backend  | ASP.NET Core 10 Web API, EF Core 10, SQL Server / InMemory |

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

## Backend setup

The backend supports two database providers, controlled by a single flag in `appsettings.Development.json`:

```json
"DatabaseProvider": "InMemory"   // or "SqlServer"
```

---

### ⚡ Quick start — InMemory (no database required)

This is the default. No SQL Server installation needed.

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
cd api/Sample.Api
dotnet restore
dotnet run
```

Data lives only for the lifetime of the process and is re-seeded on every restart.

---

### 🗄️ Realistic setup — SQL Server / LocalDB

Switch to the real SQL Server provider to mirror the production stack.

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download) + a local SQL Server instance:

| Option | Server value |
|--------|-------------|
| LocalDB (Windows, easiest) | `(localdb)\MSSQLLocalDB` |
| SQL Server Express | `.\SQLEXPRESS` |
| Existing local instance | `localhost` |

**1. Set the provider and connection string** in `api/Sample.Api/appsettings.Development.json`:

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SampleAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**2. Run the API:**

```bash
cd api/Sample.Api
dotnet restore
dotnet run
```

The database is created automatically on first run (`EnsureCreated`) and seeded with 5 sample notes.

---

### API URLs

The API starts on:
- `https://localhost:5001`
- `http://localhost:5000`

Swagger UI: `http://localhost:5000/swagger`

---

## Frontend setup

### 1. Configure the API base URL

The file `web/sample-web/.env` already points to the default backend URL:

```
VITE_API_BASE_URL=http://localhost:5000
```

Change this if your API runs on a different port.

### 2. Run the frontend

**Prerequisites:** [Node.js LTS](https://nodejs.org/)

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
NotesController  →  INotesService / NotesService  →  AppDbContext  →  InMemory or SQL Server
```

- **Controllers** are thin — they only handle HTTP concerns.
- **Services** contain all business logic.
- **DTOs** (`CreateNoteRequest`, `UpdateNoteRequest`, `NoteResponse`) are used in place of EF entities.
- EF Core Code-First with `EnsureCreated()` + seeding on startup (works for both providers).

### Frontend

```
pages/Home  →  hooks/useNotes  →  services/notesApi (Axios)  →  API
```

- All API calls go through `services/notesApi.ts`.
- TanStack React Query handles data fetching, caching and mutation.
- Queries are invalidated after every mutation — no manual state updates.
