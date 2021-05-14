using System.Collections.Generic;

namespace NaiveBayesClassifier.Utils
{
    public static class Multiplier
    {
        public static double Multiply(this IEnumerable<double> arr)
        {
            double product = 1;
            foreach (var number in arr)
            {
                product *= number;
            }

            return product;
        }
    }
}