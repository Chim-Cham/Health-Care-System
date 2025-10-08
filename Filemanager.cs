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
    
    //Lägg till fler metoder här -----





}

