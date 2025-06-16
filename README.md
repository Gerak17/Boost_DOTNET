Gestion des œuvres artistiques en WPF avec EF Core et PostgreSQL  

    Projet WPF pour gérer une base d'œuvres artistiques via une interface moderne et un système CRUD complet. 

## À propos 

Ce projet permet de : 

    Visualiser des œuvres sous forme de cards 
    Ajouter, modifier ou supprimer  des œuvres
    Utiliser une base PostgreSQL  via Entity Framework Core
    Appliquer un thème Material Design  pour une interface moderne
    Gérer les données via un fichier appsettings.json
      
     

## Fonctionnalités

Ajout d’œuvre
Formulaire d’ajout avec validation

Modification	
Mise à jour en temps réel dans l’interface

Suppression
Confirmation avant suppression

Aperçu image	
Affichage dynamique depuis une URL

Interface moderne	
Thèmes Material Design intégrés

Base de données	
PostgreSQL + EF Core 8
 
 
## Technologies utilisées 

C# / .NET 8 
WPF  (Windows Presentation Foundation)
EF Core 8  avec Npgsql
MaterialDesignThemes.Wpf  et MaterialDesignColors 
PostgreSQL  comme moteur de base de données
     

## Structure du projet 
 
wpfdotnet/
│
├── MainWindow.xaml           ← Fenêtre principale avec liste de cards + code-behind   
├── App.xaml                  ← Point d'entrée de l'application + code-behind
├── appsetittings.example.json        ← Template pour la configuratoin de la chaîne de connexion
|
├── .gitignore/               ← Pour ignorer fichiers sensibles
|
├── Data/                     ← DbContext 
│   ├── AppDbContext.cs       ← Contexte EF Core
|
├── Migrations/               ← Migrations
|
├── Model/                    ← Entité Artpiece
│   └── Artpiece.cs
│
├── Service/                  ← Service ArtpieceService
│   └── ArtpieceService.cs
│
├── UI/                       ← Fichiers XAML et code-behind
|   |── EditWindow.xaml       ← Formulaire d'ajout / modification 
|   ├── CardItem.xaml         ← UserControl personnalisé pour les cartes 
│
└── wpfdotnet.csproj          ← Fichier projet .NET
 
 
 
## Prérequis 

Avant de commencer, assurez-vous d’avoir : 

    .NET SDK 8.0 installé
    PostgreSQL installé localement
    Chaîne de connexion correcte dans appsettings.example.json
    Package NuGet : Npgsql.EntityFrameworkCore.PostgreSQL
    Package NuGet : MaterialDesignThemes.Wpf, MaterialDesignColors
    
## Installation des dépendances

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version x.x.x
dotnet add package Microsoft.EntityFrameworkCore.Design --version x.x.x
dotnet add package MaterialDesignThemes.Wpf --version x.x.x
dotnet add package MaterialDesignColors --version x.x.x

## Démarrage rapide 

# Restaurer les packages
dotnet restore

# Générer les migrations
dotnet ef migrations add Init

# Appliquer la base
dotnet ef database update

# Lancer l'application
dotnet run