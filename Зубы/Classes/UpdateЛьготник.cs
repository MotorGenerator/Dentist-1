﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class UpdateЛьготник:ICommand
    {
        Льготник льготник;
        private bool _flagРайон;
        public UpdateЛьготник(int id_льготник,string фамилия, string имя, string отчество, DateTime датаРождения, string улица, string номерДома, string корпус, string номерКв, string серияПаспорт, string номерПаспорт, DateTime датаВыдачиПаcпотра, string кемВыданПаспорт, int id_льготнаяКатегория, int id_типДокумента, string серияДокумента, string номерДокумента, DateTime датаВыдачиДокумента, string кемВыданДокумент,int id_область, int id_район, int id_насПункт, bool flagРайон, string snils, string flagRaion)
        {
            льготник = new Льготник();
            льготник.id_льготник = id_льготник;
            льготник.Фамилия = фамилия;

            льготник.Имя = имя;
            льготник.Отчество = отчество;

            льготник.ДатаРождения = датаРождения;

            льготник.улица = улица;

            льготник.НомерДома = номерДома;
            льготник.корпус = корпус;
            льготник.НомерКвартиры = номерКв;

            льготник.СерияПаспорта = серияПаспорт;
            льготник.НомерПаспорта = номерПаспорт;
            льготник.ДатаВыдачиПаспорта = датаВыдачиПаcпотра;

            льготник.КемВыданПаспорт = кемВыданПаспорт;
            льготник.id_льготнойКатегории = id_льготнаяКатегория;
            льготник.id_документ = id_типДокумента;

            льготник.СерияДокумента = серияДокумента;
            льготник.НомерДокумента = номерДокумента;
            льготник.ДатаВыдачиДокумента = датаВыдачиДокумента;

            льготник.КемВыданДокумент = кемВыданДокумент;
            льготник.id_район = id_район;

            льготник.id_насПункт = id_насПункт;
            льготник.id_область = 1;//Саратовская область id = 1

            _flagРайон = flagРайон;

            льготник.SNILS = snils;

            льготник.FlagRaion = flagRaion;
        }

        public void Execute()
        {
           //StringBuilder builder = new StringBuilder();


            if (_flagРайон == false)
            {
                string query = "update Льготник " +
                               " set Фамилия = '" + льготник.Фамилия + "' " +
                               ",Имя = '" + льготник.Имя + "' " +
                               ",Отчество = '" + льготник.Отчество + "' " +
                               ",ДатаРождения = '" + льготник.ДатаРождения + "' " +
                               ",улица = '" + льготник.улица + "' " +
                               ",НомерДома = '" + льготник.НомерДома + "' " +
                               ",корпус = '" + льготник.корпус + "' " +
                               ",НомерКвартиры = '" + льготник.НомерКвартиры + "' " +
                               ",СерияПаспорта = '" + льготник.СерияПаспорта + "' " +
                               ",НомерПаспорта = '" + льготник.НомерПаспорта + "' " +
                               ",ДатаВыдачиПаспорта = '" + льготник.ДатаВыдачиПаспорта + "' " +
                               ",КемВыданПаспорт = '" + льготник.КемВыданПаспорт + "' " +
                               ",id_льготнойКатегории = " + льготник.id_льготнойКатегории + " " +
                               ",id_документ = " + льготник.id_документ + " " +
                               ",СерияДокумента = '" + льготник.СерияДокумента + "' " +
                               ",НомерДокумента = '" + льготник.НомерДокумента + "' " +
                               ",ДатаВыдачиДокумента = '" + льготник.ДатаВыдачиДокумента + "' " +
                               ",КемВыданДокумент = '" + льготник.КемВыданДокумент + "' " +
                               ",id_область = " + льготник.id_область + " " +
                               ",id_район = " + льготник.id_район + " " +
                               ",id_насПункт = " + льготник.id_насПункт + " " +
                               ",СНИЛС = '" + льготник.SNILS + "' " +
                               ",flagRaion = '"+ льготник.FlagRaion +"' " +
                               "where id_льготник = " + льготник.id_льготник + " ";

                //builder.Append(query);

                //Внесём изменения в таблицу Договор
                string queryContract = " update Договор " +
                                        "set id_льготнаяКатегория =  "+ льготник.id_льготнойКатегории +" " +
                                         "where id_льготник = " + льготник.id_льготник + " ";

                //builder.Append(queryContract);
                //string executeQuery = builder.ToString();
                
                string sCon = ConnectionDB.ConnectionString();
                Query.Execute(query, sCon);
                Query.Execute(queryContract, sCon);
            }
            else
            {
                string query = "update Льготник " +
                               " set Фамилия = '" + льготник.Фамилия + "' " +
                               ",Имя = '" + льготник.Имя + "' " +
                               ",Отчество = '" + льготник.Отчество + "' " +
                               ",ДатаРождения = '" + льготник.ДатаРождения + "' " +
                               ",улица = '" + льготник.улица + "' " +
                               ",НомерДома = '" + льготник.НомерДома + "' " +
                               ",корпус = '" + льготник.корпус + "' " +
                               ",НомерКвартиры = '" + льготник.НомерКвартиры + "' " +
                               ",СерияПаспорта = '" + льготник.СерияПаспорта + "' " +
                               ",НомерПаспорта = '" + льготник.НомерПаспорта + "' " +
                               ",ДатаВыдачиПаспорта = '" + льготник.ДатаВыдачиПаспорта + "' " +
                               ",КемВыданПаспорт = '" + льготник.КемВыданПаспорт + "' " +
                               ",id_льготнойКатегории = " + льготник.id_льготнойКатегории + " " +
                               ",id_документ = " + льготник.id_документ + " " +
                               ",СерияДокумента = '" + льготник.СерияДокумента + "' " +
                               ",НомерДокумента = '" + льготник.НомерДокумента + "' " +
                               ",ДатаВыдачиДокумента = '" + льготник.ДатаВыдачиДокумента + "' " +
                               ",КемВыданДокумент = '" + льготник.КемВыданДокумент + "' " +
                               ",id_область = " + льготник.id_область + " " +
                               ",id_район = " + -1 + " " +
                               ",id_насПункт = " + льготник.id_насПункт + " " +
                               ",СНИЛС = '" + льготник.SNILS + "' " +
                               ",flagRaion = '" + льготник.FlagRaion + "' " +
                               "where id_льготник = " + льготник.id_льготник + " ";

                //builder.Append(query);

                //Внесём изменения в таблицу Договор
                string queryContract = " update Договор " +
                                        "set id_льготнаяКатегория =  " + льготник.id_льготнойКатегории + " " +
                                         "where id_льготник = " + льготник.id_льготник + " ";

                //builder.Append(queryContract);
                //string executeQuery = builder.ToString();

                string sCon = ConnectionDB.ConnectionString();
                Query.Execute(query, sCon);
                Query.Execute(queryContract, sCon);
            }
        }        
    }
}
