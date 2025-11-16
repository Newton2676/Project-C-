using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Xml;
using Newtonsoft.Json;


namespace Projet_BON
{
    internal class InterfaceClient
    {
        private List<Clients> clientsList;
        private const string fichierClients = "clients.json";
        public InterfaceClient()
        {
            clientsList = new List<Clients>();
            ChargerClients();

        }
        private void ChargerClients()
        {
            if (File.Exists(fichierClients))
            {
                string json = File.ReadAllText(fichierClients);
                clientsList = JsonConvert.DeserializeObject<List<Clients>>(json);
                Console.WriteLine("Données clients rechargées !");
            }
        }


        private void SauvegarderClients()
        {
            string json = JsonConvert.SerializeObject(clientsList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(fichierClients, json);
        }
        public void Demarrer()
        {
            ChargerClients();
            string choix;

            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Client ---");
                Console.WriteLine("1. Ajouter un client");
                Console.WriteLine("2. Supprimer un client");
                Console.WriteLine("3. Modifier un client");
                Console.WriteLine("4. Afficher les clients par ordre alphabétique");
                Console.WriteLine("5. Afficher les meilleurs clients");
                Console.WriteLine("6. Sauvegarder les données clients");
                Console.WriteLine("0. Retour");
                Console.Write("Choix : ");
                choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.Write("Numéro de Sécurité Sociale : ");
                        int numSS = int.Parse(Console.ReadLine());
                        Console.Write("Nom : ");
                        string nom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        string prenom = Console.ReadLine();
                        Console.Write("Date de naissance (jj/mm/aaaa) : ");
                        DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
                        Console.Write("Adresse : ");
                        string adresse = Console.ReadLine();
                        Console.Write("Mail : ");
                        string mail = Console.ReadLine();
                        Console.Write("Téléphone : ");
                        int telephone = int.Parse(Console.ReadLine());

                        Clients nouveauClient = new Clients(numSS, nom, prenom, dateNaissance, adresse, mail, telephone, new List<Commande>());
                        clientsList.Add(nouveauClient);
                        SauvegarderClients();
                        Console.WriteLine("Client ajouté avec succès.");
                        break;

                    case "2":
                        Console.Write("Numéro de Sécurité Sociale du client à supprimer : ");
                        int ssSupprimer = int.Parse(Console.ReadLine());
                        var clientASupprimer = clientsList.FirstOrDefault(c => c.Num_SS == ssSupprimer);
                        if (clientASupprimer != null)
                        {
                            clientsList.Remove(clientASupprimer);
                            SauvegarderClients();
                            Console.WriteLine("Client supprimé.");
                        }
                        else Console.WriteLine("Client non trouvé.");
                        break;

                    case "3":
                        Console.Write("Numéro de Sécurité Sociale du client à modifier : ");
                        int ssModifier = int.Parse(Console.ReadLine());
                        var clientAModifier = clientsList.FirstOrDefault(c => c.Num_SS == ssModifier);
                        if (clientAModifier != null)
                        {
                            Console.Write("Nouvelle adresse : ");
                            string nouvelleAdresse = Console.ReadLine();
                            Console.Write("Nouveau mail : ");
                            string nouveauMail = Console.ReadLine();

                            clientAModifier.Adresse = nouvelleAdresse;
                            clientAModifier.Mail = nouveauMail;
                            SauvegarderClients();
                            Console.WriteLine("Informations client mises à jour.");
                        }
                        else Console.WriteLine("Client non trouvé.");
                        break;

                    case "4":
                        AfficherClientsParNom();
                        break;
                    case "5":
                        AfficherClientsParMontantAchats();
                        break;
                    case "6":
                        SauvegarderClients();
                        Console.WriteLine("Données clients sauvegardées.");
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }

            }
        }

        public void AjouterClient(Clients client)
        {

            clientsList.Add(client);
            Console.WriteLine("Client ajouté avec succès.");
        }



        public void SupprimerClient(int numSS)
        {
            var client = clientsList.FirstOrDefault(c => c.Num_SS == numSS);
            if (client != null)
            {
                clientsList.Remove(client);
                Console.WriteLine("Client supprimé.");
            }
            else
            {
                Console.WriteLine("Client non trouvé.");
            }
        }

        public void ModifierClient(int numSS, string nouvelleAdresse, string nouveauMail)
        {
            var client = clientsList.FirstOrDefault(c => c.Num_SS == numSS);
            if (client != null)
            {
                client.Adresse = nouvelleAdresse;
                client.Mail = nouveauMail;
                Console.WriteLine("Informations client mises à jour.");
            }
            else
            {
                Console.WriteLine("Client non trouvé.");
            }
        }
        public void AfficherClientsParNom()
        {
            var clientsTrieParNom = clientsList.OrderBy(c => c.Nom).ToList();
            foreach (var client in clientsTrieParNom)
            {
                Console.WriteLine($"{client.Nom} {client.Prenom}");
            }
        }

        public void AfficherClientsParMontantAchats()
        {
            var clientsTrieParAchats = clientsList.OrderByDescending(c => c.Commandes.Sum(cmd => cmd.Prix)).ToList();
            foreach (var client in clientsTrieParAchats)
            {
                double montantAchats = client.Commandes.Sum(cmd => cmd.Prix);
                Console.WriteLine($"{client.Nom} {client.Prenom}, Montant total des achats: {montantAchats} €");
            }
        }
        public static Clients CreerClient()
        {
            Console.Write("Numéro de sécurité sociale : ");
            int numSS = int.Parse(Console.ReadLine());

            Console.Write("Nom : ");
            string nom = Console.ReadLine();

            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();

            Console.Write("Date de naissance (jj/mm/aaaa) : ");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());

            Console.Write("Adresse : ");
            string adresse = Console.ReadLine();

            Console.Write("Email : ");
            string email = Console.ReadLine();

            Console.Write("Téléphone : ");
            int telephone = int.Parse(Console.ReadLine());

            return new Clients(numSS, nom, prenom, dateNaissance, adresse, email, telephone, new List<Commande>());
        }



    }
}
