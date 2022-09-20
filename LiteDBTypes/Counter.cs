using MongoDB.Bson;

namespace CosmicLearn.LiteDBTypes
{
    internal class Counter
    {
        public Counter() { }

        public Int32 _id { get; set; }
        public string collection { get; set; }
        public string value { get; set; }
        public int count { get; set; }
    }
}
