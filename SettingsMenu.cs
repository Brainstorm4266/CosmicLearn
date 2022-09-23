using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLearn
{
    internal class SettingsMenu
    {
        public static void run(DB dB, CustomConsole CC)
        {
            Console.Clear();
            Console.WriteLine("CosmicLearn\n" +
                "Coming Soon!\n" +
                "[ESC] Go back to main menu\n");
            Console.Write("");
            ConsoleKey[] whitelist = { ConsoleKey.Escape };
            CC.acceptSingleInput(whitelist, true);
            if (CC.getConsoleKey() == ConsoleKey.Escape)
            {
                Console.Clear();
                CC.clearInput();
                return;
            }
        }
    }
}
