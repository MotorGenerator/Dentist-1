using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class ДанныеДоговор
    {
        private string кодПоликлинники;

        /// <summary>
        /// Хранит код поликлинники
        /// </summary>
        public string КодПоликлинники
        {
            get
            {
                return кодПоликлинники;
            }
            set
            {
                кодПоликлинники = value;
            }
        }

    }
}
