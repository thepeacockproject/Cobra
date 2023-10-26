# Development
This document described how to get started with the project.

# Add database migration to project
Run the following command from the `Src\Cobra.Server` folder:
```
dotnet ef migrations add [Name] --project ../Cobra.Server.Database
```

Where `[Name]` is replace with the name of the migration you wish to add.

# Apply database migration to database
Run the following command from the `Src\Cobra.Server` folder:
```
dotnet ef database update --project ../Cobra.Server.Database
```