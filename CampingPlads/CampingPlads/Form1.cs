using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CampingPlads
{
    public partial class Form1 : Form
    {
        Database database;
        public Form1()
        {
            InitializeComponent();

         database = new Database(this);
            
            if (database.Connection()!=true)
            {
              database.CreateTable();
              database.Regnskab();
            }
             
            database.TjekPris();
           
            button2.Hide();
            progressBar1.Maximum = 10;
            timer1.Tick += new EventHandler(timer1_Tick);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);

            database.InsertCampingArea(Convert.ToInt32(textBox1.Text));

        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void listbox1delegate(string info)
        {
            listBox1.Items.Add(info);
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.Rejsende();
            database.Indkomst();
            database.Opdaterbudget();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            database.SætPris(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }


        private void timer1_Tick(object sender, EventArgs e)
        {



            if (progressBar1.Value != 10)
            {
                progressBar1.Value++;
            }
            else
            {
                if (progressBar1.Value == 10)
                {
                    timer1.Stop();
                    progressBar1.Value = 0;
                    progressBar1.Hide();
                    MessageBox.Show(this, "dagen er slut, tryk på næste dag for at starte næste dag");
                    button2.Show();
                }




            }
        }
    }
}
