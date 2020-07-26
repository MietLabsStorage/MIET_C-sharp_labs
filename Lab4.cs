using System;
using System.Collections.Generic;

namespace ResearchBase
{
    class Lab4
    {
        public static void Run()
        {
            //task 1
            //Создать две коллекции ResearchTeamCollection<string>.
            static string keySelector(ResearchTeam rt) => rt.GetHashCode().ToString();
            ResearchTeamCollection<string> collection1 = new ResearchTeamCollection<string>(keySelector);
            collection1.CollectionName = "collection1";
            ResearchTeamCollection<string> collection2 = new ResearchTeamCollection<string>(keySelector);
            collection2.CollectionName = "collection2";

            List<Person> persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                Random rnd = new Random(DateTime.Now.Second+i);
                persons.Add(new Person("Snm" + i, "Nm" + i, new DateTime(2005 - rnd.Next(10), rnd.Next(12) + 1, rnd.Next(15) + 1)));
            }

            List<Paper> papers = new List<Paper>();
            for (int i = 0; i < 10; i++)
            {
                Random rnd = new Random(DateTime.Now.Second + i);
                papers.Add(new Paper("title" + i, persons[rnd.Next(10)], new DateTime(2020 - rnd.Next(5), rnd.Next(12) + 1, rnd.Next(15) + 1)));
            }

            List<ResearchTeam> researchTeams = new List<ResearchTeam>();
            for (int i = 0; i < 8; i++)
            {
                Random rnd = new Random(DateTime.Now.Second + i);
                researchTeams.Add(new ResearchTeam("theme" + i, new Team("company" + i, 100001 + i), (TimeFrame)rnd.Next(3)));
                for(int j = 0; j < rnd.Next(5); j++)
                {
                    researchTeams[i].AddPersons(persons[rnd.Next(10)]);
                }
                for (int j = 0; j < rnd.Next(5); j++)
                {
                    researchTeams[i].AddPapers(papers[rnd.Next(10)]);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                collection1.AddResearchTeams(researchTeams[i]);
                collection2.AddResearchTeams(researchTeams[i+4]);
            }

            //task 2
            //Создать объект TeamsJournal, подписать его на события ResearchTeamsChanged из
            // обоих объектов ResearchTeamCollection<string>
            TeamsJournal journal = new TeamsJournal();
            collection1.ResearchTeamsChanged += journal.NewListEntry;
            collection2.ResearchTeamsChanged += journal.NewListEntry;
            for (int i = 0; i < 8; i++)
            {
                researchTeams[i].PropertyChanged += journal.NewListEntry;
            }

            //task 3
            //Внести изменения в коллекции ResearchTeamCollection<string>
            // добавить элементы в коллекции;
            // изменить значения разных свойств элементов, входящих в коллекцию;
            // удалить элемент из коллекции;
            // изменить данные в удаленном элементе;
            // заменить один из элементов коллекции;
            // изменить данные в элементе, который был удален из коллекции при замене элемента.
            collection1.AddResearchTeams(researchTeams[5]);
            collection2.AddResearchTeams(researchTeams[1]);

            for (int i = 0; i < 8; i++)
            {
                researchTeams[i].TimeFrame = TimeFrame.Long;
            }

            collection2.Remove(researchTeams[1]);

            researchTeams[1].Theme = "non-theme";

            collection1.Replace(researchTeams[2], researchTeams[6]);

            researchTeams[2].TimeFrame = TimeFrame.TwoYears;

            //task 4
            //Вывести данные объекта TeamsJournal.
            Console.WriteLine(journal.ToString());
        }

    }
}
