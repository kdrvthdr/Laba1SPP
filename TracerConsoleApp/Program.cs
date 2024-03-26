using System;
using lab1;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта трассировки
            ITracer tracer = new Tracer();

            // Создание объекта Foo
            Foo foo = new Foo(tracer);

            // Вызов метода MyMethod
            foo.MyMethod();

            // Получение результатов трассировки
            TraceResult traceResult = tracer.GetTraceResult();

            // Сериализация результатов в JSON
            IResultSerializer jsonSerializer = new JsonSerializer();
            string jsonResult = jsonSerializer.Serialize(traceResult);

            // Вывод результатов в консоль
            Console.WriteLine("JSON Result:");
            Console.WriteLine(jsonResult);

            // Запись результатов в файл JSONResult.json
            string jsonFilePath = "JSONResult.json";
            WriteToFile(jsonFilePath, jsonResult);
            Console.WriteLine($"JSON result has been written to {jsonFilePath}");

            // Сериализация результатов в XML
            IResultSerializer xmlSerializer = new XmlSerializer();
            string xmlResult = xmlSerializer.Serialize(traceResult);

            // Вывод результатов в консоль
            Console.WriteLine("\nXML Result:");
            Console.WriteLine(xmlResult);

            // Запись результатов в файл XMLResult.xml
            string xmlFilePath = "XMLResult.xml";
            WriteToFile(xmlFilePath, xmlResult);
            Console.WriteLine($"XML result has been written to {xmlFilePath}");
        }


        static void WriteToFile(string filePath, string content)
        {
            try
            {
                // Записываем содержимое в файл
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Results have been written to {filePath}");
            }
            catch (Exception ex)
            {
                // Обработка исключений при записи в файл
                Console.WriteLine($"An error occurred while writing to {filePath}: {ex.Message}");
            }
        }


    }
}
