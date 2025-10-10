namespace HCS;

class Admin : IUser
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
    public void Menu()
    {
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
                    break;

                case "5":
                    break;

                case "6":
                    break;

                case "7":
                    // en lösning här hade behövts!!!!!
                    //   active_user = null;
                    break;

                case "8":
                    runningAdmin = false;
                    break;
            }
        }

    }

}