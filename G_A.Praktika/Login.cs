using System.Windows.Forms;

namespace G_A.Praktika
{
    class Login
    {
        public Login(string Username, string Password, fLogin F)
        {
            SQL _SQL = new SQL();

            if (_SQL.userExists(Username, Password))
            {
                MessageBox.Show("Sėkmingai prisijungėte!");

                F.Hide();

                new Main(Username, F).Show();
            }
            else
                MessageBox.Show("Neteisingas vartotojo vardas arba slaptažodis!");
        }
    }
}
