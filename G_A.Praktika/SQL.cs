using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace G_A.Praktika
{
    public class SQL
    {
        //http://zetcode.com/csharp/sqlite/
        //https://www.codeguru.com/csharp/.net/net_data/using-sqlite-in-a-c-application.html

        private static SQLiteConnection SQLITE_CONNECTION;

        public SQL()
        {
            SQLITE_CONNECTION = CreateConnection();


            sanityCheck();

            InsertData();
            //InsertData();
            //ReadData();
        }

        public void sanityCheck()
        {
            if (!File.Exists("database.db"))
                CreateTable();
        }

        static SQLiteConnection CreateConnection()
        {
            SQLITE_CONNECTION = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");

            try
            {
                SQLITE_CONNECTION.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: {0}", ex.ToString());
            }

            return SQLITE_CONNECTION;
        }

        static private void CreateTable()
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = "CREATE TABLE User (Username VARCHAR(20), Password VARCHAR(20))";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "CREATE TABLE clientOrder (orderID INT, clientID INT, changeOil INT, changeTyres INT, washCar INT, engineService INT, orderComplete INT)";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "CREATE TABLE bussinessClient (id INT, Name VARCHAR(20), Surname VARCHAR(20), Phone VARCHAR(12), Mail VARCHAR(20))";
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        static void InsertData()
        {
            string Username = "admin";
            string Password = Username;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = String.Format("INSERT INTO User (Username, Password) VALUES('{0}', '{1}');", Username, Password);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        static void insertOrder(Order O)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = String.Format("INSERT INTO clientOrder (chnageOil, changeTyres, washCar, engineService, orderComplete) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');", O.getName(), O.getSurname());
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = String.Format("INSERT INTO bussinessClient (Name, Surname, Phone, Mail) VALUES('{0}', '{1}', '{2}', '{3}');", O.getName(), O.getSurname(), O.getPhone(), O.getMail());
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public static void ReadData()
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

                    while (sqlite_datareader.Read())
                    {
                        string myreader = sqlite_datareader.GetString(0);
                        Console.WriteLine(myreader);
                    }
                }
            }
        }

        public bool DB_UserExists(string Username, string Password)
        {
            bool result = false;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("SELECT * FROM User WHERE Username='{0}' AND Password='{1}'", Username, Password);

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                        if (sqlite_datareader.GetString(0) == Username && sqlite_datareader.GetString(1) == Password)
                            result = true;
                        else
                            result = false;
                }
            }
            return result;
        }
    }
}
