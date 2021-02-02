using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Microsoft.Office.Interop.Word;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.Office.Interop.Word;

using Стамотология.Classes;
using DantistLibrary;

namespace Стамотология
{
    public partial class FormListContract : Form
    {
        //Бибилиотека для храпнения выбранных договоров
        private Dictionary<string, string> library;

        private List<ReestrПроектДоговорр> list;
        //private List<ReestrПроектДоговорр> list_sort;

        private int idLK = 0;

        private decimal summ = 0.0m;

        /// <summary>
        /// Свойство хранит id льготной категории.
        /// </summary>
        public int IdЛьготнаяКатегория
        {
            get
            {
                return idLK;
            }
            set
            {
                idLK = value;
            }
        }

        public FormListContract()
        {
            InitializeComponent();
        }

        private void FormListContract_Load(object sender, EventArgs e)
        {            
            //Заполним коллекцию 
            list = new List<ReestrПроектДоговорр>();
            //ReestrПроектДоговорр договор1 = new ReestrПроектДоговорр();
            //договор1.PNum = "№ п.п.";
            //договор1.FIO = "ФИО пациента";
            //договор1.Number = "№ договора";
            //договор1.Sum = "Сумма";
            //договор1.Flag = false;
            //договор1.Адрес = "Адрес пациента";
            //договор1.СерияНомерДокумента = "Серия, №  удостоверения";

            //list.Add(договор1);

            //Выполним всё в единой транзакции
            using (OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString()))
            {    
                con.Open();
                OleDbTransaction transact = con.BeginTransaction();
                
                //Получим номера проектов договоров
                string queryContr = "SELECT Договор.НомерДоговора " +
                                    "FROM Договор " +
                                    "WHERE (((Договор.ДатаДоговора) Is Null) AND ((Договор.id_льготнаяКатегория)=  " + this.IdЛьготнаяКатегория + " ));";


     
                DataTable tabContr = ТаблицаБД.GetTable(queryContr, "НомераДоговоров", con, transact);

                int countRows = tabContr.Rows.Count;

               
              

                //Счётчик для хранения порядковых номеров
                int iCount = 1;

                foreach (DataRow row in tabContr.Rows)
                {
                    string query = "SELECT Льготник.id_льготник, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.НомерДоговора, SUM(УслугиПоДоговору.Сумма) as 'Сумма', Льготник.улица, Льготник.НомерДома,Льготник.корпус,Льготник.НомерКвартиры, " +
                                   " Льготник.СерияДокумента, Льготник.НомерДокумента " +//, Льготник.СНИЛС
                                   "FROM (Льготник INNER JOIN Договор ON Льготник.id_льготник = Договор.id_льготник) INNER JOIN УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор " +
                                   "GROUP BY Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.НомерДоговора, УслугиПоДоговору.Сумма, Льготник.улица, Льготник.НомерДома,Льготник.корпус,Льготник.НомерКвартиры,Льготник.id_льготник,Льготник.СерияДокумента, Льготник.НомерДокумента " +//, Льготник.СНИЛС " +
                                   "HAVING (((Договор.НомерДоговора)='" + row["НомерДоговора"].ToString() + "'))";

                        DataTable tabДоговор = ТаблицаБД.GetTable(query, "Договор", con, transact);
                        //Запишем данные по проектам договоров в коллекцию

                        decimal sum = 0m;
                        string фамилия = string.Empty;
                        string имя = string.Empty;
                        string отчество = string.Empty;
                        string адрес = string.Empty;
                        string серияНомерДокумента = string.Empty;
                        //string snils = string.Empty;



                        foreach (DataRow r in tabДоговор.Rows)
                        {
                            фамилия = r["Фамилия"].ToString();
                            имя = r["Имя"].ToString();
                            отчество = r["Отчество"].ToString();

                            //if (фамилия.Trim().ToLower() == "мошанцев")
                            //{
                            //    string sTest = "";
                            //}
                                    

                            sum = Math.Round(Math.Round(sum, 2) + Math.Round(Convert.ToDecimal(r["'Сумма'"]), 2), 2);

                            StringBuilder build = new StringBuilder();

                            if (r["улица"].ToString().Length > 0)
                                build.Append("ул. " + r["улица"].ToString().Trim());

                            if (r["НомерДома"].ToString().Length > 0)
                                build.Append(" д. " + r["НомерДома"].ToString().Trim());

                            if (r["корпус"].ToString().Trim().Length > 0)
                                build.Append(" д. " + r["корпус"].ToString().Trim());

                            if (r["НомерКвартиры"].ToString().Trim().Length > 0)
                                build.Append("кв. " + r["НомерКвартиры"].ToString().Trim());

                            адрес = build.ToString().Trim();

                            StringBuilder buildSerDoc = new StringBuilder();
                            buildSerDoc.Append(r["СерияДокумента"].ToString().Trim() + " " + r["НомерДокумента"].ToString().Trim());

                            серияНомерДокумента = buildSerDoc.ToString().Trim();
                            //snils = r["СНИЛС"].ToString();
                        }

                        //Запишем данные в коллекцию
                        ReestrПроектДоговорр договор = new ReestrПроектДоговорр();
                        договор.PNum = iCount;
                        договор.FIO = фамилия + " " + имя + " " + отчество;
                        договор.Number = row["НомерДоговора"].ToString().Trim();
                        договор.Sum = sum.ToString();
                        договор.Flag = false;
                        договор.Адрес = адрес;
                        договор.СерияНомерДокумента = серияНомерДокумента;
                        //договор.SNILS = snils;


                        if (договор.Sum != "0")
                        {
                            list.Add(договор);
                        }

                        iCount++;
                    

                }



                this.dataGridView1.DataSource = list;               

                this.dataGridView1.Columns["PNum"].HeaderText = "№ п.п.";
                this.dataGridView1.Columns["PNum"].DisplayIndex = 0;

                this.dataGridView1.Columns["FIO"].HeaderText = "ФИО";
                this.dataGridView1.Columns["FIO"].DisplayIndex = 1;

                this.dataGridView1.Columns["Number"].HeaderText = "Номер договора";
                this.dataGridView1.Columns["Number"].DisplayIndex = 2;

                this.dataGridView1.Columns["Sum"].HeaderText = "Сумма по договору";
                this.dataGridView1.Columns["Sum"].DisplayIndex = 3;

                this.dataGridView1.Columns["Flag"].HeaderText = "Выбрать";
                this.dataGridView1.Columns["Flag"].DisplayIndex = 4;

                this.dataGridView1.Columns["Адрес"].Visible = false;
                this.dataGridView1.Columns["СерияНомерДокумента"].Visible = false;

                // Делаем запрет на редактирование таблицы кроме, флага выбора
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].ReadOnly = true;
                dataGridView1.Columns["Flag"].ReadOnly = false;

                //this.dataGridView1.Columns["SNILS"].HeaderText = "СНИЛС";
                //this.dataGridView1.Columns["SNILS"].Visible = false;
            }

                //string query = "select * from Договор where ДатаДоговора IS NULL ";
                //DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "Договор");
           
            //Создадим словарь для хранения выбранных договоров
            library = new Dictionary<string, string>();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Text = "Кол-во совпадений - ";

            ////Выберим договоры для выгрузки в реестр
            //string numDog = string.Empty;
            //if (this.dataGridView1.CurrentRow.Cells["Flag"].Selected == true)
            //{
            //    try
            //    {
            //        //если выбрали договор
            //        numDog = this.dataGridView1.CurrentRow.Cells["Number"].Value.ToString().Trim();
            //        library.Add(numDog, numDog);
            //    }
            //    catch
            //    {
            //        //если пользователь вторично выбрал ячейку значит он хочет снять флажок и убрать из коллекции договор
            //        library.Remove(numDog);
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Закроем форму
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<ReestrPrintПроектДоговоров> listPrint = new List<ReestrPrintПроектДоговоров>();

            ReestrPrintПроектДоговоров договор1 = new ReestrPrintПроектДоговоров();
            договор1.PNum = "№ п.п.";
            договор1.FIO = "ФИО пациента";
            договор1.Number = "№ договора";
            договор1.Sum = "Сумма";
            договор1.Flag = false;
            договор1.Адрес = "Адрес пациента";
            договор1.СерияНомерДокумента = "Серия, №  удостоверения";
            //договор1.SNILS = "СНИЛС";

            listPrint.Add(договор1);

            int iCountRow = 1;

            // //Пройдёмся по таблице DataGridView и поместим в коллекцию на сохранения только договора
            ////у которых ВыгрузкаПроектДоговоров столбце Сохранить договор
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {

                if (Convert.ToBoolean(row.Cells["Flag"].Value) == true)
                {
                    string numberDog = row.Cells["Number"].Value.ToString().Trim();

                    try
                    {
                        //Запишем в словарь
                        this.library.Add(numberDog, numberDog);

                        ReestrPrintПроектДоговоров договор = new ReestrPrintПроектДоговоров();
                        договор.PNum = iCountRow.ToString().Trim();
                        договор.FIO = row.Cells["FIO"].Value.ToString().Trim();
                        договор.Number = row.Cells["Number"].Value.ToString().Trim();

                        summ = summ + Convert.ToDecimal(row.Cells["Sum"].Value);

                        decimal sumTemp = Convert.ToDecimal(row.Cells["Sum"].Value);

                        договор.Sum = sumTemp.ToString("c");
                        договор.Flag = false;
                        договор.Адрес = row.Cells["Адрес"].Value.ToString().Trim();
                        договор.СерияНомерДокумента = row.Cells["СерияНомерДокумента"].Value.ToString().Trim();

                        //договор.SNILS = row.Cells["SNILS"].Value.ToString().Trim();

                        listPrint.Add(договор);

                        iCountRow++;

                    }
                    catch
                    {
                        //Выкиним из списка задвоенные номера
                        this.library.Remove(numberDog);
                    }
                }               
            }

            // Реесст на выгрузку проектов договоров.
            UnloadReestr reestr = new UnloadReestr();
            Dictionary<string, Unload> unload = reestr.Выгрузка(library);

            string queryHosp = "select * from Поликлинника";
            DataRow rowHosp = ТаблицаБД.GetTable(queryHosp, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0];

            string поликлинника = rowHosp["КодПоликлинники"].ToString();

            //Проверим если список List<Unload> не пустой
            if (unload.Count != 0)
            {

                //получим путь к файлу
                //SaveFileDialog saveFile = new SaveFileDialg();
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = string.Empty;
                saveFile.Filter = "All files (*.*)|*.*";

               

                //Получим текущую дату
                DateTime dt = DateTime.Today;

                string дата = dt.ToShortDateString();

                // Переменная хранит льготную категорию.
                string льготнаяКатегория = string.Empty;

                foreach(Unload it in unload.Values)
                {
                    льготнаяКатегория = it.ЛьготнаяКатегория.Trim();
                    break;
                }

                
                //Получим красивое название файла
                saveFile.FileName = поликлинника + "_Список_проектов_договоров_" + льготнаяКатегория + ".r";

                string fNameP = saveFile.FileName;

                string fileBinaryName = string.Empty;

                //saveFile.ShowDialog();

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    fileBinaryName = saveFile.FileName;
                    //saveFile.InitialDirectory = @".\";
                    //WorkingDirectory
                }
                else
                {
                    return;
                }

                //сериализуем список 
                FileStream fs = new FileStream(fileBinaryName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();

                //сериализация
                bf.Serialize(fs, unload);

                //Освободим в потоке все ресурсы
                fs.Dispose();
                fs.Close();

                //Установим для нашей программы текущую директорию для корректного считывания пути к БД
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

                //закроем окно выбора договоров
                this.Close();

                
                DataTable DT = new DataTable();
                DT = ДанныеПредставление.GetПредставление("SELECT flag FROM FlagForLetter; ", "FlagForLetter");
                string test = DT.Rows[0][0].ToString();

                // проверим наличие флага для печати word-овского файла.
                if (test == "1")
                {
                    // Выгрузим письмо.
                    try
                    {
                        //Скопируем шаблон в папку Документы
                        FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ПисьмоИсходящее.doc");
                        fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fNameP + ".doc", true);
                    }
                    catch
                    {
                        MessageBox.Show("Документ с таки именем уже существует");


                        // Закоментируем, чтобы не было искючения пустого хначения; создается 2-й документ идентичный первому
                        //bool directoryExists = Directory.Exists(fNameP);

                        //if (directoryExists == true)
                        //{
                        //    File.Delete(fNameP);
                        //}

                        //FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ПисьмоИсходящее.doc");
                        //fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fNameP + ".doc", true);

                    }

                    string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fNameP + ".doc";

                    //Создаём новый Word.Application
                    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                    //Загружаем документ
                    Microsoft.Office.Interop.Word.Document doc = null;

                    object fileName = filName;
                    object falseValue = false;
                    object trueValue = true;
                    object missing = Type.Missing;

                    doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                    // Выведим название поликлинники.
                    string hosp = rowHosp["НаименованиеПоликлинники"].ToString().Trim();

                    ////Номер договора
                    object wdrepl = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt = "hospital";
                    object newtxt = (object)hosp.Trim();
                    //object frwd = true;
                    object frwd = false;
                    doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                    ref missing, ref missing);

                    // Выведим юридический адрес.
                    string адрес = rowHosp["ЮридическийАдрес"].ToString().Trim();

                    ////Номер договора
                    object wdrepl2 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt2 = "address";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt2 = (object)адрес.Trim();
                    //object frwd = true;
                    object frwd2 = false;
                    doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                    ref missing, ref missing);

                    // Выведим номер телефона.
                    string phone = rowHosp["НомерТелефона"].ToString().Trim();

                    ////Номер договора
                    object wdrepl3 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt3 = "phone";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt3 = (object)phone.Trim();
                    //object frwd = true;
                    object frwd3 = false;
                    doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
                    ref missing, ref missing);

                    // Выведим E-mail.
                    string email = rowHosp["email"].ToString().Trim();

                    ////Номер договора
                    object wdrepl4 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt4 = "e-mail";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt4 = (object)email.Trim();
                    //object frwd = true;
                    object frwd4 = false;
                    doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
                    ref missing, ref missing);

                    // Получаем id врача.
                    int idВрач = Convert.ToInt32(rowHosp["id_главВрач"]);

                    string query = "select ФИО_ГлавВрач,Должность from ГлавВрач where id_главВрач = " + idВрач + " ";

                    DataTable tabВрач = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавВрач");

                    // Должность врача.
                    string должность = tabВрач.Rows[0]["Должность"].ToString().Trim();

                    //Должность врача.
                    object wdrepl5 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt5 = "Dolescul";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt5 = (object)должность.Trim();
                    //object frwd = true;
                    object frwd5 = false;
                    doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                    ref missing, ref missing);

                    // ФИО врача.
                    string fio = tabВрач.Rows[0]["ФИО_ГлавВрач"].ToString().Trim();

                    //Должность врача.
                    object wdrepl6 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt6 = "Fio";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt6 = (object)fio.Trim();
                    //object frwd = true;
                    object frwd6 = false;
                    doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
                    ref missing, ref missing);

                    string исполнитель = rowHosp["Исполнитель"].ToString().Trim();

                    // Выведим исполнителя.
                    object wdrepl7 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt7 = "Ispexecute";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt7 = (object)исполнитель.Trim();
                    //object frwd = true;
                    object frwd7 = false;
                    doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
                    ref missing, ref missing);

                    //// Получим льготную категорию.
                    //string queryLk = "select ЛьготнаяКатегория from ЛьготнаяКатегория where id_льготнойКатегории = "+ this.IdЛьготнаяКатегория +" ";

                    //DataTable tabLK = ТаблицаБД.GetTable(queryLk, ConnectionDB.ConnectionString(), "ЛьготнаяКатегория");

                    //string льготКатегория = tabLK.Rows[0]["ЛьготнаяКатегория"].ToString().Trim();

                    // Выведим льготную категорию.
                    object wdrepl8 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt8 = "Category";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt8 = (object)льготнаяКатегория.Trim();
                    //object frwd = true;
                    object frwd8 = false;
                    doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
                    ref missing, ref missing);

                    //Вставить таблицу с договорами.
                    object bookNaziv = "таблица";
                    Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                    object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                    object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


                    Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                    table.Range.ParagraphFormat.SpaceAfter = 6;
                    table.Columns[1].Width = 40;
                    table.Columns[2].Width = 80;
                    table.Columns[3].Width = 120;
                    table.Columns[4].Width = 80;//ширина столбца с номером акта
                    table.Columns[5].Width = 80;
                    table.Columns[6].Width = 80;
                    table.Borders.Enable = 1; // Рамка - сплошная линия
                    table.Range.Font.Name = "Times New Roman";
                    //table.Range.Font.Size = 10;
                    table.Range.Font.Size = 8;

                    // Выведем строку ИТОГО.
                    ReestrPrintПроектДоговоров договорCount = new ReestrPrintПроектДоговоров();
                    договорCount.FIO = "Итого:";
                    договорCount.Sum = summ.ToString("c");

                    listPrint.Add(договорCount);


                    //счётчик строк
                    int i = 1;

                    //запишем данные в таблицу
                    foreach (ReestrPrintПроектДоговоров item in listPrint)
                    {
                        table.Cell(i, 1).Range.Text = item.PNum;//.НомерПорядковый;

                        table.Cell(i, 2).Range.Text = item.Number;//.ФИО;

                        table.Cell(i, 3).Range.Text = item.FIO;
                        table.Cell(i, 4).Range.Text = item.Адрес;
                        table.Cell(i, 5).Range.Text = item.СерияНомерДокумента;

                       // table.Cell(i, 6).Range.Text = item.SNILS;

                        table.Cell(i, 6).Range.Text = item.Sum;

                        //doc.Words.Count.ToString();
                        Object beforeRow1 = Type.Missing;
                        table.Rows.Add(ref beforeRow1);

                        i++;
                    }
                    table.Rows[i].Delete();

                    app.Visible = true;

                    //закроем окно
                    this.Close();
                }             
              }
        }
        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //Проставим везде галочки
            foreach(DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells["Flag"].Value = true;
            }
        }

                   //параметр s2 - то что вводит пользователь в textBox
                   //параметр ind - задает столбец поиск в списке (фио, номер договора)
        private void search(string s2, string ind)
        {
            int k = 0;  //обнуляем счетчмк

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string s1 = dataGridView1.Rows[i].Cells[ind].Value.ToString().ToLower().Trim();
                int index = s1.IndexOf(s2);

                dataGridView1.Rows[i].Selected = false; //обнуляем выделения таблицы

                if (index >= 0)  //если нужно с  начала строки то ==
                {
                    dataGridView1.Rows[i].Selected = true;                    
                    k++;

                    //прокрутка списк до первой выделенноой строки
                    if (k == 1)
                        dataGridView1.FirstDisplayedScrollingRowIndex = i;
                }

                label2.Text = "Кол-во совпадений - " + k.ToString();  // обновляем label
            }

            // обнуление выделения и label-a при очистки поля поиска
            if (s2 == null || s2 == String.Empty)
            {
                label2.Text = "Кол-во совпадений - ";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Selected = false;
            }
        }

        // прокрутка выделенного списка по нажатию на Enter 
        public void enter(int ind)
        {
            ind++;
            for (int i = ind; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                    return;
                }
            }
        }

        //поиск по вводу символа
        private void tb_search_fio_TextChanged_1(object sender, EventArgs e)
        {
            search(tb_search_fio.Text.ToLower().Trim().ToString(), "FIO");
        }

        private void tb_search_dogovor_TextChanged(object sender, EventArgs e)
        {
            search(tb_search_dogovor.Text.ToLower().Trim().ToString(),"Number");
        }

        //обнуление другого поля по клику (обнуление поля поиск по ФИО(tb_search_fio)
                                         //по клику в поле поиска по договору(tb_search_dogovor))
        private void tb_search_fio_Click(object sender, EventArgs e)
        {
            tb_search_dogovor.Clear();
        }

        private void tb_search_dogovor_Click(object sender, EventArgs e)
        {            
            tb_search_fio.Clear();
        }

        private void tb_search_fio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)                               //параметром является индекс
                enter(dataGridView1.FirstDisplayedScrollingRowIndex);  //отображаемой первой строки
        }

        private void tb_search_dogovor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)                               //параметром является индекс
                enter(dataGridView1.FirstDisplayedScrollingRowIndex);  //отображаемой первой строки
        }
    }
}

