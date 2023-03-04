namespace PlatoUI.Reflection
{
    public interface IPrivateFields
    {
        object this[string name] { get; set; }

        T Get<T>(string name);
        void Set<T>(string name, T value);

    }
}
