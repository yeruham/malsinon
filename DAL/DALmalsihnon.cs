﻿using System;
using System.Collections.Generic;
using MySqlConnector;

public abstract class DALMalshinon
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

    protected void openConnection()
    {
        connection.Open();
    }

    protected void stopConnection()
    {
        connection.Close();
    }

}