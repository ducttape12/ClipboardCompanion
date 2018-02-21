using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Properties;
using ClipboardCompanion.Services;
using ClipboardCompanion.Services.Interfaces;
using ClipboardCompanion.ViewModels;
using ClipboardCompanion.Views;
using SimpleInjector;

namespace ClipboardCompanion
{
    public static class DependencyRegistration
    {
        public static Container Register()
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
            container.RegisterSingleton<IApplicationLifecycleService, ApplicationLifecycleServiceService>();

            return container;
        }
    }
}
