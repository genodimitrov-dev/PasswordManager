class Program
{
    static void Main(string[] args)
    {
       bool ProgramRunning = true;
       PasswordManager.LoadFromFile();
       while(ProgramRunning)
        {
            PasswordManager.ShowMenu();
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    PasswordManager.AddPassword();
                    break;
                case "2":
                    PasswordManager.ShowPasswords();
                    break;
                case "3":
                    PasswordManager.EditPassword_Username();
                    break;
                case "4":
                    PasswordManager.DeleteEntry();
                    break;
                case "5":
                    PasswordManager.SearchByWebsite();
                    break;
                case "6":
                    ProgramRunning = false;
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        }
        
        
    }
}