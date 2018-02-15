namespace ClipboardCompanion.Persistence.Models
{
    public class CompanionModelCollection
    {
        public TextCleanerCompanionModel TextCleanerCompanionModel { get; set; } = new TextCleanerCompanionModel();
        public GuidCreatorCompanionModel GuidCreatorCompanionModel { get; set; } = new GuidCreatorCompanionModel();
        public OptionsCompanionModel OptionsCompanionModel { get; set; } = new OptionsCompanionModel();
        public XmlFormatterCompanionModel XmlFormatterCompanionModel { get; set; } = new XmlFormatterCompanionModel();
    }
}
