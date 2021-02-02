using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Класс описывает поликлиннику
    /// </summary>
    public class Hospital
    {
        private int _id_поликлинника;
        private string _НаименованиеПоликлинники;
        private string _КодПоликлинники;
        private string _ЮридическийАдрес;
        private string _ФактическийАдрес;
        private string _ФИО_ГлавВрач;
        private string _ФИО_ГлавБух;
        private int _id_главВрач;
        private int _id_главБух;
        private string _СвидетельствоРегистрации;
        private string _ИНН;
        private string _КПП;
        private string _БИК;
        private string _НаименованиеБанка;
        private string _РасчётСчёт;
        private string _ЛицевойСчёт;
        private string _НаименованиеКлиента;
        private string _НомерЛицензии;
        private string _ДатаРегистрацииЛицензии;
        private string _ОГРН;
        private string _СвидетельствоРегистрацииЕГРЮЛ;
        //private string _ОрганВыдавшийСвидетельство;
        private string _ОрганВыдавшийЛицензию;
        private string _Постановление;
        private string _ОКПО;
        private string _ОКАТО;
        private string _Phone;
        private string _Email;
        private string _Исполнитель;

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

        public string Исполнитель
        {
            get
            {
                return _Исполнитель;
            }
            set
            {
                _Исполнитель = value;
            }
        }

        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }

        public string НаименованиеПоликлинники
        {
            get
            {
                return _НаименованиеПоликлинники;
            }
            set
            {
                _НаименованиеПоликлинники = value;
            }
        }


        public string КодПоликлинники
        {
            get
            {
                return _КодПоликлинники;
            }
            set
            {
                _КодПоликлинники = value;
            }
        }

        public string ЮридическийАдрес
        {
            get
            {
                return _ЮридическийАдрес;
            }
            set
            {
                _ЮридическийАдрес = value;
            }
        }


        public string ФактическийАдрес
        {
            get
            {
                return _ФактическийАдрес;
            }
            set
            {
                _ФактическийАдрес = value;
            }
        }

        /// <summary>
        /// ФИО Глав Врача
        /// </summary>
        public string ФИО_ГлавВрач
        {
            get
            {
                return _ФИО_ГлавВрач;
            }
            set
            {
                _ФИО_ГлавВрач = value;
            }
        }

        /// <summary>
        /// ФИО Глав Буха
        /// </summary>
        public string ФИО_ГлавБух
        {
            get
            {
                return _ФИО_ГлавБух;
            }
            set
            {
                _ФИО_ГлавБух = value;
            }
        }

        public int id_главВрач
        {
            get
            {
                return _id_главВрач;
            }
            set
            {
                _id_главВрач = value;
            }
        }

        public int id_главБух
        {
            get
            {
                return _id_главБух;
            }
            set
            {
                _id_главБух = value;
            }
        }

        public string СвидетельствоРегистрации
        {
            get
            {
                return _СвидетельствоРегистрации;
            }
            set
            {
                _СвидетельствоРегистрации = value;
            }
        }


        public string ИНН
        {
            get
            {
                return _ИНН;
            }
            set
            {
                _ИНН = value;
            }
        }


        public string КПП
        {
            get
            {
                return _КПП;
            }
            set
            {
                _КПП = value;
            }
        }


        public string БИК
        {
            get
            {
                return _БИК;
            }
            set
            {
                _БИК = value;
            }
        }

        public string НаименованиеБанка
        {
            get
            {
                return _НаименованиеБанка;
            }
            set
            {
                _НаименованиеБанка = value;
            }
        }

        public string РасчётСчёт
        {
            get
            {
                return _РасчётСчёт;
            }
            set
            {
                _РасчётСчёт = value;
            }
        }

        public string ЛицевойСчёт
        {
            get
            {
                return _ЛицевойСчёт;
            }
            set
            {
                _ЛицевойСчёт = value;
            }
        }


        public string НаименованиеКлиента
        {
            get
            {
                return _НаименованиеКлиента;
            }
            set
            {
                _НаименованиеКлиента = value;
            }
        }

        public string НомерЛицензии
        {
            get
            {
                return _НомерЛицензии;
            }
            set
            {
                _НомерЛицензии = value;
            }
        }

        public string ДатаРегистрацииЛицензии
        {
            get
            {
                return _ДатаРегистрацииЛицензии;
            }
            set
            {
                _ДатаРегистрацииЛицензии = value;
            }
        }

        public string ОГРН
        {
            get
            {
                return _ОГРН;
            }
            set
            {
                _ОГРН = value;
            }
        }


        public string СвидетельствоРегистрацииЕГРЮЛ
        {
            get
            {
                return _СвидетельствоРегистрацииЕГРЮЛ;
            }
            set
            {
                _СвидетельствоРегистрацииЕГРЮЛ = value;
            }
        }

        //public string ОрганВыдавшийСвидетельство
        //{
        //    get
        //    {
        //        return _ОрганВыдавшийСвидетельство;
        //    }
        //    set
        //    {
        //        _ОрганВыдавшийСвидетельство = value;
        //    }
        //}


        public string ОрганВыдавшийЛицензию
        {
            get
            {
                return _ОрганВыдавшийЛицензию;
            }
            set
            {
                _ОрганВыдавшийЛицензию = value;
            }
        }
        


        public string Постановление
        {
            get
            {
                return _Постановление;
            }
            set
            {
                _Постановление = value;
            }
        }

        public string ОКПО
        {
            get
            {
                return _ОКПО;
            }
            set
            {
                _ОКПО = value;
            }
        }

        public string ОКАТО
        {
            get
            {
                return _ОКАТО;
            }
            set
            {
                _ОКАТО = value;
            }
        }

    }
}
