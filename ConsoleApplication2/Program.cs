using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {

            Syndi synd = new Syndi();
            
            synd.AddResoure("Ria", "https://www.dailyfx.com/feeds/alerts");
            synd.BuildData();



        }
    }
}
