using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public class BaseCompanionControl : UserControl
    {
        protected IInitializeViewModel ViewModel { get; set; }

        private bool _initialized;

        public BaseCompanionControl(IInitializeViewModel viewModel)
        {
            ViewModel = viewModel;

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
