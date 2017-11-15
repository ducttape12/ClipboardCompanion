using ClipboardCompanion.Persistance.Models;

namespace ClipboardCompanion.Persistance.Interfaces
{
    public interface ICompanionPersistence
    {
        void Save(TextCleanerCompanionModel model);
        void Save(GuidCreatorCompanionModel model);
        void Save(OptionsModel model);
        TextCleanerCompanionModel TextCleanerCompanionModel { get; }
        GuidCreatorCompanionModel GuidCreatorCompanionModel { get; }
        OptionsModel OptionsModel { get; }
    }
}
