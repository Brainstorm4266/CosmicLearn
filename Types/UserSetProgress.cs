using MongoDB.Bson;

namespace CosmicLearn.Types
{
    internal class UserSetProgress
    {
        public int setId { get; set; }
        public int wordsCorrect { get; set; }
        public int wordsRemaining { get; set; }
        public int wordsIncorrect { get; set; }
        public int rounds { get; set; }
        public SetSettings setSettings { get; set; } = new SetSettings();
    }
}
