namespace TransformerTextToMusic
{
    public enum WindowID:byte
    {
        Main=0,
        Tutorial=1,
        Settings=2,
        SettingsSolo=3,
        InstrumentsPage1=4,
        InstrumentsPage2=5,
        InstrumentsPage3=6,
        SettingsPolyphonyPage1=7,
        SettingsPolyphonyPage2=8,
        SettingsPolyphonyPage3=9,
        CountInstruments=10,
        PolyphonyInstrumentsPage1=11,
        PolyphonyInstrumentsPage2=12,
        PolyphonyInstrumentsPage3=13,
        ScaleTrackTiming=14
    }
    public enum InstrumentID: byte
    {
        SoundBassoon = 0,
        SoundCello = 1,
        SoundChimes = 2,
        SoundChoir = 3,
        SoundClarinet = 4,
        SoundDoubleBass = 5,
        SoundFlute = 6,
        SoundGlockenspiel = 7,
        SoundHarp = 8,
        SoundHorn = 9,
        SoundOboe = 10,
        SoundPercussion = 11,
        SoundPiano = 12,
        SoundTimpani = 13,
        SoundTrombone = 14,
        SoundTrumpet = 15,
        SoundTuba = 16,
        SoundViola = 17,
        SoundViolin = 18,
        SoundXylophone = 19
    }
    public class Constants
    {
        public readonly Dictionary<string, string> tokenMap;
        public readonly Dictionary<byte, SoundInfo> tokenToSoundMap;
        public readonly Dictionary<byte, string> trackFiles;
        public readonly Dictionary<byte, string> nameInsruments;
        public readonly string[] soundFiles;


        public Constants()
        {
            tokenMap = new Dictionary<string, string>()
        {
            {" ",  "00" },
            {"щ",  "01" },
            {"ш",  "02" },
            {"ч",  "03" },
            {"ць", "04" },
            {"ц",  "05" },
            {"ж",  "06" },
            {"зь", "07" },
            {"з",  "08" },
            {"сь", "09" },
            {"с",  "10" },

            {"дж", "11" },
            {"дзь","12" },
            {"дз", "13" },
            {"і",  "14" },
            {"ї",  "15" },
            {"й",  "16" },
            {"є",  "17" },
            {"у",  "18" },
            {"ю",  "19" },
            {"е",  "20" },

            {"о",  "21" },
            {"а",  "22" },
            {"я",  "23" },
            {"и",  "24" },
            {"ґ",  "25" },
            {"г",  "26" },
            {"к",  "27" },
            {"х",  "28" },
            {"рь", "29" },
            {"р",  "30" },

            {"ль", "31" },
            {"л",  "32" },
            {"м",  "33" },
            {"нь", "34" },
            {"н",  "35" },
            {"б",  "36" },
            {"п",  "37" },
            {"в",  "38" },
            {"ф",  "39" },
            {"дь", "40" },

            {"д",  "41" },
            {"ть", "42" },
            {"т",  "43" }
        };

            soundFiles =new string[]{
                "SoundBassoon.mp3",
                "SoundCello.mp3",
                "SoundChimes.mp3",
                "SoundChoir.mp3",
                "SoundClarinet.mp3",
                "SoundDoubleBass.mp3",
                "SoundFlute.mp3",
                "SoundGlockenspiel.mp3",
                "SoundHarp.mp3",
                "SoundHorn.mp3",
                "SoundOboe.mp3",
                "SoundPercussion.mp3",
                "SoundPiano.mp3",
                "SoundTimpani.mp3",
                "SoundTrombone.mp3",
                "SoundTrumpet.mp3",
                "SoundTuba.mp3",
                "SoundViola.mp3",
                "SoundViolin.mp3",
                "SoundXylophone.mp3"
            };
            trackFiles = new Dictionary<byte, string>()
            {
                {0, "track00.txt" },
                {1, "track01.txt" },
                {2, "track02.txt" },
                {3, "track03.txt" },
                {4, "track04.txt" },
                {5, "track05.txt" },
                {6, "track06.txt" },
                {7, "track07.txt" },
                {8, "track08.txt" },
                {9, "track09.txt" },
                {10,"track10.txt" },
                {11,"track11.txt" },
                {12,"track12.txt" },
                {13,"track13.txt" },
                {14,"track14.txt" },
                {15,"track15.txt" },
            };

            tokenToSoundMap = new Dictionary<byte, SoundInfo>()
            {
                {0,  new SoundInfo(86.0,88.0) },
                {1,  new SoundInfo(0.0,  2.0) },
                {2,  new SoundInfo(2.0,  4.0) },
                {3,  new SoundInfo(4.0,  6.0) },
                {4,  new SoundInfo(6.0,  8.0) },
                {5,  new SoundInfo(8.0, 10.0) },
                {6,  new SoundInfo(10.0,12.0) },
                {7,  new SoundInfo(12.0,14.0) },
                {8,  new SoundInfo(14.0,16.0) },
                {9,  new SoundInfo(16.0,18.0) },
                {10, new SoundInfo(18.0,20.0) },

                {11, new SoundInfo(20.0,22.0) },
                {12, new SoundInfo(22.0,24.0) },
                {13, new SoundInfo(24.0,26.0) },
                {14, new SoundInfo(26.0,28.0) },
                {15, new SoundInfo(28.0,30.0) },
                {16, new SoundInfo(30.0,32.0) },
                {17, new SoundInfo(32.0,34.0) },
                {18, new SoundInfo(34.0,36.0) },
                {19, new SoundInfo(36.0,38.0) },
                {20, new SoundInfo(38.0,40.0) },

                {21, new SoundInfo(40.0,42.0) },
                {22, new SoundInfo(42.0,44.0) },
                {23, new SoundInfo(44.0,46.0) },
                {24, new SoundInfo(46.0,48.0) },
                {25, new SoundInfo(48.0,50.0) },
                {26, new SoundInfo(50.0,52.0) },
                {27, new SoundInfo(52.0,54.0) },
                {28, new SoundInfo(54.0,56.0) },
                {29, new SoundInfo(56.0,58.0) },
                {30, new SoundInfo(58.0,60.0) },

                {31, new SoundInfo(60.0,62.0) },
                {32, new SoundInfo(62.0,64.0) },
                {33, new SoundInfo(64.0,66.0) },
                {34, new SoundInfo(66.0,68.0) },
                {35, new SoundInfo(68.0,70.0) },
                {36, new SoundInfo(70.0,72.0) },
                {37, new SoundInfo(72.0,74.0) },
                {38, new SoundInfo(74.0,76.0) },
                {39, new SoundInfo(76.0,78.0) },
                {40, new SoundInfo(78.0,80.0) },

                {41, new SoundInfo(80.0,82.0) },
                {42, new SoundInfo(82.0,84.0) },
                {43, new SoundInfo(84.0,86.0) }
            };

            nameInsruments = new Dictionary<byte, string>()
            {
                {(byte)InstrumentID.SoundBassoon, "Bassoon"},
                {(byte)InstrumentID.SoundCello, "Cello"},
                {(byte)InstrumentID.SoundChimes, "Chimes"},
                {(byte)InstrumentID.SoundChoir, "Choir"},
                {(byte)InstrumentID.SoundClarinet, "Clarinet"},
                {(byte)InstrumentID.SoundDoubleBass, "DoubleBass"},
                {(byte)InstrumentID.SoundFlute, "Flute"},
                {(byte)InstrumentID.SoundGlockenspiel, "Glockenspiel"},
                {(byte)InstrumentID.SoundHarp, "Harp"},
                {(byte)InstrumentID.SoundHorn, "Horn"},
                {(byte)InstrumentID.SoundOboe, "Oboe"},
                {(byte)InstrumentID.SoundPercussion, "Percussion"},
                {(byte)InstrumentID.SoundPiano, "Piano"},
                {(byte)InstrumentID.SoundTimpani, "Timpani"},
                {(byte)InstrumentID.SoundTrombone, "Trombone"},
                {(byte)InstrumentID.SoundTrumpet, "Trumpet"},
                {(byte)InstrumentID.SoundTuba, "Tuba"},
                {(byte)InstrumentID.SoundViola, "Viola"},
                {(byte)InstrumentID.SoundViolin, "Violin"},
                {(byte)InstrumentID.SoundXylophone, "Xylophone"}
            };
        }
    }
}
