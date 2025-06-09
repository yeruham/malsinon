public class People
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string secretCode { get; set; }
    public string type { get; set; }
    public int numReports { get; set; }
    public int numMentions { get; set; }

    public People(int id, string firstName, string lastName, string secretCode, string type, int numReports, int numMentions)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.secretCode = secretCode;
        this.type = type;
        this.numReports = numReports;
        this.numMentions = numMentions;

    }
}