using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ResearchBase
{
    enum TimeFrame { Year, TwoYears, Long};
    enum Revision { Remove, Replace, Property};
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    delegate TKey KeySelector<TKey>(ResearchTeam rt);
    delegate void PropertyChangedEventHandler(Object sender, PropertyChangedEventArgs e);
    delegate void ResearchTeamsChangedHandler<TKey>(object source, ResearchTeamsChangedEventArgs<TKey> args);


    class Program
    {
        static void Main(string[] args)
        {
            //Lab1.Run();
            //Lab2.Run();
            //Lab3.Run();
            //Lab4.Run();
            Lab5.Run();
        }
    }
}
