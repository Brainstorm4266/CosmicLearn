using MongoDB.Bson;

namespace CosmicLearn.Types
{
    internal class Counter
    {
        public ObjectId _id { get; set; }
        public string collection { get; set; } = "";
        public string value { get; set; } = "";
        public int count { get; set; }
    }
}
