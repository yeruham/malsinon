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
        string query = "INSERT INTO people(first_name, last_name, secret_code, type, num_reports, num_mentions)" +
                "VALUES(@first_name, @last_name, @secret_code, @type, @num_reports, @num_mentions)";
        
        try
        {
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
        string query = "INSERT INTO intelreports(reporter_id, target_id, text)" +
               "VALUES(@reporter_id, @target_id, @text)";
        
        try
        {
            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@reporter_id", report.reporter_id);
            command.Parameters.AddWithValue("@target_id", report.target_id);
            command.Parameters.AddWithValue("@text", report.text);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
    }

    public void getPeople()
    {
        this.openConnection();
        string query = "SELECT * FROM people";

        try
        {
            command = new MySqlCommand(query, connection);
            //MySqlCommand 
            command.ExecuteReader();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
    }

    public void setReports()
    {

    }
    public void getPeopleByName()
    {

    }

}