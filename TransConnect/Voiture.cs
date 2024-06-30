using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Voiture : Vehicule
    {
        private int nb_passager;
        public Voiture(string immat,string type, int nb_passager): base (immat, type)
        {
            this.nb_passager = nb_passager;
        }

        public int Nb_passager
        {
            get { return nb_passager; }
            set { nb_passager = value; }
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
