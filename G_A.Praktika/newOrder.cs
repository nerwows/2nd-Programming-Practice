using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_A.Praktika
{
    public partial class newOrder : Form
    {
        Main M;

        public newOrder(Main _M)
        {
            InitializeComponent();
            M = _M;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Neįvestas vartotojo vardas!");
                    return;
                }
                else if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Neįvesta vartotojo pavardė!");
                    return;
                }
                else if (!textBox3.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Neįvestas vartotojo telefono numeris!");
                    return;
                }
                else if (!textBox4.Text.Contains("@"))
                {
                    MessageBox.Show("Neįvestas vartotojo el. paštas!");
                    return;
                }
                else if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
                {
                    MessageBox.Show("Užsakymas negali būti tuščias!");
                    return;
                }
                else
                {
                    choresPending CP = new choresPending();

                    if (checkBox1.Checked)
                        CP.changeOil = true;
                    else
                        CP.changeOil = false;

                    if (checkBox2.Checked)
                        CP.changeTyres = true;
                    else
                        CP.changeTyres = false;

                    if (checkBox3.Checked)
                        CP.washCar = true;
                    else
                        CP.washCar = false;

                    if (checkBox4.Checked)
                        CP.engineService = true;
                    else
                        CP.engineService = false;

                    //MessageBox.Show(CP.changeOil + CP.changeTyres + CP.washCar + CP.engineService);

                    M.addNewOrder(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, CP, new choresDone());

                    MessageBox.Show("Užsakymas pridėtas!");
                    Hide();
                    M.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KLAIDA: " + ex.ToString());
            }
        }
    }
}
