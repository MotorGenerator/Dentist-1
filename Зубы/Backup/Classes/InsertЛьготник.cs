﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Инкапсулируед запрос на вставку льготника
    /// </summary>
    class InsertЛьготник:ICommand
    {
        Льготник льготник;
        private bool _flagRaion;

        public InsertЛьготник(string фамилия, string имя, string отчество, DateTime датаРождения, string улица, string номерДома, string корпус, string номерКв, string серияПаспорт, string номерПаспорт, DateTime датаВыдачиПаcпотра, string кемВыданПаспорт, int id_льготнаяКатегория, 
            int id_типДокумента, string серияДокумента, string номерДокумента, 
            DateTime датаВыдачиДокумента, string кемВыданДокумент, int id_область, 
            int id_район, int id_насПункт, string snils, bool flagРайон, string flagRaion)
        {
            льготник = new Льготник();
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
            льготник.ДатаВыдачиДокумента  = датаВыдачиДокумента;

            льготник.КемВыданДокумент = кемВыданДокумент;
            льготник.id_область = id_область;

            льготник.id_район = id_район;
            льготник.id_насПункт = id_насПункт;

            льготник.SNILS = snils;
            
            _flagRaion = flagРайон;

            льготник.FlagRaion = flagRaion;

            //льготник.SNILS = snils;

        }
        
        //",'"+  +"' " +

        public void Execute()
        {
            if (_flagRaion == false)
            {
                string query = "insert into Льготник(Фамилия,Имя,Отчество,ДатаРождения,улица,НомерДома,корпус,НомерКвартиры,СерияПаспорта,НомерПаспорта,ДатаВыдачиПаспорта,КемВыданПаспорт,id_льготнойКатегории,id_документ,СерияДокумента,НомерДокумента,ДатаВыдачиДокумента,КемВыданДокумент,id_область,id_район, СНИЛС,id_насПункт,FlagRaion)" +
                               "values('" + льготник.Фамилия + "' " +
                                ",'" + льготник.Имя + "' " +
                                ",'" + льготник.Отчество + "' " +
                                ",'" + льготник.ДатаРождения + "' " +
                               ",'" + льготник.улица + "' " +
                                ",'" + льготник.НомерДома + "' " +
                                ",'" + льготник.корпус + "' " +
                                ",'" + льготник.НомерКвартиры + "' " +
                                ",'" + льготник.СерияПаспорта + "' " +
                                ",'" + льготник.НомерПаспорта + "' " +
                                ",'" + льготник.ДатаВыдачиПаспорта + "' " +
                                ",'" + льготник.КемВыданПаспорт.Replace("'",string.Empty) + "' " +
                                ",'" + льготник.id_льготнойКатегории + "' " +
                                ",'" + льготник.id_документ + "' " +
                                ",'" + льготник.СерияДокумента + "' " +
                                ",'" + льготник.НомерДокумента + "' " +
                                ",'" + льготник.ДатаВыдачиДокумента + "' " +
                                ",'" + льготник.КемВыданДокумент.Replace("'", string.Empty) + "' " +
                                "," + льготник.id_область + " " +
                                "," + льготник.id_район + " " +
                                 ",'" + льготник.SNILS + "' " +
                                 "," + льготник.id_насПункт + " " + 
                                 ", '"+ льготник.FlagRaion +"' ) ";
                                //",'" + льготник.SNILS + "' )";
                //",'"+  +"' " +
              
                //применим частный случай паттерна фасад
                string sCon = ConnectionDB.ConnectionString();
                Query.Execute(query, sCon);
            }
            else
            {
                string query = "insert into Льготник(Фамилия,Имя,Отчество,ДатаРождения,улица,НомерДома,корпус,НомерКвартиры,СерияПаспорта,НомерПаспорта,ДатаВыдачиПаспорта,КемВыданПаспорт,id_льготнойКатегории,id_документ,СерияДокумента,НомерДокумента,ДатаВыдачиДокумента,КемВыданДокумент,id_область,id_район ,СНИЛС,id_насПункт,FlagRaion)" +
                               "values('" + льготник.Фамилия + "' " +
                                ",'" + льготник.Имя + "' " +
                                ",'" + льготник.Отчество + "' " +
                                ",'" + льготник.ДатаРождения + "' " +
                               ",'" + льготник.улица + "' " +
                                ",'" + льготник.НомерДома + "' " +
                                ",'" + льготник.корпус + "' " +
                                ",'" + льготник.НомерКвартиры + "' " +
                                ",'" + льготник.СерияПаспорта + "' " +
                                ",'" + льготник.НомерПаспорта + "' " +
                                ",'" + льготник.ДатаВыдачиПаспорта + "' " +
                                ",'" + льготник.КемВыданПаспорт + "' " +
                                ",'" + льготник.id_льготнойКатегории + "' " +
                                ",'" + льготник.id_документ + "' " +
                                ",'" + льготник.СерияДокумента + "' " +
                                ",'" + льготник.НомерДокумента + "' " +
                                ",'" + льготник.ДатаВыдачиДокумента + "' " +
                                ",'" + льготник.КемВыданДокумент + "' " +
                                "," + льготник.id_область + " " +
                                ","+ -1 + " " +
                                ",'" + льготник.SNILS + "' " +
                                "," + льготник.id_насПункт + " " +
                                ", '" + льготник.FlagRaion + "' ) ";
                               // ",'" + льготник.SNILS + "' )";
                //",'"+  +"' " +

                //применим частный случай паттерна фасад
                string sCon = ConnectionDB.ConnectionString();
                Query.Execute(query, sCon);
            }
        }
    }
}
