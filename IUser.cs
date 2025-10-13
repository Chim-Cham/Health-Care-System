using System.ComponentModel.Design;

namespace HCS;

interface IUser
{
    public bool TryLogin(string username, string password);
    public Role GetRole();

    public void Menu()
    {
        //detta är bara för att alla ska ha meny 
    }
}

public enum Role
{
    None,
    Staff,
    Patient,
    Admin,
}