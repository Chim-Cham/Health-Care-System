namespace HCS;

// det har blivit ett felmeddelande här. IUser har som krav att varje användare ska ha tryLogin och Getrole 
public class Personnel : IUser
{
    public string Username;
    string _password;

    public Personnel(string username, string password)
    {
        Username = username;
        _password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }

    public Role GetRole()
    {
        return Role.Personnel;
    }
}