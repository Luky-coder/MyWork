using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    internal class Camion_benne : Poids_Lourds
    {
        private int nb_cuve;
        private bool grue_aux;
        public Camion_benne(string immat, string type) : base(immat, type)
        {
            this.nb_cuve = 1;
            this.grue_aux = false;
        }

        public int Nb_cuve
        {
            get { return nb_cuve; }
            set { nb_cuve = value; }
        }

        public bool Grue_aux
        {
            get { return grue_aux; }
            set { grue_aux = value; }
        }
    }
}

