using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Lien
    {
        public Noeud villeA;
        public Noeud villeB;
        public int distance;

        public Lien(Noeud villeA, Noeud villeB, int distance)
        {
            this.villeA = villeA;
            this.villeB = villeB;
            this.distance = distance;
        }

        public Noeud VilleA
        {
            get { return villeA; }
            set { villeA = value; }
        }

        public Noeud VilleB
        {
            get { return villeB; }
            set { villeB = value; }
        }

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }


    }
}
