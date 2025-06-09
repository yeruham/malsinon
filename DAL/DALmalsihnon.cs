using System;
using System.Collections.Generic;
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
            command = new MySqlCommand(query, this.connection);
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
            command = new MySqlCommand(query, this.connection);
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

    private List<Person> getPeople(MySqlCommand command)
    {
        List<Person> people = new List<Person>();

        try
        {
            //command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string firstName = reader.GetString("first_name");
                string lastName = reader.GetString("last_name");
                string secretCode = reader.GetString("secret_code");
                string type = reader.GetString("type");
                int numReports = reader.GetInt32("num_reports");
                int numMentions = reader.GetInt32("num_mentions");
                Person person = new Person(id, firstName, lastName, secretCode, type, numReports, numMentions);
                person.printPerson();
                people.Add(person);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        
        return people;
    }

    public List<Person> getAllPeople()
    {
        this.openConnection();
        List<Person> people = new List<Person>();
        string query = "SELECT * FROM people";
        command = new MySqlCommand(query, this.connection);
        
        people = this.getPeople(command);
        
        return people;

    }

    public void setReports()
    {

    }
    public List<Person> getPeopleByName(string firstName, string lastName)
    {
        this.openConnection();
        List<Person> people = new List<Person>();
        string query = "SELECT * FROM people WHERE first_name = @firstName AND last_name = @lastName";
        command = new MySqlCommand(query, this.connection);
        command.Parameters.AddWithValue("@firstName", firstName);
        command.Parameters.AddWithValue("@lastName", lastName);
        people = this.getPeople(command);
        
        return people;
    }

}