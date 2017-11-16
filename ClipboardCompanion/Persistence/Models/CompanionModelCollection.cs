using ClipboardCompanion.Persistance.Models;

namespace ClipboardCompanion.Persistence.Models
{
    public class CompanionModelCollection
    {
        public TextCleanerCompanionModel TextCleanerCompanionModel { get; set; } = new TextCleanerCompanionModel();
        public GuidCreatorCompanionModel GuidCreatorCompanionModel { get; set; } = new GuidCreatorCompanionModel();
        public OptionsModel OptionsModel { get; set; } = new OptionsModel();
    }
}
