namespace HCS;

interface IUser
{
    public bool TryLogin(string username, string password);
    public Role GetRole();
}

public enum Role
{
    None,
    Personnel,
    Patient,
    Admin,
}