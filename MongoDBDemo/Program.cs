
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo;

string connectionString = "MONGODBURL HERE";

string databaseName = "upmobilecosmos";
string collectionName = "users";

var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);

try
{
    var collectionNames = database.ListCollections().ToList();
    Console.WriteLine("Connection to MongoDB is successful");
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

var collection = database.GetCollection<BsonDocument>(collectionName);

var filter = Builders<BsonDocument>.Filter.Empty;
var filter2 = Builders<BsonDocument>.Filter.Eq("enterprise_id", "shaharuddin.hamid");

var document = collection.Count(filter);
var document2 = collection.Find(filter2).First();

if (document2 == null)
{
    Console.WriteLine("No records found.");
}
else
{
    Console.WriteLine(document);
    Console.WriteLine(document2);
}