using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestionAbscenceEcole
{
    [Serializable]
    class Etudiant
    {
        public static List<Etudiant> liste_etudiants = new List<Etudiant>();
        private string nom;
        private string prenom;
        private string cne;
        private DateTime date_naissance;
        private string classe;
        private List<Absences> absences;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Cne { get => cne; set => cne = value; }
        public DateTime Date_naissance { get => date_naissance; set => date_naissance = value; }
        public string Classe { get => classe; set => classe = value; }
        internal List<Absences> Absences { get => absences; set => absences = value; }

        public Etudiant()
        {

        }
    }
}
