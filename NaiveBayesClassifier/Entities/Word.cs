using System.Text;

namespace NaiveBayesClassifier.Entities
{
    public class Word
    {
        public string Value { get; set; }
        public int Count { get; set; } = 1;
        public int SpamCount { get; set; }
        public int NonSpamCount => Count - SpamCount;
        public double SpamP { get; set; }
        public double NonSpamP { get; set; }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Value,-20}");
            sb.Append($"{"|",-5}");
            sb.Append($"{Count,-5}");
            sb.Append($"{"|",-5}");
            sb.Append($"{SpamCount,-5}");
            sb.Append($"{"|",-5}");
            sb.Append($"{NonSpamCount,-5}");
            sb.Append($"{"|",-5}");
            sb.Append($"{SpamP,-21}");
            sb.Append($"{"|",-5}");
            sb.Append($"{NonSpamP,-21}");
            return sb.ToString();
        }
    }
}