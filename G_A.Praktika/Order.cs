using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_A.Praktika
{
    public class Order
    {
        public choresDone CD;
        public choresPending CP;

        private string Name, Surname, Phone, Mail;

        public Order(string _Name, string _Surname, string _Phone, string _Mail, choresPending _CP, choresDone _CD)
        {
            Name = _Name;
            Surname = _Surname;
            Phone = _Phone;
            Mail = _Mail;
            CP = _CP;
            CD = _CD;
        }

        #region SET

        public void setName(string x)
        {
            Name = x;
        }
        public void setSurname(string x)
        {
            Surname = x;
        }

        public void setPhone(string x)
        {
            Phone = x;
        }

        public void setMail(string x)
        {
            Mail = x;
        }

        public void setChoresDone(choresDone x)
        {
            CD = x;
        }

        public void setChoresPending(choresDone x)
        {
            CP = x;
        }

        #endregion

        #region GET
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
        #endregion
    }
}
