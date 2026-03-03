# TCPortfolio - Photo Gallery

## 📌 About the Project
**TCPortfolio** is a high-performance web application designed to showcase amateur photography by **Tanguy Caillaud**. 

Beyond being a simple gallery, this project serves as a **technical laboratory**. It was built to demonstrate expertise in modern software architecture, master the latest features of **.NET 10**, and implement a robust CI/CD pipeline. The goal is to maintain a continuous technology watch (**Veille Technologique**) while delivering a production-ready product.

## 🚀 Key Features
* **Secure Photo Management:** Full CRUD for uploading and organizing photos.
* **Authentication & Authorization:** Secure login system using **JWT** (JSON Web Tokens) with role-based access control.
* **User Interactions:** Users can register and save their favorite photos to a personal list.
* **Interactive API Documentation:** Explore the API via a modern **Scalar** interface.

## 🛠 Tech Stack

### Backend (.NET 10)
* **Framework:** ASP.NET Core 10 (Web API)
* **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API)
* **ORM:** Entity Framework Core 9 (Code-First)
* **Database:** PostgreSQL
* **Mapping:** AutoMapper
* **Documentation:** Scalar (OpenAPI 3.1)
* **Security:** ASP.NET Core Identity + JWT

### Frontend (Angular)
* **Framework:** Angular 18/19+
* **State Management:** Signals / NgRx
* **Styling:** SCSS / Tailwind CSS

### DevOps & Tools
* **CI/CD:** Jenkins (Automated Build & Test)
* **Version Control:** Git
* **Testing:** xUnit / FluentAssertions

## 🏗 Methodology & Principles
To ensure high code quality and maintainability, the project strictly follows:
* **SOLID Principles:** For robust and scalable object-oriented design.
* **KISS (Keep It Simple, Stupid):** Avoiding over-engineering while solving complex problems.
* **DRY (Don't Repeat Yourself):** Reducing logic duplication across layers.
* **Clean Code:** Self-documenting code with meaningful naming and XML documentation.
3.  **Run Migrations:** `dotnet ef database update`
4.  **Run the App:** `dotnet run --project TCPortfolio.API`
