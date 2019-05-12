using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;


namespace CommandCraft_App.DBHandling
{
    public class DataAccess
    {
        public List<Building> GetBuildings()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("CommandCraftDB")))
            {
                var output = connection.Query<Building>("SELECT * FROM Buildings").ToList();
                return output;
            }
        }

        public void SaveBuilding( string _buildingName, string _buildingData)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("CommandCraftDB")))
            {
                connection.Execute($"INSERT INTO Buildings VALUES('{_buildingName }', '{_buildingData}')");
                
            }
        }


    }
}
