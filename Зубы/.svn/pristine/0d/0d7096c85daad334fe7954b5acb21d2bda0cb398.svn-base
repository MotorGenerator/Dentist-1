using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    class УслугиДоговор
    {
        static public bool ValidateУслуги(string query)
        {
            //получим строку подключения к БД
            string sCon = ConnectionDB.ConnectionString();

            OleDbConnection con = new OleDbConnection(sCon);
            OleDbCommand com = new OleDbCommand(query, con);

            //переменная для хранения следующего номера догвора
            bool flag = false;

            con.Open();
            OleDbDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                if (Convert.ToBoolean(read[0]) == false)
                {
                    //вдруг предыдущие договоры были закрыты
                    flag = false;
                    break;
                }
                else
                {
                    flag = true;
                }
            }

            return flag;
        }
    }
}
