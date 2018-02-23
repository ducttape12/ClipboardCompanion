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
    public class XmlFormatterCompanionViewModel : CompanionViewModelBase<XmlFormatterCompanionModel>
    {
        public bool XmlDeclaration
        {
            get => Persistence.Load().XmlDeclaration;
            set
            {
                var model = Persistence.Load();
                model.XmlDeclaration = value;
                Persistence.Save(model);

                RaisePropertyChanged(nameof(XmlDeclaration));
            }
        }
        
        public bool AttributesOnSeparateLines
        {
            get => Persistence.Load().AttributesOnSeparateLines;
            set
            {
                var model = Persistence.Load();
                model.AttributesOnSeparateLines = value;
                Persistence.Save(model);

                RaisePropertyChanged(nameof(AttributesOnSeparateLines));
            }
        }

        public XmlFormatterCompanionViewModel(IHotKeyService hotKeyService, IPersistence<XmlFormatterCompanionModel> persistence,
            INotificationService notificationService) : base(hotKeyService, persistence, notificationService)
        {
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
                        NotificationService.ShowWarning("No text on clipboard to format.");
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
                    OmitXmlDeclaration = !XmlDeclaration,
                    Indent = true,
                    NewLineOnAttributes = AttributesOnSeparateLines
                };

                using (var xmlWriter = XmlWriter.Create(formattedXml, settings))
                {
                    xElement.Save(xmlWriter);
                }

                Clipboard.SetText(formattedXml.ToString());

                NotificationService.ShowMessage("XML on clipboard has been formatted.");
            }
            else
            {
                NotificationService.ShowError("Clipboard contains invalid XML. Unable to format.");
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
    }
}
