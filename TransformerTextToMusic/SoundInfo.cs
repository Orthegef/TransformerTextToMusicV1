namespace TransformerTextToMusic
{
    public class SoundInfo
    {
        public double start;
        public double end;
        public SoundInfo(double start, double end)
        {
            this.start = start;
            this.end = end;
        }
        public SoundInfo()
        {
            start = 0.0;
            end = 0.0;
        }
    }
}
