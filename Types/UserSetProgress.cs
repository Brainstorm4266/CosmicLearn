using MongoDB.Bson;

namespace CosmicLearn.Types
{
    internal class UserSetProgress
    {
        public int setId { get; set; }
        public List<Word> wordsCorrect { get; set; } = new List<Word>();
        public List<Word> wordsRemaining { get; set; } = new List<Word>();
        public List<Word> wordsIncorrect { get; set; } = new List<Word>();
        public int correctNumber { get; set; }
        public int remainingNumber { get; set; }
        public int incorrectNumber { get; set; }
        public int rounds { get; set; }
        public Word currentWord { get; set; } = new Word();
        public bool setOngoing { get; set; }
        public bool hasExited { get; set; }
        public SetSettings setSettings { get; set; } = new SetSettings();
    }
}
