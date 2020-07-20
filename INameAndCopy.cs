using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchBase
{
    /// <summary>
    /// interface INameAndCopy
    /// consist of:
    /// string Name { get; set; }
    /// object DeepCopy();
    /// </summary>
    interface INameAndCopy
    {
        string Name { get; set; }
        object DeepCopy();

    }
}
