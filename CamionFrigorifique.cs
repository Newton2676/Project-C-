using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class CamionFrigorifique : Camion
    {
        private List<string> produitPerissable;
        private int nbrGrpElectrogene;

        public CamionFrigorifique(Salariés chauffeur, string typeMatière, List<string> produitPerissable, int nbrGrpElectrogene) : base(chauffeur, typeMatière)
        {
            this.produitPerissable = produitPerissable;
            this.nbrGrpElectrogene = nbrGrpElectrogene;
        }
        public CamionFrigorifique(): base() { }

        public List<string> ProduitPerissable
        {
            get { return produitPerissable; }
            set { produitPerissable = value; }
        }

        public int NbrGrpElectrogene
        {
            get { return nbrGrpElectrogene; }
            set { nbrGrpElectrogene = value; }
        }
    }
}
