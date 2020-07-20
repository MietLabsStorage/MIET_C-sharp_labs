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
            if (_birthdate > DateTime.MinValue && _birthdate < DateTime.Now)
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

        /// <summary>
        /// return true (so they are equal) if this and obj are common by value
        /// </summary>
        /// <param name="obj">compared obj - should be Person-type</param>
        /// <returns>true if obj.type - Person and fields are common by value
        /// else false </returns>
        public override bool Equals(object obj)
        {
            if (obj is Person person)
                return (name == person.Name) && (surname == person.surname) && (birthdate == person.birthdate);
            else
                return false;
        }

        /// <summary>
        /// compare by equals
        /// </summary>
        /// <param name="left"> left value</param>
        /// <param name="right"> right value </param>
        /// <returns>left.Equals(right)</returns>
        public static bool operator ==(Person left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// compare by unequals
        /// </summary>
        /// <param name="left"> left value</param>
        /// <param name="right"> right value </param>
        /// <returns>!(left.Equals(right))</returns>
        public static bool operator !=(Person left, object right)
        {
            return !(left.Equals(right));
        }

        /// <summary>
        /// override GetHashCode
        /// </summary>
        /// <returns>(name+surname).GetHashCode() + birthdate.GetHashCode()</returns>
        public override int GetHashCode()
        {
            return (name+surname).GetHashCode() + birthdate.GetHashCode();
        }

        /// <summary>
        /// Copy by value
        /// </summary>
        /// <returns>object with common fields as this</returns>
        public virtual object DeepCopy()
        {
            return new Person(name, surname, birthdate);
        }


    }
}
