using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClipboardCompanion.Persistance.Interfaces;
using ClipboardCompanion.Persistance.Models;

namespace ClipboardCompanion.Persistance
{
    public class CompanionPersistance : ICompanionPersistance
    {
        private bool _loaded = false;

        private const string SaveDirectory = "ClipboardCompanion";
        private const string SaveFileName = "configuration.xml";

        private CompanionModelCollection _companionModelCollection = new CompanionModelCollection();

        public void Save(TextCleanerCompanionModel model)
        {
            _companionModelCollection.TextCleanerCompanionModel = model;
            Save();
        }

        private void Save()
        {
            Directory.CreateDirectory(FullSaveDirectory);

            var serializer = new XmlSerializer(typeof(CompanionModelCollection));
            using (var writer = new StreamWriter(FullSavePath))
            {
                serializer.Serialize(writer, _companionModelCollection);
            }
        }

        private static string FullSavePath => Path.Combine(FullSaveDirectory, SaveFileName);

        private static string FullSaveDirectory =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), SaveDirectory);

        public TextCleanerCompanionModel TextCleanerCompanionModel
        {
            get
            {
                LoadCompanionModelCollection();
                return _companionModelCollection.TextCleanerCompanionModel;
            }
        }

        private void LoadCompanionModelCollection()
        {
            if (!_loaded && File.Exists(FullSavePath))
            {
                var serializer = new XmlSerializer(typeof(CompanionPersistance));

                using (var reader = new FileStream(FullSavePath, FileMode.Open))
                {
                    _companionModelCollection = (CompanionModelCollection)serializer.Deserialize(reader);
                }
            }
            _loaded = true;
        }
    }
}
