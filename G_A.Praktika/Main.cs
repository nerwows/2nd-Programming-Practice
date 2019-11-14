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

            O.Add(_Order);
            listBox1.Items.Add(string.Format("{0}. {1} | {2} | {3} | {4}", O.Count, Phone, Name, Surname, Mail));

            int lastClientID = -1;

            if (!_SQL.clientExists(_Order))
            {
                _SQL.insertClient(_Order);
                lastClientID = _SQL.getLastInsertID();
            }

            lastClientID = _SQL.readClientID(_Order);

            //MessageBox.Show(Convert.ToInt32(_Order.CP.changeOil) + " " + 
            //    Convert.ToInt32(_Order.CP.changeTyres) + " " + 
            //    Convert.ToInt32(_Order.CP.washCar) + " " + 
            //    Convert.ToInt32(_Order.CP.engineService));


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
            if (checkBox5.Checked && !checkBox5.Enabled)
            {
                MessageBox.Show("Užsakymas jau yra įvygdytas ir jo būsenos keisti nebegalima.");
            }
            else
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
                {
                    _CD.orderComplete = true;
                    checkBox5.Enabled = false;
                    checkBox6.Enabled = false;
                    checkBox7.Enabled = false;
                    checkBox8.Enabled = false;
                    checkBox9.Enabled = false;
                }


                O[listBox1.SelectedIndex].setChoresDone(_CD);
                _SQL.updateChoresDone(listBox1.SelectedIndex + 1, _CD);
            }

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(O[0].CD.changeOil.ToString() + O[0].CD.changeTyres + O[0].CD.washCar + O[0].CD.engineService + O[0].CD.orderComplete);

            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
            checkBox9.Enabled = true;

            //Pending
            if (O[listBox1.SelectedIndex].CP.changeOil)
                checkBox1.Checked = true;
            else
            {
                checkBox1.Checked = false;
                checkBox7.Enabled = false;
            }

            if (O[listBox1.SelectedIndex].CP.changeTyres)
                checkBox2.Checked = true;
            else
            {
                checkBox2.Checked = false;
                checkBox6.Enabled = false;
            }

            if (O[listBox1.SelectedIndex].CP.washCar)
                checkBox3.Checked = true;
            else
            {
                checkBox3.Checked = false;
                checkBox8.Enabled = false;
            }


            if (O[listBox1.SelectedIndex].CP.engineService)
                checkBox4.Checked = true;
            else
            {
                checkBox4.Checked = false;
                checkBox9.Enabled = false;
            }


            //Done
            if (O[listBox1.SelectedIndex].CD.changeOil)
                checkBox7.Checked = true;
            else
                checkBox7.Checked = false;

            if (O[listBox1.SelectedIndex].CD.changeTyres)
                checkBox6.Checked = true;
            else
                checkBox6.Checked = false;

            if (O[listBox1.SelectedIndex].CD.washCar)
                checkBox8.Checked = true;
            else
                checkBox8.Checked = false;

            if (O[listBox1.SelectedIndex].CD.engineService)
                checkBox9.Checked = true;
            else
                checkBox9.Checked = false;

            if (O[listBox1.SelectedIndex].CD.orderComplete)
            {
                checkBox5.Checked = true;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
            }
            else
                checkBox5.Checked = false;
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            //if((checkBox1.Checked && !checkBox7.Checked) || 
            //    (checkBox2.Checked && !checkBox6.Checked) ||
            //    (checkBox3.Checked && !checkBox8.Checked) ||
            //    (checkBox4.Checked && !checkBox9.Checked)
            //    )
            //{
            //    MessageBox.Show("Įvygdytas ne visas užsakymas, tad negalima jo pažymėti, kaip baigto!");
            //    checkBox5.Checked = false;
            //}
        }
    }
}