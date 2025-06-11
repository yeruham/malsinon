public class Encryption
{
    public string encryptionMessage(string message)
    {
        string encryptedMessage = "";
        int HuskyValue = (int)'a' + (int)'z';
        foreach (char c in message)
        {
            if (char.IsLetter(c))
            {
                encryptedMessage += (char)(HuskyValue - (int)c);
            }
            else
            {
                encryptedMessage += c;
            }
        }
        return encryptedMessage;
    }

}

public class Deciphering
{
    public string decipheringMessage(string message)
    {
        string decodedMessage = "";
        int HuskyValue = (int)'a' + (int)'z';
        foreach (char c in message)
        {
            if (char.IsLetter(c))
            {
                decodedMessage += (char)(HuskyValue - (int)c);
            }
            else
            {
                decodedMessage += c;
            }
        }
        return decodedMessage;
    }

}