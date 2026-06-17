namespace TransformerTextToMusic
{
    public class ConsoleUI
    {
        private FileController fileController;
        private SettingsProgram settingsProgram;
        private PolyphonyRAM polyphonyRAM;

        private Constants constants;
        
        private bool running;
        private byte indexWindows;
        private byte indexInstrument;

        private List<MenuItem> actionsMain;
        private List<MenuItem> actionsSettings;
        private List<MenuItem> actionsSettingsSolo;
        private List<MenuItem> actionsSoloInstrumentsPage1;
        private List<MenuItem> actionsSoloInstrumentsPage2;
        private List<MenuItem> actionsSoloInstrumentsPage3;

        private List<MenuItem> actionsSettingsPolyphonyPage1;
        private List<MenuItem> actionsSettingsPolyphonyPage2;
        private List<MenuItem> actionsSettingsPolyphonyPage3;
        private List<MenuItem> actionsCountInstruments;
        private List<MenuItem> actionsPolyphonyInstrumentsPage1;
        private List<MenuItem> actionsPolyphonyInstrumentsPage2;
        private List<MenuItem> actionsPolyphonyInstrumentsPage3;

        //ScaleTrackTiming
        private List<MenuItem> actionsScaleTrackTiming;

        public ConsoleUI()
        {
            running = false;
            indexWindows = (byte)WindowID.Main;
            indexInstrument = 0;

            constants = new Constants();

            actionsMain = new List<MenuItem>()
            {
                new MenuItem("Tutorial",()=>DrowTutorial()),
                new MenuItem("Convert text to token",()=>fileController.ConvertTextToToken()),
                new MenuItem("Tokens to cache",()=>{
                    settingsProgram.SetSingleTrackCache(fileController.SplitTokensArrayAsByte(-1));
                    for (byte i = 0; i < 16; i++)
                    {
                        settingsProgram.SetTracksCacheForID(i, fileController.SplitTokensArrayAsByte((sbyte)i));
                    }
                    Console.WriteLine("Можете продовжувати роботу.");
                }),
                new MenuItem("Scale Track Timing",()=>{indexWindows=(byte)WindowID.ScaleTrackTiming; ShowMenu(); }),
                new MenuItem("Settings player",()=>{
                    indexWindows=(byte)WindowID.Settings;
                    ShowMenu();
                }),
                new MenuItem("Tokens to music",()=>polyphonyRAM.TokenToMusicAsync(settingsProgram.GetSingleInstrumentID(), settingsProgram.GetSingleTrack())),
                new MenuItem("Polyphony to music",()=>polyphonyRAM.PolyphonyToMusicAsync(settingsProgram.GetInstrumentsID(), settingsProgram.GetCountInstruments(), settingsProgram.GetTracksCache())),
                new MenuItem("Stop",()=>polyphonyRAM.StopPlay()),
                new MenuItem("Exit",()=>{ running = false; })
            };
            actionsSettings = new List<MenuItem>()
            {
                new MenuItem("Back to Menu",()=>{
                    indexWindows=(byte)WindowID.Main;
                    ShowMenu();
                }),
                new MenuItem("Single Instrument",()=>{
                    indexWindows=(byte)WindowID.SettingsSolo;
                    ShowMenu();
                }),
                new MenuItem("Polyphony Instruments",()=>{
                    indexWindows=(byte)WindowID.SettingsPolyphonyPage1;
                    ShowMenu();
                }),
                new MenuItem("Save Settings",()=>settingsProgram.SaveSettings()),
                new MenuItem("Load Settings",()=>settingsProgram.LoadSettings())
            };
            actionsSettingsSolo = new List<MenuItem>()
            {
                new MenuItem("Back",()=>{
                    indexWindows=(byte)WindowID.Settings;
                    ShowMenu();
                }),
                new MenuItem("New Single Instrument",()=>{
                    indexWindows=(byte)WindowID.InstrumentsPage1;
                    ShowMenu();
                })
            };

            actionsSoloInstrumentsPage1 = new List<MenuItem>()
            {
                new MenuItem("Back",()=>{
                    indexWindows=(byte)WindowID.SettingsSolo;
                    ShowMenu();
                }),
                new MenuItem("Next Page",()=>{
                    indexWindows=(byte)WindowID.InstrumentsPage2;
                    ShowMenu();
                }),
                new MenuItem("Instrument Bassoon",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundBassoon);ShowMenu(); }),
                new MenuItem("Instrument Cello",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundCello);ShowMenu(); }),
                new MenuItem("Instrument Chimes",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundChimes);ShowMenu(); }),
                new MenuItem("Instrument Choir",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundChoir);ShowMenu(); }),
                new MenuItem("Instrument Clarinet",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundClarinet);ShowMenu(); }),
                new MenuItem("Instrument DoubleBass",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundDoubleBass);ShowMenu(); }),
                new MenuItem("Instrument Flute",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundFlute);ShowMenu(); }),
                new MenuItem("Instrument Glockenspiel",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundGlockenspiel);ShowMenu(); })
            };
            actionsSoloInstrumentsPage2 = new List<MenuItem>()
            {
                new MenuItem("Back Page",()=>{
                    indexWindows=(byte)WindowID.InstrumentsPage1;
                    ShowMenu();
                }),
                new MenuItem("Next Page",()=>{
                    indexWindows=(byte)WindowID.InstrumentsPage3;
                    ShowMenu();
                }),
                new MenuItem("Instrument Harp",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundHarp);ShowMenu(); }),
                new MenuItem("Instrument Horn",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundHorn);ShowMenu(); }),
                new MenuItem("Instrument Oboe",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundOboe);ShowMenu(); }),
                new MenuItem("Instrument Percussion",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundPercussion);ShowMenu(); }),
                new MenuItem("Instrument Piano",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundPiano);ShowMenu(); }),
                new MenuItem("Instrument Timpani",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundTimpani);ShowMenu(); }),
                new MenuItem("Instrument Trombone",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundTrombone);ShowMenu(); }),
                new MenuItem("Instrument Trumpet",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundTrumpet);ShowMenu(); })
            };
            actionsSoloInstrumentsPage3 = new List<MenuItem>()
            {
                new MenuItem("Back Page",()=>{
                    indexWindows=(byte)WindowID.InstrumentsPage2;
                    ShowMenu();
                }),
                new MenuItem("Instrument Tuba",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundTuba);ShowMenu(); }),
                new MenuItem("Instrument Viola",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundViola);ShowMenu(); }),
                new MenuItem("Instrument Violin",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundViolin);ShowMenu(); }),
                new MenuItem("Instrument Xylophone",()=>{settingsProgram.SetSingleInstrumentID((byte)InstrumentID.SoundXylophone);ShowMenu(); })
            };

            actionsSettingsPolyphonyPage1 = new List<MenuItem>()
            {
                new MenuItem("Back",()=>{
                    indexWindows=(byte)WindowID.Settings;
                    ShowMenu();
                }),
                new MenuItem("Next Page",()=>{
                    indexWindows=(byte)WindowID.SettingsPolyphonyPage2;
                    ShowMenu();
                }),
                new MenuItem("Count Instruments",()=>{indexWindows=(byte)WindowID.CountInstruments; ShowMenu();}),
                new MenuItem("Track 00",()=>{indexInstrument=0; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 01",()=>{indexInstrument=1; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 02",()=>{indexInstrument=2; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 03",()=>{indexInstrument=3; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 04",()=>{indexInstrument=4; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 05",()=>{indexInstrument=5; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();})
            };
            actionsSettingsPolyphonyPage2 = new List<MenuItem>()
            {
                new MenuItem("Back Page",()=>{
                    indexWindows=(byte)WindowID.SettingsPolyphonyPage1;
                    ShowMenu();
                }),
                new MenuItem("Next Page",()=>{
                    indexWindows=(byte)WindowID.SettingsPolyphonyPage3;
                    ShowMenu();
                }),
                new MenuItem("Track 06",()=>{indexInstrument=6; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 07",()=>{indexInstrument=7; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 08",()=>{indexInstrument=8; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 09",()=>{indexInstrument=9; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 10",()=>{indexInstrument=10; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 11",()=>{indexInstrument=11; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 12",()=>{indexInstrument=12; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();})
            };
            actionsSettingsPolyphonyPage3 = new List<MenuItem>()
            {
                new MenuItem("Back Page",()=>{
                    indexWindows=(byte)WindowID.SettingsPolyphonyPage2;
                    ShowMenu();
                }),
                new MenuItem("Track 13",()=>{indexInstrument=13; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 14",()=>{indexInstrument=14; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();}),
                new MenuItem("Track 15",()=>{indexInstrument=15; indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1; ShowMenu();})
            };
            actionsCountInstruments = new List<MenuItem>()
            {
                new MenuItem("Back",()=>{
                    indexWindows=(byte)WindowID.Settings;
                    ShowMenu();
                }),
                new MenuItem("Add Instruments",()=>DrowCountInstruments(1)),
                new MenuItem("Sub Instruments",()=>DrowCountInstruments(2))
            };

            actionsPolyphonyInstrumentsPage1 = new List<MenuItem>()
            {
                new MenuItem("Back",()=>{
                    if(indexInstrument < 6)
                    {
                        indexWindows=(byte)WindowID.SettingsPolyphonyPage1;
                    }
                    else if(indexInstrument < 13)
                    {
                        indexWindows=(byte)WindowID.SettingsPolyphonyPage2;
                    }
                    else
                    {
                        indexWindows=(byte)WindowID.SettingsPolyphonyPage3;
                    }
                    ShowMenu();
                }),
                new MenuItem("Next Page",()=>{
                    indexWindows=(byte)WindowID.PolyphonyInstrumentsPage2;
                    ShowMenu();
                }),
                new MenuItem("Instrument Bassoon",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundBassoon);ShowMenu(); }),
                new MenuItem("Instrument Cello",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundCello);ShowMenu(); }),
                new MenuItem("Instrument Chimes",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundChimes);ShowMenu(); }),
                new MenuItem("Instrument Choir",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundChoir);ShowMenu(); }),
                new MenuItem("Instrument Clarinet",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundClarinet);ShowMenu(); }),
                new MenuItem("Instrument DoubleBass",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundDoubleBass);ShowMenu(); }),
                new MenuItem("Instrument Flute",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundFlute);ShowMenu(); }),
                new MenuItem("Instrument Glockenspiel",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundGlockenspiel);ShowMenu(); })
            };
            actionsPolyphonyInstrumentsPage2 = new List<MenuItem>()
            {
                new MenuItem("Back Page",()=>{
                    indexWindows=(byte)WindowID.PolyphonyInstrumentsPage1;
                    ShowMenu();
                }),
                new MenuItem("Next Page",()=>{
                    indexWindows=(byte)WindowID.PolyphonyInstrumentsPage3;
                    ShowMenu();
                }),
                new MenuItem("Instrument Harp",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundHarp);ShowMenu(); }),
                new MenuItem("Instrument Horn",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundHorn);ShowMenu(); }),
                new MenuItem("Instrument Oboe",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundOboe);ShowMenu(); }),
                new MenuItem("Instrument Percussion",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundPercussion);ShowMenu(); }),
                new MenuItem("Instrument Piano",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundPiano);ShowMenu(); }),
                new MenuItem("Instrument Timpani",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundTimpani);ShowMenu(); }),
                new MenuItem("Instrument Trombone",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundTrombone);ShowMenu(); }),
                new MenuItem("Instrument Trumpet",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundTrumpet);ShowMenu(); })
            };
            actionsPolyphonyInstrumentsPage3 = new List<MenuItem>()
            {
                new MenuItem("Back Page",()=>{
                    indexWindows=(byte)WindowID.PolyphonyInstrumentsPage2;
                    ShowMenu();
                }),
                new MenuItem("Instrument Tuba",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundTuba);ShowMenu(); }),
                new MenuItem("Instrument Viola",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundViola);ShowMenu(); }),
                new MenuItem("Instrument Violin",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundViolin);ShowMenu(); }),
                new MenuItem("Instrument Xylophone",()=>{settingsProgram.SetInstrumentForID(indexInstrument,(byte)InstrumentID.SoundXylophone);ShowMenu(); })
            };

            actionsScaleTrackTiming = new List<MenuItem>()
            {
                new MenuItem("Back to Menu",()=>{
                    indexWindows=(byte)WindowID.Main;
                    ShowMenu();
                }),
                new MenuItem("Note 1/8 (250 milliseconds)",()=>{fileController.ScaleTrackTiming(0); }),
                new MenuItem("Note 1/4 (500 milliseconds)",()=>{fileController.ScaleTrackTiming(1); }),
                new MenuItem("Note 1/2 (1000 milliseconds)",()=>{fileController.ScaleTrackTiming(3); }),
                new MenuItem("Note 1/1 (2000 milliseconds)",()=>{fileController.ScaleTrackTiming(7); })
            };
        }
        public void Run()
        {
            running = true;
            Console.Clear();
            indexWindows = (byte)WindowID.Main;

            ShowMenu();

            while (running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.D1: PressKey(1); break;
                        case ConsoleKey.D2: PressKey(2); break;
                        case ConsoleKey.D3: PressKey(3); break;
                        case ConsoleKey.D4: PressKey(4); break;
                        case ConsoleKey.D5: PressKey(5); break;
                        case ConsoleKey.D6: PressKey(6); break;
                        case ConsoleKey.D7: PressKey(7); break;
                        case ConsoleKey.D8: PressKey(8); break;
                        case ConsoleKey.D9: PressKey(9); break;
                        case ConsoleKey.D0: PressKey(0); break;

                        case ConsoleKey.Enter: ShowMenu(); break;
                    }
                }
                Thread.Sleep(10);
            }
        }
        public void InitParam(FileController fileController, SettingsProgram settingsProgram, PolyphonyRAM polyphonyRAM)
        {
            this.fileController = fileController;
            this.settingsProgram = settingsProgram;
            this.polyphonyRAM = polyphonyRAM;
            settingsProgram.SetFileSetting(fileController);
        }
        
        private void PressKey(byte number)
        {
            switch (indexWindows)
            {
                case (byte)WindowID.Main:
                    if(number < actionsMain.Count)
                        actionsMain[number].GetAction()();
                    break;
                case (byte)WindowID.Settings:
                    if (number < actionsSettings.Count) 
                        actionsSettings[number].GetAction()();
                    break;
                case (byte)WindowID.SettingsSolo:
                    if (number < actionsSettingsSolo.Count)
                        actionsSettingsSolo[number].GetAction()();
                    break;
                case (byte)WindowID.InstrumentsPage1:
                    if (number < actionsSoloInstrumentsPage1.Count)
                        actionsSoloInstrumentsPage1[number].GetAction()();
                    break;
                case (byte)WindowID.InstrumentsPage2:
                    if (number < actionsSoloInstrumentsPage2.Count)
                        actionsSoloInstrumentsPage2[number].GetAction()();
                    break;
                case (byte)WindowID.InstrumentsPage3:
                    if (number < actionsSoloInstrumentsPage3.Count)
                        actionsSoloInstrumentsPage3[number].GetAction()();
                    break;

                case (byte)WindowID.SettingsPolyphonyPage1:
                    if (number < actionsSettingsPolyphonyPage1.Count)
                        actionsSettingsPolyphonyPage1[number].GetAction()();
                    break;
                case (byte)WindowID.SettingsPolyphonyPage2:
                    if (number < actionsSettingsPolyphonyPage2.Count)
                        actionsSettingsPolyphonyPage2[number].GetAction()();
                    break;
                case (byte)WindowID.SettingsPolyphonyPage3:
                    if (number < actionsSettingsPolyphonyPage3.Count)
                        actionsSettingsPolyphonyPage3[number].GetAction()();
                    break;

                case (byte)WindowID.CountInstruments:
                    if (number < actionsCountInstruments.Count)
                        actionsCountInstruments[number].GetAction()();
                    break;

                case (byte)WindowID.PolyphonyInstrumentsPage1:
                    if (number < actionsPolyphonyInstrumentsPage1.Count)
                        actionsPolyphonyInstrumentsPage1[number].GetAction()();
                    break;
                case (byte)WindowID.PolyphonyInstrumentsPage2:
                    if (number < actionsPolyphonyInstrumentsPage2.Count)
                        actionsPolyphonyInstrumentsPage2[number].GetAction()();
                    break;
                case (byte)WindowID.PolyphonyInstrumentsPage3:
                    if (number < actionsPolyphonyInstrumentsPage3.Count)
                        actionsPolyphonyInstrumentsPage3[number].GetAction()();
                    break;

                case (byte)WindowID.ScaleTrackTiming:
                    if (number < actionsScaleTrackTiming.Count)
                        actionsScaleTrackTiming[number].GetAction()();
                    break;
            }
        }

        private void ShowMenu()
        {
            switch(indexWindows)
            {
                case (byte)WindowID.Main: DrowMenuMain(); break;
                case (byte)WindowID.Tutorial: DrowTutorial(); break;
                case (byte)WindowID.Settings: DrowSettings(); break;
                case (byte)WindowID.SettingsSolo: DrowSettingsSolo(); break;
                case (byte)WindowID.InstrumentsPage1: DrowSettingsInstruments(1); break;
                case (byte)WindowID.InstrumentsPage2: DrowSettingsInstruments(2); break;
                case (byte)WindowID.InstrumentsPage3: DrowSettingsInstruments(3); break;

                case (byte)WindowID.SettingsPolyphonyPage1: DrowSettingsPolyphony(1); break;
                case (byte)WindowID.SettingsPolyphonyPage2: DrowSettingsPolyphony(2); break;
                case (byte)WindowID.SettingsPolyphonyPage3: DrowSettingsPolyphony(3); break;
                case (byte)WindowID.CountInstruments: DrowCountInstruments(0); break;
                case (byte)WindowID.PolyphonyInstrumentsPage1: DrowPolyphonyInstruments(1); break;
                case (byte)WindowID.PolyphonyInstrumentsPage2: DrowPolyphonyInstruments(2); break;
                case (byte)WindowID.PolyphonyInstrumentsPage3: DrowPolyphonyInstruments(3); break;

                case (byte)WindowID.ScaleTrackTiming: DrowScaleTrackTiming(); break;
            }
        }
        private void DrowScaleTrackTiming()
        {
            Console.WriteLine("\n=== Menu/Scale Track Timing ===\n");
            Console.WriteLine("За замовчуванням використовується 1/8 ноти.");
            Console.WriteLine("Варіанти додають тишу між токенами, що полегшить адаптацію токенів під тривалість звуків конкретного інструмента.");
            Console.WriteLine("Результат перемістіть до потрібного файлу. Результат записаний у файлі 'Files/longToken.txt'.\n");
            for (byte i = 0; i < actionsScaleTrackTiming.Count; i++)
            {
                Console.WriteLine($"{i} - {actionsScaleTrackTiming[i].GetText()}");
            }
        }
        private void DrowPolyphonyInstruments(byte page)
        {
            Console.WriteLine($"\n=== Menu/Settings/Polyphony Instruments/Track{indexInstrument.ToString("D2")}/Page{page} ===\n");
            Console.WriteLine($"Track{indexInstrument.ToString("D2")}: {constants.nameInsruments[settingsProgram.GetInstrumentForID(indexInstrument)]}\n");
            switch (page)
            {
                case 1:
                    for (byte i = 0; i < actionsPolyphonyInstrumentsPage1.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsPolyphonyInstrumentsPage1[i].GetText()}");
                    }
                    break;
                case 2:
                    for (byte i = 0; i < actionsPolyphonyInstrumentsPage2.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsPolyphonyInstrumentsPage2[i].GetText()}");
                    }
                    break;
                case 3:
                    for (byte i = 0; i < actionsPolyphonyInstrumentsPage3.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsPolyphonyInstrumentsPage3[i].GetText()}");
                    }
                    break;
            }
        }
        private void DrowCountInstruments(byte mode)
        {
            switch(mode)
            {
                case 0:
                    Console.WriteLine($"\n=== Menu/Settings/Polyphony Instruments/Count Instruments ===\n");
                    Console.WriteLine($"Встановіть кількість інструментів, які гратимуть при поліфонії.\n");
                    for (byte i = 0; i < actionsCountInstruments.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsCountInstruments[i].GetText()}");
                    }
                    Console.WriteLine($"\nCount = {settingsProgram.GetCountInstruments().ToString("D2")}");
                    break;
                case 1:
                    if (settingsProgram.GetCountInstruments() >= settingsProgram.GetInstrumentsID().Count())
                    {
                        settingsProgram.SetCountInstruments(1);
                    }
                    else
                    {
                        settingsProgram.SetCountInstruments((byte)(settingsProgram.GetCountInstruments() + 1));
                    }
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                    Console.WriteLine($"\nCount = {settingsProgram.GetCountInstruments().ToString("D2")}");
                    break;
                case 2:
                    if (settingsProgram.GetCountInstruments() <= 1)
                    {
                        settingsProgram.SetCountInstruments((byte)settingsProgram.GetInstrumentsID().Count());
                    }
                    else
                    {
                        settingsProgram.SetCountInstruments((byte)(settingsProgram.GetCountInstruments() - 1));
                    }
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
                    Console.WriteLine($"\nCount = {settingsProgram.GetCountInstruments().ToString("D2")}");
                    break;
            }
        }
        private void DrowSettingsPolyphony(byte page)
        {
            Console.WriteLine($"\n=== Menu/Settings/Polyphony Instruments/Page{page} ===\n");
            switch (page)
            {
                case 1:
                    Console.WriteLine($"{0} - {actionsSettingsPolyphonyPage1[0].GetText()}");
                    Console.WriteLine($"{1} - {actionsSettingsPolyphonyPage1[1].GetText()}");
                    Console.WriteLine($"{2} - {actionsSettingsPolyphonyPage1[2].GetText()} = {settingsProgram.GetCountInstruments()}");
                    for (byte i = 3; i < actionsSettingsPolyphonyPage1.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsSettingsPolyphonyPage1[i].GetText()} = {constants.nameInsruments[settingsProgram.GetInstrumentForID((byte)(i-3))]}");
                    }
                    break;
                case 2:
                    Console.WriteLine($"{0} - {actionsSettingsPolyphonyPage2[0].GetText()}");
                    Console.WriteLine($"{1} - {actionsSettingsPolyphonyPage2[1].GetText()}");
                    for (byte i = 2; i < actionsSettingsPolyphonyPage2.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsSettingsPolyphonyPage2[i].GetText()} = {constants.nameInsruments[settingsProgram.GetInstrumentForID((byte)(i+4))]}");
                    }
                    break;
                case 3:
                    Console.WriteLine($"{0} - {actionsSettingsPolyphonyPage3[0].GetText()}");
                    for (byte i = 1; i < actionsSettingsPolyphonyPage3.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsSettingsPolyphonyPage3[i].GetText()} = {constants.nameInsruments[settingsProgram.GetInstrumentForID((byte)(i+12))]}");
                    }
                    break;
            }
        }
        private void DrowSettingsInstruments(byte page)
        {
            Console.WriteLine($"\n=== Menu/Settings/Single Instrument/New Instrument/Page{page} ===\n");
            Console.WriteLine($"Поточний інструмент: {constants.nameInsruments[settingsProgram.GetSingleInstrumentID()]}\n");
            switch(page)
            {
                case 1:
                    for (byte i = 0; i < actionsSoloInstrumentsPage1.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsSoloInstrumentsPage1[i].GetText()}");
                    }
                    break;
                case 2:
                    for (byte i = 0; i < actionsSoloInstrumentsPage2.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsSoloInstrumentsPage2[i].GetText()}");
                    }
                    break;
                case 3:
                    for (byte i = 0; i < actionsSoloInstrumentsPage3.Count; i++)
                    {
                        Console.WriteLine($"{i} - {actionsSoloInstrumentsPage3[i].GetText()}");
                    }
                    break;
            }
        }
        private void DrowSettingsSolo()
        {
            Console.WriteLine("\n=== Menu/Settings/Single Instrument ===\n");
            Console.WriteLine("Режим: Tokens to music.\nФайл токенів: Files/token.txt");
            Console.WriteLine($"Поточний інструмент: {constants.nameInsruments[settingsProgram.GetSingleInstrumentID()]}\n");
            for (byte i = 0; i < actionsSettingsSolo.Count; i++)
            {
                Console.WriteLine($"{i} - {actionsSettingsSolo[i].GetText()}");
            }
        }
        private void DrowTutorial()
        {
            Console.WriteLine("\n=== Menu/Tutorial ===\n");
            Console.WriteLine("Автор: Yurii Ushynskyi.");
            Console.WriteLine("Програма розрахована на адекватного користувача.\n");
            Console.WriteLine("Вітаємо вас у програмі 'Transformer Text to Music'.");
            Console.WriteLine("Ця програма була розроблена для подолання проблеми із пошуком безкоштовних звуків для власних проектів.\n");
            Console.WriteLine("Ця програма працює за наступним принципом:");
            Console.WriteLine("* 'Convert text to token' - Перетворює український текст з файлу 'Files/input.txt' на токени, які зберігаються в 'Files/token.txt'.\n" +
                "Українська мова була взята за основу через практично ідеальну однозначність між літерою і звуком.\n" +
                "Англійська не підходила для цього через свою непослідовність, в той час як символьні коди викликали б спрацювання занадто великої кількості клавіш.\n" +
                "Усі сторонні символи заміняться на токен тиші");
            Console.WriteLine("* 'Tokens to cache' - Ви переміщуєте токени з усіх файлів ('Files/token.txt' і 16 txt-файлів з 'Files/Tracks') у кеш програми.\n" +
                "Якщо ви випадково зміните ці файли, то це не повпливає на кеш.\n" +
                "ОБОВ'ЯЗКОВО натискайте цю опцію перед запуском перетворення токенів у музику, бо для цього використовується саме закешовані токени.");
            Console.WriteLine("* 'Scale Track Timing' - Різні інструменти мають різну довжину звуку.\n" +
                "Програма завжди використовує повний звук, однак якщо в інструмента занадто протяжна нота, звуки накладатимуться.\n" +
                "Цей інструмент бере токени з 'Files/token.txt', додає обрану кількість токенів тиші і записує результат в 'Files/longToken.txt'.");
            Console.WriteLine("* 'Settings player' - Тут ви можете змінювати інструменти для кожного файлу окремо.");
            Console.WriteLine("* 'Tokens to music' - Перетворює токени з 'Files/token.txt' у музику. Якщо ви зміните цей файл, то натисніть 'Tokens to cache', для оновлення даних.");
            Console.WriteLine("* 'Polyphony to music' - Перетворює токени з 16 txt-файлів (з 'Files/Tracks') у музику.\n" +
                "Якщо ви зміните цей файл, то натисніть 'Tokens to cache', для оновлення даних.\n" +
                "Вам треба буде обрати кількість треків, які будуть грати одночасно в 'Settings player'.\n" +
                "Використовуючи 'Convert text to token', перенесіть утворені послідовності токенів до файлів у папці 'Files/Tracks'.");
            Console.WriteLine("* 'Stop' - Зупиняє відтворення музики.");
            Console.WriteLine("* 'Exit' - Вихід з програми.\n");

            Console.WriteLine("Для першого запуску рекомендуємо здійснити наступні дії:\n" +
                "* Записати свій український текст (числа варто переводити у словесну форму) у файл 'Files/input.txt' та зберігаємо зміни.\n" +
                "* Натискаємо на опцію 'Convert text to token', щоб наш текст був переписаний у токени в файл 'Files/token.txt'.\n" +
                "* Далі натисніть на опцію 'Tokens to cache', щоб усі файли з токенами завантажились у програму.\n" +
                "* Тепер натисніть на опцію 'Tokens to music' і насолоджуйтесь звучанням вашого тектсу.\n");

            Console.WriteLine("Якщо ви захочете змінити інструмент, тоді зробіть наступні дії:\n" +
                "* Натисність на опцію 'Settings player' -> 'Single Instrument' -> 'New Single Instrument' і оберіть бажаний інструмент.\n" +
                "* Повертайтеся в меню і заново прослуховуйте свою доріжку.\n" +
                "* Якщо звук накладається (деякі інструменти мають довгі ноти) то натисність на опцію 'Scale Track Timing' і оберіть бажану кількість часу між токенами.\n" +
                "* Ця опція читає токени з файлу 'Files/token.txt' і копіює їх зі змінами у файл 'Files/longToken.txt'.\n" +
                "* Скопіюйте токени з файлу 'Files/longToken.txt' і вставте у файл 'Files/token.txt'.\n" +
                "* Оскільки ви змінили файл, його треба перекешувати. Натисність опцію 'Tokens to cache' в меню.\n" +
                "* Тепер натисніть на опцію 'Tokens to music' і насолоджуйтесь звучанням вашого тектсу новим інструментом.\n");

            Console.WriteLine("Якщо ви захочете зробити цілий оркестр, тоді зробіть наступні дії:\n" +
                "* Якщо ви досі не знаєте як працювати з файлами програмами, рекомендуємо перечитати попередні інструкції (вище).\n" +
                "* Музика у форматі оркестру (декілька інструментів грає одночасно), тут називається 'поліфонія'.\n" +
                "* У програмі є 16 файлів для токенів, які знаходяться у папці 'Files/Tracks'. Вставте свої токени у ці файли.\n" +
                "* В опції меню 'Settings player' ви зможете контролювати кількість треків для поліфонії і обрати інструменти для кожного треку.\n" +
                "* Збережіть налаштування, щоб при повторному запуску програми, вам не довелося повторно все вручну набирати.\n" +
                "* Закешуйте файли (опція в меню 'Tokens to cache').\n" +
                "* Тепер натисніть в меню на опцію 'Polyphony to music' і насолоджуйтесь звучанням вашого оркестру.\n");

            Console.WriteLine("Варто зазначити, що повна нота триває 2 секунди, в той час як токени змінюються кожні 0.25 секунди. Враховуйте це при створенні композицій.\n" +
                "Якщо у вас під час відтворення накладаються звуки, то просто додайте кілька токенів тиші '00' у ваш токен-файл.");
            Console.WriteLine("Пам'ятайте, у вас завжди активне меню. Ви можете викликати його інтерфейс, просто натиснувши клавішу 'Enter'.");
            Console.WriteLine("Натисніть клавішу 'Enter', щоб знову побачити активне меню.");
        }
        private void DrowSettings()
        {
            Console.WriteLine("\n=== Menu/Settings ===\n");
            Console.WriteLine("'Single Instrument' - налаштування для соло інструмента, який використовується для режиму 'Tokens to music'");
            Console.WriteLine("'Polyphony Instruments' - налаштування для 16 інструментів, які використовується у режимі 'Polyphony to music'\n");
            for (byte i = 0; i < actionsSettings.Count; i++)
            {
                Console.WriteLine($"{i} - {actionsSettings[i].GetText()}");
            }
        }
        private void DrowMenuMain()
        {
            Console.WriteLine("\n=== Menu ===\n");
            for (byte i = 0; i < actionsMain.Count; i++)
            {
                Console.WriteLine($"{i} - {actionsMain[i].GetText()}");
            }
        }
    }
}
