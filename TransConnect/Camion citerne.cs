using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    internal class Camion_citerne : Poids_Lourds
    {
        private string cuve;
        public Camion_citerne(string immat, string type) : base(immat, type)
        {
            this.cuve = string.Empty;
        }

        public string Cuve
        {
            get { return this.cuve; }
            set { this.cuve = value; }
        }
    }
}
