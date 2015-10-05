using Library;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            RunLabs runLabs = new RunLabs();
            runLabs.Run(args, new Server(), new Client());
        }
    }
}