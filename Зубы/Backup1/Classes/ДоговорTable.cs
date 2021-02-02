using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    public class ДоговорTable
    {
        /// <summary>
        /// Возвращает номер договора
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public string GetNumberДоговор(string query, string connectionString)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand com = new OleDbCommand(query, con);

            //переменная для хранения следующего номера догвора
            string номерДоговора = string.Empty;

            con.Open();
            OleDbDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                номерДоговора = read[0].ToString();
            }
            return номерДоговора;
        }

        static public int GetIdДоговор(string query, string connectionString)
        {
            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand com = new OleDbCommand(query, con);

            //переменная для хранения следующего номера догвора
            int id = 0;

            con.Open();
            OleDbDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                id = Convert.ToInt32(read[0]);
            }
            return id;
        }
       
        /// <summary>
        /// Проверяет подписан договор или нет
        /// </summary>
        /// <param name="id_договор"></param>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public bool CloseДоговор(string query)
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
                flag = Convert.ToBoolean(read[0]);
            }

            return flag;
        }


        /// <summary>
        /// Проверяет есть ли открытые договры
        /// </summary>
        /// <param name="id_договор"></param>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public bool CloseДоговоры(string query)
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
