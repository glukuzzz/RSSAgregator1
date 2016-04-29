using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    interface INewsItem
    {
        DateTime time { get; }
        string source { get; }
        string text { get; }
        IEnumerable<object> objects { get; }
        IDictionary<int, object> objectPositions { get; }


    }
}
