namespace HCS;

public class Patient : IUser
{
    public string Email;
    string _password;

    public Patient(string email, string password)
    {
        Email = email;
        _password = password;
    }
    public string ToFileString(string username, string password)
    {
        return $"{Email};{_password}";
    }


    public void Menu()
    {
        bool runningPatient = true;

        while(runningPatient)
        {
            Console.Clear();
            Console.WriteLine("-----Healtcare-----");
            Console.WriteLine("1. Scheduale");
            Console.WriteLine("2. Journal");
            Console.WriteLine("3. Bocking");
            Console.WriteLine("4. Log out");
            Console.WriteLine("5. Quit");

            switch(Console.ReadLine())
            {
                case "1":
                    break;

                case "2":
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    break;
            }
        }
    }

}