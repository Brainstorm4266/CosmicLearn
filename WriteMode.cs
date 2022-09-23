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

        public static bool isAMistake(string word, string def, Types.UserSetProgress usp)
        {
            if (usp.setSettings.strictMode)
            {
                return word == def;
            } else
            {
                //TODO: add custom algorithm to see if correct or not
                //int mistakes = 0;
                //int mistakeThreshold = 3;
                return word == def;
            }
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

        public static void writeDatabase(DB dB, Types.Set set, Types.UserSetProgress usp)
        {
            int h = Console.CursorTop;
            int l = Console.CursorLeft;
            Console.SetCursorPosition(0, Console.BufferHeight - 4);
            Console.Write("Writing database...");
            var udata = dB.getUserData();
            if (udata is null)
            {
                throw new Exception();
            }

            for (int o = 0; o < udata.progresses.Count; o++)
            {
                var prog = udata.progresses[o];
                if (prog.setId == set.setId)
                {
                    // this is the required set progress
                    //var prgrs = udata.progresses[o];
                    //prgrs.wordsCorrect = usp.wordsCorrect ?? prgrs.wordsCorrect;
                    //prgrs.wordsRemaining = usp.wordsRemaining ?? prgrs.wordsRemaining;
                    //prgrs.wordsIncorrect = usp.wordsIncorrect ?? prgrs.wordsIncorrect;
                    //prgrs.correctNumber = usp.correctNumber;
                    //prgrs.remainingNumber = usp.remainingNumber;
                    //prgrs.incorrectNumber = usp.incorrectNumber;
                    //prgrs.rounds = usp.rounds;
                    //prgrs.currentWord = usp.currentWord ?? prgrs.currentWord;
                    udata.progresses[o] = usp;
                }
            }

            dB.setUserData(udata);
            Console.SetCursorPosition(0, Console.BufferHeight - 4);
            Console.Write("                     ");
            Console.SetCursorPosition(l, h);
        }

        public static void Write(DB dB, CustomConsole CC, Types.Set set, int setId)
        {
            var udata = dB.getUserData();
            if (udata is null)
            {
                throw new Exception();
            }

            Types.UserSetProgress upr = new Types.UserSetProgress();
            foreach (var prog in udata.progresses)
            {
                if (prog.setId == setId)
                {
                    Console.WriteLine("Setting prog with setId "+prog.setId+", current setId is "+set.setId);
                    upr = prog;
                }
            }
            var words = set.words;
            if (!upr.setOngoing)
            {
                upr.setOngoing = true;
                upr.wordsRemaining = words;
                upr.remainingNumber = words.Count;
                upr.incorrectNumber = 0;
                upr.correctNumber = 0;
                upr.wordsCorrect = new List<Types.Word>();
                upr.wordsIncorrect = new List<Types.Word>();
                writeDatabase(dB, set, upr);
            }
            NewRound(dB, CC, upr, set);
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

        public static void NewRound(DB dB, CustomConsole CC, Types.UserSetProgress usp, Types.Set set)
        {
            var running = true;

            //var roundNum = usp.rounds;
            //
            //var correct = usp.correctNumber;
            //
            //var incorrect = usp.incorrectNumber;
            //var todo = usp.remainingNumber;

            var wtotal = usp.wordsRemaining.Count;

            var ran = new Random();

            RenderTopbar(set, usp.rounds, usp.correctNumber, usp.incorrectNumber, usp.remainingNumber);

            bool showMotivationScreen = false;

            while (running) //TODO: better mistake system
            {

                if (wtotal % 2 == 0)
                {
                    var c = wtotal;
                    if (usp.remainingNumber == (c / 2))
                    {
                        showMotivationScreen = true;
                    }
                }
                else
                {
                    var c = wtotal + 1;
                    if (usp.remainingNumber == (c / 2))
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
                    RenderTopbar(set, usp.rounds, usp.correctNumber, usp.incorrectNumber, usp.remainingNumber);
                }

                if (usp.wordsRemaining.Count > 0)
                {
                    var wordv = usp.wordsRemaining[ran.Next(usp.wordsRemaining.Count)];
                    if (usp.hasExited)
                    {
                        int h = Console.CursorTop;
                        int l = Console.CursorLeft;
                        Console.SetCursorPosition(0, Console.BufferHeight - 4);
                        wordv = usp.currentWord;
                        usp.hasExited = false;
                        writeDatabase(dB, set, usp);
                        Console.Write("Override");
                        Console.SetCursorPosition(l, h);
                    }
                    var word = (usp.setSettings.reverseDefintions ? wordv.definition : wordv.word);
                    var def = (usp.setSettings.reverseDefintions ? wordv.word : wordv.definition);
                    Console.WriteLine("\n" + word + "\nType the translation and press [ENTER].");
                    int CPos = Console.CursorTop;
                    Console.Write("> ");
                    if (CC.acceptInputUntilEnterWriteAnswer(word, def))
                    {
                        // save
                        usp.currentWord = wordv;
                        usp.hasExited = true;
                        writeDatabase(dB, set, usp);
                        Console.Clear();
                        return;
                    }
                    if (isAMistake(CC.getInput(), def, usp))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nCorrect!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        usp.wordsRemaining.Remove(wordv);
                        usp.remainingNumber--;
                        usp.correctNumber++;
                        writeDatabase(dB, set, usp);
                        Thread.Sleep(1000);
                        CC.clearInput();
                        Console.Clear();
                        RenderTopbar(set, usp.rounds, usp.correctNumber, usp.incorrectNumber, usp.remainingNumber);
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
                            usp.wordsRemaining.Remove(wordv);
                            usp.remainingNumber--;
                            usp.correctNumber++;
                            writeDatabase(dB, set, usp);
                            //Thread.Sleep(1000);
                        }
                        else
                        {
                            CC.clearInput();
                            usp.wordsRemaining.Remove(wordv);
                            usp.wordsIncorrect.Add(wordv);
                            usp.remainingNumber--;
                            usp.incorrectNumber++;
                            writeDatabase(dB, set, usp);
                            if (usp.setSettings.showCorrectionDialogue)
                            {
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
                                    usp.currentWord = wordv;
                                    usp.hasExited = true;
                                    writeDatabase(dB, set, usp);
                                    Console.Clear();
                                    return;
                                }
                                CC.clearInput();
                            }
                            Thread.Sleep(1000);
                        }
                        Console.Clear();
                        RenderTopbar(set, usp.rounds, usp.correctNumber, usp.incorrectNumber, usp.remainingNumber);
                    }
                }
                else
                {
                    if (usp.wordsIncorrect.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nRound completed, you did very good!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Press any key to continue to the next round.");
                        Console.ReadKey(true);
                        Console.Clear();
                        running = false;
                        usp.rounds++;
                        writeDatabase(dB, set, usp);
                        NewRound(dB, CC, usp, set);
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nAll words completed! Good job!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(2000);
                        Console.Clear();
                        usp.setOngoing = false;
                        writeDatabase(dB, set, usp);
                        running = false;
                    }
                }
            }
        }
    }
}
