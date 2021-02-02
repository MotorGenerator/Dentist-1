using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    public class АктНазваниеУслуги
    {
        private string номер;
        private string номерУслуги;
        private string наименованиеУслуги;
        private string количество;
        private string сумма;


        //сумма
        public string Номер
        {
            get
            {
                return номер;
            }
            set
            {
                номер = value;
            }
        }

        //сумма
        public string НомерУслуги
        {
            get
            {
                return номерУслуги;
            }
            set
            {
                номерУслуги = value;
            }
        }

        //сумма
        public string НазваниеУслуги
        {
            get
            {
                return наименованиеУслуги;
            }
            set
            {
                наименованиеУслуги = value;
            }
        }

        //сумма
        public string КоличествоУсуг
        {
            get
            {
                return количество;
            }
            set
            {
                количество = value;
            }
        }

        //сумма
        public string СуммаУслуг
        {
            get
            {
                return сумма;
            }
            set
            {
                сумма = value;
            }
        }
    }
}
