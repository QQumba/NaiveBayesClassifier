using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.CompilerServices;
using NaiveBayesClassifier.Entities;

namespace NaiveBayesClassifier
{
    public static class TextParser
    {
        private static readonly string[] Separators = {".", ",", ":", ";", "(", ")", "!", "?", "\"", "\'", "-", " "};
        private static readonly char[] TrimChars = {'.', ' ', '!', '?'};
        private const string MockSeparator = "*$*";

        public static Word[] Parse(Message[] messages)
        {
            var words = new List<Word>();
            foreach (var message in messages)
            {
                var messageWords = Parse(message.Value);
                foreach (var messageWord in messageWords)
                {
                    if (string.IsNullOrEmpty(messageWord))
                        continue;
                    
                    var word = words.FirstOrDefault(w => w.Value.Equals(messageWord));
                    if (word != null)
                    {
                        word.SpamCount += message.IsSpam ? 1 : 0;
                        word.Count++;
                    }
                    else
                    {
                        words.Add(new Word
                        {
                            Value = messageWord,
                            SpamCount = message.IsSpam ? 1 : 0
                        });
                    }

                }
            }

            return words.OrderByDescending(w => w.Count).ToArray();
            //return words.ToArray();
        }
        
        private static string[] Parse(string text)
        {
            text = text.ToLower().Trim(TrimChars);
            foreach (var separator in Separators)
            {
                text = text.Replace(separator, MockSeparator);
            }

            return text.Split(MockSeparator);
        }
    }
}