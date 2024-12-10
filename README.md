# Welcom to the dotnet-webapp-ec2 Project!

The purpose of this project is for me to learn how to create and code a .NET webapp and deploy to an AWS EC2 instance via a CI/CD mechanism.  For development I am currently
using .NET 8/C# 12.  I also develop on Windows (for now) and deploy to Linux.  (Yes, .NET runs on Linux.)

## Project Contents:
You are currently at the top of the .NET "solution" and each folder contains the individual "projects" defined below:
* **dotnet-webapp-ec2**:
  A .NET MVC webapp which:
  * Uses the "mocK" [Northwind database](https://www.google.com/search?q=northwind+database&oq=northwind+data) data in [SQLite](https://www.sqlite.org/).  The Northwind DB
    contains mock data for products, customers, employeess, etc. and coupled with a DB of your choice, provides a nice backend "sandbox" to learn with.
  * Utilitizes .NET Identity for user logins, registrations, forgotten password, etc.  This was scaffolded in and resides in the Areas folder.  I have updated the auto-
    generated scaffold code to add the FirstName & LastName fields and to update the look and feel.
  * Uses .NET Entity Framework Core ORM.
* **Northwind.DataContext.Sqlite**:
  Contains the DataContext, Extension and Logger classes for the main, Northwind database.  The data context for the .NET Identity tables was scaffolded into the dotnet-webapp-ec2 project.
* **Northwind.EntityModels.Sqlite**:
  Contains the EF Core (ORM) models for the Northwind database.
* **NorthwindDb**:
  This is the only folder that is not a project.  It just contains the Northwind database file.
* **UserManagement**:
  Was originally designed to house user management logic, but was replaced with the scaffolded ASP.NET Core Identity in the dotnet-webapp-ec2 project.  I am keeping this around for now
  for the PasswordHash logic.
