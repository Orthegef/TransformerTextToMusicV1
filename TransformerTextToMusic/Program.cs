using System.Text;

namespace TransformerTextToMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Program");

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            FileController fileController = new FileController();
            SettingsProgram settingsProgram = new SettingsProgram();
            ConsoleUI consoleUI = new ConsoleUI();
            using(PolyphonyRAM polyphonyRAM=new PolyphonyRAM())
            {
                consoleUI.InitParam(fileController, settingsProgram, polyphonyRAM);
                consoleUI.Run();
            }

            Console.WriteLine("Press Enter to exit");
            Console.Read();
        }
    }
    
}
