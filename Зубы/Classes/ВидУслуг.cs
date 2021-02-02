using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    public class ВидУслуг
    {
        private int _id_услуги;
        private string _ВидУслуги;
        private decimal _Цена;
        private string _НомерПоПеречню;
        private int _количество;
        private bool _flag;

        public int id_услуги
        {
            get
            {
                return _id_услуги;
            }
            set
            {
                _id_услуги = value;
            }
        }

        public string ВидУслуги
        {
            get
            {
                return _ВидУслуги;
            }
            set
            {
                _ВидУслуги = value;
            }
        }

        public decimal Цена
        {
            get
            {
                return _Цена;
            }
            set
            {
                _Цена = value;
            }
        }

        public string НомерПоПеречню
        {
            get
            {
                return _НомерПоПеречню;
            }
            set
            {
                _НомерПоПеречню = value;
            }
        }

        /// <summary>
        /// Хранит количество
        /// </summary>
        public int Количество
        {
            get
            {
                return _количество;
            }
            set
            {
                _количество = value;
            }
        }

        /// <summary>
        /// Хранит значение выбрано или нет
        /// </summary>
        public bool Выбрать
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
            }
        }

    }
}
