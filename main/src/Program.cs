using System;

namespace Devoir_1_INF1035 {
    class Program
{
    static void Main(string[] args)
    {
        TableDeJeu table = new TableDeJeu();

        // Ajout des joueurs
        table.Joueurs.Add(new Joueur("Audrey", "Marcy", 1));
        table.Joueurs.Add(new Joueur("  Lyn", "Joyce", 2));
        table.Joueurs.Add(new Joueur("Daouda", "Sele", 3));

        JeuxDePeche jeu = new JeuxDePeche(table, 6);
        //Demarre la partie
        jeu.DemarrerPartie();
    }
}
}