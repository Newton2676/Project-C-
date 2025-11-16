using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Projet_BON;

namespace Projet_BON
{
    class InterfaceCommande
    {
        private static List<Commande> commandes = new List<Commande>();
        private static List<Clients> clients = new List<Clients>();
        private static List<Salariés> chauffeurs = new List<Salariés>();
        private static Dictionary<string, Dictionary<string, int>> graphe = new Dictionary<string, Dictionary<string, int>>();
        private const string FICHIER_COMMANDES = "commandes.json";
        private const string FICHIER_CLIENTS = "clients.json";
        public static List<Commande> CommandesChargees => commandes;
        public static List<Salariés> ChauffeursCharges => chauffeurs;

        public static void Demarrer()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("=== MENU COMMANDE ===");
                Console.WriteLine("1. Créer une commande");
                Console.WriteLine("2. Voir le prix d'une commande");
                Console.WriteLine("3. Voir le plan de route");
                Console.WriteLine("4. Sauvegarder");
                Console.WriteLine("5. Afficher toutes les commandes.");
                Console.WriteLine("0. Quitter");
                Console.Write("Choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        CreerCommande();
                        break;
                    case "2":
                        AfficherPrixCommande();
                        break;
                    case "3":
                        AfficherPlanRoute();
                        break;
                    case "4":
                        Sauvegarder();
                        break;
                    case "5":
                        AfficherToutesLesCommandes();
                        Console.ReadKey();
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide !");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void AfficherToutesLesCommandes()
        {

            if (File.Exists("commandes.json"))
            {
                string json = File.ReadAllText("commandes.json");
                var commandes = JsonConvert.DeserializeObject<List<Commande>>(json) ?? new List<Commande>();


                if (commandes.Any())
                {
                    foreach (var commande in commandes)
                    {
                        Console.WriteLine($"Commande : {commande.Client.Nom} {commande.Client.Prenom}");
                        Console.WriteLine($"  Ville de départ : {commande.Ville_A}, Ville d'arrivée : {commande.Ville_B}");
                        Console.WriteLine($"  Véhicule : {commande.Vehicule}, Chauffeur : {commande.Chauffeur.Nom} {commande.Chauffeur.Prenom}");
                        Console.WriteLine($"  Date : {commande.Date.ToShortDateString()}");
                        Console.WriteLine($"  Prix : {commande.Prix}€");
                        Console.WriteLine($"  Statut de livraison : {(commande.EstLivree ? "Livrée" : "Non livrée")}");
                        Console.WriteLine("-----------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Aucune commande trouvée.");
                }
            }
            else
            {
                Console.WriteLine("Le fichier des commandes n'existe pas.");
            }
        }

        public static void ChargerDonnees()
        {
            if (File.Exists(FICHIER_CLIENTS))
            {
                var jsonClients = File.ReadAllText(FICHIER_CLIENTS);
                clients = JsonConvert.DeserializeObject<List<Clients>>(jsonClients) ?? new List<Clients>();
            }
            else
            {
                clients = new List<Clients>();
            }

            if (File.Exists(FICHIER_COMMANDES))
            {
                var jsonCommandes = File.ReadAllText(FICHIER_COMMANDES);
                commandes = JsonConvert.DeserializeObject<List<Commande>>(jsonCommandes) ?? new List<Commande>();
                foreach (var cmd in commandes)
                {
                    cmd.EstLivree = true;
                }
            }
            else
            {
                commandes = new List<Commande>();
            }
            var tousLesSalaries = InterfaceSalarie.GetSalaries() ?? new List<Salariés>();
            chauffeurs = tousLesSalaries.Where(s => s != null && !string.IsNullOrWhiteSpace(s.Poste) && s.Poste.Equals("chauffeur", StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var cmd in commandes)
            {
                cmd.EstLivree = true;

                if (cmd.Chauffeur != null)
                {
                    var central = chauffeurs.FirstOrDefault(ch => ch.Num_SS == cmd.Chauffeur.Num_SS);
                    if (central != null)
                    {
                        cmd.Chauffeur = central;
                        central.HistoriqueCommandes.Add(cmd);
                    }
                }
            }


            graphe.Clear();
            const string CSV = "distances_villes_france.csv";
            if (File.Exists(CSV))
            {
                foreach (var ligne in File.ReadLines(CSV).Skip(1))
                {
                    var champs = ligne.Split(',');
                    if (champs.Length < 3) continue;

                    string v1 = champs[0].Trim('"');
                    string v2 = champs[1].Trim('"');
                    if (!int.TryParse(champs[2].Trim('"'), out int dist))
                        continue;

                    if (!graphe.ContainsKey(v1))
                        graphe[v1] = new Dictionary<string, int>();
                    if (!graphe.ContainsKey(v2))
                        graphe[v2] = new Dictionary<string, int>();

                    graphe[v1][v2] = dist;
                    graphe[v2][v1] = dist;
                }
            }
            else
            {
                Console.WriteLine($"Attention : le fichier {CSV} est introuvable.");
            }
        }

        private static void CreerCommande()
        {
            chauffeurs = InterfaceSalarie.GetSalaries().Where(s => s.Poste.Equals("chauffeur", StringComparison.OrdinalIgnoreCase)).ToList();
            int newId = 1;
            if (commandes.Count > 0)
            {
                newId = commandes.Max(c => c.Id) + 1;
            }
            Console.Write("Nom du client : ");
            string nom = Console.ReadLine();
            Clients client = clients.FirstOrDefault(c => c.Nom == nom);
            if (client == null)
            {
                Console.WriteLine("Client non trouvé. Création du client.");
                client = InterfaceClient.CreerClient();
                clients.Add(client);
                Sauvegarder();
            }

            Console.Write("Ville de départ : ");
            string villeA = Console.ReadLine();
            Console.Write("Ville d'arrivée : ");
            string villeB = Console.ReadLine();
            Console.Write("Date de la commande (jj/mm/aaaa) : ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Date incorrecte.");
                return;
            }

            string vehicule = ChoisirVehicule();

            var uniqueChauffeurs = chauffeurs.DistinctBy(ch => ch.Num_SS).ToList();
            var dispo = uniqueChauffeurs.Where(ch => !commandes.Any(cmd => cmd.Chauffeur.Num_SS == ch.Num_SS && cmd.Date.Date == date.Date)).ToList();

            if (dispo.Count == 0)
            {
                Console.WriteLine("Aucun chauffeur disponible à cette date !");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Chauffeurs disponibles :");
            for (int i = 0; i < dispo.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dispo[i].Nom} {dispo[i].Prenom}");
            }
            Console.Write("Sélectionnez un chauffeur (numéro) : ");
            if (!int.TryParse(Console.ReadLine(), out int choix) || choix < 1 || choix > dispo.Count)
            {
                Console.WriteLine("Choix invalide.");
                Console.ReadKey();
                return;
            }
            Salariés chauffeurSelectionne = dispo[choix - 1];

            Commande commande = new Commande(client,villeA,villeB,0,vehicule,chauffeurSelectionne,date,newId);
            

            int distance = commande.CalculerKilometrage(villeA, villeB);
            int tarifDeBase;
            if (vehicule.Equals("Voiture", StringComparison.OrdinalIgnoreCase))
            {
                tarifDeBase = 100;
            }
            else if (vehicule.Equals("Camionnette", StringComparison.OrdinalIgnoreCase))
            {
                tarifDeBase = 150;
            }
               
            else
            {
                tarifDeBase = 200;
            }

            commande.Prix = tarifDeBase + distance;

            if (commande.AssignerCommande(commandes))
            {
                commandes.Add(commande);
                client.AjouterCommande(commande);
                commande.Chauffeur.HistoriqueCommandes.Add(commande);
                commande.EstLivree = true;
                Sauvegarder();  
                Console.WriteLine($"Commande créée avec un prix de {commande.Prix} € !");
            }
            else
            {
                Console.WriteLine("Impossible d’assigner ce chauffeur.");
            }

            Console.ReadKey();
        }



        private static string ChoisirVehicule()
        {
            Console.WriteLine("Choisissez un type de véhicule :");
            Console.WriteLine("1. Voiture");
            Console.WriteLine("2. Camionnette");
            Console.WriteLine("3. CamionBenne");
            Console.WriteLine("4. CamionFrigorifique");
            Console.WriteLine("5. CamionCiterne");
            Console.Write("Choix : ");
            return Console.ReadLine() switch
            {
                "1" => "Voiture",
                "2" => "Camionnette",
                "3" => "CamionBenne",
                "4" => "CamionFrigorifique",
                "5" => "CamionCiterne",
                "6" => "Voiture"
            };
        }

        private static void AfficherPrixCommande()
        {
            Console.Write("Numéro de Sécurité Sociale du chauffeur : ");
            int num = int.Parse(Console.ReadLine());
            var commande = commandes.FirstOrDefault(c => c.Chauffeur.Num_SS == num);
            if (commande != null)
            {
                Console.WriteLine($"Prix : {commande.CalculerTarif()}€");
            }
            else
            {
                Console.WriteLine("Commande introuvable.");
            }
            Console.ReadKey();
        }

        private static void AfficherPlanRoute()
        {
            Console.Write("Ville de départ : ");
            string villeA = Console.ReadLine();
            Console.Write("Ville d'arrivée : ");
            string villeB = Console.ReadLine();

            var chemin = Dijkstra(villeA, villeB);
            if (chemin.Count == 0)
            {
                Console.WriteLine("Aucun chemin trouvé !");
            }
            else
            {
                Console.WriteLine("Plan de route : " + string.Join(" -> ", chemin));
            }
            Console.ReadKey();
        }

        private static List<string> Dijkstra(string depart, string arrivee)
        {
            var distances = new Dictionary<string, int>();
            var precedent = new Dictionary<string, string>();
            var nonVisites = new HashSet<string>(graphe.Keys);

            foreach (var ville in graphe.Keys)
                distances[ville] = int.MaxValue;
            distances[depart] = 0;

            while (nonVisites.Count > 0)
            {
                var villeActuelle = nonVisites.OrderBy(v => distances[v]).First();
                nonVisites.Remove(villeActuelle);

                if (villeActuelle == arrivee)
                    break;

                foreach (var voisin in graphe[villeActuelle])
                {
                    int nouvelleDistance = distances[villeActuelle] + voisin.Value;
                    if (nouvelleDistance < distances[voisin.Key])
                    {
                        distances[voisin.Key] = nouvelleDistance;
                        precedent[voisin.Key] = villeActuelle;
                    }
                }
            }

            var chemin = new List<string>();
            string villeChemin = arrivee;
            while (precedent.ContainsKey(villeChemin))
            {
                chemin.Insert(0, villeChemin);
                villeChemin = precedent[villeChemin];
            }
            if (chemin.Count > 0)
                chemin.Insert(0, depart);
            return chemin;
        }
        private static void Sauvegarder()
        {
            var settings = new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
            File.WriteAllText(FICHIER_CLIENTS,JsonConvert.SerializeObject(clients, Formatting.Indented, settings));
            File.WriteAllText(FICHIER_COMMANDES,JsonConvert.SerializeObject(commandes, Formatting.Indented, settings));
            Console.WriteLine("Données sauvegardées !");
            Console.ReadKey();
        }





    }
}
