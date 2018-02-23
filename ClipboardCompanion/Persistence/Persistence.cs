using System;
using System.IO;
using System.Xml.Serialization;
using ClipboardCompanion.Persistence.Interfaces;

namespace ClipboardCompanion.Persistence
{
    public class Persistence<T> : IPersistence<T> where T:new()
    {
        private const string ParentDirectory = "KeithOtt.com";
        private const string SaveDirectory = "ClipboardCompanion";
        private static string SaveFileName => $"{typeof(T)}Configuration.xml";

        private static string FullSavePath => Path.Combine(FullSaveDirectory, SaveFileName);

        private static string FullSaveDirectory =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ParentDirectory, SaveDirectory);
        
        public void Save(T model)
        {
            Directory.CreateDirectory(FullSaveDirectory);

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(FullSavePath))
            {
                serializer.Serialize(writer, model);
            }
        }

        public T Load()
        {
            var model = new T();

            if (File.Exists(FullSavePath))
            {
                var serializer = new XmlSerializer(typeof(T));

                try
                {
                    using (var reader = new FileStream(FullSavePath, FileMode.Open))
                    {
                        model = (T)serializer.Deserialize(reader);
                    }
                }
                catch
                {
                    model = new T();
                }
            }

            return model;
        }
    }
}