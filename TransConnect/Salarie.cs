using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FOURNIER_LOPES
{

    public class Salarie : Personne
    {
        private string nss;
        private double salaire;
        private DateTime date_entree;
        private string poste;


        public Salarie(string nom, string prenom, string email, string telephone, string adresse, DateTime naissance, string nss, double salaire, DateTime date_entree, string poste) : base(nom, prenom, email, telephone, adresse, naissance)
        {
            this.nss = nss;
            this.salaire = salaire;
            this.date_entree = date_entree;
            this.poste = poste;
        }
        public string NSS
        {
            get { return nss; }
            set { this.nss = value; }
        }
        public double Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }

        public DateTime DateEntree
        {
            get { return date_entree; }
            set { this.date_entree = value; }
        }

        public string Poste
        {
            get { return poste; }
            set { this.poste = value; }
        }
        public override string ToString()
        {
            return base.ToString() + "Date d'entrée : " + date_entree.ToShortDateString() + " Numéro de sécurité sociale : " + nss + " salaire : " + salaire + " poste : " + poste + "\n";
        }

    }
}
