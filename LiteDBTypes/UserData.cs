using MongoDB.Bson;

namespace CosmicLearn.LiteDBTypes
{
    internal class UserData
    {
        // There is only 1 user, because this is a local app. Not a web app.
        public Int32 _id { get; set; }
        public string name { get; set; }
        public Types.UserSetProgress[] progresses { get; set; }
    }
}
