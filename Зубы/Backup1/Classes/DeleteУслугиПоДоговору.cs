using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{

    class DeleteУслугиПоДоговору:ICommand
    {
        private int id;

        public DeleteУслугиПоДоговору(int id_услугиДоговор)
        {
            id = id_услугиДоговор;
        }


        public void Execute()
        {
            string queryDelete = "delete from УслугиПоДоговору " +
                                "where id_услугиДоговор = " + id + " ";

            //применим частный случай паттерна фасад
            string connectionString = ConnectionDB.ConnectionString();
            Query.Execute(queryDelete, connectionString);
        }
                
    }
}
