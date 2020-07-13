using System;

namespace ResearchBase
{
    /// <summary>
    /// Person, with fields name, surname and birthdate
    /// </summary>
    class Person
    {
        private string name;
        private string surname;
        private DateTime birthdate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_name">name of person</param>
        /// <param name="_surname">surname of person</param>
        /// <param name="_birthdate">birthdate of person</param>
        /// <remarks>if birthdate not in (DateTime.MinValue; DateTime.Now] than set DateTime.MinValue </remarks>
        public Person(string _name, string _surname, DateTime _birthdate)
        {
            this.name = _name;
            this.surname = _surname;
            if (birthdate > DateTime.MinValue && birthdate < DateTime.Now)
                this.birthdate = _birthdate;
            else this.birthdate = DateTime.MinValue;
        }

        /// <summary>
        /// default constructor Person("Name","Surname",DateTime.MinValue)
        /// </summary>
        public Person()
        {
            this.name = "Name";
            this.surname = "Surname";
            this.birthdate = DateTime.MinValue;
        }

        /// <value>get/set name</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <value>get/set surname</value>
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        /// <value>get/set birthdate</value>
        /// <remarks>set only if in (DateTime.MinValue; DateTime.Now] </remarks>
        public DateTime Birthdate
        {
            get { return birthdate; }
            set
            { 
                if (value > DateTime.MinValue && value < DateTime.Now)
                    birthdate = value; 
            }
        }

        /// <value>get/set birthyear</value>
        public int BirthdayYear
        {
            get { return birthdate.Year; }
            set 
            { 
                if (value > DateTime.MinValue.Year && value < DateTime.Now.Year)
                    birthdate = new DateTime(value, birthdate.Month, birthdate.Day); 
            }
        }

        /// <summary>
        /// convert fields of person in string
        /// </summary>
        /// <returns>string with name, surname and birthdate </returns>
        public override string ToString()
        {
            return surname + " " + name + " " + birthdate.ToShortDateString();
        }

        /// <summary>
        /// convert some fields of person in string
        /// </summary>
        /// <returns>string with name and surname</returns>
        public virtual string ToShortString()
        {
            return surname + " " + name;
        }
    }
}
