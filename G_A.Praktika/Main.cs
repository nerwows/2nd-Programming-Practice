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
    public partial class Main : Form
    {
        private string _Username;
        private Form1 _F;

        Order[] O = new Order[100];
        int cOrder = 0;

        SQL _SQL = new SQL();

        public void addNewOrder(string Name, string Surname, string Phone, string Mail, choresPending CP)
        {
            O[cOrder] = new Order(Name, Surname, Phone, Mail, CP);
            listBox1.Items.Add(string.Format("{0}. {1} {2} {3}", cOrder + 1, Phone, Name, Surname));
            
            cOrder++;
        }

        public Main(string Username, Form1 F)
        {
            InitializeComponent();

            _Username = Username;
            toolStripLabel1.Text = "Prisijungęs kaip: " + _Username;
            _F = F;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _F.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new newOrder(this).Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            choresDone _CD = new choresDone();

            if (checkBox7.Checked)
                _CD.changeOil = true;

            if (checkBox6.Checked)
                _CD.changeTyres = true;

            if (checkBox8.Checked)
                _CD.washCar = true;

            if (checkBox9.Checked)
                _CD.engineService = true;

            if (checkBox5.Checked)
                _CD.orderComplete = true;

            O[listBox1.SelectedIndex].setChoresDone(_CD);
        }
    }
}
