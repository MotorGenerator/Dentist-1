using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;


namespace Стамотология.Classes
{
    class Query
    {
        /// <summary>
        /// Выполняет запрос TSQL
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        static public void Execute(string query, string connectionString)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand com = new OleDbCommand(query, con);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
