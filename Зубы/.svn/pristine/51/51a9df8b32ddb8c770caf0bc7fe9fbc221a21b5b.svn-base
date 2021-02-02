using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Стамотология.Classes
{
    class UpdateEsculap:ICommand
    {
        Esculap esculap;
        public UpdateEsculap(int id_главВрач, string фиоИП, string фиоРП, string должностьИП, string должностьРП, string основание)
        {
            esculap = new Esculap();
            esculap.id_главВрач = id_главВрач;
            esculap.ДолжностьИменительный = должностьИП;
            esculap.ДолжностьРодительный = должностьРП;
            esculap.ФИО_ГлавВрачИменительный = фиоИП;
            esculap.ФИО_ГлавВрачРодительный = фиоРП;
            esculap.Основание = основание;
        }


        public void Execute()
        {
            string queryInsert = "update ГлавВрач " +
                                 "set ФИО_ГлавВрач = '" + esculap.ФИО_ГлавВрачИменительный + "' " +
                                 ", ФИО_РодительныйПадеж = '" + esculap.ФИО_ГлавВрачРодительный + "' " +
                                 ", Должность = ' " + esculap.ДолжностьИменительный + "' " +
                                 ", ДолжностьРодПадеж = '" + esculap.ДолжностьРодительный + "' " +
                                 ", Основание = '" + esculap.Основание + "' " +
                                 "where id_главВрач = " + esculap.id_главВрач + " ";

            OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString());
            OleDbCommand com = new OleDbCommand(queryInsert, con);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();

        }


    }
}
