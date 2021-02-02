using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    public class ЛьготнаяКатегория
    {
        public DataTable GetЛьготнаяКатегория(string stringConnection)
        {
            OleDbConnection con = new OleDbConnection(stringConnection);

            string query = "select * from ЛьготнаяКатегория";
            OleDbDataAdapter da = new OleDbDataAdapter(query, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "ЛьготнаяКатегория");

            return ds.Tables[0];

        }

        /// <summary>
        /// Возвращает конкретную лльготную категорию
        /// </summary>
        /// <param name="stringConnection"></param>
        /// <param name="id_льготнаяКатегория"></param>
        /// <returns></returns>
        public DataTable GetCurrentЛьготнаяКатегория(string stringConnection, int id_льготнаяКатегория)
        {
            OleDbConnection con = new OleDbConnection(stringConnection);

            string query = "select * from ЛьготнаяКатегория where id_льготнойКатегории = " + id_льготнаяКатегория + " ";
            OleDbDataAdapter da = new OleDbDataAdapter(query, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "ЛьготнаяКатегория");

            return ds.Tables[0];
        }
    }
}
