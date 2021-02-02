using System;
using System.Collections.Generic;
using System.Text;
using DantistLibrary;
using Стамотология.Classes;
using System.Data;
using System.Data.OleDb;


namespace Стамотология.Classes
{
   
    class UnloadReestr
    {

        private bool flag = false;

        /// <summary>
        /// Устанавливает режим выгрузки всех договоров
        /// </summary>
        public bool FlagВыгрузка
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        //public List<Unload> Выгрузка(Dictionary<string, string> library)
        public Dictionary<string, Unload> Выгрузка(Dictionary<string, string> library)
        {
            //Словарь для хранения проектов договоров
            Dictionary<string, Unload> list = new Dictionary<string, Unload>();

            //Выполним в единой транзакции 
            using (OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString()))
            {
                //откроем транзакцию
                con.Open();
                OleDbTransaction transact = con.BeginTransaction();

                StringBuilder build = new StringBuilder();
                foreach(string val in library.Values)
                {
                    string zn = "'" + val + "'" + ",";
                    build.Append(zn);
                }

                if (build.Length != 0)
                {
                    //Узнаем длинну строки в символах
                    int numContracts = build.ToString().Length;

                    //Удалим последний символ ','
                    string numbersContr = build.ToString().Remove(numContracts - 1, 1);

                    string договор = "select * from Договор where НомерДоговора in (" + numbersContr + ") ";
                    DataTable табДоговор = ТаблицаБД.GetTable(договор, "Договор", con, transact);

                    //счётчик
                    int iCount = 1;

                    //пройдёмся по таблице договоров
                    foreach (DataRow rowДоговор in табДоговор.Rows)
                    {
                        //Создадим объект типа Unload
                        Unload unload = new Unload();

                        try
                        {
                            //Получим к какой льготной категории отностится льготник в текущем договоре
                            string queryЛК = "select ЛьготнаяКатегория from ЛьготнаяКатегория where id_льготнойКатегории = " + Convert.ToInt32(rowДоговор["id_льготнаяКатегория"]) + " ";
                            DataRow rowЛК = ТаблицаБД.GetTable(queryЛК, ConnectionDB.ConnectionString(), "ЛьготнаяКатегория").Rows[0];
                        
                        //получим название льготной категории
                        unload.ЛьготнаяКатегория = rowЛК["ЛьготнаяКатегория"].ToString();
                        }
                        catch
                        {
                            unload.ЛьготнаяКатегория = "0";
                        }

                        //Выгрузим классификатор услуг
                        string queryClassService = "select * from КлассификаторУслуги";
                        DataTable tabClassServices = ТаблицаБД.GetTable(queryClassService, ConnectionDB.ConnectionString(), "КлассификаторУслуг");

                        unload.КлассификаторУслуг = tabClassServices;

                        //Выгрузим вид услуг
                        string queryViewServices = "select * from ВидУслуги";
                        DataTable tabViewServices = ТаблицаБД.GetTable(queryViewServices, ConnectionDB.ConnectionString(), "ВидУслуги");

                        unload.ВидУслуги = tabViewServices;

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

                        //сохраним данные по поликлиннике
                        string queryПоликлинника = "select * from Поликлинника";
                        DataTable tabПоликлинника = ТаблицаБД.GetTable(queryПоликлинника, "Поликлинника", con, transact);//.Rows[0];

                        unload.Поликлинника = tabПоликлинника;

                        //Сохраним ФИО Врача
                        string queryФиоВрач = "select * from ГлавВрач where id_главВрач = " + Convert.ToInt32(tabПоликлинника.Rows[0]["id_главВрач"]) + " ";
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

                        //Получим акт выполненных работ
                        string queryАктВыполненныхРабот = "select * from АктВыполнненныхРабот  where id_договор = " + Convert.ToInt32(rowДоговор["id_договор"]) + " ";
                        DataTable tabАктВыполненныхРабот = ТаблицаБД.GetTable(queryАктВыполненныхРабот, "АктВыполнненныхРабот", con, transact);

                        unload.АктВыполненныхРабот = tabАктВыполненныхРабот;
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


                        //Получим номер договора
                        string numDog = rowДоговор["НомерДоговора"].ToString().Trim();

                        try
                        {
                            list.Add(numDog, unload);
                        }
                        catch
                        {
                            if (this.FlagВыгрузка == false)
                            {
                                //Выкиним из списка задвоенные номера
                                list.Remove(numDog);
                            }

                            if (this.FlagВыгрузка == true)
                            {
                                //Получим номер договора
                                string numDogAdd = rowДоговор["НомерДоговора"].ToString().Trim() + " " + iCount.ToString();
                                list.Add(numDogAdd, unload);

                                iCount++;
                            }
                        }

                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Не выбраны проекты договоров.", "Ошибка");
                }

                //явно закроем соединение с БД
                //con.Close();
                con.Dispose();

            }

            return list;
        }
    }
}
