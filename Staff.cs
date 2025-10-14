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


    public static string ToJournalFile(Staff staffName, Patient name, string journal)
    {
        return $"{staffName};{name};{journal}";
    }


    // ifall denna är static så går det inte att hämta Username därför har jag den bara som void. 
    public void WriteJournal(string JournalFilepath, List<Patient> patients)
    {

        Console.WriteLine("Patient nameID: ");
        string email = Console.ReadLine();

        Patient patient = null;

        foreach (Patient patient1 in patients)
        {
            if (patient1.Email == email)
            {
                patient = patient1;
                break;
            }
        }
        if (patient == null)
        {
            Console.WriteLine("No Patient found, Try again");
            Console.WriteLine("");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Write in journal: ");
        string writing = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(JournalFilepath, append: true))
        {
            writer.WriteLine($"{Username};{patient.Email};{writing}");
        }
    }

    public bool Menu(List<Patient> patient, string JournalFilepath)
    {
        bool runningStaff = true;
        bool logout = false;


        while (runningStaff)
        {
            Console.Clear();
            Console.WriteLine("-----Healtcare-----");
            Console.WriteLine("1. Schedual");
            Console.WriteLine("2. View Journal");
            Console.WriteLine("3 Write Journal");
            Console.WriteLine("4. booking");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Quit");

            switch (Console.ReadLine())
            {
                case "1":
                    break;

                case "2":

                    break;

                case "3":
                    // ifall menyn är static så går inte denna metoden att hämtas
                    WriteJournal(JournalFilepath, patient);
                    break;

                case "4":
                    break;

                case "5":
                    //logga ut 
                    logout = true;
                    runningStaff = false;
                    break;

                case "6":
                    //avslutar programmet 
                    runningStaff = false;
                    break;
            }
        }
        return logout;
    }

}
