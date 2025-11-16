using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projet_BON
{
    internal class ModuleNotation : IMoyenne //le nom devait etre Notation erreur lors de la creation
    {
        private Commande commande;
        private Salariés chauffeur;
        private DateTime dateNotation;
        private int ponctualite;
        private int courtoisie;
        private int conduite;
        private string commentaire;
        private int commandeId;
        private int chauffeurSS;

        public ModuleNotation() { }
        public ModuleNotation(Commande commande,Salariés chauffeur,int ponctualite,int courtoisie,int conduite,string commentaire) 
        {
            this.commandeId = commande.Id;
            this.chauffeurSS = chauffeur.Num_SS;
            this.commande = commande;
            this.chauffeur = chauffeur;
            this.dateNotation = DateTime.Now;
            this.ponctualite = Math.Clamp(ponctualite,1,5);
            this.courtoisie= Math.Clamp(courtoisie,1,5);
            this.conduite = Math.Clamp(conduite,1,5);
            this.commentaire = commentaire;
        }


        public double Moyenne()
        {
            return (ponctualite + courtoisie + conduite) / 3.0;
        }

        [JsonIgnore]
        public Commande Commande 
        { 
            get { return commande; } 
            set { commande = value; }
        }
        [JsonIgnore]
        public Salariés Chauffeur 
        { 
            get { return chauffeur; }
            set { chauffeur = value; }
        }
        public DateTime DateNotation 
        { 
            get {  return dateNotation; } 
            set { dateNotation = value; }
        }
        public int Ponctualite 
        { 
            get {  return ponctualite; }
            set { ponctualite = value; }
        }
        public int Courtoisie
        {  
            get { return courtoisie; }
            set { courtoisie = value; }
        }
        public int Conduite 
        {
            get { return conduite; }
            set { conduite = value; }
        }
        public string Commentaire 
        { 
            get {  return commentaire; } 
            set { commentaire = value; }
        }
        public int CommandeId
        {
            get { return commandeId; }
            set { commandeId = value; } 
        }

        public int ChauffeurSS
        {
            get { return chauffeurSS; }
            set { chauffeurSS = value;}
        }
    }
}
