using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class SQLManager : MonoSingleton<SQLManager>
{

    private Sphere _sphere; //sphere id is 0
    private Cube _cube; //sphere id is 1
    private Capsule _capsule; //sphere id is 2


    public void Start()
    {
        AssignScripts();

        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); 
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableExtended"; 
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); 

        while (dataReader.Read()) 
        {
            // The `id` has index 0, our `value` has the index 1.
            int id = dataReader.GetInt32(0); 
            int hits = dataReader.GetInt32(1);
            if (id == 0)
            {
                _sphere.SetHitCount(hits);
                UIManager.Instance.SetValues(id, hits);
            }
            else if (id == 1)
            {
                _cube.SetHitCount(hits);
                UIManager.Instance.SetValues(id, hits);
            }
            else if (id == 2)
            {
                _capsule.SetHitCount(hits);
                UIManager.Instance.SetValues(id, hits);
            }
        }

        // Remember to always close the connection at the end.
        dbConnection.Close();

        //return hits;
    }

    private void AssignScripts()
    {
        _sphere = GameObject.Find("Sphere").GetComponent<Sphere>();
        _cube = GameObject.Find("Cube").GetComponent<Cube>();
        _capsule = GameObject.Find("Capsule").GetComponent<Capsule>();
    }
    public void AddValueToDatabase(int id, int hits)
    {
        // Insert hits into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 10
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTableExtended (id, hits) VALUES (" + id + ", " + hits + ")"; // 11
        dbCommandInsertValue.ExecuteNonQuery(); // 12

        // Remember to always close the connection at the end.
        dbConnection.Close(); // 13
    }

    private IDbConnection CreateAndOpenDatabase() // 3
    {
        // Open a connection to the database.
        string dbUri = "URI=file:MyDatabase.sqlite"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 7
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableExtended (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        return dbConnection;
    }
}
