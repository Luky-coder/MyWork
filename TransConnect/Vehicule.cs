using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Vehicule
    {
        private string immat;
        private string type;
        private List<DateTime> dispo;

        public Vehicule(string immat, string type)
        {
            this.immat = immat;
            this.type = type;
            this.dispo = new List<DateTime>();
        }

        public string Immat
        {
            get { return this.immat; }
            set { this.immat = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
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

        public void AjouterDateDispo(Vehicule vehicule, DateTime date)
        {
            dispo.Add(new DateTime(date.Year, date.Month, date.Day));
            string cheminvehicule = "vehicules.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminvehicule));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 1)
                {
                    if (valeur[0] == vehicule.Immat)
                    {
                        string datesExistant = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;
                        List<DateTime> dates = new List<DateTime>();

                        if (!string.IsNullOrEmpty(datesExistant))
                        {
                            dates.AddRange(datesExistant.Split(';').Select(DateTime.Parse));
                        }

                        dates.Add(new DateTime(date.Year, date.Month, date.Day));

                        string nouvellesDates = "{" + string.Join(";", dates) + "}";
                        line = Regex.Replace(line, @"\{(.*?)\}", nouvellesDates);

                        lines[i] = line;

                    }

                }
                File.WriteAllLines(cheminvehicule, lines);
            }
        }

        public void ChargerDispo(Vehicule vehicule)
        {
            string cheminvehicules = "vehicules.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminvehicules));
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 1)
                {
                    if (valeur[0] == vehicule.Immat)
                    {
                        if (Regex.IsMatch(line, @"\{\s*\}"))
                        {
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

        public void SupprimerLivraison(Vehicule vehicule, string Immat, DateTime date)
        {
            string cheminvehicule = "vehicules.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminvehicule));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 1)
                {
                    if (valeur[0] == vehicule.Immat)
                    {
                        string dispoexistant = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;
                        List<DateTime> dispos = new List<DateTime>();

                        if (!string.IsNullOrEmpty(dispoexistant))
                        {
                            dispos.AddRange(dispoexistant.Split(';').Select(DateTime.Parse));

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

                        lines[i] = line;

                    }

                }
                File.WriteAllLines(cheminvehicule, lines);
            }
        }
        

        public override string ToString()
        {
            return " Immatriculation : " + immat + "Type : " + type + "Dispo : " + dispo;
        }
    }
}
