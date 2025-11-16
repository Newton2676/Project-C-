using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal abstract class Camion : Vehicule
    {
        private string typeMatière;

        public Camion(Salariés chauffeur, string typeMatière) : base(chauffeur)
        {
            this.typeMatière = typeMatière;
        }
        public Camion() : base() { }

        public string TypeMatière
        {
            get { return this.typeMatière; }
            set { typeMatière = value; }
        }

    }
}
