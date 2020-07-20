using System;
using System.Diagnostics;

namespace ResearchBase
{
    class Lab1
    {
        /// <summary>
        /// done tasks from lab 1
        /// </summary>
        public static void Run()
        {
            //task 1
            //Создать один объект типа ResearchTeam, преобразовать данные в
            //текстовый вид с помощью метода ToShortString() и вывести данные
            ResearchTeam researchTeam = new ResearchTeam("C#", new Team("PIN-24", 100000), TimeFrame.Long);
            Console.WriteLine(researchTeam.ToShortString());


            //task 2
            //Вывести значения индексатора для значений индекса TimeFrame.Year,
            //TimeFrame.TwoYears, TimeFrame.Long
            Console.WriteLine("Year::     " + researchTeam[TimeFrame.Year]);
            Console.WriteLine("TwoYears:: " + researchTeam[TimeFrame.TwoYears]);
            Console.WriteLine("Long::     " + researchTeam[TimeFrame.Long]);

            //task 3
            //Присвоить значения всем определенным в типе ResearchTeam свойствам,
            //преобразовать данные в текстовый вид с помощью метода ToString() и
            //вывести данные.
            researchTeam.Theme = ".NET";
            researchTeam.Name = "MP-25A";
            researchTeam.RegisterNumber = 999999;
            researchTeam.TimeFrame = TimeFrame.TwoYears;
            Console.WriteLine(researchTeam.ToString());

            //task 4
            //С помощью метода AddPapers (params Paper []) добавить элементы в
            //список публикаций и вывести данные объекта ResearchTeam.
            researchTeam.AddPapers(new Paper(), new Paper("lab1", new Person(), new DateTime(2020, 7, 13)), new Paper());
            Console.WriteLine(researchTeam.ToString());

            //task 5
            //Вывести значение свойства, которое возвращает ссылку на публикацию с
            //самой поздней датой выхода;
            Console.WriteLine(researchTeam.LastPaper);

            //task 6
            //Сравнить время выполнения операций с элементами одномерного,
            //двумерного прямоугольного и двумерного ступенчатого массивов с
            //одинаковым числом элементов типа Paper.
            int row = 0, column = 0;
            while (true)
            {
                Console.WriteLine("Введите число столбцов и строк одной строкой без пробелов разделив их символом  '&', '^', ',', '_' или ' '");
                string[] size = Console.ReadLine().Split(new char[] { '&', '^', ',', '_', ' ' });

                if (Int32.TryParse(size[0], out row) && Int32.TryParse(size[1], out column))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nError: cant convert to int32\n");
                }
            }
            Console.Write("Строк:: "); Console.WriteLine(row);
            Console.Write("Столбцов:: "); Console.WriteLine(column);

            int size1d = row * column;
            int numberStepD = row * column;
            int i, j;

            Paper[] arr1 = new Paper[size1d]; 
            for (i = 0; i < size1d; i++)
            {
                arr1[i] = new Paper();
            }

            Paper[,] arr2 = new Paper[row, column]; 
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < column; j++)
                {
                    arr2[i, j] = new Paper();
                }
            }

            int h = Convert.ToInt32(Math.Round(Math.Sqrt(numberStepD / 2) * 2));
            //looks as:
            //*
            //**
            //***
            //etc
            Paper[][] arr3 = new Paper[h][];
            int k = 0; 
            for (i = 0; i < h; i++)
            {
                arr3[i] = new Paper[i + 1];
                for (j = 0; j <= i; j++)
                {
                    arr3[i][j] = new Paper();
                    k++;
                    if (k == numberStepD)
                    {
                        break;
                    }
                }
                if (k == numberStepD)
                {
                    break;
                }
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (i = 0; i < size1d; i++)
            {
                arr1[i].Title = "Publication ";
            }
            sw.Stop();
            Console.Write("Время выполнения для одномерного массива (в миллисекундах):: "); Console.WriteLine(sw.ElapsedMilliseconds);

            //двухмерный массив
            sw.Restart();
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < column; j++)
                {
                    arr2[i, j].Title = "Publication ";
                }
            }
            sw.Stop();
            Console.Write("Время выполнения для двухмерного массива (в миллисекундах):: "); Console.WriteLine(sw.ElapsedMilliseconds);

            //ступенчатый массив
            sw.Restart();
            for (i = 0; i < h; i++)
            {
                for (j = 0; j <= arr3[i].Length; j++)
                {
                    arr3[i][j].Title = "Publication ";
                }
            }
            sw.Stop();
            Console.Write("Время выполнения для ступечатого массива (в миллисекундах):: "); Console.WriteLine(sw.ElapsedMilliseconds);

            Console.ReadKey();

        }
    }
}
