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
            container.Register<GuidCreatorCompanionViewModel>();
            container.Register<GuidCreatorControl>();
            container.Register<OptionsUserControl>();
            container.Register<TextCleanerCompanionViewModel>();
            container.Register<TextCleanerUserControl>();
            container.Register<MainWindowViewModel>();
            container.Register<CompanionSelector>();
            container.Register<CompanionSelectorViewModel>();

            container.RegisterSingleton<IHotKeyService, HotKeyService>();
            container.RegisterSingleton<IWindowsHotKeyApiService, WindowsHotKeyApiService>();
            container.RegisterSingleton<IWindowHandleService, WindowHandleService>();
            container.Register<INotificationService>(() => new NotificationService(Resources.ApplicationTitle));
            container.RegisterSingleton<IPersistence, Persistence.Persistence>();
            container.RegisterSingleton<ITrayIconService, TrayIconService>();

            container.Verify();

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
