using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ResearchBase
{
    class TeamsJournal
    {
        private readonly List<TeamsJournalEntry> journal = new List<TeamsJournalEntry>();

        public void NewListEntry(object obj, EventArgs e)
        {
            if (e is ResearchTeamsChangedEventArgs<string> eventR)
            {
                journal.Add(new TeamsJournalEntry(eventR.CollectionName, eventR.Revision, eventR.PropertyName, eventR.RegisterNumber));
            }
            else if (e is PropertyChangedEventArgs eventP)
            {
                TeamsJournalEntry NewEntry = new TeamsJournalEntry("", Revision.Property, eventP.PropertyName, ((ResearchTeam)obj).RegisterNumber);
                journal.Add(NewEntry);
            }
        }

        public override string ToString()
        {
            string line = "";
            foreach (TeamsJournalEntry obj in journal)
            {
                line += "-*-*-*-*-*-*-*-*-*-\n" + obj.ToString() + "\n";
            }
            line += "-*-*-*-*-*-*-*-*-*-";
            return line;
        }

    }
}
