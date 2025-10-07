namespace HCS;

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