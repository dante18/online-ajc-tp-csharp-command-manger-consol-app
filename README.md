# online-ajc-tp-csharp-command-manger-consol-app

## Objectif pédagogique
L'objectif de ce TP est de créer une application console servant à gérer le catalogue de produits d'une pizzeria, ainsi que la gestion des commandes et des utilisateurs.

Ce projet permettra de mettre en pratique :
- L’utilisation de la programmation orientée objet (POO) en C#
- L’accès aux données avec Entity Framework (Code First) et une base de données locale

## Énoncé
Vous devez concevoir une application WPF permettant de créer et gérer des commandes, des produits et utilisateur. L’application doit permettre les fonctionnalités suivantes :

- Création des entités correspondant au schéma.
- Créer une base de données : EatDomicile
- Insérer des données
- Créer les endpoints CRUD sur les entités suivantes :
  - Ingredient*
  - Drink*
  - Dough
  - Burger*
  - Pizza
  - Pasta
  - User*
  - Address*
- Créer un endpoint de création de commande
- Créer un ou plusieurs endpoint pour modifier le statut de la commande
- Créer les endpoints pour les requêtes suivantes :
  - Lister les utilisateurs qui ont effectué une commande
  - Lister les commandes exclusivement végétariennes
  - Récupérer le nombre de calories d’une commande
  - Lister les produits qui contiennent un allergène (Il faut donc ajouter l’information dans la base de données).
  - Lister toutes les commandes en cours
  - Etc.
