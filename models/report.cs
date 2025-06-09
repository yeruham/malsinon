using System;

public class Report
{
    public int reported_id { get; set; }
    public int target_id { get; set; }
    public string text { get; set; }
    public DateTime timestamp { get; set; }

    public Report(int reported_id, int target_id, string text, DateTime timestamp)
    {
        this.reported_id = reported_id;
        this.target_id = target_id;
        this.text = text;
        this.timestamp = timestamp;
    }
}