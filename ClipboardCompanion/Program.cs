using System;
using ClipboardCompanion.Persistance;
using ClipboardCompanion.Persistance.Interfaces;
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
            container.Register<TextCleanerCompanionViewModel>();
            container.Register<TextCleanerCompanionControl>();
            container.Register<MainWindowViewModel>();

            container.RegisterSingleton<IHotKeyService, HotKeyService>();
            container.RegisterSingleton<IWindowsHotKeyApiService, WindowsHotKeyApiService>();
            container.RegisterSingleton<IWindowHandleService, WindowHandleService>();
            container.Register<INotificationService>(() => new NotificationService(Resources.ApplicationTitle));
            container.RegisterSingleton<ICompanionPersistance, CompanionPersistance>();

            container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
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
