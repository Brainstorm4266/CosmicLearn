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

        public static explicit operator Types.Counter(LiteDBTypes.Counter c) => new Types.Counter { 
            collection = c.collection,
            count = c.count,
            value = c.value
        };
        public static implicit operator LiteDBTypes.Counter(Types.Counter c) => new LiteDBTypes.Counter {
            collection = c.collection,
            count = c.count,
            value = c.value
        };
    }
}
