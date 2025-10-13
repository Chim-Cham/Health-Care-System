namespace HCS;

class Patient : IUser
{
    public string Email;
    string _password;

    public string Journal;

    public Patient(string email, string password, string journal)
    {
        Email = email;
        _password = password;
        Journal = journal;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Email && password == _password;
    }

    public bool IsRole(Role role)
    {
        return Role.Patient == role;
    }

    public Role GetRole()
    {
        return Role.Patient;
    }
    public string ToFileString(string username, string password)
    {
        return $"{Email};{_password}";
    }


    public string ToFileJournal(string journal)
    {
        return $";{Journal}";
    }

    
}