
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo;

string connectionString = "MONGODB CONNECTION HERE";

if(connectionString == null)
{
    Console.WriteLine("Connection Failed");
    Environment.Exit(0);
}

string databaseName = "upmobilecosmos";
string collectionName = "users";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<BsonDocument>("users");

var filter = Builders<BsonDocument>.Filter.Eq("enterprise_id", "mnazrulhisham.mohama");

var document = collection.Find(filter).First();

if(document == null)
{
    Console.WriteLine("No records found.");
}
else
{
    Console.WriteLine(document.ToJson());
}