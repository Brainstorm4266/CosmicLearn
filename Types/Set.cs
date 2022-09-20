using MongoDB.Bson;

namespace CosmicLearn.Types
{
    internal class Set
    {
        public ObjectId _id { get; set; }
        public Int32 setId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Word> words { get; set; }
        public string wordlang { get; set; }
        public string deflang { get; set; }
    }
}
