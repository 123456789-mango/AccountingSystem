# Accounting System MVP - Multi Project Solution

## Solution Structure

AccountingSystem.sln
└── src
    ├── AccountingSystem.Api
    ├── AccountingSystem.Application
    ├── AccountingSystem.Domain
    ├── AccountingSystem.Infrastructure.SQLRepo
    └── WebApp
        ├── Controllers
        ├── Properties
        ├── Views
        ├── wwwroot
        └── admin-app

## Architecture

WebApp/admin-app (React)
        ↓ HTTP API
AccountingSystem.Api
        ↓
AccountingSystem.Application
        ↓
AccountingSystem.Domain
        ↑
AccountingSystem.Infrastructure.SQLRepo
        ↓
PostgreSQL / Supabase

## Run

1. Run database/01_schema.sql in Supabase SQL Editor.
2. Update src/AccountingSystem.Api/appsettings.json.
3. Open AccountingSystem.sln in Visual Studio.
4. Build Solution.
5. Start AccountingSystem.Api on http://localhost:5001.
6. In src/WebApp/admin-app run:
   npm install
   npm run dev
7. Open http://localhost:5173.

The WebApp project is included in the same Visual Studio solution to match the requested project structure.
