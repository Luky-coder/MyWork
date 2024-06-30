using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Chauffeur : Salarie
    {
        private List<Commande> commande;
        private List<DateTime> dispo;


        public Chauffeur(string nom, string prenom, string email, string telephone, string adresse, DateTime naissance, string nss, double salaire, DateTime date_entree, string poste)
            : base(nom, prenom, email, telephone, adresse, naissance, nss, salaire, date_entree, poste)
        {
            this.commande = new List<Commande>();
            this.dispo = new List<DateTime>();
        }

        public List<Commande> Commande
        {
            get { return commande; }
            set { commande = value; }
        }

        public List<DateTime> Dispo
        {
            get { return dispo; }
            set { dispo = value; }
        }

        public bool EstDisponible(DateTime date)
        {
            foreach (DateTime dateDispo in dispo)
            {
                if (date.Year == dateDispo.Year && date.Month == dateDispo.Month && date.Day == dateDispo.Day)
                {
                    return false;
                }
            }
            return true;
        }

        public void AjouterDateDispo(Chauffeur chauffeur, DateTime date)
        {
            dispo.Add(new DateTime(date.Year, date.Month, date.Day));

            string cheminchauffeur = "chauffeurs.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminchauffeur));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 7)
                {
                    if (valeur[6] == chauffeur.NSS)
                    {
                        string datesExistantes = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;
                        List<DateTime> dates = new List<DateTime>();

                        if (!string.IsNullOrEmpty(datesExistantes))
                        {
                            dates.AddRange(datesExistantes.Split(';').Select(DateTime.Parse));
                        }

                        dates.Add(new DateTime(date.Year, date.Month, date.Day));

                        string nouvellesDates = "{" + string.Join(";", dates) + "}";
                        line = Regex.Replace(line, @"\{(.*?)\}", nouvellesDates);

                        lines[i] = line;

                    }

                }
                File.WriteAllLines(cheminchauffeur, lines);
            }
        }

        public void AjouterCommande(Chauffeur chauffeur,int ncom)
        {

            string cheminchauffeur = "chauffeurs.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminchauffeur));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 7)
                {
                    if (valeur[6] == chauffeur.NSS)
                    {
                        string datesExistantes = Regex.Match(line, @"\[(.*?)\]").Groups[1].Value;
                        List<int> dates = new List<int>();

                        if (!string.IsNullOrEmpty(datesExistantes))
                        {
                            dates.AddRange(datesExistantes.Split(';').Select(int.Parse));
                        }

                        dates.Add(ncom);

                        string nouvellesDates = "[" + string.Join(";", dates) + "]";
                        line = Regex.Replace(line, @"\[(.*?)\]", nouvellesDates);

                        lines[i] = line;

                    }

                }
                File.WriteAllLines(cheminchauffeur, lines);
            }
        }
        public void ChargerDispo(Chauffeur chauffeur)
        {
            string cheminchauffeur = "chauffeurs.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminchauffeur));
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];
                    string[] valeur = line.Split(',');
                    if (valeur.Length >= 10)
                    {
                        if (valeur[6] == chauffeur.NSS)
                        {
                            if (Regex.IsMatch(line, @"\{\s*\}"))
                            {//Si c'est vide 
                            return;
                            }
                            else
                            {
                                string dates = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value; 
                                string[] var = dates.Split(";");
                                foreach (string date in var)
                                {
                                    dispo.Add(DateTime.Parse(date));
                                }
                            }
                        }
                    }
                }
        }

        public void SupprimerCommande(Chauffeur chauffeur, DateTime date, int Nliv)
        {
            string cheminchauffeur = "chauffeurs.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminchauffeur));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 7)
                {
                    if (valeur[6] == chauffeur.NSS)
                    {
                        string dispoexistante = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;
                        List<DateTime> dispos = new List<DateTime>();

                        string livraisonexistante = Regex.Match(line, @"\[(.*?)\]").Groups[1].Value;
                        List<int> livraisons = new List<int>();

                        if (!string.IsNullOrEmpty(livraisonexistante))
                        {
                            livraisons.AddRange(livraisonexistante.Split(';').Select(int.Parse));

                            for (int k = 0; k < livraisons.Count; k++)
                            {
                                if (livraisons[k] == Nliv)
                                {
                                    livraisons.RemoveAt(k);
                                    k--;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty (dispoexistante))
                        {
                            dispos.AddRange(dispoexistante.Split(";").Select(DateTime.Parse));

                            for (int j = 0; j < dispos.Count; j++)
                            {
                                if (dispos[j] == date)
                                {
                                    dispos.RemoveAt(j);
                                    j--;
                                }
                            }

                        }

                        string nouveldispo = "{" + string.Join(";", dispos) + "}";
                        line = Regex.Replace(line, @"\{(.*?)\}", nouveldispo);

                        string nouvellivraison = "[" + string.Join(";", livraisons) + "]";
                        line = Regex.Replace(line, @"\[(.*?)\]", nouvellivraison);
                        lines[i] = line;

                    }
                }
                
            }
            File.WriteAllLines(cheminchauffeur, lines);
        }

        public void Planning()
        {
            Console.WriteLine($"Planning de {Prenom} {Nom} : \n");
            

            foreach (DateTime date in dispo)
            {
                Console.WriteLine(date.ToShortDateString());
            }

            Console.WriteLine(new string('-', 40));
        }

        public override string ToString()
        {
            string resultat = "[";
            foreach (DateTime commandes in dispo)
            {
                resultat += commandes.ToString("yyyy-MM-dd") + ", ";
            }
            resultat += "]";
            return base.ToString() + resultat;
        }
    }
}
