using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class Esculap
    {
        private int _id_главВрач;
        private string _ФИО_ГлавВрачИменительный;
        private string _ФИО_ГлавВрачРодительный;
        private string _ДолжностьИменительный;
        private string _ДолжностьРодительный;
        private string _Основание;

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

        public string ФИО_ГлавВрачИменительный
        {
            get
            {
                return _ФИО_ГлавВрачИменительный;
            }
            set
            {
                _ФИО_ГлавВрачИменительный = value;
            }
        }

        public string ФИО_ГлавВрачРодительный
        {
            get
            {
                return _ФИО_ГлавВрачРодительный;
            }
            set
            {
                _ФИО_ГлавВрачРодительный = value;
            }
        }

        public string ДолжностьИменительный
        {
            get
            {
                return _ДолжностьИменительный;
            }
            set
            {
                _ДолжностьИменительный = value;
            }
        }

        public string ДолжностьРодительный
        {
            get
            {
                return _ДолжностьРодительный;
            }
            set
            {
                _ДолжностьРодительный = value;
            }
        }

        public string Основание
        {
            get
            {
                return _Основание;
            }
            set
            {
                _Основание = value;
            }
        }
    }
}
