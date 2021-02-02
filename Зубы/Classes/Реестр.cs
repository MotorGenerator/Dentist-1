using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    public class Реестр
    {
        private int id_акт;
        private string категория;
        private string номерПорядковый;//
        private string фио;//
        private string номерДатаДоговора;//
        private string номерДатаакта;//
        private string серияДатаВыдачиДокумента;//
        private string стоимсотьУслуги;//
        //private string snils;
        private bool флагАктРеестр;



        /// <summary>
        /// Хранит id акта выполненных работ
        /// </summary>
        public int  Id_акт
        {
            get
            {
                return id_акт;
            }
            set
            {
                id_акт = value;
            }
        }



        /// <summary>
        /// Указывает, что данный акт попал в реестр
        /// </summary>
        public bool ФлагАктРеестр
        {
            get
            {
                return флагАктРеестр;
            }
            set
            {
                флагАктРеестр = value;
            }
        }


        public string Категория
        {
            get
            {
                return категория;
            }
            set
            {
                категория = value;
            }
        }

        public string НомерПорядковый
        {
            get
            {
                return номерПорядковый;
            }
            set
            {
                номерПорядковый = value;
            }
        }


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
        
        public string НомерДатаДоговора
        {
            get
            {
                return номерДатаДоговора;
            }
            set
            {
                номерДатаДоговора = value;
            }
        }


        public string НомерДатаАкта
        {
            get
            {
                return номерДатаакта;
            }
            set
            {
                номерДатаакта = value;
            }
        }

        public string СерияДатаВыдачиДокумента
        {
            get
            {
                return серияДатаВыдачиДокумента;
            }
            set
            {
                серияДатаВыдачиДокумента = value;
            }
        }


        //public string SNILS
        //{
        //    get
        //    {
        //        return snils;
        //    }
        //    set
        //    {
        //        snils = value;
        //    }
        //}


        public string СтоимсотьУслуги
        {
            get
            {
                return стоимсотьУслуги;
            }
            set
            {
                стоимсотьУслуги = value;
            }
        }
    }
}
