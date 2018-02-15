using System;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class XmlFormatterCompanionViewModel : CompanionViewModelBase
    {
        private readonly IPersistence _persistance;
        private readonly INotificationService _notificationService;

        private bool _forceXmlDeclaration;
        public bool ForceXmlDeclaration
        {
            get => _forceXmlDeclaration;
            set
            {
                _forceXmlDeclaration = value;
                RaisePropertyChanged(nameof(ForceXmlDeclaration));
            }
        }

        private bool _attributesOnSeparateLines;
        public bool AttributesOnSeparateLines
        {
            get => _attributesOnSeparateLines;
            set
            {
                _attributesOnSeparateLines = value;
                RaisePropertyChanged(nameof(AttributesOnSeparateLines));
            }
        }

        public XmlFormatterCompanionViewModel(IHotKeyService hotKeyService, IPersistence persistance, INotificationService notificationService) :
            base(hotKeyService, persistance.XmlFormatterCompanionModel)
        {
            _persistance = persistance;
            _notificationService = notificationService;
        }

        public override Action HotKeyPressedAction =>
            () =>
                {
                    if (Clipboard.ContainsText())
                    {
                        FormatClipboardToXml();
                    }
                    else
                    {
                        _notificationService.ShowWarning("No text on clipboard to format.");
                    }
                };

        private void FormatClipboardToXml()
        {
            var text = Clipboard.GetText();

            if (TryParseXElement(text, out var xElement))
            {
                var formattedXml = new StringBuilder();

                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = !ForceXmlDeclaration,
                    Indent = true,
                    NewLineOnAttributes = AttributesOnSeparateLines
                };

                using (var xmlWriter = XmlWriter.Create(formattedXml, settings))
                {
                    xElement.Save(xmlWriter);
                }

                Clipboard.SetText(formattedXml.ToString());

                _notificationService.ShowMessage("XML on clipboard has been formatted.");
            }
            else
            {
                _notificationService.ShowError("Clipboard contains invalid XML. Unable to format.");
            }
        }

        private static bool TryParseXElement(string text, out XElement xElement)
        {
            try
            {
                xElement = XElement.Parse(text);
                return true;
            }
            catch (XmlException)
            {
                xElement = null;
                return false;
            }
        }

        protected override void SaveConfiguration()
        {
            if (IsInitialized)
            {
                _persistance.Save(new XmlFormatterCompanionModel
                {
                    IsEnabled = IsEnabled,
                    ShiftModifier = ShiftModifier,
                    ControlModifier = ControlModifier,
                    ForceXmlDeclaration = ForceXmlDeclaration,
                    AttributesOnSeparateLines = AttributesOnSeparateLines
                });
            }
        }
    }
}
