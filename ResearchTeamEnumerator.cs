using System;
using System.Collections;

namespace ResearchBase
{
    /// <summary>
    /// Enumerator for search persons who have publications
    /// </summary>
    class ResearchTeamEnumerator: IEnumerator
    {
        private ArrayList persons;
        private ArrayList papers;
        public ResearchTeamEnumerator(ArrayList persons, ArrayList papers)
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
