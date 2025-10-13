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

    public bool TryLogin(string username, string password)
    {
        return username == Email && password == _password;
    }

    public Role GetRole()
    {
        return Role.Patient;
    }
    public string ToFileString(string username, string password)
    {
        return $"{Email};{_password}";
    }
}