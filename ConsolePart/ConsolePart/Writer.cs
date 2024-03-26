using System;
using System.IO;
using Newtonsoft.Json; // Добавлено пространство имен для JsonConvert
using TracerLibrary;

namespace TracerConsoleApp
{
    public interface IResultWriter
    {
        void Write(string content);
    }

    public class ConsoleResultWriter : IResultWriter
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }

    public class FileResultWriter : IResultWriter
    {
        private readonly string _filePath;

        public FileResultWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string content)
        {
            try
            {
                // Записываем содержимое в файл
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    writer.Write(content);
                }
                Console.WriteLine($"Results have been written to {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to {_filePath}: {ex.Message}");
            }
        }
    }
}
