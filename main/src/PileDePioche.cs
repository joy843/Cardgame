namespace Devoir_1_INF1035;

// Classe représentant une pile de pioche de cartes, implémentant l'interface IPileDeCartes
class PileDePioche : IPileDeCartes
{
    // Utilisation de la classe Stack pour gérer la pile de cartes
    private Stack<Carte> pioche = new Stack<Carte>();

    // Ajoute une carte en haut de la pile
    public void AjouterCarte(Carte carte)
    {
        pioche.Push(carte); // Ajoute la carte en haut de la pile
    }

    // Retire et retourne la carte située en haut de la pile
    public Carte RetirerCarte()
    {
        return pioche.Pop(); // Retire la carte du haut de la pile et la retourne
    }

    // Vérifie si la pile est vide
    public bool EstVide()
    {
        return pioche.Count == 0; // Retourne true si la pile est vide, sinon false
    }
}