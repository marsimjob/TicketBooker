using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketPrinter
{
    internal class ProgramManager
    {

        public static void ShowBannerCentered()
        {
            string[] lines =
            {
        "╔══════════════════════╗",
        "║   [ TICKET BOOKER ]  ║",
        "╚══════════════════════╝"
    };

            int winWidth = Console.WindowWidth;
            int winHeight = Console.WindowHeight;

            // Vertical starting row so it’s centered
            int startRow = (winHeight - lines.Length) / 2;
            if (startRow < 0) startRow = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                // Horizontal start column to center this line
                int startCol = (winWidth - line.Length) / 2;
                if (startCol < 0) startCol = 0;

                Console.SetCursorPosition(startCol, startRow + i);
                // Set colors around the content
                if (i == 0 || i == lines.Length - 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    // Middle line: change foreground for “TICKET BOOKER”
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                Console.Write(line);
                Console.ResetColor();
            }
        }

        public static void StartGame()
        {
            Console.CursorVisible = false;

            Console.Clear();
            ShowBannerCentered();

            // Now “Press ENTER to continue...” under the banner
            string prompt = "Press ENTER to continue...";

            int winWidth = Console.WindowWidth;
            int winHeight = Console.WindowHeight;

            // Place the prompt a bit below the banner
            int promptRow = (winHeight - (3 + 1)) / 2 + 3;  // 3 banner lines + 1 line below

            if (promptRow < 0) promptRow = winHeight - 1;

            int promptCol = (winWidth - prompt.Length) / 2;
            if (promptCol < 0) promptCol = 0;

            Console.SetCursorPosition(promptCol, promptRow);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(prompt);
            Console.ResetColor();

            Console.SetCursorPosition(0, winHeight - 1);  // or somewhere safe
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
