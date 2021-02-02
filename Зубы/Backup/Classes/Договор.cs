using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    public class Договор
    {
        private int _id_договор;
        private string _НомерДоговора;
        private DateTime _ДатаДоговора;
        private DateTime _ДатаАктаВыполненныхРабот;
        private decimal _СуммаАктаВыполненныхРабот;
        private int _id_льготнаяКатегория;
        private int _id_поликлинника;
        private string _Примечаниме;
        private int _id_комитет;
        private bool _ФлагНаличияДоговора;
        private bool _ФлагНаличияАкта;
        private int _id_льготник;

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

        public string НомерДоговора
        {
            get
            {
                return _НомерДоговора;
            }
            set
            {
                _НомерДоговора = value;
            }
        }

        public DateTime ДатаДоговора
        {
            get
            {
                return _ДатаДоговора;
            }
            set
            {
                _ДатаДоговора = value;
            }
        }

        public DateTime ДатаАктаВыполненныхРабот
        {
            get
            {
                return _ДатаАктаВыполненныхРабот;
            }
            set
            {
                _ДатаАктаВыполненныхРабот = value;
            }
        }

        public decimal СуммаАктаВыполненныхРабот
        {
            get
            {
                return _СуммаАктаВыполненныхРабот;
            }
            set
            {
                _СуммаАктаВыполненныхРабот = value;
            }
        }


        public int id_льготнаяКатегория
        {
            get
            {
                return _id_льготнаяКатегория;
            }
            set
            {
                _id_льготнаяКатегория = value;
            }
        }

        public int id_поликлинника
        {
            get
            {
                return _id_поликлинника;
            }
            set
            {
                _id_поликлинника = value;
            }
        }

        public string _Примечаним
        {
            get
            {
                return _Примечаниме;
            }
            set
            {
                _Примечаниме = value;
            }
        }
        
        
        public int id_комитет
        {
            get
            {
                return _id_комитет;
            }
            set
            {
                _id_комитет = value;
            }
        }


        public bool ФлагНаличияДоговора
        {
            get
            {
                return _ФлагНаличияДоговора;
            }
            set
            {
                _ФлагНаличияДоговора = value;
            }
        }

        public bool ФлагНаличияАкта
        {
            get
            {
                return _ФлагНаличияАкта;
            }
            set
            {
                _ФлагНаличияАкта = value;
            }
        }

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
    }
}
