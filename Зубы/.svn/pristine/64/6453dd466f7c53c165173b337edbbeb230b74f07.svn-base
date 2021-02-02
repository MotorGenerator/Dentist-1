using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class DeleteЛьготнаяКатегория:ICommand 
    {
        private int id;

        public DeleteЛьготнаяКатегория(int id_льготнойКатегории)
        {
            id = id_льготнойКатегории;
        }

                    
        public void  Execute()
        {
            string sCon = ConnectionDB.ConnectionString();
            //OleDbConnection con = new OleDbConnection(sCon);

            string query = "DELETE FROM ЛьготнаяКатегория " +
                           "WHERE id_льготнойКатегории = "+ id +" ";

            Query.Execute(query, sCon);
        }

    }
}
