using System.Globalization;

namespace HCS;

public class Admin : IUser
{
    public string Username;
    string _password;
    public Permission permission;

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

        // Visa patienter som har status Pending
        foreach (Patient patient in patients)
        {
            if (patient.status == Status.Pending)
            {
                Console.WriteLine(patient.Email + " " + patient.status);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Which registration would you like to accept or decline");
        string ChosingRegister = Console.ReadLine();

        //skapar variablen för att hitta rätt patient.
        Patient? selected_patient = null;

        // Leta upp patienten med foreach
        foreach (Patient patient in patients)
        {
            if (patient.Email == ChosingRegister)
            {
                selected_patient = patient;
                break;
            }
        }

        //ifall den inte hittas
        if (selected_patient == null)
        {
            Console.Clear();
            Console.WriteLine("No such user found: " + ChosingRegister);
            Console.WriteLine("");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        Console.Clear();
        Console.WriteLine("Accept or Decline: " + selected_patient.Email);
        Console.WriteLine("Type A or D: ");
        string AorD = Console.ReadLine();

        //skapar variablen för newstatus text assaingar den för mindre strul längre fram. 
        string newStatusText = "";

        //accept request
        if (AorD == "a" || AorD == "A")
        {
            selected_patient.status = Status.Accept;
            newStatusText = "Accepted";
        }

        //denied request
        else if (AorD == "d" || AorD == "D")
        {
            selected_patient.status = Status.Denied;
            newStatusText = "Denied";
        }

        // fel input
        else
        {
            Console.WriteLine("Invalid input.");
            Console.WriteLine("");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        // Skapa ny lista för att skriva uppdaterade rader
        List<string> updatedLines = new List<string>();

        // Läs filen, uppdatera raden med selected_patient email
        using (StreamReader reader = new StreamReader(PatientFilePath))
        {
            string line;
            //läser rader och hoppar över tomma rader.
            while ((line = reader.ReadLine()) != null)
            {
                //splittar 
                string[] parts = line.Split(';');

                // if part[0] namnet/emial är samma som selected_user email 
                if (parts[0] == selected_patient.Email)
                {
                    //gör raden till en lista
                    List<string> partsList = parts.ToList();

                    //ifall partlist.count är mindre än 2 gå vidare till nästa o 
                    while (partsList.Count <= 2)
                    {
                        partsList.Add("");
                    }
                    // bytar ut delen med pending mot A or D
                    partsList[2] = newStatusText;

                    //gör en ny lista och uppdaterar den raden med nya status.
                    string updatedLine = string.Join(";", partsList);
                    updatedLines.Add(updatedLine);
                }

                // för alla andra registrations så sparas den raden som den är. 
                else
                {
                    updatedLines.Add(line);
                }
            }
        }

        // Skriv tillbaka uppdaterad lista till filen
        using (StreamWriter writer = new StreamWriter(PatientFilePath, append: false))
        {
            foreach (string updatedLine in updatedLines)
            {
                writer.WriteLine(updatedLine);
            }
        }

        Console.WriteLine($"Patient {selected_patient.Email} has been {newStatusText}.");
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }


    public void addLocation(string LocationFilepath)
    {
        try { Console.Clear(); } catch { }
        Console.WriteLine("Write in which region: blekinge, halland, skåne, kronoberg?");
        string choosenRegion = Console.ReadLine();
        AllRegions selectedRegion = AllRegions.none;

        if (choosenRegion == "blekinge")
        {
            selectedRegion = AllRegions.Blekinge;
        }

        else if (choosenRegion == "halland")
        {
            selectedRegion = AllRegions.Halland;
        }

        else if (choosenRegion == "skåne")
        {
            selectedRegion = AllRegions.Skåne;
        }

        else if (choosenRegion == "kronoberg")
        {
            selectedRegion = AllRegions.Kronoberg;
        }

        else if (choosenRegion == null)
        {
            Console.WriteLine("Region was not found, press Enter to go back");
            Console.ReadLine();
        }

        Console.WriteLine("Write the name of the new reception");
        string newReception = Console.ReadLine();

        Location newLocation = new Location(newReception, selectedRegion);
        Console.WriteLine($"New reception: {newReception} was added in Region {selectedRegion}");
        Console.WriteLine("Press ENTER to go back to menu");
        Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(LocationFilepath, append: true))
        {
            writer.WriteLine(newLocation.ToFileString(newReception, selectedRegion));
        }
    }

    public void GivePermission(string AdminFilepath, Admin currentAdmin, List<Admin> admins)
    {
        try { Console.Clear(); } catch { }

        /* if (currentAdmin.permission != Permission.Master)
         {
             Console.WriteLine("You don't have permission to give permissions.");
             Console.WriteLine("Press ENTER to go back.");
             Console.ReadLine();
             return;
         }*/

        Console.WriteLine("-----Permissions-----");
        Console.WriteLine("1. Give permission for: Registrations");
        Console.WriteLine("2. Give permission for: Add Location");
        Console.WriteLine("3. Assign to region");
        string choice = Console.ReadLine();

        Console.WriteLine("Which Admin would you like to give this permission to?");

        // Lista admins
        foreach (Admin admin in admins)
        {
            if (admin.Username != currentAdmin.Username)
            {
                Console.WriteLine($"{admin.Username} (Current: {admin.permission})");
            }
        }
        string chosenUsername = Console.ReadLine();

        //skapar variabel selected admin.
        Admin? selectedAdmin = null;

        // letar igenom admins och fångar den som användaren är ute efter.
        foreach (Admin admin in admins)
        {
            if (admin.Username == chosenUsername)
            {
                selectedAdmin = admin;
                break;
            }
        }

        // felmeddelande för användaren
        if (selectedAdmin == null)
        {
            Console.WriteLine("Admin not found.");
            Console.WriteLine("Press ENTER to go back.");
            Console.ReadLine();
            return;
        }

        string newPermission = "";
        // Uppdatera permission
        switch (choice)
        {
            case "1":
                selectedAdmin.permission = Permission.Registration;
                newPermission = "Registration";
                break;
            case "2":
                selectedAdmin.permission = Permission.Location;
                newPermission = "Location";
                break;
            case "3":
                selectedAdmin.permission = Permission.AssaingRegion;
                newPermission = "AssaingRegion";
                break;
            default:
                Console.WriteLine("Invalid choice.");
                Console.ReadLine();
                return;
        }

        // Skriv tillbaka till filen
        List<string> updatedLines = new List<string>();
        using (StreamReader reader = new StreamReader(AdminFilepath))
        {
            string line;
            //läser rader och hoppar över tomma rader.
            while ((line = reader.ReadLine()) != null)
            {
                //splittar 
                string[] parts = line.Split(';');

                // if part[0] namnet/emial är samma som selected_user email 
                if (parts[0] == selectedAdmin.Username)
                {
                    //gör raden till en lista
                    List<string> partsList = parts.ToList();

                    int owncount = 0;

                    //ifall partlist.count är mindre än 2 gå vidare till nästa o 
                    while (partsList.Count <= 2 ||partsList.Count <= 3 || partsList.Count <= 4 || partsList.Count <= 5 || partsList.Count <= 6 )
                    {
                        partsList.Add("");
                        owncount++;
                    }
                    // detta är fel 
                    partsList[owncount++] = newPermission;

                    //gör en ny lista och uppdaterar den raden med nya status.
                    string updatedLine = string.Join(";", partsList);
                    updatedLines.Add(updatedLine);
                }
                Console.ReadLine();

                // fortfarande knasigt.
                using (StreamWriter writer = new StreamWriter(AdminFilepath, append: false))
                {
                    foreach (string lines in updatedLines)
                    {
                        writer.WriteLine(lines);
                    }
                }

                Console.WriteLine($"Permission updated for {selectedAdmin.Username}.");
                Console.WriteLine("Press ENTER to go back.");
                Console.ReadLine();
            }
        }
    }

    public enum Permission
    {
        None,
        Registration,
        AssaingRegion,
        Location,
        Permissions,
        Master
    }


    // denna kan man override för en annan meny för andra användare. 
    //kan va att man behöver ändra till ej static när andra punkter körs om man ska hämta variablar från program.cs
    public bool Menu(string StaffFilepath, List<Patient> patients, Status status, string PatientFilePath, string LocationFilepath, string AdminFilepath, Admin currentAdmin, List<Admin> admins)
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

            // lägga till locations / vi ser det som avdelningar
            Console.WriteLine("3. Adding locations [X]");
            Console.WriteLine("4. Registrations");
            Console.WriteLine("5. Create account - Staff");
            Console.WriteLine("6. List permissions [X]");
            Console.WriteLine("7. Log out");
            Console.WriteLine("8. Quit");

            switch (Console.ReadLine())
            {
                case "1":
                    break;

                case "2":
                    GivePermission(AdminFilepath, currentAdmin, admins);
                    break;

                case "3":
                    //add locations
                    addLocation(LocationFilepath);
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