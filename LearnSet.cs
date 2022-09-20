using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLearn
{
    internal class LearnSet
    {
        public static void run(DB dB, CustomConsole CC)
        {
            Console.Clear();
            Console.WriteLine("CosmicLearn\n" +
                "[W] Write\n" +
                "[Esc] Cancel\n");
            Console.Write("Select mode: ");
            ConsoleKey[] whitelist = { ConsoleKey.W, ConsoleKey.Escape };
            CC.acceptSingleInput(whitelist, true);
            if (CC.getConsoleKey() == ConsoleKey.Escape)
            {
                Console.Clear();
                CC.clearInput();
                return;
            } else if (CC.getConsoleKey() == ConsoleKey.W)
            {
                CC.clearInput();
                Console.WriteLine("\n");
                var sets = dB.getSets();
                Console.Clear();
                if (sets.Count > 0)
                {
                    int pages = (int)decimal.Truncate(sets.Count / 10);
                    var selected = false;

                    while (!selected)
                    {
                        var i = 0;
                        var page = 0;
                        var amntRendered = 0;
                        if (((page + 1) * 10) > sets.Count)
                        {
                            int e = sets.Count - (pages * 10);
                            amntRendered = ((page) * 10) + e;
                        } else
                        {
                            amntRendered = ((page + 1) * 10);
                        }
                        foreach (var set in sets.GetRange(page * 10, amntRendered))
                        {
                            Console.WriteLine("[" + i + "] " + set.name);
                            i++;
                        }

                        CC.clearInput();

                        Console.Write($"\nPage {page + 1} of {pages + 1}\nSelect set using arrow keys.\n");

                        var sel = false;
                        var selectedSet = 0;
                        var l = Console.CursorLeft;
                        var h = Console.CursorTop;
                        while (!sel)
                        {
                            Console.SetCursorPosition(0, 0);
                            i = 0;
                            foreach (var set in sets.GetRange(page * 10, amntRendered))
                            {
                                if (selectedSet == i)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("[" + i + "] " + set.name);
                                    i++;
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                } else
                                {
                                    Console.WriteLine("[" + i + "] " + set.name);
                                    i++;
                                }
                            }
                            Console.SetCursorPosition(l, h);
                            var key = Console.ReadKey(true);

                            if (key.Key == ConsoleKey.DownArrow)
                            {
                                if (selectedSet < (i-1))
                                {
                                    selectedSet++;
                                }
                            } else if (key.Key == ConsoleKey.UpArrow)
                            {
                                if (selectedSet != 0)
                                {
                                    selectedSet--;
                                }
                            } else if (key.Key == ConsoleKey.RightArrow)
                            {
                                if (!(page >= pages))
                                {
                                    page++;
                                    break;
                                }
                            } else if (key.Key == ConsoleKey.LeftArrow)
                            {
                                if (page != 0)
                                {
                                    page--;
                                    break;
                                }
                            } else if (key.Key == ConsoleKey.Spacebar)
                            {
                                sel = true;
                                selected = true;
                            }
                        }
                        Console.Clear();
                        WriteMode.Write(dB, CC, sets[(page * 10) + selectedSet], true);
                    }
            } else
                {
                    Console.WriteLine("No sets are defined. Go back and make a new set.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return;
                }
            }
        }
    }
}
