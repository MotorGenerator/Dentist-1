using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    /// <summary>
    /// Отображает результат запроса в виде таблицы
    /// </summary>
    public class ДанныеПредставление
    {

        //public ДанныеПредставление()
        //{

        //}
        
        /// <summary>
        /// Возвращает результат запроса в виде таблицы DateTable
        /// </summary>
        /// <param name="query">запрос к БД на TSQL</param>
        /// <param name="tableName">Название таблицы</param>
        /// <returns></returns>
        static public DataTable GetПредставление(string query, string tableName)
        {
            string sCon = ConnectionDB.ConnectionString();
            DataSet ds = new DataSet();            
            ////string query = "SELECT [id_льготнойКатегории] ,[ЛьготнаяКатегория] FROM ЛьготнаяКатегория";
            ////try
            ////{
                OleDbConnection con = new OleDbConnection(sCon);
                con.Open();
                //OleDbCommand com = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(query, con);

                
                da.Fill(ds, tableName);
                con.Close();
            ////}
            ////catch (Exception e)
            ////{
            ////    System.Windows.Forms.MessageBox.Show("Ошибка. " + e.Message);
            ////}

            ////DataTable dt = new DataTable();
            ////dt.Columns.Add("id_льготник", typeof(String));
            ////dt.Columns.Add("id_льготнойКатегории", typeof(String));
            ////dt.Columns.Add("id_документ", typeof(String));
            ////dt.Columns.Add("id_область", typeof(String));
            ////dt.Columns.Add("id_насПункт", typeof(String));
            ////dt.Columns.Add("id_район", typeof(String));

            ////if (ds.Tables.Count > 0)
                return ds.Tables[0];
            ////else
            ////    return dt;
        }

        static public List<ВидУслуг> GetListПредставление(string query, string tableName)
        {
            string sCon = ConnectionDB.ConnectionString();

            //string query = "SELECT [id_льготнойКатегории] ,[ЛьготнаяКатегория] FROM ЛьготнаяКатегория";

            OleDbConnection con = new OleDbConnection(sCon);
            OleDbCommand com = new OleDbCommand(query, con);
            
            con.Open();
            //объявим список

            List<ВидУслуг> list = new List<ВидУслуг>();
            
            OleDbDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ВидУслуг item = new ВидУслуг();
                item.id_услуги = Convert.ToInt32(read["id_услуги"]);
                item.ВидУслуги = read["ВидУслуги"].ToString();
                
                item.Цена = Convert.ToDecimal(read["Цена"]);
                item.НомерПоПеречню = read["НомерПоПеречню"].ToString();

                item.Выбрать = false;

                list.Add(item);
                //item.Количество = read[""]
            }

            //OleDbDataAdapter da = new OleDbDataAdapter(query, con);

            //DataSet ds = new DataSet();
            //da.Fill(ds, tableName);
            con.Close();
            return list;
        }
                
    }
}
