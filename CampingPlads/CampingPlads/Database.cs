using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace CampingPlads
{
    class Database
    {
        static String connStr = "Data Source=CampingPlads.db;Version=3";
        SQLiteConnection conn = new SQLiteConnection(connStr);
        private int lokalTeltPris;
        private int lokalCampingvognPris;

        public void Connection()
        {
            string curFile = @"CampingPlads.db";
            if (!File.Exists(curFile))
            {

            SQLiteConnection.CreateFile("CampingPlads.db");
            }


            conn.Open();


        }

        public void CreateTable()
        {

            String sql = "create table Budget (indkomst integer, udgift integer,overskud integer,teltPris integer,campingvognPris integer);";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "create table Rejsende (id integer primary key, nationalitet string , penge integer, campingvogn boolean,tolerance integer);";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            sql = "create table Plads (id integer primary key, campingvogn boolean,rejsende integer, foreign key (rejsende) references Rejsende(id));";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void InsertCampingArea(int campingpladser)
        {
            //works!
            int campingvognspladser = campingpladser * 2;
            int antal = 200;

            antal -= campingvognspladser;
            int rest = (200 - antal) / 2;
            for (int i = 0; i < antal; i++)
            {
                String sql = "insert into Plads values(null,0,null);";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();

            }
            for (int i = 0; i < rest; i++)
            {
                String sql = "insert into Plads values(null,1,null);";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();

            }
        }


        public void Regnskab()
        {
            String sql = "insert into Budget values(0,0,0,0,0);";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
        public void Rejsende()
        {
            //(id integer primary key, nationalitet string , penge integer, campingvogn boolean, tolerance integer)
            string[] nationalitet = new string[] { "Dansker", "Svensker", "Nordmand", "Tysker", "Finner" };
            Random rd = new Random();
            int i = rd.Next(0, 6);
            int penge = rd.Next(500, 3000);
            int campingvogn = rd.Next(0, 2);
            int tolerence = rd.Next(500, 1000);

            for (int j = 0; j < 20; j++)
            {
                String sql = "insert into Rejsende values(null, " + nationalitet[i] + "," + penge + "," + campingvogn + "," + tolerence + ");";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
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
               lokalCampingvognPris= Convert.ToInt32(reader["campingvognpris"]);


            }

        }
        public void SætPris(int teltPris, int campingvognPris)
        {
            String sql = "update Budget set teltpris = " + teltPris + ", campingvognPris = " + campingvognPris + ";";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

        }
        //public void insert()
        //{

        //    conn.Open();
        //    String sql = "insert into ny values(null,'Kalasnikov');";
        //    SQLiteCommand command = new SQLiteCommand(sql, conn);
        //    command.ExecuteNonQuery();
        //}
        //public string select()
        //{

        //    conn.Open();
        //    string sql = "select * from ny order by navn;";
        //    SQLiteCommand command = new SQLiteCommand(sql, conn);
        //    SQLiteDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {

        //        return (string)reader["navn"];
        //    }
        //    return "error";
        //}
    }
}
