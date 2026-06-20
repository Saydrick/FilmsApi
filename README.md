# Films API

API REST de gestion de films et séries, construite en C# / ASP.NET Core.

Projet d'apprentissage .NET.

## Stack technique

- .NET 10 / ASP.NET Core
- Entity Framework Core + SQLite
- xUnit + Moq pour les tests
- API TMDB pour l'enrichissement des données

## Architecture

Architecture en 3 couches avec Repository pattern :
Controller → Service → Repository (interface) → EF Core / Mémoire

- **Controllers** : endpoints HTTP, codes de retour REST
- **Services** : logique métier, validation, exceptions custom
- **Repositories** : accès aux données (interchangeable mémoire ↔ EF Core)
- **DTOs** : objets de transfert dédiés, séparés des modèles internes

## Lancer le projet

```bash
git clone https://github.com/Saydrick/FilmsApi.git
cd FilmsApi
cp FilmsApi.Api/appsettings.Example.json FilmsApi.Api/appsettings.json
# Renseigner la clé API TMDB dans appsettings.json
dotnet ef database update --project FilmsApi.Api
dotnet run --project FilmsApi.Api
```

## Lancer les tests

```bash
dotnet test
```
## Endpoints principaux

| Méthode | Route | Description |
|---|---|---|
| GET | /api/films | Liste tous les films |
| GET | /api/films/{id} | Récupère un film |
| POST | /api/films | Crée un film |
| PUT | /api/films/{id} | Met à jour un film |
| DELETE | /api/films/{id} | Supprime un film |
| POST | /api/films/{id}/enrich | Enrichit via TMDB |

Mêmes endpoints disponibles sous `/api/series`.

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

## Ce que j'améliorerais avec plus de temps

- Authentification JWT
- Pagination sur les listes
- Recherche multi-critères avec query params
- Déploiement sur Azure / Railway
- Intégration IA pour la recherche et suggestion de films/series