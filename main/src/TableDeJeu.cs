namespace Cardgame;

// Classe représentant la table de jeu, avec une pioche, un dépôt et une liste de joueurs
class TableDeJeu
{
    // Propriétés pour la pioche de cartes, le dépôt de cartes, et la liste des joueurs
    public PileDePioche Pioche { get; set; }
    public PileDeDepot Depot { get; set; }
    public List<Joueur> Joueurs { get; set; }

    // Constructeur qui initialise la pioche, le dépôt et la liste des joueurs
    public TableDeJeu()
    {
        Pioche = new PileDePioche();
        Depot = new PileDeDepot();
        Joueurs = new List<Joueur>();
    }

    // Méthode pour remplir la pioche si elle est vide en utilisant les cartes du dépôt
    public void RemplirPiocheSiVide()
    {
        if (Pioche.EstVide()) // Vérifie si la pioche est vide
        {
            Console.WriteLine("La pioche est vide. Remplissage à partir de la pile de dépôt.");
            var depot = Depot;

            // Retire la dernière carte de la pile de dépôt pour la remettre plus tard
            Carte derniereCarte = depot.RetirerCarte();

            // Crée une liste temporaire pour les cartes du dépôt
            List<Carte> cartesDepot = new List<Carte>();
            while (!depot.EstVide()) // Déplace toutes les cartes du dépôt dans la liste temporaire
            {
                cartesDepot.Add(depot.RetirerCarte());
            }

            // Remet la dernière carte retirée dans le dépôt
            depot.AjouterCarte(derniereCarte);

            // Mélange les cartes dans la liste temporaire
            Random rand = new Random();
            for (int i = cartesDepot.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                Carte temp = cartesDepot[i];
                cartesDepot[i] = cartesDepot[j];
                cartesDepot[j] = temp;
            }

            // Ajoute chaque carte de la liste temporaire mélangée dans la pioche
            foreach (var carte in cartesDepot)
            {
                Pioche.AjouterCarte(carte);
            }

            Console.WriteLine("La pile de dépôt a été mélangée et ajoutée à la pioche.");
        }
    }
}
