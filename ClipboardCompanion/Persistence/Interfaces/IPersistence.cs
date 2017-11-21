using ClipboardCompanion.Persistence.Models;

namespace ClipboardCompanion.Persistence.Interfaces
{
    public interface IPersistence
    {
        void Save(TextCleanerCompanionModel model);
        void Save(GuidCreatorCompanionModel model);
        void Save(OptionsCompanionModel companionModel);
        TextCleanerCompanionModel TextCleanerCompanionModel { get; }
        GuidCreatorCompanionModel GuidCreatorCompanionModel { get; }
        OptionsCompanionModel OptionsCompanionModel { get; }
    }
}
