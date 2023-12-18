using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

public class SQLiteDataController : MonoBehaviour {
    public SystemPosition[] EnactDatabase() {
		// Open connection but first creates database if not exists
		IDbConnection dbcon = new SqliteConnection("Data Source=D:/Projects/Programming/Unity CSharp/Delta V Budgeter/Assets/Data/starsystems.db");
		dbcon.Open();

		ReformDB(dbcon);
        SystemPosition[] Sol = ReadDBIntoSystem(dbcon);

        // Close connection
        dbcon.Close();

        return Sol;
	}
	void ReformDB(IDbConnection dbcon) { // Removes tables, then Creates tables and populates them
		IDbCommand cmnd_form = dbcon.CreateCommand();
		cmnd_form.CommandText = "DROP TABLE StarSystems; DROP TABLE SystemPositions;" + File.ReadAllText("Assets/Data/DVBFormDatabase.sql");
		cmnd_form.ExecuteNonQuery();
	}
    SystemPosition[] ReadDBIntoSystem(IDbConnection dbcon) { // Read all values in table and add to SolarSystem object
        IDbCommand cmnd_read_systems = dbcon.CreateCommand();
        IDbCommand cmnd_read_positions = dbcon.CreateCommand();
        IDataReader reader;

        cmnd_read_systems.CommandText = "SELECT * FROM StarSystems;";
        reader = cmnd_read_systems.ExecuteReader();
        SystemPosition[] Sol = new SystemPosition[Convert.ToInt32(reader[2])];

        cmnd_read_positions.CommandText = "SELECT * FROM SystemPositions;";
        reader = cmnd_read_positions.ExecuteReader();
        List<int[]> locations = new List<int[]>();

        while (reader.Read()) {
            int parentID = Convert.ToInt16(reader["ParentID"]) - 1;
            int positionUnderParent;
            SystemPosition positionToAdd = new SystemPosition(reader["PositionName"].ToString(), Convert.ToInt32(reader["DVFromTransfer"]), Convert.ToInt16(reader["SubCount"]), Convert.ToBoolean(reader["Atmosphere"]));
            if (parentID == -1) {
                positionUnderParent = Sol.Count(x => x != null);    
                Sol[positionUnderParent] = positionToAdd;
                locations.Add(new int[] { positionUnderParent });
                //Debug.Log(positionToAdd.Name + " " + locations[^1][0]);
            } else if (locations[parentID].Length == 1) {
                positionUnderParent = Sol[locations[parentID][0]].SubPositions.Count(x => x != null);
                Sol[locations[parentID][0]].SubPositions[positionUnderParent] = positionToAdd;
                locations.Add(new int[] { locations[parentID][0], positionUnderParent } );
                //Debug.Log(positionToAdd.Name + " " + locations[^1][0] + " " + locations[^1][1]);
            } else {
                positionUnderParent = Sol[locations[parentID][0]].SubPositions[locations[parentID][1]].SubPositions.Count(x => x != null);
                Sol[locations[parentID][0]].SubPositions[locations[parentID][1]].SubPositions[positionUnderParent] = positionToAdd;
                locations.Add(new int[] { locations[parentID][0], locations[parentID][1], positionUnderParent });
                //Debug.Log(positionToAdd.Name + " " + locations[^1][0] + " " + locations[^1][1] + " " + locations[^1][2]);
            }
        }

        return Sol;
    }
}