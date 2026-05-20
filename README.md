# 📌 InternFlow – Task & Project Management System (Web + MVC + EF Core)

## 📖 Overview

InternFlow is a **web-based task, project, and collaboration management system** developed using **ASP.NET Core MVC, Entity Framework Core, and layered architecture principles**.

The system simulates a real-world team workflow environment where users can manage projects, assign tasks, track progress, and analyze performance through a dashboard.

It is designed as a **collaborative internship project**, focusing on clean architecture, Git workflow, and scalable backend development practices.

---

## 🏗️ Architecture

The project follows a clean layered architecture:

- **MVC Layer (UI)** → Web interface (Views, Controllers)
- **BLL (Business Logic Layer)** → Business rules and service logic
- **DAL (Data Access Layer)** → Repository pattern and EF Core operations
- **EL (Entity Layer)** → Database entities and models

This structure ensures separation of concerns, scalability, and maintainability.

---

## 🌐 System Architecture Flow

User (Browser)
↓
ASP.NET Core MVC
↓
BLL (Services)
↓
DAL (Repository Pattern)
↓
Entity Framework Core
↓
SQL Server Database

---

## 🚀 Features

- 👤 User management system (basic structure)
- 📁 Project creation and management
- 📋 Task creation and assignment
- 🔄 Task status tracking (To Do / In Progress / Done)
- 💬 Task comments system
- 📊 Dashboard with live statistics
- 🧠 Activity logging system (audit trail)
- 🗂️ Project-member relationship (many-to-many)
- ⚙️ Clean layered architecture implementation
- 🔍 LINQ-based filtering and queries

---

## 🛠️ Technologies Used

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server / LocalDB
- LINQ
- HTML / CSS
- JavaScript
- Git & GitHub

---

## 🗄️ Database Design

Core entities:

- **User**
- **Project**
- **TaskItem**
- **ProjectMember**
- **TaskComment**
- **ActivityLog**

Relationships:

- User → Tasks (1-N)
- Project → Tasks (1-N)
- Project ↔ Users (N-N)
- Task → Comments (1-N)
- Task → Activity Logs (1-N)

---

## 🔄 System Workflow

1. Users are assigned to projects
2. Projects contain multiple tasks
3. Tasks are assigned to specific users
4. Users update task status and add comments
5. All changes are recorded in ActivityLog
6. Dashboard displays real-time statistics

---

## 📊 Dashboard Features

- Total projects count
- Total tasks count
- Completed tasks
- In-progress tasks
- User-based task distribution
- Recent activity logs

---

## 👨‍💻 My Role in the Project

This project was developed as a **collaborative internship project**.

My responsibilities:

- Designed and implemented backend architecture
- Built Entity Framework Core models and DbContext
- Implemented repository pattern (DAL layer)
- Developed business logic services (BLL layer)
- Created dashboard statistics in HomeController
- Managed database migrations and structure design
- Ensured clean layered architecture implementation

---

## 📂 Project Structure

InternFlow
│
├── InternFlow.MVC → UI Layer (Controllers + Views)
├── InternFlow.BLL → Business Logic Layer
├── InternFlow.DAL → Data Access Layer (Repositories)
├── InternFlow.EL → Entity Models
└── Database (SQL Server)


---

## 📌 Current Status

The project is currently in **active development phase**.

Planned future improvements:

- 🔐 Authentication & Authorization (Login system)
- ⚡ SignalR real-time updates
- 📊 Advanced analytics dashboard
- 🎯 Role-based access control (Admin / User)
- 📎 File upload system
- 🎨 UI/UX improvements

---

## 💡 Key Learnings

- ASP.NET Core MVC architecture
- Layered software design principles
- Entity Framework Core (Code First approach)
- Repository pattern implementation
- Git branching and team collaboration
- Database design and normalization
- Real-world project structuring

---

## 🧠 Summary

InternFlow demonstrates a **real-world team-based software development workflow**, focusing on clean architecture, maintainable code structure, and scalable backend design using modern .NET technologies.

---

## 📌 Note

This project is developed for educational and internship purposes and will continue to evolve with new features and improvements.
