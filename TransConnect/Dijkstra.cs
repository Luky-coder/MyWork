using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOURNIER_LOPES
{
    public class Dijkstra
    {
        private Graphe graphe;

        public Dijkstra(Graphe graphe)
        {
            this.graphe = graphe;
        }

        public List<Arc> TrouverCheminPlusCourt(string depart, string arrivee)
        {
            Dictionary<string, int> distances = new Dictionary<string, int>();
            Dictionary<string, string> noeudsPrecedents = new Dictionary<string, string>();
            List<string> noeuds = new List<string>();

            foreach (var sommet in graphe.ListeAdjacence)
            {
                distances[sommet.Key] = int.MaxValue;
                noeudsPrecedents[sommet.Key] = null;
                noeuds.Add(sommet.Key);
            }

            distances[depart] = 0;

            while (noeuds.Count != 0)
            {
                noeuds.Sort((x, y) => distances[x] - distances[y]);
                string plusPetit = noeuds[0];
                noeuds.Remove(plusPetit);

                if (plusPetit == arrivee)
                {
                    List<Arc> chemin = new List<Arc>();
                    while (noeudsPrecedents[plusPetit] != null)
                    {
                        string predecesseur = noeudsPrecedents[plusPetit];
                        Arc arc = null;

                        // Trouver l'arc correspondant
                        foreach (var voisin in graphe.ListeAdjacence[predecesseur])
                        {
                            if (voisin.Destination == plusPetit)
                            {
                                arc = voisin;
                                break;
                            }
                        }

                        if (arc != null)
                        {
                            chemin.Add(arc);
                        }
                        plusPetit = noeudsPrecedents[plusPetit];
                    }
                    chemin.Reverse();
                    return chemin;
                }

                if (distances[plusPetit] == int.MaxValue)
                {
                    break;
                }

                foreach (var voisin in graphe.ListeAdjacence[plusPetit])
                {
                    int alt = distances[plusPetit] + voisin.Distance;
                    if (alt < distances[voisin.Destination])
                    {
                        distances[voisin.Destination] = alt;
                        noeudsPrecedents[voisin.Destination] = plusPetit;
                    }
                }
            }

            return null;
        }
    }
}

