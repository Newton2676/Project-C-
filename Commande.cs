using Projet_BON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Commande
    {
        private Clients client;
        private string ville_A;
        private string ville_B;
        private int prix;
        private string vehicule;
        private Salariés chauffeur;
        private DateTime date;
        private bool estLivree;
        private int id;
        private List<ModuleNotation> notations ;

        public Commande(Clients client, string ville_A, string ville_B, int prix, string vehicule, Salariés chauffeur, DateTime date,int id)
        {
            this.client = client;
            this.ville_A = ville_A;
            this.ville_B = ville_B;
            this.prix = prix;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.date = date;
            this.estLivree = false;
            this.id = id;
            this.notations = new List<ModuleNotation>();
        }

        public bool ChauffeurLibre(DateTime date, List<Commande> historiqueCommandes)
        {

            foreach (var commande in historiqueCommandes)
            {
                if (commande.chauffeur == this.chauffeur && commande.date.Date == date.Date || chauffeur.Poste != "chauffeur")
                {
                    return false;
                }
            }
            return true;
        }


        public bool AssignerCommande(List<Commande> historiqueCommandes)
        {
            if (ChauffeurLibre(date, historiqueCommandes))
            {
                
                Console.WriteLine("Commande assignée au chauffeur.");
                return true;
            }
            else
            {
                Console.WriteLine("Le chauffeur est déjà occupé ce jour-là.");
                return false;
            }
        }

        public int CalculerTarif()
        {

            int distance = CalculerKilometrage(ville_A, ville_B);

            int tarifDeBase = 0;

            if (vehicule is Voiture)
            {
                tarifDeBase = 100;
            }

            else if (vehicule is Camionnette)
            {
                tarifDeBase = 150;
            }
            else if (vehicule is CamionBenne || vehicule is CamionCiterne || vehicule is CamionFrigorifique)
            {
                tarifDeBase = 200;
            }
            return tarifDeBase + distance ;
        }

        public int CalculerKilometrage(string villeA, string villeB)
        {
            villeA = villeA.Trim().ToLowerInvariant();
            villeB = villeB.Trim().ToLowerInvariant();

            var lignes = File.ReadAllLines("distances_villes_france.csv");
            for (int i = 1; i < lignes.Length; i++) 
            {
                var colonnes = lignes[i].Split(',');
                if (colonnes.Length < 3) continue;

                string v1 = colonnes[0].Trim('"').Trim().ToLowerInvariant();
                string v2 = colonnes[1].Trim('"').Trim().ToLowerInvariant();
                if ((v1 == villeA && v2 == villeB) || (v1 == villeB && v2 == villeA))
                {
                    if (int.TryParse(colonnes[2].Trim('"'), out int dist))
                        return dist;
                }
            }

            Console.WriteLine($"Distance {villeA} jusqu'à {villeB} non trouvée !");
            return 0; 
        }



        public Clients Client
        {
            get { return client; }
            set { client = value; }
        }

        public string Ville_A
        {
            get { return ville_A; }
            set { ville_A = value; }
        }

        public string Ville_B
        {
            get { return ville_B; }
            set { ville_B = value; }
        }

        public int Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public string Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }

        public Salariés Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public bool EstLivree
        {
            get { return estLivree; }
            set { estLivree = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<ModuleNotation> Notations
        {
            get { return notations; }
            set { notations = value; }
        }
    }
}
