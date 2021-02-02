using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    public class ТипДокумента
    {
        public DataTable GetТипДокумента(string stringConnection)
        {
            OleDbConnection con = new OleDbConnection(stringConnection);

            string query = "select * from ТипДокумента";
            OleDbDataAdapter da = new OleDbDataAdapter(query, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "ТипДокумента");

            return ds.Tables[0];
        }

        /// <summary>
        /// Возвращает конкретный тип документа
        /// </summary>
        /// <param name="stringConnection"></param>
        /// <param name="id_документ"></param>
        /// <returns></returns>
        public DataTable GetCurrentТипДокумента(string stringConnection, int id_документ)
        {
            OleDbConnection con = new OleDbConnection(stringConnection);

            string query = "select * from ТипДокумента where id_документ = "+ id_документ +" ";
            OleDbDataAdapter da = new OleDbDataAdapter(query, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "ТипДокумента");

            return ds.Tables[0];
        }
    }
}
