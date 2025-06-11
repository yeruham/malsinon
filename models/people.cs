using System;

public class Person
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string secretCode { get; set; }
    public string type { get; set; }
    public int numReports { get; set; }
    public int numMentions { get; set; }

    // consteactor with id 
    public Person(int id, string firstName, string lastName, string secretCode, string type, int numReports, int numMentions)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.secretCode = secretCode;
        this.type = type;
        this.numReports = numReports;
        this.numMentions = numMentions;
    }

    // constractor without id 
    public Person(string firstName, string lastName, string secretCode, string type, int numReports, int numMentions)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.secretCode = secretCode;
        this.type = type;
        this.numReports = numReports;
        this.numMentions = numMentions;
        Console.WriteLine($"new person {this.secretCode} Created successfully");
    }

    public void printPerson()
    {
        Console.WriteLine($"person id: {this.id}, full name: {this.firstName} {this.lastName}, secret code {this.secretCode}." +
            $"\ntype: {this.type}, num reports: {this.numReports} num mentions: {this.numMentions}.\n");
    }
}