using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Обнуляет в базе данных Акт выполненных работ связанный с текущим договором
    /// </summary>
    class NullАктВыполненныхРабот:ICommand
    {
        private int _id_оговор;

        public NullАктВыполненныхРабот(int id_договор)
        {
            _id_оговор = id_договор;
        }


        public void Execute()
        {
            string query = "delete from АктВыполнненныхРабот "  +
                           "where id_договор = "+ _id_оговор +" ";

            //применим частный случай паттерна фасад
            string connectionString = ConnectionDB.ConnectionString();
            Query.Execute(query, connectionString);
        }

        
    }
}
