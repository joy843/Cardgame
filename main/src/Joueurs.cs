namespace Cardgame;

// la class joueurs derive de Personnes
public class Joueur : Personne
{
    public int Id { get; set; }
    public List<Carte> Main { get; set; }

    public Joueur(string nom, string prenom, int id) : base(nom, prenom)
    {
        Id = id;
        Main = new List<Carte>();
    }

    // Ajouter une carte à la main du joueur
    public void AjouterCarteDansMain(Carte carte)
    {
        if (Main.Count < 8)
        {
            Main.Add(carte);
        }
    }

    // Jouer une carte de la main
    public Carte JouerCarte(int index)
    {
        Carte carte = Main[index];
        Main.RemoveAt(index);
        return carte;
    }

    // Calculer le bilan de points
    public int CalculerBilan()
    {
        int totalPoints = 0;
        foreach (var carte in Main)
        {
            switch (carte.Valeur)
            {
                case Valeur.As:
                    totalPoints += 11;
                    break;
                case Valeur.Valet:
                case Valeur.Dame:
                case Valeur.Roi:
                    totalPoints += 2;
                    break;
                default:
                    totalPoints += (int)carte.Valeur;
                    break;
            }
        }
        return totalPoints;
    }
}
