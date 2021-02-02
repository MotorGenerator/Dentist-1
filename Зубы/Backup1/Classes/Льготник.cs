using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class Льготник
    {
        private int _id_льготник;
        private string _Фамилия;
        private string _Имя;
        private string _Отчество;
        private DateTime _ДатаРождения;
        private string _улица;
        private string _НомерДома;
        private string _корпус;
        private string _НомерКвартиры;
        private string _СерияПаспорта;
        private string _НомерПаспорта;
        private DateTime _ДатаВыдачиПаспорта;
        private string _КемВыданПаспорт;
        private int _id_льготнойКатегории;
        private int _id_документ;
        private string _СерияДокумента;
        private string _НомерДокумента;
        private DateTime _ДатаВыдачиДокумента;
        private string _КемВыданДокумент;
        private int _id_область;
        private int _id_район;
        private int _id_насПункт;
        private string snils;
        private string flagRegion;
        

        public int id_льготник
        {
            get
            {
                return _id_льготник;
            }
            set
            {
                _id_льготник = value;
            }
        }

        public string Фамилия
        {
            get
            {
                return _Фамилия;
            }
            set
            {
                _Фамилия = value;
            }
        }

        public string Имя
        {
            get
            {
                return _Имя;
            }
            set
            {
                _Имя = value;
            }
        }

        public string Отчество
        {
            get
            {
                return _Отчество;
            }
            set
            {
                _Отчество = value;
            }
        }

        public DateTime ДатаРождения
        {
            get
            {
                return _ДатаРождения;
            }
            set
            {
                _ДатаРождения = value;
            }
        }


        public string улица
        {
            get
            {
                return _улица;
            }
            set
            {
                _улица = value;
            }
        }

        public string НомерДома
        {
            get
            {
                return _НомерДома;
            }
            set
            {
                _НомерДома = value;
            }
        }

        public string корпус
        {
            get
            {
                return _корпус;
            }
            set
            {
                _корпус = value;
            }
        }


        public string НомерКвартиры
        {
            get
            {
                return _НомерКвартиры;
            }
            set
            {
                _НомерКвартиры = value;
            }
        }

        public string СерияПаспорта
        {
            get
            {
                return _СерияПаспорта;
            }
            set
            {
                _СерияПаспорта = value;
            }
        }

        public string НомерПаспорта
        {
            get
            {
                return _НомерПаспорта;
            }
            set
            {
                _НомерПаспорта = value;
            }
        }

        public DateTime ДатаВыдачиПаспорта
        {
            get
            {
                return _ДатаВыдачиПаспорта;
            }
            set
            {
                _ДатаВыдачиПаспорта = value;
            }
        }


        public string КемВыданПаспорт
        {
            get
            {
                return _КемВыданПаспорт;
            }
            set
            {
                _КемВыданПаспорт = value;
            }
        }

        public int id_льготнойКатегории
        {
            get
            {
                return _id_льготнойКатегории;
            }
            set
            {
                _id_льготнойКатегории = value;
            }
        }

        public int id_документ
        {
            get
            {
                return _id_документ;
            }
            set
            {
                _id_документ = value;
            }
        }

        public string СерияДокумента
        {
            get
            {
                return _СерияДокумента;
            }
            set
            {
                _СерияДокумента = value;
            }
        }

        public string НомерДокумента
        {
            get
            {
                return _НомерДокумента;
            }
            set
            {
                _НомерДокумента = value;
            }
        }

        public DateTime ДатаВыдачиДокумента
        {
            get
            {
                return _ДатаВыдачиДокумента;
            }
            set
            {
                _ДатаВыдачиДокумента = value;
            }
        }
        

        public string КемВыданДокумент
        {
            get
            {
                return _КемВыданДокумент;
            }
            set
            {
                _КемВыданДокумент = value;
            }
        }

        public int id_область
        {
            get
            {
                return _id_область; 
            }
            set
            {
                _id_область = value;
            }
        }

        public int id_район
        {
            get
            {
                return _id_район;
            }
            set
            {
                _id_район = value;
            }
        }

        public int id_насПункт
        {
            get
            {
                return _id_насПункт;
            }
            set
            {
                _id_насПункт = value;
            }
        }

        public string SNILS
        {
            get
            {
                return snils;
            }
            set
            {
                snils = value;
            }
        }

        public string FlagRaion
        {
            get
            {
                return flagRegion;
            }
            set
            {
                flagRegion = value;
            }
        }


    }
}
