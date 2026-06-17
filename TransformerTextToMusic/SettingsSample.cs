namespace TransformerTextToMusic
{
    public class SettingsSample
    {
        private int start;          //початкове положення ітератора
        private int step;           //крок
        private int iterator;       //ітератор
        private byte instrumentID;  //ID інструмента
        public SettingsSample()
        {
            start = 0;
            step = 0;
            iterator = start;
            instrumentID = 0;
        }
        public SettingsSample(int start, int step, byte instrumentID)
        {
            this.step = step;
            this.start=start;
            iterator = this.start;
            this.instrumentID = instrumentID;
        }
        public bool NextStep()
        {
            if(iterator<step)
            {
                iterator++;
                return false;
            }
            else
            {
                iterator = 0;
                return true;
            }
        }
        public void StartPosition()
        {
            iterator = start;
        }
        public int GetStartIterator() { return start; }
        public int GetStep() { return step; }
        public int GetIterator() { return iterator; }
        public byte GetInstrumentID() { return instrumentID; }
    }
}
