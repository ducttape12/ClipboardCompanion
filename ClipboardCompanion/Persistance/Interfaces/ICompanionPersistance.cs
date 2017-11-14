using ClipboardCompanion.Persistance.Models;

namespace ClipboardCompanion.Persistance.Interfaces
{
    public interface ICompanionPersistance
    {
        void Save(TextCleanerCompanionModel model);
        TextCleanerCompanionModel TextCleanerCompanionModel { get; }
        GuidCreatorCompanionModel GuidCreatorCompanionModel { get; }
    }
}
