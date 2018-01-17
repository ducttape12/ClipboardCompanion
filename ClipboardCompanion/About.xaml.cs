using System.Diagnostics;
using System.Windows;

namespace ClipboardCompanion
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void VisitWebsite_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(Properties.Resources.WebsiteLink));
            e.Handled = true;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
