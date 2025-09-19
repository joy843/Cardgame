# Projet C# – Jeu de cartes

Ce projet a été réalisé dans le cadre du cours **INF1035-00 – Concepts avancés des objets**.  

Il a été pour moi une vraie opportunité de comprendre un besoin métier simple (les règles d’un jeu de cartes), le traduire en règles claires, modéliser des entités et interactions, puis valider tout cela avec un prototype en C#.

## Ma démarche 

### 1. Analyse et cadrage
J’ai commencé par **poser les bases fonctionnelles** :  
- Identification des **acteurs** (joueurs, table de jeu).  
- Définition des **entités** (carte, pile, paire).  
- Description des **règles métier** et simulation du jeu pour mieux visualiser (former des paires, tirer une carte, déposer une carte).  

### 2. Application des notes de cours
- Utilisation d’**interfaces** (`IPileDeCartes`) pour définir des comportements génériques.  
- Création de **classes spécialisées** (`PileDePioche`, `PileDeDepot`) pour refléter les différentes règles.  
- Mise en place d’**événements** (`TourDeJeuEventArgs`, `NotificationJoueurEventArgs`) pour simuler la communication.  

Ici, j’ai appris à **formaliser les règles métier** pour qu’elles soient traduisibles en code.  

### 3. Implémentation et validation
Avec le langage **C#**, j’ai implémenté :  
- Le **déroulement d’une partie** (`Program.cs`, `TableDeJeu.cs`).  
- La logique **tour par tour**.  
- La gestion des **paires de cartes et des notifications aux joueurs**.  

L’objectif n’était pas seulement de coder, mais de **valider que l’analyse tenait la route**.  

## Ce que j’ai appris
- Structurer une communication claire entre besoin et technique.  
- Expérimenter la **traduction fonctionnel → technique** avec C#.  

## Pour l'exécution du projet

### Prérequis
- **.NET 8 SDK** installé  

