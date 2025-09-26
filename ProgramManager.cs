using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketPrinter
{
    internal class ProgramManager
    {

        public static void ShowBanner()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("╔══════════════════════╗");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("║   [ TICKET BOOKER ]  ║");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("╚══════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

        }

        public static void StartGame()
        {
            Console.CursorVisible = false;
            ShowBanner();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press ENTER to continue...");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        public static void IntroduceGame(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(prompt);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press ENTER to start...!");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        public static List<Ticket> list = new List<Ticket>();
        public static void RunGame()
        {
            for (int i = 1; i <= 3; i++)
            {
                CreateAndPrintTicket(i);
            }

            foreach (Ticket tick in list)
            {
                tick.ignoreClear = true;
                tick.PrintTicket();
            }

        }
        public static void EndGame()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Thank you for playing!");
            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press ENTER to exit...");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Mario Simic 2025");
            Console.ResetColor();
            Console.ReadLine();
        }
        private static void CreateAndPrintTicket(int ticketNumber)
        {
            Ticket ticket = new Ticket();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Ticket {ticketNumber}...");
            Console.ResetColor();

            Console.CursorVisible = true;
            ticket.ReadInfo();
            Console.CursorVisible = false;

            list.Add(ticket);

            ticket.PrintTicket();
        }
    }
}
