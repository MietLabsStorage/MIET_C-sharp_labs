using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace ResearchBase
{
    /// <summary>
    /// Class for compare timesearch list and dictionary
    /// </summary>
    /// <typeparam name="TKey">dictionary's key</typeparam>
    /// <typeparam name="TValue">dictionary's value</typeparam>
    class TestCollection<TKey, TValue>
    {
        private readonly List<TKey> listTKey;
        private readonly List<string> listString;
        private readonly Dictionary<TKey, TValue> dictTKey;
        private readonly Dictionary<string, TValue> dictString;
        private readonly GenerateElement<TKey, TValue> generateElement;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="n">count of objects in collection</param>
        /// <param name="generator">function-generator for elements in collection</param>
        public TestCollection(int n, GenerateElement<TKey, TValue> generator)
        {
            listTKey = new List<TKey>();
            listString = new List<string>();
            dictTKey = new Dictionary<TKey, TValue>();
            dictString = new Dictionary<string, TValue>();

            if (n <= 0 || n >= Int32.MaxValue)
            {
                throw new Exception("n in [0; Int32.MaxValue]");
            }
            else
            {
                generateElement = generator;
                for (int i = 0; i < n; i++)
                {
                    var obj = generateElement(i);
                    listTKey.Add(obj.Key);
                    listString.Add(obj.Key.ToString());
                    dictTKey.Add(obj.Key, obj.Value);
                    dictString.Add(obj.Key.ToString(), obj.Value);
                }
            }
        }

        /// <summary>
        /// search in list of keys
        /// </summary>
        public void SearchListTKey()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("List<TKey>::");
            var elem_first = listTKey[0];
            var elem_center = listTKey[listTKey.Count / 2];
            var elem_last = listTKey[listTKey.Count - 1];
            var elem_other = generateElement(listTKey.Count + 1).Key;

            sw.Restart();
            listTKey.Contains(elem_first);
            sw.Stop();
            Console.WriteLine("First:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            listTKey.Contains(elem_center);
            sw.Stop();
            Console.WriteLine("Center:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            listTKey.Contains(elem_last);
            sw.Stop();
            Console.WriteLine("Last:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            listTKey.Contains(elem_other);
            sw.Stop();
            Console.WriteLine("Other:: " + sw.ElapsedMilliseconds + "\n");
        }

        /// <summary>
        /// search in list of string
        /// </summary>
        public void SearchListString()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("List<String>::");
            var elem_first = listString[0];
            var elem_center = listString[listString.Count / 2];
            var elem_last = listString[listString.Count - 1];
            var elem_other = generateElement(listString.Count + 1).Key.ToString();

            sw.Restart();
            listString.Contains(elem_first);
            sw.Stop();
            Console.WriteLine("First:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            listString.Contains(elem_center);
            sw.Stop();
            Console.WriteLine("Center:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            listString.Contains(elem_last);
            sw.Stop();
            Console.WriteLine("Last:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            listString.Contains(elem_other);
            sw.Stop();
            Console.WriteLine("Other:: " + sw.ElapsedMilliseconds + "\n");
        }

        /// <summary>
        /// serach in dictionary by key
        /// </summary>
        public void SearchDictTkey()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Dictionary<TKay, TValue>::");
            var elem_first = dictTKey.ElementAt(0).Key;
            var elem_center = dictTKey.ElementAt(dictTKey.Count / 2).Key;
            var elem_last = dictTKey.ElementAt(dictTKey.Count - 1).Key;
            var elem_other = generateElement(dictTKey.Count + 1).Key;

            sw.Restart();
            dictTKey.ContainsKey(elem_first);
            sw.Stop();
            Console.WriteLine("First:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictTKey.ContainsKey(elem_center);
            sw.Stop();
            Console.WriteLine("Center:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictTKey.ContainsKey(elem_last);
            sw.Stop();
            Console.WriteLine("Last:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictTKey.ContainsKey(elem_other);
            sw.Stop();
            Console.WriteLine("Other:: " + sw.ElapsedMilliseconds + "\n");
        }

        /// <summary>
        /// serach in dictionary by string
        /// </summary>
        public void SearchDictString()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Dictionary<string, TValue>::");
            var elem_first = dictString.ElementAt(0).Key;
            var elem_center = dictString.ElementAt(dictString.Count / 2).Key;
            var elem_last = dictString.ElementAt(dictString.Count - 1).Key;
            var elem_other = generateElement(dictString.Count + 1).Key.ToString();

            sw.Restart();
            dictString.ContainsKey(elem_first);
            sw.Stop();
            Console.WriteLine("First:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictString.ContainsKey(elem_center);
            sw.Stop();
            Console.WriteLine("Center:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictString.ContainsKey(elem_last);
            sw.Stop();
            Console.WriteLine("Last:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictString.ContainsKey(elem_other);
            sw.Stop();
            Console.WriteLine("Other:: " + sw.ElapsedMilliseconds + "\n");
        }

        /// <summary>
        /// serach in dictionary by value
        /// </summary>
        public void SearchDictValue()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Dictionary<string, TValue>::");
            var elem_first = dictTKey.ElementAt(0).Value;
            var elem_center = dictTKey.ElementAt(dictTKey.Count / 2).Value;
            var elem_last = dictTKey.ElementAt(dictTKey.Count - 1).Value;
            var elem_other = generateElement(dictTKey.Count + 1).Value;

            sw.Restart();
            dictTKey.ContainsValue(elem_first);
            sw.Stop();
            Console.WriteLine("First:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictTKey.ContainsValue(elem_center);
            sw.Stop();
            Console.WriteLine("Center:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictTKey.ContainsValue(elem_last);
            sw.Stop();
            Console.WriteLine("Last:: " + sw.ElapsedMilliseconds);

            sw.Restart();
            dictTKey.ContainsValue(elem_other);
            sw.Stop();
            Console.WriteLine("Other:: " + sw.ElapsedMilliseconds + "\n");
        }

    }
}
