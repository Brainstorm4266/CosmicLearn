using MongoDB.Bson;

namespace CosmicLearn.LiteDBTypes
{
    internal class Set
    {
        public Int32 _id { get; set; }
        public Int32 setId { get; set; }
        public string name { get; set; } = "";
        public string description { get; set; } = "";
        public List<Types.Word> words { get; set; } = new List<Types.Word>();
        public string wordlang { get; set; } = "";
        public string deflang { get; set; } = "";

        public static explicit operator Types.Set(LiteDBTypes.Set s) => new Types.Set
        {
            setId = s.setId,
            name = s.name,
            description = s.description,
            words = s.words,
            wordlang = s.wordlang,
            deflang = s.deflang,
        };

        public static implicit operator LiteDBTypes.Set(Types.Set s) => new LiteDBTypes.Set
        {
            setId = s.setId,
            name = s.name,
            description = s.description,
            words = s.words,
            wordlang = s.wordlang,
            deflang = s.deflang,
        };
    }
}
