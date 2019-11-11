using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_A.Praktika
{
    class Login
    {
        public Login(string Username, string Password, Form1 F)
        {
            SQL _SQL = new SQL();

            if (_SQL.DB_UserExists(Username, Password))
            {
                MessageBox.Show("Sėkmingai prisijungėte!");

                F.Hide();

                new Main(Username, F).Show();
            }
            else
                MessageBox.Show("Neteisingas vartotojo vardas arba slaptažodis!");

            //if(_SQL.DB_UserExists(Username, Password))
            //{
            //    MessageBox.Show("Sėkmingai prisijungėte!");
            //    new Main(Username).Show();
            //}
            //_SQL.sanityCheck();
        }
    }
}
