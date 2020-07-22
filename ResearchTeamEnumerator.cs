using System;
using System.Collections;
using System.Collections.Generic;

namespace ResearchBase
{
    /// <summary>
    /// Enumerator for search persons who have publications
    /// </summary>
    class ResearchTeamEnumerator: IEnumerator
    {
        private readonly List<Person> persons;
        private readonly List<Paper> papers;
        public ResearchTeamEnumerator(List<Person> persons, List<Paper> papers)
        {
            this.persons = persons;
            this.papers = papers;
        }

        int index = -1;

        public bool MoveNext()
        {
            for (int i = index + 1; i < persons.Count; i++)
            {
                foreach(Paper paper in papers)
                {
                    if (paper.Author == persons[i])
                    {
                        index = i;
                        return true;
                    }
                }
            }
            return false;

        }

        public void Reset()
        {
            index = -1;
        }


        public object Current
        {
            get
            {
                return persons[index];
            }
        }

    }
}
