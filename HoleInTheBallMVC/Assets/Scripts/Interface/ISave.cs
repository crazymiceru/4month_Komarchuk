namespace Hole
{
    internal interface ISave
    {
        public void Save<T>(T data, string name);
        public T Load<T>(string name);
    }
}