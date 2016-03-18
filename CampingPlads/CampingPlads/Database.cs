using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace CampingPlads
{
    class Database
    {

        private static Form1 myForm;
        static String connStr = "Data Source=CampingPlads.db;Version=3";
        SQLiteConnection conn = new SQLiteConnection(connStr);
        private int lokalTeltPris;
        private int lokalCampingvognPris;
        public int antalCampingvognsBeboere;
        public int antalTeltBeboere;
        private int totalIndkomst;
        public int teltPladser;
        public int campingvognsPladser;

        public Database(Form1 newform)
        {
            myForm = newform;
        }

        public bool Connection()
        {
            string curFile = @"CampingPlads.db";
            if (!File.Exists(curFile))
            {

                SQLiteConnection.CreateFile("CampingPlads.db");
                DelegateConsoleinfo("database fil ikke fundet laver ny");
                conn.Open();
                return false;
            }

            conn.Open();

            return true;

        }

        public void CreateTable()
        {

            String sql = "create table Budget (indkomst integer, udgift integer,overskud integer,teltPris integer,campingvognPris integer);";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "create table Rejsende (id integer primary key, nationalitet string , penge integer, campingvogn boolean,tolerance integer, plads integer, foreign key (plads) references Plads(id));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "create table Plads (id integer primary key, campingvogn boolean, optaget boolean);";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            DelegateConsoleinfo("tabeller blev lavet");
        }

        public void InsertCampingArea(int campingpladser)
        {
            //works!
            int campingvognspladser = campingpladser * 2;
            int antal = 200;

            antal -= campingvognspladser;
            teltPladser = antal;
            int rest = (200 - antal) / 2;
            campingvognsPladser = rest;
            for (int i = 0; i < antal; i++)
            {
                String sql = "insert into Plads values(null,0,0);";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();

            }
            for (int i = 0; i < rest; i++)
            {
                String sql = "insert into Plads values(null,1,0);";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();

            }
            DelegateConsoleinfo("vi har delt de 200 pladser ud og du har nu " + rest + " campingvognspladser" + "og" + antal + "");
        }


        public void Regnskab()
        {
            String sql = "insert into Budget values(0,0,0,0,0);";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            DelegateConsoleinfo("budget  værdier sat ind");
        }
        public void Rejsende()
        {
            //(id integer primary key, nationalitet string , penge integer, campingvogn boolean, tolerance integer)
            string[] nationalitet = new string[] { "Dansker", "Svensker", "Nordmand", "Tysker", "Finner" };
            Random rd = new Random();
            for (int j = 0; j < 20; j++)
            {
                int i = rd.Next(0, 5);
                int penge = rd.Next(500, 3000);
                int campingvogn = rd.Next(0, 2);
                int tolerence = rd.Next(500, 1000);
                if (campingvogn == 0)
                {
                    if (tolerence >= lokalTeltPris)
                    {
                        String sql = "select id,optaget from Plads where optaget = 0 and campingvogn = 0";
                        SQLiteCommand command = new SQLiteCommand(sql, conn);
                        SQLiteDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int optaget = Convert.ToInt32(reader["optaget"]);
                            int id = Convert.ToInt32(reader["id"]);
                            if (optaget == 0)
                            {
                                sql = "insert into Rejsende values(null, " + "'" + nationalitet[i] + "'" + " ," + penge + "," + campingvogn + "," + tolerence + ", " + id + ");";
                                command = new SQLiteCommand(sql, conn);
                                command.ExecuteNonQuery();

                                sql = "update Plads set optaget = 1 where id = " + id + ";";
                                command = new SQLiteCommand(sql, conn);
                                command.ExecuteNonQuery();
                                break;
                            }
                        }
                    }
                }
                else if (campingvogn == 1)
                {
                    if (tolerence >= lokalCampingvognPris)
                    {
                        String sql = "select id,optaget, campingvogn from Plads where optaget = 0 and campingvogn = 1";
                        SQLiteCommand command = new SQLiteCommand(sql, conn);
                        SQLiteDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int optaget = Convert.ToInt32(reader["optaget"]);
                            int id = Convert.ToInt32(reader["id"]);
                            if (optaget == 0)
                            {
                                sql = "insert into Rejsende values(null, " + "'" + nationalitet[i] + "'" + " ," + penge + "," + campingvogn + "," + tolerence + ", " + id + ");";
                                command = new SQLiteCommand(sql, conn);
                                command.ExecuteNonQuery();

                                sql = "update Plads set optaget = 1 where id = " + id + ";";
                                command = new SQLiteCommand(sql, conn);
                                command.ExecuteNonQuery();
                                break;
                            }
                        }
                    }
                }
            }

        }
        public void TjekPris()
        {
            String sql = "select teltPris,campingvognPris from Budget";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                lokalTeltPris = Convert.ToInt32(reader["teltPris"]);
                lokalCampingvognPris = Convert.ToInt32(reader["campingvognpris"]);


            }

        }

        public void rejsendeForladerCampingplads()
        {
            String sql = "select rejsende.id,penge,rejsende.campingvogn,plads  from rejsende left join plads where plads.id =rejsende.plads ;";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                int campingvogn = Convert.ToInt32(reader["campingvogn"]);
                int penge = Convert.ToInt32(reader["penge"]);
                int plads = Convert.ToInt32(reader["plads"]);
                if (campingvogn == 0)
                {
                    if (penge<=lokalTeltPris)
                    {
                        sql = "update Plads set optaget = 0 where id = " + plads + ";";
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                        sql = "delete from rejsende where id="+id+"";
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                    else if(penge>lokalTeltPris)
                    {
                        sql = "update rejsende set penge=" + (penge - lokalTeltPris) + " where id = " + id + ";";
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                }
                else if (campingvogn == 1)
                {
                    if (penge <= lokalCampingvognPris)
                    {
                        sql = "update Plads set optaget = 0 where id = " + plads + ";";
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                        sql = "delete from rejsende where id=" + id + "";
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                    else if (penge > lokalCampingvognPris)
                    {
                        sql = "update rejsende set penge=" + (penge - lokalCampingvognPris) + " where id = " + id + ";";
                        command = new SQLiteCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                }
               
            }
        }
        public void Indkomst()
        {

            String sql = "select count(campingvogn) as campingvogne from rejsende  where campingvogn =1";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                antalCampingvognsBeboere = Convert.ToInt32(reader["campingvogne"]);
            }
            sql = "select count(campingvogn) as telte from rejsende  where campingvogn =0";
            command = new SQLiteCommand(sql, conn);
            reader = command.ExecuteReader();
            while (reader.Read())
            {

                antalTeltBeboere = Convert.ToInt32(reader["telte"]);

            }
            totalIndkomst = antalTeltBeboere * lokalTeltPris;
            totalIndkomst += antalCampingvognsBeboere * lokalCampingvognPris;



        }
        public void Opdaterbudget()
        {
            string sql = "select overskud from budget";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            int basisoverskud = 0;
            while (reader.Read())
            {

                basisoverskud = Convert.ToInt32(reader["overskud"]);

            }
            int totaludgift = campingvognsPladser * 375 + teltPladser * 150 + 2000;
            int overskud = basisoverskud + totalIndkomst - totaludgift;
            sql = "update  Budget set indkomst= " + totalIndkomst + ",udgift=" + totaludgift + ",overskud=" + overskud + ";";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            DelegateConsoleinfo("indkomst: " + totalIndkomst + " udgift: " + totaludgift + " overskud: " + overskud + "");

        }
        public void SætPris(int teltPris, int campingvognPris)
        {
            String sql = "update Budget set teltpris = " + teltPris + ", campingvognPris = " + campingvognPris + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            TjekPris();
            DelegateConsoleinfo("Telt pris er: " + lokalTeltPris + " Campingvognspris er: " + lokalCampingvognPris + "");
        }
        public void DelegateConsoleinfo(string info)
        {
            myForm.listbox1delegate(info);

        }
        public void dropTables()
        {
            String sql = "drop table rejsende;";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "drop table budget;";
             command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "drop table plads;";
          command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
    }
}
