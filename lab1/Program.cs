using Library;

namespace lab1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            RunLabs runLabs = new RunLabs();
            runLabs.Run(args, new Server(), new Client());
        }
    }
}
