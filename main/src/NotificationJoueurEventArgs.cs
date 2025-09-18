namespace Devoir_1_INF1035;
// class qui contient les info que levenement affiche
public class NotificationJoueurEventArgs : EventArgs {
    public Joueur Joueur { get; }

    public NotificationJoueurEventArgs(Joueur joueur) {
        Joueur = joueur;
    }
}