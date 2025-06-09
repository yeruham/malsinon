using System;
using MySqlConnector;

public class DALMalshinon
{
    private string ConnectionAddress = "server=localhost;user=root;password=;database=malsinon";
    private MySqlConnection connection;
    private MySqlCommand command;

    public DALMalshinon()
    {
        this.startConnection(ConnectionAddress);
    }

    private void startConnection(string ConnectionAddress)
    {
        this.connection = new MySqlConnection(ConnectionAddress);
    }

    public void openConnection()
    {
        connection.Open();
    }

    public void stopConnection()
    {
        connection.Close();
    }

    public void addPeople(Person person)
    {
        this.openConnection();
        try
        {
            string query = "INSERT INTO people(first_name, last_name, secret_code, type, num_reports, num_mentions)" +
                           "VALUES(@first_name, @last_name, @secret_code, @type, @num_reports, @num_mentions)";

            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@first_name", person.firstName);
            command.Parameters.AddWithValue("@last_name", person.lastName);
            command.Parameters.AddWithValue("@secret_code", person.secretCode);
            command.Parameters.AddWithValue("@type", person.type);
            command.Parameters.AddWithValue("@num_reports", person.numReports);
            command.Parameters.AddWithValue("@num_mentions", person.numMentions);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

    }

    public void addReport(Report report)
    {
        this.openConnection();
        try
        {
            string query = "INSERT INTO report(reported_id, target_id, text, timestamp)" +
                           "VALUES(@reported_id, @target_id, @text, @timestamp)";

            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@reported_id", report.reported_id);
            command.Parameters.AddWithValue("@target_id", report.target_id);
            command.Parameters.AddWithValue("@text", report.text);
            command.Parameters.AddWithValue("@timestamp", report.timestamp);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
    }


    public void getPeople()
    {

    }

    public void setReports()
    {

    }
    public void getPeopleByName()
    {

    }

}