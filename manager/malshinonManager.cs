using System;
using System.Collections.Generic;

public class MalshinonManager
{
    private DALpeople dalPeople;
    private DALreports dalReports;

    public MalshinonManager()
    {
        DALpeople dalPeople = new DALpeople();
        DALreports dalReports = new DALreports();
    }
    public void start()
    {
        dalPeople.openConnection();
        dalReports.openConnection();
        string selectionNum = "";
        string[] newReport;

        do
        {
            selectionNum = this.menu();

            switch (selectionNum)
            {
                case "1":
                    newReport = this.receivingReport();

                    break;
                case "2":
                    break;
                case "3":
                    break;
            }

        } while (selectionNum != "4");

        dalPeople.stopConnection();
        dalReports.stopConnection();
    }

    public string menu()
    {
        string menu = "To insert a new report enter 1.\n" +
                      "To show all people enter 2.\n" +
                      "To show all reports enter 3.\n" +
                      "To exit enter 4";

        Console.WriteLine(menu);
        string selectionNum = Console.ReadLine();
        return selectionNum;
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
        if (details.Length != 3)
        {
            return false;
        }else
        {
            string[] reporter = details[0].Split(',');
            string[] target = details[2].Split(',');
            List<Person> personExists = new List<Person>();
            string firstName = "";
            string lastName = "";
            string secretCode = "";


            if (reporter.Length > 1)
            {
                firstName = reporter[0];
                lastName = reporter[1];
                personExists = dalPeople.getPeopleByName(firstName, lastName);
            }
            else
            {
                secretCode = reporter[0];
                personExists = dalPeople.getPeopleBySecretCode(secretCode);
            }


            if (personExists.Count == 0)
            {
                this.creatPerson(firstName, lastName, secretCode, "reporter");
            }
            else
            {
                dalPeople.updateNumReports(personExists[0].id);
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
                this.creatPerson(firstName, lastName, secretCode, "target");
            }
            else
            {
                dalPeople.updateNumReports(personExists[0].id);
            }
        }
        return true;
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