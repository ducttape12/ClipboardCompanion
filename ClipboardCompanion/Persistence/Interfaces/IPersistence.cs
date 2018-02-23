namespace ClipboardCompanion.Persistence.Interfaces
{
    public interface IPersistence<T> where T:new()
    {
        void Save(T model);
        T Load();
    }
}
