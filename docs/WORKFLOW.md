<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet&logoColor=white">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-Minimal%20APIs-512BD4?logo=dotnet&logoColor=white">
  <img alt="EF Core / SQLite" src="https://img.shields.io/badge/EF%20Core-SQLite-003B57?logo=sqlite&logoColor=white">
  <img alt="Swagger/OpenAPI" src="https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=black">
  <img alt="Docker" src="https://img.shields.io/badge/Docker-available-2496ED?logo=docker&logoColor=white">
  <img alt="Tests" src="https://img.shields.io/badge/Tests-xUnit-blueviolet">
  <img alt="License" src="https://img.shields.io/badge/License-MIT-lightgrey.svg">
</p>

# End-to-End Workflow
> Stand: 2025-11-11

Dieser Workflow zeigt, **wie** entwickelt, getestet, gebaut, verpackt und ausgeliefert wird – klein, aber komplett.

## Inhalt
- [1) Planung & Issues](#1-planung--issues)
- [2) Branching & Commits](#2-branching--commits)
- [3) Entwicklung (Dev)](#3-entwicklung-dev)
- [4) Tests](#4-tests)
- [5) Build & CI](#5-build--ci)
- [6) Containerization](#6-containerization)
- [7) Deployment (Varianten)](#7-deployment-varianten)
- [8) Monitoring & Ops](#8-monitoring--ops)
- [9) Dokumentation](#9-dokumentation)

---

## 1) Planung & Issues
- Anforderungen in **Issues** erfassen (User Story, Akzeptanzkriterien).
- Labels: `feature`, `bug`, `docs`, `tech-debt`.
- Kanban: ToDo → In Progress → Review → Done.

## 2) Branching & Commits
- Branch-Namen: `feature/<topic>`, `fix/<topic>`.
- **Conventional Commits**: `feat: …`, `fix: …`, `docs: …`, `test: …`, `refactor: …`.
- PR-Checks & Review nutzen.

## 3) Entwicklung (Dev)
- **ASP.NET Core Minimal APIs** für schlanke Endpunkte.
- **EF Core** (SQLite lokal; auf andere DBs austauschbar).
- **Swagger/OpenAPI** als Developer-UX (lokal aktiv).
- Start: siehe [`README.md`](../README.md).

## 4) Tests
- **xUnit** Integration-Tests mit `WebApplicationFactory<Program>`.
- Lokal & CI: `dotnet test`.

## 5) Build & CI
- **GitHub Actions**: Restore → Format-Check → Build → Test (optional Coverage).

## 6) Containerization
- **Dockerfile** (Multi-stage), Port **8080**.
- Lokal per `docker build`/`docker run` oder `docker-compose.yml` (optional).

## 7) Deployment (Varianten)
- Docker Image → Registry → Server `pull & run`.
- Alternativ: Azure App Service / Container Apps / Kubernetes (später).

## 8) Monitoring & Ops
- Structured Logging (z. B. Serilog, optional).
- Healthcheck: `GET /health`.

## 9) Dokumentation
- **API Examples:** [`docs/API-EXAMPLES.http`](./API-EXAMPLES.http)
- **Buglog:** [`docs/BUGLOG.md`](./BUGLOG.md)
- **README:** [`../README.md`](../README.md)
