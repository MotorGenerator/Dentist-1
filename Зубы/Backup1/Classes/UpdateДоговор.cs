using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class UpdateДоговор:ICommand
    {
        Договор договор;

        public UpdateДоговор(int id_договор,DateTime дата, bool flag)
        {
            договор = new Договор();
            договор.id_договор = id_договор;
            
            договор.ДатаДоговора = дата;
            договор.ФлагНаличияДоговора = flag;

        }

        public void Execute()
        {
             
            string query = "update Договор " +
                               "set ДатаДоговора = '"+ договор.ДатаДоговора +"' " +
                               ",ФлагНаличияДоговора = "+ договор.ФлагНаличияДоговора +" " +
                               "where id_договор = " + договор.id_договор + " ";
                
                //применим частный случай паттерна фасад
                string connectionString = ConnectionDB.ConnectionString();
                Query.Execute(query, connectionString);
        }

        
    }
}
