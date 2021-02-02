using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    public class InsertВрач:ICommand
    {
        private string _фамилия = string.Empty;



        public InsertВрач(string фамилия)
        {
            _фамилия = фамилия;

        }


        public void Execute()
        {
            string query = "insert into Врач(ФИО) values('" + _фамилия.Trim() + "')";
            Query.Execute(query, ConnectionDB.ConnectionString());
        }
    }
}
