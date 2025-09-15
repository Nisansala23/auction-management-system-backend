# Auction Management System - Backend

ASP.NET Core Web API backend for our **Auction Management System** group project.  
Implements a clean architecture with **Controllers**, **Services**, **DTOs**, and **Entity Framework Core** (Code-First Migrations) using SQL Server Express.

---

## 🚀 Features (Backend)
- User management (register, login, profile CRUD, roles)
- Auction management (create, update, delete, filter by seller/status)
- Bidding system (place bids, update current price)
- Notifications (outbid alerts, winner alerts)
- Database: SQL Server + EF Core Migrations
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


---

## 🛠️ Setup Instructions (Local Development)

1. Clone repository:
   ```bash
   git clone https://github.com/YOUR_USERNAME/auction-management-system-backend.git
   cd auction-management-system-backend

2.  Build backend:
  ```bash
    dotnet build

3. Configure DB Connection → in appsettings.json:
  ```bash
"ConnectionStrings": {
   "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=AuctionDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

4. Apply database migrations:
  ```bash
  Update-Database

5. Run backend:
  ```bash
  dotnet run

Swagger will be available at:
  https://localhost:5001/swagger


🔀 Git Workflow (Team Rules)
✅ Main Branch (main)

Always clean & stable code.
Nobody pushes directly to main.
✅ Feature Branches

Each member codes only in their branch:
feature/user-crud → Beginner (User CRUD API)
feature/auction-filters → Beginner (Auction filters, seller auctions)
feature/auction-crud → Intermediate (Auction CRUD)
feature/bids-read → Intermediate (Fetch bids by user/auction)
feature/authentication → Advanced (Register, Login, JWT)
feature/bids-logic-notifications → Advanced (Bidding + Notifications)


✅ Workflow

Always pull latest main:
  ```bash
git checkout main
git pull origin main


Switch to your feature branch:
  ```bash
git checkout feature/branchname
git merge main


After coding and testing → push:
  ```bash
git add .
git commit -m "Meaningful update message"
git push origin feature/branchname


Open a Pull Request (PR) to merge into main.
Leader (Team Lead) reviews → merges.
🗄️ Entity Framework Core (Migrations Rules)
Initial migration ✅ already created by Team Lead.



Every member, after pulling, just runs:
PowerShell
Update-Database
❌ Do NOT run Add-Migration InitialCreate again.
If new tables are added (e.g., Reports, Transactions):
That member runs:
PowerShell
Add-Migration AddReportsTable
Update-Database
Pushes changes (Migrations folder updated).

Other members: pull + run Update-Database.

🤝 Contribution Rules
Follow folder guidelines (Controllers → endpoints only, Services → logic only, Models → entities only).
Do not add random files/folders — ask Team Lead before.
Always test APIs in Swagger before committing.
Use meaningful commit messages ("Add Auction GetById endpoint", not "final fix").

🛡️ CI/CD with GitHub Actions
This repo includes a GitHub Actions workflow (.github/workflows/backend-ci.yml) that:

Restores dependencies
Builds the project
Runs tests (if added later)
Ensures broken code never reaches main

👥 Team Roles & Branches
Member	Skill Level	Branch	Responsibility
Member 1	Kaweesha Sathsarani 🌱	feature/user-crud	User Profile CRUD API
Member 2	Dasuni Rathnayaka🌱	feature/auction-filters	Auction filters/search
Member 3	Lochana Ehelapitiya⚡	feature/auction-crud	Auction CRUD
Member 4	Nisansala Sandeepani⚡	feature/bids-read	Fetch bids by user/auction
Member 5	Yasith Sanduruwan 🦸	feature/authentication	Register/Login, JWT
Member 6	Supasan Praharshana 🦸	feature/bids-logic-notifications	Place bid, enforce rules, notifications



