using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    public class СтрокаДоговор
    {
        private int id;
        private string _НомерДоговора;
        private string _ДатаДоговора;
        private bool _ФлагНаличияДоговора;
        private string _ФлагДопСоглашения;
        private bool _ФлагНаличияАкта;

        public int id_договор
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
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

        public string ДатаДоговора
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

        public string ФлагДопСоглашения
        {
            get
            {
                return _ФлагДопСоглашения;
            }
            set
            {
                _ФлагДопСоглашения = value;
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
    }
}
