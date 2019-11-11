using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_A.Praktika
{
    class Order
    {
        public choresDone CD;
        public choresPending CP;

        private string Name, Surname, Phone, Mail;

        public Order(string _Name, string _Surname, string _Phone, string _Mail, choresPending _CP)
        {
            Name = _Name;
            Surname = _Surname;
            Phone = _Phone;
            Mail = _Mail;
            CP = _CP;

            CD = new choresDone();
        }

        public void setChoresDone(choresDone _CD)
        {
            CD = _CD;
        }

        public string getName()
        {
            return Name;
        }

        public string getSurname()
        {
            return Surname;
        }

        public string getPhone()
        {
            return Phone;
        }

        public string getMail()
        {
            return Mail;
        }
    }
}
