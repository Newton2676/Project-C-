using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class InterfaceNotation
    {
        private List<Commande> commandes;
        private List<Salariés> chauffeurs;
        private const string FICHIER_NOTATIONS = "notations.json";
        private List<ModuleNotation> notations;

        public InterfaceNotation(List<Commande> commandes,List<Salariés> chauffeurs)
        {
            this.commandes = commandes;
            this.chauffeurs = chauffeurs;
            ChargerNotations();
        }
        
        public void Demarrer()
        {
            string choix;
            do
            {
                Console.WriteLine("--- Module de notation des chauffeurs ---");
                Console.WriteLine("1. Noter un chauffeur");
                Console.WriteLine("2. Consulter note moyenne d'un chauffeur");
                Console.WriteLine("3. Lister tous les feedbacks");
                Console.WriteLine("0. Retour");
                Console.Write("Choix: ");
                choix = Console.ReadLine();

                switch (choix)
                {
                    case "1": 
                        NoterChauffeur(); 
                        break;
                    case "2": 
                        AfficherNoteMoyenne(); 
                        break;
                    case "3": 
                        ListerFeedbacks();
                        break;
                    case "0": 
                        Console.WriteLine("Retour au menu principal."); 
                        break;
                    default: 
                        Console.WriteLine("Option invalide."); 
                        break;
                }
            } 
            while (choix != "0");
        }
        
        private void NoterChauffeur()
        {
            var commandesLivrees = commandes.Where(com => com.EstLivree && (com.Notations == null || com.Notations.Count == 0)).ToList();
            
            if (commandesLivrees.Count==0)
            {
                Console.WriteLine("Aucune commande n'est éligible pour notation.");
                return;
            }

            for (int i = 0; i < commandesLivrees.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Commande #{commandesLivrees[i].Id} - Chauffeur: {commandesLivrees[i].Chauffeur.Nom}");
            }

            Console.Write("Sélectionnez la commande à noter (numéro): ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > commandesLivrees.Count)
            {
                Console.WriteLine("Choix invalide.");
                return;
            }
            var cmd = commandesLivrees[idx - 1];

            Console.WriteLine("Notez sur 1 à 5:");
            int p = LireNote("Ponctualité: ");
            int c = LireNote("Conduite: ");
            int co = LireNote("Courtoisie: ");
            Console.Write("Commentaire (facultatif): ");
            string commentaire = Console.ReadLine();

            var notation = new ModuleNotation
            {
                CommandeId = cmd.Id,
                ChauffeurSS = cmd.Chauffeur.Num_SS,
                DateNotation = DateTime.Now,
                Ponctualite = p,
                Courtoisie = co,
                Conduite = c,
                Commentaire = commentaire,
                Commande = cmd,
                Chauffeur = cmd.Chauffeur
            };
            notations.Add(notation);
            notation.Chauffeur.Notations.Add(notation);
            SauvegarderNotations();

            Console.WriteLine("Notation enregistrée. Merci pour votre retour.");
        }

        private int LireNote(string label)
        {
            Console.Write(label);
            if (int.TryParse(Console.ReadLine(), out int n) && n >= 1 && n <= 5)
            {
                return n;
            }
            Console.WriteLine("Veuillez entrer un nombre de 1 à 5.");
            return LireNote(label);
        }

        private void AfficherNoteMoyenne()
        {
            Console.Write("Entrez le nom du chauffeur: ");
            string nom = Console.ReadLine();
            Salariés chauffeurTrouve = null;
            foreach (var ch in chauffeurs)
            {
                if(ch.Nom.ToLower() == nom.ToLower())
                {
                    chauffeurTrouve = ch;
                    break;
                }
            }
            if (chauffeurTrouve == null)
            {
                Console.WriteLine("Chauffeur non trouvé.");
                return;
            }
            double moyenne = chauffeurTrouve.Moyenne();
            if(moyenne > 0)
            {
                Console.WriteLine($"Note moyenne de {chauffeurTrouve.Nom}: {moyenne:F2}/5");
            }
            else
            {
                Console.WriteLine("Aucune notation disponible pour ce chauffeur");
            }
        }

        private void ListerFeedbacks()
        {
            foreach (var ch in chauffeurs)
            {
                Console.WriteLine($"** Chauffeur: {ch.Nom} ** Moyenne: {ch.NoteMoyenneGlobale():F2}/5");
                foreach (var n in ch.Notations)
                {
                    Console.WriteLine($" {n.DateNotation:dd/MM/yyyy} Commandes n°{n.Commande.Id} , Moyenne: {n.Moyenne():F2} , {n.Commentaire}");
                }
                Console.WriteLine();
            }
        }

        public void ChargerNotations()
        {
            if (File.Exists(FICHIER_NOTATIONS))
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                notations = JsonConvert.DeserializeObject<List<ModuleNotation>>(File.ReadAllText(FICHIER_NOTATIONS), settings) ?? new List<ModuleNotation>();
                foreach (var n in notations)
                {
                    var cmd = commandes.FirstOrDefault(c => c.Id == n.CommandeId);
                    var ch = chauffeurs.FirstOrDefault(s => s.Num_SS == n.ChauffeurSS);
                    if (cmd != null)
                    {
                        n.Commande = cmd;
                        if (cmd.Notations == null) cmd.Notations = new List<ModuleNotation>();
                        cmd.Notations.Add(n);
                    }
                    if (ch != null)
                    {
                        n.Chauffeur = ch;
                        ch.Notations.Add(n);
                    }
                }
            }
            else
            {
                notations = new List<ModuleNotation>();
            }
        }

        private void SauvegarderNotations()
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(notations,Formatting.Indented,settings);
            File.WriteAllText(FICHIER_NOTATIONS, json);
        }
    }
}
