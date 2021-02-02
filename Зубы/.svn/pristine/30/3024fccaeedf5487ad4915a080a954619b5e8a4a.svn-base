using System;
using System.Collections.Generic;
using System.Text;


namespace Стамотология.Classes
{
    class InsertБухгалтер:ICommand
    {
        string _фио;
        public InsertБухгалтер(string фио)
        {
            _фио = фио;
        }


        public void Execute()
        {
            string query = "insert into ГлавБух(ФИО_ГлавБух) values('"+ _фио +"')";
            Query.Execute(query, ConnectionDB.ConnectionString());
        }


    }
}
