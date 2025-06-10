using System;
using System.Collections.Generic;
using MySqlConnector;

public  class DALMalshinon
{
    protected string ConnectionAddress = "server=localhost;user=root;password=;database=malsinon";
    protected MySqlConnection connection;
    protected MySqlCommand command;

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

    public void setReports()
    {

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

}