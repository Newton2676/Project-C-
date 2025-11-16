using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_BON
{
    abstract internal class Personne
    {


        protected int num_SS;
        protected string nom;
        protected string prenom;
        protected DateTime date_naissance;
        protected string adresse;
        protected string mail;
        protected int telephone;

        public Personne(int num_SS, string nom, string prenom, DateTime date_naissance, string adresse, string mail, int telephone)
        {
            this.num_SS = num_SS;
            this.nom = nom;
            this.prenom = prenom;
            this.date_naissance = date_naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.telephone = telephone;
        }


        public int Num_SS
        {
            get { return num_SS; }
            set { num_SS = value; }
        }


        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public int Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }





    }
}
