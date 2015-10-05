
using FancyApps.Model;
using MongoDB.Driver;

namespace FancyApps.Data
{

    public class FancyAppsDbContext
    {

        private MongoDatabase database;

        //Production
        //const string DatabaseHost = "mongodb://power:pwtest1@ds055752.mongolab.com:55752";
        //const string DatabaseName = "fancyapps";

        //const string DatabaseHost = "mongodb://localhost:27017";
        //const string DatabaseName = "fancyapps";

        //public FancyAppsDbContext()
        //{
        //    this.database = GetDatabase();
        //}

        //public FancyAppsDbContext(string dataBaseName, string fromHost)
        //{
        //    this.database = GetDatabase(dataBaseName, fromHost);
        //}

        //private MongoDatabase GetDatabase()
        //{
        //    return GetDatabase(DatabaseName, DatabaseHost);
        //}

        //private MongoDatabase GetDatabase(string name, string fromHost)
        //{
        //    var mongoClient = new MongoClient(fromHost);
        //    var server = mongoClient.GetServer();
        //    return server.GetDatabase(name);
        //}

        //public MongoDatabase Database
        //{
        //    get { return database; }
        //}

        //public MongoCollection<User> Users {
        //    get { return this.database.GetCollection<User>("user"); }
        //}


    }
}
