# Films API

API REST de gestion de films et séries, construite en C# / ASP.NET Core.

Projet d'apprentissage .NET développé en 2 semaines.

## Stack technique

- .NET 8 / ASP.NET Core
- Entity Framework Core + SQLite
- xUnit pour les tests

## Lancer le projet

```bash
dotnet run --project FilmsApi.Api
```
## Configuration

Copier `appsettings.Example.json` en `appsettings.json` et renseigner la clé API TMDB.

## Structure

```
FilmsApi.Api/
├── Controllers/   # Endpoints HTTP
├── Models/        # Entités métier
├── DTOs/          # Objets de transfert
├── Repositories/  # Accès aux données
├── Services/      # Logique métier
└── Exceptions/    # Exceptions custom
```