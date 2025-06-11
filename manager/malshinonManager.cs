using System;
using System.Collections.Generic;

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

        Console.WriteLine("The program has ended.");
    }

    private void startReporting()
    {
        this.reportManager();
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

}