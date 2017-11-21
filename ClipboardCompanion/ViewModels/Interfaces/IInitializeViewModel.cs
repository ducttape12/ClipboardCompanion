namespace ClipboardCompanion.ViewModels.Interfaces
{
    public interface IInitializeViewModel
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
