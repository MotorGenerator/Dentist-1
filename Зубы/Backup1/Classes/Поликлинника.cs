using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    class Поликлинника
    {
        /// <summary>
        /// Возвращает id поликлинники
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public int GetIdПоликлинники(string query, string connectionString)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand com = new OleDbCommand(query, con);

            con.Open();
            int id_поликлинники = (int)com.ExecuteScalar();
            con.Close();
            //OleDbDataReader read = com.ExecuteReader();
            return id_поликлинники;
            
        }

        /// <summary>
        /// Возвращает код поликлинники
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public DataTable GetПоликлинники(string query, string connectionString)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            //OleDbCommand com = new OleDbCommand(query, con);
            OleDbDataAdapter da = new OleDbDataAdapter(query, connectionString);

            //string код = string.Empty;
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds, "Поликлинника");
            con.Close();

            return ds.Tables[0];

        }
    }
}
