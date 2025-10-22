
using Microsoft.VisualBasic;

namespace HCS;

public class Filemanage
{

    // metod för att säkerställa att filerna som används finns och ifall det inte stämmer så skapas de filerna.
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



    //skapar en klass för Read som läser användaren
    class ReadUser
    {
        string line;

        //Läser in från filen anändarnamn och lösenord och splitar så vi kan använda detta när vi ska ladda alla users. 
        public static Admin FromFileToStringAdmin(string line)
        {
            //skapar en aray där vi splitar hela line in i delar.
            string[] adminParts = line.Split(";");

            string username = adminParts[0];
            string password = adminParts[1];

            // splittar denna delen anorlunda för att det är en lista i Admin
            List<string> permissions = new List<string>();
            if (adminParts.Length > 3)
            {
                permissions = adminParts[3].Split(",").ToList();
            }

            // Returnerar dessa värden
            return new Admin(username, password, permissions);
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
                writer.WriteLine(newPatient.ToFileString(email, password, Status.Pending));
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

    // Method for fetching journal entries relating to the given user. Patients can only acces their own journal while doctors can access all available journals.
    public static void fetchJournal(string user, string JournalFilepath)
    {
        // Goes through "Journal.txt" and find the patient name matching the given username. information is saved in the format of <doctor>;<patient>;<notes>
        string[] readJournal = File.ReadAllLines(JournalFilepath);
        foreach (string line in readJournal)
        {
            if (line != "")
            {
                // The read lines get's split into parts at every given ";" in the string and then used to indentify the patient matching the journal
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

    // reqBooking är för Patientens point of view
    public static void ReqBooking(List<Staff> staff, string user, string BookingFilepath)
    {
        try { Console.Clear(); } catch{ }
        foreach (Staff staffer in staff)
        {
            System.Console.WriteLine($"{staffer.Username}");
        }
        System.Console.Write("What doctor do you wanna meet?: ");
        string staffSelect = Console.ReadLine();
        // kan vara otydligt men user experiance får lida pågrund av tidsbegränsning. 
        System.Console.Write("What time would you like to meet?(8-16): ");
        string time = Console.ReadLine();
        System.Console.Write("What month would you like to meet?(Jan-Dec): ");
        string month = Console.ReadLine();
        // felhantering fattas pågrund av samma sak som 4 rader upp.
        System.Console.Write("What day?(1-28): ");
        string day = Console.ReadLine();
        int.TryParse(time, out int timer);

        // skriver alla sparade värden på en rad i filen booking.txt
        using (StreamWriter writer = new StreamWriter(BookingFilepath, append: true))
        {
            writer.WriteLine($"{staffSelect};{user};{timer}:00;{timer + 1}:00;{month};{day};Pending");
        }
        System.Console.WriteLine("Appointment Requested! Press Enter to continue.");
        Console.ReadLine();
    }

    // regBooking är för staffs point of view 
    public static void RegBooking(List<Patient> patients, string user, string BookingFilepath)
    {
        try { Console.Clear(); } catch { }
        foreach (Patient patient in patients)
        {
            System.Console.WriteLine($"{patient.Email}");
        }
        System.Console.Write("What patient do you wanna meet?: ");
        string patientSelect = Console.ReadLine();
        System.Console.Write("What time would you like to meet?(8-16): ");
        string time = Console.ReadLine();
        System.Console.Write("What month would you like to meet?(Jan-Dec): ");
        string month = Console.ReadLine();
        System.Console.Write("What day?(1-28): ");
        string day = Console.ReadLine();
        int.TryParse(time, out int timer);
        using (StreamWriter writer = new StreamWriter(BookingFilepath, append: true))
        {
            writer.WriteLine($"{user};{patientSelect};{timer}:00;{timer + 1}:00;{month};{day};Accepted");
        }
        System.Console.WriteLine("Appointment Registered! Press Enter to continue.");
        Console.ReadLine();
    }
    // funktion för staff att kunna acceptera eller neka bookings som har lagts till i systemet
    public static void HandleBooking(List<Patient> patients, string user, string BookingFilepath)
    {
        string[] lines = File.ReadAllLines(BookingFilepath);
        string[] lineArray = new string[lines.Count()];
        string[] lineSplit = new string[0];
        // kollar igenom "Booking.txt" för alla bookings som har status "pending" och läger till dem in lineArray
        int i = 0;
        int e = 1;
        for (i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("Pending"))
            {
                lineArray[e] = lines[i];
                e++;
            }
        }
        // skriver ut alla bookings som hittades och lades till i lineArray
        i = 0;
        foreach (string line1 in lineArray)
        {
            if (line1 != null)
            {
                string[] line1Split = line1.Split(";");
                System.Console.WriteLine($"{i}.>Doctor:{line1Split[0]}");
                System.Console.WriteLine($"    >Patient:{line1Split[1]}");
                System.Console.WriteLine($"    >Time:{line1Split[2]}-{line1Split[3]}");
                System.Console.WriteLine($"    >Date:{line1Split[4]} {line1Split[5]}");
                System.Console.WriteLine($"    >Status:{line1Split[6]}");
                System.Console.WriteLine();
            }
            i++;
        }
        // användare anger vilket booking man vill acceptera eller neka, tack vare att vi la till 
        // alla bookings med pending i si egen array så blir det ett enkelt ange 1-5 exempel
        System.Console.WriteLine("What bookings do you wanna respond to?");
        string lineSelect = Console.ReadLine();
        int.TryParse(lineSelect, out int lineNumber);
        // användare ger anger sedan om man vill acceptera eller neka bookingen
        System.Console.WriteLine("1. Accept or 2. Decline?");
        string choiceSelect = Console.ReadLine();
        if (choiceSelect == "1")
        {
            lineSplit = lineArray[lineNumber].Split(";");
            lineSplit[6] = "Accepted";
        }
        if (choiceSelect == "2")
        {
            lineSplit = lineArray[lineNumber].Split(";");
            lineSplit[6] = "Declined";
        }
        // för att sedan uppdatera "Booking.txt" så går man igenom lines array igen för att hitta den 
        // booking man nekade eller accepterade och byter ut den mot den nya bookingen.
        for (i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains(lineSplit[0]) && lines[i].Contains(lineSplit[1]) && lines[i].Contains("Pending"))
            {
                lines[i] = string.Join(";", lineSplit);
            }
        }
        // all data man importerade från "Booking.txt" skrivs sedan tillbaka in i filen men dem ändringar som gjorts
        File.WriteAllLines(BookingFilepath, lines);
    }
    // funktion för staff att kunna göra ändringar till bookings som har lagts till i systemet
    // som det är skrivet är bara bookings som är accepterade tillgängliga för ändring.
    public static void EditBooking(List<Patient> patients, string user, string BookingFilepath)
    {
        // Samma logik som används för HandleBooking men hanterar ändring som har att göra med tid och datum
        string[] lines = File.ReadAllLines(BookingFilepath);
        string[] lineArray = new string[lines.Count()];
        string[] lineSplit = new string[0];
        int i = 0;
        for (i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("Accepted"))
            {
                lineArray[i] = lines[i];
            }
        }
        i = 0;
        foreach (string line1 in lineArray)
        {
            if (line1 != null)
            {
                string[] line1Split = line1.Split(";");
                System.Console.WriteLine($"{i}.>Doctor:{line1Split[0]}");
                System.Console.WriteLine($"    >Patient:{line1Split[1]}");
                System.Console.WriteLine($"    >Time:{line1Split[2]}-{line1Split[3]}");
                System.Console.WriteLine($"    >Date:{line1Split[4]} {line1Split[5]}");
                System.Console.WriteLine($"    >Status:{line1Split[6]}");
                System.Console.WriteLine();
            }
            i++;
        }
        System.Console.WriteLine("What bookings do you wanna Edit to?");
        string lineSelect = Console.ReadLine();
        int.TryParse(lineSelect, out int lineNumber);
        System.Console.WriteLine("What do you wanna edit?");
        System.Console.WriteLine("1. Time");
        System.Console.WriteLine("2. Date");
        string choiceSelect = Console.ReadLine();
        if (choiceSelect == "1")
        {
            System.Console.Write("What time?(8-16): ");
            string timeSelect = Console.ReadLine();
            int.TryParse(timeSelect, out int timeNumber);
            lineSplit = lineArray[lineNumber].Split(";");
            lineSplit[2] = $"{timeNumber}:00";
            lineSplit[3] = $"{timeNumber + 1}:00";
        }
        if (choiceSelect == "2")
        {
            System.Console.Write("What month would you like to meet?(Feb-Nov): ");
            string month = Console.ReadLine();
            System.Console.Write("What day?(1-28): ");
            string day = Console.ReadLine();
            lineSplit = lineArray[lineNumber].Split(";");
            lineSplit[4] = month;
            lineSplit[5] = day;
        }
        for (i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains(lineSplit[0]) && lines[i].Contains(lineSplit[1]) && lines[i].Contains("Accepted"))
            {
                lines[i] = string.Join(";", lineSplit);
            }
        }
        File.WriteAllLines(BookingFilepath, lines);
    }
    // Logik som går igenom alla bookings och skriver ut de som har matchande namn till den inloggade användaren. I detta fallet 
    // alla bookings där den ingloggade doktorn är listad.
    public static void DoctorSchedule(string user, string BookingFilepath)
    {
        string[] lines = File.ReadAllLines(BookingFilepath);
        foreach (string line in lines)
        {
            string[] lineSplit = line.Split(";");
            if (lineSplit[0] == user)
            {
                System.Console.WriteLine($"Patient: {lineSplit[1]}");
                System.Console.WriteLine($"Time: {lineSplit[2]}-{lineSplit[3]}");
                System.Console.WriteLine($"Date: {lineSplit[4]} {lineSplit[5]}");
                System.Console.WriteLine();
            }
        }
        Console.ReadLine();
    }
    // Samma logik som DoctorSchedule men för patienter
    public static void PatientSchedule(string user, string BookingFilepath)
    {
        string[] lines = File.ReadAllLines(BookingFilepath);
        foreach (string line in lines)
        {
            string[] lineSplit = line.Split(";");
            if (lineSplit[1] == user)
            {
                System.Console.WriteLine($"Doctor: {lineSplit[0]}");
                System.Console.WriteLine($"Time: {lineSplit[2]}-{lineSplit[3]}");
                System.Console.WriteLine($"Date: {lineSplit[4]} {lineSplit[5]}");
                System.Console.WriteLine();
            }
        }
        Console.ReadLine();
    }

}

