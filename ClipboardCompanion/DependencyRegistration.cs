using ClipboardCompanion.Persistence;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
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
            container.Register<IPersistence<OptionsCompanionModel>, Persistence<OptionsCompanionModel>>();

            container.Register<CompanionSelector>();
            container.Register<CompanionSelectorViewModel>();

            container.Register<GuidCreatorControl>();
            container.Register<GuidCreatorCompanionViewModel>();
            container.Register<IPersistence<GuidCreatorCompanionModel>, Persistence<GuidCreatorCompanionModel>>();

            container.Register<TextCleanerControl>();
            container.Register<TextCleanerCompanionViewModel>();
            container.Register<IPersistence<TextCleanerCompanionModel>, Persistence<TextCleanerCompanionModel>>();

            container.Register<XmlFormatterCompanionViewModel>();
            container.Register<XmlFormatterCompanionControl>();
            container.Register<IPersistence<XmlFormatterCompanionModel>, Persistence<XmlFormatterCompanionModel>>();

            container.Register<JsonFormatterCompanionViewModel>();
            container.Register<JsonFormatterCompanionControl>();
            container.Register<IPersistence<JsonFormatterCompanionModel>, Persistence<JsonFormatterCompanionModel>>();

            container.RegisterSingleton<IHotKeyService, HotKeyService>();
            container.RegisterSingleton<IWindowsHotKeyApiService, WindowsHotKeyApiService>();
            container.Register<INotificationService>(() => new NotificationService(Resources.ApplicationName));
            container.RegisterSingleton<ITrayIconService, TrayIconService>();
            container.RegisterSingleton<IApplicationLifecycleService, ApplicationLifecycleServiceService>();

            return container;
        }
    }
}
