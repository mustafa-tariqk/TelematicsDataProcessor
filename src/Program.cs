using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter the input file path: ");
        string filePath = Console.ReadLine();

        Console.Write("Output files directory: ");
        string outputDirectory = Console.ReadLine();
        
        var processor = new DataProcessor();
        Task live = processor.StreamingInput(filePath);
        Console.WriteLine("Live data streaming started...");

        while (true)
        {
            Console.Write("r to generate report, any other key to exit: ");
            string command = Console.ReadLine();
            if (command != "r")
            {
                break;
            }
            else 
            {
                var reporter = new Reporter();
                string fileName = reporter.SaveToFile(processor.vehicleDataStore, outputDirectory);
                Console.WriteLine($"Report generated: {fileName}");
            }
        }
    }
}