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
    //no peeking at my username and password please
    private static string username = Environment.GetEnvironmentVariable("mongoname");
    private static string password = Environment.GetEnvironmentVariable("mongopassword");

    private string team;
    private string boss;
    private ScoreController sC;

    //string connectionString = "mongodb://localhost:27017";
    string connectionString = "mongodb://" + username + ":" + password + "@ds125862.mlab.com:25862/battlewareleaderboard";

    private void Start()
    {
        team = PlayerPrefs.GetString("name");
        boss = PlayerPrefs.GetString("boss");
        sC = GameObject.Find("Score").GetComponent<ScoreController>();
    }

    private void OnEnable()
    {
        GameController.gameOver += loadScore;
    }

    private void OnDisable()
    {
        GameController.gameOver -= loadScore;
    }

    public void loadScore()
    {
        print(team + boss + sC.Score);

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


        if (team != null)   //load the teams score to the mongodb if a team name was entered
        {
            scorecollection.Insert(new BsonDocument{
            { "team", team }, 
            { "score", sC.Score },
            { "boss", boss }
        });
            Debug.Log("2. INSERTED A DOC");
        }
    }
}
