using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Стамотология.Classes;
using System.Data;


namespace Стамотология.Classes
{
    public static class ExesTable
    {

        public static bool Exec()
        {
            bool flag = false;

            //string queryExecTable = "select txtShortHospital,ЕКС,ОКТМО from Реквизиты2021";
            string queryExecTable = "select * from Реквизиты2021";

            try
            {
                Query.Execute(queryExecTable, ConnectionDB.ConnectionString());

                DataTable tabMin = ТаблицаБД.GetTable(queryExecTable, ConnectionDB.ConnectionString(), "Реквизиты2021");

                var countRow = tabMin.Rows.Count;

                flag = true;
            }
            catch
            {
                //string queryCreate = string.Empty;

                //queryCreate = @"CREATE TABLE Реквизиты2021
                //            ( idМинистерство AUTOINCREMENT PRIMARY KEY,
                //              txtShortHospital char(100) NULL,
                //              ЕКС char(20) NULL,
                //              ОКТМО char(20) NULL
                //            )";

                //Query.Execute(queryCreate, ConnectionDB.ConnectionString());

                //string queryInsert = @"INSERT INTO Реквизиты2021 (txtShortHospital,ЕКС,ОКТМО) 
                //                     VALUES('Введите ГКУ','00000000000000000000','00000000000000000000') ";

                //Query.Execute(queryInsert, ConnectionDB.ConnectionString());

                //flag = true;

                InsertTable.Execute();
            }

            return flag;
        }
    }
}
