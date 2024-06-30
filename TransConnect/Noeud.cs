using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FOURNIER_LOPES
{
    public class Noeud
    {
        private Salarie salarie;
        private List<Noeud> enfants;
        private Noeud parent;

        public Noeud (Salarie salarie)
        {
            this.salarie = salarie;
            this.enfants = new List<Noeud>();
        }
        public Salarie Salarie 
        { 
            get { return salarie; }
            set { salarie = value; }
        }
 
        public List<Noeud> Enfant
        {
            get { return enfants; }
            set { enfants = value; } 
        }

        public Noeud Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public void AjouterEnfant(Noeud enfant)
        {
            enfants.Add(enfant);
        }

        public void SupprimerEnfant(Noeud enfant)
        {
            enfants.Remove(enfant);
        }

        public override string ToString()
        {
            return this.Salarie.Nom;
        }
    }
}
