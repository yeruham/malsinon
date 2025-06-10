using MySqlConnector;
using System;
using System.Collections.Generic;

public class DALpeople: DALMalshinon
{
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

    private List<Person> getPeople(string query, Dictionary<string, string> parametrs = null)
    {
        List<Person> people = new List<Person>();

        try
        {
            command = this.creatCommand(query, parametrs);
            
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

    private MySqlCommand creatCommand(string query, Dictionary<string, string> parametrs = null)
    {
        this.openConnection();
        try
        {
            command = new MySqlCommand(query, this.connection);

            foreach (KeyValuePair<string, string> parmetr in parametrs)
            {
                command.Parameters.AddWithValue(parmetr.Key, parmetr.Value);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        return command;
    }

    public List<Person> getAllPeople()
    {
        List<Person> people = new List<Person>();
        string query = "SELECT * FROM people";

        people = this.getPeople(query);

        return people;

    }

    public List<Person> getPeopleByName(string firstName, string lastName)
    {
        List<Person> people = new List<Person>();
        string query = "SELECT * FROM people WHERE first_name = @firstName AND last_name = @lastName";

        Dictionary<string, string> parametrs = new Dictionary<string, string> { };
        parametrs.Add("@firstName", firstName);
        parametrs.Add("@lastName", lastName);

        people = this.getPeople(query, parametrs);

        return people;
    }

    public List<Person> getPeopleBySecretCode(string secretCode)
    {
        List<Person> people = new List<Person>();
        string query = "SELECT * FROM people WHERE secret_Code = @secretCode";
        Dictionary<string, string> parametrs = new Dictionary<string, string> { };
        parametrs.Add("@secretCode", secretCode);

        people = this.getPeople(query, parametrs);
        return people;
    }

    public List<Person> getPeopleByType(string type)
    {
        List<Person> people = new List<Person>();
        string query = "SELECT * FROM people WHERE type = @type";
        Dictionary<string, string> parametrs = new Dictionary<string, string> { };
        parametrs.Add("@type", type);

        people = this.getPeople(query, parametrs);
        return people;
    }

}