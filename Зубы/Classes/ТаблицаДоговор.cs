using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Хранит строку таблицы в договоре
    /// </summary>
    public class ТаблицаДоговор
    {
        private string порядковыНомер;
        private string номерУслуги;
        private string наименование;
        private string цена;
        private string количество;
        private string стоимость;
        private bool flagPrint;
        private bool flagSelect;

        /// <summary>
        /// Флаг указывает что договор отправлен на печать
        /// </summary>
        public bool FlagPrint
        {
            get
            {
                return flagPrint;
            }
            set
            {
                flagPrint = value;
            }
        }
        /// <summary>
        /// Флаг указывает что данный договор уже был выбран
        /// </summary>
        public bool FlagSelect
        {
            get
            {
                return flagSelect;
            }
            set
            {
                flagSelect = value;
            }
        }

        public string ПорядковыНомер
        {
            get
            {
                return порядковыНомер;
            }
            set
            {
                порядковыНомер = value;
            }
        }

        public string НомерУслуги
        {
            get
            {
                return номерУслуги;
            }
            set
            {
                номерУслуги = value;
            }
        }

        public string Наименование
        {
            get
            {
                return наименование;
            }
            set
            {
                наименование = value;
            }
        }
        
        public string Цена
        {
            get
            {
                return цена;
            }
            set
            {
                цена = value;
            }
        }

        public string Количество
        {
            get
            {
                return количество;
            }
            set
            {
                количество = value;
            }
        }

        public string Стоимость
        {
            get
            {
                return стоимость;
            }
            set
            {
                стоимость = value;
            }
        }
    }
}
