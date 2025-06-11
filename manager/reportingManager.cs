using System.Collections.Generic;
using System;

public class reportingManager
{
    protected DALpeople dalPeople = new DALpeople();
    protected DALreports dalReports = new DALreports();
    protected Analysiscec analysiscec;

    public reportingManager()
    {
        this.analysiscec = new Analysiscec(this.dalPeople, this.dalReports);
    }
    protected void reportManager()
    {
        string[] newReport;
        newReport = this.receivingReport();

        string[] reporter = newReport[0].Split(',');
        string[] target = newReport[2].Split(',');
        int reported_id = -1;
        int target_id = -1;
        
        string nameOrCodeReporter = this.nameOrCode(reporter);
        
        if (nameOrCodeReporter == "name")
        {
            reported_id = this.reportByName(reporter, "reporter");
        }
        else
        {
            reported_id = this.reportByCode(reporter, "reporter");
        }

        
        string nameOrCodeTarget = this.nameOrCode(target);
        
        if (nameOrCodeReporter == "name")
        {
            target_id = this.reportByName(target, "target");
        }
        else
        {
            target_id = this.reportByCode(target , "target");
        }

        this.analysiscec.PotentialAgent(reported_id);
        if (this.analysiscec.isDangerous(target_id))
        {
            Console.WriteLine("this target man is very dangerous!!");
        }

        if (reported_id != -1 && target_id != -1) 
        {
            Report report = new Report(reported_id, target_id, newReport[1]);
            dalReports.addReport(report); 
        }
    }

    private string[] receivingReport()
    {
        Console.WriteLine("Enter your full name in such a format - first name,last name " +
                         "(with a comma between them without profits) or your secrat code.");
        string reporter = Console.ReadLine();

        Console.WriteLine("Enter full name of target in such a format - first name,last name " +
            "(with a comma between them without profits) or his secret code.");
        string target = Console.ReadLine();

        Console.WriteLine("insert yuor information");
        string information = Console.ReadLine();

        string[] details = { reporter, information, target };
        return details;
    }

    private int reportByName(string[] details, string type)
    {
        int id = -1;
        List<Person> personExists;
        string firstName = details[0];
        string lastName = details[1];

        personExists = dalPeople.getPeopleByName(firstName, lastName);

        if (personExists.Count == 0)
        {
            string secretCode = Creation.createSecretCode(firstName, lastName);
            Person person = Creation.creatPerson(firstName, lastName, secretCode, type);
            dalPeople.addPerson(person);
            
            id = this.getPersonId(firstName, lastName);
        }
        else
        {
            id = personExists[0].id;

            if (type == "reporter")
            {
                dalPeople.updateNumReports(id);
            }
            else
            {
                dalPeople.updateNumMentions(id);
            }

            string currentType = personExists[0].type;
            if (currentType != type)
            {
                dalPeople.updateType(id, "both");
            }
        }
        return id;
    }

    private int reportByCode(string[] details, string type)
    {
        List<Person> personExists;
        string secretCode = details[0];
        int id = -1;
        
        personExists = dalPeople.getPeopleBySecretCode(secretCode);


        if (personExists.Count == 0)
        {
            Console.WriteLine("The code name does not exist in the system.");
        }
        else
        {
            id = personExists[0].id;

            if (type == "reporter")
            {
                dalPeople.updateNumReports(personExists[0].id);
            }
            else
            {
                dalPeople.updateNumMentions(personExists[0].id);
            }
        }
        return id;
    }

    private string nameOrCode(string[] personalDetails)
    {
        if (personalDetails.Length > 1)
        {
            return "name";
        }
        else
        {
            return "code";
        }
    }

    private int getPersonId(string firstName, string lastName)
    {
        int id = -1;
        List<Person> people = dalPeople.getPeopleByName(firstName, lastName);
        if (people.Count > 0)
        {
            id = people[0].id;
        }
        return id;
    }

}