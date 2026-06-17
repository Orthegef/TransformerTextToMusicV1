namespace TransformerTextToMusic
{
    public class MenuItem
    {
        private string text;
        private Action action;

        public MenuItem(string text, Action action)
        {
            this.text = text;
            this.action = action;
        }
        public string GetText()
        {
            return text;
        }
        public Action GetAction()
        {
            return action;
        }
    }
}
