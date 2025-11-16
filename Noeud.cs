using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Noeud
    {
        public string Nom { get; set; }
        public Salariés Salarié { get; set; }
        public List<Noeud> Enfants { get; set; }

        public Noeud(string nom)
        {
            Nom = nom;
            Enfants = new List<Noeud>();
        }
    }
}
