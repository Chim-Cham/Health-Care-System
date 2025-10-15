using HCS;

// List<IUser> users = new();
List<Patient> patients = new();
List<Admin> admins = new();
List<Staff> staff = new();


string AdminFilepath = Path.Combine("Data", "Admin.txt");
string PatientFilePath = Path.Combine("Data", "Patient.txt");
string StaffFilepath = Path.Combine("Data", "Staff.txt");
string JournalFilepath = Path.Combine("Data", "Journal.txt");
string LocationFilepath = Path.Combine("Data", "Location.txt");

string BookingFilepath = Path.Combine("Data", "Booking.txt");



IUser? active_user = null;
bool running = true;

//kallar metoden EnsurePath för alla 3 txt filer
Filemanage.EnsurePath(AdminFilepath, PatientFilePath, StaffFilepath, JournalFilepath, LocationFilepath);

Filemanage.LoadUsers(AdminFilepath, PatientFilePath, StaffFilepath, admins, patients, staff);


while (running)
{
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

        switch (active_user.GetRole())
        {
            case Role.Admin:
                bool adminLoggedOut = ((Admin)active_user).Menu(StaffFilepath, LocationFilepath);
                if (adminLoggedOut)
                {
                    active_user = null; // loggar ut admin och går till login
                }
                else
                {
                    running = false;
                }

                break;

            case Role.Staff:
                bool staffLoggedOut = ((Staff)active_user).Menu(patients, JournalFilepath);
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




