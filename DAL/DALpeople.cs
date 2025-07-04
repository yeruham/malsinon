﻿using MySqlConnector;
using System;
using System.Collections.Generic;

public class DALpeople: DALMalshinon
{
    // function to insert new Person to people_table - receiving Person object and return bool if success.
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
        this.stopConnection();
        return (success > 0);
    }

    // private function to get lines from people_table
    // receiving MySqlCommand object and return list with lines that compatible to query.
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

    // function to get all lines from people_table
    // return list with objects of Person. use in private function - getPeople.
    public List<Person> getAllPeople()
    {
        List<Person> people;
        string query = "SELECT * FROM people";
        this.openConnection();

        try
        {
            this.command = new MySqlCommand(query, connection);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }
        
        people = this.getPeople(command);
        this.stopConnection();

        return people;

    }

    // function to get lines from people_table by id
    // receiving id, and return list with objects of Person. use in private function - getPeople.
    public List<Person> getPeopleById(int id)
    {
        List<Person> people;
        string query = "SELECT * FROM people WHERE id = @id";
        this.openConnection();

        try
        {
            this.command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.GetType().Name}. message: {e.Message}.");
        }

        people = this.getPeople(command);
        this.stopConnection();

        return people;
    }

    // function to get lines from people_table by full name
    // receiving first and last name, and return list with objects of Person. use in private function - getPeople.
    public List<Person> getPeopleByName(string firstName, string lastName)
    {
        List<Person> people;
        string query = "SELECT * FROM people WHERE first_name = @firstName AND last_name = @lastName";
        this.openConnection();

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
        this.stopConnection();

        return people;
    }

    // function to get lines from people_table by secretCode 
    // receiving secretCode, and return list with objects of Person. use in private function - getPeople.
    public List<Person> getPeopleBySecretCode(string secretCode)
    {
        List<Person> people;
        string query = "SELECT * FROM people WHERE secret_Code = @secretCode";
        this.openConnection();

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
        this.stopConnection();
        
        return people;
    }

    // function to get lines from people_table by type
    // receiving type, and return list with objects of Person. use in private function - getPeople.
    public List<Person> getPeopleByType(string type)
    {
        List<Person> people;
        string query = "SELECT * FROM people WHERE type = @type";
        this.openConnection();

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
        this.stopConnection();
        
        return people;
    }

    // function to change num reports in line of people_table by id
    // receiving id of people and num (defult 1), and return bool if success.
    public bool updateNumReports(int reporter_id, int num = 1)
    {
        int success = 0;
        string query = "UPDATE people SET num_reports = num_reports + @num WHERE id = @reporter_id";
        this.openConnection();

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
        this.stopConnection();

        return (success > 0);
    }

    // function to change num mentions in line of people_table by id
    // receiving id of people and num (defult 1), and return bool if success.
    public bool updateNumMentions(int target_id, int num = 1)
    {
        int success = 0;
        string query = "UPDATE people SET num_mentions = num_mentions + @num WHERE id = @target_id";
        this.openConnection();

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
        this.stopConnection();

        return (success > 0);
    }

    // function to change type in line of people_table by id
    // receiving id of people and num (defult 1), and return bool if success.
    public bool updateType(int id, string type)
    {
        int success = 0;
        string query = $"UPDATE people SET type = @type WHERE id = @id";
        this.openConnection();

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
        this.stopConnection();

        return (success > 0);
    }

}