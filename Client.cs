using Projet_BON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Clients : Personne
    {

        private List<Commande> commandes;




        public Clients(int num_SS, string nom, string prenom, DateTime date_naissance, string adresse, string mail, int telephone, List<Commande> commandes) : base(num_SS, nom, prenom, date_naissance, adresse, mail, telephone)
        {
            this.commandes = commandes;

        }


        public void AjouterCommande(Commande commande)
        {
            commandes.Add(commande);
            Console.WriteLine($"Commande du client {Nom} ajoutée à l'historique.");
        }


        public void AppliquerRemise()
        {
            int totalLivraisons = commandes.Count;
            double remise = 0;

            if (totalLivraisons > 10)
            {
                remise = 0.1;
                Console.WriteLine("Remise de 10% appliquée pour fidélité.");
            }
            else if (totalLivraisons > 5)
            {
                remise = 0.05;
                Console.WriteLine("Remise de 5% appliquée pour fidélité.");
            }


            if (commandes.Count > 0)
            {
                Commande derniereCommande = commandes[commandes.Count - 1];
                int prixAvecRemise = (int)(derniereCommande.Prix * (1 - remise));
                Console.WriteLine($"Prix après remise: {prixAvecRemise}€");
            }
        }


        public List<Commande> Commandes
        {
            get { return commandes; }
        }

    }
}
