using System.Globalization;

namespace HCS;

public class Admin : IUser
{
    public string Username;
    string _password;

    // get role för admin 



    public Admin(string username, string password)
    {
        Username = username;
        _password = password;
    }

    // denna override för Admin meny 

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }

    public Role GetRole()
    {
        return Role.Admin;
    }


    public void addStaff(string StaffFilepath)

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

    public void Registration(List<Patient> patients, Status status, string PatientFilePath)
    {
        try { Console.Clear(); } catch { }
        Console.WriteLine("Here is all pending registrations");

        // lopar igenom alla patienter som har status pending
        foreach (Patient patient in patients)
        {
            if (status == Status.Pending)
            {
                Console.WriteLine(patient.Email + " " + status);
            }
        }
        Console.WriteLine();
        Console.WriteLine("Which registration would you like to accept or decline");
        Console.WriteLine();
        string ChosingRegister = Console.ReadLine();

        // skapar variablen för att gämföra vem användaren söker 
        Patient? selected_patient = null;

        

        List<string> lines = new List<string>();
        Status newStatus;
        using (StreamReader reader = new StreamReader(PatientFilePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {

                //lopar igenom dem igen och ger selected_patient värdet för patienten.
                foreach (Patient patient in patients)
                {
                    if (ChosingRegister == patient.Email )
                    {
                        selected_patient = patient;
                        break;
                    }
                }
                string[] parts = line.Split(";");

                lines.Add(line);
            }
        }
        
        
        // här får man välja accept eller decline en registrering.
            try { Console.Clear(); } catch { }
            Console.WriteLine("Accept or Decline: " + selected_patient.Email);
            Console.WriteLine("Type A or D: ");
            string AorD = Console.ReadLine();

            if (AorD == "a" || AorD == "A")
            {
                // ändra status till Accepted.
                newStatus = Status.Accept;

            }

            else if (AorD == "d" || AorD == "D")
            {
                // ändra status till denied
                newStatus = Status.Denied;
            }
        // ifall den sökta patienten inte finns så kommer detta meddelandet. 
        if (selected_patient == null)
        {
            try { Console.Clear(); } catch { }
            Console.WriteLine("No such user found: " + ChosingRegister);
            Console.WriteLine("Try again");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        using (StreamWriter writer = new StreamWriter(PatientFilePath, append: false))
        {
            foreach (string newLine in lines)
            {
                writer.WriteLine(patients);
            }
        }
    }




    // denna kan man override för en annan meny för andra användare. 
    //kan va att man behöver ändra till ej static när andra punkter körs om man ska hämta variablar från program.cs
    public bool Menu(string StaffFilepath, List<Patient> patients, Status status , string PatientFilePath)
    {
        bool logout = false;
        bool runningAdmin = true;

        while (runningAdmin)
        {
            Console.Clear();
            Console.WriteLine("-----Healtcare-----");
            Console.WriteLine("1. Assaing Admin to region [X]");

            /// region/creating account for personnel/ location/ list permissions
            Console.WriteLine("2. Assaing permission for Admins [X]");

            // lägga till locations / ci ser det som avdelningar
            Console.WriteLine("3. Adding locations [X]");

            // Patients / Accept or deny
            Console.WriteLine("4. Registrations [X]");

            // Personnel / Admin 
            Console.WriteLine("5. Create account [X]");
            Console.WriteLine("6. List permissions [X]");
            Console.WriteLine("7. Log out [x]");
            Console.WriteLine("8. Quit [X]");

            switch (Console.ReadLine())
            {
                case "1":
                    break;

                case "2":
                    break;

                case "3":
                    break;

                case "4":
                // funderar på hur det ska kallas
                    Registration(patients, status, PatientFilePath);
                    break;

                case "5":
                    addStaff(StaffFilepath);
                    break;

                case "6":
                    break;

                case "7":
                    //logga ut 
                    logout = true;
                    runningAdmin = false;

                    break;

                case "8":
                    //avsluta programmet 
                    runningAdmin = false;
                    break;
            }
        }
        return logout;
    }

}