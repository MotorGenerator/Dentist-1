using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class ОтчётТехЛист
    {
        private string _номерПП = string.Empty;
        public string _фио = string.Empty;
        public string _номерДатаДоговора = string.Empty;
        public string _номерДатаАкта = string.Empty;
        public string _серияНомерДок = string.Empty;
        public string _стоимостьУслуг = string.Empty;
        public string _фиоВрач = string.Empty;
        public string _номерТехЛиста = string.Empty;


        /// <summary>
        /// Порядковый номер.
        /// </summary>
        public string НомерПП
        {
            get
            {
                return _номерПП;
            }
            set
            {
                _номерПП = value;
            }
        }

        /// <summary>
        /// ФИо льготника.
        /// </summary>
        public string ФиоЛьготника
        {
            get
            {
                return _фио;
            }
            set
            {
                _фио = value;
            }
        }

        /// <summary>
        /// Номер и дата договора на зубопротезирование.
        /// </summary>
        public string НомерДатаДоговора
        {
            get
            {
                return _номерДатаДоговора;
            }
            set
            {
                _номерДатаДоговора = value;
            }
        }

        /// <summary>
        /// Номер и дата акта приёма выполненных работ по зубопротезированию.
        /// </summary>
        public string НомерДатаАкта
        {
            get
            {
                return _номерДатаАкта;
            }
            set
            {
                _номерДатаАкта = value;
            }
        }

        /// <summary>
        /// Серия и номер документа дающего право на льготу.
        /// </summary>
        public string СерияНомерДок
        {
            get
            {
                return _серияНомерДок;
            }
            set
            {
                _серияНомерДок = value;
            }
        }

        /// <summary>
        /// Стоимость услуг.
        /// </summary>
        public string СтоимостьУслуг
        {
            get
            {
                return _стоимостьУслуг;
            }
            set
            {
                _стоимостьУслуг = value;
            }
        }

        /// <summary>
        /// Фио врача протезиста.
        /// </summary>
        public string ФиоВрач
        {
            get
            {
                return _фиоВрач;
            }
            set
            {
                _фиоВрач = value;
            }
        }

        /// <summary>
        /// Номер тех. листа.
        /// </summary>
        public string НомерТехЛиста
        {
            get
            {
                return _номерТехЛиста;
            }
            set
            {
                _номерТехЛиста = value;
            }
        }
    }
}
