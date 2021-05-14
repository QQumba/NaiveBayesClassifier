using System;
using System.Collections.Generic;
using System.Linq;
using NaiveBayesClassifier.Entities;
using NaiveBayesClassifier.Utils;

namespace NaiveBayesClassifier
{
    public class SpamClassifier
    {
        private List<Word> _words;
        
        private readonly double _spamP;
        private readonly double _nonSpamP;
        
        public int SpamMessagesCount { get; } = 0;
        
        public SpamClassifier(Message[] messages)
        {
            foreach (var message in messages)
            {
                if (message.IsSpam)
                    SpamMessagesCount++;
            }
            
            _words = TextParser.Parse(messages).ToList();
            _spamP = GetClassP(messages.Length, SpamMessagesCount);
            _nonSpamP = GetClassP(messages.Length, messages.Length - SpamMessagesCount);
            
            Train();
        }

        public double GetNonSpamProbability(Message message)
        {
            var messageWords = TextParser.Parse(new[] {message});
            
            var messageWordsP = new List<double>();
            foreach (var messageWord in messageWords)
            {
                var word = _words.FirstOrDefault(w => w.Value.Equals(messageWord.Value));
                if (word != null)
                {
                    messageWordsP.Add(word.NonSpamP);
                }
            }

            var p = _nonSpamP * messageWordsP.Multiply();
            
            // Console.Write($"{_nonSpamP} * (");
            // for (var i = 0; i < messageWordsP.Count - 1; i++)
            // {
            //     Console.Write($"{messageWordsP[i]} * ");
            // }
            // Console.Write($"{messageWordsP[^1]}) = {p}\n");

            
            
            return Math.Round(p, 15);
        }

        public double GetSpamProbability(Message message)
        {
            var messageWords = TextParser.Parse(new[] {message});
            
            var messageWordsP = new List<double>();
            foreach (var messageWord in messageWords)
            {
                var word = _words.FirstOrDefault(w => w.Value.Equals(messageWord.Value));
                if (word != null)
                {
                    messageWordsP.Add(word.SpamP);
                }
            }

            var p = _spamP * messageWordsP.Multiply();
            
            // Console.Write($"{_spamP} * (");
            // for (var i = 0; i < messageWordsP.Count - 1; i++)
            // {
            //     Console.Write($"{messageWordsP[i]} * ");
            // }
            // Console.Write($"{messageWordsP[^1]}) = {p}\n");

            
            
            return Math.Round(p, 15);
        }

        public void PrintWords()
        {
            foreach (var word in _words)
            {
                Console.WriteLine(word);
            }
        }

        private void Train()
        {
            // PrintWords();
            // Console.WriteLine($"words count before filtering :{_words.Count}");
            // FilterWords();
            // Console.WriteLine($"words count after filtering :{_words.Count}");
            foreach (var word in _words)
            {
                word.SpamP = GetWordPNormalized(word.Count, word.SpamCount, _spamP);
                word.NonSpamP = GetWordPNormalized(word.Count, word.NonSpamCount, _nonSpamP);
            }
        }

        private void FilterWords()
        {
            var newWords = new List<Word>();
            var minThreshold = 2;
            var maxThreshold = 10;
            foreach (var word in _words)
            {
                if (word.Count > minThreshold && word.Count < maxThreshold)
                {
                    newWords.Add(word);
                }
            }

            _words = newWords;
        }

        private double GetClassP(int overallCount, int classCount)
        {
            var classP = 1 / (double)overallCount * classCount;
            return classP;
        }
        
        private double GetWordPNormalized(int wordCount, int classWordCount, double classP)
        {
            var wordPIrregular = 1 / (double)wordCount * classWordCount;
            var wordPNormalized = (wordCount * wordPIrregular + classP) / (wordCount + 1);
            return Math.Round(wordPNormalized, 15);
        }
        
        private double GetClassProbability(Message message, double classP)
        {
            var messageWords = TextParser.Parse(new[] {message});
            
            var messageWordsP = new List<double>();
            foreach (var messageWord in messageWords)
            {
                var word = _words.FirstOrDefault(w => w.Value.Equals(messageWord.Value));
                if (word != null)
                {
                    messageWordsP.Add(word.SpamP);
                }
            }

            var p = classP * messageWordsP.Multiply();
            
            Console.Write($"{classP} * (");
            for (var i = 0; i < messageWordsP.Count - 1; i++)
            {
                Console.Write($"{messageWordsP[i]} * ");
            }
            Console.Write($"{messageWordsP[^1]}) = {p}");

            
            
            return Math.Round(p) ;
        }
    }
}