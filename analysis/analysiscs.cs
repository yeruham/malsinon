using System;
using System.Collections.Generic;

public class Analysiscec
{
    DALpeople dalPeople;
    DALreports dalReports;
    public Analysiscec(DALpeople dalPeople, DALreports dalReports)
    {
        this.dalPeople = dalPeople;
        this.dalReports = dalReports;
    }

    public bool PotentialAgent(int id)
    {

        List<Person> people;

        people = this.dalPeople.getPeopleById(id);

        if (people.Count == 0)
        {
            return false;
        }

        Person person;
        person = people[0];

        if (person.numReports < 10)
        {
            return false;
        }

        List<Report> reports;
        reports = this.dalReports.getReportsByReporterId(person.id);
        int averageLengthReports = this.averageLengthReports(reports);

        if (averageLengthReports < 100)
        {
            return false; 
        }

        this.dalPeople.updateType(person.id, "potential_agent");

        return true;

    }

    private int averageLengthReports(List<Report> reports)
    {
        int average = 0;
        int sum = 0;
        foreach (Report report in reports)
        {
            sum += report.text.Split().Length;
        }
        average = sum / reports.Count;
        
        return average;
    }

    public bool isDangerous(int id)
    {
        List<Person> people;

        people = this.dalPeople.getPeopleById(id);

        if (people.Count == 0)
        {
            return false;
        }

        Person person;
        person = people[0];

        if (person.numMentions < 3)
        {
            return false;
        }

        List<Report> reports;
        reports = this.dalReports.getReportsByReporterId(person.id);

        if (reports.Count > 3)
        {
            int timeGap = (int)(reports[0].timestamp - reports[2].timestamp).TotalMinutes;
            if (timeGap > 15)
            {
                return false;
            }
        }

        this.dalPeople.updateType(person.id, "dangerous");

        return true;
    }
}