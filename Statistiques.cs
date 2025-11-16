using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Projet_BON;

namespace Projet_BON
{
    class Statistiques
    {
        private static List<Commande> commandes = new();
        private static List<Clients> clients = new();

        private const string FICHIER_COMMANDES = "commandes.json";
        private const string FICHIER_CLIENTS = "clients.json";

        public static void Demarrer()
        {
            ChargerDonnees();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Module Statistiques ===");
                Console.WriteLine("1. Nombre de livraisons par chauffeur");
                Console.WriteLine("2. Commandes sur une période");
                Console.WriteLine("3. Moyenne des prix des commandes");
                Console.WriteLine("4. Moyenne des commandes par client");
                Console.WriteLine("5. Liste des commandes d’un client");
                Console.WriteLine("0. Quitter");
                Console.Write("Choix : ");

                switch (Console.ReadLine())
                {
                    case "1": 
                        AfficherLivraisonsParChauffeur(); 
                        break;
                    case "2": 
                        AfficherCommandesParPeriode(); 
                        break;
                    case "3": 
                        AfficherMoyennePrixCommandes(); 
                        break;
                    case "4": 
                        AfficherMoyenneCommandesParClient(); 
                        break;
                    case "5": 
                        AfficherCommandesClient(); 
                        break;
                    case "0": 
                        return;
                    default: 
                        Console.WriteLine("Choix invalide."); 
                        break;
                }

                Console.WriteLine("Appuyez sur une touche pour continuer.");
                Console.ReadKey();
            }
        }

        private static void ChargerDonnees()
        {
            if (File.Exists(FICHIER_COMMANDES))
            {
                commandes = JsonConvert.DeserializeObject<List<Commande>>(File.ReadAllText(FICHIER_COMMANDES)) ?? new();
            }
            if (File.Exists(FICHIER_CLIENTS))
            {
                clients = JsonConvert.DeserializeObject<List<Clients>>(File.ReadAllText(FICHIER_CLIENTS)) ?? new();
            }
        }

        private static void AfficherLivraisonsParChauffeur()
        {
            var livraisons = commandes.Where(c => c.EstLivree).GroupBy(c => c.Chauffeur.Nom).Select(g => new { Chauffeur = g.Key, Nb = g.Count() });
            foreach (var l in livraisons)
            {
                Console.WriteLine($"{l.Chauffeur} : {l.Nb} livraisons");
            }
        }

        private static void AfficherCommandesParPeriode()
        {
            Console.Write("Date début (jj/mm/aaaa) : ");
            DateTime debut = DateTime.Parse(Console.ReadLine());
            Console.Write("Date fin (jj/mm/aaaa) : ");
            DateTime fin = DateTime.Parse(Console.ReadLine());

            var resultats = commandes.Where(c => c.Date >= debut && c.Date <= fin);
            foreach (var c in resultats)
            {
                Console.WriteLine($"{c.Date.ToShortDateString()} : {c.Client.Nom} -> {c.Ville_A} vers {c.Ville_B}, {c.Prix} €");
            }
        }

        private static void AfficherMoyennePrixCommandes()
        {
            if (commandes.Count == 0)
            {
                Console.WriteLine("Aucune commande.");
                return;
            }

            double moyenne = commandes.Average(c => c.Prix);
            Console.WriteLine($"Moyenne des prix : {moyenne:F2} €");
        }

        private static void AfficherMoyenneCommandesParClient()
        {
            if (clients.Count == 0)
            {
                Console.WriteLine("Aucun client.");
                return;
            }

            double moyenne = clients.Average(c => c.Commandes.Count);
            Console.WriteLine($"Nombre moyen de commandes par client : {moyenne:F2}");
        }

        private static void AfficherCommandesClient()
        {
            Console.Write("Nom du client : ");
            string nom = Console.ReadLine();

            var client = clients.FirstOrDefault(c => c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase));

            if (client == null)
            {
                Console.WriteLine("Client non trouvé.");
                return;
            }

            if (client.Commandes.Count == 0)
            {
                Console.WriteLine("Ce client n’a passé aucune commande.");
                return;
            }

            foreach (var c in client.Commandes)
                Console.WriteLine($"{c.Date.ToShortDateString()} : {c.Ville_A} -> {c.Ville_B}, {c.Prix} €");
        }


    }
}

