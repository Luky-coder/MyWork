using Newtonsoft.Json;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;

namespace FOURNIER_LOPES
{
   
    class Program
    { ///static List<Commande> listeCommandes = new List<Commande>();
        static void Main(string[] args)
        {
            #region Logo
            Console.Title = "TRANSCONNECT";
            Console.ForegroundColor = ConsoleColor.Blue;
            string title = @"
████████╗██████╗  █████╗ ███╗   ██╗███████╗ ██████╗ ██████╗ ███╗   ██╗███╗   ██╗███████╗ ██████╗████████╗
╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗████╗  ██║████╗  ██║██╔════╝██╔════╝╚══██╔══╝
   ██║   ██████╔╝███████║██╔██╗ ██║███████╗██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██║        ██║   
   ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██║     ██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██║        ██║   
   ██║   ██║  ██║██║  ██║██║ ╚████║███████║╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║███████╗╚██████╗   ██║   
   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝ ╚═════╝   ╚═╝   
                                                                                                         
";
            Console.WriteLine(title);
            Console.ResetColor();
            #endregion

            #region Organigramme
            /// <summary>
            /// Dans cette partie, nous avons instancier toutes les personnes présentent dans l'entreprise afin de pouvoir
            /// fournir au PDG l'organigramme global de son entreprise.
            /// Chaque création de salarié est catégorisée en fonction de son secteur 
            /// </summary> 
            static void ChargerOrganigrammeBase()
            {
                #region PDG
                Salarie PDG = new Salarie("Dupond", "Mr", "mr.dupont@transconnect.com", "123456789", "123 rue de la connexion", new DateTime(1980, 5, 10), "472346824", 150000, new DateTime(2000, 1, 1), "Directeur Général");
                #endregion

                #region Commerciale
                Salarie directriceCommerciale = new Salarie("Fiesta", "Mme", "mme.fiesta@transconnect.com", "123456789", "123 rue de code", new DateTime(1978, 3, 15), "1472834243", 72000, new DateTime(2005, 6, 1), "Directrice Commerciale");
                Salarie commercial1 = new Salarie("Forge", "Mr", "mr.forge@transconnect.com", "07456489764", "444 avenue du maçon", new DateTime(1998, 9, 20), "15425253", 48000, new DateTime(2020, 4, 20), "Commercial");
                Salarie commerciale1 = new Salarie("Fermi", "Mme", "mme.fermi@transconnect.com", "592460546", "666 avenue du kar", new DateTime(1999, 8, 14), "23416785126", 47000, new DateTime(2022, 4, 30), "Commerciale");
                #endregion

                #region Operation
                Salarie directeurOperations = new Salarie("Fetard", "Mr", "mr.fetard@transconnect.com", "054214253553", "321 rue du cactus", new DateTime(1976, 12, 18), "87249568456", 122750, new DateTime(2003, 10, 10), "Directeur des opérations");
                Salarie chefEquipe1 = new Salarie("Royal", "Mr", "mr.royal@transconnect.com", "777987654", "123 rue aude", new DateTime(1980, 5, 12), "4728967465841", 38000, new DateTime(2010, 4, 15), "Chef d'Équipe");
                Salarie chefEquipe2 = new Salarie("Prince", "Mme", "mme.prince@transconnect.com", "4513135251", "147 rue vaugirard", new DateTime(1995, 5, 1), "17823214652", 38500, new DateTime(2009, 11, 5), "Chef d'Équipe");
                Salarie chauffeur1 = new Salarie("Romu", "Mr", "mr.romu@transconnect.com", "54627652", "2bis avenue des Fleurs", new DateTime(1965, 9, 20), "782687415", 1700, new DateTime(2011, 8, 20), "Chauffeur");
                Salarie chauffeur2 = new Salarie("Romi", "Mme", "mme.romi@transconnect.com", "1526549684", "78 boulevard Soleil", new DateTime(1976, 3, 10), "27583674635", 2000, new DateTime(2001, 6, 10), "Chauffeur");
                Salarie chauffeur3 = new Salarie("Roma", "Mme", "mme.roma@transconnect.com", "524.343562753", "89 boulevard de la voiture", new DateTime(1994, 4, 20), "352782442", 1650, new DateTime(2012, 8, 10), "Chauffeur");
                Salarie chauffeur4 = new Salarie("Rome", "Mme", "mme.rome@transconnect.com", "8625768675241", "12 avenue kessel", new DateTime(2000, 4, 20), "56274632463", 1650, new DateTime(2012, 8, 10), "Chauffeur");
                Salarie chauffeur5 = new Salarie("Rimou", "Mme", "mme.rimou@transconnect.com", "65125641564", "78 boulevard de la villette", new DateTime(2001, 4, 30), "41545525", 1500, new DateTime(2022, 7, 30), "Chauffeur");
                #endregion

                #region RH
                Salarie directriceRH = new Salarie("Joyeuse", "Mme", "mme.joyeuse@transconnect.com", "111222333", "111 avenue de la paye", new DateTime(1970, 9, 5), "5412352351", 85000, new DateTime(2000, 3, 1), "Directrice des RH");
                Salarie formation = new Salarie("Couleur", "Mme", "mme.couleur@transconnect.com", "468274687", "987 avenue des champs", new DateTime(1970, 9, 5), "7852362286554", 30000, new DateTime(1998, 3, 1), "Formation");
                Salarie contrat = new Salarie("ToutleMonde", "Mme", "mme.toutlemonde@transconnect.com", "541245754", "32 avenue des signatures", new DateTime(1970, 9, 5), "23856465284361", 38000, new DateTime(1998, 3, 1), "Contrat");
                #endregion

                #region Finance
                Salarie directeurFinancier = new Salarie("GripSous", "Mr", "mr.gripsous@transconnect.com", "999888777", "555 rue ecoute s'il pleut", new DateTime(1971, 10, 11), "15264635256426", 80000, new DateTime(1997, 6, 30), "Directeur Financier");
                Salarie directionComptable = new Salarie("Picsou", "Mr", "mr.picsou@transconnect.com", "465246456170", "111 avenue du chiffre", new DateTime(1971, 11, 10), "15425253", 74000, new DateTime(2014, 6, 20), "Direction Comptable");
                Salarie controleur_de_Gestion = new Salarie("GrosSous", "Mr", "mr.grossous@transconnect.com", "04746046560", "777 avenue du krach", new DateTime(1975, 10, 14), "62589765", 45000, new DateTime(2000, 12, 5), "Controleur de Gestion");
                Salarie comptable1 = new Salarie("Fournier", "Mme", "mme.fournier@transconnect.com", "0504879053", "888 rue des capucines", new DateTime(1998, 8, 5), "324015634103", 42000, new DateTime(2010, 05, 5), "Comptable");
                Salarie comptable2 = new Salarie("Gautier", "Mme", "mme.gautier@transconnect.com", "06898754056", "989 rue du compte", new DateTime(1973, 4, 15), "401230652154", 42000, new DateTime(2010, 2, 28), "Comptable");
                #endregion

                #region Création
                /// <summary>
                /// Cette partie sert à la création des l'organigramme selon la structure de l'arbre n-aire. Voici les étapes: 
                /// 1- Création de l'organigramme aavec comme racine le PDG
                /// 2- Création des noeuds associés à tous les salariés créés ci-dessus
                /// 3- Ajout des salariés en fonction de leur hierarchie (cf. sujet du problème) 
                /// </summary>

                Organigramme organigramme = new Organigramme(PDG);
                Noeud racine = organigramme.Racine;

                Noeud DirectriceCommerciale = new Noeud(directriceCommerciale);
                Noeud DirecteurOperations = new Noeud(directeurOperations);
                Noeud DirectriceRH = new Noeud(directriceRH);
                Noeud DirecteurFinancier = new Noeud(directeurFinancier);

                Noeud Commercial1 = new Noeud(commercial1);
                Noeud Commerciale1 = new Noeud(commerciale1);

                Noeud Formation = new Noeud(formation);
                Noeud Contrat = new Noeud(contrat);

                Noeud DirectionComptable = new Noeud(directionComptable);
                Noeud ControleurDeGestion = new Noeud(controleur_de_Gestion);
                Noeud Comptable1 = new Noeud(comptable1);
                Noeud Comptable2 = new Noeud(comptable2);


                Noeud ChefEquipe1 = new Noeud(chefEquipe1);
                Noeud ChefEquipe2 = new Noeud(chefEquipe2);
                Noeud Chauffeur1 = new Noeud(chauffeur1);
                Noeud Chauffeur2 = new Noeud(chauffeur2);
                Noeud Chauffeur3 = new Noeud(chauffeur3);
                Noeud Chauffeur4 = new Noeud(chauffeur4);
                Noeud Chauffeur5 = new Noeud(chauffeur5);

                organigramme.AjouterHierarchique(racine, DirectriceCommerciale);
                organigramme.AjouterHierarchique(racine, DirecteurOperations);
                organigramme.AjouterHierarchique(racine, DirectriceRH);
                organigramme.AjouterHierarchique(racine, DirecteurFinancier);

                organigramme.AjouterHierarchique(DirectriceCommerciale, Commercial1);
                organigramme.AjouterHierarchique(DirectriceCommerciale, Commerciale1);

                organigramme.AjouterHierarchique(DirectriceRH, Formation);
                organigramme.AjouterHierarchique(DirectriceRH, Contrat);

                organigramme.AjouterHierarchique(DirecteurFinancier, ControleurDeGestion);
                organigramme.AjouterHierarchique(DirecteurFinancier, DirectionComptable);
                organigramme.AjouterHierarchique(DirectionComptable, Comptable1);
                organigramme.AjouterHierarchique(DirectionComptable, Comptable2);

                organigramme.AjouterHierarchique(DirecteurOperations, ChefEquipe1);
                organigramme.AjouterHierarchique(DirecteurOperations, ChefEquipe2);
                organigramme.AjouterHierarchique(ChefEquipe1, Chauffeur1);
                organigramme.AjouterHierarchique(ChefEquipe1, Chauffeur2);
                organigramme.AjouterHierarchique(ChefEquipe1, Chauffeur3);
                organigramme.AjouterHierarchique(ChefEquipe2, Chauffeur4);
                organigramme.AjouterHierarchique(ChefEquipe2, Chauffeur5);
                #endregion
                string chemin = "organigramme.txt";
                EnregistrerOrganigramme(organigramme, chemin);
            }
            #region Serialisation & Enregistrement
            static void EnregistrerOrganigramme(Organigramme organigramme, string chemin)
            {
                string enregistrer = JsonConvert.SerializeObject(organigramme);
                File.WriteAllText(chemin, enregistrer);
                Console.WriteLine("Enregistrement OK!");
            }
            static Organigramme ChargerOrganigramme(string chemin)
            {
                string json = File.ReadAllText(chemin);
                Organigramme organigramme = JsonConvert.DeserializeObject<Organigramme>(json);
                return organigramme;
            }
            #endregion
            #endregion

            #region Chargement 
            List<Client> clients = new List<Client>();
            static Client ChargementClient(string ident)
            {
                Client client = null;
                string cheminclient = "clients.txt";
                if (File.Exists(cheminclient))
                {
                    string[] lines = File.ReadAllLines(cheminclient);
                    if (lines.Length > 0)
                    {
                        foreach (string line in lines)
                        {
                            string[] valeur = line.Split(',');
                            if (valeur[8] == ident)
                            {
                                client = new Client(valeur[0], valeur[1], valeur[2], valeur[3], valeur[4], Convert.ToDateTime(valeur[5]), valeur[6]);
                            }
                        }
                    }
                }
                return client;
            }
            #endregion

            #region Chargement BDD
            #region Véhicules
            static List<Vehicule> ChargerVehicules(string chemin)
            {///Chargement depuis les datas
                List<Vehicule> listeVehicules = new List<Vehicule>();
                if (File.Exists(chemin))
                {
                    foreach (var line in File.ReadAllLines(chemin))
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var valeurs = line.Split(',');
                            listeVehicules.Add(new Vehicule(valeurs[0], valeurs[1]));  // Assuming valeurs[0] is the Immat and valeurs[1] is the Type or other detail
                        }
                    }
                    foreach (Vehicule vehicule in listeVehicules)
                    {
                        vehicule.ChargerDispo(vehicule);
                    }
                }
                else
                {
                    Console.WriteLine("Fichier de véhicules non trouvé.");
                }
                return listeVehicules;
            }
            #endregion 
            #region Chauffeurs 
            static List<Chauffeur> ChargerChauffeurs(string chemin)
            {
                List<Chauffeur> listechauffeurs = new List<Chauffeur>();

                if (File.Exists(chemin))
                {
                    foreach (var line in File.ReadAllLines(chemin))
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var valeurs = line.Split(',');
                            Chauffeur chauffeur = new Chauffeur(valeurs[0], valeurs[1], valeurs[2], valeurs[3], valeurs[4], Convert.ToDateTime(valeurs[5]), valeurs[6], int.Parse(valeurs[7]), Convert.ToDateTime(valeurs[8]), valeurs[9]);
                        
                            listechauffeurs.Add(chauffeur);
                            chauffeur.ChargerDispo(chauffeur);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Fichier de chauffeurs non trouvé.");
                }
                return listechauffeurs;
            }
            #endregion
            #region Clients
             static List<Client> ChargerClients(string cheminClients)
            {
                List<Client> listeClients = new List<Client>();
                string cheminclient = "clients.txt";

                if (File.Exists(cheminclient))
                {
                    var lines = File.ReadAllLines(cheminclient);
                    foreach (var line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            string[] valeur = line.Split(',');
                            var client = new Client(valeur[0], valeur[1], valeur[2], valeur[3], valeur[4], Convert.ToDateTime(valeur[5]), valeur[6]);
                            listeClients.Add(client);
                        }
                    }
                }
                return listeClients;
            }
                #endregion
            #region Livraisons
            static List<Livraison> ChargerLivraisons(string chemin, List<Vehicule> listeVehicules, List<Chauffeur> listechauffeurs)
        {
            List<Livraison> listeLivraisons = new List<Livraison>();
            if (File.Exists(chemin))
            {
                foreach (var line in File.ReadAllLines(chemin))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var valeurs = line.Split(';');
                    var estVehicule = listeVehicules.Find(v => v.Immat == valeurs[4]);
                    var estChauffeur = listechauffeurs.Find(c => c.NSS == valeurs[5]);

                    if (estVehicule == null || estChauffeur == null)
                    {
                        Console.WriteLine($"Échec de chargement pour la ligne: {line}");
                        continue;
                    }

                    if (double.TryParse(valeurs[0], out double prix) &&
                        DateTime.TryParse(valeurs[3], out DateTime dateliv) &&
                        int.TryParse(valeurs[6], out int nliv))
                    {
                        listeLivraisons.Add(new Livraison(prix, valeurs[1], valeurs[2], dateliv, estVehicule, estChauffeur, nliv));
                    }
                    else
                    {
                        Console.WriteLine($"Erreur de format pour la ligne '{line}'. Vérifiez que les formats de prix, date et ID de livraison sont corrects.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Fichier de livraisons non trouvé.");
            }
            return listeLivraisons;
        }
            #endregion
            #region Commandes
            static List<Commande> ChargerCommandes(string cheminCommandes, List<Client> listeClients, List<Livraison> listeLivraisons)
            {
                List<Commande> listeCommandes = new List<Commande>();

                if (!File.Exists(cheminCommandes))
                {
                    Console.WriteLine("Fichier de commandes non trouvé.");
                    return listeCommandes;
                }

                var lines = File.ReadAllLines(cheminCommandes);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var valeurs = line.Split(',');
                    if (valeurs.Length < 6) continue; // Assurez-vous qu'il y a assez de données

                    Client client = null;
                    Livraison livraison = null;

                    // Recherche manuelle du client
                    foreach (Client c in listeClients)
                    {
                        if (c.N_Client == valeurs[0])
                        {
                            client = c;
                            break; // Quittez la boucle dès que le client est trouvé
                        }
                    }

                    // Recherche manuelle de la livraison
                    foreach (Livraison l in listeLivraisons)
                    {
                        if (l.Nliv.ToString() == valeurs[5])
                        {
                            livraison = l;
                            break; // Quittez la boucle dès que la livraison est trouvée
                        }
                    }

                    if (client != null && livraison != null && int.TryParse(valeurs[3], out int nCom) && DateTime.TryParse(valeurs[4], out DateTime dateCom))
                    {
                        Commande commande = new Commande(client, valeurs[1], valeurs[2])
                        {
                            NCom = nCom,
                            DateCom = dateCom,
                            Livraison = livraison
                        };
                        listeCommandes.Add(commande);
                    }
                }

                return listeCommandes;
            }
            #endregion
            #endregion

            #region Menu Principal
            bool auth = true;
            while (auth)
            {
                bool clientstatus = false;
                bool adminstatus = false;
                Client client = null;

                Console.WriteLine("Bienvenue.");
                Console.WriteLine("1. Espace Client ");
                Console.WriteLine("2. Espace Admin ");
                Console.WriteLine("3. Quitter");
                Console.Write("Entrez un numéro : ");
                string choix = Console.ReadLine();
                if (choix == "1")
                {
                    Console.WriteLine("1. Inscription ");
                    Console.WriteLine("2. Connexion ");
                    Console.Write("Entrez un numéro : ");
                    string choix1 = Console.ReadLine();
                    if (choix1 == "1")
                    {
                        client = Inscription();

                        Console.WriteLine("Quel est votre identifiant ?");
                        string ident = Console.ReadLine();
                        Console.WriteLine("Quel est votre mot de passe ?");
                        string motdepasse = Console.ReadLine();

                        clientstatus = AuthentificationClient(ident, motdepasse);

                    }
                    else if (choix1 == "2")
                    {
                        Console.WriteLine("Quel est votre identifiant ?");
                        string ident = Console.ReadLine();
                        Console.WriteLine("Quel est votre mot de passe ?");
                        string motdepasse = Console.ReadLine();


                        if (string.IsNullOrWhiteSpace(ident) || string.IsNullOrWhiteSpace(motdepasse))
                        {
                            Console.WriteLine("Incorrect.\nRetour au menu principal...");
                        }
                        else
                        {
                            client = ChargementClient(ident);
                            if (client != null)
                            {
                                client.ChargementCommande(client);
                                clientstatus = AuthentificationClient(ident, motdepasse);
                            }
                            else
                            {
                                Console.WriteLine("Échec du chargement du client. Veuillez vérifier votre identifiant.");
                            }
                        }
                    }
                }
                else if (choix == "2")
                {
                    adminstatus = AuthentificationAdmin();
                }
                else if (choix == "3")
                {
                    auth = false;
                    continue;
                }

                string chemin = "organigramme.txt";
                Organigramme organigramme = ChargerOrganigramme(chemin);
                while (clientstatus || adminstatus)
                {
                    if (adminstatus)
                    {
                        Console.WriteLine("1. Gestion des salariés");
                        Console.WriteLine("2. Gestion des commandes");
                        Console.WriteLine("3. Gestion des clients");
                        Console.WriteLine("4. Gestion des statistiques");
                        Console.WriteLine("5. Quitter");

                        Console.Write("Entrez un numéro : ");
                        string choixadmin = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(choixadmin))
                        {
                            Console.WriteLine("Veuillez entrer un numéro.");
                            continue;
                        }
                        if (choixadmin == "1")
                        {
                            MenuSalarie(organigramme);
                        }
                        else if (choixadmin == "2")
                        {
                            MenuCommande(client, clientstatus);
                        }
                        else if (choixadmin == "3")
                        {
                            MenuClient(clients);
                        }
                        else if (choixadmin == "4")
                        {
                            MenuStatistiques();
                        }
                        else if (choixadmin == "5")
                        {
                            adminstatus = false;
                            break;
                        }
                    }
                    else if (clientstatus)
                    {
                        Console.WriteLine("1. Gestion des commandes");
                        Console.WriteLine("2. Quitter");
                        Console.Write("Entrez un numéro : ");
                        string choixclient = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(choixclient))
                        {
                            Console.WriteLine("Veuillez entrer un numéro.");
                            continue;
                        }
                        if (choixclient == "1")
                        {
                            MenuCommande(client, clientstatus);
                        }
                        else if (choixclient == "2")
                        {
                            clientstatus = false;
                            break;
                        }
                    }
                }
            }
            #endregion

            #region Inscription
            static Client Inscription()
            {
                Console.WriteLine("Quel est votre nom ?");
                string nom = Console.ReadLine();
                Console.WriteLine("Quel est votre prenom ?");
                string prenom = Console.ReadLine();
                Console.WriteLine("Quel est votre email ?");
                string email = Console.ReadLine();
                Console.WriteLine("Quel est votre numéro de téléphone ?");
                string telephone = Console.ReadLine();
                Console.WriteLine("Quel est votre adresse ?");
                string adresse = Console.ReadLine();

                DateTime naissance = DateTime.MinValue;
                bool format = false;
                while (!format)
                {
                    Console.WriteLine("Quel est votre date de naissance ?");
                    Console.WriteLine("YYYY-MM-DD");
                    if (!DateTime.TryParse(Console.ReadLine(), out naissance))
                    {
                        Console.WriteLine("Format de date incorrect. Veuillez entrer une date valide.");
                    }
                    else
                    {
                        format = true;
                    }
                }
                Console.WriteLine("Quel est votre numéro de sécu ?");
                string n_client = Console.ReadLine();
                Console.WriteLine("Créez votre mot de passe :");
                string mdp = Console.ReadLine();
                string id = prenom[0].ToString() + nom;
                Console.WriteLine($" Votre identifiant est : {id} ");

                Client nouveauclient = new Client(nom, prenom, email, telephone, adresse, naissance, n_client);
                nouveauclient.CreerClientData(nom, prenom, email, telephone, adresse, naissance, n_client, id, mdp);

                Console.WriteLine("Le client à été créer");

                return nouveauclient;
            }
            #endregion

            #region Authentification
            static bool AuthentificationClient(string ident, string motdepasse)
            {
                string cheminconnexion = "clients.txt";

                if (File.Exists(cheminconnexion))
                {
                    string[] lines = File.ReadAllLines(cheminconnexion);
                    if (lines.Length > 0)
                    {
                        foreach (string line in lines)
                        {
                            string[] valeur = line.Split(',');
                            if (valeur[8] == ident && valeur[9] == motdepasse)
                            {
                                Console.WriteLine($"{valeur[8]}");
                                Console.WriteLine("Authentification réussie !");
                                return true;
                            }
                        }
                    }
                }
                Console.WriteLine("Une erreur dans l'identifiant ou le mot de passe, réessayer");
                return false;
            }

            static bool AuthentificationAdmin()
            {
                Console.Write("Entrez votre identifiant : ");
                string identifiant = Console.ReadLine();
                Console.Write("Entrez votre mot de passe : ");
                string motde = Console.ReadLine();

                string cheminadmin = "admin.txt";
                if (File.Exists(cheminadmin))
                {
                    string[] lines = File.ReadAllLines(cheminadmin);
                    if (lines.Length > 0)
                    {
                        string[] connexion = lines[0].Split(':');
                        string id = connexion[0];
                        string mdp_base = connexion[1];

                        if (identifiant == id && motde == mdp_base)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                return false;
            }
            #endregion

            #region Menu Salarié
            /// <summary>
            /// Sous menu pour la partie gestion de l'entreprise. Il permet de faire ce qui est décrit au dessus.
            /// </summary>
            static void MenuSalarie(Organigramme organigramme)
            {
                bool menu_ppl = false;
                while (menu_ppl != true)
                {
                    Console.WriteLine("0. Réinitialiser l'organigramme");
                    Console.WriteLine("1. Afficher l'organigramme");
                    Console.WriteLine("2. Ajouter une personne à organigramme");
                    Console.WriteLine("3. Supprimer une personne de l'organigramme");
                    Console.WriteLine("4. Accorder une promotion / retrogradation");
                    Console.WriteLine("5. Modification des informations d'un salarie");
                    Console.WriteLine("6. Afficher le planning des chauffeurs");
                    Console.WriteLine("7. Afficher les informations sur les salaries");
                    Console.WriteLine("8. Retour au menu");
                    Console.Write("Entrez un numéro : ");
                    string choix = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(choix))
                    {
                        Console.WriteLine("Veuillez entrer un numéro.");
                        continue;
                    }
                    if (choix == "0")
                    {
                        ChargerOrganigrammeBase();
                    }
                    else if (choix == "1")
                    {
                        string chemin = "organigramme.txt";
                        organigramme = ChargerOrganigramme(chemin);
                        Console.WriteLine("Organigramme :");
                        organigramme.AfficherOrganigramme();
                    }
                    else if (choix == "2")
                    {
                        Console.WriteLine("Ajout d'une personne à l'organigramme :");
                        Console.WriteLine("Nom du supérieur hierarchique :");
                        string nomParent = Console.ReadLine();

                        Noeud parent = organigramme.RechercherNoeud(nomParent);
                        if (parent != null)
                        {
                            Console.Write("Nom :");
                            string nom = Console.ReadLine();

                            Console.Write("Prénom :");
                            string prenom = Console.ReadLine();

                            Console.Write("Email :");
                            string email = Console.ReadLine();

                            Console.Write("Téléphone :");
                            string telephone = Console.ReadLine();

                            Console.Write("Adresse :");
                            string adresse = Console.ReadLine();

                            Console.Write("Date de naissance : (yyyy-mm--dd)");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime naissance))
                            {
                                Console.WriteLine("Format de date incorrect.");
                                return;
                            }

                            Console.Write("NSS :");
                            string nss = Console.ReadLine();

                            Console.Write("Salaire :");
                            double salaire = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Date d'entrée : (yyyy-mm--dd)");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateEntree))
                            {
                                Console.WriteLine("Format de date incorrect.");
                                return;
                            }
                            Console.Write("Poste :");
                            string poste = Console.ReadLine();

                            Salarie nouveau_Salarie = new Salarie(nom, prenom, email, telephone, adresse, naissance, nss, salaire, dateEntree, poste);
                            Noeud Salarie_n = new Noeud(nouveau_Salarie);

                            if(poste == "Chauffeur")
                            {
                                string cheminchauffeurs = "chauffeurs.txt";

                                string ligne = $"{nom},{prenom},{email},{telephone},{adresse},{naissance.ToString("yyyy-MM-dd")},{nss},{salaire},{dateEntree.ToString("yyyy-MM-dd")},{poste}" + ",{}, []";

                                string[] lignesEcrites = File.ReadAllLines(cheminchauffeurs);

                                if (lignesEcrites.Length == 0)
                                {
                                    File.WriteAllText(cheminchauffeurs, ligne);
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

                                    File.WriteAllLines(cheminchauffeurs, lignesEcrites);

                                }
                            }

                            organigramme.AjouterHierarchique(parent, Salarie_n);
                            Console.WriteLine("Salarié ajouté");
                            string chemin = "organigramme.txt";
                            EnregistrerOrganigramme(organigramme, chemin);
                        }
                        else
                        {
                            Console.WriteLine("Supérieur Introuvable");
                        }
                    }
                    else if (choix == "3")
                    {
                        Console.WriteLine("Suppression d'une personne à l'organigramme :");
                        Console.WriteLine("Nom du salarié renvoyé : ");
                        string Salarie_suppr = Console.ReadLine();

                        Console.WriteLine("Nom du supérieur hierarchique :");
                        string n_parent = Console.ReadLine();

                        Noeud parent = organigramme.RechercherNoeud(n_parent);
                        Noeud salarie = organigramme.RechercherNoeud(Salarie_suppr);
                        if (salarie != null && parent != null)
                        {
                            organigramme.SupprimerHierarchique(parent, salarie);
                            Console.WriteLine("Le salarié a été renvoyé");
                            string chemin = "organigramme.txt";
                            EnregistrerOrganigramme(organigramme, chemin);
                        }
                        else
                        {
                            if (salarie == null) Console.WriteLine("Le salarié n'existe pas");
                            if (parent == null) Console.WriteLine("Le supérieur est introuvable");
                        }
                    }
                    else if (choix == "4")
                    {
                        Console.WriteLine("Nom du salarié :");
                        string nom_salarie = Console.ReadLine();
                        Noeud salarie = organigramme.RechercherNoeud(nom_salarie);
                        if (salarie != null)
                        {
                            Console.Write("Nom du nouveau supérieur hiérarchique : ");
                            string n_parent = Console.ReadLine();

                            Console.WriteLine("Nom de l'ancien supérieur :");
                            string a_parent = Console.ReadLine();

                            Console.WriteLine("Nom du nouveau poste :");
                            string nouveauPoste = Console.ReadLine();

                            Console.WriteLine("Nouveau Salaire :");
                            string salaire = Console.ReadLine();
                            double nouveauSalaire = 0;
                            bool salaire_ = double.TryParse(salaire, out nouveauSalaire);

                            Noeud nouveau_parent = organigramme.RechercherNoeud(n_parent);
                            Noeud ancien_parent = organigramme.RechercherNoeud(a_parent);
                            if (nouveau_parent != null)
                            {
                                organigramme.ModifierHierarchique(nouveau_parent, salarie, nouveauPoste, nouveauSalaire, ancien_parent);
                                Console.WriteLine("La hiérarchie du salarié a été modifiée avec succès");
                                string chemin = "organigramme.txt";
                                EnregistrerOrganigramme(organigramme, chemin);
                            }
                            else
                            {
                                Console.WriteLine("Le nouveau supérieur hiérarchique est introuvable");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Le salarié n'existe pas");
                        }
                    }
                    else if (choix == "5")
                    {
                        Console.WriteLine("Nom du salarié :");
                        string nom_salarie = Console.ReadLine();
                        Noeud salarie = organigramme.RechercherNoeud(nom_salarie);
                        if (salarie != null)
                        {
                            Console.Write("Nouveau nom : ");
                            string nouveauNom = Console.ReadLine();

                            Console.WriteLine("Nouvelle adresse :");
                            string nouvelleAdresse = Console.ReadLine();

                            Console.WriteLine("Nouveau mail :");
                            string nouveauMail = Console.ReadLine();

                            Console.WriteLine("Nouveau n° de téléphone :");
                            string nouveauTel = Console.ReadLine();

                            Console.WriteLine("Nouveau poste :");
                            string nouveauPoste = Console.ReadLine();

                            Console.WriteLine("Nouveau Salaire :");
                            string salaire = Console.ReadLine();
                            double nouveauSalaire = 0;
                            bool salaire_ = double.TryParse(salaire, out nouveauSalaire);

                            organigramme.ModifierSalarie(salarie, nouveauNom, nouvelleAdresse, nouveauMail, nouveauTel, nouveauPoste, nouveauSalaire);
                            Console.WriteLine("Le salarié a été modifié");
                            string chemin = "organigramme.txt";
                            EnregistrerOrganigramme(organigramme, chemin);
                        }
                    }
                    else if (choix == "6")
                    {
                        string cheminchauffeurs = "chauffeurs.txt";
                        List<Chauffeur> listechauffeurs = ChargerChauffeurs(cheminchauffeurs);
                        foreach(Chauffeur chauffeur in listechauffeurs)
                        {
                            chauffeur.Planning();

                        }
                        
                    }
                    else if (choix == "7")
                    {
                        organigramme.AfficherDetailsSalarie();
                    }
                    else if (choix == "8")
                    {
                        string chemin = "organigramme.txt";
                        EnregistrerOrganigramme(organigramme, chemin);
                        menu_ppl = true;
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    }
                }
            }
            #endregion

            #region Menu Commande
            /// <summary>
            /// Sous Menu pour la gestion des commandes
            /// </summary>
            static void MenuCommande(Client client, bool clientstatus)
            {
                bool menu_ppl = false;
                while (menu_ppl != true)
                {
                    List<Vehicule> listeVehicules = ChargerVehicules("vehicules.txt");
                    List<Chauffeur> listeChauffeurs = ChargerChauffeurs("chauffeurs.txt");
                    List<Livraison> listeLivraisons = ChargerLivraisons("livraison.txt", listeVehicules, listeChauffeurs);
                    List<Client> listeClients = ChargerClients("clients.txt");
                    List<Commande> listeCommandes = ChargerCommandes("commandes.txt", listeClients, listeLivraisons);

                    if (clientstatus)
                    {
                        Console.WriteLine("1. Effectuer une commande");
                    }
                    Console.WriteLine("2. Gestion des commandes");
                    Console.WriteLine("3. Quitter");
                    Console.WriteLine("Entrez un numéro : ");

                    string choix = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(choix))
                    {
                        Console.WriteLine("Veuillez entrer un numéro.");
                        continue;
                    }
                    if (choix == "1" && clientstatus)
                    {
                        Console.WriteLine("Commander :");
                        Console.WriteLine("Rentrez les informations de la commande :");
                        List<string> villes = new List<string>();
                        if (File.Exists("distances.txt"))
                        {
                            string[] lignes = File.ReadAllLines("distances.txt");
                            foreach (string ligne in lignes)
                            {
                                if (!string.IsNullOrWhiteSpace(ligne))
                                {
                                    string[] parts = ligne.Split(';');
                                    if (parts.Length >= 2)
                                    {
                                        villes.Add(parts[1]);
                                    }
                                }
                            }
                        }

                        Console.WriteLine("Quel est le point d'arrivée ?");

                        Console.WriteLine("Villes d'arrivée disponibles :");
                        foreach (string ville in villes)
                        {
                            Console.WriteLine("- " + ville);
                        }
                        string pointArriv = Console.ReadLine();

                        while (!villes.Contains(pointArriv))
                        {
                            Console.WriteLine("Le point d'arrivée n'est pas valide. Veuillez entrer une ville valide.");
                            pointArriv = Console.ReadLine();
                        }
                        Console.WriteLine("Quel vehicule souhaitez vous utilisez ?");
                        Console.WriteLine("1. Voiture");
                        Console.WriteLine("2. Camionette");
                        Console.WriteLine("3. Poids Lourd");

                        string choix1 = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(choix1))
                        {
                            Console.WriteLine("Veuillez entrer un numéro.");
                            continue;
                        }
                        string transport = null;

                        if (choix1 == "1")
                        {
                            transport = "Voiture";
                        }
                        else if (choix1 == "2")
                        {
                            transport = "Camionette";
                        }
                        else if (choix1 == "3")
                        {
                            Console.WriteLine("Quel Type de Poids Lourd ? : ");
                            Console.WriteLine("1. Camion_benne");
                            Console.WriteLine("2. Camion_citerne");
                            Console.WriteLine("3. Camion_frigorifique");
                            string choix2 = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(choix2))
                            {
                                Console.WriteLine("Veuillez entrer un numéro.");
                                continue;
                            }
                            if (choix2 == "1")
                            {
                                transport = "Camion_benne";
                            }
                            else if (choix2 == "2")
                            {
                                transport = "Camion_citerne";
                            }
                            else if (choix2 == "3")
                            {
                                transport = "Camion_frigorifique";
                            }
                        }
                        

                        PrendreCommande prendreCommande = new PrendreCommande(listeVehicules, listeChauffeurs);
                        Commande nouvelleCommande = prendreCommande.Commander(client, pointArriv, transport);
                        Console.WriteLine(nouvelleCommande);

                        listeCommandes = ChargerCommandes("commandes.txt", listeClients, listeLivraisons);
                        Console.ResetColor();
                        
                    }
                    else if (choix == "2")
                    {
                        bool sous_menu = true;
                        while (sous_menu)
                        {
                            Console.WriteLine("1. Afficher toutes les commandes du client");
                            Console.WriteLine("2. Supprimer une commande du client");
                            Console.WriteLine("3. Retour au menu principal");
                            Console.Write("Choisissez une option : ");
                            string choixCommande = Console.ReadLine();

                            if (choixCommande == "1")
                            {
                                listeCommandes = ChargerCommandes("commandes.txt", listeClients, listeLivraisons);
                                int nbCommandesClient = listeCommandes.Count(commande => commande.Client.N_Client == client.N_Client);
                                Console.WriteLine("Nombre de commandes pour le client: " + nbCommandesClient);

                                foreach (Commande commande in listeCommandes)
                                {
                                    if (commande.Client.N_Client == client.N_Client)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(commande.ToString());
                                        Console.ResetColor();
                                    }
                                }
                            }
                            else if (choixCommande == "2")
                            {
                                Console.WriteLine(" Veuillez entrez le numéro de la commande que vous voulez annuler : ");
                                string numeroCommande = Console.ReadLine();

                                bool commandeTrouvee = false;
                                bool commandeSupprimee = false;

                                foreach (Commande commande in listeCommandes)
                                {
                                    if (int.Parse(numeroCommande) == commande.NCom)
                                    {
                                        commandeTrouvee = true;
                                        if (commande.Client.N_Client == client.N_Client)
                                        {
                                            client.Commande.Remove(commande.NCom);
                                            commande.SupprimerCommandeData(numeroCommande);
                                            foreach (Chauffeur chauffeur in listeChauffeurs)
                                            {
                                                if (commande.Livraison.Chauf == chauffeur)
                                                {
                                                    chauffeur.SupprimerCommande(chauffeur, commande.Livraison.Dateliv, int.Parse(numeroCommande));
                                                }
                                            }
                                            foreach (Vehicule vehicule in listeVehicules)
                                            {
                                                if (commande.Livraison.Vehicule == vehicule)
                                                {
                                                    vehicule.SupprimerLivraison(vehicule, vehicule.Immat, commande.Livraison.Dateliv);
                                                }
                                            }
                                            client.SupprimerCommande(commande, client);
                                            commandeSupprimee = true;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Vous ne pouvez pas supprimer cette commande car elle n'appartient pas à votre compte.");
                                        }
                                    }
                                }
                                if (!commandeTrouvee)
                                {
                                    Console.WriteLine("La commande n'a pas été trouvée.");
                                }
                                else if (commandeSupprimee)
                                {
                                    foreach (Livraison livraison in listeLivraisons)
                                    {
                                        if (int.Parse(numeroCommande) == livraison.Nliv)
                                        {
                                            livraison.SupprimerLivraisonData(numeroCommande);
                                        }
                                    }
                                    listeCommandes = ChargerCommandes("commandes.txt", listeClients, listeLivraisons);
                                }
                            }
                            else if (choixCommande == "3")
                            {
                                sous_menu = false;
                            }
                            else
                            {
                                Console.WriteLine("Option invalide, veuillez réessayer.");
                            }
                        }
                    }
                    else if (choix == "3")
                    {
                        menu_ppl = true;
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    }
                }
            }
            #endregion

            #region Menu Client
            /// <summary>
            /// Sous menu pour la gestion des clients
            /// </summary>
            static void MenuClient(List<Client> clients)
            {
                

                bool menu_ppl = false;
                while (menu_ppl != true)
                {
                    string cheminClients = "clients.txt";
                    List<Client> listeClients = ChargerClients(cheminClients);
                    Console.WriteLine("1. Ajouter un client");
                    Console.WriteLine("2. Supprimer un client");
                    Console.WriteLine("3. Modifier un client");
                    Console.WriteLine("4. Afficher les clients");
                    Console.WriteLine("5. Quitter");

                    string choix = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(choix))
                    {
                        Console.WriteLine("Veuillez entrer un numéro.");
                        continue;
                    }
                    Console.WriteLine("Entrez un numéro : ");

                    if (choix == "1")
                    {
                        Console.WriteLine("1. Ajouter un client depuis la console");
                        Console.WriteLine("2. Ajouter un client depuis un fichier");
                        Console.WriteLine("3. Quitter");
                        string choix2 = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(choix2)) ;
                        {
                            Console.WriteLine("Veuillez entrer un numéro.");
                        }
                        Console.WriteLine("Entrez un numéro : ");
                        if (choix2 == "1")
                        {
                            Inscription();
                        }
                        else if (choix2 == "2")
                        {
                            void AjoutClientFichier(string chemin)
                            {
                                string[] lignes = File.ReadAllLines(chemin);
                                foreach (string ligne in lignes)
                                {
                                    string[] elmt = ligne.Split(',');
                                    if (elmt.Length == 7)
                                    {
                                        string nom = elmt[0];
                                        string prenom = elmt[1];
                                        string email = elmt[2];
                                        string telephone = elmt[3];
                                        string adresse = elmt[4];
                                        DateTime naissance = Convert.ToDateTime(elmt[5]);
                                        string n_client = elmt[6];
                                        string id = elmt[7];
                                        string mdp = elmt[8];
                                        Client nouveau_client = new Client(nom, prenom, email, telephone, adresse, naissance, n_client);
                                        clients.Add(nouveau_client);
                                        nouveau_client.CreerClientData(nom, prenom, email, telephone, adresse, naissance, n_client, id, mdp);
                                    }
                                }
                            }

                            Console.WriteLine("Entrez le nom du fichier : ");
                            string chemin = Console.ReadLine();
                            AjoutClientFichier(chemin);
                        }
                        else if (choix2 == "3")
                        {
                            break;
                        }
                    }
                    else if (choix == "2")
                    {
                        Console.WriteLine("Entrez le numéro du client à supprimer : ");
                        string n_client = Console.ReadLine();
                        Client.SupprimerClient(clients, n_client);
                    }
                    else if (choix == "3")
                    {
                        Console.WriteLine("Entrez le numéro du client à modifier : ");
                        string n_client = Console.ReadLine();
                        Client.ModifierClient(clients, n_client);
                    }
                    else if (choix == "4")
                    {
                        Console.WriteLine("Liste des clients :\n");
                        Client.Afficher(listeClients);
                        //foreach (var client in clients)
                        //{
                        //    client.Afficher();
                        //}
                    }
                    else if (choix == "5")
                    {
                        menu_ppl = true;
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    }
                }
            }
            #endregion

            #region Menu Statistiques
            /// <summary>
            /// Sous menu pour la gestion des statistiques de l'entreprise
            /// </summary>
            static void MenuStatistiques()
            {
                string cheminVehicules = "vehicules.txt";
                string cheminChauffeurs = "chauffeurs.txt";
                string cheminClients = "clients.txt";
                string cheminLivraisons = "livraison.txt";
                string cheminCommandes = "commandes.txt";

                List<Client> listeClients = ChargerClients(cheminClients);
                List<Chauffeur> listeChauffeurs = ChargerChauffeurs(cheminChauffeurs);
                List<Vehicule> listeVehicules = ChargerVehicules(cheminVehicules);
                List<Livraison> listeLivraisons = ChargerLivraisons(cheminLivraisons, listeVehicules, listeChauffeurs);
                List<Commande> listeCommandes = ChargerCommandes(cheminCommandes, listeClients, listeLivraisons);
                bool menu_ppl = false;
                while (!menu_ppl)
                {
                    Console.WriteLine("1. Afficher le nombre de livraisons par chauffeur");
                    Console.WriteLine("2. Afficher les commandes selon une période de temps");
                    Console.WriteLine("3. Afficher la moyenne des prix des livraisons");
                    Console.WriteLine("4. Afficher la moyenne des comptes clients");
                    Console.WriteLine("5. Afficher la liste des commandes pour un client");
                    Console.WriteLine("6. Quitter");                    
                    string choix = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(choix))
                    {
                        Console.WriteLine("Veuillez entrer un numéro.");
                        continue;
                    }
                    Console.WriteLine("Entrez un numéro : ");

                    if (choix == "1")
                    {
                        Console.WriteLine("1. Afficher les livraisons pour un chauffeur spécifique");
                        Console.WriteLine("2. Afficher toutes les livraisons pour tous les chauffeurs");
                        string choix2 = Console.ReadLine();

                        if (choix2 == "1")
                        {
                            Console.WriteLine("Choisissez le chauffeur :");
                            for (int i = 0; i < listeChauffeurs.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {listeChauffeurs[i].Prenom} {listeChauffeurs[i].Nom} ");
                            }
                            int choixChauffeur = int.Parse(Console.ReadLine()) - 1;
                            string nss = listeChauffeurs[choixChauffeur].NSS;
                            Statistiques.AfficherLivraisonsParChauffeur(listeLivraisons, listeChauffeurs, nss);
                        }
                        else if (choix2 == "2")
                        {
                            Statistiques.AfficherLivraisonsParChauffeur(listeLivraisons, listeChauffeurs);
                        }
                        else
                        {
                            Console.WriteLine("Choix invalide.");
                        }
                    }
                    else if (choix == "2")
                    {
                        Console.WriteLine("Entrez la date de début (format AAAA-MM-JJ) : ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime debut))
                        {
                            Console.WriteLine("Format de date incorrect pour la date de début.");
                            return;
                        }
                        Console.WriteLine("Entrez la date de fin (format AAAA-MM-JJ) : ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime fin))
                        {
                            Console.WriteLine("Format de date incorrect pour la date de fin.");
                            return;
                        }

                        if (debut > fin)
                        {
                            Console.WriteLine("La date de début doit être antérieure à la date de fin.");
                            return;
                        }

                        Statistiques.LivraisonFiltre filtreDate = livraison => livraison.Dateliv >= debut && livraison.Dateliv <= fin;

                        Statistiques.AfficherCommandesParPeriode(listeLivraisons, listeChauffeurs, filtreDate);
                    }
                    else if (choix == "3")
                    {
                        Statistiques.AfficherMoyennePrixLivraisons(listeLivraisons);
                    }
                    else if (choix == "4")
                    {
                        Statistiques.AfficherMoyenneComptesClients(listeCommandes, listeLivraisons);
                    }
                    else if (choix == "5")
                    {
                        Console.WriteLine("Choisissez le client :");
                        for (int i = 0; i < listeClients.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {listeClients[i].Prenom} {listeClients[i].Nom} ");
                        }
                        int choixClientIndex;
                        if (!int.TryParse(Console.ReadLine(), out choixClientIndex) || choixClientIndex < 1 || choixClientIndex > listeClients.Count)
                        {
                            Console.WriteLine("Choix invalide.");
                            continue;
                        }
                        string N_Client = listeClients[choixClientIndex - 1].N_Client;
                        Statistiques.AfficherCommandesClient(listeCommandes, listeLivraisons, N_Client);
                    }
                    else if (choix == "6")
                    {
                        menu_ppl = true;
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    }
                }
            }
            #endregion

            #region Planning

            static void Planning()
            {



            }

            #endregion
        }
    }
}