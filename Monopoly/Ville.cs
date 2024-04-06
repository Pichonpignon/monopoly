using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Ville : Case
    {
        private float prixAchat = 0;
        private float prixNuitee = 0;
        private string nom = "";
        private bool libre = true;
        private Joueur proprietaire;

        public float PrixAchat { get => prixAchat; set => prixAchat = value; }
        public float PrixNuitee { get => prixNuitee; set => prixNuitee = value; }
        public string Nom { get => nom; set => nom = value; }
        public bool Libre { get => libre; set => libre = value; }
        public Joueur Proprietaire { get => proprietaire; set => proprietaire = value; }

        public Ville(int emplacement, float prixAchat, float prixNuitee, string nom, bool vlibre) : base(emplacement)
        {
            this.prixAchat = prixAchat;
            this.prixNuitee = prixNuitee;
            this.nom = nom;
            this.libre = vlibre;
        }

        public override string ToString()
        {
            return "\tName: " + nom + "\n\tPrix achat : €" + prixAchat + "\n\tNuit: €" + prixNuitee + "\n\t Libre : " + this.libre.ToString();
        }

        public void Acheter(Joueur joueur, string reponse)
        {

            if (reponse.ToLower() == "o")
            {
                joueur.RetirerArgent(prixAchat);
                proprietaire = joueur;
                libre = false;
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("\nFélicitations, vous avez acheté la ville " + nom + " !\n");
                Console.WriteLine("----------------------------------------------");
            }
            else
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("\nVous avez décidé de ne pas acheter la ville " + nom + ".\n");
                Console.WriteLine("----------------------------------------------");
            }
        }
    }
}
