using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Versioning;
using NaiveBayesClassifier.Entities;
using NaiveBayesClassifier.Utils;

namespace NaiveBayesClassifier
{
    class Program
    {
        private static readonly string Path = Configuration.GetFilePath();

        static void Main(string[] args)
        {
            var allMessages = TsvReader.ReadMessage(Path);

            PrintFullInfo(allMessages);
            PrintDatasetSizeDependentInfo(allMessages);
        }

        private static void PrintFullInfo(Message[] messages)
        {

            var trainingMessages = messages.Take(messages.Length * 2 / 3).ToArray();
                var testMessages = messages.Skip(trainingMessages.Length)
                    .Take(messages.Length - trainingMessages.Length)
                    .ToArray();
                
            var spamClassifier = new SpamClassifier(trainingMessages);
            spamClassifier.PrintWords();

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("overall messages count  --> " + messages.Length);
            Console.WriteLine("training messages count --> " + trainingMessages.Length);
            Console.WriteLine("test messages count     --> " + testMessages.Length);
            Console.WriteLine("--------------------------------------------");

            var positiveCounter = 0;
                
            foreach (var message in testMessages)
            {
                Console.WriteLine(message);
                    
                Console.WriteLine($"spam P: " + spamClassifier.GetSpamProbability(message));
                Console.WriteLine("non spam P: " + spamClassifier.GetNonSpamProbability(message));
                    
                Console.WriteLine(spamClassifier.GetSpamProbability(message) >
                                  spamClassifier.GetNonSpamProbability(message)
                    ? "spam"
                    : "not spam");
                    
                Console.WriteLine();

                var isSpam = spamClassifier.GetSpamProbability(message) >
                             spamClassifier.GetNonSpamProbability(message);

                if (message.IsSpam == isSpam)
                {
                    positiveCounter++;
                }
            }

            Console.WriteLine($"positive result: {positiveCounter}");
        }

        private static void PrintDatasetSizeDependentInfo(Message[] messages)
        {
            for (int i = 0; i < 10; i++)
            {
                var iterationMessages = messages.Take(messages.Length - i).ToArray();
                
                var trainingMessages = iterationMessages.Take(iterationMessages.Length * 2 / 3).ToArray();
                var testMessages = iterationMessages.Skip(trainingMessages.Length)
                    .Take(iterationMessages.Length - trainingMessages.Length)
                    .ToArray();

                var spamClassifier = new SpamClassifier(trainingMessages);

                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("overall messages count  --> " + iterationMessages.Length);
                Console.WriteLine("training messages count --> " + trainingMessages.Length);
                Console.WriteLine("test messages count     --> " + testMessages.Length);
                Console.WriteLine("--------------------------------------------");

                var positiveCounter = 0;
                
                foreach (var message in testMessages)
                {
                    var isSpam = spamClassifier.GetSpamProbability(message) >
                                 spamClassifier.GetNonSpamProbability(message);

                    if (message.IsSpam == isSpam)
                    {
                        positiveCounter++;
                    }
                }

                Console.WriteLine($"positive result: {positiveCounter}");
            }  
        }
    }
}