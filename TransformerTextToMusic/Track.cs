namespace TransformerTextToMusic
{
    public class Track
    {
        private byte[]? bufferTokens;


        public byte[] GetBufferTokens()
        {
            if(bufferTokens!=null)
                return bufferTokens;
            else
                return new byte[1] { 0 };
        }
        public void SetBufferTokens(byte[] bufferTokens)
        {
            this.bufferTokens = bufferTokens;
        }
    }
}
