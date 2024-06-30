using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Poids_Lourds : Vehicule
    {
        private int volume;
        private string matiere;
        public Poids_Lourds(string immat, string type) : base(immat, type)
        {
            this.volume = 0;
            this.matiere = string.Empty;
        }

        public int Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public string Matiere
        {
            get { return matiere; }
            set { matiere = value; }
        }
    }
}

