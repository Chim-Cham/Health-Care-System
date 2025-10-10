namespace HCS;

interface IUser
{
    public bool TryLogin(string username, string password, Role role);
    public Role GetRole();
}

enum Role
{
    None,
    Personnel,
    Patient,
    Admin,
}