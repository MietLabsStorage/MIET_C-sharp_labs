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
        private readonly KeySelector<TKey> keySelector;

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

        ///<value> get/set collection name</value>
        public string CollectionName { get; set; }

        public event ResearchTeamsChangedHandler<TKey> ResearchTeamsChanged;

        /// <summary>
        /// remove from collection
        /// </summary>
        /// <param name="rt">removing object</param>
        /// <returns>true if succes else false</returns>
        public bool Remove(ResearchTeam rt)
        {
            TKey key = keySelector(rt);
            if (researchTeamCollection.ContainsKey(key))
            {
                researchTeamCollection.Remove(key);
                ResearchTeamsChanged?.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, Revision.Remove, "", rt.RegisterNumber));
                return true;
            }
            return false;
        }

        /// <summary>
        /// replace in collection
        /// </summary>
        /// <param name="rtold">that will remove</param>
        /// <param name="rtnew">that will change rtold</param>
        /// <returns>true if succes else false</returns>
        public bool Replace(ResearchTeam rtold, ResearchTeam rtnew)
        {
            TKey rtoldKey = keySelector(rtold);
            TKey rtnewKey = keySelector(rtnew);
            researchTeamCollection.Add(rtnewKey, rtnew);
            if (researchTeamCollection.ContainsKey(rtoldKey))
            {
                researchTeamCollection[rtoldKey] = researchTeamCollection[rtnewKey];
                researchTeamCollection.Remove(rtnewKey);

                ResearchTeamsChanged?.Invoke(this, new ResearchTeamsChangedEventArgs<TKey>(CollectionName, Revision.Replace, "", rtold.RegisterNumber));
                return true;
            }
            return false;
        }


    }
}
