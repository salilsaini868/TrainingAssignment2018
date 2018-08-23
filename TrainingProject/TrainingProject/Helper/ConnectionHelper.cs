using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TrainingProject.Helper
{
    public class ConnectionHelper
    {
        SqlConnection connection;
        public static string strConnect = ConfigurationManager.ConnectionStrings["dataConnect"].ConnectionString;

        public ConnectionHelper()
        {
            connection = new SqlConnection(strConnect);
        }
        
        private SqlCommand CreateCommand(string cmdstring, CommandType type, List<KeyValuePair<string,object>> parameters)
        {
            SqlCommand cmd = new SqlCommand(cmdstring, connection);
            if(connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            cmd.CommandType = type;
            foreach (var item in parameters)
            {
                cmd.Parameters.AddWithValue("@"+item.Key, item.Value);
            }
            return cmd;
        }

        public dynamic CreateResult(ExecuteEnum executeType ,string query, CommandType command, List<KeyValuePair<string, object>> valuePairs)
        {
            var result_command = CreateCommand( query, command, valuePairs);
            if(executeType == ExecuteEnum.Insert)
            {
                int insertVal = result_command.ExecuteNonQuery();
                return insertVal;
            }
            if(executeType == ExecuteEnum.Delete)
            {
                int deleteVal = result_command.ExecuteNonQuery();
                return deleteVal;
            }
            if(executeType == ExecuteEnum.List)
            {
                DataTable searchResult = new DataTable();
                SqlDataAdapter adapter_search = new SqlDataAdapter(result_command);
                adapter_search.Fill(searchResult);
                return searchResult;
            }
            if(executeType == ExecuteEnum.Detail)
            {
                SqlDataReader reader = result_command.ExecuteReader();                
                return reader;
            }
            return null;
        }
    }

}
