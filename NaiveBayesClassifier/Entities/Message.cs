namespace NaiveBayesClassifier.Entities
{
    public class Message
    {
        public string Value { get; set; }
        public bool IsSpam { get; set; }

        public override string ToString()
        {
            return $"{(IsSpam?"spam":"non spam")}\n{Value}";
        }
    }
}