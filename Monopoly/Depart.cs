using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Depart : Case
    {
        private float rapportArret = 400;
        private float rapportPassage = 200;

        public float RapportArret { get => rapportArret; set => rapportArret = value; }
        public float RapportPassage { get => rapportPassage; set => rapportPassage = value; }

        public Depart(int emplacement, float rapportA, float rapportP) : base(emplacement)
        {
            this.rapportArret = rapportA;
            this.rapportPassage = rapportP;
        }

        
    }
}
