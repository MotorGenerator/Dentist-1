using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Хранит строку акта
    /// </summary>
    public class Акт
    {
        private int _id_акт;
        private string _НомерАкта;
        private int _id_договор;
        private DateTime _ДатаПодписанияАкта;
        private bool _ФлагПодписания;
        private string _НомерПоПеречню;
        private string _НаименованиеУслуги;
        private decimal _Цена;
        private int _Количество;
        private decimal _Сумма;


        public int id_акт
        {
            get
            {
                return _id_акт;
            }
            set
            {
                _id_акт = value;
            }
        }


        public string НомерАкта
        {
            get
            {
                return _НомерАкта;
            }
            set
            {
                _НомерАкта = value;
            }
        }


        public int id_договор
        {
            get
            {
                return _id_договор;
            }
            set
            {
                _id_договор = value;
            }
        }


        public DateTime ДатаПодписанияАкта
        {
            get
            {
                return _ДатаПодписанияАкта;
            }
            set
            {
                _ДатаПодписанияАкта = value;
            }
        }

        public bool ФлагПодписания
        {
            get
            {
                return _ФлагПодписания;
            }
            set
            {
                _ФлагПодписания = value;
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

        public string НаименованиеУслуги
        {
            get
            {
                return _НаименованиеУслуги;
            }
            set
            {
                _НаименованиеУслуги = value;
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

        public int Количество
        {
            get
            {
                return _Количество;
            }
            set
            {
                _Количество = value;
            }
        }

        public decimal Сумма
        {
            get
            {
                return _Сумма;
            }
            set
            {
                _Сумма = value;
            }
        }
    }
}
