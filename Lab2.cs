using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchBase
{
    class Lab2
    {
        /// <summary>
        /// done tasks from lab 1
        /// </summary>
        public static void Run()
        {
            //task 1
            //Создать два объекта типа Team с совпадающими данными и проверить,
            //что ссылки на объекты не равны, а объекты равны, вывести значения хэш-
            //кодов для объектов.
            Team team1 = new Team();
            Team team2 = new Team();
            Console.WriteLine("Equals by value: " + team1.Equals(team2));
            Console.WriteLine("Equals by ref:   " + Object.ReferenceEquals(team1, team2));
            Console.WriteLine("Hashcode 1: " + team1.GetHashCode());
            Console.WriteLine("Hashcode 2: " + team1.GetHashCode());

            //task 2
            //В блоке try/catch присвоить свойству с номером регистрации
            //некорректное значение, в обработчике исключения вывести сообщение,
            //переданное через объект-исключение
            try
            {
                team1.RegisterNumber = 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //task 3
            //Создать объект типа ResearchTeam, добавить элементы в список
            //публикаций и список участников проекта и вывести данные объекта
            //ResearchTeam.
            ResearchTeam researchTeam1 = new ResearchTeam();
            Person person1 = new Person("N1", "S1", new DateTime(2000, 1, 1));
            Person person2 = new Person("N2", "S2", new DateTime(2000, 2, 1));
            Person person3 = new Person("N1", "S2", new DateTime(2000, 1, 1));
            researchTeam1.AddPersons(person1, person2, person3);
            researchTeam1.AddPapers(new Paper("T1", person1, new DateTime(2020, 7, 20)), new Paper("T2", person3, new DateTime(2019, 7, 20)));

            //task 4
            //Вывести значение свойства Team для объекта типа ResearchTeam.
            Console.WriteLine(researchTeam1.ToString());

            //task 5
            // С помощью метода DeepCopy() создать полную копию объекта
            //ResearchTeam.Изменить данные в исходном объекте ResearchTeam и
            //вывести копию и исходный объект, полная копия исходного объекта
            //должна остаться без изменений.
            ResearchTeam researchTeam2 = (ResearchTeam)researchTeam1.DeepCopy();
            researchTeam2.RegisterNumber = 999998;
            Console.WriteLine(researchTeam1.ToString());
            Console.WriteLine(researchTeam2.ToString());

            //task 6
            //С помощью оператора foreach для итератора, определенного в классе
            //ResearchTeam, вывести список участников проекта, которые не имеют
            //публикаций.
            foreach (object obj in researchTeam1)
            {
                Console.WriteLine(obj.ToString());
            }

            //task 7
            //С помощью оператора foreach для итератора с параметром,
            //определенного в классе ResearchTeam, вывести список всех публикаций,
            //вышедших за последние два года.
            foreach (object obj in researchTeam1.GetEnumerator(2))
            {
                Console.WriteLine(obj.ToString());
            }

            //task 8
            //С помощью оператора foreach для объекта типа ResearchTeam вывести
            //список участников проекта, у которых есть публикации.
            foreach (object obj in researchTeam1.GetPersonsWithPubs())
            {
                Console.WriteLine(obj.ToString());
            }

            //task 9
            //С помощью оператора foreach для итератора, определенного в классе
            //ResearchTeam, вывести список участников проекта, имеющих более
            //одной публикации.
            foreach (object obj in researchTeam1.GetPersonsWithMoreOnePubs())
            {
                Console.WriteLine(obj.ToString());
            }

            //task 10
            //C помощью оператора foreach для итератора, определенного в классе
            //ResearchTeam, вывести список публикаций, вышедших за последний год.
            foreach (object obj in researchTeam1.GetThisYearPubs())
            {
                Console.WriteLine(obj.ToString());
            }

        }
    }
}
