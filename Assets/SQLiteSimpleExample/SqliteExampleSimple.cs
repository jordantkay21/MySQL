using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class SqliteExampleSimple : MonoBehaviour
{

// Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    [SerializeField] private int hitCount = 0;

    void Start() // 14
    {
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 15
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 16
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableSimple"; // 17
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 18

        while (dataReader.Read()) // 19
        {
            // The `id` has index 0, our `hits` have the index 1.
            hitCount = dataReader.GetInt32(1); // 20
        }

        // Remember to always close the connection at the end.
        dbConnection.Close(); // 21
    }

    private void OnMouseDown()
    {
        hitCount++;

        // Insert hits into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 10
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTableSimple (id, hits) VALUES (0, " + hitCount + ")"; // 11
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
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, hits INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        return dbConnection;
    }
}

