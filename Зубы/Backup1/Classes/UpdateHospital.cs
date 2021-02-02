using System;
using System.Collections.Generic;
using System.Text;

namespace Стамотология.Classes
{
    class UpdateHospital:ICommand 
    {
         Hospital hosp;
        private int cDogovor = 0;

        private string phone = string.Empty;
        private string email = string.Empty;

        private string исполнитель = string.Empty;

        /// <summary>
        /// Хранит E-mail.
        /// </summary>
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

        /// <summary>
        /// Хранит E-mail.
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        public UpdateHospital(int id, string наименованиеПоликлинники, string кодПоликлинники, string юридическийАдрес, string фактическийАдрес, int id_главВрач, int id_главБух, string свидетельствоРегистрации, string ИНН, string КПП, string БИК, string наименованиеБанка, string расчётныйСчёт, string лицевойСчёт, string номерЛицензии, string датаРегистрацииЛицензии, string свидетельствоРегистрацииЕГРЮЛ, string ОГРН, string органВыдавшийЛицензию, string постановление, string ОКПО, string ОКАТО, int countDogovor)
        {
            hosp = new Hospital();
            hosp.id_поликлинника = id;
            hosp.НаименованиеПоликлинники = наименованиеПоликлинники;
            hosp.КодПоликлинники = кодПоликлинники;

            hosp.ЮридическийАдрес = юридическийАдрес;
            hosp.ФактическийАдрес = фактическийАдрес;

            hosp.id_главВрач = id_главВрач;
            hosp.id_главБух = id_главБух;

            hosp.СвидетельствоРегистрации = свидетельствоРегистрации;
            hosp.ИНН = ИНН;

            hosp.КПП = КПП;
            hosp.БИК = БИК;

            hosp.НаименованиеБанка = наименованиеБанка;
            hosp.РасчётСчёт = расчётныйСчёт;

            hosp.ЛицевойСчёт = лицевойСчёт;
            hosp.НомерЛицензии = номерЛицензии;

            hosp.ДатаРегистрацииЛицензии = датаРегистрацииЛицензии;
            hosp.ОГРН = ОГРН;

            hosp.СвидетельствоРегистрацииЕГРЮЛ = свидетельствоРегистрацииЕГРЮЛ;
            hosp.ОрганВыдавшийЛицензию = органВыдавшийЛицензию;

            hosp.Постановление = постановление;
            hosp.ОКПО = ОКПО;

            hosp.ОКАТО = ОКАТО;
            cDogovor = countDogovor;
        }

        public void Execute()
        {
            string query = "update Поликлинника " +
                           "set НаименованиеПоликлинники = '" + hosp.НаименованиеПоликлинники + "' " +
                           ",КодПоликлинники = '" + hosp.КодПоликлинники + "' " +
                           ",ЮридическийАдрес = '" + hosp.ЮридическийАдрес + "' " +
                           ",ФактическийАдрес = '" + hosp.ФактическийАдрес + "' " +
                           ",id_главВрач = " + hosp.id_главВрач + " " +
                           ",id_главБух = " + hosp.id_главБух + " " +
                           ",СвидетельствоРегистрации = '" + hosp.СвидетельствоРегистрации + "' " +
                           ",ИНН = '" + hosp.ИНН + "' " +
                           ",КПП = '" + hosp.КПП + "' " +
                           ",БИК = '" + hosp.БИК + "' " +
                           ",НаименованиеБанка = '" + hosp.НаименованиеБанка + "' " +
                           ",РасчётныйСчёт = '" + hosp.РасчётСчёт + "' " +
                           ",ЛицевойСчёт = '" + hosp.ЛицевойСчёт + "' "+
                           ",НомерЛицензии = '" + hosp.НомерЛицензии + "' " +
                           ",ДатаРегистрацииЛицензии = '" + hosp.ДатаРегистрацииЛицензии + "' " +
                           ",ОГРН = '" + hosp.ОГРН + "' " +
                           ",СвидетельствоРегистрацииЕГРЮЛ = '" + hosp.СвидетельствоРегистрацииЕГРЮЛ + "' " +
                           ",ОрганВыдавшийЛицензию = '" + hosp.ОрганВыдавшийЛицензию + "' " + 
                           ",Постановление = '" + hosp.Постановление + "' " +
                           ",ОКПО = '" + hosp.ОКПО + "' " +
                           ",ОКАТО = '" + hosp.ОКАТО + "' " +
                           ",НачальныйНомерДоговора = " + cDogovor + " " +
                           ",НомерТелефона = '"+ this.Phone.Trim() +"' " +
                           ",email = '" + this.Email.Trim() + "' " +
                           ",Исполнитель = '" + this.Исполнитель.Trim() + "' " + 
                           "where id_поликлинника = "+ hosp.id_поликлинника +" ";

            Query.Execute(query, ConnectionDB.ConnectionString());
        }

    }
}
