namespace HCS;

// det har blivit ett felmeddelande här. IUser har som krav att varje användare ska ha tryLogin och Getrole 
class Personnel : IUser
{
    public string Username;
    string _password;

    public Personnel(string username, string password)
    {
        Username = username;
        _password = password;
    }
}