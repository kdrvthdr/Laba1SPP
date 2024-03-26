using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace lab1
{
    public interface IResultSerializer
    {
        string Serialize(TraceResult traceResult);
    }

    public class JsonSerializer : IResultSerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult, Newtonsoft.Json.Formatting.Indented);
        }
    }

    public class XmlSerializer : IResultSerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TraceResult));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, traceResult);
                return stringWriter.ToString();
            }
        }
    }
}
