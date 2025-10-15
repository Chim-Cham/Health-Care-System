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




    // denna kan man override för en annan meny för andra användare. 
    //kan va att man behöver ändra till ej static när andra punkter körs om man ska hämta variablar från program.cs
    public bool Menu(string StaffFilepath)
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
                    //add locations;
                    try { Console.Clear(); } catch { }
                    Console.WriteLine("Write in which region: blekinge, halland, skåne, kronoberg?");
                    string choosenRegion = Console.ReadLine();
                    AllRegions selectedRegion = AllRegions.none;
                    switch (choosenRegion)
                    {
                        case "blekinge":
                            selectedRegion = AllRegions.Blekinge;
                            Console.WriteLine("Write the name of the new reception");
                            string newReceptionBlekinge = Console.ReadLine();

                            Location newlocationBlekinge = new Location(newReceptionBlekinge, AllRegions.Blekinge);
                            Console.WriteLine($"New reception: {newReceptionBlekinge} was added in Region {AllRegions.Blekinge}");
                            Console.WriteLine("Press ENTER to go back to menu");
                            Console.ReadLine();
                            break;

                        case "halland":
                            selectedRegion = AllRegions.Halland;
                            Console.WriteLine("Write the name of the new reception");
                            string newReceptionHalland = Console.ReadLine();

                            Location newlocationHalland = new Location(newReceptionHalland, AllRegions.Halland);
                            Console.WriteLine($"New reception: {newReceptionHalland} was added in Region {AllRegions.Halland}");
                            Console.WriteLine("Press ENTER to go back to menu");
                            Console.ReadLine();
                            break;

                        case "skåne":
                            selectedRegion = AllRegions.Skåne;

                            Console.WriteLine("Write the name of the new reception");
                            string newReceptionSkåne = Console.ReadLine();
                            Location newlocationSkåne = new Location(newReceptionSkåne, AllRegions.Skåne);
                            Console.WriteLine($"New reception: {newReceptionSkåne} was added in Region {AllRegions.Skåne}");
                            Console.WriteLine("Press ENTER to go back to menu");
                            Console.ReadLine();
                            break;

                        case "kronoberg":
                            selectedRegion = AllRegions.Kronoberg;
                            Console.WriteLine("Write the name of the new reception");
                            string newReceptionKronoberg = Console.ReadLine();

                            Location newlocationKronoberg = new Location(newReceptionKronoberg, AllRegions.Skåne);
                            Console.WriteLine($"New reception: {newReceptionKronoberg} was added in Region {AllRegions.Kronoberg}");
                            Console.WriteLine("Press ENTER to go back to menu");
                            Console.ReadLine();
                            break;
                    }
                    break;

                case "4":
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