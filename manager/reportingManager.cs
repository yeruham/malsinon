using System.Collections.Generic;
using System;

public abstract class reportingManager
{
    protected DALpeople dalPeople = new DALpeople();
    protected DALreports dalReports = new DALreports();
    protected Analysiscec analysiscec = new Analysiscec();

    protected void startReportManager()
    {
        string[] newReport;
        newReport = this.receivingReport();
        this.reportAnalysis(newReport);
    }
    private string[] receivingReport()
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

    private bool reportAnalysis(string[] details)
    {
        bool success = false;

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
            secretCode = Creation.createSecretCode(firstName, lastName);
            personExists = dalPeople.getPeopleByName(firstName, lastName);
        }
        else
        {
            secretCode = reporter[0];
            personExists = dalPeople.getPeopleBySecretCode(secretCode);
        }


        if (personExists.Count == 0)
        {
            newPerson = Creation.creatPerson(firstName, lastName, secretCode, "reporter");
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
            secretCode = Creation.createSecretCode(firstName, lastName);
            personExists = dalPeople.getPeopleByName(firstName, lastName);
        }
        else
        {
            secretCode = target[0];
            personExists = dalPeople.getPeopleBySecretCode(secretCode);
        }

        if (personExists.Count == 0)
        {
            newPerson = Creation.creatPerson(firstName, lastName, secretCode, "target");
            success = dalPeople.addPerson(newPerson);

        }
        else
        {
            success = dalPeople.updateNumReports(personExists[0].id);
        }
        return success;
    }

}