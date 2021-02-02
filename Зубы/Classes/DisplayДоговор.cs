using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    public class DisplayДоговор
    {
        static public List<СтрокаДоговор> ДанныеДоговор(string query, string connectionString, string nameTable)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand com = new OleDbCommand(query, con);

            con.Open();

            List<СтрокаДоговор> list = new List<СтрокаДоговор>();

            OleDbDataReader read = com.ExecuteReader();
            while(read.Read())
            {
                СтрокаДоговор str = new СтрокаДоговор();
                str.id_договор  = Convert.ToInt32(read["id_договор"]);
                str.НомерДоговора = read["НомерДоговора"].ToString();
                if (read["ДатаДоговора"] != DBNull.Value)
                {
                    str.ДатаДоговора = Convert.ToDateTime(read["ДатаДоговора"]).ToShortDateString();
                }
                else
                {
                    str.ДатаДоговора = "";
                }
                str.ФлагНаличияДоговора = Convert.ToBoolean(read["ФлагНаличияДоговора"]);
                str.ФлагДопСоглашения = read["ФлагДопСоглашения"].ToString();
                list.Add(str);
            }
            //OleDbDataAdapter da = new OleDbDataAdapter(query, connectionString);

            ////string код = string.Empty;
            //DataSet ds = new DataSet();
            //con.Open();
            //da.Fill(ds, nameTable);
            //con.Close();

            return list;
        }

    }
}
