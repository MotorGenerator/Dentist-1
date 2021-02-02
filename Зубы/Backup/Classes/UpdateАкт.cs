using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Вносит изменения в акт
    /// </summary>
    class UpdateАкт:ICommand
    {
        private int _id_договор;
        private DateTime _data;

        public UpdateАкт(int id_договор, DateTime data)
        {
            _id_договор = id_договор;
            _data = data;
        }

        /// <summary>
        /// При внесении изменния происходит удаление записей и вставка записей
        /// </summary>
        public void  Execute()
        {
            string query = "update АктВыполнненныхРабот " +
                           " set ФлагПодписания = 'True' " +
                           ", ДатаПодписания = '" + _data + "' " +
                           "where  id_договор = " + _id_договор + " ";


            //применим частный случай паттерна фасад
            string connectionString = ConnectionDB.ConnectionString();
            Query.Execute(query, connectionString);

        }


}
}
