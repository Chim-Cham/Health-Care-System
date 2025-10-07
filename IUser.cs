namespace HCS;

interface IUser
{
    public bool TryLogin(string username, string password, Role role);
    public Role GetRole();

    public NewUser();
}

enum Role
{
    None,
    Personnel,
    Patient,
    Admin,
}