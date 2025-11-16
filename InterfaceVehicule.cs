using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class InterfaceVehicule
    {
        private static List<Vehicule> vehicules = new List<Vehicule>();
        private const string FICHIER_VEHICULES = "vehicules.json";
        public static List<Vehicule> VehiculesChargees => vehicules;
        public static void ChargerVehicules()
        {
            if (File.Exists(FICHIER_VEHICULES))
            {
                var json = File.ReadAllText(FICHIER_VEHICULES);
                vehicules = JsonConvert.DeserializeObject<List<Vehicule>>(json) ?? new List<Vehicule>();
            }
            else
            {
                vehicules = new List<Vehicule>();
            }
        }
        private static void SauvegarderVehicules()
        {
            File.WriteAllText(FICHIER_VEHICULES,JsonConvert.SerializeObject(vehicules, Formatting.Indented));
        }
        public void Demarrer()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("=== Gestion des véhicules ===");
                Console.WriteLine("1. Ajouter un véhicule");
                Console.WriteLine("2. Lister les véhicules");
                Console.WriteLine("3. Sauvegarder");
                Console.WriteLine("0. Retour");
                Console.Write("Choix : ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AjouterVehicule();
                        break;
                    case "2":
                        ListerVehicules();
                        break;
                    case "3":
                        SauvegarderVehicules();
                        Console.WriteLine("Véhicules sauvegardés.");
                        Console.ReadKey();
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AjouterVehicule()
        {
            Console.Write("Immatriculation : ");
            string immat = Console.ReadLine();

            Console.WriteLine("Type de véhicule :");
            Console.WriteLine("1) Voiture");
            Console.WriteLine("2) Camionnette");
            Console.WriteLine("3) CamionBenne");
            Console.WriteLine("4) CamionFrigorifique");
            Console.WriteLine("5) CamionCiterne");
            Console.Write("Choix : ");
            string type = Console.ReadLine();

            Vehicule v;
            switch (type)
            {
                case "1": 
                    v = new Voiture(); 
                    break;
                case "2": 
                    v = new Camionnette(); 
                    break;
                case "3": 
                    v = new CamionBenne(); 
                    break;
                case "4": 
                    v = new CamionFrigorifique(); 
                    break;
                case "5": 
                    v = new CamionCiterne(); 
                    break;
                default:
                    Console.WriteLine("Type invalide, on prend une Voiture par défaut.");
                    v = new Voiture();
                    break;
            }

            v.Immatriculation = immat;

            Console.Write("Date d'expiration du CT (jj/mm/aaaa) : ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dte))
            {
                v.DateCTExp = dte;
            }
            else
                Console.WriteLine("Format non reconnu, valeur par défaut gardée.");

            vehicules.Add(v);
            Console.WriteLine("Véhicule ajouté.");
            Console.ReadKey();
        }

        private void ListerVehicules()
        {
            if (vehicules.Count == 0)
            {
                Console.WriteLine("Aucun véhicule enregistré.");
            }
            else
            {
                Console.WriteLine("Immatriculation | Type                 | CT exp.");
                Console.WriteLine("------------------------------------------------");
                foreach (var v in vehicules)
                {
                    string typeName = v.GetType().Name.PadRight(20);
                    Console.WriteLine($"{v.Immatriculation.PadRight(14)} | {typeName} | {v.DateCTExp:dd/MM/yyyy}");
                }
            }
            Console.WriteLine("\nAppuyez sur une touche...");
            Console.ReadKey();
        }
    }
}
