using System;

public class Report
{
    public int reporter_id { get; set; }
    public int target_id { get; set; }
    public string text { get; set; }
    public DateTime timestamp { get; set; }

    // constractor with timestamp
    public Report(int reporter_id, int target_id, string text, DateTime timestamp)
    {
        this.reporter_id = reporter_id;
        this.target_id = target_id;
        this.text = text;
        this.timestamp = timestamp;
    }

    // constractor without timestamp
    public Report(int reporter_id, int target_id, string text)
    {
        this.reporter_id = reporter_id;
        this.target_id = target_id;
        this.text = text;
    }

    public void printReport()
    {
        Console.WriteLine($"report by: {this.reporter_id}, target on {this.target_id}, time report: {this.timestamp}.\n" +
            $"text: {this.text}");
    }
}