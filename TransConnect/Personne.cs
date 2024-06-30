using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public abstract class Personne
    {
        private string nom;
        private string prenom;
        private string email;
        private string telephone;
        private string adresse;
        private DateTime naissance;


        public Personne(string nom, string prenom, string email, string telephone, string adresse, DateTime naissance)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.telephone = telephone;
            this.adresse = adresse;
            this.naissance = naissance;
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }
        public DateTime Naissance
        {
            get { return naissance; }
            set { naissance = value; }
        }

        public override string ToString()
        {
            return "Nom : " + nom + " Prenom : " + prenom + " Email : " + email + " Telephone : " + telephone + " Adresse : " + adresse + " Date de naissance : " + naissance.ToShortDateString() + "\n";
        }
    }
}
