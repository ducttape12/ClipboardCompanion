using System;
using ClipboardCompanion.Converters;
using SimpleInjector;

namespace ClipboardCompanion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = DependencyRegistration.Register();
            RunApplication(container);
        }

        private static void RunApplication(Container container)
        {
            var app = new App();
            app.Resources.Add("FalseToVisibleConverter", new FalseToVisibleConverter());
            app.Resources.Add("TrueToVisibleConverter", new TrueToVisibleConverter());
            var mainWindow = container.GetInstance<MainWindow>();
            app.Run(mainWindow);
        }
    }
}
