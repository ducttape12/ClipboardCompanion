using ClipboardCompanion.Persistence.Models;

namespace ClipboardCompanion.Persistence.Interfaces
{
    public interface IPersistence
    {
        void Save(TextCleanerCompanionModel companionModel);
        void Save(GuidCreatorCompanionModel companionModel);
        void Save(OptionsCompanionModel companionModel);
        void Save(XmlFormatterCompanionModel companionModel);
        TextCleanerCompanionModel TextCleanerCompanionModel { get; }
        GuidCreatorCompanionModel GuidCreatorCompanionModel { get; }
        OptionsCompanionModel OptionsCompanionModel { get; }
        XmlFormatterCompanionModel XmlFormatterCompanionModel { get; }
    }
}
