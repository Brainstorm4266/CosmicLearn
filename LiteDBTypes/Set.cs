using MongoDB.Bson;

namespace CosmicLearn.LiteDBTypes
{
    internal class Set
    {
        public Int32 _id { get; set; }
        public Int32 setId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Types.Word> words { get; set; }
        public string wordlang { get; set; }
        public string deflang { get; set; }
    }
}
