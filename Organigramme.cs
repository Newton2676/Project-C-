using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using Projet_BON;

namespace Projet_BON
{
    internal class Organigramme
    {

        public NoeudSalarie Racine { get; set; }
        public List<Salariés> SalariésList { get; set; }

        public Organigramme() {
            SalariésList = new List<Salariés>();
        }
        public Organigramme(Salariés directeur) : this()
        {
            Racine = new NoeudSalarie(directeur);
            SalariésList.Add(directeur);
        }

        public void Afficher()
        {
            AfficherRecursif(Racine, 0);
        }


        private void AfficherRecursif(NoeudSalarie noeud, int niveau)
        {
            if (noeud == null || noeud.Salarie == null)
            {
                Console.WriteLine(new string('-', niveau * 2) + "[Aucun salarié]");
                return;
            }
            Console.WriteLine(new string('-', niveau * 2) + noeud.Salarie.Nom + " " + noeud.Salarie.Prenom+" : "+noeud.Salarie.Poste);

            if (noeud.Subordonnes != null)
            {
                foreach (var subordonne in noeud.Subordonnes)
                {
                    AfficherRecursif(subordonne, niveau + 1);
                }
            }
        }


        public bool AjouterSubordonne(int numSSSuperieur, Salariés subordonne)
        {
            var noeudSuperieur = ChercherNoeud(Racine, numSSSuperieur);
            if (noeudSuperieur != null && subordonne != null)
            {
                noeudSuperieur.Subordonnes.Add(new NoeudSalarie(subordonne));
                SalariésList.Add(subordonne);
                return true;
            }
            return false;
        }


        public bool SupprimerSalarie(int numSS)
        {
            return SupprimerRecursif(Racine, numSS);
        }


        private bool SupprimerRecursif(NoeudSalarie parent, int numSS)
        {
            for (int i = 0; i < parent.Subordonnes.Count; i++)
            {
                if (parent.Subordonnes[i].Salarie.Num_SS == numSS)
                {
                    parent.Subordonnes.RemoveAt(i);
                    var salarie = SalariésList.FirstOrDefault(s => s.Num_SS == numSS);
                    if (salarie != null) SalariésList.Remove(salarie);
                    return true;
                }
                else if (SupprimerRecursif(parent.Subordonnes[i], numSS))
                {
                    return true;
                }
            }
            return false;
        }

        private NoeudSalarie ChercherNoeud(NoeudSalarie noeud, int numSS)
        {
            if (noeud.Salarie.Num_SS == numSS)
            {
                return noeud;
            }

            foreach (var sub in noeud.Subordonnes)
            {
                var resultat = ChercherNoeud(sub, numSS);
                if (resultat != null)
                {
                    return resultat;
                }
            }
            return null;
        }


        public void Sauvegarder(string fichier)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fichier, json);
            Console.WriteLine("Organigramme sauvegardé.");
        }

        public static Organigramme Charger(string fichier)
        {
            if (!File.Exists(fichier))
            {
                return null;
            }

            string json = File.ReadAllText(fichier);
            var organigramme = JsonConvert.DeserializeObject<Organigramme>(json);
            if (organigramme == null || organigramme.Racine == null)
            {
                return null;
            }
            if (organigramme.SalariésList == null)
            {
                organigramme.SalariésList = new List<Salariés>();
            }
            void Parcours(NoeudSalarie n)
            {
                if (n?.Salarie != null && !organigramme.SalariésList.Any(s => s.Num_SS == n.Salarie.Num_SS))
                {
                    organigramme.SalariésList.Add(n.Salarie);
                }
                foreach (var sub in n.Subordonnes)
                {
                    Parcours(sub);
                }
            }
            Parcours(organigramme.Racine);

            return organigramme;
        }




    }




}

