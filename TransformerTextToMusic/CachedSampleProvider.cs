using NAudio.Wave;

namespace TransformerTextToMusic
{
    public class CachedSampleProvider : ISampleProvider
    {
        private float[] data;   //дані про ноту
        private int position;   //поточна позиція
        public WaveFormat WaveFormat { get; }   //формат звуку

        public CachedSampleProvider(float[] data,WaveFormat waveFormat)
        {
            this.data = data;
            this.WaveFormat = waveFormat;
        }

        public int Read(float[] buffer,int offset, int count)
        {
            //скільки samples ще доступно
            int available = data.Length - position;

            //скільки реально копіюємо
            int toCopy = Math.Min(available, count);

            //копіюємо samples для мікшера
            Array.Copy(data, position, buffer, offset, toCopy);

            //рухаємось вперед
            position += toCopy;

            //при 0 мікшер зрозуміє, що звук закінчився
            return toCopy;
        }
    }
}
