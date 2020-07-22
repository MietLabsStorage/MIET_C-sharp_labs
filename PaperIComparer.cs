using System;
using System.Collections.Generic;

namespace ResearchBase
{
    /// <summary>
    /// class for realise IComparer^Paper^ to compare vy author's surname
    /// </summary>
    class PaperIComparer : IComparer<Paper>
    {
        public int Compare(Paper p1, Paper p2)
        {
            return p1.Author.Surname.CompareTo(p2.Author.Surname);
        }
    }
}
