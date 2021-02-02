using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class InsertАктДопСоглашениеЗакрытый:ICommand 
    {
        
        Акт akt;
        string flag = string.Empty;
        string датаЗакрыт = string.Empty;

        public InsertАктДопСоглашениеЗакрытый(string номерАкта, int id_договор, bool флагПодписания, string номерПоПеречню, string наименованиеУслуги, decimal цена, int количество, string flagДопСоглашения,string датаЗакрытия)
        {
            akt = new Акт();
            akt.НомерАкта = номерАкта;
            akt.id_договор = id_договор;
            akt.ФлагПодписания = флагПодписания;
            string номер = номерПоПеречню.Replace(',', '.');
            akt.НомерПоПеречню = номер;
            akt.НаименованиеУслуги = наименованиеУслуги;
            akt.Цена = цена;
            akt.Количество = количество;
            akt.Сумма = Math.Round(цена * количество, 2);

            flag = flagДопСоглашения;
            датаЗакрыт = датаЗакрытия;
        }



        public void Execute()
        {
            string query = "insert into АктВыполнненныхРабот(НомерАкта,id_договор,ФлагПодписания,ДатаПодписания,НомерПоПеречню,НаименованиеУслуги,Цена,Количество,Сумма,ФлагДопСоглашение) " +
                           "values('" + akt.НомерАкта + "'," + akt.id_договор + ",'" + akt.ФлагПодписания + "','"+ датаЗакрыт +"' ,'" + akt.НомерПоПеречню + "', '" + akt.НаименованиеУслуги + "','" + akt.Цена + "', " + akt.Количество + ",'" + akt.Сумма + "','" + flag + "') ";

            //string query = "insert into АктВыполнненныхРабот(НомерАкта,id_договор,ФлагПодписания)values('" + akt.НомерАкта + "'," + akt.id_договор + ",'" + akt.ФлагПодписания + "' )";
            //применим частный случай паттерна фасад
            string connectionString = ConnectionDB.ConnectionString();
            Query.Execute(query, connectionString);
        }


    }
}
