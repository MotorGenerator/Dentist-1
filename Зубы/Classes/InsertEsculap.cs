using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    /// <summary>
    /// Добавляет запись в таблицу ГлавВрач
    /// </summary>
    class InsertEsculap:ICommand 
    {
        Esculap esculap;
        public InsertEsculap(string фиоИП, string фиоРП, string должностьИП, string должностьРП, string основание)
        {
            esculap = new Esculap();
            esculap.ДолжностьИменительный = должностьИП;
            esculap.ДолжностьРодительный = должностьРП;
            esculap.ФИО_ГлавВрачИменительный = фиоИП;
            esculap.ФИО_ГлавВрачРодительный = фиоРП;
            esculap.Основание = основание;



        }

        /// <summary>
        /// Выполняет инчструкцию на вставку
        /// </summary>
        public void Execute()
        {
            string queryInsert = "insert into ГлавВрач(ФИО_ГлавВрач,ФИО_РодительныйПадеж,Должность,ДолжностьРодПадеж,Основание) values('" + esculap.ФИО_ГлавВрачИменительный + "','" + esculap.ФИО_ГлавВрачРодительный + "','" + esculap.ДолжностьИменительный + "','" + esculap.ДолжностьРодительный + "','" + esculap.Основание + "')";

            OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString());
            OleDbCommand com = new OleDbCommand(queryInsert, con);
            
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

        }


    }
}
