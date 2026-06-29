# Running the Application

## Prerequisites

* .NET 10 SDK
* Node.js LTS
* SQL Server (one of the following):

  * SQL Server Express (recommended for Windows)
  * LocalDB (Windows only, easiest setup)
  * OR Docker Desktop (optional alternative)

---

## Database Setup Options

### Option 1 (Recommended): SQL Server Express / LocalDB

Update the connection string in:

```
api/Sample.Api/appsettings.Development.json
```

Example (LocalDB):

```
Server=(localdb)\MSSQLLocalDB;Database=SampleAppDb;Trusted_Connection=True;
```

Example (SQL Server Express):

```
Server=localhost;Database=SampleAppDb;Trusted_Connection=True;TrustServerCertificate=True;
```

---

### Option 2 (Optional): Docker SQL Server

If Docker is installed:

```bash
docker compose up -d
```

Connection string:

```
Server=localhost,1433;Database=SampleAppDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;
```

---

## Running the API

```bash
cd api/Sample.Api
dotnet restore
dotnet run
```

API will run on:

```
https://localhost:5001
http://localhost:5000
```

Swagger available at:

```
/swagger
```

---

## Running the Frontend

```bash
cd web/sample-web
npm install
npm run dev
```

Frontend will run on:

```
http://localhost:5173
```

---

## Notes

* Ensure the API is running before starting the frontend.
* The frontend expects the API base URL configured in an environment file (`.env`).
* Database will be created automatically on first run (EF Core migrations or EnsureCreated).
