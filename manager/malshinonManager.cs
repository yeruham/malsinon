using System;
using System.Collections.Generic;

public class MalshinonManager
{
    private DALpeople dalPeople = new DALpeople();
    private DALreports dalReports = new DALreports();

    //public MalshinonManager()
    //{
    //    DALpeople dalPeople = new DALpeople();
    //    DALreports dalReports = new DALreports();
    //}
    public void startProgram()
    {
        string selection = "0";

        do
        {
            selection = this.basicMenu();

            switch (selection)
            {
                case "1":
                    this.startReporting();
                    break;
                case "2":
                    this.startManaging();
                    break;
            }

        } while (selection != "0");

        Console.WriteLine("The program has ended.");
    }

    private void startReporting()
    {
        string[] newReport;
        newReport = this.receivingReport();
        this.reportAnalysis(newReport);
    }

    private void startManaging()
    {
        string selection = "0";

        do
        {
            selection = this.menuManager();

            switch (selection)
            {
                case "1":
                    this.printPeople();
                    break;
                case "2":
                    this.printReports();
                    break;
            }
        }
        while (selection != "0");
    }


    private string basicMenu()
    {
        string menu = "To insert a new report enter 1.\n" +
                      "To access information enter 2\n" +
                      "To exit enter 0";

        Console.WriteLine(menu);
        string selectionNum = Console.ReadLine();
        return selectionNum;
    }

    private string menuManager()
    {
        string menu = "To show all people enter 1.\n" +
                      "To show all reports enter 2.\n" +
                      "To return to the main menu 0";
        Console.WriteLine(menu);
        string selectionNum = Console.ReadLine();
        return selectionNum;
    }

    private void printPeople()
    {
        List<Person> people = dalPeople.getAllPeople();
        foreach (Person person in people)
        {
            person.printPerson(); 
        }
    }
    
    private void printReports()
    {
        List<Report> reports = dalReports.getAllReports();
        foreach (Report report in reports)
        {
            report.printReport();
        }
    }

    public string[] receivingReport()
    {
        Console.WriteLine("Enter your full name in such a format - first name, last name " +
                         "(with a comma between them) or your secrat code.");
        string reporter = Console.ReadLine();
        
        Console.WriteLine("Enter full name of target in such a format - first name, last name " +
            "(with a comma between them) or his secret code.");
        string target = Console.ReadLine();
        
        Console.WriteLine("insert yuor information");
        string information = Console.ReadLine();

        string[] details = { reporter, information, target };
        return details;
    }

    public bool reportAnalysis(string[] details)
    {
        bool success = false;
        foreach (string s in details)
        {
            Console.WriteLine("------>" + s);
        }
        if (details.Length != 3)
        {
            return success;
        }else
        {
            string[] reporter = details[0].Split(',');
            string[] target = details[2].Split(',');
            List<Person> personExists = new List<Person>();
            string firstName = "";
            string lastName = "";
            string secretCode = "";
            Person newPerson;


            if (reporter.Length > 1)
            {
                firstName = reporter[0];
                lastName = reporter[1];
                Console.WriteLine(firstName + " " + lastName);
                personExists = dalPeople.getPeopleByName(firstName, lastName);
            }
            else
            {
                secretCode = reporter[0];
                personExists = dalPeople.getPeopleBySecretCode(secretCode);
            }


            if (personExists.Count == 0)
            {
                newPerson = this.creatPerson(firstName, lastName, secretCode, "reporter");
                success = dalPeople.addPerson(newPerson);
            }
            else
            {
                success = dalPeople.updateNumReports(personExists[0].id);
            }


            if (target.Length > 1)
            {
                firstName = target[0];
                lastName = target[1];
                personExists = dalPeople.getPeopleByName(firstName, lastName);
            }
            else
            {
                secretCode = target[0];
                personExists = dalPeople.getPeopleBySecretCode(secretCode);
            }

            if (personExists.Count == 0)
            {
                newPerson = this.creatPerson(firstName, lastName, secretCode, "target");
                success = dalPeople.addPerson(newPerson);

            }
            else
            {
                success = dalPeople.updateNumReports(personExists[0].id);
            }
        }
        return success;
    }


    public Person creatPerson(string firtstName, string lastName, string secretCode, string type)
    {
        if (type == "reporter")
        {
            return new Person(firtstName, lastName, secretCode, type, 1, 0);
        }else
        {
            return new Person(firtstName, lastName, secretCode, type, 0, 1);
        }
    }


}