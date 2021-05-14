using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NaiveBayesClassifier.Entities;

namespace NaiveBayesClassifier.Utils
{
    public static class TsvReader
    {
        private const string SpamMark = "spam";
        public static Message[] ReadMessage(string path)
        {
            var messages = new List<Message>();
            using (var sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var message = line.Split('\t');
                    try
                    { 
                        messages.Add(new Message()
                        {
                            Value = message[0],
                            IsSpam = message[1].Equals(SpamMark)
                        });
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            
            // var random = new Random();
            // return messages.OrderBy(m => random.Next()).ToArray();
            return messages.ToArray();
        }
    }
}