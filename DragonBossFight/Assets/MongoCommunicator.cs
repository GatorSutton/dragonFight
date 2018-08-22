/**
 * Documentation of C# driver 1.10:
 * http://mongodb.github.io/mongo-csharp-driver/1.11/getting_started/
 * c# Driver for MongoDBprovided by http://answers.unity3d.com/questions/618708/unity-and-mongodb-saas.html
 */


using UnityEngine;
using System; //
using System.Collections;
using System.Collections.Generic;  // Lists

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

public class MongoCommunicator : MonoBehaviour
{
    private static string username = System.Environment.GetEnvironmentVariable("mongoname");
    private static string password = System.Environment.GetEnvironmentVariable("mongopassword");
    //string connectionString = "mongodb://localhost:27017";
    string connectionString = "mongodb://" + username + ":" + password + "@ds125862.mlab.com:25862/battlewareleaderboard";
    string team = "test";
    int score = 1000;
    string boss = "dragon";

    void Start()
    {

        print(username);
        print(password);
        /*
		 * 1. establish connection
		 */



        var client = new MongoClient(connectionString);
        var server = client.GetServer();
        var database = server.GetDatabase("battlewareleaderboard");
        var scorecollection = database.GetCollection<BsonDocument>("scores");
        Debug.Log("1. ESTABLISHED CONNECTION");



        /*
		 * 2. INSERT new dataset: INSERT INTO players (email, name, scores, level) VALUES("..","..","..","..");
		 */



        scorecollection.Insert(new BsonDocument{
            { "team", team },
            { "score", score },
            { "boss", boss }
        });
        Debug.Log("2. INSERTED A DOC");
    }
}
