using System;
using System.Collections.Generic;
using System.Linq;

namespace ResearchBase
{
    /// <summary>
    /// Dictionary collection of research team
    /// </summary>
    /// <typeparam name="TKey">type of key in dictionary</typeparam>
    class ResearchTeamCollection<TKey>
    {
        private Dictionary<TKey, ResearchTeam> researchTeamCollection;
        private KeySelector<TKey> keySelector;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_keySelector">key selector</param>
        public ResearchTeamCollection(KeySelector<TKey> _keySelector)
        {
            keySelector = _keySelector;
            researchTeamCollection = new Dictionary<TKey, ResearchTeam>();
        }

        /// <summary>
        /// add new ResearchTeam() in dictionary 
        /// </summary>
        public void AddDefault()
        {
            ResearchTeam rt = new ResearchTeam();
            researchTeamCollection.Add(keySelector(rt), rt);
        }

        /// <summary>
        /// add some research teams in dictionary
        /// </summary>
        /// <param name="addResearchTeam">added research teams</param>
        public void AddResearchTeams(params ResearchTeam[] addResearchTeam)
        {
            for (int i = 0; i < addResearchTeam.Length; i++)
            {
                researchTeamCollection.Add(keySelector(addResearchTeam[i]), addResearchTeam[i]);
            }
        }

        /// <summary>
        /// convert fields of dictionary in string
        /// </summary>
        /// <returns>string with title, author and date of publication</returns>
        public override string ToString()
        {
            string str = "";
            foreach (ResearchTeam obj in researchTeamCollection.Values)
            {
                str += obj.ToString() + "\n\n";
            }
            return str;
        }

        /// <summary>
        /// convert some fields of dictionary in string
        /// </summary>
        /// <returns>string with name and surname</returns>
        public string ToShortString()
        {
            string str = "";
            foreach (ResearchTeam obj in researchTeamCollection.Values)
            {
                str += obj.ToShortString() + "\nчисло участников проекта:: " + obj.Persons.Count + "\nчисло публикаций:: " + obj.Papers.Count + "\n\n";
            }
            return str;

        }

        /// <value>get date of last all publication in dictionary</value>
        public DateTime LastPublication
        {
            get 
            {
                if (researchTeamCollection.Count == 0)
                {
                    return default;
                }
                else
                {
                    List<Paper> maxDateList = new List<Paper>();
                    foreach (ResearchTeam rt in researchTeamCollection.Values)
                    {
                        if (rt.LastPaper == null)
                            continue;
                        maxDateList.Add(rt.LastPaper);
                    }
                    return maxDateList.Max(paper => paper.PublicationDate);
                }

            }
        }

        /// <summary>
        /// research teams with so time frame
        /// </summary>
        /// <param name="value">so time frame</param>
        /// <returns>research teams with so time frame</returns>
        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return researchTeamCollection.Where(rt => rt.Value.TimeFrame == value);
        }

        /// <value>get groups by TimeFrame</value>
        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>> GroupByTimeFrame
        {
            get
            {
                return researchTeamCollection.GroupBy(rt => rt.Value.TimeFrame);
            }
        }

    }
}
