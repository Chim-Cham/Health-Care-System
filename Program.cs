using HCS;

// lister av IUsers som sparas temporärt.
List<Patient> patients = new();
List<Admin> admins = new();
List<Staff> staff = new();

//Alla file paths för lättare hantering. 
string AdminFilepath = Path.Combine("Data", "Admin.txt");
string PatientFilePath = Path.Combine("Data", "Patient.txt");
string StaffFilepath = Path.Combine("Data", "Staff.txt");
string JournalFilepath = Path.Combine("Data", "Journal.txt");
string LocationFilepath = Path.Combine("Data", "Location.txt");
string BookingFilepath = Path.Combine("Data", "Booking.txt");


// skapar en active user som är baserad på IUser men kallar den activa användarens roll.
IUser? active_user = null;
bool running = true;

//kallar metoden EnsurePath för alla 3 txt filer
Filemanage.EnsurePath(AdminFilepath, PatientFilePath, StaffFilepath, JournalFilepath, LocationFilepath);

// kallar metoden för att ladda alla användare. 
Filemanage.LoadUsers(AdminFilepath, PatientFilePath, StaffFilepath, admins, patients, staff);


// skaparr den första menyn som är i en loop
while (running)
{
    //ifall ingen är inloggad så körs denna menyn.
    if (active_user == null)
    {
        //meny valen! Välj med 1-3
        try { Console.Clear(); } catch { }
        Console.WriteLine("Health Care System, choose one of the options (1-3)");
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Register as a patient");
        Console.WriteLine("3. Quit");

        // matar in användarens val
        string menu1 = Console.ReadLine();

        // ifall log in väljs.
        if (menu1 == "1")
        {
            try { Console.Clear(); } catch { }
            Console.Write("Email:");
            string username = Console.ReadLine();

            try { Console.Clear(); } catch { }
            Console.Write("Password:");
            string password = Console.ReadLine();

            try { Console.Clear(); } catch { }
            foreach (Patient user in patients)
            {
                // Loppar igenom alla patienter i listan för patienter
                if (user.TryLogin(username, password))
                {
                    active_user = user;
                    break;
                }
            }
            if (active_user == null)
            {
                foreach (Admin user in admins)
                {
                // Loppar igenom alla Admins i listan för Admins                    
                    if (user.TryLogin(username, password))
                    {
                        active_user = user;
                        break;
                    }

                }
            }

            if (active_user == null)
            {
                foreach (Staff user in staff)
                {
                // Loppar igenom all personal i listan för personal
                    if (user.TryLogin(username, password))
                    {
                        active_user = user;
                        break;
                    }
                }
            }

            if (active_user == null) //om inlogg inte funkar kommmer fel meddelande
            {
                Console.WriteLine("No matching user, try again or create an account, press enter to go back");
                Console.ReadLine();
            }

        }

        //ifall create user väljs
        if (menu1 == "2")
        {
            //kallar på metod i Filemanager, class Filemanage och metod heter add patient, man kan bara lägga till patienter här 
            Filemanage.AddPatient.AddUser(PatientFilePath);

        }

        // ifall quit väljs
        if (menu1 == "3")
        {
            running = false;
        }

    }
    else //här är resten av programmet när user är inloggad
    {
        try { Console.Clear(); } catch { }

        // skapar en switch case som kallar på de olika meny metoderna för det olika användare som finns.
        switch (active_user.GetRole())
        {
            //Ifall active_user == Admin
            case Role.Admin:
                //Detta är en boolian på grund av att vi skulle kunna logga ut eller stänga programmet som specifik användare.
                //På höger sida om (=) är biten för att kalla på metoden för specifik menyn.
                // I det vi gör efter menyn är allt som vi måste hämta från Specifika .cs filer, med alla metoder som vi instansierar i de olika metoderna.
                bool adminLoggedOut = ((Admin)active_user).Menu(StaffFilepath, patients, Status.Pending, PatientFilePath, LocationFilepath, (Admin)active_user, admins, AdminFilepath);
                // Här hämtas ett return värde från de specifika menyerna för att logga ut.
                if (adminLoggedOut)
                {
                    active_user = null; // loggar ut admin och går till start menyn!
                }
                else
                {
                    running = false; // Quit
                }

                break;

            case Role.Staff:
                bool staffLoggedOut = ((Staff)active_user).Menu(patients, JournalFilepath, BookingFilepath);
                if (staffLoggedOut)
                {
                    active_user = null;
                }
                else
                {
                    running = false;
                }
                break;

            case Role.Patient:
                bool patientLoggedOut = ((Patient)active_user).Menu(staff, JournalFilepath, BookingFilepath);
                if (patientLoggedOut)
                {
                    active_user = null;
                }
                else
                {
                    running = false;
                }
                break;
        }
    }


}




