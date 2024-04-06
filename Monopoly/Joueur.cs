using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monopoly
{
    public class Joueur
    {
        private string nom;


        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        private string prenom;

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }


        private float portefeuille = 1500;

        public float Portefeuille
        {
            get { return portefeuille; }
            set { portefeuille = value; }
        }
        private Case macase;




        public Case Macase { get => macase; set => macase = value; }


        public Joueur(string nom, string prenom, Case maCase)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.macase = maCase;

        }

        public override string ToString()
        {
            return "Joueur : " + nom + "\nPosition : " + macase + "\nargent : €" + portefeuille;

        }


        public int LancerDe()
        {
            Random rnd = new Random();
            int de1 = rnd.Next(1, 7);

            return de1;
        }

        public void Avancer(int nbCases, PlateauDeJeu Plateau)
        {
            int positionActuelle = Plateau.MesCases.IndexOf(macase);
            int nouvellePosition = (positionActuelle + nbCases) % Plateau.MesCases.Count;
            this.macase = Plateau.MesCases[nouvellePosition];
        }

        public void RetirerArgent(float montant)
        {
            if (montant > 0 && portefeuille >= montant)
            {
                portefeuille -= montant;
                Console.WriteLine("Le joueur " + nom + " a retiré " + montant + " de son portefeuille.");
            }
            else
            {
                Console.WriteLine($"Le joueur " + nom + " n'a pas suffisamment d'argent pour retirer " + montant);
            }
        }

        public void PayerPrixNuitee(float montant)
        {
            // Vérifier si le joueur a assez d'argent pour payer la nuitée
            if (Portefeuille >= montant)
            {
                Portefeuille -= montant;
                // Transférer l'argent au propriétaire de la ville
                Ville villeActuelle = (Ville)Macase;
                villeActuelle.Proprietaire.Portefeuille += montant;
                Console.WriteLine(Nom + " a payé €" + montant + " de nuitée à " + villeActuelle.Proprietaire.Nom);
            }
            else
            {
                Console.WriteLine(Nom + " n'a pas assez d'argent pour payer la nuitée !");
            }
        }


        //public void AllerEnPrison()
        //{
        //    Console.WriteLine(nom + " va en prison !");
        //    macase = new Prison(13);
        //}





    }
}
