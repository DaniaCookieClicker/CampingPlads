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

        public void Connection()
        {
            //string curFile = @"c:\temp\CampingPlads.db";
            //if (!File.Exists(curFile))
            //{

            //}
            SQLiteConnection.CreateFile("CampingPlads.db");
           // String connStr = "Data Source=ny.db;Version=3";
           // SQLiteConnection conn = new SQLiteConnection(connStr);
            conn.Open();
            

        }

        public void CreateTable()
        {
            //String connStr = "Data Source=ny.db;Version=3";
            //SQLiteConnection conn = new SQLiteConnection(connStr);
            conn.Open();
            String sql = "create table Budget (indkomst integer, udgift integer);";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
           sql = "create table Plads (id integer primary key, type boolean,rejsende integer, foreign key (rejsende) references rejsende(id));";
             command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
             sql = "create table rejsende (id integer primary key, nationalitet string , penge integer, type boolean,tolerance integer);";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void insert()
        {
            //String connStr = "Data Source=ny.db;Version=3";
            //SQLiteConnection conn = new SQLiteConnection(connStr);
            conn.Open();
            String sql = "insert into ny values(null,'Kalasnikov');";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }
        public string select()
        {
            //String connStr = "Data Source=ny.db;Version=3";
            //SQLiteConnection conn = new SQLiteConnection(connStr);
            conn.Open();
            string sql = "select * from ny order by navn;";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                return (string)reader["navn"];
            }
            return "error";
        }
    }
}
