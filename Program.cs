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
            //DALMalshinon malshinon = new DALMalshinon();
            //Person person = new Person("yoav", "ben arzi", "y", "reporter", 1, 0);
            //Person person = new Person("yeruham", "mendelson", "x", "target", 0, 1);
            //malshinon.addPeople(person);
            //Report report = new Report(4, 3, "he is very dangers");
            //malshinon.addReport(report);
            //malshinon.getPeopleByName("yeruham", "mendelson");
            //DALreports reports = new DALreports();
            //DALpeople people = new DALpeople();
            //reports.openConnection();
            //reports.getAllReports();
            //reports.getReportsByReporterId(4);
            //reports.stopConnection();

            //people.openConnection();
            //people.getPeopleByName("yeruham", "mendelson");
            //people.stopConnection();
            //people.openConnection();
            //people.getPeopleByType("reporter");
            //people.updateType(3, "both");
            //people.updateNumReports(3);
            //people.stopConnection();
            MalshinonManager manager = new MalshinonManager();
            manager.startProgram();

        }
    }
}
