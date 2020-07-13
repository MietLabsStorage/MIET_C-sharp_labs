using System;

namespace ResearchBase
{
    /// <summary>
    /// Paper, with fields title, author and date of publication
    /// </summary>
    class Paper
    {
        public string Title { get; set; }
        public Person Author { get; set; }
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_title">title of paper</param>
        /// <param name="_author">author of paper</param>
        /// <param name="_publicationDate">date of publication of paper</param>
        public Paper(string _title, Person _author, DateTime _publicationDate)
        {
            this.Title = _title;
            this.Author = _author;
            this.PublicationDate = _publicationDate;
        }

        /// <summary>
        /// default constructor Paper("Title", new Person(), DateTime.MinValue)
        /// </summary>
        public Paper()
        {
            this.Title = "Title";
            this.Author = new Person();
            this.PublicationDate = DateTime.MinValue;
        }

        /// <summary>
        /// convert fields of paper in string
        /// </summary>
        /// <returns>string with title, author and date of publication</returns>
        public override string ToString()
        {
            return Title + " /author:" + Author.ToString() + "/ " + PublicationDate.ToShortDateString();
        }
    }
}
