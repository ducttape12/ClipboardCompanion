﻿using System;
using System.IO;
using System.Xml.Serialization;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;

namespace ClipboardCompanion.Persistence
{
    public class Persistence : IPersistence
    {
        private bool _loaded;

        private const string ParentDirectory = "KeithOtt.com";
        private const string SaveDirectory = "ClipboardCompanion";
        private const string SaveFileName = "configuration.xml";

        private CompanionModelCollection _companionModelCollection = new CompanionModelCollection();

        public void Save(TextCleanerCompanionModel model)
        {
            _companionModelCollection.TextCleanerCompanionModel = model;
            Save();
        }

        public void Save(GuidCreatorCompanionModel model)
        {
            _companionModelCollection.GuidCreatorCompanionModel = model;
            Save();
        }

        public void Save(OptionsModel model)
        {
            _companionModelCollection.OptionsModel = model;
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
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ParentDirectory, SaveDirectory);

        public TextCleanerCompanionModel TextCleanerCompanionModel
        {
            get
            {
                LoadCompanionModelCollection();
                return _companionModelCollection.TextCleanerCompanionModel;
            }
        }

        public GuidCreatorCompanionModel GuidCreatorCompanionModel
        {
            get
            {
                LoadCompanionModelCollection();
                return _companionModelCollection.GuidCreatorCompanionModel;
            }
        }

        public OptionsModel OptionsModel
        {
            get
            {
                LoadCompanionModelCollection();
                return _companionModelCollection.OptionsModel;
            }
        }

        private void LoadCompanionModelCollection()
        {
            if (!_loaded && File.Exists(FullSavePath))
            {
                var serializer = new XmlSerializer(typeof(CompanionModelCollection));

                try
                {
                    using (var reader = new FileStream(FullSavePath, FileMode.Open))
                    {
                        _companionModelCollection = (CompanionModelCollection) serializer.Deserialize(reader);
                    }
                }
                catch
                {
                   _companionModelCollection = new CompanionModelCollection(); 
                }
            }
            _loaded = true;
        }
    }
}