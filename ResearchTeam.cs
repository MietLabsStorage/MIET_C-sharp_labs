using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ResearchBase
{
    /// <summary>
    /// Research team
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Enumerator for search persons who have more than one publications
        /// </summary>
        /// <returns>persons who have more than one publications</returns>
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

        /// <summary>
        /// Enumerator for search persons who have publications in this year
        /// </summary>
        /// <returns>persons who have publications in this year</returns>
        public IEnumerable GetThisYearPubs()
        {
            for (int i = 0; i < papers.Count; i++)
            {
                if ((DateTime.Now.Year == ((Paper)papers[i]).PublicationDate.Year))
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

        /// <summary>
        /// object with common fields as this
        /// </summary>
        /// <param name="serialize">if true copy with serialize also with DeepCopy()</param>
        /// <returns>object with common fields as this</returns>
        public ResearchTeam DeepCopy(bool serialize)
        {
            if (serialize)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream
                {
                    Position = 0
                };
                formatter.Serialize(ms, this);
                ms.Position = 0;
                ResearchTeam rt = (ResearchTeam)formatter.Deserialize(ms);
                ms.Close();
                return rt;
            }
            else
                return (ResearchTeam)DeepCopy();
        }

        /// <summary>
        /// Save by serialize
        /// </summary>
        /// <param name="filename">filename (without filename extension) for saving</param>
        /// <returns>true if succes else false</returns>
        public bool Save(string filename)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename + Program.filenameExtension, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Load by serialize
        /// </summary>
        /// <param name="filename">filename (without filename extension) for loading</param>
        /// <returns>true if succes else false</returns>
        public bool Load(string filename)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename + ".dat", FileMode.OpenOrCreate))
                {
                    ResearchTeam rt = (ResearchTeam)formatter.Deserialize(fs);
                    this.theme = rt.theme;
                    this.name = rt.name;
                    this.registerNumber = rt.registerNumber;
                    this.timeFrame = rt.timeFrame;
                    foreach(Paper paper in rt.papers)
                    {
                        this.papers.Add(paper);
                    }
                    foreach(Person person in rt.persons)
                    {
                        this.persons.Add(person);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// add paper with UI
        /// </summary>
        /// <returns></returns>
        public bool AddFromConsole()
        {
            Console.WriteLine("Write new Paper object in format:");
            Console.WriteLine("pubName authorName authorSurname authorBirthDay.authorBirthMont.authorBirthYear pubDay.pubMonth.pubYear");
            string str;
            str = Console.ReadLine();
            string[] dat = str.Split(new char[] { ' ', '.' });
            try
            {
                this.AddPapers(new Paper(dat[0], new Person(dat[1], dat[2], new DateTime(Convert.ToInt32(dat[5]), Convert.ToInt32(dat[4]), Convert.ToInt32(dat[3]))), new DateTime(Convert.ToInt32(dat[8]), Convert.ToInt32(dat[7]), Convert.ToInt32(dat[6]))));
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error writing");
                return false;
            }
        }

        /// <summary>
        /// Save by serialize
        /// </summary>
        /// <param name="filename">filename (without filename extension) for saving</param>
        /// <param name="obj">saving object</param>
        /// <returns>true if succes else false</returns>
        static public bool Save(string filename, ResearchTeam obj)
        {
            return obj.Save(filename);
        }

        /// <summary>
        /// Load by serialize
        /// </summary>
        /// <param name="filename">filename (without filename extension) for loading</param>
        /// <param name="obj">loading object</param>
        /// <returns>true if succes else false</returns>
        static public bool Load(string filename, ResearchTeam obj)
        {
            return obj.Load(filename);
        }
    }
}
