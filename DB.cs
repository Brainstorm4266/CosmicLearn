using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using LiteDB;

namespace CosmicLearn
{
    internal class DB
    {
        MongoClient? mongoDbClient;
        LiteDatabase? liteDatabase;
        IMongoDatabase? database;
        IMongoCollection<Types.Set>? sets;
        IMongoCollection<Types.UserData>? userData;
        IMongoCollection<Types.Counter>? counters;

        ILiteCollection<LiteDBTypes.Set>? liteSets;
        ILiteCollection<LiteDBTypes.UserData>? liteUserData;
        ILiteCollection<LiteDBTypes.Counter>? liteCounters;

        bool useMongoDB;
        public DB(string server, bool isMongoDB)
        {
            useMongoDB = isMongoDB;
            if (isMongoDB)
            {
                Console.WriteLine("Connecting to database " + server + "...");
                mongoDbClient = new MongoClient(server);
            } else
            {
                Console.WriteLine("Reading database "+server+"...");
                liteDatabase = new LiteDatabase(server);
            }
        }

        public Types.UserData? getUserData()
        {
            if (useMongoDB)
            {
                if (userData is null)
                {
                    throw new Exception();
                }
                var lookup = userData.Find(x => true);
                if (lookup.CountDocuments() == 0)
                {
                    return null;
                } else
                {
                    return lookup.First();
                }
            } else
            {
                if (liteUserData is null)
                {
                    throw new Exception();
                }
                var lookup = liteUserData.Find(x => true);
                if (lookup.Count() == 0)
                {
                    return null;
                } else
                {
                    return (Types.UserData?)lookup.First();
                }
            }
        }

        public void setUserData(Types.UserData ud)
        {
            if (useMongoDB)
            {
                if (userData is null)
                {
                    throw new Exception();
                }
                userData.DeleteMany("{}");
                userData.InsertOne(ud);
            } else
            {
                if (liteUserData is null)
                {
                    throw new Exception();
                }
                liteUserData.DeleteAll();
                liteUserData.Insert(ud);
            }
        }

        public string? getUsername(string uname)
        {
            if (useMongoDB)
            {
                if (userData is null)
                {
                    throw new Exception();
                }
                var lookup = userData.Find(x => true);
                if (lookup.CountDocuments() == 0)
                {
                    return null;
                } else
                {
                    return lookup.ToList()[0].name;
                }
            } else
            {
                if (liteUserData is null)
                {
                    throw new Exception();
                }
                var lookup = liteUserData.Find(x => true);
                if (lookup.Count() == 0)
                {
                    return null;
                } else
                {
                    return lookup.ToList()[0].name;
                }
            }
        }

        public int newSet(Types.Set set)
        {
            if (useMongoDB)
            {
                if ((sets is null) || (counters is null))
                {
                    throw new Exception();
                }
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
            } else
            {
                if ((liteSets is null) || (liteCounters is null))
                {
                    throw new Exception();
                }
                var counterLookup = liteCounters.Find(x => ((x.collection == "sets") && (x.value == "setId")));
                var countbl = counterLookup.ToList();
                var count = countbl[0];

                var sid = count.count;
                set.setId = sid;
                liteSets.Insert(set);

                count.count = count.count + 1;
                liteCounters.Update(count);

                return sid;
            }
        }

        public List<Types.Set> getSets()
        {
            if (useMongoDB)
            {
                var lookup = sets.Find(x => true);

                if (lookup.CountDocuments() > 0)
                {
                    var list = lookup.ToList();

                    return list;
                }
                else
                {
                    return new List<Types.Set>();
                }
            } else
            {
                if (liteSets is null)
                {
                    throw new Exception();
                }
                var liteDbSet = liteSets.FindAll().ToList();
                var lst = new List<Types.Set>();
                liteDbSet.ForEach(s =>
                {
                    lst.Add((Types.Set)s);
                });
                return lst;
            }
        }

        public void init(string databaseName)
        {
            Console.WriteLine("Loading database " + databaseName + "...");

            if (useMongoDB)
            {
                if (mongoDbClient is null)
                {
                    throw new Exception();
                }
                database = mongoDbClient.GetDatabase(databaseName);
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
                }
                else
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
                }
                else
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
                }
                else
                {
                    Console.WriteLine("Checking collection userData...");
                    userData = database.GetCollection<Types.UserData>("userData");
                }
            } else
            {
                if (liteDatabase is null)
                {
                    throw new Exception();
                }
                liteCounters = liteDatabase.GetCollection<LiteDBTypes.Counter>("counters");
                liteSets = liteDatabase.GetCollection<LiteDBTypes.Set>("sets");
                if (!liteCounters.Exists(x => ((x.collection == "sets") && (x.value == "setId"))))
                {
                    var counter = new LiteDBTypes.Counter
                    {
                        collection = "sets",
                        value = "setId",
                        count = 0
                    };
                    liteCounters.Insert(counter);
                }
                liteUserData = liteDatabase.GetCollection<LiteDBTypes.UserData>("userData");
            }
        }
    }
}
