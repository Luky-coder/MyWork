using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Client : Personne, IComparable<Client>, IAchatCumules

    {
        private string n_client;
        private List<int> commande;

        public Client(string nom, string prenom, string email, string telephone, string adresse, DateTime naissance, string n_client)
            : base(nom, prenom, email, telephone, adresse, naissance)
        {
            this.n_client = n_client;
            this.commande = new List<int>();
        }

        public string N_Client
        {
            get { return n_client; }
            set { n_client = value; }
        }

        public List<int> Commande
        {
            get { return commande; }
            set { commande = value; }
        }

        public void AjouterCommandeClientData(Commande nouvelleCommande, Client client)
        {
            client.commande.Add(nouvelleCommande.NCom);
            string cheminclient = "clients.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminclient));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 7)
                {
                    if (valeur[6] == nouvelleCommande.Client.N_Client)
                    {
                        string datesExistantes = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;

                        List<int> dates = new List<int>();

                        if (!string.IsNullOrEmpty(datesExistantes))
                        {
                            dates.AddRange(datesExistantes.Split(';').Select(int.Parse));
                        }

                        dates.Add(nouvelleCommande.NCom);

                        string nouvellesDates = "{" + string.Join(";", dates) + "}";
                        line = Regex.Replace(line, @"\{(.*?)\}", nouvellesDates);

                        lines[i] = line;

                    }

                }
                File.WriteAllLines(cheminclient, lines);
            }
        }

        public void CreerClientData(string nom, string prenom, string email, string telephone, string adresse, DateTime naissance, string n_client, string id, string mdp)
        {
            string cheminclient = "clients.txt";

            string ligne = $"{nom},{prenom},{email},{telephone},{adresse},{naissance.ToString("yyyy-MM-dd")},{n_client}" + ",{}" + $",{id},{mdp}";

            string[] lignesEcrites = File.ReadAllLines(cheminclient);

            if (lignesEcrites.Length == 0)
            {
                File.WriteAllText(cheminclient, ligne);
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

                File.WriteAllLines(cheminclient, lignesEcrites);

            }
        }

        public void ChargementCommande(Client client)
        {
            string cheminclient = "clients.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminclient));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 7)
                {
                    string commandesexistante = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;

                    if (!string.IsNullOrEmpty(commandesexistante))
                    {
                        client.Commande.AddRange(commandesexistante.Split(';').Select(int.Parse));
                    }
                }

            }
        }

        public void AjouterCommande(Commande commandeAAjouter)
        {
            commande.Add(commandeAAjouter.NCom);
        }

        public void SupprimerCommande(Commande commandeASupprimer, Client client)
        {
            commande.Remove(commandeASupprimer.NCom);

            string cheminclient = "clients.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminclient));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');
                if (valeur.Length >= 8)
                {
                    if (valeur[6] == client.N_Client)
                    {
                        string commandeexistante = Regex.Match(line, @"\{(.*?)\}").Groups[1].Value;
                        List<int> commandes = new List<int>();

                        if (!string.IsNullOrEmpty(commandeexistante))
                        {
                            commandes.AddRange(commandeexistante.Split(';').Select(int.Parse));

                            for (int j = 0; j < commandes.Count; j++)
                            {
                                if (commandes[j] == commandeASupprimer.NCom)
                                {
                                    commandes.RemoveAt(j);
                                    j--;
                                }
                            }
                        }

                        string nouvelcommande = "{" + string.Join(";", commandes) + "}";
                        line = Regex.Replace(line, @"\{(.*?)\}", nouvelcommande);

                        lines[i] = line;

                    }

                }
                File.WriteAllLines(cheminclient, lines);
            }
        }

        #region Edition Client
        /// Méthode pour supprimer un client. 
        public static void SupprimerClient(List<Client> clients, string n_client)
        {
            Client client_suppr = clients.Find(client => client.N_Client == n_client);
            if (client_suppr != null)
            {
                clients.Remove(client_suppr);
            }
            string cheminclient = "clients.txt";

            List<string> lines = new List<string>(File.ReadAllLines(cheminclient));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] valeur = line.Split(',');

                if (valeur.Length >= 6)
                {
                    if (valeur[6] == n_client)
                    {
                        lines.RemoveAt(i);
                       
                    }
                }
            }
            File.WriteAllLines(cheminclient, lines);
        }

        /// Méthode pour modifier diverses caractéristiques d'un client. 
        public static void ModifierClient(List<Client> clients, string n_client)
        {
            Client client_modif = clients.Find(client => client.N_Client == n_client);
            if (client_modif != null)
            {
                Console.WriteLine("Entrez le nom du client : ");
                string nom = Console.ReadLine();

                Console.WriteLine("Entrez le prénom du client : ");
                string prenom = Console.ReadLine();

                Console.WriteLine("Entrez l'email du client : ");
                string email = Console.ReadLine();

                Console.WriteLine("Entrez le numéro de téléphone du client : ");
                string telephone = Console.ReadLine();

                Console.WriteLine("Entrez l'adresse du client : ");
                string adresse = Console.ReadLine();

                client_modif.Nom = nom;
                client_modif.Prenom = prenom;
                client_modif.Email = email;
                client_modif.Telephone = telephone;
                client_modif.Adresse = adresse;

                Console.WriteLine("Client modifié.");
            }
        }

        /// Implémentation de l'interface IComparable
        public int CompareTo(Client other)
        {
            return this.Nom.CompareTo(other.Nom);
        }

        /// Cette méthode implémenté avec l'interface IAchatCumules permet de calculer la somme des achats cumulés par clients
        public double MontantAchatsCumules()
        {
            double total = 0;
            string cheminCommande = "commandes.txt";
            string cheminLivraison = "livraison.txt";
            if (cheminCommande == null || cheminLivraison == null)
            {
                Console.WriteLine("Fichier(s) non trouvé(s)");
                return 0;
            }

            /// Lecture des livraisons et stockage dans un dictionnaire
            Dictionary<int, double> livraisons = new Dictionary<int, double>();
            foreach (string line in File.ReadAllLines(cheminLivraison))
            {
                var element = line.Split(';');
                int nliv = int.Parse(element[6]);  /// Identifiant de livraison
                double prix = double.Parse(element[0]);  /// Prix de la livraison
                livraisons[nliv] = prix;
            }

            /// Lecture des commandes et calcul du total grace aux identifiants de livraison
            foreach (string line in File.ReadAllLines(cheminCommande))
            {
                var element = line.Split(',');
                if (element[0] == n_client)  /// Filtre les commandes de ce client
                {
                    int nliv = int.Parse(element[5]);  /// Identifiant de livraison associé à la commande
                    if (livraisons.TryGetValue(nliv, out double prixLivraison))
                    {
                        total += prixLivraison;  /// Ajoute le prix de la livraison au total
                    }
                }
            }
            return total;
        }

        /// Cette méthode permet d'afficher la liste des clients suivant plusieurs critères et de les trier.
        public static void Afficher(List<Client> clients)
        {
            List<Client> clientsBase = new List<Client>(clients);
            while (true)
            {
                Console.WriteLine("Sélectionnez le critère de tri :");
                Console.WriteLine("1. Par ordre alphabétique (nom)");
                Console.WriteLine("2. Par ville");
                Console.WriteLine("3. Par montant des achats cumulés");
                Console.WriteLine("4. Rechercher par nom ou ville");
                Console.WriteLine("5. Quitter");
                Console.Write("Entrez votre choix : ");
                string choix = Console.ReadLine();

                if (choix == "1")
                {
                    clients = Sort(clientsBase, "nom");
                }
                else if (choix == "2")
                {
                    clients = Sort(clientsBase, "adresse");
                }
                else if (choix == "3")
                {
                    clients = Sort(clientsBase, "montant");
                }
                else if (choix == "4")
                {
                    Console.WriteLine("Sélectionnez le critère de recherche :");
                    Console.WriteLine("1. Par nom");
                    Console.WriteLine("2. Par ville");
                    Console.Write("Entrez votre choix : ");
                    string critere = Console.ReadLine();

                    if (critere == "1")
                    {
                        Console.Write("Quel est le nom recherché ? : ");
                        string valeur = Console.ReadLine();
                        clients = FindAll(clientsBase, c => c.Nom.ToLower().Contains(valeur.ToLower()));
                    }
                    else if (critere == "2")
                    {
                        Console.Write("Quelle ville ? ");
                        string ville = Console.ReadLine();
                        clients = FindAll(clientsBase, c => c.Adresse.ToLower().Contains(ville.ToLower()));
                    }
                    else
                    {
                        Console.WriteLine("Critère de recherche invalide.");
                        continue;
                    }
                }
                else if (choix == "5")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Choix invalide, retour au menu principal.");
                    continue;
                }

                foreach (var client in clients)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Nom: {client.Nom}");
                    Console.WriteLine($"Prénom: {client.Prenom}");
                    Console.WriteLine($"Email: {client.Email}");
                    Console.WriteLine($"Téléphone: {client.Telephone}");
                    Console.WriteLine($"Adresse: {client.Adresse}");
                    Console.WriteLine($"Date de naissance: {client.Naissance.ToShortDateString()}");
                    Console.WriteLine($"Numéro de client: {client.N_Client}");
                    Console.WriteLine($"Montant des Achats Cumulé: {client.MontantAchatsCumules()}\n");
                    Console.ResetColor();
                }
            }
        }
        

        /// trouver les clients d'une liste selon la condition 
        public static List<Client> FindAll(List<Client> clients, Predicate<Client> condition)
        {
            return clients.FindAll(condition);
        }

        public static List<Client> Sort(List<Client> clients, string critere)
        {
            if (critere == "nom")
            {
                return clients.OrderBy(c => c.Nom).ToList();
            }
            else if (critere == "adresse")
            {
                return clients.OrderBy(c => c.Adresse).ToList();
            }
            else if (critere == "montant")
            {
                return clients.OrderBy(c => c.MontantAchatsCumules()).ToList();
            }
            else
            {
                Console.WriteLine("Choix invalide");
                return clients;
            }
        }
    }
    #endregion
}
