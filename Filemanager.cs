using System.Security.Cryptography.X509Certificates;

namespace HCS;


class Filemanage
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

        //Läser in från filen anändarnamn och lösenord - koppla denna till log in.
        public static Admin FromFileToStringAdmin(string line)
        {
            string[] parts = line.Split(";");
            return new Admin(parts[0], parts[1]);
        }

        public static Patient FromFileToStringPatient(string line)
        {
            string[] parts = line.Split(";");
            return new Patient(parts[0], parts[1]);
        }

        public static Personnel FromFileToStringPersonnel(string line)
        {
            string[] parts = line.Split(";");
            return new Personnel(parts[0], parts[1]);
        }
    }


    //skapar en class för att lägga till användare 
    class Add
    {




        //Här har jag gjort så att man lägger till en annändare som är admin men vet inte hur jag ska ta mig till väga härifrån
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
                writer.WriteLine(newPatient.ToFileString(email,password));
            }

            // Bekräftar att användaren sparades
            Console.WriteLine($"User '{email}' have been added!");
            Console.ReadLine();
        }
    }



}

