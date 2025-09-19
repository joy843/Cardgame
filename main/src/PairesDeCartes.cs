namespace Cardgame;

using System;
using System.Collections.Generic;

// Classe gérant un jeu complet de 52 cartes
class PaireDeCartes
{
    // Liste de cartes représentant le jeu de 52 cartes
    private List<Carte> cartes;

    // Constructeur de la classe PaireDeCartes, initialise un jeu de 52 cartes avec toutes les combinaisons de couleurs et valeurs possibles
    public PaireDeCartes()
    {
        cartes = new List<Carte>();
        foreach (Couleur couleur in Enum.GetValues(typeof(Couleur))) // Boucle sur chaque couleur disponible
        {
            foreach (Valeur valeur in Enum.GetValues(typeof(Valeur))) // Boucle sur chaque valeur de carte
            {
                cartes.Add(new Carte(couleur, valeur)); // Ajoute une carte de la couleur et de la valeur actuelles
            }
        }
    }

    // Méthode pour afficher toutes les cartes du jeu dans la console
    public void AfficherCartes()
    {
        int i = 0;
        cartes.Count(); // Compte le nombre total de cartes dans le jeu
        
        foreach (var carte in cartes)
        {
            Console.WriteLine(carte); // Affiche chaque carte dans la console
            i++;
        }
        Console.WriteLine(cartes.Count()); // Affiche le nombre total de cartes dans la console
    }

    // Méthode pour mélanger les cartes dans un ordre aléatoire
    public void Melanger()
    {
        Random rand = new Random(); // Génère un nouvel objet Random pour des valeurs aléatoires
        int n = cartes.Count; // Nombre total de cartes
        while (n > 1) // Tant qu'il y a plus d'une carte à mélanger
        {
            n--;
            int k = rand.Next(n + 1); // Sélectionne une position aléatoire parmi les cartes restantes
            Carte valeur = cartes[k]; // Échange les cartes entre la position aléatoire et la dernière position non mélangée
            cartes[k] = cartes[n];
            cartes[n] = valeur;
        }
    }

    // Méthode pour distribuer un certain nombre de cartes depuis le début du jeu
    public List<Carte> DistribuerCartes(int nombre)
    {
        List<Carte> main = new List<Carte>(); // Liste représentant la main de cartes distribuées
        for (int i = 0; i < nombre; i++)
        {
            if (cartes.Count == 0) break; // Arrête la distribution si le jeu est vide
            main.Add(cartes[0]); // Ajoute la première carte du jeu à la main
            cartes.RemoveAt(0); // Retire la carte du jeu
        }
        return main; // Retourne la liste de cartes distribuées
    }

    // Vérifie si le jeu de cartes est vide
    public bool EstVide()
    {
        return cartes.Count == 0; // Retourne vrai si le jeu ne contient plus de cartes, faux sinon
    }
}
