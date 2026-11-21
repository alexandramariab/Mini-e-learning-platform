# 📚 Mini e-Learning Platform

A modern short online course platform built with **ASP.NET Core MVC** and **C#**, utilizing **Entity Framework Core** for data management and **ASP.NET Identity** for secure authentication and role-based access control.

---

## 🌟 Key Features

### 👤 Role-Based Access Control (RBAC)
* **Unregistered Visitor:** Can browse the course catalog, view course presentations, and read reviews. Redirected to authentication when attempting restricted actions.
* **Student:** Can enroll in courses, manage a personal wishlist, write reviews, and access enrolled course lessons.
* **Instructor (Collaborator):** Can propose new courses for approval, manage lessons, and track course states.
* **Administrator:** Complete platform oversight—manages categories, courses, reviews, users, and reviews/approves instructor requests.
* **Instructor Request System:** Registered users can apply for the instructor role. Admins can approve or reject requests, and users can track their application status in real-time.

### 🗂️ Dynamic Categories
* Admins can create, edit, and delete course categories directly from the UI.
* Category names are unique and mandatory. Cascade deletion options safely handle associated courses.

### 📖 Course Management & Approval Workflow
* Courses contain titles, descriptions, presentation images, enrollment limits, levels (*Beginner, Intermediate, Advanced*), and lessons.
* **Lifecycle:** Courses proposed by instructors enter a **"Pending"** state. Admins review them, approve/reject them, and provide feedback.
* **Lessons:** Support text, uniformly sized images, and uploaded/embedded videos, ordered sequentially.

### 🛒 Enrollments & Wishlist
* **Enrollments:** Free, one-click course registration. Tracks available seats dynamically and prevents duplicate enrollments.
* **Wishlist:** Personal wishlist management without duplication, with direct enrollment capabilities from the wishlist page.
* **Access Control:** Lesson content is strictly restricted to enrolled students, course instructors, and administrators.

### ⭐ Reviews & Ratings
* Students can add, edit, and delete reviews (text and 1–5 star ratings).
* The system automatically computes and updates the average score for each course.

### 🔍 Search, Filter & Sort
* **Search:** Search courses by partial or full title matching.
* **Filters:** Filter courses by category and difficulty level.
* **Sorting:** Sort results in ascending/descending order based on star ratings and enrollment counts.

### 🤖 AI Course Assistant
* An interactive sidebar chat on each course page powered by course descriptions, lessons, and saved FAQs.
* Answers student queries (e.g., *“Do I need prior programming experience?”*, *“In which lesson is foreach explained?”*) and references specific lessons when available.

---

## 🛠️ Tech Stack

* **Backend:** C#, ASP.NET Core MVC
* **Data Access:** Entity Framework Core (EF Core)
* **Authentication & Authorization:** ASP.NET Identity
* **Frontend:** HTML5, CSS3, Bootstrap, Razor Views
* **Database:** SQL Server / LocalDB
