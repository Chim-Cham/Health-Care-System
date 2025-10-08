using HCS;

string AdminFilepath = Path.Combine("Data", "Admin.txt");
string PatientFilePath = Path.Combine("Data", "Patient.txt");
string PersonnelFilepath = Path.Combine("Data", "Persnoal.txt");

IUser? active_user = null;
bool running = true;

//kallar metoden EnsurePath för alla 3 txt filer
Filemanage.EnsurePath(AdminFilepath, PatientFilePath, PersonnelFilepath);

// skapar första menyn ifall active_user == false. 
if (active_user == null)
{
    while (running)
    {
        //meny valen! Välj med 1-3
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Create user");
        Console.WriteLine("3. Quit");

        // matar in användarens val
        string menu1 = Console.ReadLine();

        // ifall log in väljs.
        if (menu1 == "1")
        {
            Console.WriteLine("Email:");
            string name = Console.ReadLine();

            Console.WriteLine("Password:");
            string passwrod = Console.ReadLine();

            //lägga till log in 

        }

        //ifall create user väljs
        if (menu1 == "2")
        {
            Console.WriteLine("Email:");
            string name = Console.ReadLine();

            Console.WriteLine("Password:");
            string passwrod = Console.ReadLine();

            //lägga till create

        }

        // ifall quit väljs
        if (menu1 == "3")
        {
            //avsluta programmet. 
        }
    }
}


