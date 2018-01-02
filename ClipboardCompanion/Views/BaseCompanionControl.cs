using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public abstract class BaseCompanionControl : UserControl
    {
        protected abstract IInitializeViewModel ViewModel { get; }

        private bool _initialized;

        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass", Justification = "Must be public for XAML designer to function.")]
        public BaseCompanionControl()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!_initialized)
            {
                ViewModel.Initialize();
                DataContext = ViewModel;
                _initialized = true;
            }
        }
    }
}
