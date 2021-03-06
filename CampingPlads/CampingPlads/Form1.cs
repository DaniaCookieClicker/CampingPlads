﻿using System;
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
        int dayCounter = 0;
        public Form1()
        {
            InitializeComponent();

         database = new Database(this);
            
            if (database.Connection()!=true)
            {
              database.CreateTable();
              database.BudgetManagement();
            }
           else 
            {
                database.DropTables();
                database.CreateTable();
                database.BudgetManagement();
            }
            int antalbeboere = database.antalCampingvognsBeboere;
            int ledigepladser = database.campingvognsPladser - antalbeboere;
            label1.Text = "ledige vognpladser: " + ledigepladser + " ud af " + database.campingvognsPladser;
            antalbeboere = database.antalTeltBeboere;
            ledigepladser = database.teltPladser - antalbeboere;
            label2.Text = "ledige teltpladser: " + ledigepladser + " ud af " + database.teltPladser;
            label3.Text = "dag: " + dayCounter + "";
            dayCounter++;
            HideStuff();

            
            //progressBar1.Maximum = 10;
            //timer1.Tick += new EventHandler(timer1_Tick);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add(textBox1.Text);

            database.InsertCampingArea(Convert.ToInt32(textBox1.Text));
            int antalbeboere = database.antalCampingvognsBeboere;
            int ledigepladser = database.campingvognsPladser - antalbeboere;
            label1.Text = "ledige vognpladser: " + ledigepladser + " ud af " + database.campingvognsPladser;
            antalbeboere = database.antalTeltBeboere;
            ledigepladser = database.teltPladser - antalbeboere;
            label2.Text = "ledige teltpladser: " + ledigepladser + " ud af " + database.teltPladser;
            ShowStuff();

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
            database.TravelersLeaving();
            database.Travelers();
            database.Payment();
            database.UpdateBudget();
            int antalbeboere = database.antalCampingvognsBeboere;
            int ledigepladser = database.campingvognsPladser - antalbeboere;
            label1.Text = "ledige vognpladser: " + ledigepladser + " ud af " + database.campingvognsPladser;
            antalbeboere = database.antalTeltBeboere;
            ledigepladser = database.teltPladser - antalbeboere;
            label2.Text = "ledige teltpladser: " + ledigepladser + " ud af " + database.teltPladser;
            label3.Text = "dag: " + dayCounter + "";
            dayCounter++;
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            database.SetPrice(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {



            //if (progressBar1.Value != 10)
            //{
            //    progressBar1.Value++;
            //}
            //else
            //{
            //    if (progressBar1.Value == 10)
            //    {
            //        timer1.Stop();
            //        progressBar1.Value = 0;
            //        progressBar1.Hide();
            //        MessageBox.Show(this, "dagen er slut, tryk på næste dag for at starte næste dag");
            //        button2.Show();
            //    }




            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void HideStuff()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            button2.Hide();
            button3.Hide();
            listBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
           
        }

        private void ShowStuff()
        {
            textBox1.Hide();
            button1.Hide();
            label4.Hide();

            label1.Show();
            label2.Show();
            label3.Show();
            button2.Show();
            button3.Show();
            listBox1.Show();
            textBox2.Show();
            textBox3.Show();
           
        }
    }
}
