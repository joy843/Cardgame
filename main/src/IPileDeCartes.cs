namespace Devoir_1_INF1035;

// Interface représentant une pile de cartes avec des opérations d'ajout et de retrait de carte
interface IPileDeCartes
{
    // Méthode pour ajouter une carte à la pile
    void AjouterCarte(Carte carte);

    // Méthode pour retirer une carte de la pile et la retourner
    Carte RetirerCarte();
}