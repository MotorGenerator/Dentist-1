using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    class ТаблицаБД
    {
         /// <summary>
        /// Возвращает код поликлинники
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public DataTable GetTable(string query, string connectionString,string nameTable)
        {

            string aaa = connectionString;

            OleDbConnection con = new OleDbConnection(connectionString);
            //OleDbCommand com = new OleDbCommand(query, con);
            OleDbDataAdapter da = new OleDbDataAdapter(query, connectionString);

            //string код = string.Empty;
            DataSet ds = new DataSet();            
            try
            {
                con.Open();
                da.Fill(ds, nameTable);
                con.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка. " + e.Message);
                
            }

            DataTable dt = ds.Tables.Add();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return dt;
        }

        static public void AlterTableПоликлинника(string query, string connectionString, string nameTable)
        {
            OleDbConnection con = new OleDbConnection(connectionString);

            con.Open();
            OleDbCommand com = new OleDbCommand(query, con);

            com.ExecuteNonQuery();

            con.Close();

            //OleDbDataAdapter da = new OleDbDataAdapter(query, connectionString);

            ////string код = string.Empty;
            //DataSet ds = new DataSet();
            //con.Open();
            //da.Fill(ds, nameTable);
            //con.Close();

            //return ds.Tables[0];

        }

        /// <summary>
        /// Возвращает код поликлинники
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public DataTable GetTable(string query, string nameTable, OleDbConnection con, OleDbTransaction transact)
        {
            //выполним команду в единой транзакции
            OleDbCommand com = new OleDbCommand(query, con);
            com.Transaction = transact;

            //настроим адаптер данных
            OleDbDataAdapter da = new OleDbDataAdapter(com);

            //Заполним таблицу данными
            DataSet ds = new DataSet();
            da.Fill(ds, nameTable);

            return ds.Tables[0];
        }
    }
}
