namespace HCS;

// det har blivit ett felmeddelande här. IUser har som krav att varje användare ska ha tryLogin och Getrole 
class Personnel : IUser
{
    public string Username;
    string _password;

    public Personnel(string username, string password)
    {
        Username = username;
        _password = password;
    }

    public void Menu()
    {
        bool runningPersonnel = true;

        while(runningPersonnel)
        {
            Console.Clear();
            Console.WriteLine("-----Healtcare-----");
            Console.WriteLine("1. Schedual");
            Console.WriteLine("2. Journal");
            Console.WriteLine("3. bocking");
            Console.WriteLine("4. Logout");
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