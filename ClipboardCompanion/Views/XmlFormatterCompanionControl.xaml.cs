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
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    /// <summary>
    /// Interaction logic for XmlFormatterCompanionControl.xaml
    /// </summary>
    public partial class XmlFormatterCompanionControl : BaseCompanionControl
    {
        public XmlFormatterCompanionControl(XmlFormatterCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }

        public override string Description => "XML Formatter";
    }
}
