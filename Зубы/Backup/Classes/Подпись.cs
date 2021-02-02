using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Строка для подписей в договоре
    /// </summary>
    class Подпись
    {
        private string исполнитель;
        private string потребитель;

        public string Исполнитель
        {
            get
            {
                return исполнитель;
            }
            set
            {
                исполнитель = value;
            }
        }

        public string Потребитель
        {
            get
            {
                return потребитель;
            }
            set
            {
                потребитель = value;
            }
        }

    }
}
