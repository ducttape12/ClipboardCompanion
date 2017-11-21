using ClipboardCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClipboardCompanion.Views
{
    /// <summary>
    /// Interaction logic for OptionsCompanionControl.xaml
    /// </summary>
    public partial class OptionsCompanionControl : UserControl
    {
        private readonly OptionsCompanionViewModel _optionsCompanionViewModel;

        private bool _initialized;

        public OptionsCompanionControl(OptionsCompanionViewModel optionsCompanionViewModel)
        {
            InitializeComponent();
            _optionsCompanionViewModel = optionsCompanionViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
            {
                _optionsCompanionViewModel.Initialize();
                DataContext = _optionsCompanionViewModel;
                _initialized = true;
            }
        }
    }
}
