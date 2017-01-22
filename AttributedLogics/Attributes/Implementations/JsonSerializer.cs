using AttributedLogics.Attributes.Contracts;

namespace AttributedLogics.Attributes.Implementations
{
    public class JsonSerializer : IDataSerializer
    {
        public string SerializeAsString<T>(T data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        public T DeserializeFromString<T>(string input)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input);
        }
    }
}
