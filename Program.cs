using System.ComponentModel;

namespace TicketPrinter
{

    internal class Program
    {
        static void Main(string[] args)
        { 
            ProgramManager.StartGame();
            ProgramManager.IntroduceGame("You will be asked to make 3 tickets, they can be for different shows or the same show!" +
                "\nEnter the name, location and seat for the respective shows, but also the colorscheme of the ticket!");
            ProgramManager.RunGame();
            ProgramManager. EndGame();
        }
    }
}
