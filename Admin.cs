namespace HCS;

class Admin : IUser
{
    public string Username;
    string _password;

    public Admin(string username, string password)
    {
        Username = username;
        _password = password;
    }

    public string ToFileString()
    {
        return $"{Username};{_password}";
    }
}