using System.IO;

namespace NaiveBayesClassifier.Utils
{
    public static class Configuration
    {
        private const string ConfigurationFile = "file.conf";
        
        public static string GetFilePath()
        {
            if (!File.Exists(ConfigurationFile))
            {
                throw new FileNotFoundException("Config file corrupted.");
            }

            string dataset;
            using (var sr = new StreamReader(ConfigurationFile))
            {
                dataset = sr.ReadLine()?.Split("file: ")[1];
            }

            if (!File.Exists(dataset))
            {
                throw new FileNotFoundException("Dataset file not found.");
            }

            return dataset;
        }
    }
}