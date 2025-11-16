using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class InterfaceAlertes
    {
        private List<Salariés> chauffeurs;
        private List<Vehicule> vehicules;

        public InterfaceAlertes(List<Salariés> chauffeurs, List<Vehicule> vehicules) 
        { 
            this.chauffeurs = chauffeurs;
            this.vehicules = vehicules;
        }
        public static List<Vehicule> VehiculesChargees { get; private set; }

        public void Demarrer()
        {
            Console.WriteLine("*** Module d'Alertes Expirations ***");
            DateTime aujourdhui = DateTime.Today;
            DateTime seuil = aujourdhui.AddDays(30);

            Console.WriteLine("\n** Permis expirant dans 30 jours **");
            bool chaquePermis = false;
            foreach ( Salariés ch in chauffeurs )
            {
                if(ch.DatePermisExp <= seuil)
                {
                    Console.WriteLine("Chauffeur "+ ch.Nom + " (permis) : exp." +ch.DatePermisExp.ToString("dd/MM/yyyy"));
                    chaquePermis = true;
                }
            }
            if (!chaquePermis)
            {
                Console.WriteLine("Aucun permis ne va expirer dans les 30 prochains jours.");
            }

            Console.WriteLine("\n*** Contrôles techniques expirant dans 30 jours ***");
            bool chaqueCT = false;
            foreach (Vehicule v in vehicules)
            {
                if (v.DateCTExp <= seuil)
                {
                    Console.WriteLine("Vehicule " + v.Immatriculation + " (CT) : exp. " + v.DateCTExp.ToString("dd/MM/yyyy"));
                    chaqueCT = true;
                }
            }
            if (!chaqueCT)
            {
                Console.WriteLine("Aucun contrôle technique ne va expirer dans les 30 prochains jours.");
            }
            Console.WriteLine("Appuyez sur une touche pour revenir au menu");
            Console.ReadKey();
        }
    }
}
