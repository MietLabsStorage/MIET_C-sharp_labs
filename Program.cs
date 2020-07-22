using System;
using System.Collections.Generic;

namespace ResearchBase
{
    enum TimeFrame { Year, TwoYears, Long};
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    delegate TKey KeySelector<TKey>(ResearchTeam rt);


    class Program
    {
        static void Main(string[] args)
        {
            //Lab1.Run();
            //Lab2.Run();
            Lab3.Run();
        }
    }
}
