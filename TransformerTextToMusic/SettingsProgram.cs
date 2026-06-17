namespace TransformerTextToMusic
{
    public class SettingsProgram
    {
        private byte singleInstrumentID;
        private byte[] instrumentsID = new byte[16];

        private Track singleCache;
        private Track[] tracksCache = new Track[16];

        private byte countInstruments;
        private FileController settingsFile;

        public SettingsProgram()
        {
            singleCache = new Track();
            singleInstrumentID = (byte)InstrumentID.SoundHarp;
            for (byte i=0;i<16;i++)
            {
                instrumentsID[i] = (byte)InstrumentID.SoundHarp;
                tracksCache[i] = new Track();
            }
            countInstruments = 3;
        }
        public byte GetSingleInstrumentID() { return singleInstrumentID; }
        public void SetSingleInstrumentID(byte instrumentID) { this.singleInstrumentID = instrumentID; }
        public byte[] GetInstrumentsID() {  return instrumentsID; }
        public void SetInstrumentsID(byte[] instrumentsID) { this.instrumentsID = instrumentsID; }
        public byte GetInstrumentForID(byte ID) { return instrumentsID[ID]; }
        public void SetInstrumentForID(byte ID, byte instrumentID) { instrumentsID[ID] = instrumentID; }

        public Track GetSingleTrack() { return singleCache; }
        public void SetSingleTrackCache(byte[] bufferTokens) { singleCache.SetBufferTokens(bufferTokens); }
        public Track[] GetTracksCache() { return tracksCache; }
        public void SetTracksCacheForID(byte ID, byte[] bufferTokens) { tracksCache[ID].SetBufferTokens(bufferTokens); }

        public byte GetCountInstruments() { return countInstruments; }
        public void SetCountInstruments(byte count) { countInstruments = count; }

        public void SetFileSetting(FileController file)
        {
            settingsFile = file;
        }

        public void SaveSettings()
        {
            settingsFile.SaveSettings(singleInstrumentID, countInstruments, instrumentsID);
        }
        public void LoadSettings()
        {
            settingsFile.LoadSettings(out singleInstrumentID, out countInstruments, out instrumentsID);
        }
    }
}
