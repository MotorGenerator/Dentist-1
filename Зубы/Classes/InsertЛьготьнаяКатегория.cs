using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;


namespace Стамотология.Classes
{
    /// <summary>
    /// Добавляет данные 
    /// </summary>
    class InsertЛьготьнаяКатегория:ICommand
    {
        private string _льготнаяКатегория;

        public InsertЛьготьнаяКатегория(string льготнаяКатегория)
        {
            _льготнаяКатегория = льготнаяКатегория;
        }


        public void Execute()
        {
            string sCon = ConnectionDB.ConnectionString();
            //OleDbConnection con = new OleDbConnection(sCon);

            string query = "INSERT INTO ЛьготнаяКатегория " +
                           "([ЛьготнаяКатегория]) VALUES " +
                           "('" + _льготнаяКатегория + "')";

            Query.Execute(query, sCon);

            //OleDbCommand com = new OleDbCommand(query, con);

            //con.Open();
            //com.ExecuteNonQuery();
            //con.Close();
        }

        
    }
}
