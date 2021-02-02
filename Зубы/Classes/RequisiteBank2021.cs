using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Реквизиты банка поправка от 2021 года.
    /// </summary>
    public class RequisiteBank2021 : ICommand
    {
        // Лицевой счет УФК.
        private string lk = string.Empty;

        // Корреспондентский счет.
        private string ks = string.Empty;

        // ОКТМО.
        private string oktmo = string.Empty;

        // id записи.
        private int id = 0;

        public RequisiteBank2021(string lk, string ks, string oktmo, int id)
        {
            this.lk = lk;
            this.ks = ks;
            this.oktmo = oktmo;
            this.id = id;
        }

        public void Execute()
        {
            // Запрос на обновление таблицы.
            string updateQuery = @"UPDATE Реквизиты2021 
                                  set txtShortHospital = '" + this.lk.Trim() +"', " +
                                  " ЕКС = '40102810845370000052' ," +
                                  " ОКТМО = '"+ this.oktmo.Trim() +"' " +
                                  " where idМинистерство = "+ this.id +" ";

            // Выполнение запроса.
            Query.Execute(updateQuery, ConnectionDB.ConnectionString());

        }
    }
}
