namespace Cardgame;
// class qui contient les info que levenement affiche
public class NotificationJoueurEventArgs : EventArgs {
    public Joueur Joueur { get; }

    public NotificationJoueurEventArgs(Joueur joueur) {
        Joueur = joueur;
    }
}
