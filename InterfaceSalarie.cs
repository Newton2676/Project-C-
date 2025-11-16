using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Projet_BON;

namespace Projet_BON
{
    internal class InterfaceSalarie
    {

        private static Organigramme organigramme;
        private static string cheminFichier = "organigramme.json";


        public static void Demarrer()
        {
            organigramme = Organigramme.Charger(cheminFichier);
            if (organigramme == null)
            {
                Console.WriteLine("Aucun organigramme trouvé. Création du salarié directeur :");
                var directeur = CreerSalarie();
                organigramme = new Organigramme(directeur);
                organigramme.Sauvegarder(cheminFichier);
                Console.WriteLine("Directeur embauché et sauvegardé.");
                Console.ReadKey();
            }


            bool continuer = true;
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine("=== MENU SALARIÉ ===");
                Console.WriteLine("1. Embaucher un salarié");
                Console.WriteLine("2. Licencier un salarié");
                Console.WriteLine("3. Ajouter dans l'organigramme");
                Console.WriteLine("4. Supprimer de l'organigramme");
                Console.WriteLine("5. Afficher l'organigramme");
                Console.WriteLine("6. Sauvegarder les données");
                Console.WriteLine("0. Retour au menu principal");
                Console.Write("Votre choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        EmbaucherSalarie();
                        break;
                    case "2":
                        LicencierSalarie();
                        break;
                    case "3":
                        AjouterDansOrganigramme();
                        break;
                    case "4":
                        SupprimerDeOrganigramme();
                        break;
                    case "5":
                        organigramme.Afficher();
                        Console.WriteLine("Appuyez sur une touche pour continuer.");
                        Console.ReadKey();
                        break;
                    case "6":
                        Sauvegarder();
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Appuyez sur une touche pour continuer.");
                        Console.ReadKey();
                        break;
                }
            }
        }


        private static Salariés CreerSalarie()
        {
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
            Console.Write("Email : ");
            string mail = Console.ReadLine();
            Console.Write("Téléphone : ");
            int telephone = int.Parse(Console.ReadLine());
            Console.Write("Date d'entrée dans l'entreprise (jj/mm/aaaa) : ");
            DateTime entreeEntreprise = DateTime.Parse(Console.ReadLine());
            Console.Write("Poste : ");
            string poste = Console.ReadLine();
            Console.Write("Salaire (€) : ");
            int salaire = int.Parse(Console.ReadLine());
            Console.Write("Date d'expiration du permis (jj/mm/aaaa) : ");
            DateTime datePermis = DateTime.Parse(Console.ReadLine());

            var salarie = new Salariés(numSS, nom, prenom, dateNaissance, adresse, mail, telephone, entreeEntreprise, poste, salaire);
            salarie.DatePermisExp = datePermis;
            return salarie;
        }
        private static void EmbaucherSalarie()
        {
            var salarie = CreerSalarie();

            if (organigramme.SalariésList.Any(s => s.Num_SS == salarie.Num_SS))
            {
                Console.WriteLine("Ce numéro de Sécurité Sociale existe déjà.");
            }
            else
            { 
                organigramme.SalariésList.Add(salarie);
                organigramme.Sauvegarder(cheminFichier);
                Console.WriteLine("Salarié embauché et sauvegardé avec succès.");
            }
            Console.ReadKey();
        }

        private static void LicencierSalarie()
        {
            Console.Write("Entrez le numéro de Sécurité Sociale du salarié à licencier : ");
            int numSS = int.Parse(Console.ReadLine());

            var salarie = organigramme.SalariésList.FirstOrDefault(s => s.Num_SS == numSS);
            if (salarie != null)
            {
                organigramme.SalariésList.Remove(salarie);
                organigramme.SupprimerSalarie(numSS);
                organigramme.Sauvegarder(cheminFichier);
                Console.WriteLine("Salarié licencié et retiré de l'organigramme.");
            }
            else
            {
                Console.WriteLine("Salarié introuvable.");
            }
            Console.ReadKey();
        }


        private static void AjouterDansOrganigramme()
        {
            Console.Write("Numéro de Sécurité Sociale du supérieur hiérarchique : ");
            int numSSSuperieur = int.Parse(Console.ReadLine());
            Console.Write("Numéro de Sécurité Sociale du salarié à ajouter : ");
            int numSS = int.Parse(Console.ReadLine());

            var subordonne = organigramme.SalariésList.FirstOrDefault(s => s.Num_SS == numSS);
            if (subordonne == null)
            {
                Console.WriteLine("Salarié introuvable.");
            }
            else if (organigramme.AjouterSubordonne(numSSSuperieur, subordonne))
            {
                organigramme.Sauvegarder(cheminFichier); 
                Console.WriteLine("Ajouté dans l'organigramme et sauvegardé !");
            }
            else
            {
                Console.WriteLine("Supérieur introuvable, ajout annulé.");
            }
            Console.ReadKey();
        }


        private static void SupprimerDeOrganigramme()
        {
            Console.Write("Numéro de SS du salarié à retirer de l'organigramme : ");
            int numSS = int.Parse(Console.ReadLine());

            if (organigramme.SupprimerSalarie(numSS))
            {
                Console.WriteLine("Retiré de l'organigramme.");
            }
            else
                Console.WriteLine("Salarié non trouvé dans l'organigramme.");
            Console.ReadKey();
        }


        private static void Sauvegarder()
        {
            organigramme.Sauvegarder(cheminFichier);
            Console.WriteLine("Données sauvegardées !");
        }
        public static List<Salariés> GetSalaries()
        {
            if (!File.Exists("organigramme.json"))
                return new List<Salariés>();

            var json = File.ReadAllText("organigramme.json");
            var orga = JsonConvert.DeserializeObject<Organigramme>(json);
            if (orga == null) return new List<Salariés>();
            return orga.SalariésList;
        }


        private static void Parcours(NoeudSalarie noeud, List<Salariés> liste)
        {
            if (noeud?.Salarie != null)
            {
                liste.Add(noeud.Salarie);
            }

            foreach (var enfant in noeud.Subordonnes)
            {
                Parcours(enfant, liste);
            }
        }
    }
}
