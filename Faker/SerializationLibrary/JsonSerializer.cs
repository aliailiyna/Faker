using Newtonsoft.Json;

namespace SerializationLibrary
{
    public class JsonSerializer: ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        public string GetName()
        {
            return "json";
        }
    }
}
