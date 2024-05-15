using System;
using System.IO;

class DataProcessor
{
    public void ProcessData(string filePath)
    {
        // Read the CSV file
        string[] lines = File.ReadAllLines(filePath);

        // Print each line
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
    }
}