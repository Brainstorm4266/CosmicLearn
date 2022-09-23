using MongoDB.Bson;

namespace CosmicLearn.Types
{
    internal class UserData
    {
        // There is only 1 user, because this is a local app. Not a web app.
        public ObjectId _id { get; set; }
        public string name { get; set; } = "";
        public List<UserSetProgress> progresses { get; set; } = new List<UserSetProgress>();
    }
}
