using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monopoly
{
    public class Case
    {
        private int emplacement;

        public int Emplacement
        {
            get { return emplacement; }
            set { emplacement = value; }
        }

        public Case(int emplacement)
        {
            this.emplacement = emplacement;
        }

       
    }
}
