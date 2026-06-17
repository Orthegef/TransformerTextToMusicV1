using System.Text;

namespace TransformerTextToMusic
{
    public class FileController
    {
        //Зовнішні файли
        private string folderPathFiles = "Files/";
        private string folderPathTracks = "Files/Tracks/";
        private string inputPath = "input.txt";
        private string tokenPath = "token.txt";
        private string settingsPath = "settings.txt";
        private string longTokenPath = "longToken.txt";
        
        //Словник символ -> нота
        private Constants constants = new Constants();

        private int maxTokenLenght => constants.tokenMap.Keys.Max(k => k.Length);
        public FileController()
        {
            //Створення файлу, якщо не існує
            FileCreate($"{folderPathFiles}{inputPath}");
            FileCreate($"{folderPathFiles}{tokenPath}");
            FileCreate($"{folderPathFiles}{settingsPath}");
            FileCreate($"{folderPathFiles}{longTokenPath}");

            //Створення файлів для поліфонії
            for (byte i=0;i<16;i++)
            {
                FileCreate($"{folderPathTracks}{constants.trackFiles[i]}");
            }

            //Перевірка на порожній файл
            if (IsFileEmpty($"{folderPathFiles}{inputPath}"))
            {
                Console.WriteLine($"Файл {folderPathFiles}{inputPath} порожній.");
            }
        }
        //Створює новий файл розширений токенами тиші
        public void ScaleTrackTiming(byte sountSilents)
        {
            string[] buffer;
            int i = 0;
            using (StreamReader reader = new StreamReader($"{folderPathFiles}{tokenPath}", Encoding.UTF8))
            {
                buffer = reader.ReadToEnd().Split(' ');
            }
            using (StreamWriter writer = new StreamWriter($"{folderPathFiles}{longTokenPath}", false, Encoding.UTF8)) //false = перезапис файлу, а не доповнення
            {
                if(buffer != null && buffer.Length > 0)
                {
                    while (i < buffer.Length)
                    {
                        writer.Write(buffer[i] + " ");
                        for (byte j = 0; j < sountSilents; j++)
                        {
                            writer.Write($"{constants.tokenMap[" "]} ");
                        }
                        i++;
                    }
                }
                writer.Write(constants.tokenMap[" "]);
            }
            Console.WriteLine($"Додані токени тиші. Оновлені токени зберігаються у файлі {folderPathFiles}{longTokenPath}");
        }
        //Створення файлу
        public void FileCreate(string path)
        {
            //отримуємо папку з шляху
            string? directory = Path.GetDirectoryName(path);

            //створюємо папку якщо її нема
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            //створити файл, якщо його не існує
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "", Encoding.UTF8);
            }
        }

        //Перевірка на порожність
        private bool IsFileEmpty(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Length == 0;
        }

        //Завантаження налаштувань з файлу
        public void LoadSettings(out byte instrumentSoloID, out byte count, out byte[] instrumentsID)
        {
            string[] buffer;
            using (StreamReader reader=new StreamReader($"{folderPathFiles}{settingsPath}", Encoding.UTF8))
            {
                instrumentSoloID = byte.Parse(reader.ReadLine());
                count = byte.Parse(reader.ReadLine());
                buffer = reader.ReadLine().Split(' ');
                instrumentsID = new byte[buffer.Length - 1];
                for (byte i=0;i< buffer.Length - 1; i++)
                {
                    instrumentsID[i] = byte.Parse(buffer[i]);
                }
                Console.WriteLine("Load Settings complete");
            }
        }
        //Збереження налаштувань в файл
        public void SaveSettings(byte instrumentSoloID, byte count, byte[] instrumentsID)
        {
            using (StreamWriter writer = new StreamWriter($"{folderPathFiles}{settingsPath}", false, Encoding.UTF8)) //false = перезапис файлу, а не доповнення
            {
                writer.WriteLine(instrumentSoloID);
                writer.WriteLine(count);
                for (byte i=0;i<instrumentsID.Count();i++)
                {
                    writer.Write($"{instrumentsID[i]} ");
                }
                Console.WriteLine("Save Settings complete");
            }
        }

        //Перетворення тексту в токени.
        public void ConvertTextToToken()
        {
            string text = File.ReadAllText($"{folderPathFiles}{inputPath}", Encoding.UTF8);
            text = text.ToLower();
            int i = 0;
            string tokenFound, sub, code;
            using (StreamWriter writer = new StreamWriter($"{folderPathFiles}{tokenPath}", false, Encoding.UTF8)) //false = перезапис файлу, а не доповнення
            {
                while (i < text.Length)
                {
                    tokenFound = null;
                    //Шукаємо найдовший токен
                    for (int len = maxTokenLenght; len > 0; len--)
                    {
                        //перевірка виходу за межі
                        if (i + len > text.Length)
                            continue;

                        //виймаємо шматок рядка на перевірку
                        sub = text.Substring(i, len);
                        if (constants.tokenMap.ContainsKey(sub))
                        {
                            tokenFound = sub;
                            break;
                        }
                    }
                    if (tokenFound != null)
                    {
                        code = constants.tokenMap[tokenFound];
                        writer.Write(code + " ");   //записуємо токен у файл
                        i += tokenFound.Length;
                    }
                    else
                    {
                        writer.Write("");         //записуємо токен невідомого символа у файл
                        i++;
                    }
                }
                writer.Write(constants.tokenMap[" "]);
            }
            Console.WriteLine($"Текст переписаний в токени. Вони зберігаються у файлі {folderPathFiles}{tokenPath}");
        }

        //Буферизація токенів
        public byte[] SplitTokensArrayAsByte(sbyte trackID)
        {
            byte[] array;
            string[] buffer;
            byte token;
            if (trackID == -1)
            {
                using (StreamReader reader = new StreamReader($"{folderPathFiles}{tokenPath}", Encoding.UTF8))
                {
                    buffer = reader.ReadToEnd().Split(' ');
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader($"{folderPathTracks}{constants.trackFiles[(byte)trackID]}", Encoding.UTF8))
                {
                    buffer = reader.ReadToEnd().Split(' ');
                }
            }
            array = new byte[buffer.Length];
            for(int i = 0; i < buffer.Length; i++)
            {
                if (!byte.TryParse(buffer[i], out token))
                    token = 0;
                array[i] = token;
            }
            Console.WriteLine("Буферизацію токенів завершено");
            return array;
        }

    }
}
