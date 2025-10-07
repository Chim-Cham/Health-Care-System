

IUser? active_user = null;
bool running = true;


// skapar första menyn ifall active_user == false. 
if (active_user == false)
{
    while (running)
    {
        //meny valen! Välj med 1-3
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Create user");
        Console.WriteLine("3. Quit");

        // matar in användarens val
        string menu1 = Console.Readline();

        // ifall log in väljs.
        if (menu1 == "1")
        {
            Console.WriteLine("Email:");
            string name = Console.Readline();

            Console.WriteLine("Password:");
            string passwrod = Console.Readline();

            //lägga till log in 

        }

        //ifall create user väljs
        if (menu1 == "2")
        {
            Console.WriteLine("Email:");
            string name = Console.Readline();

            Console.WriteLine("Password:");
            string passwrod = Console.Readline();

            //lägga till create

        }

        // ifall quit väljs
        if (menu1 == "3")
        {
            //avsluta programmet. 
        }
    }
}


