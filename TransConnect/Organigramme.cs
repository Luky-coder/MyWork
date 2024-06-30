using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FOURNIER_LOPES
{
    public class Organigramme
    {
        private Noeud racine;

        public Organigramme(Salarie PDG = null)
        {
            this.racine = new Noeud(PDG);
        }

        public Noeud Racine
        {
            set { this.racine = value; }
            get { return this.racine; }
        }

        public void AjouterHierarchique(Noeud parent, Noeud nouveau) 
        {
            if (parent != null || nouveau != null)
            {
                parent.AjouterEnfant(nouveau);
                nouveau.Parent = parent;
            }
            else
            {
                Console.WriteLine("Erreur");
            }
        }

        public void SupprimerHierarchique(Noeud parent, Noeud salarie)
        {
            if (parent == null || salarie == null)
            {
                Console.WriteLine("Le salarié ou le parent est null.");
                return;
            }
            if (parent.Enfant.Contains(salarie) == false)
            {
                Console.WriteLine($"Erreur: {parent} n'est pas le supérieur de {salarie}.");
                return;
            }
            Noeud nouveauParent = parent;
            foreach (var frere in parent.Enfant)
            {
                if (frere != salarie)
                {
                    nouveauParent = frere;
                    break;
                }
            }
            if (nouveauParent == salarie)
            {
                nouveauParent = parent;
            }
            foreach (var enfant in new List<Noeud>(salarie.Enfant))
            {
                AjouterHierarchique(nouveauParent, enfant);
            }
            parent.Enfant.Remove(salarie);
            Console.WriteLine("OK.");
        }

        public bool Frere(Noeud salarie)
        {
            if ( salarie.Parent !=null )
            {
                foreach (var frere in salarie.Parent.Enfant )
                {
                    if ( frere != salarie )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ModifierHierarchique(Noeud n_parent, Noeud salarie, string nouveauPoste, double nouveauSalaire, Noeud ancien_parent = null)
        {
            salarie.Salarie.Poste = nouveauPoste;
            salarie.Salarie.Salaire = nouveauSalaire;
            AjouterHierarchique(n_parent, salarie);
            SupprimerHierarchique(ancien_parent, salarie);
        }

        public void ModifierSalarie(Noeud salarie, string nouveauNom, string nouvelleAdresse, string nouveauMail, string nouveauTel, string nouveauPoste, double ?nouveauSalaire = null)
        {
            if (!string.IsNullOrEmpty(nouveauNom))
            {
                salarie.Salarie.Nom = nouveauNom;
            }

            if (!string.IsNullOrEmpty(nouvelleAdresse))
            {
                salarie.Salarie.Adresse = nouvelleAdresse;
            }

            if (!string.IsNullOrEmpty(nouveauMail))
            {
                salarie.Salarie.Email = nouveauMail;
            }

            if (!string.IsNullOrEmpty(nouveauTel))
            {
                salarie.Salarie.Telephone = nouveauTel;
            }

            if (!string.IsNullOrEmpty(nouveauPoste))
            {
                salarie.Salarie.Poste = nouveauPoste;
            }

            if (nouveauSalaire != null)
            {
                salarie.Salarie.Salaire = nouveauSalaire.Value;
            }
        }

        public Noeud RechercherNoeud(string nom)
        {
            return RechercherNoeudRec(racine, nom);
        }

        public Noeud RechercherNoeudRec(Noeud noeud_courrant, string nom)
        {
            if (noeud_courrant == null)
            {
                return null;
            }
            if (noeud_courrant.Salarie.Nom == nom)
            {
                return noeud_courrant;
            }
            foreach (var enfant in noeud_courrant.Enfant)
            {
                Noeud resultat = RechercherNoeudRec(enfant, nom);
                if (resultat != null)
                {
                    return resultat;
                }
            }
            return null;
        }

        public void AfficherOrganigramme()
        {
            Console.WriteLine("Organigramme :");
            if (racine != null)
            {
                AfficherOrganigrammeRec(racine, 0);
            }
        }
     
        public  void AfficherOrganigrammeRec(Noeud noeud, int niveau)
        {
            if (noeud != null)
            {
                Console.WriteLine(new string(' ', niveau * 5) + "- " + noeud.Salarie.Prenom + " " + noeud.Salarie.Nom + " (" + noeud.Salarie.Poste + ")\n");
                foreach (var enfant in noeud.Enfant)
                {
                    AfficherOrganigrammeRec(enfant, niveau + 1);
                }
            }
        }

        public void AfficherDetailsSalarie()
        {
            Console.WriteLine("Détails de tous les salariés :");
            if (racine != null)
            {   
                Console.ForegroundColor = ConsoleColor.Green;
                AfficherDetailsSalariesRec(racine);
                Console.ResetColor();
            }
        }

        private void AfficherDetailsSalariesRec(Noeud noeud)
        {
            if (noeud != null)
            {
                Salarie salarie = noeud.Salarie;
                Console.WriteLine(noeud.Salarie.ToString());

                foreach (var enfant in noeud.Enfant)
                {
                    AfficherDetailsSalariesRec(enfant);
                }
            }
        }
    }
}
