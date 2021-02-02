using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    class TableЛьготнаяКатегория
    {
        /// <summary>
        /// Возвращает DataTable4
        /// </summary>
        /// <returns></returns>
        static public DataTable GetDateTable()
        {
            string sCon = ConnectionDB.ConnectionString();

            string query = "SELECT [id_льготнойКатегории] ,[ЛьготнаяКатегория] FROM ЛьготнаяКатегория";

            OleDbConnection con = new OleDbConnection(sCon);
            con.Open();
            //OleDbCommand com = new OleDbCommand(query, con);
            OleDbDataAdapter da = new OleDbDataAdapter(query, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "ЛьготнаяКатегория");
            con.Close();
            return ds.Tables[0];
        }
    }
}
