namespace HCS;

// det har blivit ett felmeddelande här. IUser har som krav att varje användare ska ha tryLogin och Getrole 
public class Staff : IUser
{
    public string Username;
    string _password;

    public Staff(string username, string password)
    {
        Username = username;
        _password = password;
    }

    public Role GetRole()
    {
        return Role.Staff;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }


    //saves the staff data
    public string ToFileString(string username, string password)
    {
        return $"{Username};{_password}";
    }


    public static string ToJournalFile(Staff staffName, Patient name,string journal)
    {
        return $"{staffName};{name};{journal}";
    }

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

    public static void WriteJournal(string JournalFilepath, Staff active_user, Patient Pname, string Journal)
    {
        Console.WriteLine("Patient name: ");
        string Paname = Console.ReadLine();

        Staff activeuser = active_user;

        Console.WriteLine("Write in journal: ");
        string writing = Console.ReadLine();

        Staff.ToJournalFile(activeuser, Pname, Journal);
    }

    public void Menu()
    {
        bool runningPersonnel = true;

        while (runningPersonnel)
        {
            Console.Clear();
            Console.WriteLine("-----Healtcare-----");
            Console.WriteLine("1. Schedual");
            Console.WriteLine("2. View Journal");
            Console.WriteLine("3,5 Write Journal");
            Console.WriteLine("3. booking");
            Console.WriteLine("4. Logout");
            Console.WriteLine("5. Quit");

            switch (Console.ReadLine())
            {
                case "1":
                    break;

                case "2":

                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    break;
            }
        }
    }

}
