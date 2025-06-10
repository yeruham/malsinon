using MySqlConnector;
using System;
using System.Collections.Generic;

public class DALpeople: DALMalshinon
{
    public bool addPerson(Person person)
    {
        int success = 0;
        this.openConnection();
        string query = "INSERT INTO people(first_name, last_name, secret_code, type, num_reports, num_mentions)" +
                "VALUES(@first_name, @last_name, @secret_code, @type, @num_reports, @num_mentions)";

        Dictionary<string, string> parametrs = new Dictionary<string, string> { };

        try
        {
            command = new MySqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@first_name", person.firstName);
            command.Parameters.AddWithValue("@last_name", person.lastName);
            command.Parameters.AddWithValue("@secret_code", person.secretCode);
            command.Parameters.AddWithValue("@type", person.type);
            command.Parameters.AddWithValue("@num_reports", person.numReports);
            command.Parameters.AddWithValue("@num_mentions", person.numMentions);

            success = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        return (success > 0);
    }

    private List<Person> getPeople(MySqlCommand command)
    {
        List<Person> people = new List<Person>();

        try
        {
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
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        return people;
    }

    public List<Person> getAllPeople()
    {
        List<Person> people;
        string query = "SELECT * FROM people";
        try
        {
            this.command = new MySqlCommand(query, connection);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        people = this.getPeople(command);

        return people;

    }

    public List<Person> getPeopleByName(string firstName, string lastName)
    {
        List<Person> people;

        string query = "SELECT * FROM people WHERE first_name = @firstName AND last_name = @lastName";
        try
        {
            this.command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        people = this.getPeople(command);

        return people;
    }

    public List<Person> getPeopleBySecretCode(string secretCode)
    {
        List<Person> people;

        string query = "SELECT * FROM people WHERE secret_Code = @secretCode";
        try
        {
            this.command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@secretCode", secretCode);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        people = this.getPeople(command);
        
        return people;
    }

    public List<Person> getPeopleByType(string type)
    {
        List<Person> people;

        string query = "SELECT * FROM people WHERE type = @type";
        try
        {
            this.command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        people = this.getPeople(command);
        
        return people;
    }

    public bool updateNumReports(int reporter_id, int num = 1)
    {
        int success = 0;
        string query = "UPDATE people SET num_reports = num_reports + @num WHERE id = @reporter_id";
        try
        {
            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@num", num);
            command.Parameters.AddWithValue("@reporter_id", reporter_id);
            success = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        return (success > 0);
    }


    public bool updateNumMentions(int target_id, int num = 1)
    {
        int success = 0;
        string query = "UPDATE people SET num_mentions = num_mentions + @num WHERE id = @target_id";
        try
        {
            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@num", num);
            command.Parameters.AddWithValue("@target_id", target_id);
            success = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        return (success > 0);
    }



    public bool updateType(int id, string type)
    {
        int success = 0;
        string query = $"UPDATE people SET type = @type WHERE id = @id";
        try
        {
            command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@id", id);
            success = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        return (success > 0);
    }

}