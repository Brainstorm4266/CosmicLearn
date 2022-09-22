using MongoDB.Bson;

namespace CosmicLearn.LiteDBTypes
{
    internal class UserData
    {
        // There is only 1 user, because this is a local app. Not a web app.
        public Int32 _id { get; set; }
        public string name { get; set; }
        public Types.UserSetProgress[] progresses { get; set; }

        public static explicit operator Types.UserData(LiteDBTypes.UserData ud) => new Types.UserData
        {
            name = ud.name,
            progresses = ud.progresses,
        };

        public static implicit operator LiteDBTypes.UserData(Types.UserData ud) => new LiteDBTypes.UserData
        {
            name = ud.name,
            progresses = ud.progresses,
        };
    }
}
