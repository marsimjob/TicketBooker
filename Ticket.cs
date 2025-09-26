namespace TicketPrinter
{
    public class Ticket
    {

        private string? EventName { get; set; }
        private string? Location { get; set; }
        private string? Seat { get; set; }

        public ConsoleColor ticketColor { get; set; }
        public ConsoleColor textColor { get; set; }

        public bool ignoreClear = false;

        public void ReadInfo()
        {
            do
            {
                // Ask user for info
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Please enter the name of the Event: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                EventName = Console.ReadLine();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Add a location for the event: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Location = Console.ReadLine();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Type in the seat number: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Seat = Console.ReadLine();
                Console.ResetColor();
                if (string.IsNullOrEmpty(EventName) ||
                string.IsNullOrEmpty(Location) ||
                string.IsNullOrEmpty(Seat))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Information is missing, try filling in the missing information!");
                    Console.ResetColor();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

            } while (string.IsNullOrEmpty(EventName) ||
                string.IsNullOrEmpty(Location) ||
                string.IsNullOrEmpty(Seat));
            
            Console.Clear();

            SetTicketColor();
        }

        private void SetTicketColor()
        {
            // Ask for text (foreground) color
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please customise your ticket!");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Available colors: " + string.Join(", ", Enum.GetNames(typeof(ConsoleColor))));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter the Text color: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            string? fgInput = Console.ReadLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter the Ticket color: ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            string? bgInput = Console.ReadLine();
            Console.ResetColor();
            // Try to parse them
            if (Enum.TryParse<ConsoleColor>(fgInput, true, out var fgColor))
            {
                textColor = fgColor;
            }
            else
            {
                Console.WriteLine("Invalid Text color name. Using default -- Black");
                textColor = ConsoleColor.Black;
            }

            if (Enum.TryParse<ConsoleColor>(bgInput, true, out var bgColor))
            {
                ticketColor = bgColor;
            }
            else
            {
                Console.WriteLine("No Ticket color found! Using default -- DarkGray");
                ticketColor = ConsoleColor.DarkGray;
            }

            Console.Clear();
        }

        public void PrintTicket()
        {
            if (string.IsNullOrEmpty(EventName) ||
                string.IsNullOrEmpty(Location) ||
                string.IsNullOrEmpty(Seat))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Information is missing, try ReadInfo() and fill in the missing information!");
                Console.ResetColor();
                Console.ReadLine();
                Console.Clear();
                return; // Don't print a ticket
            }

            // Save old colors
            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;

            // Apply ticket colors
            Console.ForegroundColor = textColor;    // text color
            Console.BackgroundColor = ticketColor;  // background color for ticket area

            // Dimensions
            int width = 40;   // total width including borders
            int height = 8;   // total lines including top & bottom border
            int interiorWidth = width - 2;

            // Helper: truncate string with "..."
            string Truncate(string s, int max)
            {
                if (s.Length <= max) return s;
                if (max <= 3) return s.Substring(0, max);
                return s.Substring(0, max - 3) + "...";
            }

            string ev = Truncate(EventName, interiorWidth);
            string loc = Truncate(Location, interiorWidth);
            string seat = Truncate(Seat, interiorWidth);

            string CenterText(string s)
            {
                if (s.Length >= interiorWidth) return s;
                int left = (interiorWidth - s.Length) / 2;
                int right = interiorWidth - s.Length - left;
                return new string(' ', left) + s + new string(' ', right);
            }

            // Border characters
            char topLeft = '╔', topRight = '╗';
            char botLeft = '╚', botRight = '╝';
            char horiz = '═', vert = '║';

            for (int line = 0; line < height; line++)
            {
                if (line == 0)
                {
                    // Top border
                    Console.Write(topLeft);
                    Console.Write(new string(horiz, interiorWidth));
                    Console.Write(topRight);
                }
                else if (line == 1)
                {
                    Console.Write(vert);
                    Console.Write(new string(horiz, interiorWidth));
                    Console.Write(vert);
                }
                else if (line == height - 2)
                {
                    Console.Write(vert);
                    Console.Write(new string(horiz, interiorWidth));
                    Console.Write(vert);
                }
                else if (line == height - 1)
                {
                    Console.Write(botLeft);
                    Console.Write(new string(horiz, interiorWidth));
                    Console.Write(botRight);
                }
                else
                {
                    Console.Write(vert);

                    int ticketLine = 2, evLine = 3, locLine = 4, seatLine = 5;
                    string interior;

                    if (line == ticketLine)
                    {
                        interior = CenterText("TICKET");
                    }
                    else if (line == evLine)
                    {
                        // Left-align (or center) event name
                        interior = ev.PadLeft((interiorWidth + ev.Length) / 2).PadRight(interiorWidth);
                    }
                    else if (line == locLine)
                    {
                        interior = loc.PadLeft((interiorWidth + loc.Length) / 2).PadRight(interiorWidth);
                    }
                    else if (line == seatLine)
                    {
                        interior = seat.PadLeft((interiorWidth + seat.Length) / 2).PadRight(interiorWidth);
                    }
                    else
                    {
                        interior = new string(' ', interiorWidth);
                    }

                    Console.Write(interior);
                    Console.Write(vert);
                }

                Console.WriteLine();
            }

            // Restore old colors
            Console.ForegroundColor = oldFg;
            Console.BackgroundColor = oldBg;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press ENTER to continue");
            Console.ResetColor();
            Console.ReadLine();
            if(!ignoreClear) 
            Console.Clear();
        }
    }
}
