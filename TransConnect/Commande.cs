using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Commande
    {
        private Client client;
        private string pointArriv;
        private string marchand;
        private int nCom;
        private DateTime dateCom;
        private Livraison livraison;

        public Commande(Client client, string pointArriv, string marchand)
        {
            this.dateCom = DateTime.Now;
            this.client = client;
            this.pointArriv = pointArriv;
            this.marchand = marchand;
        }

        public DateTime DateCom
        {
            get { return dateCom; }
            set { dateCom = value; }
        }

        public string PointArriv
        {
            get { return pointArriv; }
            set { pointArriv = value; }
        }

        public string Marchand
        {
            get { return marchand; }
            set { marchand = value; }
        }

        public int NCom
        {
            get { return nCom; }
            set { nCom = value; }
        }

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public Livraison Livraison
        {
            get { return livraison; }
            set { livraison = value; }
        }

        public int NumeroCommande()
        {
            string chemincommande = "commandes.txt";
            int compt = 0;


            if (File.Exists(chemincommande))
            {
                string[] lines = File.ReadAllLines(chemincommande);
                if (lines.Length > 0)
                { 
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] valeur = line.Split(',');
                            int compar = int.Parse(valeur[3]);

                            if (compar >= compt)
                            {
                                compt = compar + 1;
                            }
                        }
                    }
                }
            }
        this.NCom = compt;
         return compt;
        }

        public Livraison CreerLivraison(double prix, string pointa, string pointb, DateTime dateliv, Vehicule vehicule, Chauffeur chauf, int nliv)
        {
            Livraison livraison = new Livraison(prix, pointa, pointb, dateliv, vehicule, chauf, nliv);
            this.livraison = livraison;
            return livraison;
        }

        public void CreerCommandeData(string n_client, string pointArriv, string marchand, int nCom, DateTime dateCom, int nLiv)
        {
            string chemincommande = "commandes.txt";

            string ligne = $"{n_client},{pointArriv},{marchand},{nCom},{dateCom.ToString("yyyy-MM-dd")},{nLiv}";

            string[] lignesEcrites = File.ReadAllLines(chemincommande);

            if (lignesEcrites.Length == 0)
            {
                File.WriteAllText(chemincommande, ligne);
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

                File.WriteAllLines(chemincommande, lignesEcrites);

            }
        }
        public void SupprimerCommandeData(string nCom)
        {
            string chemincommande = "commandes.txt";

            List<string> lines = new List<string>(File.ReadAllLines(chemincommande));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');

                if (valeur.Length >= 6)
                {
                    if (valeur[3] == nCom)
                    {
                        lines.RemoveAt(i);
                       
                    }
                }
            }
            File.WriteAllLines(chemincommande, lines);
        }


        public void ResetCommandeData()
        {
            string chemincommande = "commandes.txt";

            File.WriteAllText(chemincommande, string.Empty);
        }

        public override string ToString()
        {
            return "\nCommande n°"+ nCom +" :" + "\n\nDate de la commande : " + dateCom.ToShortDateString() + "\nClient  : " + $"{client.Nom} {client.Prenom}" + "\nAdresse de livraison : " + pointArriv + "\nVehicule : " + marchand +"\nImmatriculation : " + livraison.Vehicule.Immat + "\nN° Commande : " + nCom + $"\nPrix : {livraison.Prix} euros\nDate de livraison prévu : {livraison.Dateliv.ToShortDateString()} \nChauffeur : {livraison.Chauf.Nom} {livraison.Chauf.Prenom}\n" + "Merci d'avoir passer commande chez Transconnect\n";
        }
    }
}