using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlateauDeJeu Plateau = new PlateauDeJeu();
            Case Case0 = new Depart(0,400,200);
            Case Case1 = new Ville(1, 60, 30, "Lecce",true);
            Case Case2 = new Ville(2, 80, 40, "Syracuse", true);
            Case Case3 = new Ville(3, 100, 50, "Verone", true);
            Case Case4 = new Ville(4, 120, 60, "Catane", true);
            Case case5 = new Case(5); //case sans effet
            Case Case6 = new Ville(6, 120, 60, "Cagliari", true);
            Case Case7 = new Ville(7, 140, 70, "Bologne", true);
            Case Case8 = new Ville(8, 160, 80, "Pise", true);
            Case Case9 = new CaseCDC(9); 
            Case Case10= new Ville(10, 180, 90, "Turin", true);
            Case Case11 = new Ville(11, 200, 100, "Florence", true);
            Case Case12 = new Ville(12, 220, 110, "Venise", true);
            Case Case13 = new Case(13); // Prison
            Case Case14 = new Ville(14, 240, 120, "Palerme", true);
            Case Case15 = new Ville(15, 260, 130, "Milan", true);
            Case Case16 = new Ville(16, 280, 140, "Rome", true);
            Case Case17 = new CaseCDC(17); 
            Case Case18 = new Ville(18, 300, 170, "Naples", true);
            Case Case19 = new Ville(19, 400, 200, "Avellino", true);
            

            Plateau.ajoutercase(Case0);
            Plateau.ajoutercase(Case1);
            Plateau.ajoutercase(Case2);
            Plateau.ajoutercase(Case3);
            Plateau.ajoutercase(Case4);
            Plateau.ajoutercase(case5);
            Plateau.ajoutercase(Case6);
            Plateau.ajoutercase(Case7);
            Plateau.ajoutercase(Case8);
            Plateau.ajoutercase(Case9);
            Plateau.ajoutercase(Case10);
            Plateau.ajoutercase(Case11);
            Plateau.ajoutercase(Case12);
            Plateau.ajoutercase(Case13);
            Plateau.ajoutercase(Case14);
            Plateau.ajoutercase(Case15);
            Plateau.ajoutercase(Case16);
            Plateau.ajoutercase(Case17);
            Plateau.ajoutercase(Case18);
            Plateau.ajoutercase(Case19);
            

            Console.WriteLine("Vous êtes arrivé dans mon monopoly");
            Console.WriteLine("-------------------------------");


            Console.Write("Entrez le nombre de joueurs : "); //demande le nombre de joueur
            int nombreJoueurs = int.Parse(Console.ReadLine());
            Joueur[] joueurs = new Joueur[nombreJoueurs];   // instancie une liste au nombre des joueurs

            for (int i = 0; i < nombreJoueurs; i++)
            {
                Console.Write($"Entrez le nom du joueur {i + 1} : ");
                string nom = Console.ReadLine();
                Console.Write($"Entrez le prénom du joueur {i + 1} : ");
                string prenom=Console.ReadLine();
                Joueur joueur = new Joueur(nom, prenom,Case0);   //Met les joueurs dans la liste
                joueurs[i] = joueur;
            }




            while (joueurs.Length > 1)  //début de la boucle du jeu
            {
                foreach (Joueur joueur in joueurs) 
                {
                    int de = joueur.LancerDe();  // utilisation de la méthode lancerde
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine(joueur.Nom + " a lancé le dé et a obtenu " + de);
                    Console.WriteLine("----------------------------------------------");
                    joueur.Avancer(de, Plateau);
                    Console.WriteLine(joueur);

                    if (joueur.Macase is Ville)  // instruction si il est dans une ville
                    {
                        Ville villeActuelle = (Ville)joueur.Macase;
                        Console.WriteLine(villeActuelle);

                        if (villeActuelle.Libre)
                        {
                            Console.WriteLine("----------------------------------------------");
                            Console.WriteLine("Voulez-vous acheter cette ville ? (O/N)");
                            Console.WriteLine("----------------------------------------------");
                            string reponse = Console.ReadLine();

                            if (reponse.ToUpper() == "O")  // si la ville est libre on peut l'acheter ou  non
                            {
                                if (joueur.Portefeuille >= villeActuelle.PrixAchat)
                                {
                                    villeActuelle.Acheter(joueur,reponse); // utilisation de la méthode acheter si la ville est achete
                                }
                                else
                                {
                                    Console.WriteLine("Vous n'avez pas assez d'argent pour acheter cette ville !");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cette ville appartient à " + villeActuelle.Proprietaire.Nom + ". Vous devez payer €" + villeActuelle.PrixNuitee + " pour y passer la nuit.");
                            joueur.PayerPrixNuitee(villeActuelle.PrixNuitee);
                        }
                    }
                    else if (joueur.Macase is Depart)
                    {
                        Depart departActuel = (Depart)joueur.Macase;

                        // Si le joueur s'arrête sur la case Départ, il gagne 400 euros
                        if (de + Plateau.MesCases.IndexOf(joueur.Macase) == Plateau.MesCases.IndexOf(departActuel))
                        {
                            joueur.Portefeuille += departActuel.RapportArret;
                            Console.WriteLine(joueur.Nom + " s'est arrêté sur la case Départ et a gagné €" + departActuel.RapportArret);
                            Console.WriteLine("----------------------------------------------");
                            
                        }
                        // Si le joueur passe par la case Départ sans s'arrêter, il gagne 200 euros
                        else if (de + Plateau.MesCases.IndexOf(joueur.Macase) > Plateau.MesCases.IndexOf(departActuel))
                        {
                            joueur.Portefeuille += departActuel.RapportPassage;
                            Console.WriteLine(joueur.Nom + " a passé la case Départ et a gagné €" + departActuel.RapportPassage);
                        }
                    }

                    else if (joueur.Macase is CaseCDC)
                    {
                        CaseCDC caseActuelle = (CaseCDC)joueur.Macase;
                        Console.WriteLine(caseActuelle);

                        // Le joueur tire une carte et effectue l'action correspondante
                        caseActuelle.Action(joueur);

                        
                        
                    }
                    else
                    {
                        // Afficher les informations de la case
                        Console.WriteLine(joueur.Macase);
                        Console.WriteLine("----------------------------------------------");
                        
                       
                    }
                    Console.WriteLine("Voulez-vous continuer à jouer ? (O/N)");
                    string rep = Console.ReadLine();

                    switch (rep.ToUpper())
                    {
                        case "O":
                            Console.WriteLine("Vous avez choisi de continuer à jouer.");
                            Console.Clear();
                            break;
                        case "N":
                            Console.WriteLine("Vous avez choisi d'arrêter de jouer.");
                            joueurs = joueurs.Where(val => val != joueur).ToArray();
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Réponse invalide. Veuillez répondre O pour oui ou N pour non.");
                            break;
                    }

                    if (joueur.Portefeuille <= 0) // si le joueur n'a plus d'argent, il perd
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Le joueur " + joueur.Nom + " " + joueur.Prenom + " n'a plus d'argent, il perd !");
                        Console.WriteLine("----------------------------------------------");
                        joueurs = joueurs.Where(val => val != joueur).ToArray(); // retire le joueur de la liste des joueurs

                    }

                    if (joueurs.Length == 1) // si il ne reste qu'un joueur, il a gagné
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Le joueur " + joueurs[0].Nom + " " + joueurs[0].Prenom + " a gagné !");
                        Console.WriteLine("----------------------------------------------");
                        break;
                    }

                }
            }
            
        }
        

    }
}
    
