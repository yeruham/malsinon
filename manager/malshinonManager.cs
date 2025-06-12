using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

public class MalshinonManager : reportingManager
{
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

        Console.WriteLine("\nThe program has ended.");
    }

    private string basicMenu()
    {
        string menu = "\nTo insert a new report enter 1.\n" +
                      "To access information enter 2\n" +
                      "To exit enter 0";

        Console.WriteLine(menu);
        string selectionNum = Console.ReadLine();
        return selectionNum;
    }

    private void startReporting()
    {
        this.reportManager();
    }

    private void startManaging()
    {
        bool isAllowed;
        isAllowed = this.passwordManager();
        if (!isAllowed)
        {
            return;
        }

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
                    this.getReports();
                    break;
                case "3":
                    this.getReportsByName("target");
                    break;
                case "4":
                    this.getReportsBySecretCode("target");
                    break;
                case "5":
                    this.getReportsByName("reporter");
                    break;
                case "6":
                    this.getReportsBySecretCode("reporter");
                    break;
                case "7":
                    this.printPotentialAgents();
                    break;
            }
        }
        while (selection != "0");
    }


    private string menuManager()
    {
        string menu = "\nTo show all people enter 1.\n" +
                      "To show all reports enter 2.\n" +
                      "To show reports by target name enter 3\n" +
                      "To show reports by target secreat_code entre 4\n" +
                      "To show reports by reporter name enter 5\n" +
                      "To show reports by reporter secreat_code entre 6\n" +
                      "To show all potential agents enter 7\n" +
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
    
    private void getReports()
    {
        List<Report> reports = dalReports.getAllReports();
        this.printReports(reports);
    }

    private void getReportsByName(string type)
    {
        Console.WriteLine("\nenter his first name");
        string firstName = Console.ReadLine();
        Console.WriteLine("\nenter his last name");
        string lastName = Console.ReadLine();

        List<Person> people = dalPeople.getPeopleByName(firstName, lastName);
        try
        {
            int id = people[0].id;
            this.printReportsByPersonId(type, id);
        }
        catch
        {
            Console.WriteLine("\nthis person not exit\n");
        }

    }

    private void getReportsBySecretCode(string type)
    {
        Console.WriteLine("\nenter his secret_code");
        string secretCode = Console.ReadLine();
        List<Person> people = dalPeople.getPeopleBySecretCode(secretCode);
        try
        {
            int id = people[0].id;
            this.printReportsByPersonId(type, id);
        }
        catch
        {
            Console.WriteLine("\nthis person not exit\n");
        }

    }

    private void printReportsByPersonId(string type, int id)
    {
        List<Report> reports;
        if (type == "reporter")
        {
            reports = dalReports.getReportsByReporterId(id);
        }
        else
        {
            reports = dalReports.getReportsByTargetId(id);
        }

        this.printReports(reports);
    }

    private void printReports(List<Report> reports)
    {
        foreach (Report report in reports)
        {
            report.printReport();
        }
    }

    private void printPotentialAgents()
    {
        List<Person> people = dalPeople.getPeopleByType("potential_agent");
        foreach (Person person in people)
        {
            person.printPerson();
        }
        if (people.Count == 0)
        {
            Console.WriteLine("\nNo potential agents\n");
        }
    }


    private bool passwordManager()
    {
        bool isAllowed;
        string password = this.receivePassword();
        isAllowed = this.passwordCheck(password);
        if (!isAllowed)
        {
            Console.WriteLine("\nThe password does not match. Entry denied\n");
        }
        return isAllowed;
    }
    private string receivePassword()
    {
        Console.WriteLine("\nEnter your password");
        string password = Console.ReadLine();
        return password;
    }

    private bool passwordCheck(string password)
    {
        string passwordSaved = "1234";
        if (password == passwordSaved)
        {
            return true;
        }

        return false;
    }
}