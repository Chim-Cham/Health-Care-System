using System.Security.Cryptography.X509Certificates;

namespace HCS;

public class Filemanage
{
    public static void EnsurePath(string AdminFilepath, string PatientFilePath, string StaffFilepath, string JournalFilepath, string LocationFilepath)
    {
        string directoryAdmin = Path.GetDirectoryName(AdminFilepath);
        string directoryPatient = Path.GetDirectoryName(PatientFilePath);
        string directoryStaff = Path.GetDirectoryName(StaffFilepath);
        string directoryJournal = Path.GetDirectoryName(JournalFilepath);
        string directoryLocation = Path.GetDirectoryName(LocationFilepath);

        // kontorllerar att filen Admin.txt finns
        if (!string.IsNullOrEmpty(directoryAdmin))
        {
            //ifall filen inte finns så skapas den filen
            if (!Directory.Exists(directoryAdmin))
            {
                Directory.CreateDirectory(directoryAdmin);
                Console.WriteLine($"Created file:  {directoryAdmin}");
            }
        }

        // kontorllerar att filen Patient.txt finns
        if (!string.IsNullOrEmpty(directoryPatient))
        {
            //ifall filen inte finns så skapas den filen
            if (!Directory.Exists(directoryPatient))
            {
                Directory.CreateDirectory(directoryPatient);
                Console.WriteLine($"Created file: {directoryPatient}");
            }
        }

        // kontorllerar att filen Staff.txt finns
        if (!string.IsNullOrEmpty(directoryStaff))
        {
            //ifall filen inte finns så skapas den filen
            if (!Directory.Exists(directoryStaff))
            {
                Directory.CreateDirectory(directoryStaff);
                Console.WriteLine($"Created file: {directoryStaff}");
            }
        }

        if (!string.IsNullOrEmpty(directoryJournal))
        {
            //ifall filen inte finns så skapas den filen
            if (!Directory.Exists(directoryJournal))
            {
                Directory.CreateDirectory(directoryJournal);
                Console.WriteLine($"Created file: {directoryJournal}");
            }
        }

        if (!string.IsNullOrEmpty(directoryLocation))
        {
            //ifall filen inte finns så skapas den filen
            if (!Directory.Exists(directoryLocation))
            {
                Directory.CreateDirectory(directoryLocation);
                Console.WriteLine($"Created file: {directoryLocation}");
            }
        }
    }



    //skapar en klass för Read som läser 
    class ReadUser
    {
        string line;

        //Läser in från filen anändarnamn och lösenord och splitar så vi kan använda detta när vi ska ladda alla users. 
        public static Admin FromFileToStringAdmin(string line)
        {
            string[] adminParts = line.Split(";");
            return new Admin(adminParts[0], adminParts[1]);
        }

        public static Patient FromFileToStringPatient(string line)
        {
            string[] patientParts = line.Split(";");
            return new Patient(patientParts[0], patientParts[1]);
        }

        public static Staff FromFileToStringStaff(string line)
        {
            string[] staffParts = line.Split(";");
            return new Staff(staffParts[0], staffParts[1]);
        }
    }


    //skapar en class för att lägga till användare 
    public class AddPatient
    {


        //Här har jag gjort så att man lägger till en användare som är admin men vet inte hur jag ska ta mig till väga härifrån

        //endast metod för att lägga till patient
        public static void AddUser(string PatientFilePath)
        {

            // Ber användaren skriva in email
            Console.WriteLine("-----Creating account-----\n");
            Console.Write("Email: ");
            // Läser email från konsolen
            string email = Console.ReadLine();

            // Ber användaren skriva in lösenord
            Console.Write("Password: ");
            // Läser lösenord från konsolen
            string password = Console.ReadLine();

            // Skapar ett nytt User-objekt med den inmatade datan
            Patient newPatient = new Patient(email, password);

            // Öppnar filen för att lägga till text i slutet
            using (StreamWriter writer = new StreamWriter(PatientFilePath, append: true))
            {
                // Hämtar metoden för att skriva in användaren i filen.
                writer.WriteLine(newPatient.ToFileString(email, password));
            }

            // Bekräftar att användaren sparades
            Console.WriteLine($"User '{email}' have been added!, press ENTER to go back to login");
            Console.ReadLine();
        }
    }

    //Loadusers hämtar datan från textfilerna och laddar de innan programmet startar. sen kallas metoden i program.cs
    public static void LoadUsers(string AdminFilepath, string PatientFilePath, string StaffFilepath, List<Admin> admins, List<Patient> patients, List<Staff> staff)
    {
        //laddar alla admins inlogg 
        if (File.Exists(AdminFilepath))
        {
            string[] lines = File.ReadAllLines(AdminFilepath);

            foreach (string line in lines)
            {
                if (line != "") //ignorera tomma rader 
                {
                    Admin admin = ReadUser.FromFileToStringAdmin(line); //hämtar metoden ReadUser som splittar alla strings 
                    admins.Add(admin);
                }
            }

        }

        //laddar alla patienters inlogg 
        if (File.Exists(PatientFilePath))
        {
            string[] lines = File.ReadAllLines(PatientFilePath);
            foreach (string line in lines)
            {
                if (line != "") //ignorera tomma rader 
                {
                    Patient patient = ReadUser.FromFileToStringPatient(line); //hämtar metod från ReadUser
                    patients.Add(patient);

                }
            }

        }

        //laddar alla personnel inlogg så de kan matcha när man loggar in 
        if (File.Exists(StaffFilepath))
        {
            string[] lines = File.ReadAllLines(StaffFilepath);
            foreach (string line in lines)
            {
                if (line != "") //ignorera tomma rader 
                {
                    Staff p = ReadUser.FromFileToStringStaff(line); //hämtar metod ReadUser
                    staff.Add(p);
                }

            }

        }
    }

    public static void fetchJournal(string user, string JournalFilepath)
    {
        string[] readJournal = File.ReadAllLines(JournalFilepath);
        foreach (string line in readJournal)
        {
            if (line != "")
            {
                string[] lineArray = line.Split(";");
                if (lineArray[1] == user)
                {
                    System.Console.WriteLine($"Doctor: {lineArray[0]}");
                    System.Console.WriteLine($"Patient: {lineArray[1]}");
                    System.Console.WriteLine($"Notes: {lineArray[2]}");
                }
            }
        }
    }

    public static void loadLocation(Location location, string LocationFilepath)
    {
        string[] lines = File.ReadAllLines(LocationFilepath);
        foreach (string line in lines)
        {
            if (line != "")
            {
                string[] locationParts = line.Split(";");
            }
        }

    }

}

