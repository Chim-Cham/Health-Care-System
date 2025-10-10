using System.ComponentModel.Design;

namespace HCS;

interface IUser
{
    public bool TryLogin(string username, string password, Role role);
    public Role GetRole();

    public void Menu()
    {
        //detta är bara för att alla ska ha meny 
    }
}

enum Role
{
    None,
    Personnel,
    Patient,
    Admin,
}