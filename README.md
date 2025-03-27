# ServicesPortal

## Design
- Uses layered architecture pattern.
- Inversion of Control pattern is used. i.e. Business Layer is dependant on Data Access Layer but not vice versa. Same for API layer.
- Data Access layer uses design patterns as Unit Of Work and Repository.
- API layer uses Adapter design pattern.
- API Layer contains partial implementation for JWT Token APIs with refresh token strategy 

## Practice followed
- API Layer follows REST principles and proper naming conventions for API URLs.
- API Layer uses extension methods, dependancy injection for services and exception middleware
- API Layer does validations using Fluent Validations
- API Layer includes Tests using NUnit, NSubstitute and DeepEquals
- Business layer uses FluentValidations for DTO validations
- Data Access Layer fetches records without tracking wherever applicable
- Data Access Layer uses Unt Of Work for Products and Item repositories
- Data Access Layer has 'ScaffoldingScript.txt' file, which is script for generating models and database context class for 'Database First' approach
- Included database project as 'Products.Database'

## Deployment
- Create SQL Server database on Azure SQL Server from Azure portal
- Run scripts using database project 'Products.Database'
- Go to Publish website option in Visual Studio 
- Select previously created database 
- Publish website to Azure App service 
