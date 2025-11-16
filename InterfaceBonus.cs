using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class InterfaceBonus
    {
        private List<Salariés> chauffeurs;

        public InterfaceBonus(List<Salariés> chauffeurs)
        {
            this.chauffeurs = chauffeurs;
        }

        public void Demarrer()
        {
            Console.WriteLine("*** Classement des chauffeurs ***");

            List<Salariés> classement = new List<Salariés>(chauffeurs);

            for (int i = 0; i < classement.Count; i++)
            {
                for (int j = 0; j < classement.Count; j++)
                {
                    if (classement[j].CalculerPoints() < classement[i].CalculerPoints())
                    {
                        Salariés temp = classement[i];
                        classement[i] = classement[j];
                        classement[j] = temp;
                    }
                }
            }
            if (classement.Count == 0)
            {
                Console.WriteLine("Aucun chauffeur trouvé.");
                Console.WriteLine("Appuyez sur une touche pour revenir au menu");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Rang| Nom   | Livraisons | Note Moy. | Points");
            Console.WriteLine("--------------------------------------------");
            for (int i = 0;i < classement.Count;i++)
            {
                Salariés ch = classement[i];
                string nom = ch.Nom;
                nom = nom.PadRight(20);
                int liv = ch.HistoriqueCommandes.Count;
                double note = ch.NoteMoyenneGlobale();
                int points = ch.CalculerPoints();
                Console.WriteLine($"{i + 1,4} | {nom} | {liv,10} | {note,8:F2} | {points,6}");
            }
            Console.WriteLine("--------------------------------------------") ;
            Console.WriteLine("Appuyez sur une touche pour revenir au menu");
            Console.ReadKey() ;
        }
    }
}
