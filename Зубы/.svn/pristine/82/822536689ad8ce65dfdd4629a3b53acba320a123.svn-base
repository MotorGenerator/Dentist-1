using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Стамотология.Classes
{
    public static class ConvertTo
    {
        /// <summary>
        /// Преобразуем коллекцию в DataTable.
        /// </summary>
        /// <param name="dictionaty"></param>
        /// <returns></returns>
        public static DataTable DataRowsArray(IDictionary<int, string> dictionaty)
        {
            // Экземпляр таблицы.
            DataTable tab = new DataTable();

            // Создадим и добавим к таблице колонки.
            DataColumn idColumn = new DataColumn("id_район", Type.GetType("System.Int16"));
            DataColumn NameRegion = new DataColumn("РайонОбласти", Type.GetType("System.String"));

            tab.Columns.Add(idColumn);
            tab.Columns.Add(NameRegion);

            if (dictionaty != null)
            {
                // Заполним колонки данными из словоря.
                foreach (KeyValuePair<int, string> keyValue in dictionaty)
                {
                    DataRow row = tab.NewRow();
                    row["id_район"] = keyValue.Key;
                    row["РайонОбласти"] = keyValue.Value;

                    tab.Rows.Add(row);
                }
            }

            return tab;
        }
    }
}
