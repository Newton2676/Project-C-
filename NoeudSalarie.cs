using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class NoeudSalarie
    {

        public Salariés Salarie { get; set; }
        public List<NoeudSalarie> Subordonnes { get; set; }

        public NoeudSalarie(Salariés salarie)
        {
            Salarie = salarie;
            Subordonnes = new List<NoeudSalarie>();
        }
    }
}
