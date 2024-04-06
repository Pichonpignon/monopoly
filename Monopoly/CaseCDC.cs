using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class CaseCDC : Case
    {
        
            private Random rnd = new Random();
            private static readonly string[] messages = new string[] { "Vous avez gagné 200€", "Allez en prison !" };

            public CaseCDC(int emplacement) : base(emplacement) { }

            public void Action(Joueur joueur)
            {
                Console.WriteLine(joueur.Nom + " est sur la case caisse de communauté");

                int choix = rnd.Next(2);
                Console.WriteLine("La carte piochée indique : " + messages[choix]);

                switch (choix)
                {
                    case 0:
                        joueur.Portefeuille += 200;
                        Console.WriteLine(joueur.Nom + " a gagné 200€");
                        break;
                    case 1:
                        Console.WriteLine(joueur.Nom + " est envoyé en prison !");
                        //joueur.AllerEnPrison();
                        break;
                    default:
                        break;
                }
            }
        

    }
}
