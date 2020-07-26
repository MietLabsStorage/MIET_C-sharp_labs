using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ResearchBase
{
    /// <summary>
    /// Research team
    /// </summary>
    class ResearchTeam: Team, INameAndCopy, IEnumerable
    {
        private string theme;
        private TimeFrame timeFrame;
        private readonly List<Paper> papers;
        private readonly List<Person> persons;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_theme">theme of research</param>
        /// <param name="_team">reseacher team</param>
        /// <param name="_timeFrame">time frame of research</param>
        /// <remarks>if _registerNumber not in (100000;999999] than set 100000</remarks>
        public ResearchTeam(string _theme, Team _team, TimeFrame _timeFrame): base(_team.Name,_team.RegisterNumber)
        {
            this.theme = _theme;
            this.timeFrame = _timeFrame;
            this.papers = new List<Paper>();
            this.persons = new List<Person>();
        }

        /// <summary>
        /// default constructor ResearchTeam("theme", new Team("company", 1000000), TimeFrame.Year)
        /// </summary>
        public ResearchTeam(): base()
        {
            this.theme = "theme";
            this.timeFrame = TimeFrame.Year;
            this.papers = new List<Paper>();
            this.persons = new List<Person>();
        }

        /// <value>get/set theme of research</value>
        public string Theme 
        { 
            get { return theme; } 
            set 
            {
                theme = value;
                OnPropertyChanged("Research.Theme");
            }
        }

        /// <value>get/set time frame of research</value>
        public TimeFrame TimeFrame 
        {
            get { return timeFrame; }
            set 
            { 
                timeFrame = value;
                OnPropertyChanged("Research.TimeFrame");
            }
        }

        /// <value>get/set papers</value>
        public List<Paper> Papers 
        { 
            get { return papers; }
            set 
            {
                papers.Clear();
                foreach(Paper paper in value)
                {
                    papers.Add(paper);
                }
            }
        }

        /// <value>get paper with last publication date</value>
        public Paper LastPaper
        {
            get
            {
                if(papers.Count == 0)
                {
                    return null;
                }
                int returnIndex = 0;
                for(int i = 1; i < papers.Count; i++)
                {
                    if (((Paper)papers[i]).PublicationDate > ((Paper)papers[returnIndex]).PublicationDate)
                        returnIndex = i;
                }
                return (Paper)papers[returnIndex];
            }
        }

        /// <summary>
        /// indexator by TimeFrame
        /// </summary>
        /// <param name="index">current timeframe index</param>
        /// <returns>return true if timeframe == index</returns>
        public bool this[TimeFrame index]
        {
            get
            {
                return timeFrame == index;
            }
        }

        /// <summary>
        /// add some papers to current papers
        /// </summary>
        /// <param name="addPapers">papers that added</param>
        public List<Paper> AddPapers(params Paper[] addPapers)
        {
            if(addPapers != null)
                foreach (Paper value in addPapers)
                {
                    papers.Add(value);
                }
            return papers;
        }


        /// <summary>
        /// convert fields of research team in string
        /// </summary>
        /// <returns>string with theme, company, register number, timeframe and papers</returns>
        public override string ToString()
        {
            string str = 
                "Theme::           " + theme + "\n" +
                "Company::         " + name + "\n" +
                "Register number:: " + registerNumber + "\n" +
                "Time Frame::      " + timeFrame + "\n";
            str += "Papers:: ";
            for(int i = 0; i < papers?.Count; i++)
            {
                str += "\n *" + papers[i].ToString();
            }
            str += "\nPersons:: ";
            for (int i = 0; i < persons?.Count; i++)
            {
                str += "\n *" + persons[i].ToString();
            }
            return str;
        }

        /// <summary>
        /// convert some fields of research team in string
        /// </summary>
        /// <returns>string with theme, company, register number and time frame</returns>
        public virtual string ToShortString()
        {
            return theme + " " + name + " " + registerNumber + " " + timeFrame;
        }

        /// <summary>
        /// Copy by value
        /// </summary>
        /// <returns>object with common fields as this</returns>
        public override object DeepCopy()
        {
            ResearchTeam researchTeam = new ResearchTeam(theme, new Team(name, registerNumber), timeFrame);
            foreach (Paper value in this.papers)
                researchTeam.AddPapers(value);
            foreach (Person value in this.persons)
                researchTeam.AddPersons(value);
            return researchTeam;
        }

        /// <value>get/set persons</value>
        public List<Person> Persons
        {
            get { return persons; }
            set
            {
                persons.Clear();
                foreach (Person person in value)
                {
                    persons.Add(person);
                }
            }
        }

        /// <summary>
        /// add some papers to current persons
        /// </summary>
        /// <param name="addPersons">persons that added</param>
        public List<Person> AddPersons(params Person[] addPersons)
        {
            if(addPersons!= null)
                foreach (Person value in addPersons)
                {
                    persons.Add(value);
                }
            return persons;
        }

        /// <value>get/set team</value>
        public Team Team
        {
            get { return new Team(name, registerNumber); }
            set 
            {
                name = value.Name;
                registerNumber = value.RegisterNumber;
            }
        }

        /// <summary>
        /// Enumerator for persons with non-publications
        /// </summary>
        /// <returns>person without publication</returns>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < persons.Count; i++)
            {
                bool noPubs = true;
                for (int j = 0; j < papers.Count; j++)
                {
                    if ((Person)((Paper)papers[j]).Author == (Person)persons[i])
                    {
                        noPubs = false;
                        break;
                    }
                }
                if (noPubs == true)
                {
                    yield return persons[i];
                }
            }
        }

        /// <summary>
        /// Enumerator for publication which publicated in last n years
        /// </summary>
        /// <param name="n">count of last years</param>
        /// <returns>publication in last n years</returns>
        public IEnumerable GetEnumerator(int n)
        {
            for (int i = 0; i < papers.Count; i++)
            {
                if ((DateTime.Now.Year - ((Paper)papers[i]).PublicationDate.Year) <= n)
                {
                    yield return papers[i];
                }
            }
        }

        /// <summary>
        /// Enumerator for search persons who have publications
        /// </summary>
        /// <returns>persons with publications</returns>
        public IEnumerable GetPersonsWithPubs()
        {
            IEnumerator ie = new ResearchTeamEnumerator(persons, papers);
            while (ie.MoveNext())   
            {
                yield return (Person)ie.Current;
            }
            ie.Reset(); 
        }

        public IEnumerable GetPersonsWithMoreOnePubs()
        {
            for (int i = 0; i < persons.Count; i++)
            {
                int countPubs = 0;
                for (int j = 0; j < papers.Count; j++)
                {
                    if ((Person)((Paper)papers[j]).Author == (Person)persons[i])
                    {
                        countPubs++;
                    }
                }
                if (countPubs > 1)
                {
                    yield return persons[i];
                }
            }
        }

        public IEnumerable GetThisYearPubs()
        {
            for (int i = 0; i < papers.Count; i++)
            {
                if ((DateTime.Now.Year - ((Paper)papers[i]).PublicationDate.Year) < 1)
                {
                    yield return papers[i];
                }
            }
        }

        /// <summary>
        /// sort by publication date
        /// </summary>
        public void SortByDate()
        {
            papers.Sort();
        }

        /// <summary>
        /// sort by title
        /// </summary>
        public void SortByName()
        {
            papers.Sort(new Paper());
        }

        /// <summary>
        /// sort by author's surname
        /// </summary>
        public void SortByAuthorSnm()
        {
            papers.Sort(new PaperIComparer());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
