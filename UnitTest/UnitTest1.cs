using System.Diagnostics;

namespace FOURNIER_LOPES
{
    [TestClass]
    public class UnitTest1
    {
        private Organigramme organigramme;
        private Salarie pdg;
        private Noeud PDG;
        private Client client;
        private List<Vehicule> listeVehicules;
        private List<Chauffeur> listeChauffeurs;
        private Graphe graphe;

        [TestInitialize]
        public void Initialisation()
        {
            pdg = new Salarie("Dupond", "Mr", "mr.dupont@example.com", "123456789", "123 Rue de Paris", new DateTime(1980, 5, 10), "1234567890", 150000, new DateTime(2000, 1, 1), "Directeur Général");
            PDG = new Noeud(pdg);
            organigramme = new Organigramme(pdg);
            client = new Client("Fournier", "Killian", "killian.fournier@gmail.com", "0785958746", "1 rue de la liberté", new DateTime(2003, 9, 20), "1084510560156");

            string cheminvehicule = "vehicules.txt";
            listeVehicules = new List<Vehicule>();
            if (File.Exists(cheminvehicule))
            {
                foreach (var line in File.ReadAllLines(cheminvehicule))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var valeurs = line.Split(',');
                        listeVehicules.Add(new Vehicule(valeurs[0], valeurs[1]));
                    }
                }
                foreach (Vehicule vehicule in listeVehicules)
                {
                    vehicule.ChargerDispo(vehicule);
                }
            }

            string cheminchauffeur = "chauffeurs.txt";
            listeChauffeurs = new List<Chauffeur>();

            if (File.Exists(cheminchauffeur))
            {
                foreach (var line in File.ReadAllLines(cheminchauffeur))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var valeurs = line.Split(',');
                        Chauffeur chauffeur = new Chauffeur(valeurs[0], valeurs[1], valeurs[2], valeurs[3], valeurs[4], Convert.ToDateTime(valeurs[5]), valeurs[6], int.Parse(valeurs[7]), Convert.ToDateTime(valeurs[8]), valeurs[9]);

                        listeChauffeurs.Add(chauffeur);
                        chauffeur.ChargerDispo(chauffeur);
                    }
                }
            }

            string cheminFichier = "distances.txt";
            graphe = new Graphe();
            foreach (string ligne in File.ReadLines(cheminFichier))
            {
                string[] parties = ligne.Split(';');
                string source = parties[0];
                string destination = parties[1];
                int distance = int.Parse(parties[2]);
                graphe.AjouterArc(source, destination, distance);
            }
        }

        [TestMethod]
        public void Test_EstFrere()
        {
            Salarie salarie1 = new Salarie("Dupont", "Jean", "jean.dupont@example.com", "123456789", "123 Rue de Paris", new DateTime(1985, 5, 15), "111111111", 50000, new DateTime(2010, 6, 1), "Commercial");
            Noeud noeud1 = new Noeud(salarie1);
            Salarie salarie2 = new Salarie("Durand", "Paul", "paul.durand@example.com", "987654321", "456 Rue de Paris", new DateTime(1987, 7, 20), "222222222", 52000, new DateTime(2011, 7, 1), "Commercial");
            Noeud noeud2 = new Noeud(salarie2);

            organigramme.AjouterHierarchique(PDG, noeud1);
            organigramme.AjouterHierarchique(PDG, noeud2);

            Assert.AreEqual(PDG, noeud1.Parent);
            Assert.AreEqual(PDG, noeud2.Parent);

            bool result1 = organigramme.Frere(noeud1);
            bool result2 = organigramme.Frere(noeud2);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void Test_SupprimerHierarchique()
        {
            Salarie salarie1 = new Salarie("Dupont", "Jean", "jean.dupont@example.com", "123456789", "123 Rue de Paris", new DateTime(1985, 5, 15), "111111111", 50000, new DateTime(2010, 6, 1), "Commercial");
            Noeud noeud1 = new Noeud(salarie1);
            Salarie salarie2 = new Salarie("Durand", "Paul", "paul.durand@example.com", "987654321", "456 Rue de Paris", new DateTime(1987, 7, 20), "222222222", 52000, new DateTime(2011, 7, 1), "Commercial");
            Noeud noeud2 = new Noeud(salarie2);
            Salarie salarie3 = new Salarie("Martin", "Luc", "luc.martin@example.com", "111222333", "789 Rue de Lyon", new DateTime(1990, 8, 25), "333333333", 53000, new DateTime(2012, 8, 1), "Assistant");
            Noeud noeud3 = new Noeud(salarie3);

            organigramme.AjouterHierarchique(PDG, noeud1);
            organigramme.AjouterHierarchique(PDG, noeud2);
            organigramme.AjouterHierarchique(noeud1, noeud3);

            // Suppression d'un nœud avec des enfants
            organigramme.SupprimerHierarchique(PDG, noeud1);

            // Vérification que le parent de noeud3 est maintenant noeud2 (frère de noeud1)
            Assert.AreEqual(noeud2, noeud3.Parent);
            Assert.IsFalse(PDG.Enfant.Contains(noeud1));
            Assert.IsTrue(noeud2.Enfant.Contains(noeud3));

            // Suppression d'un noeud sans enfants
            organigramme.SupprimerHierarchique(PDG, noeud2);
            Assert.IsFalse(PDG.Enfant.Contains(noeud2));
        }

        [TestMethod]
        public void Test_PriseDeCommande()
        {

            PrendreCommande Commander = new PrendreCommande(listeVehicules, listeChauffeurs);

            Commande nouvelleCommande = Commander.Commander(client, "Paris", "Voiture");

            Assert.IsTrue(client.Commande.Contains(nouvelleCommande.NCom));
        }

        [TestMethod]
        public void Test_Disponibilité()
        {
            Chauffeur chauffeur = new Chauffeur("Test", "Test", "test@test.com", "test", "test", new DateTime(1987, 7, 20), "test", 0, new DateTime(2011, 7, 1), "Chauffeur");
            Vehicule vehicule = new Vehicule("test", "Voiture");
            DateTime date = new DateTime(2024, 05, 22);

            chauffeur.Dispo.Add(new DateTime(date.Year, date.Month, date.Day));
            vehicule.Dispo.Add(new DateTime(date.Year, date.Month, date.Day));

            // Le chauffeur et vehicule renvoie False si ils ne sont pas disponibles
            Assert.IsFalse(chauffeur.EstDisponible(date));
            Assert.IsFalse(vehicule.EstDisponible(date));
        }

        [TestMethod]
        public void Test_AfficherMoyennePrixLivraisons_AucuneLivraison()
        {
            List<Livraison> livraisons = new List<Livraison>();

            using (var ligne = new StringWriter())
            {
                Console.SetOut(ligne);

                Statistiques.AfficherMoyennePrixLivraisons(livraisons);

                string sortie = "Aucune livraison fournie";
                var result = ligne.ToString();

                Assert.IsTrue(result.Contains(sortie), $"La sortie de la méthode ne correspond pas à la sortie attendue. Sortie actuelle : {result}");
            }
        }


        [TestMethod]
        public void Test_Dijkstra()
        {
            Dijkstra dijkstra = new Dijkstra(graphe);

            List<Arc> cheminpluscourtprevu = new List<Arc>
        {
            new Arc { Source = "Paris", Destination = "Lyon", Distance = 464 },
            new Arc { Source = "Lyon", Destination = "Nimes", Distance = 256 },
            new Arc { Source = "Nimes", Destination = "Marseille", Distance = 126 }
        };

            List<Arc> cheminpluscourt = dijkstra.TrouverCheminPlusCourt("Paris", "Marseille");

            Assert.AreEqual(cheminpluscourtprevu.Count, cheminpluscourt.Count, "Les chemins n'ont pas la même longueur.");

            for (int i = 0; i < cheminpluscourtprevu.Count; i++)
            {
                Assert.AreEqual(cheminpluscourtprevu[i].Source, cheminpluscourt[i].Source, $"Les sources des éléments à l'index {i} ne sont pas égales.");
                Assert.AreEqual(cheminpluscourtprevu[i].Destination, cheminpluscourt[i].Destination, $"Les destinations des éléments à l'index {i} ne sont pas égales.");
                Assert.AreEqual(cheminpluscourtprevu[i].Distance, cheminpluscourt[i].Distance, $"Les distances des éléments à l'index {i} ne sont pas égales.");
            }
        }
    }
}
