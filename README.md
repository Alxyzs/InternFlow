# 📌 InternFlow – Task & Project Management System

## 📖 Overview
InternFlow is a web-based task, project, and collaboration management system developed using **ASP.NET Core MVC**, **Entity Framework Core**, **SignalR**, and **layered architecture** principles.

The system simulates a real-world team workflow environment where users can manage projects, assign tasks, track progress, communicate via comments, and receive real-time notifications.

---

## 🏗️ Architecture
The project follows a clean **4-layer architecture**:InternFlow.EL  → Entity Layer (Database models)
InternFlow.DAL → Data Access Layer (Repository Pattern + EF Core)
InternFlow.BLL → Business Logic Layer (Services + Business Rules)
InternFlow.MVC → Presentation Layer (Controllers + Views)

This structure ensures separation of concerns, scalability, and maintainability.
---

## 🚀 Features

### 🔐 Authentication & Authorization
- JWT + Cookie based login system
- Role-based access control (Admin / User / Stajyer)
- Register & profile management

### 👥 User Management
- User listing, creation, editing
- Role management panel (Admin only)
- Profile settings with password change

### 📁 Project Management
- Create, edit, delete projects
- Project status filtering (Active / Passive / Pending)
- Project member tracking

### 📋 Task Management
- Create tasks with multi-user assignment
- Task status tracking (Active / Passive / Pending / Completed)
- Priority levels (Low / Normal / High)
- Due date with expiry detection
- My Tasks / Other Tasks separation with pagination

### 💬 Comment System
- Real-time comments via SignalR (no page reload)
- ProjectMember / Guest badge on comments
- Comment delete (own comments or Admin)

### 📊 Dashboard
- Total users, projects, active tasks, completed tasks
- Live statistics from database

### 📜 Activity Log
- Automatic logging on task create and status change
- Admin-only activity log panel

### 📋 Kanban Board
- Drag & drop task management (Admin only)
- Real-time status update via SignalR

### ⚡ SignalR Real-Time
- Task status change notifications
- Live comment system
- Project creation notifications

### 🎨 UI/UX
- Toastr notifications for all CRUD operations
- SweetAlert2 for profile updates
- Responsive Bootstrap 5 design
- Search and pagination support

---

## 🛠️ Technologies Used

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core 6 |
| Database | SQL Server / LocalDB |
| Real-Time | SignalR |
| Auth | JWT + Cookie Authentication |
| Frontend | HTML, CSS, Bootstrap 5, JavaScript |
| Notifications | Toastr.js, SweetAlert2 |
| Version Control | Git & GitHub |

---

## 🗄️ Database Design

### Entities
| Entity | Description |
|--------|-------------|
| User | System users with roles |
| Project | Project information |
| ProjectMember | Many-to-many: User ↔ Project |
| TaskItem | Task details, status, priority |
| TaskAssignee | Many-to-many: User ↔ Task |
| TaskComment | Task comments |
| ActivityLog | Audit trail for all changes |

### Relationships
