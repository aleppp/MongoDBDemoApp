
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using MongoDBDemo;

string connectionString = "mongodb://pet-up-mobile-cosmos-prod:ZiV8UgLxMIYANAiAbQjvRAEadABH7ginkMnTqU5wzVcWUUO9QsfBRnieCMjGleYa5uR6K7eSUt0MVhYyRx8cEw==@pet-up-mobile-cosmos-prod.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";

string databaseName = "upmobilecosmos";
string collectionName = "users";

var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);

var collection = database.GetCollection<BsonDocument>(collectionName);

try
{
    database.ListCollections().ToList();
    Console.WriteLine("Connection to MongoDB is successful");
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

try
{

    using (StreamReader sr = new StreamReader("D:\\PluralSight\\C#\\Getting Started\\MongoDBDemoApp\\MongoDBDemo\\UPusers-moduleURLs.txt"))
    {
        // Create a list to hold the file lines.
        var fileLines = new System.Collections.Generic.List<string>();

        // Read each line from the file and add it to the list.
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            fileLines.Add(line);
        }

        // Convert the list to an array.
        string[] txtArray = fileLines.ToArray();

        //new list to hold the new output
        var outputLines = new List<string>();

        //access mongodb collection
        var filter = Builders<BsonDocument>.Filter.Empty;
        var documents = await collection.FindAsync(filter);

        while (await documents.MoveNextAsync())
        {
            var batch = documents.Current;
            foreach (var document in batch)
            {
                // Check if the enterprise_id field matches any value in txtArray.
                string enterpriseId = document.GetValue("enterprise_id").AsString;

                if (Array.Exists(txtArray, element => element == enterpriseId) && document.Contains("department"))
                {
                    Console.WriteLine(enterpriseId);
                    // If a match is found, extract the department field value and write to output.
                    string? department = document.GetValue("department").IsBsonNull ? null : document.GetValue("department").AsString;
                    outputLines.Add($"{enterpriseId},{department}");
                }
            }
        }

        // Write the output lines to a new text file.
        using (StreamWriter sw = new StreamWriter("D:\\PluralSight\\C#\\Getting Started\\MongoDBDemoApp\\MongoDBDemo\\outputprod.txt"))
        {
            foreach (string outputLine in outputLines)
            {
                sw.WriteLine(outputLine);
            }
        }
    }
}
catch (Exception e)
{
    // If an error occurs, display it to the console.
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}
finally
{
    Console.WriteLine("Executing finally block.");
}