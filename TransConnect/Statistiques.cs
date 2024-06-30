using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Statistiques
    {
        public static void AfficherLivraisonsParChauffeur(List<Livraison> listeLivraisons, List<Chauffeur> listeChauffeurs, string chauffeurSelectionne = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (chauffeurSelectionne != null)
            {
                string nomChauffeur = "";
                foreach (var chauffeur in listeChauffeurs)
                {
                    if (chauffeur.NSS == chauffeurSelectionne)
                    {
                        nomChauffeur = chauffeur.Prenom + " " + chauffeur.Nom;
                        break;
                    }
                }

                Console.WriteLine($"Livraisons pour le chauffeur {nomChauffeur} sélectionné :");
                int nombreLivraisons = 0;
                foreach (var livraison in listeLivraisons)
                {
                    if (livraison.Chauf.NSS == chauffeurSelectionne)
                    {
                        Console.WriteLine($"Prix: {livraison.Prix}, Point A: {livraison.Pointa}, Point B: {livraison.Pointb}, Date de livraison: {livraison.Dateliv.ToShortDateString()}");
                        nombreLivraisons++;
                    }
                }
                Console.WriteLine($"Nombre de livraisons pour le chauffeur {nomChauffeur}: {nombreLivraisons}");
            }
            else
            {
                var livraisonsParChauffeur = new Dictionary<string, List<Livraison>>();
                foreach (var livraison in listeLivraisons)
                {
                    string nomChauffeur = "Inconnu";
                    foreach (var chauffeur in listeChauffeurs)
                    {
                        if (chauffeur.NSS == livraison.Chauf.NSS)
                        {
                            nomChauffeur = chauffeur.Prenom + " " + chauffeur.Nom;
                            break;
                        }
                    }
                    if (!livraisonsParChauffeur.ContainsKey(nomChauffeur))
                    {
                        livraisonsParChauffeur[nomChauffeur] = new List<Livraison>();
                    }
                    livraisonsParChauffeur[nomChauffeur].Add(livraison);
                }
                foreach (var chauffeurLivraisons in livraisonsParChauffeur)
                {
                    Console.WriteLine($"Livraisons pour le chauffeur {chauffeurLivraisons.Key} :");
                    foreach (var livraison in chauffeurLivraisons.Value)
                    {
                        Console.WriteLine($"Prix: {livraison.Prix}, Point A: {livraison.Pointa}, Point B: {livraison.Pointb}, Date de livraison: {livraison.Dateliv.ToShortDateString()}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Nombre total de livraisons: {listeLivraisons.Count}\n");
            }
            Console.ResetColor();
        }

        public delegate bool LivraisonFiltre(Livraison livraison);
        public static void AfficherCommandesParPeriode(List<Livraison> listeLivraisons, List<Chauffeur> listeChauffeurs, LivraisonFiltre filtre)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var chauffeursDict = listeChauffeurs.ToDictionary(chauffeur => chauffeur.NSS, chauffeur => $"{chauffeur.Prenom} {chauffeur.Nom}");

            foreach (var livraison in listeLivraisons.Where(l => filtre(l)))
            {
                string nomChauffeur = chauffeursDict.TryGetValue(livraison.Chauf.NSS, out var nom) ? nom : "Inconnu";
                Console.WriteLine($"Prix: {livraison.Prix}, Point A: {livraison.Pointa}, Point B: {livraison.Pointb}, Date de livraison: {livraison.Dateliv.ToShortDateString()}, Chauffeur: {nomChauffeur} \n");
            }
            Console.ResetColor();
        }

        public static void AfficherMoyennePrixLivraisons(List<Livraison> listeLivraisons)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (listeLivraisons == null || listeLivraisons.Count == 0)
            {
                Console.WriteLine("Aucune livraison fournie.");
                return;
            }
            double sommePrix = 0;
            int nombreLivraisons = 0;
            foreach (var livraison in listeLivraisons)
            {
                sommePrix += livraison.Prix;
                nombreLivraisons++;
            }
            if (nombreLivraisons > 0)
            {
                double moyennePrix = sommePrix / nombreLivraisons;
                Console.WriteLine($"La moyenne des prix des livraisons est de {moyennePrix.ToString("F2")} euros.\n");
            }
            else
            {
                Console.WriteLine("Aucune livraison trouvée dans la liste.");
            }
            Console.ResetColor();
        }

        public static void AfficherCommandesClient(List<Commande> listeCommandes, List<Livraison> listeLivraisons, string N_Client)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Liste des commandes pour le client selectionné :");
            foreach (var commande in listeCommandes)
            {
                if (commande.Client.N_Client == N_Client)
                {
                    Livraison Livraisons = listeLivraisons.FirstOrDefault(l => l.Nliv == commande.NCom);

                    if (Livraisons != null)
                    {
                        Console.WriteLine($"ID Commande : {commande.NCom}, Date : {commande.DateCom.ToShortDateString()}, Montant : {Livraisons.Prix} euros.");
                    }
                }
            }
            Console.ResetColor();
        }

        public static void AfficherMoyenneComptesClients(List<Commande> listeCommandes, List<Livraison> listeLivraisons)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Dictionary<string, double> MontantClient = new Dictionary<string, double>();
            Dictionary<string, int> nbCommandesClient = new Dictionary<string, int>();
            Dictionary<string, string> nomClients = new Dictionary<string, string>();

            foreach (var commande in listeCommandes)
            {
                string idClient = commande.Client.N_Client;
                string nom = commande.Client.Nom + " " + commande.Client.Prenom;
                if (!nomClients.ContainsKey(idClient))
                {
                    nomClients[idClient] = nom;
                }
                foreach (var livraison in listeLivraisons)
                {
                    if (livraison.Nliv == commande.NCom)
                    {
                        if (!MontantClient.ContainsKey(idClient))
                        {
                            MontantClient[idClient] = 0;
                        }
                        MontantClient[idClient] += livraison.Prix;
                        if (!nbCommandesClient.ContainsKey(idClient))
                        {
                            nbCommandesClient[idClient] = 0;
                        }
                        nbCommandesClient[idClient]++;
                        break;
                    }
                }
            }
            foreach (var idClient in MontantClient.Keys)
            {
                double sommeMontant = MontantClient[idClient];
                int nombreCommandes = nbCommandesClient[idClient];
                double moyenne = sommeMontant / nombreCommandes;
                Console.WriteLine($"La moyenne pour le client {nomClients[idClient]} est de {moyenne.ToString("F2")} euros.");
            }
            Console.ResetColor();
        }
    }
}
