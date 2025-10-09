namespace HCS;

public class Patient : IUser
{
    public string Email;
    string _password;

    public Patient(string email, string password)
    {
        Email = email;
        _password = password;
    }
    public string ToFileString(string username, string password)
    {
        return $"{Email};{_password}";
    }
}