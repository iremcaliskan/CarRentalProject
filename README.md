# Car Rental Project a.k.a 'Rent A Car'

## Back-End Side of Car Rental Project
- Car Rental Project was created by taking inspiration from the car rental systems. 
- The project was created in accordance with the **SOLID** rules using the **Enterprise-Layered Architecture** structure and written in **C#** language.
- In this project, **Plug-and-Play** structures were established and the **Clean-Code** techniques were used.
- Appropriate **Design Patterns** were selected and adapted to the project.
- The project has been created in a format that **does not resist development, technical change and innovation**.

### Enterprise-Layered Architecture
- **Entities**: Entity Layer of the project and database tables are mapped to project objects and **DTOs** are used for filtering and extending object properties.
- **DataAccess**: Data Access Layer of the project and the project connected with the database and includes **CRUD** operations.
- **Business**: Business Layer of the project and the project includes various business rules such as **Data Controls, Validations, IoC Container and Authorization
  Controls**.
- **Core**: Core Layer of the project and used for general and universal operation.
- **WebAPI**: Restful API Layer of the project and includes HTTP Methods.

### Contents
- .Net Standard 2.0 & .Net Core 3.1
- Entity Framework
- SQL Server
- Repository Pattern
- Result Structure
  - Success
    - Success Result
    - Success Data Result
  - Error 
    - Error Result
    - Error Data Result 
- Aspect Oriented Programming
  - Cross Cutting Concerns
    - Security 
      - Encryption
      - Hashing
      - JWT
    - Caching
      - Microsoft
    - Logging
      - Serilog
        -File Logger 
    - Performance
    - Transaction
    - Validation
      - Fluent Validation 
- Autofac
  - IoC Container
  - Interceptors
- Extensions
  - ICollection<Claim>
  - Claims Principal
  - IServiceCollection
- Restful API
