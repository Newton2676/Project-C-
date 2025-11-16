using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Graphe
    {
        private List<Noeud> noeuds;
        private List<Lien> liens;
        private int[,] matriceAdjacence;

        public Graphe()
        {
            noeuds = new List<Noeud>();
            liens = new List<Lien>();
        }

        public void AjouterNoeud(Noeud noeud)
        {
            noeuds.Add(noeud);
            RecalculerMatriceAdjacence();
        }

        public void AjouterLien(Noeud villeA, Noeud villeB, int distance)
        {
            liens.Add(new Lien(villeA, villeB, distance));
            RecalculerMatriceAdjacence();
        }

        private void RecalculerMatriceAdjacence()
        {
            int n = noeuds.Count;
            matriceAdjacence = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriceAdjacence[i, j] = (i == j) ? 0 : int.MaxValue;
                }
            }

            foreach (Lien lien in liens)
            {
                int indexA = noeuds.IndexOf(lien.VilleA);
                int indexB = noeuds.IndexOf(lien.VilleB);
                if (indexA != -1 && indexB != -1)
                {
                    matriceAdjacence[indexA, indexB] = lien.Distance;
                    matriceAdjacence[indexB, indexA] = lien.Distance;
                }
            }
        }

        public List<Noeud> GetVoisins(Noeud noeud)
        {
            List<Noeud> voisins = new List<Noeud>();
            int index = noeuds.IndexOf(noeud);
            if (index == -1) return voisins;

            for (int j = 0; j < noeuds.Count; j++)
            {
                if (matriceAdjacence[index, j] != int.MaxValue && index != j)
                {
                    voisins.Add(noeuds[j]);
                }
            }
            return voisins;
        }

        public int GetDistance(Noeud a, Noeud b)
        {
            int i = noeuds.IndexOf(a);
            int j = noeuds.IndexOf(b);
            if (i != -1 && j != -1)
            {
                return matriceAdjacence[i, j];
            }
            return int.MaxValue;
        }

        public List<Noeud> Afficher_Noeuds()
        {
            return noeuds;
        }

        public Noeud Rechercher(Noeud start, string valeur)
        {
            if (start == null) return null;

            if (start.Nom == valeur)
                return start;

            foreach (Noeud voisin in GetVoisins(start))
            {
                Noeud res = Rechercher(voisin, valeur);
                if (res != null) return res;
            }

            return null;
        }

        public void ParcoursEnLargeur(Noeud start)
        {
            if (start == null) return;

            Queue<Noeud> file = new Queue<Noeud>();
            HashSet<Noeud> visites = new HashSet<Noeud>();

            file.Enqueue(start);
            visites.Add(start);

            while (file.Count > 0)
            {
                Noeud courant = file.Dequeue();
                Console.WriteLine(courant.Nom);

                foreach (Noeud voisin in GetVoisins(courant))
                {
                    if (!visites.Contains(voisin))
                    {
                        file.Enqueue(voisin);
                        visites.Add(voisin);
                    }
                }
            }
        }

        public bool EstConnexe()
        {
            if (noeuds.Count == 0) return true;

            HashSet<Noeud> visites = new HashSet<Noeud>();
            DFS(noeuds[0], visites);

            return visites.Count == noeuds.Count;
        }

        private void DFS(Noeud courant, HashSet<Noeud> visites)
        {
            visites.Add(courant);
            foreach (Noeud voisin in GetVoisins(courant))
            {
                if (!visites.Contains(voisin))
                {
                    DFS(voisin, visites);
                }
            }
        }

        public bool ContientCycle()
        {
            HashSet<Noeud> visites = new HashSet<Noeud>();
            foreach (Noeud noeud in noeuds)
            {
                if (!visites.Contains(noeud))
                {
                    if (DFS_Cycle(noeud, null, visites))
                        return true;
                }
            }
            return false;
        }

        private bool DFS_Cycle(Noeud courant, Noeud parent, HashSet<Noeud> visites)
        {
            visites.Add(courant);

            foreach (Noeud voisin in GetVoisins(courant))
            {
                if (!visites.Contains(voisin))
                {
                    if (DFS_Cycle(voisin, courant, visites))
                        return true;
                }
                else if (voisin != parent)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
