using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Стамотология.Classes
{
    public static class InsertTable
    {
        public static void Execute()
        {
            string queryCreate = string.Empty;

            queryCreate = @"CREATE TABLE Реквизиты2021
                            ( idМинистерство AUTOINCREMENT PRIMARY KEY,
                              txtShortHospital char(100) NULL,
                              ЕКС char(20) NULL,
                              ОКТМО char(20) NULL,
                              flagEKS BIT null
                            )";

            Query.Execute(queryCreate, ConnectionDB.ConnectionString());

            string queryInsert = @"INSERT INTO Реквизиты2021 (txtShortHospital,ЕКС,ОКТМО,flagEKS) 
                                     VALUES('Введите ГКУ','00000000000000000000','00000000000000000000',1) ";

            Query.Execute(queryInsert, ConnectionDB.ConnectionString());
        }
    }
}
