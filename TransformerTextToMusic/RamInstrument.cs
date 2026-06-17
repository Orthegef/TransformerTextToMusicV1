using NAudio.Wave;

namespace TransformerTextToMusic
{
    public class RamInstrument : IDisposable
    {
        private float[] audioData;      //тут лежать всі дані аудіофайлу
        private WaveFormat waveFormat;  //формат звуку

        //Завантаження файлу у RAM
        public void LoadToRAM(string filePath)
        {
            using (AudioFileReader reader = new AudioFileReader(filePath))
            {
                waveFormat=reader.WaveFormat;

                //тимчасовий контейнер для копіювання всього аудіофайлу
                List<float> buffer = new List<float>();

                //тимчасовий масив для читання шматками
                float[] temp = new float[reader.WaveFormat.SampleRate];

                //читаємо файл до кінця і копіюємо в RAM
                int read;
                while((read = reader.Read(temp, 0, temp.Length)) > 0)
                {
                    for(int i=0;i<read;i++)
                    {
                        buffer.Add(temp[i]);
                    }
                }

                //трансформація List<float> => float[]
                audioData=buffer.ToArray();
            }
            Console.WriteLine("File load to RAM.");
        }

        //Створення ноти-фрагмента
        public ISampleProvider CreateNote(double startSec,double endSec)
        {
            if (audioData == null)
                return null;

            //кількість samples в секунду. Вони взагалі не можуть бути дробовими.
            int samplesPerSecond = waveFormat.SampleRate * waveFormat.Channels;

            //конвертація секунд в індекси масиву. Спотворення мінімальне.
            int startSample = (int)(startSec * samplesPerSecond);
            int endSample = (int)(endSec * samplesPerSecond);

            //захист від виходу за межі [0, audioData.Length]
            startSample = Math.Clamp(startSample, 0, audioData.Length);
            endSample = Math.Clamp(endSample, 0, audioData.Length);

            //довжина ноти
            int length = endSample - startSample;
            if (length <= 0)
                return null;

            //створення копії ноти
            float[]noteData=new float[length];
            Array.Copy(audioData, startSample, noteData, 0, length);

            return new CachedSampleProvider(noteData, waveFormat);
        }
        public void Dispose()
        {
            audioData = null;
            waveFormat = null;
            Console.WriteLine("RAM cleaned.");
        }
    }
}
