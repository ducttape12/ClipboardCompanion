using ClipboardCompanion.Persistence.Models;

namespace ClipboardCompanion.Persistence.Interfaces
{
    public interface IPersistence
    {
        void Save(TextCleanerCompanionModel model);
        void Save(GuidCreatorCompanionModel model);
        void Save(OptionsModel model);
        TextCleanerCompanionModel TextCleanerCompanionModel { get; }
        GuidCreatorCompanionModel GuidCreatorCompanionModel { get; }
        OptionsModel OptionsModel { get; }
    }
}
