using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class PrendreCommande
    {
        private List<Vehicule> vehicule;

        private List<Chauffeur> chauffeur;

        private Graphe graphe;

        public PrendreCommande(List<Vehicule> vehicule, List<Chauffeur> chauffeur)
        {
            this.chauffeur = chauffeur;
            this.vehicule = vehicule;
            graphe = CreerGrapheDepuisFichierDistances();
        }

        public Commande Commander(Client client, string pointArriv, string marchand)
        {

            // Commande
            Commande commande = new Commande(client, pointArriv, marchand);
            int ncom = commande.NumeroCommande();
            client.AjouterCommandeClientData(commande, client);
            commande.CreerCommandeData(client.N_Client, pointArriv, marchand, commande.NCom, commande.DateCom, commande.NCom);

            // Disponibilité
            (Chauffeur chauffeur, Vehicule vehicule, DateTime date) = VerifDispoChauffeur(commande, commande.DateCom);

            // Ajouter Commande Chauffeur
            chauffeur.AjouterCommande(chauffeur,commande.NCom);

            // PRix
            double prix = CalculPrix(pointArriv, marchand,client, chauffeur.NSS);/// Faire la reduction avec client plus tard


            // Livraison
            commande.CreerLivraison(prix, "Paris", pointArriv, date, vehicule, chauffeur, commande.NCom);
            commande.Livraison.CreerLivraisonData(prix, "Paris", pointArriv, date, vehicule.Immat, chauffeur.NSS, commande.NCom);

            

            Console.ForegroundColor = ConsoleColor.Green;
            return commande;
        }

        public Tuple<Chauffeur, Vehicule, DateTime> VerifDispoChauffeur(Commande commande, DateTime date)
        {
            date = date.AddDays(1);
            foreach (Chauffeur chauffeur in chauffeur)
            {
                if (chauffeur.EstDisponible(date) == true)
                {
                    foreach (Vehicule vehicule in vehicule)
                    {
                        if (commande.Marchand == vehicule.Type)
                        {
                            if (vehicule.EstDisponible(date) == true)
                            {
                                chauffeur.AjouterDateDispo(chauffeur,date);
                                vehicule.AjouterDateDispo(vehicule,date);
                                return Tuple.Create<Chauffeur, Vehicule, DateTime>(chauffeur, vehicule, date);
                            }
                        }
                    }
                }
            }
            return VerifDispoChauffeur(commande, date);
        }

        private Graphe CreerGrapheDepuisFichierDistances()
        {
            string cheminFichier = "distances.txt";
            Graphe graphe = new Graphe();
            foreach (string ligne in File.ReadLines(cheminFichier))
            {
                string[] parties = ligne.Split(';');
                string source = parties[0];
                string destination = parties[1];
                int distance = int.Parse(parties[2]);
                graphe.AjouterArc(source, destination, distance);
            }
            return graphe;
        }

        public static double CalculPrix(string pointArriv, string marchand, Client client, string nssChauffeur)
        {
            string cheminDistances = "distances.txt";
            string cheminClients = "clients.txt";
            Dictionary<(string, string), double> tempsTrajets = new Dictionary<(string, string), double>();
            Dictionary<(string, string), int> distances = new Dictionary<(string, string), int>();
            Graphe graphe = new Graphe();

            if (File.Exists(cheminDistances))
            {
                string[] lignes = File.ReadAllLines(cheminDistances);
                foreach (string ligne in lignes)
                {
                    if (!string.IsNullOrWhiteSpace(ligne))
                    {
                        string[] valeurs = ligne.Split(';');
                        string source = valeurs[0];
                        string destination = valeurs[1];
                        int distance = int.Parse(valeurs[2]);
                        double temps = ConvertirTemps(valeurs[3]);
                        graphe.AjouterArc(source, destination, distance);
                        distances[(source, destination)] = distance;
                        distances[(destination, source)] = distance;
                        tempsTrajets[(source, destination)] = temps;
                        tempsTrajets[(destination, source)] = temps;
                    }
                }
            }

            Dijkstra dijkstra = new Dijkstra(graphe);
            List<Arc> cheminPlusCourt = dijkstra.TrouverCheminPlusCourt("Paris", pointArriv);

            if (cheminPlusCourt != null)
            {
                foreach (Arc arc in cheminPlusCourt)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"-> {arc.Source} -> {arc.Destination} ");
                    Console.ResetColor();
                }
            }

            double distanceTotale = cheminPlusCourt.Sum(arc => arc.Distance);
            double tempsTotal = cheminPlusCourt.Sum(arc => tempsTrajets[(arc.Source, arc.Destination)]);

            double tauxHoraire = 0;
            string cheminChauffeurs = "chauffeurs.txt";

            if (File.Exists(cheminChauffeurs))
            {
                string[] lignesChauffeurs = File.ReadAllLines(cheminChauffeurs);
                foreach (string ligne in lignesChauffeurs)
                {
                    if (!string.IsNullOrWhiteSpace(ligne))
                    {
                        string[] valeurs = ligne.Split(',');
                        string nss = valeurs[6];
                        if (nss == nssChauffeur)
                        {
                            double salaire = double.Parse(valeurs[7]);
                            tauxHoraire = (salaire / 4) / 40;
                            break;
                        }
                    }
                }
            }
            double prix = 0;

            if (marchand == "Voiture")
            {
                prix += distanceTotale;
            }
            else if (marchand == "Camionette")
            {
                prix += distanceTotale * 1.2;
            }
            else if (marchand == "Camion_benne")
            {
                prix += distanceTotale * 1.5;
            }
            else if (marchand == "Camion_citerne")
            {
                prix += distanceTotale * 1.8;
            }
            else if (marchand == "Camion_frigorifique")
            {
                prix += distanceTotale * 2.0;
            }

            double coutChauffeur = tempsTotal * tauxHoraire;
            prix += coutChauffeur;

            int nbCommandesClient = 0;

            if (File.Exists(cheminClients))
            {
                string[] lignesClients = File.ReadAllLines(cheminClients);
                foreach (string ligne in lignesClients)
                {
                    if (!string.IsNullOrWhiteSpace(ligne))
                    {
                        string[] valeurs = ligne.Split(',');
                        if (valeurs[0] == client.Nom && valeurs[1] == client.Prenom)
                        {
                            string commandes = valeurs[7];
                            if (commandes != "{}")
                            {
                                nbCommandesClient = commandes.Split(';').Length;
                            }
                            else
                            {
                                nbCommandesClient = 0;
                            }
                            break;
                        }
                    }
                }
            }
            if (nbCommandesClient > 4)
            {
                prix *= 0.80;
            }
            else if (nbCommandesClient > 2)
            {
                prix *= 0.90;
            }

            return Math.Round(prix, 2);
        }

        public static double ConvertirTemps(string temps)
        {
            var parts = temps.Split('h');
            int heures = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            return heures + (minutes / 60.0);
        }
    }
}
