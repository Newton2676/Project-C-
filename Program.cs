using System.ComponentModel.Design;

namespace Projet_BON
{       internal class Program
        {
            static void Main(string[] args)
            {
            InterfaceCommande.ChargerDonnees();
            InterfaceVehicule.ChargerVehicules();
            var notations = new InterfaceNotation(InterfaceCommande.CommandesChargees, InterfaceCommande.ChauffeursCharges);
            bool continuer = true;
          

                while (continuer)
                {
                    Console.Clear();
                    Console.WriteLine("=== MENU PRINCIPAL ===");
                    Console.WriteLine("1. Gestion des clients");
                    Console.WriteLine("2. Gestion des salaries");
                    Console.WriteLine("3. Gestion des commandes");
                    Console.WriteLine("4. Statistiques");
                    Console.WriteLine("5. Notations");
                    Console.WriteLine("6. Bonus");
                    Console.WriteLine("7. Gestion des vehicules");
                    Console.WriteLine("8. Alertes");
                    Console.WriteLine("0. Quitter");
                    Console.Write("Votre choix : ");
                    string choix = Console.ReadLine();
                
                switch (choix)
                    {
                        case "1":
                        
                        InterfaceClient interfaceClient = new InterfaceClient();
                            interfaceClient.Demarrer();
                            break;
                        case "2":
                            InterfaceSalarie.Demarrer();
                            break;
                    case "3":
                        InterfaceCommande.Demarrer();
                        break;
                    case "4":
                       Statistiques.Demarrer();
                        break;
                    case "5":
                        notations.Demarrer();
                        break;
                    case "6":
                        var chauffeursUniques = InterfaceCommande.ChauffeursCharges.GroupBy(c => c.Num_SS).Select(g => g.First()).ToList();
                        new InterfaceBonus(chauffeursUniques).Demarrer();
                        break;
                    case "7":
                        new InterfaceVehicule().Demarrer();
                        break;
                    case "8":
                        new InterfaceAlertes(InterfaceSalarie.GetSalaries(),InterfaceVehicule.VehiculesChargees).Demarrer();
                        break;
                    case "0":
                            continuer = false;
                            break;
                        default:
                            Console.WriteLine("Choix invalide. Appuyez sur une touche pour continuer...");
                            Console.ReadKey();
                            break;
                    }

                }


                Console.WriteLine("Hello, World!");

            }
        }
}