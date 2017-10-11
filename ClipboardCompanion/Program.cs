using System;
using ClipboardCompanion.Services;
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
            
            container.Register<IHotKeyService, HotKeyService>();
            container.Register<IWindowsHotKeyApiService, WindowsHotKeyApiService>();
            container.RegisterSingleton<IWindowHandleService, WindowHandleService>();
            
            container.Register<MainWindow>();
            container.Register<GuidCreatorCompanionViewModel>();
            container.Register<GuidCreatorControl>();

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
