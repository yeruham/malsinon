using MySqlConnector;
using System;
using System.Collections.Generic;

public class DALreports: DALMalshinon
{
    public bool addReport(Report report)
    {
        int success = 0;
        this.openConnection();
        string query = "INSERT INTO intelreports(reporter_id, target_id, text)" +
               "VALUES(@reporter_id, @target_id, @text)";

        try
        {
            command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@reporter_id", report.reporter_id);
            command.Parameters.AddWithValue("@target_id", report.target_id);
            command.Parameters.AddWithValue("@text", report.text);

            success = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        return (success > 0);
    }

    private List<Report> getReports(string query, Dictionary<string, string> parametrs = null)
    {
        List<Report> reports = new List<Report>();

        try
        {
            command = this.creatCommand(query, parametrs);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int reporter_id = reader.GetInt32("reporter_id");
                int target_id = reader.GetInt32("target_id");
                string text = reader.GetString("text");
                DateTime timestamp = reader.GetDateTime("timestamp");
                Report report = new Report(reporter_id, target_id, text, timestamp);
                report.printReport();
                reports.Add(report);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        return reports;
    }

    public List<Report> getAllReports()
    {
        List<Report> reports;
        string query = "SELECT * FROM intelreports";

        reports = this.getReports(query);
        
        return reports;
    }

    //public List<Report> getReportsByReporterId()
    //{

    //}

}