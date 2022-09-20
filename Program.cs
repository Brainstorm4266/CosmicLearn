using System.Diagnostics;
using System.Text.Json;

namespace CosmicLearn
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("CosmicLearn initializing...");

            if (!File.Exists("config.json"))
            {
                Console.WriteLine("Creating config...");
                System.IO.File.WriteAllText("config.json",
@"{
    ""configVersion"": 0,
    ""databaseAddress"": ""mongodb://localhost:27017"",
    ""databaseName"": ""CosmicLearn"",
    ""databaseFile"": ""./CosmicLearn.db"",
    ""useLiteDb"": true
}
");
            }

            Console.WriteLine("Loading config...");
            string ConfigJson = File.ReadAllText("config.json");

            if (string.IsNullOrEmpty(ConfigJson))
            {
                Console.WriteLine("Error: Config is empty or failed to load. Deleting the file may fix this issue.");
                return;
            }

            Config conf;

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.AllowTrailingCommas = true;
                conf = JsonSerializer.Deserialize<Config>(ConfigJson, options);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Got exception while loading file. The config.json file is probably incorrectly formatted.");
                return;
            }

            DB dB;
            if (conf.useLiteDb)
            {
                dB = new DB(conf.databaseFile, false);
            } else
            {
                dB = new DB(conf.databaseAddress, true);
            }

            dB.init(conf.databaseName);

            var CC = new CustomConsole();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("CosmicLearn v1.0.0\n" +
                    "Options:\n" +
                    "[C] Import/Create new set\n" +
                    "[E] Edit set\n" +
                    "[L] Learn set\n" +
                    "[Esc] Exit\n");
                Console.Write("Pick an option: ");
                ConsoleKey[] whitelist = { ConsoleKey.C, ConsoleKey.E, ConsoleKey.L, ConsoleKey.Escape };
                CC.acceptSingleInput(whitelist, true);
                if (CC.getConsoleKey() == ConsoleKey.C)
                {
                    CC.clearInput();

                    System.IO.File.WriteAllText("temp.txt", "Delete this text and paste your set here!\nLines are seperated by Enter, and words/definitions by Tab.\nClose this window to continue.");

                    Console.WriteLine("\nA text window will now open. Write your set in there, and close it when done.");

                    var proc = Process.Start(new ProcessStartInfo { FileName = "temp.txt", UseShellExecute = true });

                    if (proc is null)
                    {
                        Console.WriteLine("Failed to start process");
                        File.Delete("temp.txt");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        proc.WaitForExit();
                        string upset = File.ReadAllText("temp.txt");
                        List<Types.Word> words = new List<Types.Word>();

                        try
                        {
                            var lines = upset.Split('\n');

                            foreach (string line in lines)
                            {
                                var word = new Types.Word();
                                var e = line.Split('\t');
                                word.word = e[0];
                                word.definition = e[1];
                                words.Add(word);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error while reading. Does your set follow the format?");
                            File.Delete("temp.txt");
                            Thread.Sleep(2000);
                            continue;
                        }
                        Console.Write("\n");
                        var confirm = false;
                        string name = "";
                        while (!confirm)
                        {
                            Console.Write("Type a name for the set: ");
                            CC.acceptInputUntilEnter();
                            name = CC.getInput();
                            CC.clearInput();
                            Console.WriteLine("\nSet Name: " + name);
                            Console.Write("Please confirm your choice of name (y/n): ");
                            string[] yesorno = { "y", "n" };
                            CC.acceptSingleInput(yesorno, false);
                            if (CC.getInput() == "y")
                            {
                                CC.clearInput();
                                Console.Write("\n\n");
                                confirm = true;
                            }
                            else
                            {
                                CC.clearInput();
                                Console.Write("\n\n");
                                confirm = false;
                            }
                        }

                        var set = new Types.Set();
                        set.words = words;
                        set.name = name;
                        set.description = "";
                        set.wordlang = "";
                        set.deflang = "";

                        var setId = dB.newSet(set);

                        Console.WriteLine("Created new set with id " + setId + "!");
                        File.Delete("temp.txt");
                        Thread.Sleep(2000);
                    }
                }
                else if (CC.getConsoleKey() == ConsoleKey.Escape)
                {
                    CC.clearInput();
                    exit = true;
                }
                else if (CC.getConsoleKey() == ConsoleKey.L)
                {
                    LearnSet.run(dB, CC);
                }
            }
        }
    }
}