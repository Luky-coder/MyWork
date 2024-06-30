using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Livraison
    {
        private double prix;
        private string pointa;
        private string pointb;
        private DateTime dateliv;
        private Vehicule vehicule;
        private Chauffeur chauf;
        private int nliv;

        public Livraison(double prix, string pointa, string pointb, DateTime dateliv, Vehicule vehicule, Chauffeur chauf, int nliv)
        {
            this.prix = prix;
            this.pointa = pointa;
            this.pointb = pointb;
            this.dateliv = dateliv;
            this.vehicule = vehicule;
            this.chauf = chauf;
            this.nliv = nliv;
        }

        public double Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public Chauffeur Chauf
        {
            get { return chauf; }
            set { chauf = value; }
        }
        public string Pointa
        {
            get { return pointa; }
            set { pointa = value; }
        }
        public string Pointb
        {
            get { return pointb; }
            set { pointb = value; }
        }
        public DateTime Dateliv
        {
            get { return dateliv; }
            set { dateliv = value; }
        }

        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }

        public int Nliv
        { get { return nliv; }
          set { nliv = value; }
        }

        public void CreerLivraisonData(double prix, string pointa, string pointb, DateTime dateliv, string immat_vehicule, string nss_chauf, int nliv)
        {
            string cheminlivraison = "livraison.txt";

            string ligne = $"{prix};{pointa};{pointb};{dateliv.ToString("yyyy-MM-dd")};{immat_vehicule};{nss_chauf};{nliv}";

            string[] lignesEcrites = File.ReadAllLines(cheminlivraison);

            if (lignesEcrites.Length == 0)
            {
                File.WriteAllText(cheminlivraison, ligne);
            }
            else
            {
                bool LigneVide = false;

                for (int i = 0; i < lignesEcrites.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lignesEcrites[i]))
                    {
                        lignesEcrites[i] = ligne;
                        LigneVide = true;
                        break;
                    }
                }
                if (!LigneVide)
                {
                    Array.Resize(ref lignesEcrites, lignesEcrites.Length + 1);
                    lignesEcrites[lignesEcrites.Length - 1] = ligne;
                }
                File.WriteAllLines(cheminlivraison, lignesEcrites);
            }
        }

        public void SupprimerLivraisonData(string nCom)
        {
            string cheminlivraison = "livraison.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminlivraison));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(';');

                if (valeur.Length >= 6)
                {
                    if (valeur[6] == nCom)
                    {
                        lines.RemoveAt(i);
                        
                    }
                }
            }
            File.WriteAllLines(cheminlivraison, lines);
        }

        public override string ToString()
        {
            return "Prix : " + prix + " euros\n Point de Départ : " + pointa + "\n Point d'Arrivé : " + pointb + "\n Date de Livraison : " + dateliv.ToShortDateString() + "\n Vehicule : " + vehicule + "\n  Chauffeur : " + chauf;
        }
    }
}