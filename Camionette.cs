using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Camionnette : Vehicule
    {
        private string usage;

        public Camionnette(Salariés chauffeur, string usage) : base(chauffeur)
        {
            this.usage = usage;
        }
        public Camionnette() : base() { }

        public string Usage
        {
            get { return usage; }
            set { usage = value; }
        }

    }
}
