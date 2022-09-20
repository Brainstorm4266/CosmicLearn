using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CosmicLearn
{
    internal class DB
    {
        MongoClient dbClient;
        IMongoDatabase database;
        IMongoCollection<Types.Set> sets;
        IMongoCollection<Types.UserData> userData;
        IMongoCollection<Types.Counter> counters;

        public DB(string server)
        {
            Console.WriteLine("Connecting to database "+server+"...");
            dbClient = new MongoClient(server);
        }

        public int newSet(Types.Set set)
        {
            var counterLookup = counters.Find((x => ((x.collection == "sets") && (x.value == "setId"))));
            var count = counterLookup.First();

            var sid = count.count;
            set.setId = sid;
            sets.InsertOne(set);

            var builder = Builders<Types.Counter>.Filter;
            FilterDefinition<Types.Counter> filter = builder.Eq("collection", "sets") & builder.Eq("value", "setId");
            var update = Builders<Types.Counter>.Update
                .Set(p => p.count, count.count + 1);
            counters.UpdateOne(filter, update);

            return sid;
        }

        public List<Types.Set> getSets()
        {
            var lookup = sets.Find(x => true);

            if (lookup.CountDocuments() > 0)
            {
                var list = lookup.ToList();

                return list;
            } else
            {
                return new List<Types.Set>();
            }
        }

        public void init(string databaseName)
        {
            Console.WriteLine("Loading database " + databaseName + "...");

            database = dbClient.GetDatabase(databaseName);

            bool setsCollectionPresent = false;
            bool userDataCollectionPresent = false;
            bool counterCollectionPresent = false;

            var collections = database.ListCollections().ToList();

            foreach (var collection in collections)
            {
                List<BsonElement> col = collection.ToList();

                col.ForEach(x =>
                {
                    if (x.Name == "name")
                    {
                        if (x.Value == "sets")
                        {
                            setsCollectionPresent = true;
                        }
                        else if (x.Value == "userData")
                        {
                            userDataCollectionPresent = true;
                        }
                        else if (x.Value == "counters")
                        {
                            counterCollectionPresent = true;
                        }
                    }
                });
            }

            if (!counterCollectionPresent)
            {
                Console.WriteLine("Creating collection counters...");
                database.CreateCollection("counters");
                counters = database.GetCollection<Types.Counter>("counters");
            } else
            {
                Console.WriteLine("Checking collection counters...");
                counters = database.GetCollection<Types.Counter>("counters");
            }

            if (!setsCollectionPresent)
            {
                Console.WriteLine("Creating collection sets...");
                database.CreateCollection("sets");
                sets = database.GetCollection<Types.Set>("sets");

                var setIdCounter = new Types.Counter();
                setIdCounter.collection = "sets";
                setIdCounter.value = "setId";
                setIdCounter.count = 0;
                counters.InsertOne(setIdCounter);
            } else
            {
                Console.WriteLine("Checking collection sets...");
                sets = database.GetCollection<Types.Set>("sets");

                if (counters.CountDocuments(x => ((x.collection == "sets") && (x.value == "setId"))) == 0)
                {
                    var setIdCounter = new Types.Counter();
                    setIdCounter.collection = "sets";
                    setIdCounter.value = "setId";
                    setIdCounter.count = 0;
                    counters.InsertOne(setIdCounter);
                }
            }

            if (!userDataCollectionPresent)
            {
                Console.WriteLine("Creating collection userData...");
                database.CreateCollection("userData");
                userData = database.GetCollection<Types.UserData>("userData");
            } else
            {
                Console.WriteLine("Checking collection userData...");
                userData = database.GetCollection<Types.UserData>("userData");
            }
        }
    }
}
