using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ResearchBase
{
    class Lab5
    {
        public static void Run()
        {
            //task 1
            //Создать объект типа T с непустым списком элементов, для которого
            //предусмотрен ввод данных с консоли.Создать полную копию объекта с
            //помощью метода, использующего сериализацию, и вывести исходный
            //объект и его копию.
            ResearchTeam researchTeam1 = new ResearchTeam("R1", new Team("team1", 220020), TimeFrame.Long);
            Person person1 = new Person("N1", "S1", new DateTime(2000, 1, 1));
            Person person2 = new Person("N2", "S2", new DateTime(2000, 2, 1));
            Person person3 = new Person("N1", "S2", new DateTime(2000, 1, 1));
            researchTeam1.AddPersons(person1, person2, person3);
            researchTeam1.AddPapers(new Paper("T1", person1, new DateTime(2020, 7, 20)), new Paper("T2", person3, new DateTime(2019, 7, 20)));

            ResearchTeam researchTeam2 = researchTeam1.DeepCopy(true);
            Console.WriteLine(researchTeam1);
            Console.WriteLine(researchTeam2);

            //task 2
            //Предложить пользователю ввести имя файла:
            // если файла с введенным именем нет, приложение должно
            //сообщить об этом и создать файл;
            // если файл существует, вызвать метод Load(string filename) для
            //инициализации объекта T данными из файла.
            ResearchTeam researchTeam3 = new ResearchTeam();
            Console.WriteLine("Write file name");
            string filename = Console.ReadLine();
            if (File.Exists(filename + ".dat"))
            {
                bool isLoadSucces = researchTeam3.Load(filename);
                if (!isLoadSucces)
                {
                    Console.WriteLine("load from clear file");
                }
            }
            else
            {
                Console.WriteLine("File didn\'t found but create now");
                File.Create(filename + ".dat");
            }

            //task 3
            //Вывести объект T.
            Console.WriteLine(researchTeam3);

            //task 4
            //Для этого же объекта T сначала вызвать метод AddFromConsole(), затем
            //метод Save(string filename). Вывести объект T.
            researchTeam3.AddFromConsole();
            researchTeam3.Save(filename + ".dat");
            Console.WriteLine(researchTeam3);

            //task 5
            //Вызвать последовательно
            // статический метод Load(string filename, T obj), передав как
            //параметры ссылку на тот же самый объект T и введенное ранее имя
            //файла;
            // метод AddFromConsole();
            // статический метод Save(string filename, T obj).
            ResearchTeam.Load(filename + ".dat", researchTeam3);
            researchTeam3.AddFromConsole();
            ResearchTeam.Save(filename + ".dat", researchTeam3);

            //task 6
            //Вывести объект T.
            Console.WriteLine(researchTeam3);
        }

    }
}
