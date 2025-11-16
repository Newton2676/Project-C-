using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal abstract class Vehicule
    {
        private Salariés chauffeur;
        private DateTime dateCTExp;
        private string immatriculation;

        public Vehicule(Salariés chauffeur)
        {
            this.chauffeur = chauffeur;
        }
        public Vehicule() { }

        public Salariés Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public DateTime DateCTExp
        {
            get { return dateCTExp; }
            set { dateCTExp = value; }
        }

        public string Immatriculation
        {
            get { return immatriculation; }
            set { immatriculation = value; }
        }
    }
}
