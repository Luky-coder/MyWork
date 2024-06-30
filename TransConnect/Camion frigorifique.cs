using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    internal class Camion_frigorifique : Poids_Lourds
    {
        private int nb_electro;
        public Camion_frigorifique(string immat, string type) : base(immat, type)
        {
            this.nb_electro = 1;
        }

        public int Nb_electro
        {
            get { return nb_electro; }
            set { nb_electro = value; }
        }
    }
}