namespace ClipboardCompanion.ViewModels.Interfaces
{
    public interface ICompanionViewModel : IInitializeViewModel
    {
        bool IsEnabled { get; }
    }
}
