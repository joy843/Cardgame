namespace Cardgame;

// Structure représentant une carte
public struct Carte
{
    //Déclaration des propriétés de la carte
    public Couleur Couleur { get; }
    public Valeur Valeur { get; }

    //Déclaration du constructeur
    public Carte(Couleur couleur, Valeur valeur)
    {
        Couleur = couleur;
        Valeur = valeur;
    }

    //Déclaration de la méthode ToString pour l'affichage
    public override string ToString()
    {
        return $"{Valeur} de {Couleur}";
    }
    
}
