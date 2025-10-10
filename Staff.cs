namespace HCS;

// det har blivit ett felmeddelande här. IUser har som krav att varje användare ska ha tryLogin och Getrole 
class Staff : IUser
{
    public string Username;
    string _password;

    public Staff(string username, string password)
    {
        Username = username;
        _password = password;
    }

    //saves the staff data

    public string ToFileString(string username, string password)
    {
        return $"{Username};{_password}";
    }
}