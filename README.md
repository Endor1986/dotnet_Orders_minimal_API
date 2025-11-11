<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet&logoColor=white">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-Minimal%20APIs-512BD4?logo=dotnet&logoColor=white">
  <img alt="EF Core / SQLite" src="https://img.shields.io/badge/EF%20Core-SQLite-003B57?logo=sqlite&logoColor=white">
  <img alt="Swagger/OpenAPI" src="https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=black">
  <img alt="Docker" src="https://img.shields.io/badge/Docker-available-2496ED?logo=docker&logoColor=white">
  <img alt="Tests" src="https://img.shields.io/badge/Tests-xUnit-blueviolet">
  <img alt="License" src="https://img.shields.io/badge/License-MIT-lightgrey.svg">
</p>

<h1 align="center">dotnet-orders-minimalapi</h1>
<p align="center">Eine kompakte <strong>ASP.NET Core 8 Minimal API</strong> für Bestellungen – mit <strong>EF Core (SQLite)</strong>, <strong>Swagger/OpenAPI</strong>, <strong>Docker</strong> und <strong>xUnit</strong>-Tests.</p>

<p align="center">
  <!-- Setze deinen echten Repo-Slug: Endor1986/dotnet-orders-minimalapi -->
  <a href="https://github.com/Endor1986/dotnet-orders-minimalapi/actions/workflows/ci.yml">
    <img alt=".NET CI" src="https://github.com/Endor1986/dotnet-orders-minimalapi/actions/workflows/ci.yml/badge.svg">
  </a>
</p>

---

## 🌟 Features
- **Minimal APIs**: schlanke Endpunkte ohne Controller-Overhead
- **Persistenz**: EF Core mit **SQLite** (lokal), leicht austauschbar
- **Dokumentation**: integriertes **Swagger/OpenAPI**
- **Qualität**: xUnit-Integrationstests, `Directory.Build.props` (Warnings as Errors), `.editorconfig`
- **CI**: GitHub Actions Workflow inkl. Format-Check & Coverage-Artifact
- **Container**: Multi-stage **Dockerfile** und optional `docker-compose.yml`
- **Komfort-Endpoints**: `/orders/latest`, `/orders/by-customer/{name}`

## 🧱 Tech Stack
- **Runtime**: .NET 8, ASP.NET Core Minimal APIs
- **DB/ORM**: SQLite + EF Core
- **Testing**: xUnit, Microsoft.AspNetCore.Mvc.Testing, FluentAssertions
- **CI/CD**: GitHub Actions
- **Docs & Tools**: Swagger, Postman-Collection, VS Code REST Client (`.http`)

## 📦 Projektstruktur
```
.
├─ src/
│  └─ OrderService/                 # API-Projekt (.csproj, Program.cs, Models, Data)
├─ tests/
│  └─ OrderService.Tests/           # Integration-Tests (WebApplicationFactory)
├─ docs/
│  ├─ WORKFLOW.md                   # End-to-End-Workflow (Projektablauf)
│  ├─ API-EXAMPLES.http             # Abfragen für VS Code REST Client
│  └─ BUGLOG.md                     # Kompakter Bug-Verlauf
├─ orders.postman_collection.json   # Postman-Collection
├─ .github/workflows/ci.yml         # CI-Pipeline (Format, Build, Test, Coverage)
├─ docker-compose.yml               # Optionaler lokaler Start (Compose)
└─ README.md
```

## 🚀 Schnellstart
Voraussetzungen: **.NET 8 SDK**

```bash
dotnet --version
# 8.x

# Restore, Build, Run
dotnet restore
dotnet build -c Release
dotnet run --project ./src/OrderService/OrderService.csproj -c Release --urls "http://localhost:8080"

# Swagger:
# http://localhost:8080/swagger
```

> 💡 Wenn der Port 8080 belegt ist:
> ```bash
> dotnet run --project ./src/OrderService/OrderService.csproj -c Release --urls "http://localhost:5080"
> ```

## 🔌 API-Überblick
- `GET  /health` – Healthcheck
- `GET  /orders` – Liste (neueste zuerst, DB-seitig via `CreatedAtUnixMs`)
- `GET  /orders/{id}` – Detail
- `GET  /orders/latest` – letzte Order
- `GET  /orders/by-customer/{name}` – Filter nach Kunde
- `POST /orders` – Anlegen (Body mit `customer`, `total`, optional `status`)
- `PUT  /orders/{id}` – Aktualisieren (teilweise Updates möglich)
- `DELETE /orders/{id}` – Löschen

👉 Beispiele findest du in **[docs/API-EXAMPLES.http](docs/API-EXAMPLES.http)**

## 🧪 Tests
```bash
# Vom Repo-Root
dotnet test ./tests/OrderService.Tests/OrderService.Tests.csproj -c Release
```

## 🧰 Qualität & DX
- **Warnings as Errors** & **AnalysisLevel** in `Directory.Build.props`
- **Code Style** via `.editorconfig`
- **CI**: `.github/workflows/ci.yml` – Restore → Format-Check → Build → Test (+Coverage)
- **Docs**: 
  - Workflow: **[docs/WORKFLOW.md](docs/WORKFLOW.md)**
  - Buglog: **[docs/BUGLOG.md](docs/BUGLOG.md)**

## 🐳 Docker (optional)
```bash
# Image bauen
docker build -t orders-api:dev -f ./src/OrderService/Dockerfile ./src/OrderService

# Starten
docker run --rm -p 8080:8080 orders-api:dev
# Swagger: http://localhost:8080/swagger
```

## 🧭 Workflow (Ablauf)
- Planung in Issues → Branching (`feature/<topic>`) → PR → Review
- Tests lokal & in CI, `dotnet format` vor Commit
- Deployment via Container oder direktes Publish (je nach Zielumgebung)
- Mehr im Dokument **[docs/WORKFLOW.md](docs/WORKFLOW.md)**

## 🧩 Bekannte Stolpersteine
- **SQLite & ORDER BY DateTimeOffset** → Wir sortieren nach `CreatedAtUnixMs` (INTEGER). Details im **[docs/BUGLOG.md](docs/BUGLOG.md)**.
- **Swagger „Undocumented“** → Paket `Microsoft.AspNetCore.OpenApi` + `using Microsoft.AspNetCore.OpenApi;` sicherstellen.

## 📄 Lizenz
Dieses Projekt steht unter der **MIT-Lizenz**. Siehe Datei `LICENSE`.

---

**© 2025 Phillip Kley** – Alle Rechte vorbehalten gemäß MIT-Lizenzbedingungen.
