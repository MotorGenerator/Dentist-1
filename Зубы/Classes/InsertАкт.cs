using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class InsertАкт:ICommand
    {
        //Акт akt;
        //string flag = string.Empty;

        //public InsertАкт(string номерАкта, int id_договор, DateTime датаПодписанияАкта, bool флагПодписания, string номерПоПеречню, string наименованиеУслуги, decimal цена, int количество)
        //{
        //    akt = new Акт();
        //    akt.НомерАкта = номерАкта;
        //    akt.id_договор = id_договор;
        //    akt.ДатаПодписанияАкта = датаПодписанияАкта;
        //    akt.ФлагПодписания = флагПодписания;
        //    string номер = номерПоПеречню.Replace(',', '.');
        //    akt.НомерПоПеречню = номер;
        //    akt.НаименованиеУслуги = наименованиеУслуги;
        //    akt.Цена = цена;
        //    akt.Количество = количество;
        //    akt.Сумма = Math.Round(цена * количество, 2);
        //}

        //public InsertАкт(string номерАкта, int id_договор, bool флагПодписания, string номерПоПеречню, string наименованиеУслуги, decimal цена, int количество)
        //{
        //    akt = new Акт();
        //    akt.НомерАкта = номерАкта;
        //    akt.id_договор = id_договор;
        //    akt.ФлагПодписания = флагПодписания;
        //    string номер = номерПоПеречню.Replace(',', '.');
        //    akt.НомерПоПеречню = номер;
        //    akt.НаименованиеУслуги = наименованиеУслуги;
        //    akt.Цена = цена;
        //    akt.Количество = количество;
        //    akt.Сумма = Math.Round(цена * количество, 2);
        //}

        //public InsertАкт(string номерАкта, int id_договор, bool флагПодписания, string номерПоПеречню, string наименованиеУслуги, decimal цена, int количество, string flagДопСоглашения)
        //{
        //    akt = new Акт();
        //    akt.НомерАкта = номерАкта;
        //    akt.id_договор = id_договор;
        //    akt.ФлагПодписания = флагПодписания;
        //    string номер = номерПоПеречню.Replace(',', '.');
        //    akt.НомерПоПеречню = номер;
        //    akt.НаименованиеУслуги = наименованиеУслуги;
        //    akt.Цена = цена;
        //    akt.Количество = количество;
        //    akt.Сумма = Math.Round(цена * количество, 2);

        //    flag = flagДопСоглашения;
        //}

        private int _id_договор;
        private string _номерАкта;
        private string _flag;

        public InsertАкт(int id_договор, string номерАкта,string flag)
        {
            _id_договор = id_договор;
            _номерАкта = номерАкта;
            _flag = flag;
        }

        public void Execute()
        {
            string query = "insert into АктВыполнненныхРабот(НомерАкта,id_договор,ФлагПодписания) " +
                           "values('" + _номерАкта + "'," + _id_договор + ",'" + _flag + "') ";

            //string query = "insert into АктВыполнненныхРабот(НомерАкта,id_договор,ФлагПодписания)values('" + akt.НомерАкта + "'," + akt.id_договор + ",'" + akt.ФлагПодписания + "' )";
            //применим частный случай паттерна фасад
            string connectionString = ConnectionDB.ConnectionString();
            Query.Execute(query, connectionString);
        }


    }
}
