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

        public void SaveBuilding( string _buildingName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("CommandCraftDB")))
            {
                connection.Query<Building>($"INSERT INTO Buildings VALUES('{3}', {_buildingName }, 'sample data')");
                
            }
        }


    }
}
