using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLearn
{
    internal class WriteMode
    {
        public static char[] keyboard_chars = {
            'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
            '`','~','!','@','#','$','%','^','&','*','(',')','-','_','+','=',
            '1','2','3','4','5','6','7','8','9','0',
            '[',']','{','}','\\','|',';',':','"','\'','<','>',',','.','?','/',
            ' ','\t'
        };

        public static bool isAMistake(string word, string def)
        {
            return word == def;
        }

        public static List<char> checkSpecialChars(string word)
        {
            List<char> special_chars = new List<char>();
            for (int i = 0; i < word.Length; i++)
            {
                char c = word.ToLower()[i];
                if (!keyboard_chars.Contains(c))
                {
                    if (!special_chars.Contains(c))
                    {
                        special_chars.Add(c);
                    }
                }
            }
            return special_chars;
        }

        public static void Write(DB dB, CustomConsole CC, Types.Set set, bool revDef)
        {
            var words = set.words;
            NewRound(dB, CC, words, revDef, 1, set, 0);
        }

        public static void RenderTopbar(Types.Set set, int roundNum, int correct, int incorrect, int todo)
        {
            Console.Write("CosmicLearn <WRITE MODE> " + set.name + "  Round: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(roundNum);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(", Correct: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(correct);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(", Incorrect: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(incorrect);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(", To go: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(todo);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void NewRound(DB dB, CustomConsole CC, List<Types.Word> words, bool revDef, int roundNum, Types.Set set, int correct)
        {
            var wordsNextRound = new List<Types.Word>();
            var running = true;

            var incorrect = 0;
            var todo = words.Count;
            var wtotal = words.Count;

            var ran = new Random();

            RenderTopbar(set, roundNum, correct, incorrect, todo);

            bool showMotivationScreen = false;

            while (running) //TODO: better mistake system
            {

                if (wtotal % 2 == 0)
                {
                    var c = wtotal;
                    if (todo == (c / 2))
                    {
                        showMotivationScreen = true;
                    }
                }
                else
                {
                    var c = wtotal + 1;
                    if (todo == (c / 2))
                    {
                        showMotivationScreen = true;
                    }
                }

                if (showMotivationScreen)
                {
                    showMotivationScreen = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nHalfway point!\nYou're doing very good! Keep it up!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Press any key to continue!");
                    Console.ReadKey(true);
                    Console.Clear();
                    RenderTopbar(set, roundNum, correct, incorrect, todo);
                }

                if (words.Count > 0)
                {
                    var wordv = words[ran.Next(words.Count)];
                    var word = (revDef ? wordv.definition : wordv.word);
                    var def = (revDef ? wordv.word : wordv.definition);
                    Console.WriteLine("\n" + word + "\nType the translation and press [ENTER].");
                    int CPos = Console.CursorTop;
                    Console.Write("> ");
                    if (CC.acceptInputUntilEnterWriteAnswer(word, def))
                    {
                        // save
                        Console.Clear();
                        return;
                    }
                    if (isAMistake(CC.getInput(), def))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nCorrect!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        words.Remove(wordv);
                        todo = todo - 1;
                        correct++;
                        Thread.Sleep(1000);
                        CC.clearInput();
                        Console.Clear();
                        RenderTopbar(set, roundNum, correct, incorrect, todo);
                    }
                    else
                    {
                        Console.Write("\nIncorrect. The correct word was \"" + def + "\".");
                        var inp = CC.getInput();
                        CC.clearInput();
                        string ableToSkip = "n";
                        if (inp != "")
                        {
                            Console.Write("\nWould you like to mark as correct? (y/n): ");
                            CC.acceptSingleInput(false);
                            ableToSkip = CC.getInput();
                        }
                        
                        if (ableToSkip == "y")
                        {
                            CC.clearInput();
                            words.Remove(wordv);
                            todo = todo - 1;
                            correct++;
                            //Thread.Sleep(1000);
                        }
                        else
                        {
                            CC.clearInput();
                            words.Remove(wordv);
                            wordsNextRound.Add(wordv);
                            todo = todo - 1;
                            incorrect++;
                            Console.Write("\nType the word again, for practice.\nWord: " + def);
                            Console.SetCursorPosition(0, CPos);
                            int i = Console.BufferWidth;

                            for (int j = 0; j < i; j++)
                            {
                                Console.Write(" ");
                            }
                            Console.SetCursorPosition(0, CPos - 1);
                            if (CC.acceptInputSpecificInput(def, word, def))
                            {
                                // save
                                Console.Clear();
                                return;
                            }
                            CC.clearInput();
                            Thread.Sleep(1000);
                        }
                        Console.Clear();
                        RenderTopbar(set, roundNum, correct, incorrect, todo);
                    }
                }
                else
                {
                    if (wordsNextRound.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nRound completed, you did very good!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Press any key to continue to the next round.");
                        Console.ReadKey(true);
                        Console.Clear();
                        running = false;
                        NewRound(dB, CC, wordsNextRound, revDef, roundNum+1, set, correct);
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nAll words completed! Good job!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(2000);
                        Console.Clear();
                        running = false;
                    }
                }
            }
        }
    }
}
