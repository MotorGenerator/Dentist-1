using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DantistLibrary;

namespace Стамотология.Classes
{
    public class UnloadDate
    {
        private string startDate = string.Empty;
        private string endDate = string.Empty;
        private string льготКатегория = string.Empty;
        private List<Реестр> listРеестр;

        public UnloadDate(string dateStart, string dateEnd, string льготнаяКатегория, List<Реестр> listReestr)
        {
            startDate = dateStart;
            endDate = dateEnd;
            льготКатегория = льготнаяКатегория;
            listРеестр = listReestr;
        }


        public UnloadDate(string dateStart, string dateEnd, string льготнаяКатегория)
        {
            startDate = dateStart;
            endDate = dateEnd;
            льготКатегория = льготнаяКатегория;
        }


        /// <summary>
        /// Возвращает коллекцию 
        /// </summary>
        /// <returns></returns>
        public List<Unload> Выгрузка()
        {
            //Объявим список классов для выгрузки
            List<Unload> list = new List<Unload>();

           //Выполним в единой транзакции 
            using (OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString()))
            {
                //откроем транзакцию
                con.Open();
                OleDbTransaction transact = con.BeginTransaction();

                string queryЛК = "select * from ЛьготнаяКатегория where ЛьготнаяКатегория = '"+ льготКатегория +"' ";
                DataRow rowЛК = ТаблицаБД.GetTable(queryЛК,"ЛьготнаяКатегория",con,transact).Rows[0];

                //Получим данные таблицы договор за период
                //SELECT Договор.ДатаДоговора
                //FROM Договор
                //WHERE (([Договор]![ДатаДоговора]>#2/1/2013# And #3/6/2013#<[Договор]![ДатаДоговора]));

                //Рабочий вариант
                //string договор = "select * from Договор where ДатаДоговора > " + startDate + " And ДатаДоговора < " + endDate + " and id_льготнаяКатегория = " + Convert.ToInt32(rowЛК["id_льготнойКатегории"]) + " ";
                string договор = "select * from Договор where id_договор in (select id_договор from АктВыполнненныхРабот where ДатаПодписания >= " + startDate + " And ДатаПодписания < " + endDate + " ) and id_льготнаяКатегория = " + Convert.ToInt32(rowЛК["id_льготнойКатегории"]) + " and ДатаДоговора is not NULL";

                //string договор = "SELECT ЛьготнаяКатегория.ЛьготнаяКатегория AS ['Льготная категория'], Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, АктВыполнненныхРабот.НомерАкта, Договор.НомерДоговора, АктВыполнненныхРабот.ДатаПодписания, Sum(УслугиПоДоговору.Сумма) AS Сумма " +
                //                  "FROM (ЛьготнаяКатегория INNER JOIN Льготник ON ЛьготнаяКатегория.id_льготнойКатегории = Льготник.id_льготнойКатегории) INNER JOIN ((Договор INNER JOIN АктВыполнненныхРабот ON Договор.id_договор = АктВыполнненныхРабот.id_договор) INNER JOIN УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор) ON Льготник.id_льготник = Договор.id_льготник " +
                //                  "WHERE (((ЛьготнаяКатегория.ЛьготнаяКатегория)='" + льготКатегория.Trim() + "') AND ((Договор.ФлагНаличияАкта)=True)) " +
                //                  "GROUP BY ЛьготнаяКатегория.ЛьготнаяКатегория, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, АктВыполнненныхРабот.НомерАкта, Договор.НомерДоговора, АктВыполнненныхРабот.ДатаПодписания " +
                //                  "HAVING (((АктВыполнненныхРабот.ДатаПодписания)>= " + startDate + " And (АктВыполнненныхРабот.ДатаПодписания)< " + endDate + "))"; //AND (Договор.ФлагНаличияАкта <> True)



                DataTable табДоговор = ТаблицаБД.GetTable(договор, "Договор", con, transact);

                //if (табДоговор.Rows["НомерДоговора"].ToString().Trim() == "АРБ/851 ")
                //{
                //    string sTest = "Test";
                //}

                //int iCountCol = табДоговор.Columns.Count;

                
                //пройдёмся по таблице
                foreach (DataRow rowДоговор in табДоговор.Rows)
                {
                    //Создадим объект типа Unload
                    Unload unload = new Unload();


                    //Выгрузим классификатор услуг
                    string queryClassService = "select * from КлассификаторУслуги";
                    DataTable tabClassServices = ТаблицаБД.GetTable(queryClassService, ConnectionDB.ConnectionString(), "КлассификаторУслуг");

                    unload.КлассификаторУслуг = tabClassServices;

                    //Выгрузим вид услуг
                    string queryViewServices = "select * from ВидУслуги";
                    DataTable tabViewServices = ТаблицаБД.GetTable(queryViewServices, ConnectionDB.ConnectionString(), "ВидУслуги");

                    unload.ВидУслуги = tabViewServices;

                    //if (Convert.ToInt32(rowДоговор["id_льготник"]) == 319)
                    //{
                    //    string sTest = "Test";
                    //}

                    //Получим льготника с которым составлен договор
                    string queryЛьготник = "select * from Льготник where id_льготник = " + Convert.ToInt32(rowДоговор["id_льготник"]) + " ";
                    DataTable rowЛьготник = ТаблицаБД.GetTable(queryЛьготник, "Льготник", con, transact);//.Rows[0];

                    //Добавим в клон таблицы строку содержащую текущий договор
                    DataTable tДоговор = new DataTable("ДоговорЛиния");
                    tДоговор = табДоговор.Clone();

                    //Заполним строку данными из текущего договора
                    DataRow row = tДоговор.NewRow();
                    row[0] = rowДоговор[0];
                    row[1] = rowДоговор[1];
                    row[2] = rowДоговор[2];
                    row[3] = rowДоговор[3];
                    row[4] = rowДоговор[4];
                    row[5] = rowДоговор[5];
                    row[6] = rowДоговор[6];
                    row[7] = rowДоговор[7];
                    row[8] = rowДоговор[8];
                    row[9] = rowДоговор[9];
                    row[10] = rowДоговор[10];
                    row[11] = rowДоговор[11];
                    row[12] = rowДоговор[12];

                    tДоговор.Rows.Add(row);

                    //присвоим договор
                    unload.Договор = tДоговор;


                    //Добавим в uhnload льготника с которым подписан текущий договор
                    unload.Льготник = rowЛьготник;

                    //получим название льготной категории
                    unload.ЛьготнаяКатегория = льготКатегория;

                    //сохраним данные по поликлиннике
                    string queryПоликлинника = "select * from Поликлинника";
                    DataTable tabПоликлинника = ТаблицаБД.GetTable(queryПоликлинника, "Поликлинника", con, transact);//.Rows[0];
                    
                    unload.Поликлинника = tabПоликлинника;

                    //Сохраним ФИО Врача
                    string queryФиоВрач = "select * from ГлавВрач where id_главВрач = "+ Convert.ToInt32(tabПоликлинника.Rows[0]["id_главВрач"]) +" ";
                    DataRow rowФИО = ТаблицаБД.GetTable(queryФиоВрач, "ГлавВрач", con, transact).Rows[0];
                    
                    //запишем ФИО глав врача
                    unload.ФиоВрач = rowФИО["ФИО_ГлавВрач"].ToString();

                    //получим услуги по договору
                    string queryУслугиДоговор = "select * from УслугиПоДоговору where id_договор = " + Convert.ToInt32(rowДоговор["id_договор"]) + " ";
                    DataTable rowУслугиДоговор = ТаблицаБД.GetTable(queryУслугиДоговор, "УслугиПоДоговору", con, transact);

                    //добавим услуги по договору
                    unload.УслугиПоДоговору = rowУслугиДоговор;

                    string queryДопСоглашение = "select * from ДопСоглашение where id_договор = " + Convert.ToInt32(rowДоговор["id_договор"]) + " ";
                    DataTable tabДопСоглашение = ТаблицаБД.GetTable(queryДопСоглашение, "ДопСоглашение", con, transact);

                    //добавим доп соглашения
                    unload.ДопСоглашение = tabДопСоглашение;

                    //if (Convert.ToInt32(rowДоговор["id_договор"]) == 897)
                    //{
                    //    string sTest = "Test";
                    //}



                    //Получим акт выполненных работ
                    string queryАктВыполненныхРабот = "select * from АктВыполнненныхРабот  where id_договор = " + Convert.ToInt32(rowДоговор["id_договор"]) + " ";
                    DataTable tabАктВыполненныхРабот = ТаблицаБД.GetTable(queryАктВыполненныхРабот, "АктВыполнненныхРабот", con, transact);


                   

                    //Получим номер акта выполненных работ
                    int numAct = Convert.ToInt32(tabАктВыполненныхРабот.Rows[0]["id_акт"]);



                    foreach (Реестр реестр in listРеестр)
                    {
                        if (реестр.Id_акт == numAct)
                        {
                            unload.АктВыполненныхРабот = tabАктВыполненныхРабот;
                        }
                    }
                    //добавим объект unload типа Unload в List

                    //Сохраним сторку ТипДокумента
                    string queryТипДокумента = "select * from ТипДокумента where id_документ = " + Convert.ToInt32(rowЛьготник.Rows[0]["id_документ"]) + " ";
                    DataTable rowТипДокумента = ТаблицаБД.GetTable(queryТипДокумента, "ТипДокумента", con, transact);//.Rows[0];

                    unload.ТипДокумента = rowТипДокумента;

                    //получим наименование района
                    if (Convert.ToInt32(rowЛьготник.Rows[0]["id_район"]) != -1)
                    {
                        string queryНазваниеРайона = "select * from НаименованиеРайона where id_район = " + Convert.ToInt32(rowЛьготник.Rows[0]["id_район"]) + " ";
                        DataTable rТипДокумента = ТаблицаБД.GetTable(queryНазваниеРайона, "НаименованиеРайона", con, transact);//.Rows[0];

                        unload.НаименованиеРайона = rТипДокумента;
                    }

                    //получим наименование населённого пункта
                    if (Convert.ToInt32(rowЛьготник.Rows[0]["id_насПункт"]) != -1)
                    {
                        string queryНаселённыйПункт = "select * from НаселенныйПункт where id_насПункт = " + Convert.ToInt32(rowЛьготник.Rows[0]["id_насПункт"]) + " ";
                        DataTable rwТипДокумента = ТаблицаБД.GetTable(queryНаселённыйПункт, "НаселенныйПункт", con, transact);//.Rows[0];

                        unload.НаселённыйПункт = rwТипДокумента;
                    }
                    //else
                    //{
                    //    string queryСаратов = "select * from НаселенныйПункт where like %Саратов% ";
                    //    DataRow rowСаратов = ТаблицаБД.GetTable(queryСаратов, "НаселенныйПункт", con, transact).Rows[0];
                    //    unload.НаселённыйПункт = rowСаратов;
                    //}

                    list.Add(unload);
                }

                //явно закроем соединение с БД
                //con.Close();
                con.Dispose();
                
            }

            return list;
        }
    }
}
