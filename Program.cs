using HCS;

List<IUser> users = new();
List<Patient> patients = new();

string AdminFilepath = Path.Combine("Data", "Admin.txt");
string PatientFilePath = Path.Combine("Data", "Patient.txt");
string PersonnelFilepath = Path.Combine("Data", "Persnoal.txt");

//kallar metoden EnsurePath för alla 3 txt filer
Filemanage.EnsurePath(AdminFilepath, PatientFilePath, PersonnelFilepath);

IUser? active_user = null;
bool running = true;

while (running)
{
    if (active_user == null)
    {
        //meny valen! Välj med 1-3
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

            foreach (IUser user in users)
            {
                if (user.TryLogin(username, password))
                {
                    active_user = user;
                    break;
                }
            }
            // if (active_user == null)
            // {
            //     Console.WriteLine("No matching user, try again or create an account, press enter to go back");
            //     Console.ReadLine();
            // }
            // break;
        }

        //ifall create user väljs
        if (menu1 == "2")
        {
            try { Console.Clear(); } catch { }
            Console.WriteLine("Enter your email");
            string newEmail = Console.ReadLine();

            try { Console.Clear(); } catch { }
            Console.WriteLine("Create password");
            string newPassword = Console.ReadLine();

            patients.Add(new Patient(newEmail, newPassword));

            try { Console.Clear(); } catch { }
            Console.WriteLine("Account successfully registerd, press enter to log in");
            Console.ReadLine();

        }

        // ifall quit väljs
        if (menu1 == "3")
        {
            running = false;
        }

    }
    else //här är resten av programmet när user är inloggad
    {
        Console.WriteLine("Hej nu är du i main menu ");
        Console.ReadLine();
    }


}




