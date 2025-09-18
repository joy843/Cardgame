namespace Devoir_1_INF1035;

// Classe représentant une personne avec un nom et un prénom
public class Personne
{
    // Propriété pour le nom de la personne
    public string Nom { get; set; }

    // Propriété pour le prénom de la personne
    public string Prenom { get; set; }

    // Constructeur de la classe Personne, initialisant le nom et le prénom
    public Personne(string nom, string prenom)
    {
        Nom = nom;      // Initialise la propriété Nom
        Prenom = prenom; // Initialise la propriété Prenom
    }
}