using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class PlateauDeJeu
    {
        private List<Case> mesCases;



        public List<Case> MesCases
        {
            get { return mesCases; }
            set { mesCases = value; }
        }
        public PlateauDeJeu()
        {
            mesCases = new List<Case>();
        }
        public void ajoutercase(Case unecase)
        {

            mesCases.Add(unecase);

        }


        public override string ToString()
        {
            string msg = "";
            foreach (Case unCase in mesCases)
            {
                msg += unCase.ToString() + ", " + "\n";
            }
            return msg;
        }

        public Case PositionCase(int position)
        {
            return mesCases[position];
        }



    }
}
