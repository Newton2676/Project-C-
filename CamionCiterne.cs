using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class CamionCiterne : Camion
    {
        private int tailleCuve;
        private string typeLiquideGaz;

        public CamionCiterne(Salariés chauffeur, string typeMatière, int tailleCuve, string typeLiquideGaz) : base(chauffeur, typeMatière)
        {
            this.tailleCuve = tailleCuve;
            this.typeLiquideGaz = typeLiquideGaz;
        }
        public CamionCiterne():base() { }

        public int TailleCuve
        {
            get { return this.tailleCuve; }
            set { this.tailleCuve = value; }
        }

        public string TypeLiquideGaz
        {
            get { return this.typeLiquideGaz; }
            set { this.typeLiquideGaz = value; }
        }

    }
}
