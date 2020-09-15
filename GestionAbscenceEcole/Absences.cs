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
    class Absences
    {
        private DateTime dt_absence;
        string etat;
        private string motif;
        private string matiere;

        public Absences()
        {

        }

        public DateTime Dt_absence { get => dt_absence; set => dt_absence = value; }
        public string Etat { get => etat; set => etat = value; }
        public string Motif { get => motif; set => motif = value; }
        public string Matiere { get => matiere; set => matiere = value; }
    }
}
