Markdown

# Auction Management System - Backend

ASP.NET Core Web API backend for our Auction Management System group project.  
Implements **Entity Framework Core** (Code-First with SQL Server) and a clean team workflow.

---

## 🚀 Features
- User management (register, login, CRUD, roles)
- Auction management (create, update, delete, list with filters)
- Bidding system (place bids, update current price)
- Notifications (outbid alerts, winner alerts)
- Swagger UI for API testing

---

## 📂 Project Structure
AuctionManagementSystem.sln → Visual Studio solution
AuctionManagementSystem/ → Source code
├── Controllers/ → API endpoints
├── Data/ → DbContext
├── Dtos/ → Clean request/response classes
├── Models/ → EF Core entities (User, Auction, Bid, Notification)
├── Services/ → Interfaces + Implementations
├── Migrations/ → EF Core migration files
├── Program.cs → Entry point
└── appsettings.json → DB connection config

text


---

## ⚙️ Setup
1. Clone repo:
   ```bash
   git clone https://github.com/YOUR_USERNAME/auction-management-system-backend.git
Configure SQL Server in appsettings.json:

JSON

"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=AuctionDB;Trusted_Connection=True;TrustServerCertificate=True;"
Run EF Core migration:

PowerShell

Update-Database
Start project:

Bash

dotnet run
API available at:
https://localhost:5001/swagger

🔀 Team Workflow
Work only in your feature branch (not main).
Always pull latest main before coding:
Bash

git checkout main
git pull origin main
git checkout feature/your-branch
git merge main
After coding → commit → push → open Pull Request → Team Lead merges.
🗄️ EF Core Migration Rules
InitialCreate migration ✅ already exists.
Everyone runs:
PowerShell

Update-Database
❌ Do NOT create duplicate InitialCreate migrations.
If new tables are needed → feature owner runs:
PowerShell

Add-Migration AddNewTable
Update-Database
…then pushes migration file → others pull and only run Update-Database.
