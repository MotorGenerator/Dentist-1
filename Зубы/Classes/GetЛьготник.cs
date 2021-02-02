using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    class GetЛьготник
    {
        /// <summary>
        /// Возвращает указанную строку
        /// </summary>
        /// <returns></returns>
        static public Льготник GetRow(int id_льготник)
        {
            string sCon = ConnectionDB.ConnectionString();
            string query = "select * from Льготник where id_льготник = " + id_льготник + " ";
            
            //Объявим переменную льготник типа Льготник
            Льготник льготник = new Льготник();
            
            OleDbConnection con = new OleDbConnection(sCon);
            OleDbCommand com = new OleDbCommand(query, con);
            con.Open();

            OleDbDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                льготник.Фамилия = read["Фамилия"].ToString();
                льготник.Имя = read["Имя"].ToString();

                льготник.Отчество = read["Отчество"].ToString();
                льготник.ДатаРождения = Convert.ToDateTime(read["ДатаРождения"]);
                
                льготник.улица = read["улица"].ToString();
                льготник.НомерДома = read["НомерДома"].ToString();
               
                льготник.корпус = read["корпус"].ToString();
                льготник.НомерКвартиры = read["НомерКвартиры"].ToString();
                
                льготник.СерияПаспорта = read["СерияПаспорта"].ToString();
                льготник.НомерПаспорта = read["НомерПаспорта"].ToString();
                
                льготник.ДатаВыдачиПаспорта = Convert.ToDateTime(read["ДатаВыдачиПаспорта"]);
                льготник.КемВыданПаспорт = read["КемВыданПаспорт"].ToString();
                
                льготник.СерияДокумента = read["СерияДокумента"].ToString();
                льготник.НомерДокумента = read["НомерДокумента"].ToString();
                
                льготник.ДатаВыдачиДокумента = Convert.ToDateTime(read["ДатаВыдачиДокумента"]);
                льготник.КемВыданДокумент = read["КемВыданДокумент"].ToString();

                льготник.SNILS = read["СНИЛС"].ToString();

                льготник.id_район = Convert.ToInt32(read["id_район"]);

                льготник.FlagRaion = read["FlagRaion"].ToString().Trim();
            }
            con.Close();
            return льготник;
        }
    }
}
