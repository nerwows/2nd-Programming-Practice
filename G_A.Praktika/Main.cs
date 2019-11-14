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

        List<Order> O = new List<Order>();

        SQL _SQL = new SQL();

        public void addNewOrder(string Name, string Surname, string Phone, string Mail, choresPending CP, choresDone CD)
        {
            Order _Order = new Order(Name, Surname, Phone, Mail, CP, CD);

            //MessageBox.Show(Convert.ToInt32(CP.changeOil) + " " + Convert.ToInt32(CP.changeTyres) + " " + Convert.ToInt32(CP.washCar) + " " + Convert.ToInt32(CP.engineService));

            O.Add(_Order);
            listBox1.Items.Add(string.Format("{0}. {1} {2} {3}", O.Count, Phone, Name, Surname));

            int lastClientID = -1;

            if (!_SQL.clientExists(_Order))
            {
                _SQL.insertClient(_Order);
                lastClientID = _SQL.getLastInsertID();
                //MessageBox.Show(lastClientID.ToString());
            }

            lastClientID = _SQL.readClientID(_Order);

            _SQL.insertDoneOrder(_Order, lastClientID);
            _SQL.insertPendingOrder(_Order, lastClientID);
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
            _SQL.GetChoresPending(1);

            O = _SQL.loadData();

            for (int i = 0; i < O.Count; i++)
                listBox1.Items.Add(string.Format("{0}. {1} | {2} | {3} | {4}", i + 1, O[i].getPhone(), O[i].getName(), O[i].getSurname(), O[i].getMail()));
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

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pending
            if (O[listBox1.SelectedIndex].CP.changeOil)
                checkBox1.Checked = true;

            if (O[listBox1.SelectedIndex].CP.changeTyres)
                checkBox2.Checked = true;

            if (O[listBox1.SelectedIndex].CP.washCar)
                checkBox3.Checked = true;

            if (O[listBox1.SelectedIndex].CP.engineService)
                checkBox4.Checked = true;


            //Done
            if (O[listBox1.SelectedIndex].CD.changeOil)
                checkBox7.Checked = true;

            if (O[listBox1.SelectedIndex].CD.changeTyres)
                checkBox6.Checked = true;

            if (O[listBox1.SelectedIndex].CD.washCar)
                checkBox8.Checked = true;

            if (O[listBox1.SelectedIndex].CD.engineService)
                checkBox9.Checked = true;

            if (O[listBox1.SelectedIndex].CD.orderComplete)
                checkBox5.Checked = true;
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }
    }
}