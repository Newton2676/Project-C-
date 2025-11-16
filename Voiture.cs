using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Voiture : Vehicule
    {
        private List<Clients> passagers;

        public Voiture(Salariés chauffeur, List<Clients> passagers) : base(chauffeur)
        {
            this.passagers = passagers;
        }
        public Voiture() : base()
        {
        }

        public void AddPassager(Clients p)
        {
            passagers.Add(p);
        }

        public void RemovePassager(Clients p)
        {
            passagers.Remove(p);
        }

        public List<Clients> Passagers
        {
            get { return passagers; }
            set { passagers = value; }
        }

    }
}
