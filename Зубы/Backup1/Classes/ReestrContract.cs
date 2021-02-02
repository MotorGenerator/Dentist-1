using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Хранит реестр проектов договоров отправляемых на подпись
    /// </summary>
    class ReestrContract
    {
        private int порядковыйНомер;
        private string фио;
        private string номерДоговора;
        private string сумма;
        private bool flag;

        /// <summary>
        /// Хранит порядковый номер в таблице
        /// </summary>
        public int ПорядковыйНомер
        {
            get
            {
                return порядковыйНомер;
            }
            set
            {
                порядковыйНомер = value;
            }
        }


        /// <summary>
        /// Хранит сумму проекта договора
        /// </summary>
        public string ФИО
        {
            get
            {
                return фио;
            }
            set
            {
                фио = value;
            }
        }


        /// <summary>
        /// Хранит номер договора
        /// </summary>
        public string НомерДоговора
        {
            get
            {
                return номерДоговора;
            }
            set
            {
                номерДоговора = value;
            }
        }

        /// <summary>
        /// Хранит сумму проекта договора
        /// </summary>
        public string Сумма
        {
            get
            {
                return сумма;
            }
            set
            {
                сумма = value;
            }
        }

        /// <summary>
        /// Хранит состояние об отметке выбран договор или нет
        /// </summary>
        public bool Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }
    }
}
