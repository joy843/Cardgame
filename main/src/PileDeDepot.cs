namespace Cardgame;

//Création d'une classe représentant une pile de dépôt de cartes
public class PileDeDepot : IPileDeCartes
{
    //Déclaration des attributs de la classe
    private Stack<Carte> depot = new Stack<Carte>();//champ privé contenant les cartes dans la pile de dépot

    //Méthode pour ajouter une carte au sommet de la pile
    public void AjouterCarte(Carte carte)
    {
        depot.Push(carte);
        //Console.WriteLine($"{carte} a été ajoutée à la pile de dépôt.");
    }

    //Méthode pour retirer la carte du sommet de la pile et la retourner
    public Carte RetirerCarte()
    {
        return depot.Pop();
    }

    //Méthode pour vérifier si la pile de dépôt est vide
    public bool EstVide()
    {
        return depot.Count == 0;
    }
    
    //Méthode pour retourner la derniere carte au-dessus de la pile de depot
    public Carte derniereCartePile(){
        return depot.Peek();

    }
    
}
