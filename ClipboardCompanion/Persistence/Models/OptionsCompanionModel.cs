namespace ClipboardCompanion.Persistence.Models
{
    public class OptionsCompanionModel
    {
        public bool AlwaysShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool StartMinimized { get; set; }

        public OptionsCompanionModel()
        {
            AlwaysShowTrayIcon = true;
            MinimizeToTray = true;
            StartMinimized = false;
        }
    }
}
