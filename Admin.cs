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

// lägger till staff
    public static void addStaff(string StaffFilepath)
    {
        // Ber användaren skriva in email
        Console.WriteLine("Create Staff Username: ");
        string username = Console.ReadLine();

        // Ber användaren skriva in lösenord
        Console.WriteLine("Create Staff Password");
        string password = Console.ReadLine();

        // Skapar ett nytt staff member med den inmatade datan
        Staff newStaff = new Staff(username, password);

        // Öppnar filen för att lägga till text i slutet
        using (StreamWriter writer = new StreamWriter(StaffFilepath, append: true))
        {
            // Hämtar metoden för att skriva in användaren i filen.
            writer.WriteLine(newStaff.ToFileString(username, password));
        }
        // Bekräftar att användaren sparades
        Console.WriteLine($"User '{username}' have been added!");
        Console.ReadLine();
    }

}