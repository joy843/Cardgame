    using System;

    namespace Devoir_1_INF1035;



    class JeuxDePeche {
        public event EventHandler<TourDeJeuEventArgs>TourDeJeuEvent;
        public delegate void NotificationJoueurEventHandler(object sender, NotificationJoueurEventArgs e);
        public event NotificationJoueurEventHandler NotificationJoueurEvent;


        private TableDeJeu table;
        private int cartesAPiocher=0;
        private int nombreDeCartesParJoueur;

        int indexJoueurSuivant;
//Constructer de la classe JeuDePeche
        public JeuxDePeche(TableDeJeu table, int nombreDeCartesParJoueur) {
            this.table=table;
            this.nombreDeCartesParJoueur=nombreDeCartesParJoueur;
        }
//Methode pour demarrer la partie 
        public void DemarrerPartie() {
            if (nombreDeCartesParJoueur < 5 || nombreDeCartesParJoueur > 8) {
                throw new ArgumentException("Le nombre de cartes par joueur doit être entre 5 et 8.");
            }

            // Mélanger et distribuer les cartes
            PaireDeCartes jeuDeCartes=new PaireDeCartes();
            jeuDeCartes.Melanger();

            foreach (var joueur in table.Joueurs) {
                joueur.Main=jeuDeCartes.DistribuerCartes(nombreDeCartesParJoueur);
            }

            //Afficher main du Joueur

            AfficherMainJoueurs();

            // Remplir la pioche avec le reste des cartes
            while ( !jeuDeCartes.EstVide()) {
                table.Pioche.AjouterCarte(jeuDeCartes.DistribuerCartes(1).First());
            }

            // Choisir un joueur aléatoirement pour commencer la partie et deposer la premiere carte sur la pile de depot
            Random rand=new Random();
            int indexPremierJoueur=rand.Next(table.Joueurs.Count);
            var premierJoueur=table.Joueurs[indexPremierJoueur];

            // Vérifier si la pioche est vide avant de commencer le tour
            table.RemplirPiocheSiVide();

            // Le premier joueur pose une carte sur le dépôt
            var cartePremierJoueur=premierJoueur.JouerCarte(0);
            table.Depot.AjouterCarte(cartePremierJoueur);

            Console.WriteLine($"{premierJoueur.Nom} {premierJoueur.Prenom} commence la partie en posant {cartePremierJoueur} sur le dépôt.");

            // Passer au joueur suivant
            indexJoueurSuivant=(indexPremierJoueur + 1) % table.Joueurs.Count;
            

            // Commencer le jeu avec le joueur suivant
            Jouer(indexJoueurSuivant);
        }
         
        // Methode pour gerer le déroulement du jeu
        private void Jouer(int indexJoueur) {
            bool jeuEnCours=true;
            bool directionInverse=false; // Pour gérer la direction du jeu
             //Abonnement à l'événement
            this.TourDeJeuEvent+=(sender, e)=> {
                Console.WriteLine($"Dernière carte sur le dépôt : {table.Depot.derniereCartePile()}");
            };
            // tant que le jeu est en cours
            while (jeuEnCours) {
                var joueur=table.Joueurs[indexJoueur];
                Console.WriteLine($"Tour de {joueur.Nom} {joueur.Prenom}");

                // Logique de jeu : jouer une carte ou piocher
                bool carteJouee=false;
                Carte carteDepot=table.Depot.RetirerCarte();
                table.Depot.AjouterCarte(carteDepot);  
                // Remettre la carte de dépôt sur la pile
                          
        
                for (int i=0; i < joueur.Main.Count; i++) {
                    Carte carte=joueur.Main[i];

                    // Utilisation du switch pour gérer les cartes spéciales
                    switch (carte.Valeur) {
                       
                        case Valeur.Valet: if (carteDepot.Valeur !=Valeur.Sept) 
                         // ne peut pas être posé sur un 7

                            {
                            joueur.Main.RemoveAt(i);
                            table.Depot.AjouterCarte(carte);
                            Console.WriteLine($"{joueur.Nom} {joueur.Prenom} joue {carte} et change la couleur.");

                            // Logique pour changer la couleur au random
                            Array couleurs=Enum.GetValues(typeof(Couleur));
                            Couleur couleurChoisie=(Couleur)couleurs.GetValue(new Random().Next(couleurs.Length));
                            Console.WriteLine($"La nouvelle couleur choisie est : {couleurChoisie}");

                            // Remplacer carteDepot par la nouvelle carte avec la couleur choisie
                            carteDepot=new Carte(couleurChoisie, Valeur.Valet);
                            table.Depot.AjouterCarte(carteDepot);

                            carteJouee=true;
                        }

                        break;

                        case Valeur.As: // As fait sauter le tour du prochain joueur

                        if (carte.Couleur==carteDepot.Couleur || carte.Valeur==carteDepot.Valeur) {
                            joueur.Main.RemoveAt(i);
                            table.Depot.AjouterCarte(carte);
                            Console.WriteLine($"{joueur.Nom} {joueur.Prenom} joue {carte} et le prochain joueur saute son tour.");
                            // Passer au joueur suivant (en sautant)
                            indexJoueur=(indexJoueur + 1) % table.Joueurs.Count;
                            carteJouee=true;
                        }
                        

                        break;
                         // dix renverse la direction de jeu
                        case Valeur.Dix: if (carte.Couleur==carteDepot.Couleur || carte.Valeur==carteDepot.Valeur) {
                            joueur.Main.RemoveAt(i);
                            table.Depot.AjouterCarte(carte);
                            Console.WriteLine($"{joueur.Nom} {joueur.Prenom} joue {carte} et change la direction du jeu.");
                            directionInverse= !directionInverse;
                            carteJouee=true;
                        }

                        break;
                            // Sept pour faire piocher deux carte au joueur suivant ou contre attaque si il a un 7 dans sa main
                        case Valeur.Sept: if (carteDepot.Couleur==carte.Couleur || carteDepot.Valeur==carte.Valeur) {
                            joueur.Main.RemoveAt(i);
                            table.Depot.AjouterCarte(carte);
                            Console.WriteLine($"{joueur.Nom} {joueur.Prenom} joue {carte} et oblige le joueur suivant à piocher 2 cartes.");
                            cartesAPiocher=2;
                            carteJouee=true;

                            // Passer au joueur suivant immédiatement
                            indexJoueur=(indexJoueur + 1) % table.Joueurs.Count;

                            // Le joueur suivant doit jouer immédiatement
                            var prochainJoueur=table.Joueurs[indexJoueur];
                            bool aContreAttaque=false;

                            for (int j=0; j < prochainJoueur.Main.Count; j++) {
                                if (prochainJoueur.Main[j].Valeur==Valeur.Sept) {
                                    // Le joueur suivant contre-attaque avec un autre 7
                                    var carteContreAttaque=prochainJoueur.JouerCarte(j);
                                    table.Depot.AjouterCarte(carteContreAttaque);
                                    Console.WriteLine($"{prochainJoueur.Nom} {prochainJoueur.Prenom} contre-attaque avec {carteContreAttaque}.");
                                    cartesAPiocher=2;
                                    aContreAttaque=true;
                                    break;
                                }
                            }

                            if ( !aContreAttaque) {
                                // Le joueur suivant pioche les cartes s'il ne contre-attaque pas
                                Console.WriteLine($"{prochainJoueur.Nom} {prochainJoueur.Prenom} doit piocher {cartesAPiocher} cartes.");

                                for (int j=0; j < cartesAPiocher; j++) {
                                    prochainJoueur.AjouterCarteDansMain(table.Pioche.RetirerCarte());
                                }

                                cartesAPiocher=2;
                                carteJouee=true;

                                // Passer le tour du joueur suivant après avoir pioché
                                Console.WriteLine($"{prochainJoueur.Nom} {prochainJoueur.Prenom} passe son tour après avoir pioché.");

                            }
                        }

                        break;



                        default: // Sinon on compare la couleur ou la valeur

                        if (carte.Couleur==carteDepot.Couleur || carte.Valeur==carteDepot.Valeur) {
                            joueur.Main.RemoveAt(i);
                            table.Depot.AjouterCarte(carte);
                            Console.WriteLine($"{joueur.Nom} {joueur.Prenom} joue {carte}");
                            carteJouee=true;
                            var carteChoisie= ChoisirCarteMinimale(joueur, carteDepot);
                        }

                        break;
                    }

                        //Notifie si un joueur finis bientot
                     if (joueur.Main.Count == 1) {
                       Console.WriteLine($"{joueur.Nom} {joueur.Prenom} a bientot finie, il lui reste une carte");
        
                        // Déclencher l'événement pour notifier les autres joueurs
                         OnNotificationJoueur(new NotificationJoueurEventArgs(joueur));
         
                     }



                    if (carteJouee) {
                        // Déclencher l'événement de tour de jeu
                        OnTourDeJeu(new TourDeJeuEventArgs(joueur, carteDepot, carte));
                        break; // Sortir de la boucle si une carte a été jouée
                    }


                }
                // Si il na pas de carte correspondant

                if ( !carteJouee) {
                    Console.WriteLine($"{joueur.Nom} {joueur.Prenom} doit piocher une carte");
                    joueur.AjouterCarteDansMain(table.Pioche.RetirerCarte());
                }
                //Affichage quand un joueur a gagner
                if (joueur.Main.Count==0) {

                    Console.WriteLine($"{joueur.Nom} {joueur.Prenom} a gagné la partie !");

                    jeuEnCours=false;
                }

                // Passer au joueur suivant
                indexJoueur=directionInverse ? (indexJoueur - 1 + table.Joueurs.Count) % table.Joueurs.Count : (indexJoueur + 1) % table.Joueurs.Count;

                System.Threading.Tasks.Task.Delay(2000).Wait();
            }
            // Vérifier si un adversaire a une seule carte
       
    
            if (jeuEnCours==false) { 
                // si le jeu nest pas en cours calcul des point

                foreach (var joueurFinal in table.Joueurs) {
                    var scoreFinal=joueurFinal.CalculerBilan();
                    Console.WriteLine($"Score final de {joueurFinal.Nom} : {scoreFinal} points");
                }

            }
            
        }
         // Méthode pour déclencher l'événement notifier quand un joueur a une seule carte
      protected virtual void OnNotificationJoueur(NotificationJoueurEventArgs e) {
        NotificationJoueurEvent?.Invoke(this, e);
    }

        // Méthode pour déclencher l'événement
        protected virtual void OnTourDeJeu(TourDeJeuEventArgs e) {
            TourDeJeuEvent?.Invoke(this, e);
        }

       //Methode pour afficher la main du joueur
        public void AfficherMainJoueurs() {
            foreach (var joueur in table.Joueurs) {
                Console.WriteLine($"Main de {joueur.Nom} {joueur.Prenom}:");

                foreach (var carte in joueur.Main) {
                    Console.WriteLine($" {carte}");
                }

                Console.WriteLine(" ");
            }


        }
        //Logique pour minimiser le decompte de point a la fin
        
        private Carte ChoisirCarteMinimale(Joueur joueur, Carte carteDepot) {
        Carte carteChoisie = new  Carte();

    foreach (var carte in joueur.Main) {
        if (carte.Couleur == carteDepot.Couleur || carte.Valeur == carteDepot.Valeur) {
            if (carte.Valeur < carteChoisie.Valeur) {
                carteChoisie = carte;
            }
        }
    }

    return carteChoisie;
}



    }