using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Camionette : Vehicule
    {
        private string usage;
        public Camionette(string immat, string type) : base(immat, type)
        {
            this.usage = string.Empty;
        }

        public string Usage
        {
            get { return usage; }
            set { usage = value; }
        }
    }
}



