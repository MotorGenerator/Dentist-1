using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Обнавляет таблицу главный бухгалтер
    /// </summary>
    class UpdateБухгалтер:ICommand
    {
        private int _id;
        private string _фио;
        public UpdateБухгалтер(int id, string фио)
        {
            _id = id;
            _фио = фио;
        }

        public void Execute()
        {
            string query = "update ГлавБух " +
                           "set ФИО_ГлавБух = '"+ _фио +"' " +
                           "where id_главБух = "+ _id +" ";

            Query.Execute(query, ConnectionDB.ConnectionString());
        }


    }
}
