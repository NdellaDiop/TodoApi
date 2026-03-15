# TodoApi

API web basée sur un contrôleur avec ASP.NET Core, Entity Framework InMemory, sécurisée avec JWT et ASP.NET Core Identity.

## Liens

- **Swagger (production)** : https://todoapi-3xoi.onrender.com/swagger
- **GitHub** : https://github.com/NdellaDiop/TodoApi

---

## Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Git
- Compte [Render](https://render.com) (pour le déploiement)

---

## Exécution en local

### 1. Cloner le dépôt
```bash
git clone https://github.com/NdellaDiop/TodoApi.git
cd TodoApi
```

### 2. Restaurer les packages
```bash
dotnet restore
```

### 3. Lancer l'application
```bash
dotnet run
```

### 4. Accéder à Swagger
```
http://localhost:5200/swagger
```

---

## Déploiement sur Render

### 1. Initialiser Git et pousser sur GitHub
```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin https://github.com/VOTRE_USERNAME/TodoApi.git
git push -u origin main
```

### 2. Créer un compte Render
Allez sur https://render.com et inscrivez-vous avec GitHub.

### 3. Créer un nouveau service sur Render
- Cliquez sur **New +** → **Web Service**
- Onglet **Public Git Repository**
- Collez l'URL : `https://github.com/NdellaDiop/TodoApi`
- Cliquez **Continue**

### 4. Configurer le service

| Champ | Valeur |
|-------|--------|
| Name | todoapi |
| Region | Frankfurt (EU) |
| Branch | main |
| Runtime | Docker |
| Instance Type | Free |

### 5. Créer le Dockerfile
Le projet contient un Dockerfile à la racine :
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000
ENTRYPOINT ["dotnet", "TodoApi.dll"]
```

### 6. Déployer
Render déploie automatiquement à chaque push sur la branche **main**.

Pour redéployer manuellement :
```bash
git add .
git commit -m "Update"
git push
```

---

## Endpoints disponibles

### Authentification (public)

| Méthode | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/Auth/register | Créer un utilisateur |
| POST | /api/Auth/register-admin | Créer un administrateur |
| POST | /api/Auth/login | Obtenir un token JWT |

### TodoItems (authentification requise)

| Méthode | Endpoint | Rôle requis | Description |
|---------|----------|-------------|-------------|
| GET | /api/TodoItems | Authentifié | Lire tous les todos |
| GET | /api/TodoItems/{id} | Authentifié | Lire un todo |
| POST | /api/TodoItems | Admin | Créer un todo |
| PUT | /api/TodoItems/{id} | Admin | Modifier un todo |
| DELETE | /api/TodoItems/{id} | Admin | Supprimer un todo |

---

## Guide d'utilisation de l'API

### Étape 1 — Créer un admin
```json
POST /api/Auth/register-admin
{
  "username": "admin",
  "email": "admin@todo.com",
  "password": "Admin@123456"
}
```

### Étape 2 — Se connecter
```json
POST /api/Auth/login
{
  "username": "admin",
  "password": "Admin@123456"
}
```
Réponse :
```json
{
  "token": "eyJhbGci..."
}
```

### Étape 3 — Utiliser le token dans Swagger
- Cliquez sur **Authorize 🔒**
- Entrez votre token (sans "Bearer")
- Cliquez **Authorize**

---

## Technologies utilisées

- ASP.NET Core 10
- Entity Framework Core InMemory
- ASP.NET Core Identity
- JWT Bearer Authentication
- NSwag / Swagger UI
- Render (hébergement cloud)

---

