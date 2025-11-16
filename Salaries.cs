using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    internal class Salariés : Personne,IMoyenne
    {

        private DateTime entree_entreprise;
        private string poste;
        private int salaire;
        private List<Commande> historiqueCommandes;
        private List<ModuleNotation> notations;
        private DateTime datePermisExp;

        public Salariés(int num_SS, string nom, string prenom, DateTime date_naissance, string adresse, string mail, int telephone, DateTime entree_entreprise, string poste, int salaire) : base(num_SS, nom, prenom, date_naissance, adresse, mail, telephone)
        {

            this.entree_entreprise = entree_entreprise;
            this.poste = poste;
            this.salaire = salaire;
            this.historiqueCommandes = new List<Commande>();
            this.notations = new List<ModuleNotation>();
        }
        public double Moyenne()
        {
            return NoteMoyenneGlobale();
        }


        public void AjouterNotation(ModuleNotation notation)
        {
            if (notation.Chauffeur == this)
            {
                Notations.Add(notation);
            }
        }

        public double NoteMoyenneGlobale()
        {
            if (Notations == null || Notations.Count == 0)
                return 0.0;
            double totalPonct = Notations.Sum(n => n.Ponctualite);
            double totalCourt = Notations.Sum(n => n.Courtoisie);
            double totalCond = Notations.Sum(n => n.Conduite);
            return (totalPonct + totalCourt + totalCond) / (Notations.Count * 3.0);
        }

        public int CalculerPoints()
        {
            double noteMoyenne = NoteMoyenneGlobale();
            double totalNotation = noteMoyenne * 5;
            int pointsLivraisons = historiqueCommandes.Count * 10;
            int pointsNotation = Convert.ToInt32(totalNotation);
            return pointsLivraisons + pointsNotation;
        }

        public List<Commande> HistoriqueCommandes
        {
            get { return historiqueCommandes; }
        }

        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }

        public List<ModuleNotation> Notations
        {
            get { return notations; }
            set { notations = value; }
        }

        public int Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }

        public DateTime DatePermisExp 
        { 
            get { return datePermisExp; }
            set { datePermisExp = value; }
        }



    }
}
