using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    /// <summary>
    /// Возвращает первый символ в строке
    /// </summary>
    class ShortString
    {
        /// <summary>
        /// Возвращает первый символ в строке
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string Short(string str)
        {
            //определим длинну строки
            int length = str.Length;

            //удалим все символы из строки кроем первого
            string sShort =  str.Remove(1, length - 1);

            return sShort;
        }
    }
}
