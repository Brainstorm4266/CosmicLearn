using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLearn
{
    internal class CustomConsole
    {

        private string input = "";
        private ConsoleKey consoleKey = ConsoleKey.NoName;

        public void acceptInputUntilEnter(string preWrite)
        {
            if (!(preWrite is null))
            {
                input = preWrite;
            }
            Console.Write(input);
            ConsoleKeyInfo key;
            bool enterPressed = false;
            while (!enterPressed)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    enterPressed = true;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else if ((key.Key == ConsoleKey.UpArrow) || (key.Key == ConsoleKey.DownArrow) || (key.Key == ConsoleKey.LeftArrow) || (key.Key == ConsoleKey.RightArrow))
                {
                    continue; //TODO: left and right arrow support
                }
                else
                {
                    string k;
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        k = key.KeyChar.ToString().ToUpper();
                        input += k;
                        Console.Write(k);
                    }
                    else
                    {
                        k = key.KeyChar.ToString();
                        input += k;
                        Console.Write(k);
                    }
                }

            }
        }

        public bool acceptInputSpecificInput(string inp, string word, string def)
        {
            int h;
            int l;
            while (Console.KeyAvailable)
                Console.ReadKey(true); // flush stdin
            Console.Write("\n"+"> "+input);
            ConsoleKeyInfo key;
            List<char> specialChars = WriteMode.checkSpecialChars(def);
            int currSpecialChar = 0;
            bool specialSelectMode = false;
            if (specialChars.Count > 0)
            {
                h = Console.CursorTop;
                l = Console.CursorLeft;
                Console.SetCursorPosition(0, Console.BufferHeight - 2);
                Console.Write("TAB = Special character row, SHIFT+TAB = Left in special character row, SPACE = Select character");
                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                specialChars.ForEach((char ch) =>
                {
                    Console.Write("< " + ch + " > ");
                });
                Console.SetCursorPosition(l, h);
            }
            else
            {
                h = Console.CursorTop;
                l = Console.CursorLeft;
                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                Console.Write("<NO SPECIAL CHARACTERS IN DEFINITION>");
                Console.SetCursorPosition(l, h);
            }
            while (!(input == inp))
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    continue;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        continue;
                    }
                }
                else if ((key.Key == ConsoleKey.UpArrow) || (key.Key == ConsoleKey.DownArrow) || (key.Key == ConsoleKey.LeftArrow) || (key.Key == ConsoleKey.RightArrow))
                {
                    continue; //TODO: left and right arrow support
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return true;
                }
                else if ((key.Key == ConsoleKey.Tab) && (specialChars.Count > 0))
                {
                    //Console.Write("tab triggered");
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        if (currSpecialChar > 0)
                        {
                            currSpecialChar = currSpecialChar - 1;
                            if (currSpecialChar == 0)
                            {
                                h = Console.CursorTop;
                                l = Console.CursorLeft;
                                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                                specialSelectMode = false;
                                specialChars.ForEach((char ch) =>
                                {
                                    Console.Write("< " + ch + " > ");
                                });
                                Console.SetCursorPosition(l, h);
                            }
                            else
                            {
                                h = Console.CursorTop;
                                l = Console.CursorLeft;
                                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                                for (int d = 0; d < (specialChars.Count); d++)
                                {
                                    char v = specialChars[d];
                                    if (d == (currSpecialChar - 1))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write("< " + v + " > ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    else
                                    {
                                        Console.Write("< " + v + " > ");
                                    }
                                }
                                Console.SetCursorPosition(l, h);
                            }
                        }
                    }
                    else
                    {
                        specialSelectMode = true;
                        if (currSpecialChar != (specialChars.Count))
                        {
                            currSpecialChar++;
                            h = Console.CursorTop;
                            l = Console.CursorLeft;
                            Console.SetCursorPosition(0, Console.BufferHeight - 1);
                            //Console.Write("for loop");
                            for (int d = 0; d < (specialChars.Count); d++)
                            {
                                char v = specialChars[d];
                                if (d == (currSpecialChar - 1))
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("< " + v + " > ");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                else
                                {
                                    Console.Write("< " + v + " > ");
                                }
                            }
                            Console.SetCursorPosition(l, h);
                        }
                    }
                }
                else if ((key.Key == ConsoleKey.Spacebar) && specialSelectMode && (specialChars.Count > 0))
                {
                    //Console.Write("space");
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        input += specialChars[currSpecialChar - 1].ToString().ToUpper();
                        Console.Write(specialChars[currSpecialChar - 1].ToString().ToUpper());
                        specialSelectMode = false;
                        currSpecialChar = 0;
                        h = Console.CursorTop;
                        l = Console.CursorLeft;
                        Console.SetCursorPosition(0, Console.BufferHeight - 1);
                        specialSelectMode = false;
                        specialChars.ForEach((char ch) =>
                        {
                            Console.Write("< " + ch + " > ");
                        });
                        Console.SetCursorPosition(l, h);
                    }
                    else
                    {
                        input += specialChars[currSpecialChar - 1].ToString();
                        Console.Write(specialChars[currSpecialChar - 1].ToString());
                        specialSelectMode = false;
                        currSpecialChar = 0;
                        h = Console.CursorTop;
                        l = Console.CursorLeft;
                        Console.SetCursorPosition(0, Console.BufferHeight - 1);
                        specialSelectMode = false;
                        specialChars.ForEach((char ch) =>
                        {
                            Console.Write("< " + ch + " > ");
                        });
                        Console.SetCursorPosition(l, h);
                    }
                }
                else
                {
                    string k;
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        k = key.KeyChar.ToString().ToUpper();
                        input += k;
                        Console.Write(k);
                    }
                    else
                    {
                        k = key.KeyChar.ToString();
                        input += k;
                        Console.Write(k);
                    }
                }

            }
            // fill in the gaps
            //h = Console.CursorTop;
            //l = Console.CursorLeft;
            //Console.SetCursorPosition(0, Console.BufferHeight - 1);
            //int i = Console.BufferWidth;
            //
            //for (int j = 0; j < i; j++)
            //{
            //    Console.Write(" ");
            //}
            //Console.SetCursorPosition(0, Console.BufferHeight - 2);
            //
            //for (int j = 0; j < i; j++)
            //{
            //    Console.Write(" ");
            //}
            //System.Console.SetCursorPosition(h, l);
            specialSelectMode = false;
            currSpecialChar = 0;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("> " + input);
            Console.ForegroundColor = ConsoleColor.Gray;
            return false;
        }

        public void acceptInputUntilEnter()
        {
            Console.Write(input);
            ConsoleKeyInfo key;
            bool enterPressed = false;
            while (!enterPressed)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    enterPressed = true;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else if ((key.Key == ConsoleKey.UpArrow)||(key.Key == ConsoleKey.DownArrow)||(key.Key == ConsoleKey.LeftArrow)|| (key.Key == ConsoleKey.RightArrow))
                {
                    continue; //TODO: left and right arrow support
                }
                else
                {
                    string k;
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        k = key.KeyChar.ToString().ToUpper();
                        input += k;
                        Console.Write(k);
                    }
                    else
                    {
                        k = key.KeyChar.ToString();
                        input += k;
                        Console.Write(k);
                    }
                }

            }
        }

        public bool acceptInputUntilEnterWriteAnswer(string word, string def)
        {
            int h;
            int l;
            while (Console.KeyAvailable)
                Console.ReadKey(true); // flush stdin
            Console.Write(input);
            ConsoleKeyInfo key;
            bool enterPressed = false;
            bool specialSelectMode = false;
            List<char> specialChars = WriteMode.checkSpecialChars(def);
            int currSpecialChar = 0;
            if (specialChars.Count > 0)
            {
                h = Console.CursorTop;
                l = Console.CursorLeft;
                Console.SetCursorPosition(0, Console.BufferHeight - 2);
                Console.Write("TAB = Special character row, SHIFT+TAB = Left in special character row, SPACE = Select character");
                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                specialChars.ForEach((char ch) =>
                {
                    Console.Write("< " + ch + " > ");
                });
                Console.SetCursorPosition(l, h);
            } else
            {
                h = Console.CursorTop;
                l = Console.CursorLeft;
                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                Console.Write("<NO SPECIAL CHARACTERS IN DEFINITION>");
                Console.SetCursorPosition(l, h);
            }
            while (!enterPressed)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    if (!specialSelectMode)
                    {
                        enterPressed = true;
                        //// fill in the gaps
                        //h = Console.CursorTop;
                        //l = Console.CursorLeft;
                        //Console.SetCursorPosition(0, Console.BufferHeight - 1);
                        //int i = Console.BufferWidth;
                        //
                        //for (int j = 0; j < i; j++)
                        //{
                        //    Console.Write(" ");
                        //}
                        //Console.SetCursorPosition(0, Console.BufferHeight - 2);
                        //
                        //for (int j = 0; j < i; j++)
                        //{
                        //    Console.Write(" ");
                        //}
                        //System.Console.SetCursorPosition(h,l);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return true;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (!specialSelectMode)
                    {
                        if (input.Length > 0)
                        {
                            input = input.Remove(input.Length - 1, 1);
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if ((key.Key == ConsoleKey.UpArrow) || (key.Key == ConsoleKey.DownArrow) || (key.Key == ConsoleKey.LeftArrow) || (key.Key == ConsoleKey.RightArrow))
                {
                    continue; //TODO: left and right arrow support
                }
                else if ((key.Key == ConsoleKey.Tab) && (specialChars.Count > 0))
                {
                    //Console.Write("tab triggered");
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        if (currSpecialChar > 0)
                        {
                            currSpecialChar = currSpecialChar - 1;
                            if (currSpecialChar == 0)
                            {
                                h = Console.CursorTop;
                                l = Console.CursorLeft;
                                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                                specialSelectMode = false;
                                specialChars.ForEach((char ch) =>
                                {
                                    Console.Write("< " + ch + " > ");
                                });
                                Console.SetCursorPosition(l, h);
                            } else
                            {
                                h = Console.CursorTop;
                                l = Console.CursorLeft;
                                Console.SetCursorPosition(0, Console.BufferHeight - 1);
                                for (int i = 0; i < (specialChars.Count); i++)
                                {
                                    char v = specialChars[i];
                                    if (i == (currSpecialChar - 1))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write("< " + v + " > ");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    else
                                    {
                                        Console.Write("< " + v + " > ");
                                    }
                                }
                                Console.SetCursorPosition(l, h);
                            }
                        }
                    } else
                    {
                        specialSelectMode = true;
                        if (currSpecialChar != (specialChars.Count))
                        {
                            currSpecialChar++;
                            h = Console.CursorTop;
                            l = Console.CursorLeft;
                            Console.SetCursorPosition(0, Console.BufferHeight - 1);
                            //Console.Write("for loop");
                            for (int i = 0; i < (specialChars.Count); i++)
                            {
                                char v = specialChars[i];
                                if (i == (currSpecialChar-1))
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("< " + v + " > ");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                } else {
                                    Console.Write("< " + v + " > ");
                                }
                            }
                            Console.SetCursorPosition(l, h);
                        }
                    }
                }
                else if ((key.Key == ConsoleKey.Spacebar) && specialSelectMode && (specialChars.Count > 0))
                {
                    //Console.Write("space");
                    if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        input += specialChars[currSpecialChar-1].ToString().ToUpper();
                        Console.Write(specialChars[currSpecialChar - 1].ToString().ToUpper());
                        specialSelectMode = false;
                        currSpecialChar = 0;
                        h = Console.CursorTop;
                        l = Console.CursorLeft;
                        Console.SetCursorPosition(0, Console.BufferHeight - 1);
                        specialSelectMode = false;
                        specialChars.ForEach((char ch) =>
                        {
                            Console.Write("< " + ch + " > ");
                        });
                        Console.SetCursorPosition(l, h);
                    } else
                    {
                        input += specialChars[currSpecialChar - 1].ToString();
                        Console.Write(specialChars[currSpecialChar - 1].ToString());
                        specialSelectMode = false;
                        currSpecialChar = 0;
                        h = Console.CursorTop;
                        l = Console.CursorLeft;
                        Console.SetCursorPosition(0, Console.BufferHeight - 1);
                        specialSelectMode = false;
                        specialChars.ForEach((char ch) =>
                        {
                            Console.Write("< " + ch + " > ");
                        });
                        Console.SetCursorPosition(l, h);
                    }
                }
                else
                {
                    if (!specialSelectMode)
                    {
                        string k;
                        if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                        {
                            k = key.KeyChar.ToString().ToUpper();
                            input += k;
                            Console.Write(k);
                        }
                        else
                        {
                            k = key.KeyChar.ToString();
                            input += k;
                            Console.Write(k);
                        }
                    }
                }
            }
            return false;
        }

        public void acceptSingleInput(bool forceCaps)
        {
            var key = Console.ReadKey(true);
            if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
            {
                input = key.KeyChar.ToString().ToUpper();
                var toBeWritten = translateNonChar(key.Key);
                if (toBeWritten == "")
                {
                    toBeWritten = key.KeyChar.ToString();
                }
                Console.Write(toBeWritten.ToUpper());
            } else
            {
                if (forceCaps)
                {
                    input = key.KeyChar.ToString().ToUpper();
                    var toBeWritten = translateNonChar(key.Key);
                    if (toBeWritten == "")
                    {
                        toBeWritten = key.KeyChar.ToString();
                    }
                    Console.Write(toBeWritten.ToUpper());
                } else
                {
                    input = key.KeyChar.ToString();
                    var toBeWritten = translateNonChar(key.Key);
                    if (toBeWritten == "")
                    {
                        toBeWritten = key.KeyChar.ToString();
                    }
                    Console.Write(toBeWritten);
                }
            }
        }

        public void acceptSingleInput(string[] whitelist, bool forceCaps)
        {
            bool gotValidInput = false;
            while (!gotValidInput)
            {
                var key = Console.ReadKey(true);
                if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    if (whitelist.Contains(key.KeyChar.ToString().ToUpper()))
                    {
                        input = key.KeyChar.ToString().ToUpper();
                        var toBeWritten = translateNonChar(key.Key);
                        if (toBeWritten == "")
                        {
                            toBeWritten = key.KeyChar.ToString();
                        }
                        Console.Write(toBeWritten.ToUpper());
                        gotValidInput = true;
                    }
                }
                else
                {
                    if (forceCaps)
                    {
                        if (whitelist.Contains(key.KeyChar.ToString().ToUpper()))
                        {
                            input = key.KeyChar.ToString().ToUpper();
                            var toBeWritten = translateNonChar(key.Key);
                            if (toBeWritten == "")
                            {
                                toBeWritten = key.KeyChar.ToString();
                            }
                            Console.Write(toBeWritten);
                            gotValidInput = true;
                        }
                    }
                    else
                    {
                        if (whitelist.Contains(key.KeyChar.ToString()))
                        {
                            input = key.KeyChar.ToString();
                            var toBeWritten = translateNonChar(key.Key);
                            if (toBeWritten == "")
                            {
                                toBeWritten = key.KeyChar.ToString();
                            }
                            Console.Write(toBeWritten);
                            gotValidInput = true;
                        }
                    }
                }
            }
        }

        public void acceptSingleInput(ConsoleKey[] whitelist, bool forceCaps)
        {
            bool gotValidInput = false;
            while (!gotValidInput)
            {
                var key = Console.ReadKey(true);
                //Console.Write(key.Key);
                if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    if (whitelist.Contains(key.Key))
                    {
                        input = key.KeyChar.ToString().ToUpper();
                        consoleKey = key.Key;
                        var toBeWritten = translateNonChar(key.Key);
                        if (toBeWritten == "")
                        {
                            toBeWritten = key.KeyChar.ToString();
                        }
                        Console.Write(toBeWritten.ToUpper());
                        gotValidInput = true;
                    }
                }
                else
                {
                    if (forceCaps)
                    {
                        if (whitelist.Contains(key.Key))
                        {
                            input = key.KeyChar.ToString().ToUpper();
                            consoleKey = key.Key;
                            var toBeWritten = translateNonChar(key.Key);
                            if (toBeWritten == "")
                            {
                                toBeWritten = key.KeyChar.ToString();
                            }
                            Console.Write(toBeWritten.ToUpper());
                            gotValidInput = true;
                        }
                    }
                    else
                    {
                        if (whitelist.Contains(key.Key))
                        {
                            input = key.KeyChar.ToString();
                            var toBeWritten = translateNonChar(key.Key);
                            if (toBeWritten == "")
                            {
                                toBeWritten = key.KeyChar.ToString();
                            }
                            Console.Write(toBeWritten);
                            gotValidInput = true;
                        }
                    }
                }
            }
        }

        public void clearInput()
        {
            input = "";
            consoleKey = ConsoleKey.NoName;
        }

        public string getInput()
        {
            return input;
        }

        public ConsoleKey getConsoleKey()
        {
            return consoleKey;
        }

        private string translateNonChar(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                return "[Esc]";
            } else
            {
                return "";
            }
        }
    }
}
