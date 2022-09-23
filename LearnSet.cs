namespace CosmicLearn
{
    internal class LearnSet
    {
        public static void setSettings(DB dB, CustomConsole CC, Types.Set set)
        {
            CC.clearInput();
            ConsoleKey[] whitelist = { ConsoleKey.Y, ConsoleKey.N };
            Console.Write("Would you like to edit set settings before continuing? (y/n) ");
            CC.acceptSingleInput(whitelist, false);
            if (CC.getInput().ToLower() == "y") //TODO: get user data to pre-set these values
            {
                var userData = dB.getUserData();
                if (userData is null)
                {
                    Console.Write("Failed to get data! (UserData not found). Try restarting CosmicLearn");
                    Thread.Sleep(10000);
                    return;
                }
                else
                {
                    bool setSettings = false;
                    for (int o = 0; o < userData.progresses.Count; o++)
                    {
                        var setProgress = userData.progresses[o];
                        if (setProgress.setId == set.setId)
                        {
                            setSettings = true;
                        }
                    }

                    if (!setSettings)
                    {
                        // make new data
                        userData.progresses.Add(new Types.UserSetProgress
                        {
                            setId = set.setId,
                            setSettings = new Types.SetSettings(),
                            rounds = 0,
                            correctNumber = 0,
                            incorrectNumber = 0,
                            remainingNumber = 0,
                            currentWord = new Types.Word(),
                            wordsCorrect = new List<Types.Word>(),
                            wordsIncorrect = new List<Types.Word>(),
                            wordsRemaining = new List<Types.Word>(),
                            setOngoing = false
                        });
                    }

                    dB.setUserData(userData);
                }
                CC.clearInput();
                Console.Clear();
                bool done = false;
                int selectedOption = 0;
                var settings = new Types.SetSettings();
                foreach (var prog in userData.progresses)
                {
                    if (prog.setId == set.setId)
                    {
                        settings = prog.setSettings;
                    }
                }
                bool saveSettings = false;
                while (!done)
                {
                    Console.Clear();
                    string[] options = {
                        "[] Reverse Definitions: "+(settings.reverseDefintions? "Yes":"No"),
                        "[] Strict Mode: "+(settings.strictMode? "Yes":"No"),
                        "[] Show correction dialogue: "+(settings.showCorrectionDialogue? "Yes":"No"),
                        "[] Save settings",
                        "[] Discard settings"
                    };
                    for (int i = 0; i < options.Length; i++)
                    {
                        if (selectedOption == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(options[i] + "\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        } else
                        {
                            Console.Write(options[i]+"\n");
                        }
                    }
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (selectedOption < options.Length)
                            {
                                selectedOption++;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (selectedOption != 0)
                            {
                                selectedOption--;
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            switch (selectedOption)
                            {
                                case 0:
                                    settings.reverseDefintions = settings.reverseDefintions ? false : true;
                                    break;
                                case 1:
                                    settings.strictMode = settings.strictMode ? false : true;
                                    break;
                                case 2:
                                    settings.showCorrectionDialogue = settings.showCorrectionDialogue ? false : true;
                                    break;
                                case 3:
                                    saveSettings = true;
                                    done = true;
                                    break;
                                case 4:
                                    saveSettings = false;
                                    done = true;
                                    break;
                            }
                            break;
                    }
                }
                if (saveSettings)
                {
                    userData = dB.getUserData();
                    if (userData is null)
                    {
                        Console.Write("Failed saving data! (UserData not found). Try restarting CosmicLearn");
                        Thread.Sleep(10000);
                        return;
                    } else
                    {
                        bool setSettings = false;
                        for (int i = 0; i < userData.progresses.Count; i++)
                        {
                            var setProgress = userData.progresses[i];
                            if (setProgress.setId == set.setId)
                            {
                                userData.progresses[i].setSettings = settings;
                                setSettings = true;
                            }
                        }

                        if (!setSettings)
                        {
                            // make new data
                            userData.progresses.Add(new Types.UserSetProgress
                            {
                                setId = set.setId,
                                setSettings = new Types.SetSettings(),
                                rounds = 0,
                                correctNumber = 0,
                                incorrectNumber = 0,
                                remainingNumber = 0,
                                currentWord = new Types.Word(),
                                wordsCorrect = new List<Types.Word>(),
                                wordsIncorrect = new List<Types.Word>(),
                                wordsRemaining = new List<Types.Word>(),
                                setOngoing = false
                            });
                        }

                        dB.setUserData(userData);
                    }
                }
                Console.Clear();
            } else if (CC.getInput().ToLower() == "n")
            {
                CC.clearInput();
                Console.Clear();
                return;
            }
        }

        public static void run(DB dB, CustomConsole CC) //TODO: set settings
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
                        setSettings(dB, CC, sets[(page * 10) + selectedSet]);
                        var userData = dB.getUserData();
                        if (userData is null)
                        {
                            Console.Write("Failed to get data! (UserData not found). Try restarting CosmicLearn");
                            Thread.Sleep(10000);
                            return;
                        }
                        else
                        {
                            bool setSettings = false;
                            for (int o = 0; o < userData.progresses.Count; o++)
                            {
                                var setProgress = userData.progresses[o];
                                if (setProgress.setId == sets[(page * 10) + selectedSet].setId)
                                {
                                    setSettings = true;
                                }
                            }

                            if (!setSettings)
                            {
                                // make new data
                                userData.progresses.Add(new Types.UserSetProgress
                                {
                                    setId = sets[(page * 10) + selectedSet].setId,
                                    setSettings = new Types.SetSettings(),
                                    rounds = 0,
                                    correctNumber = 0,
                                    incorrectNumber = 0,
                                    remainingNumber = 0,
                                    currentWord = new Types.Word(),
                                    wordsCorrect = new List<Types.Word>(),
                                    wordsIncorrect = new List<Types.Word>(),
                                    wordsRemaining = new List<Types.Word>(),
                                    setOngoing = false
                                });
                            }

                            dB.setUserData(userData);
                        }
                        WriteMode.Write(dB, CC, sets[(page * 10) + selectedSet]);
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
