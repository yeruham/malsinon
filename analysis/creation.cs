public static class Creation
{
    public static string createSecretCode(string firstName, string lastName)
    {
        string fullName = firstName + lastName;
        Encryption encription = new Encryption();
        string secretCode = encription.encryptionMessage(fullName);

        return secretCode;
    }

    public static Person creatPerson(string firtstName, string lastName, string secretCode, string type)
    {
        if (type == "reporter")
        {
            return new Person(firtstName, lastName, secretCode, type, 1, 0);
        }
        else
        {
            return new Person(firtstName, lastName, secretCode, type, 0, 1);
        }
    }

}