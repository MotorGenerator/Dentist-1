using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Стамотология.Classes
{
    class ConnectionDB
    {
        static public string ConnectionString()
        {
            return //ConfigurationSettings.AppSettings["connect"].ToString(); //connectionString

                ConfigurationManager.AppSettings["connect"].ToString();

            //string sCon = string.Empty;
            //sCon = ConfigurationSettings.AppSettings["connectionString"].ToString();

            //return sCon;
        }
    }
}
