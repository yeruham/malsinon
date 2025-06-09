using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malsinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Report report = new Report(1, 2, "yeruham", DateTime.Now);
            Console.WriteLine(report.timestamp.Minute);
        }
    }
}
