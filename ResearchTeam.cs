using System;

namespace ResearchBase
{
    /// <summary>
    /// Research team
    /// </summary>
    class ResearchTeam
    {
        private string theme;
        private string company;
        private int registerNumber;
        private TimeFrame timeFrame;
        private Paper[] papers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_theme">theme of research</param>
        /// <param name="_company">reseacher company</param>
        /// <param name="_registerNumber">number of register</param>
        /// <param name="_timeFrame">time frame of research</param>
        /// <remarks>if _registerNumber not in (100000;999999] than set 100000</remarks>
        public ResearchTeam(string _theme, string _company, int _registerNumber, TimeFrame _timeFrame)
        {
            this.theme = _theme;
            this.company = _company;
            if (_registerNumber > 100000 && _registerNumber <= 999999)
                this.registerNumber = _registerNumber;
            else this.registerNumber = 100000;
            this.timeFrame = _timeFrame;
            this.papers = null;
        }

        /// <summary>
        /// default constructor ResearchTeam("theme", "company", 1000000, TimeFrame.Year)
        /// </summary>
        public ResearchTeam()
        {
            this.theme = "theme";
            this.company = "company";
            this.registerNumber = 100000;
            this.timeFrame = TimeFrame.Year;
            this.papers = null;
        }

        /// <value>get/set theme of research</value>
        public string Theme 
        { 
            get { return theme; } 
            set { theme = value; }
        }

        /// <value>get/set researcher company</value>
        public string Company 
        { 
            get { return company; }
            set { company = value; }
        }

        /// <value>get/set theme of research</value>
        /// <remarks>set only if in (100000;999999] </remarks>
        public int RegisterNumber
        {
            get { return registerNumber; }
            set { if (value > 100000 && value <= 999999) registerNumber = value; }
        }

        /// <value>get/set time frame of research</value>
        public TimeFrame TimeFrame 
        {
            get { return timeFrame; }
            set { timeFrame = value; }
        }

        /// <value>get/set papers</value>
        public Paper[] Papers { get; set; }

        /// <value>get paper with last publication date</value>
        public Paper LastPaper
        {
            get
            {
                if(papers.Length == 0)
                {
                    return null;
                }
                int returnIndex = 0;
                for(int i = 1; i < papers.Length; i++)
                {
                    if (papers[i].PublicationDate > papers[returnIndex].PublicationDate)
                        returnIndex = i;
                }
                return papers[returnIndex];
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
        public void AddPapers(params Paper[] addPapers)
        {
            int arrSize = papers == null ? 0 : papers.Length;
            Array.Resize<Paper>(ref papers, arrSize + addPapers.Length);
            for (int i = 0; i < addPapers.Length; i++)
            {
                papers[arrSize + i] = addPapers[i];
            }
        }

        /// <summary>
        /// convert fields of research team in string
        /// </summary>
        /// <returns>string with theme, company, register number, timeframe and papers</returns>
        public override string ToString()
        {
            string str = 
                "Theme::           " + theme + "\n" +
                "Company::         " + company + "\n" +
                "Register number:: " + registerNumber + "\n" +
                "Time Frame::      " + timeFrame + "\n";
            str += "Papers:: ";
            for(int i = 0; i < papers?.Length; i++)
            {
                str += "\n *" + papers[i].ToString();
            }
            return str;
        }

        /// <summary>
        /// convert some fields of research team in string
        /// </summary>
        /// <returns>string with theme, company, register number and time frame</returns>
        public virtual string ToShortString()
        {
            return theme + " " + company + " " + registerNumber + " " + timeFrame;
        }
    }
}
