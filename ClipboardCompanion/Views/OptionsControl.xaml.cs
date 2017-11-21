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
    /// Interaction logic for OptionsControl.xaml
    /// </summary>
    public partial class OptionsControl : UserControl
    {
        private readonly OptionsViewModel _optionsViewModel;

        private bool _initialized;

        public OptionsControl(OptionsViewModel optionsViewModel)
        {
            InitializeComponent();
            _optionsViewModel = optionsViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
            {
                _optionsViewModel.Initialize();
                DataContext = _optionsViewModel;
                _initialized = true;
            }
        }
    }
}
