using System;
using ClipboardCompanion.Converters;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Properties;
using ClipboardCompanion.Services;
using ClipboardCompanion.Services.Interfaces;
using ClipboardCompanion.ViewModels;
using ClipboardCompanion.Views;
using SimpleInjector;

namespace ClipboardCompanion
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = Bootstrap();

            RunApplication(container);
        }

        private static Container Bootstrap()
        {
            var container = new Container();

            container.Register<MainWindow>();
            container.Register<MainWindowViewModel>();
            container.Register<CompanionSelector>();
            container.Register<CompanionSelectorViewModel>();

            container.Register<GuidCreatorControl>();
            container.Register<GuidCreatorCompanionViewModel>();

            container.Register<TextCleanerControl>();
            container.Register<TextCleanerCompanionViewModel>();

            container.Register<XmlFormatterCompanionViewModel>();
            container.Register<XmlFormatterCompanionControl>();

            container.Register<JsonFormatterCompanionViewModel>();
            container.Register<JsonFormatterCompanionControl>();

            container.RegisterSingleton<IHotKeyService, HotKeyService>();
            container.RegisterSingleton<IWindowsHotKeyApiService, WindowsHotKeyApiService>();
            container.Register<INotificationService>(() => new NotificationService(Resources.ApplicationName));
            container.RegisterSingleton<IPersistence, Persistence.Persistence>();
            container.RegisterSingleton<ITrayIconService, TrayIconService>();

            // TODO: Since Verify() creates instances of everything and later we create instances again, this creates duplicate companions that try to register the same shortcut.  Move this to an automated test class.
            //container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                app.Resources.Add("FalseToVisibleConverter", new FalseToVisibleConverter());
                app.Resources.Add("TrueToVisibleConverter", new TrueToVisibleConverter());
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch (Exception)
            {
                //Log the exception and exit
            }
        }
    }
}
