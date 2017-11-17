namespace ClipboardCompanion.Persistence.Models
{
    public class OptionsModel
    {
        public bool AlwaysShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool StartMinimized { get; set; }

        public OptionsModel()
        {
            AlwaysShowTrayIcon = true;
            MinimizeToTray = true;
            StartMinimized = true;
        }
    }
}
