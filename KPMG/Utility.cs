using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KPMG
{
    public class Utility
    {
        public static DataTable ExecuteQuery(string connectionString,string queryString,List<SqlParameter> parList = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    if (parList != null)
                    {
                        foreach (var item in parList)
                        {
                            command.Parameters.AddWithValue(item.ParameterName, item.Value);
                        }
                    }
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    
                    da.Fill(dt);

                    return dt;
                }
                catch (Exception ex)
                {
                    return dt;
                }
                
            }
        }
    }
}