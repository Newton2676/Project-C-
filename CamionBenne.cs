using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class CamionBenne : Camion
    {
        private int nbrBennes;
        private bool grueAuxiliaire;

        public CamionBenne(Salariés chauffeur, string typeMatière, int nbrBennes, bool grueAuxiliaire) : base(chauffeur, typeMatière)
        {
            this.nbrBennes = nbrBennes;
            this.grueAuxiliaire = grueAuxiliaire;
        }
        public CamionBenne() : base() { }

        public int NbrBennes
        {
            get { return nbrBennes; }
            set { nbrBennes = value; }
        }
    }
}
