namespace SerializationLibrary
{
    public interface ISerializer
    {
        string Serialize(object obj);
        string GetName();
    }
}
