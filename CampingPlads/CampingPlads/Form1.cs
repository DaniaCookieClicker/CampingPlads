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
            Database database = new Database();
        public Form1()
        {
            InitializeComponent();

            database.Connection();
            //database.CreateTable();
            //database.Regnskab();
            database.TjekPris();
            listBox2.Hide();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);

            //database.InsertCampingArea(Convert.ToInt32(textBox1.Text));
            listBox1.Hide();
            listBox2.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //and then stuff
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.Rejsende();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            database.SætPris(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
