namespace AttributedLogics.Attributes.Contracts
{
    public interface IDataSerializer
    {
        string SerializeAsString<T>(T data);
        T DeserializeFromString<T>(string input);
    }

    public interface IDataProtector
    {
        U Protect<T, U>(T input);
        T UnProtect<T, U>(U input);
    }
}
