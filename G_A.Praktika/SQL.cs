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

            if (!userExists("admin", "admin"))
                insertUser("admin", "admin");

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

                sqlite_cmd.CommandText = "CREATE TABLE pendingClientOrder (orderID INT, clientID INT, changeOil INT, changeTyres INT, washCar INT, engineService INT)";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "CREATE TABLE doneClientOrder (orderID INT, clientID INT, changeOil INT, changeTyres INT, washCar INT, engineService INT, orderComplete INT)";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "CREATE TABLE bussinessClient (id INT, Name VARCHAR(20), Surname VARCHAR(20), Phone VARCHAR(12), Mail VARCHAR(20))";
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        #region Insert

        static void insertUser(string Username, string Password)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = String.Format("INSERT INTO User (Username, Password) VALUES('{0}', '{1}');", Username, Password);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void insertPendingOrder(Order O, int clientID)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = String.Format("INSERT INTO pendingClientOrder (clientID, changeOil, changeTyres, washCar, engineService) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');", clientID, O.CP.changeOil, O.CP.changeTyres, O.CP.washCar, O.CP.engineService);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void insertDoneOrder(Order O, int clientID)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = String.Format("INSERT INTO doneClientOrder (clientID, changeOil, changeTyres, washCar, engineService, orderComplete) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", clientID, O.CP.changeOil, O.CD.changeTyres, O.CD.washCar, O.CD.engineService, O.CD.orderComplete);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void insertClient(Order O)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = String.Format("INSERT INTO bussinessClient (Name, Surname, Phone, Mail) VALUES('{0}', '{1}', '{2}', '{3}');", O.getName(), O.getSurname(), O.getPhone(), O.getMail());
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        #endregion


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

        public bool userExists(string Username, string Password)
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

        public bool clientExists(Order O)
        {
            bool result = false;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("SELECT * FROM bussinessClient WHERE Name='{0}' AND Surname='{1}' AND Phone='{2}' AND Mail='{3}'", O.getName(), O.getSurname(), O.getPhone(), O.getMail());

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        if (sqlite_datareader.GetString(1) == O.getName() &&
                            sqlite_datareader.GetString(2) == O.getSurname() &&
                            sqlite_datareader.GetString(3) == O.getPhone() &&
                            sqlite_datareader.GetString(4) == O.getMail()
                            )
                            result = true;
                        else
                            result = false;
                    }
                }
            }
            return result;
        }

        public int readClientID(Order O)
        {
            int result = -1;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("SELECT rowid FROM bussinessClient WHERE Name='{0}' AND Surname='{1}' AND Phone='{2}' AND Mail='{3}'", O.getName(), O.getSurname(), O.getPhone(), O.getMail());

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        result = sqlite_datareader.GetInt32(0);
                    }
                }
            }
            return result;
        }

        public int getLastInsertID()
        {
            //SELECT last_insert_rowid()
            int result = -1;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT last_insert_rowid()";

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        result = sqlite_datareader.GetInt32(0);
                    }
                }
            }
            return result;
        }

        public bool TEST(string Username, string Password)
        {
            bool result = false;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("SELECT rowid FROM User WHERE Username='{0}' AND Password='{1}'", Username, Password);

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        int myreader = sqlite_datareader.GetInt32(0);
                        MessageBox.Show(myreader.ToString());
                    }
                }
            }
            return result;
        }
    }
}
