using System;

namespace NaiveBayesClassifier.Utils
{
    [Flags]
    public enum Options : byte
    {
        ShowWords = 0,
        UseDataSetSeparation = 1,
    }
}