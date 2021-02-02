using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class DeleteЛьготник:ICommand
    {
        private int _id_льготник;
        public DeleteЛьготник(int id_льготник)
        {
            _id_льготник = id_льготник;
        }

        public void Execute()
        {
            string query = "delete from Льготник where id_льготник = " + _id_льготник + " ";
            string sCon = ConnectionDB.ConnectionString();
            Query.Execute(query, sCon);
        }

        
    }
}
