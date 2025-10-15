namespace HCS;

public class Patient : IUser
{
    public string Email;
    string _password;

    public Patient(string email, string password)
    {
        Email = email;
        _password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Email && password == _password;
    }

    public Role GetRole()
    {
        return Role.Patient;
    }
    public string ToFileString(string username, string password)
    {
        return $"{Email};{_password}";
    }


    public bool Menu(List<Staff> doctors,string JournalFilepath, string BookingFilepath)
    {
        bool runningPatient = true;
        bool logout = false;

        while (runningPatient)
        {
            Console.Clear();
            Console.WriteLine("-----Healtcare-----");
            Console.WriteLine("1. Scheduale");
            Console.WriteLine("2. Journal");
            Console.WriteLine("3. Booking");
            Console.WriteLine("4. Log out");
            Console.WriteLine("5. Quit");

            switch (Console.ReadLine())
            {
                case "1":
                    break;

                case "2":
                    Filemanage.fetchJournal(Email, JournalFilepath);
                    Console.ReadLine();
                    break;

                case "3":
                    Filemanage.ReqBooking(doctors, Email, BookingFilepath);
                    break;

                case "4":
                    //loggar ut 
                    logout = true;
                    runningPatient = false;
                    break;

                case "5":
                    //avslutar programmet
                    runningPatient = false;
                    break;
            }
        }
        return logout;
    }

}