using System.Security.Cryptography.X509Certificates;

namespace HCS;


public class Filemanage
{
    public static void EnsurePath(string AdminFilepath, string PatientFilePath, string PersonnelFilepath)
    {
        string directoryAdmin = Path.GetDirectoryName(AdminFilepath);
        string directoryPatient = Path.GetDirectoryName(PatientFilePath);
        string directoryPersonnel = Path.GetDirectoryName(PersonnelFilepath);

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

        // kontorllerar att filen Personal.txt finns
        if (!string.IsNullOrEmpty(directoryPersonnel))
        {
            //ifall filen inte finns så skapas den filen
            if (!Directory.Exists(directoryPersonnel))
            {
                Directory.CreateDirectory(directoryPersonnel);
                Console.WriteLine($"Created file: {directoryPersonnel}");
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

        public static Personnel FromFileToStringPersonnel(string line)
        {
            string[] personnelParts = line.Split(";");
            return new Personnel(personnelParts[0], personnelParts[1]);
        }
    }


    //skapar en class för att lägga till användare 
    public class AddPatient
    {




        //Här har jag gjort så att man lägger till en användare som är admin men vet inte hur jag ska ta mig till väga härifrån
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
    public static void LoadUsers(string AdminFilepath, string PatientFilePath, string PersonnelFilepath, List<Admin> admins, List<Patient> patients, List<Personnel> personnel)
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
        if (File.Exists(PersonnelFilepath))
        {
            string[] lines = File.ReadAllLines(PersonnelFilepath);
            foreach (string line in lines)
            {
                if (line != "") //ignorera tomma rader 
                {
                    Personnel p = ReadUser.FromFileToStringPersonnel(line); //hämtar metod ReadUser
                    personnel.Add(p);
                }

            }

        }
    }






}

