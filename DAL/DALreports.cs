using MySqlConnector;
using System;
using System.Collections.Generic;

public class DALreports: DALMalshinon
{
    // function to insert new Report to intelreport_table - receiving Report object and return bool if success.
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

    // private function to get lines from intelreport_table
    // receiving MySqlCommand object and return list with lines that compatible to query.
    private List<Report> getReports(MySqlCommand command)
    {
        List<Report> reports = new List<Report>();

        try
        {
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

    // function to get all lines from intelreport_table 
    // return list with objects of Report. use in private function - getReports.
    public List<Report> getAllReports()
    {
        List<Report> reports;
        string query = "SELECT * FROM intelreports";
        try
        {
            this.command = new MySqlCommand(query, connection);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        reports = this.getReports(command);

        return reports;
    }

    // function to get lines from intelreport_table by reported_id
    // receiving  reported_id, and return list with objects of Report. use in private function - getPeople.
    public List<Report> getReportsByReporterId(int reporterId)
    {
        List<Report> reports;
        string query = "SELECT * FROM intelreports WHERE reporter_id = @reporterId";
        try
        {
            this.command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue(" @reporterId", reporterId);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        reports = this.getReports(command);

        return reports;
    }

    // function to get lines from intelreport_table by target_id
    // receiving  target_id, and return list with objects of Report. use in private function - getPeople.
    public List<Report> getReportsByTargetId(int targetId)
    {
        List<Report> reports;
        string query = "SELECT * FROM intelreports WHERE target_id = @targetId";
        try
        {
            this.command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue(" @targetId", targetId);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        reports = this.getReports(command);

        return reports;
    }

}