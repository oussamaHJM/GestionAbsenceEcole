using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestionAbscenceEcole
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        DateTime dt;
        string sm;
        string smtf;

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panelAcceuil1.Visible = true;
            panelAjouter2.Visible = false;
            panelListeEtudiant3.Visible = false;
            panelSuprimer4.Visible = false;
            panelModifier5.Visible = false;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panelAcceuil1.Visible = true;
            panelAjouter2.Visible = true;
            panelListeEtudiant3.Visible = true;
            panelSuprimer4.Visible = false;
            panelModifier5.Visible = false;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            panelAcceuil1.Visible = true;
            panelAjouter2.Visible = true;
            panelListeEtudiant3.Visible = false;
            panelSuprimer4.Visible = false;
            panelModifier5.Visible = false;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            panelAcceuil1.Visible = true;
            panelAjouter2.Visible = true;
            panelListeEtudiant3.Visible = true;
            panelSuprimer4.Visible = true;
            panelModifier5.Visible = false;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            panelAcceuil1.Visible = true;
            panelAjouter2.Visible = true;
            panelListeEtudiant3.Visible = true;
            panelSuprimer4.Visible = true;
            panelModifier5.Visible = true;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void more1_Click(object sender, EventArgs e)
        {
            if (panel2.Width != 50)
            {
                more1Animator.Hide(more1);
                panel2.Visible = false;
                panel2.Width = 50;
                more2Animator.ShowSync(panel2);
            }
            more2.Visible = true;
        }

        private void more2_Click(object sender, EventArgs e)
        {
            if (panel2.Width == 50)
            {
                more2.Visible = false;
                panel2.Visible = true;
                panel2.Width = 245;
                more1Animator.ShowSync(panel2);
                more2Animator.ShowSync(more1);
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            Etudiant et = new Etudiant();
            et.Nom = bunifuMaterialTextbox1.Text;
            et.Prenom = bunifuMaterialTextbox2.Text;
            et.Cne = bunifuMaterialTextbox3.Text;
            et.Classe = bunifuMaterialTextbox4.Text;
            et.Date_naissance = bunifuDatepicker1.Value.Date;
            DialogResult dr = MessageBox.Show("voulez-vous vraiment aouter cet étudiant ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.ToString().Equals("Yes"))
                Etudiant.liste_etudiants.Add(et);
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox4.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("mesDonnees", FileMode.OpenOrCreate);
            if (fs.Length > 0) Etudiant.liste_etudiants = (List<Etudiant>)bf.Deserialize(fs);
            fs.Close();
            panel3.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("mesDonnees", FileMode.OpenOrCreate);
            bf.Serialize(fs, Etudiant.liste_etudiants);

            fs.Close();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                comboBox1.Items.Add(item.Nom);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuMaterialTextbox6.Text = Etudiant.liste_etudiants[comboBox1.SelectedIndex].Nom;
            bunifuMaterialTextbox7.Text = Etudiant.liste_etudiants[comboBox1.SelectedIndex].Prenom;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Absences b = new Absences();
            if (bunifuCheckbox2.Checked) b.Etat = "justifier";
            else b.Etat = "non justifier";
            b.Dt_absence = bunifuDatepicker2.Value.Date;
            b.Motif = bunifuMaterialTextbox5.Text;
            b.Matiere = bunifuMaterialTextbox8.Text;
            foreach (var item in Etudiant.liste_etudiants)
            {
                if (item.Nom.CompareTo(comboBox1.Text) == 0)
                {
                    item.Absences.Add(b);
                    MessageBox.Show("L'absence à été bien enregistrer dans la base");
                    bunifuMaterialTextbox5.Text = "";
                    bunifuMaterialTextbox8.Text = "";
                }
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                comboBox2.Items.Add(item.Nom);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                foreach (var h in item.Absences)
                {
                    if (item.Nom.CompareTo(comboBox2.Text) == 0)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = h.Dt_absence;
                        dataGridView1.Rows[n].Cells[1].Value = h.Etat;
                        dataGridView1.Rows[n].Cells[2].Value = h.Matiere;
                        dataGridView1.Rows[n].Cells[3].Value = h.Motif;
                    }
                }
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                comboBox3.Items.Add(item.Nom);
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("voulez-vous vraiùment supprimer " + comboBox3.Text + " de la base ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.ToString().Equals("Yes"))
            {
                Etudiant.liste_etudiants.RemoveAt(comboBox3.SelectedIndex);
                MessageBox.Show("l'étudiant à été suprimer avec succsé");
            }
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                comboBox4.Items.Add(item.Nom);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                foreach (var h in item.Absences)
                {
                    if (item.Nom.CompareTo(comboBox4.Text) == 0)
                    {
                        if ((h.Matiere != null) && (h.Motif != null))
                        {
                            int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = h.Dt_absence;
                            dataGridView2.Rows[n].Cells[1].Value = h.Etat;
                            dataGridView2.Rows[n].Cells[2].Value = h.Matiere;
                            dataGridView2.Rows[n].Cells[3].Value = h.Motif;
                        }
                        else if((h.Matiere == null) && (h.Motif == null))
                        {
                            h.Motif = "";
                            h.Matiere = "";
                        }
                        else if((h.Matiere == null))
                        {
                            h.Matiere = "";
                        }
                        else if ((h.Motif == null))
                        {
                            h.Motif = "";
                        }
                    }
                }
            }
            /*dataGridView2.Rows.Clear();
            foreach (var item in Etudiant.liste_etudiants)
            {
                foreach (var h in item.Absences)
                {
                    if (item.Nom.CompareTo(comboBox4.Text) == 0)
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = h.Dt_absence;
                        dataGridView2.Rows[n].Cells[1].Value = h.Etat;
                        dataGridView2.Rows[n].Cells[2].Value = h.Matiere;
                        dataGridView2.Rows[n].Cells[3].Value = h.Motif;
                    }
                }
            }*/
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("voulez-vous vraiment Modifier cet absence ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.ToString().Equals("Yes"))
            {
                foreach (var item in Etudiant.liste_etudiants)
                {
                    if (item.Nom.CompareTo(comboBox4.Text) == 0)
                    {
                        foreach (var itm in item.Absences)
                        {
                            if ((dt == itm.Dt_absence) && (smtf.CompareTo(itm.Motif) == 0) && (sm.CompareTo(itm.Matiere) == 0))
                            {
                                itm.Matiere = bunifuMaterialTextbox9.Text;
                                itm.Motif = bunifuMaterialTextbox10.Text;
                                itm.Dt_absence = bunifuDatepicker3.Value.Date;
                                if (bunifuCheckbox1.Checked)
                                {
                                    itm.Etat = "Justifier";
                                }
                                else
                                {
                                    itm.Etat = "Non justifier";
                                }

                                bunifuMaterialTextbox10.Text = "";
                                bunifuMaterialTextbox9.Text = "";
                            }
                        }
                    }
                }
                panel3.Visible = false;
            }
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox10.Text = "";
            bunifuMaterialTextbox9.Text = "";
            bunifuCheckbox1.Checked = false;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Selected)
                {
                    bunifuMaterialTextbox9.Text = dataGridView2.Rows[i].Cells[2].Value.ToString();
                    bunifuMaterialTextbox10.Text = dataGridView2.Rows[i].Cells[3].Value.ToString();
                    bunifuDatepicker3.Value = (DateTime) dataGridView2.Rows[i].Cells[0].Value;
                    if (dataGridView2.Rows[i].Cells[3].Value.ToString().CompareTo("justifier") == 0)
                    {
                        bunifuCheckbox1.Checked = true;
                    }
                }
            }
            dt = bunifuDatepicker3.Value.Date;
            sm = bunifuMaterialTextbox9.Text;
            smtf = bunifuMaterialTextbox10.Text;
            panel3.Visible = true;
            MessageBox.Show("Les données sont bien chargé");
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
