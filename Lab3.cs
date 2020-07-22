using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchBase
{
    class Lab3
    {
        public static void Run()
        {
            //task 1
            // Создать объект ResearchTeam и вызвать методы, выполняющие
            //сортировку списка публикаций List<Paper> по разным критериям, после
            //каждой сортировки вывести данные объекта. Выполнить сортировку
            // по дате выхода публикации;
            // по названию публикации;
            // по фамилии автора.
            ResearchTeam researchTeam1 = new ResearchTeam("R1", new Team("team1",220020), TimeFrame.Long);
            Person person1 = new Person("N1", "S1", new DateTime(2000, 1, 1));
            Person person2 = new Person("N2", "S2", new DateTime(2000, 2, 1));
            Person person3 = new Person("N1", "S2", new DateTime(2000, 1, 1));
            researchTeam1.AddPersons(person1, person2, person3);
            researchTeam1.AddPapers(new Paper("T1", person1, new DateTime(2020, 7, 20)), new Paper("T2", person3, new DateTime(2019, 7, 20)));

            researchTeam1.SortByDate();
            Console.WriteLine(researchTeam1.ToString());
            researchTeam1.SortByName();
            Console.WriteLine(researchTeam1.ToString());
            researchTeam1.SortByAuthorSnm();
            Console.WriteLine(researchTeam1.ToString());

            //task 2
            //Создать объект ResearchTeamCollection<string>. Добавить в коллекцию
            //несколько разных элементов ResearchTeam и вывести объект
            //ResearchTeamCollection<string>
            KeySelector<string> keySelector = (rt) => rt.GetHashCode().ToString();
            ResearchTeamCollection<string> researchTeamCollection = new ResearchTeamCollection<string>(keySelector);
            ResearchTeam researchTeam2 = new ResearchTeam("R2", new Team("team2", 220021), TimeFrame.Long);
            researchTeam2.AddPersons(person1, person3); researchTeam2.AddPapers(new Paper("T3", person3, new DateTime(2018, 7, 20)));
            researchTeamCollection.AddDefault();
            researchTeamCollection.AddResearchTeams(researchTeam1, researchTeam2);
            Console.WriteLine(researchTeamCollection.ToString());

            //task 3
            // Вызвать методы класса ResearchTeamCollection<string>, выполняющие
            //операции с коллекцией - словарем Dictionary<TKey, ResearchTeam>, после
            //каждой операции вывести результат операции. Выполнить
            // поиск даты последней по времени выхода публикации среди всех
            //элементов коллекции;
            // вызвать метод TimeFrameGroup для выбора объектов ResearchTeam
            //с заданным значением продолжительности исследований;
            // вызвать свойство класса, выполняющее группировку элементов
            //коллекции по значениию продолжительности исследований;
            //вывести все группы элементов из списка.

            Console.WriteLine(researchTeamCollection.LastPublication.ToShortDateString());
            foreach (var obj in researchTeamCollection.TimeFrameGroup(TimeFrame.Long))
            {
                Console.WriteLine(obj.Key + "\n" + obj.Value.ToString() + "\n");
            }
            foreach (var group in researchTeamCollection.GroupByTimeFrame)
            {
                Console.WriteLine(group.Key + "::");
                foreach (var obj in group)
                {
                    Console.WriteLine(obj.Key);
                    Console.WriteLine(obj.Value);
                }
                Console.WriteLine();
            }

            //task 4
            //Создать объект типа TestCollection<Team, ResearchTeam>. Ввести число
            //элементов в коллекциях и вызвать метод для поиска первого,
            //центрального, последнего и элемента, не входящего в коллекции.
            //Вывести значения времени поиска для всех четырех случаев.
            GenerateElement<Team, ResearchTeam> generator = delegate (int j)
            {
                Team el_key = new Team();
                el_key.Name += j;
                ResearchTeam el_value = new ResearchTeam();
                return new KeyValuePair<Team, ResearchTeam>(el_key, el_value);
            };

            string ch;
            while (true)
            {
                ch = Console.ReadLine();
                try
                {
                    var testColl = new TestCollection<Team, ResearchTeam>(Convert.ToInt32(ch), generator);
                    testColl.SearchListTKey();
                    testColl.SearchListString();
                    testColl.SearchDictTkey();
                    testColl.SearchDictString();
                    testColl.SearchDictValue();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\nTry again");
                }
            }

        }
    }
}
