using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchBase
{
    class Team: INameAndCopy
    {
        protected string name;
        protected int registerNumber;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_name">name of company</param>
        /// <param name="_registerNumber">number of register</param>
        public Team(string _name, int _registerNumber)
        {
            this.name = _name;
            if (_registerNumber > 100000 && _registerNumber <= 999999)
                this.registerNumber = _registerNumber;
        }

        /// <summary>
        /// default constructor Team("company", 100000)
        /// </summary>
        public Team()
        {
            this.name = "Company";
            this.registerNumber = 100000;
        }

        /// <value>get/set name of company</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <value>get/set register number of company</value>
        /// <exception cref="System.ArgumentException">value must be > 100000</exception>
        public int RegisterNumber
        {
            get { return registerNumber; }
            set
            {
                if(value <= 100000)
                {
                    throw new ArgumentException("value less or equals 100000");
                }
                else
                {
                    registerNumber = value;
                }
            }
        }

        /// <summary>
        /// Copy by value
        /// </summary>
        /// <returns>object with common fields as this</returns>
        public virtual object DeepCopy()
        {
            return new Team(name, registerNumber);
        }

        /// <summary>
        /// return true (so they are equal) if this and obj are common by value
        /// </summary>
        /// <param name="obj">compared obj - should be Team-type</param>
        /// <returns>true if obj.type - Team and fields are common by value
        /// else false </returns>
        public override bool Equals(object obj)
        {
            if (obj is Team team)
                return (name == team.name) && (registerNumber == team.registerNumber);
            else
                return false;       
        }

        /// <summary>
        /// compare by equals
        /// </summary>
        /// <param name="left"> left value</param>
        /// <param name="right"> right value </param>
        /// <returns>left.Equals(right)</returns>
        public static bool operator ==(Team left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// compare by unequals
        /// </summary>
        /// <param name="left"> left value</param>
        /// <param name="right"> right value </param>
        /// <returns>!(left.Equals(right))</returns>
        public static bool operator !=(Team left, object right)
        {
            return !(left.Equals(right));
        }

        /// <summary>
        /// override GetHashCode
        /// </summary>
        /// <returns>(name).GetHashCode() + registerNumber.GetHashCode();</returns>
        public override int GetHashCode()
        {
            return (name).GetHashCode() + registerNumber.GetHashCode();
        }

        /// <summary>
        /// convert fields of person in string
        /// </summary>
        /// <returns>string with name, surname and birthdate </returns>
        public override string ToString()
        {
            return name + " " + registerNumber;
        }
    }
}
