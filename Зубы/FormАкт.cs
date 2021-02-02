using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.IO;
using Microsoft.Office.Interop.Word;


namespace Стамотология
{
    public partial class FormАкт : Form
    {
        private Договор договор;
        private List<int> списокДопСоглашений;
        private string номерПоликлинники = string.Empty;

        private string названиеПоликлинники;
        private string фиоЛьготника;

        //private string нормерАкта;

        private bool flagДопСоглашений;

        /// <summary>
        /// Флаг наличия доп соглашений
        /// </summary>
        public bool FlagДопСоглашений
        {
            get
            {
                return flagДопСоглашений;
            }
            set
            {
                flagДопСоглашений = value;
            }
        }


        //получим локальную дату
        string датаАкта = string.Empty;

        private string фиоГлаВрач = string.Empty;

        /// <summary>
        /// Хранит ФИО глав врача
        /// </summary>
        public string ФиоГлаВрач
        {
            get
            {
                return фиоГлаВрач;
            }
            set
            {
                фиоГлаВрач = value;
            }
        }


        /// <summary>
        /// Наименование поликлинники
        /// </summary>
        public string НазваниеПоликлинники
        {
            get
            {
                return названиеПоликлинники;
            }
            set
            {
                названиеПоликлинники = value;
            }
        }


        /// <summary>
        /// ФИО льготника
        /// </summary>
        public string ФиоЛьготника
        {
            get
            {
                return фиоЛьготника;
            }
            set
            {
                фиоЛьготника = value;
            }
        }

        /// <summary>
        /// Хранит номер и данные текущего договора
        /// </summary>
        public Договор CurrentДоговор
        {
            get
            {
                return договор;
            }
            set
            {
                договор = value;
            }
        }

        /// <summary>
        /// Хранит номер поликлинники
        /// </summary>
        public string НомерПоликлинники
        {
            get
            {
                return номерПоликлинники;
            }
            set
            {
                номерПоликлинники = value;
            }
        }

        /// <summary>
        /// Хранит список id доп соглашений
        /// </summary>
        public List<int> СписокДопСоглашений
        {
            get
            {
                return списокДопСоглашений;
            }
            set
            {
                списокДопСоглашений = value;
            }
        }


        

        public FormАкт()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //закроем форму
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //bool test = this.flagДопСоглашений;

            ////если форма работает в режиме доп соглашения т.е. flagДопСоглашений == true
            //if (this.flagДопСоглашений == false)
            //{

            //    string sCon = ConnectionDB.ConnectionString();
            //    //сохраним значения в акт
            //    if (this.checkBox1.Checked == true)
            //    {
            //        //нужно проверить есть ли такой акт в таблице если есть то update если нет то insert
            //        string queryCountAkt = "select count(НомерАкта) from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " order by НомерАкта disinct desc";
            //        DataTable tabCount = ТаблицаБД.GetTable(queryCountAkt, sCon, "АктВыполнненныхРабот");

            //        DataRow rC = tabCount.Rows[0];
            //        int count = Convert.ToInt32(rC[0]);

            //        if (count != 0)
            //        {
            //            Receiver recivaer = new Receiver();

            //            //узнаем количество строк
            //            int iCountRow = this.gvАкт.Rows.Count;

            //            //счётчик
            //            int iCount = 1;

            //            //акт подписан значит вносим изменение в БД
            //            //так как у нас 1 договор 1 акт то ориентируемся на id-договора
            //            foreach (DataGridViewRow r in this.gvАкт.Rows)
            //            {
            //                if (iCount <= iCountRow - 1)
            //                {
            //                    UpdateАкт akt = new UpdateАкт(this.договор.id_договор, this.dateTimePicker1.Value);
            //                    recivaer.Action(akt);
            //                }

            //                iCount++;
            //            }

            //        }

            //        int iCountRow1 = this.gvАкт.Rows.Count;


            //        //Создадим список
            //        List<АктНазваниеУслуги> list = new List<АктНазваниеУслуги>();

            //        //заполним шапку
            //        АктНазваниеУслуги шапка = new АктНазваниеУслуги();
            //        шапка.Номер = "№ п.п";
            //        шапка.НомерУслуги = "№ услуги в справочнике";
            //        шапка.НазваниеУслуги = "Наименование услуги";
            //        шапка.КоличествоУсуг = "Количество";
            //        шапка.СуммаУслуг = "Сумма, руб.";

            //        list.Add(шапка);

            //        string queryRowsAct = "select * from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            //        DataTable tabAct = ТаблицаБД.GetTable(queryRowsAct, sCon, "АктВыполнненныхРабот");

            //        decimal сумма = 0.0m;

            //        //счётчик
            //        int iCount1 = 1;

            //        foreach (DataRow row in tabAct.Rows)
            //        {
            //            АктНазваниеУслуги акт = new АктНазваниеУслуги();
            //            акт.Номер = iCount1.ToString();
            //            акт.НомерУслуги = row["НомерПоПеречню"].ToString().Replace(',', '.');
            //            акт.НазваниеУслуги = row["НаименованиеУслуги"].ToString();

            //            //акт.Номер = r.Cells["НомерПоПеречню"].Value.ToString();
            //            акт.КоличествоУсуг = row["Количество"].ToString();
            //            акт.СуммаУслуг = row["Сумма"].ToString();

            //            сумма = Math.Round(сумма, 2) + Math.Round(Convert.ToDecimal(row["Сумма"]), 2);

            //            list.Add(акт);
            //            датаАкта = Convert.ToDateTime(row["ДатаПодписания"]).ToShortDateString();

            //            iCount1++;
            //        }

            //        АктНазваниеУслуги подвал = new АктНазваниеУслуги();
            //        подвал.НомерУслуги = "Итого";
            //        подвал.СуммаУслуг = сумма.ToString("c");

            //        list.Add(подвал);

            //        //соберём данные для отчёта
            //        string номерДоговора = this.договор.НомерДоговора;

            //        //передадим номер акта
            //        string номерАкта = this.lblNextNumber.Text;

            //        string исполнитель = this.НазваниеПоликлинники;
            //        string потребитель = this.ФиоЛьготника;


            //        string fName = this.ФиоЛьготника;

            //        //Скопируем шаблон в папку Документы
            //        FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Акт4.doc");
            //        fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);

            //        string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";

            //        //Создаём новый Word.Application
            //        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();



            //        //Загружаем документ
            //        Microsoft.Office.Interop.Word.Document doc = null;

            //        object fileName = filName;
            //        object falseValue = false;
            //        object trueValue = true;
            //        object missing = Type.Missing;

            //        doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            //        ref missing, ref missing, ref missing, ref missing, ref missing,
            //        ref missing, ref missing, ref missing, ref missing, ref missing,
            //        ref missing, ref missing, ref missing);


            //        ////Номер договора
            //        object wdrepl = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt = "номердоговора";
            //        object newtxt = (object)номерДоговора;
            //        //object frwd = true;
            //        object frwd = false;
            //        doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl2 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt2 = "номеракта";
            //        object newtxt2 = (object)номерАкта;
            //        //object frwd = true;
            //        object frwd2 = false;
            //        doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl3 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt3 = "Больница";
            //        object newtxt3 = (object)исполнитель;
            //        //object frwd = true;
            //        object frwd3 = false;
            //        doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl4 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt4 = "Льготник";
            //        object newtxt4 = (object)потребитель;
            //        //object frwd = true;
            //        object frwd4 = false;
            //        doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            //        ref missing, ref missing);

            //        //выведим дату договора
            //        string датаДоговора = this.договор.ДатаДоговора.ToShortDateString();

            //        ////Номер договора
            //        object wdrepl5 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt5 = "датадоговора";
            //        object newtxt5 = (object)датаДоговора;
            //        //object frwd = true;
            //        object frwd5 = false;
            //        doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl6 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt6 = "датаакта";
            //        object newtxt6 = (object)датаАкта;
            //        //object frwd = true;
            //        object frwd6 = false;
            //        doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
            //        ref missing, ref missing);

            //        //ФИО глав врача Главврач
            //        object wdrepl7 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt7 = "Главврач";
            //        object newtxt7 = (object)this.ФиоГлаВрач;
            //        //object frwd = true;
            //        object frwd7 = false;
            //        doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
            //        ref missing, ref missing);

            //        string summa = Валюта.Рубли.Пропись(сумма);

            //        //Выведим сумму
            //        object wdrepl8 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt8 = "сумпрописью";
            //        object newtxt8 = (object)summa;
            //        //object frwd = true;
            //        object frwd8 = false;
            //        doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
            //        ref missing, ref missing);



            //        //Вставить таблицу
            //        object bookNaziv = "таблица";
            //        Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            //        object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            //        object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            //        Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 5, ref behavior, ref autobehavior);
            //        table.Range.ParagraphFormat.SpaceAfter = 6;
            //        table.Borders.Enable = 1; // Рамка - сплошная линия
            //        table.Range.Font.Name = "Times New Roman";
            //        table.Range.Font.Size = 10;

            //        //установим ширину столбцов
            //        table.Columns[1].Width = 40;
            //        table.Columns[2].Width = 80;
            //        table.Columns[2].AutoFit();
            //        table.Columns[3].Width = 180;
            //        table.Columns[4].Width = 60;
            //        table.Columns[5].Width = 100;

            //        table.Borders.Enable = 1; // Рамка - сплошная линия
            //        table.Range.Font.Name = "Times New Roman";
            //        table.Range.Font.Size = 10;
            //        //table.Range.HorizontalInVertical = WdHorizontalLineAlignment.wdHorizontalLineAlignCenter.;
            //        //счётчик строк
            //        int i = 1;

            //        //запишем данные в таблицу
            //        foreach (АктНазваниеУслуги item in list)
            //        {
            //            table.Cell(i, 1).Range.Text = item.Номер;

            //            table.Cell(i, 2).Range.Text = item.НомерУслуги;

            //            table.Cell(i, 3).Range.Text = item.НазваниеУслуги;
            //            table.Cell(i, 4).Range.Text = item.КоличествоУсуг;
            //            table.Cell(i, 5).Range.Text = item.СуммаУслуг;

            //            //doc.Words.Count.ToString();
            //            Object beforeRow1 = Type.Missing;
            //            table.Rows.Add(ref beforeRow1);

            //            i++;
            //        }
            //        table.Rows[i].Delete();

            //        //выведим документа
            //        app.Visible = true;

            //    }
            //    else
            //    {
            //        //нужно проверить есть ли такой акт в таблице если есть то update если нет то insert
            //        string queryCountAkt = "select count(НомерАкта) from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " order by НомерАкта distinct desc ";
            //        DataTable tabCount = ТаблицаБД.GetTable(queryCountAkt, sCon, "АктВыполнненныхРабот");

            //        DataRow rC = tabCount.Rows[0];
            //        int count = Convert.ToInt32(rC[0]);

            //        if (count == 0)
            //        {
            //            //узнаем количество строк
            //            int iCountRow = this.gvАкт.Rows.Count;

            //            //счётчик
            //            int iCount = 1;

            //            //акт не подписан
            //            foreach (DataGridViewRow r in this.gvАкт.Rows)
            //            {
            //                if (iCount <= iCountRow - 1)
            //                {
            //                    //string номерАкта = this.договор.НомерДоговора + "/" + this.lblNextNumber.Text;
            //                    string номерАкта = this.lblNextNumber.Text;


            //                    InsertАкт akt = new InsertАкт(номерАкта, this.договор.id_договор, false, r.Cells["НомерПоПеречню"].Value.ToString(), r.Cells["НаименованиеУслуги"].Value.ToString(), Convert.ToDecimal(r.Cells["Цена"].Value), Convert.ToInt32(r.Cells["Количество"].Value));

            //                    Receiver recivaer = new Receiver();
            //                    recivaer.Action(akt);
            //                }

            //                iCount++;
            //            }
            //        }
            //        //если запись относящаяся к текущему договору существует
            //        if (count != 0)
            //        {
            //            //получим текущий номер акта
            //            string queryCountAkt1 = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            //            DataTable tabCount1 = ТаблицаБД.GetTable(queryCountAkt, sCon, "АктВыполнненныхРабот");


            //            //Удалим строки которые относятся к текущему договору
            //            NullАктВыполненныхРабот акт = new NullАктВыполненныхРабот(this.договор.id_договор);

            //            Receiver recivaer = new Receiver();
            //            recivaer.Action(акт);


            //            //узнаем количество строк
            //            int iCountRow = this.gvАкт.Rows.Count;

            //            //счётчик
            //            int iCount = 1;

            //            //акт подписан значит вносим изменение в БД
            //            //так как у нас 1 договор 1 акт то ориентируемся на id-договора
            //            foreach (DataGridViewRow r in this.gvАкт.Rows)
            //            {
            //                if (iCount <= iCountRow - 1)
            //                {
            //                    //string номерАкта = this.договор.НомерДоговора + "/" + this.lblNextNumber.Text;
            //                    string номерАкта = this.lblNextNumber.Text;
            //                    InsertАкт akt = new InsertАкт(номерАкта, this.договор.id_договор, false, r.Cells["НомерПоПеречню"].Value.ToString(), r.Cells["НаименованиеУслуги"].Value.ToString(), Convert.ToDecimal(r.Cells["Цена"].Value), Convert.ToInt32(r.Cells["Количество"].Value));

            //                    //Receiver recivaer = new Receiver();
            //                    recivaer.Action(akt);
            //                }

            //                iCount++;
            //            }

            //        }
            //    }
            //}
            //else
            //{
            //    //форма работает в режиме доп соглашения
            //    //если не стоит флаг 
            //    if (this.checkBox1.Checked == false)
            //    {
            //        //Удалим из таблицы Акт выполненных работ записи и внесём их по новой из формы

            //        //получим текущий номер акта
            //        string queryCountAkt1 = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            //        DataTable tabCount1 = ТаблицаБД.GetTable(queryCountAkt1, ConnectionDB.ConnectionString(), "АктВыполнненныхРабот");

            //        //Удалим строки которые относятся к текущему договору
            //        NullАктВыполненныхРабот акт = new NullАктВыполненныхРабот(this.договор.id_договор);

            //        Receiver recivaer = new Receiver();
            //        recivaer.Action(акт);

            //        //узнаем количество строк
            //        int iCountRow = this.gvАкт.Rows.Count;

            //        //счётчик
            //        int iCount = 1;

            //        //акт подписан значит вносим изменение в БД
            //        //так как у нас 1 договор 1 акт то ориентируемся на id-договора
            //        foreach (DataGridViewRow r in this.gvАкт.Rows)
            //        {
            //            if (iCount <= iCountRow - 1)
            //            {
            //                //string номерАкта = this.договор.НомерДоговора + "/" + this.lblNextNumber.Text;
            //                string номерАкта = this.lblNextNumber.Text;
            //                InsertАктДопСоглашения akt = new InsertАктДопСоглашения(номерАкта, this.договор.id_договор, false, r.Cells["НомерПоПеречню"].Value.ToString(), r.Cells["НаименованиеУслуги"].Value.ToString(), Convert.ToDecimal(r.Cells["Цена"].Value), Convert.ToInt32(r.Cells["Количество"].Value), "True");

            //                //Receiver recivaer = new Receiver();
            //                recivaer.Action(akt);
            //            }

            //            iCount++;
            //        }
            //    }
            //    else
            //    {
            //        //Удалим из таблицы Акт выполненных работ записи и внесём их по новой из формы

            //        //получим текущий номер акта
            //        string queryCountAkt1 = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            //        DataTable tabCount1 = ТаблицаБД.GetTable(queryCountAkt1, ConnectionDB.ConnectionString(), "АктВыполнненныхРабот");

            //        //Удалим строки которые относятся к текущему договору
            //        NullАктВыполненныхРабот акт = new NullАктВыполненныхРабот(this.договор.id_договор);

            //        Receiver recivaer = new Receiver();
            //        recivaer.Action(акт);

            //        //узнаем количество строк
            //        int iCountRow = this.gvАкт.Rows.Count;

            //        //счётчик
            //        int iCount = 1;

            //        //акт подписан значит вносим изменение в БД
            //        //так как у нас 1 договор 1 акт то ориентируемся на id-договора
            //        foreach (DataGridViewRow r in this.gvАкт.Rows)
            //        {
            //            if (iCount <= iCountRow - 1)
            //            {
            //                //string номерАкта = this.договор.НомерДоговора + "/" + this.lblNextNumber.Text;
            //                string номерАкта = this.lblNextNumber.Text;
            //                //InsertАктДопСоглашения akt = new InsertАктДопСоглашения(номерАкта, this.договор.id_договор, false, r.Cells["НомерПоПеречню"].Value.ToString(), r.Cells["НаименованиеУслуги"].Value.ToString(), Convert.ToDecimal(r.Cells["Цена"].Value), Convert.ToInt32(r.Cells["Количество"].Value), "True");
            //                InsertАктДопСоглашениеЗакрытый akt = new InsertАктДопСоглашениеЗакрытый(номерАкта, this.договор.id_договор, true, r.Cells["НомерПоПеречню"].Value.ToString(), r.Cells["НаименованиеУслуги"].Value.ToString(), Convert.ToDecimal(r.Cells["Цена"].Value), Convert.ToInt32(r.Cells["Количество"].Value), "True",this.dateTimePicker1.Value.ToShortDateString());

            //                //Receiver recivaer = new Receiver();
            //                recivaer.Action(akt);
            //            }

            //            iCount++;
            //        }

            //        //выведим WORD
            //        //Создадим список
            //        List<АктНазваниеУслуги> list = new List<АктНазваниеУслуги>();

            //        //заполним шапку
            //        АктНазваниеУслуги шапка = new АктНазваниеУслуги();
            //        шапка.Номер = "№ п.п";
            //        шапка.НомерУслуги = "№ услуги в справочнике";
            //        шапка.НазваниеУслуги = "Наименование услуги";
            //        шапка.КоличествоУсуг = "Количество";
            //        шапка.СуммаУслуг = "Сумма, руб.";

            //        list.Add(шапка);

            //        string queryRowsAct = "select * from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            //        DataTable tabAct = ТаблицаБД.GetTable(queryRowsAct, ConnectionDB.ConnectionString(), "АктВыполнненныхРабот");

            //        decimal сумма = 0.0m;

            //        //счётчик
            //        int iCount1 = 1;

            //        foreach (DataRow row in tabAct.Rows)
            //        {
            //            АктНазваниеУслуги акт1 = new АктНазваниеУслуги();
            //            акт1.Номер = iCount1.ToString();
            //            акт1.НомерУслуги = row["НомерПоПеречню"].ToString().Replace(',', '.');
            //            акт1.НазваниеУслуги = row["НаименованиеУслуги"].ToString();

            //            //акт.Номер = r.Cells["НомерПоПеречню"].Value.ToString();
            //            акт1.КоличествоУсуг = row["Количество"].ToString();
            //            акт1.СуммаУслуг = row["Сумма"].ToString();

            //            сумма = Math.Round(сумма, 2) + Math.Round(Convert.ToDecimal(row["Сумма"]), 2);

            //            list.Add(акт1);
            //            //датаАкта = Convert.ToDateTime(row["ДатаПодписания"]).ToShortDateString();
            //            датаАкта = this.dateTimePicker1.Value.ToShortDateString();

            //            iCount1++;
            //        }

            //        АктНазваниеУслуги подвал = new АктНазваниеУслуги();
            //        подвал.НомерУслуги = "Итого";
            //        подвал.СуммаУслуг = сумма.ToString("c");

            //        list.Add(подвал);

            //        //соберём данные для отчёта
            //        string номерДоговора = this.договор.НомерДоговора;

            //        //передадим номер акта
            //        string номерАкта1 = this.lblNextNumber.Text;

            //        string исполнитель = this.НазваниеПоликлинники;
            //        string потребитель = this.ФиоЛьготника;


            //        string fName = this.ФиоЛьготника;

            //        //Скопируем шаблон в папку Документы
            //        FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Акт4.doc");
            //        fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);

            //        string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";

            //        //Создаём новый Word.Application
            //        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();



            //        //Загружаем документ
            //        Microsoft.Office.Interop.Word.Document doc = null;

            //        object fileName = filName;
            //        object falseValue = false;
            //        object trueValue = true;
            //        object missing = Type.Missing;

            //        doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            //        ref missing, ref missing, ref missing, ref missing, ref missing,
            //        ref missing, ref missing, ref missing, ref missing, ref missing,
            //        ref missing, ref missing, ref missing);


            //        ////Номер договора
            //        object wdrepl = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt = "номердоговора";
            //        object newtxt = (object)номерДоговора;
            //        //object frwd = true;
            //        object frwd = false;
            //        doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl2 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt2 = "номеракта";
            //        object newtxt2 = (object)номерАкта1;
            //        //object frwd = true;
            //        object frwd2 = false;
            //        doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl3 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt3 = "Больница";
            //        object newtxt3 = (object)исполнитель;
            //        //object frwd = true;
            //        object frwd3 = false;
            //        doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl4 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt4 = "Льготник";
            //        object newtxt4 = (object)потребитель;
            //        //object frwd = true;
            //        object frwd4 = false;
            //        doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            //        ref missing, ref missing);

            //        //выведим дату договора
            //        string датаДоговора = this.договор.ДатаДоговора.ToShortDateString();

            //        ////Номер договора
            //        object wdrepl5 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt5 = "датадоговора";
            //        object newtxt5 = (object)датаДоговора;
            //        //object frwd = true;
            //        object frwd5 = false;
            //        doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
            //        ref missing, ref missing);

            //        ////Номер договора
            //        object wdrepl6 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt6 = "датаакта";
            //        object newtxt6 = (object)датаАкта;
            //        //object frwd = true;
            //        object frwd6 = false;
            //        doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
            //        ref missing, ref missing);

            //        //ФИО глав врача Главврач
            //        object wdrepl7 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt7 = "Главврач";
            //        object newtxt7 = (object)this.ФиоГлаВрач;
            //        //object frwd = true;
            //        object frwd7 = false;
            //        doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
            //        ref missing, ref missing);

            //        string summa = Валюта.Рубли.Пропись(сумма);

            //        //Выведим сумму
            //        object wdrepl8 = WdReplace.wdReplaceAll;
            //        //object searchtxt = "GreetingLine";
            //        object searchtxt8 = "сумпрописью";
            //        object newtxt8 = (object)summa;
            //        //object frwd = true;
            //        object frwd8 = false;
            //        doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
            //        ref missing, ref missing);



            //        //Вставить таблицу
            //        object bookNaziv = "таблица";
            //        Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            //        object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            //        object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            //        Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 5, ref behavior, ref autobehavior);
            //        table.Range.ParagraphFormat.SpaceAfter = 6;
            //        table.Borders.Enable = 1; // Рамка - сплошная линия
            //        table.Range.Font.Name = "Times New Roman";
            //        table.Range.Font.Size = 10;

            //        //установим ширину столбцов
            //        table.Columns[1].Width = 40;
            //        table.Columns[2].Width = 80;
            //        table.Columns[2].AutoFit();
            //        table.Columns[3].Width = 180;
            //        table.Columns[4].Width = 60;
            //        table.Columns[5].Width = 100;

            //        table.Borders.Enable = 1; // Рамка - сплошная линия
            //        table.Range.Font.Name = "Times New Roman";
            //        table.Range.Font.Size = 10;
            //        //table.Range.HorizontalInVertical = WdHorizontalLineAlignment.wdHorizontalLineAlignCenter.;
            //        //счётчик строк
            //        int i = 1;

            //        //запишем данные в таблицу
            //        foreach (АктНазваниеУслуги item in list)
            //        {
            //            table.Cell(i, 1).Range.Text = item.Номер;

            //            table.Cell(i, 2).Range.Text = item.НомерУслуги;

            //            table.Cell(i, 3).Range.Text = item.НазваниеУслуги;
            //            table.Cell(i, 4).Range.Text = item.КоличествоУсуг;
            //            table.Cell(i, 5).Range.Text = item.СуммаУслуг;

            //            //doc.Words.Count.ToString();
            //            Object beforeRow1 = Type.Missing;
            //            table.Rows.Add(ref beforeRow1);

            //            i++;
            //        }
            //        table.Rows[i].Delete();

            //        //выведим документа
            //        app.Visible = true;


            //    }




            //}



            ////закроем форму
            //this.Close();
        }

        private void FormАкт_Load(object sender, EventArgs e)
        {
            if (this.FlagДопСоглашений == false)
            {
                //получим строку подключения
                string sCon = ConnectionDB.ConnectionString();

                //получим последную строку
                //string query = "select НомерАкта from АктВыполнненныхРабот order by НомерАкта desc";// where id_договор = " + this.CurrentДоговор.id_договор + " ";
                string query = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + this.CurrentДоговор.id_договор + " ";
                DataTable tabArt = ТаблицаБД.GetTable(query, sCon, "АктВыполнненныхРабот");

                if (tabArt.Rows.Count == 0)
                {

                    //узнаем последний номер акта
                    //string queryNumAct = "select НомерАкта from АктВыполнненныхРабот order by НомерАкта desc";
                    string queryNumAct = "select НомерАкта from АктВыполнненныхРабот where id_акт in (select max(id_акт) from АктВыполнненныхРабот)";
                    DataTable tabArt1 = ТаблицаБД.GetTable(queryNumAct, sCon, "АктВыполнненныхРабот");

                    string nextNum = string.Empty;
                    if (tabArt1.Rows.Count != 0)
                    {
                        string numAct = tabArt1.Rows[0][0].ToString();

                        //получим номер акта
                        string[] sArr = numAct.Split('/');
                        nextNum = sArr[1];
                    }
                    else
                    {
                        nextNum = "0";
                    }

                    string номер = Convert.ToString(Convert.ToInt32(nextNum) + 1);
                    //отобразим полученный номе договора
                    this.lblNextNumber.Text = this.НомерПоликлинники + "/" + номер;

                    //отобразим выбранные в договоре услуги
                    string queryАкт = "select * from УслугиПоДоговору where id_договор = " + this.договор.id_договор + " ";
                    DataTable tableAct = ТаблицаБД.GetTable(queryАкт, sCon, "УслугиПоДоговору");

                    this.gvАкт.DataSource = tableAct;
                    this.gvАкт.Columns["id_услугиДоговор"].Visible = false;
                    this.gvАкт.Columns["id_договор"].Visible = false;

                    //установим порядок столбцов
                    this.gvАкт.Columns["НомерПоПеречню"].DisplayIndex = 0;
                    this.gvАкт.Columns["НомерПоПеречню"].ReadOnly = true;

                    this.gvАкт.Columns["НаименованиеУслуги"].DisplayIndex = 1;
                    this.gvАкт.Columns["НаименованиеУслуги"].ReadOnly = true;

                    this.gvАкт.Columns["Цена"].DisplayIndex = 2;
                    this.gvАкт.Columns["Цена"].ReadOnly = true;

                    this.gvАкт.Columns["Количество"].DisplayIndex = 3;
                }
                else
                {
                    //string numDoc = tabArt.Rows[0][0].ToString();
                    //получим последную строку
                    //string query1 = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + this.CurrentДоговор.id_договор + " ";
                    string query1 = "select top 1 НомерАкта from АктВыполнненныхРабот where id_договор = " + this.CurrentДоговор.id_договор + " ";
                    DataTable tabArt1 = ТаблицаБД.GetTable(query1, sCon, "АктВыполнненныхРабот");

                    string numDoc = string.Empty;
                    string[] sArr;
                    string nextNum = string.Empty;
                    string номер = string.Empty;

                    //===========================получим номер акта=================
                    //количество строк
                    int countR = tabArt1.Rows.Count;
                    //если записей нет
                    if (countR == 0)
                    {
                        numDoc = tabArt.Rows[0][0].ToString();

                        //получим номер акта
                        sArr = numDoc.Split('/');
                        nextNum = sArr[1];

                        //то к 0 прибавим 1
                        номер = Convert.ToString(Convert.ToInt32(nextNum) + 1);
                        //string номер = nextNum.ToString();
                    }
                    else
                    {
                        numDoc = tabArt1.Rows[0][0].ToString();

                        //получим номер акта
                        sArr = numDoc.Split('/');
                        nextNum = sArr[1];

                        //string номер = Convert.ToString(Convert.ToInt32(nextNum) + 1);
                        номер = nextNum.ToString();
                    }



                    this.lblNextNumber.Text = this.НомерПоликлинники + "/" + номер;

                    string queryАкт = "select * from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
                    DataTable tableAct = ТаблицаБД.GetTable(queryАкт, sCon, "УслугиПоДоговору");

                    //отобразим выбранные в договоре услуги
                    //string queryАкт = "select * from УслугиПоДоговору where id_договор = " + this.договор.id_договор + " ";
                    //DataTable tableAct = ТаблицаБД.GetTable(queryАкт, sCon, "УслугиПоДоговору");

                    this.gvАкт.DataSource = tableAct;
                    //this.gvАкт.Columns["id_услугиДоговор"].Visible = false;
                    this.gvАкт.Columns["id_договор"].Visible = false;

                    //установим порядок столбцов
                    this.gvАкт.Columns["НомерПоПеречню"].DisplayIndex = 0;
                    this.gvАкт.Columns["НомерПоПеречню"].ReadOnly = true;

                    this.gvАкт.Columns["НаименованиеУслуги"].DisplayIndex = 1;
                    this.gvАкт.Columns["НаименованиеУслуги"].ReadOnly = true;

                    //установим размер
                    int ширина = this.gvАкт.Width;
                    this.gvАкт.Columns["НаименованиеУслуги"].Width = Convert.ToInt32(ширина / 1.4m);

                    this.gvАкт.Columns["Цена"].DisplayIndex = 2;
                    this.gvАкт.Columns["Цена"].ReadOnly = true;

                    this.gvАкт.Columns["Количество"].DisplayIndex = 3;
                    this.gvАкт.Columns["Количество"].ReadOnly = false;

                    //скроем не нужные записи
                    this.gvАкт.Columns["id_акт"].Visible = false;
                    this.gvАкт.Columns["НомерАкта"].Visible = false;

                    this.gvАкт.Columns["ФлагПодписания"].Visible = false;
                    this.gvАкт.Columns["ДатаПодписания"].Visible = false;

                    this.gvАкт.Columns["ФлагДопСоглашение"].Visible = false;
                    this.gvАкт.Columns["Сумма"].Visible = false;

                    //проверим подписан ли акт
                    if (tableAct.Rows.Count != 0)
                    {
                        string flag = tableAct.Rows[0]["ФлагПодписания"].ToString();
                        if (flag == "True")
                        {

                            this.button2.Enabled = false;
                            this.checkBox1.Checked = true;

                            this.dateTimePicker1.Visible = true;
                            this.gvАкт.Columns["Количество"].ReadOnly = true;

                            this.btnPrint.Enabled = true;
                        }
                        else
                        {
                            //this.btnPrint.Enabled = false;
                        }
                    }

                }
            }

            if (this.FlagДопСоглашений == true)
            {
                ////отобразим в форме данные которые записаны для текущего договора в таблице Услуги по договору
                //this.lblNextNumber.Text = this.НомерПоликлинники + "/" + номер;

                ////отобразим выбранные в договоре услуги
                //string queryАкт = "select * from УслугиПоДоговору where id_договор = " + this.договор.id_договор + " ";
                //DataTable tableAct = ТаблицаБД.GetTable(queryАкт, sCon, "УслугиПоДоговору");


                //получим последную строку
                string query1 = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + this.CurrentДоговор.id_договор + " ";
                DataTable tabArt1 = ТаблицаБД.GetTable(query1, ConnectionDB.ConnectionString(), "АктВыполнненныхРабот");

                string numDoc = string.Empty;
                string[] sArr;
                string nextNum = string.Empty;
                string номер = string.Empty;

                //===========================получим номер акта=================
                //количество строк
                int countR = tabArt1.Rows.Count;
                //если записей нет
                if (countR == 0)
                {
                    numDoc = tabArt1.Rows[0][0].ToString();

                    //получим номер акта
                    sArr = numDoc.Split('/');
                    nextNum = sArr[1];

                    //то к 0 прибавим 1
                    номер = Convert.ToString(Convert.ToInt32(nextNum) + 1);
                    //string номер = nextNum.ToString();
                }
                else
                {
                    numDoc = tabArt1.Rows[0][0].ToString();

                    //получим номер акта
                    sArr = numDoc.Split('/');
                    nextNum = sArr[1];

                    //string номер = Convert.ToString(Convert.ToInt32(nextNum) + 1);
                    номер = nextNum.ToString();
                }



                this.lblNextNumber.Text = this.НомерПоликлинники + "/" + номер;

                //отобразим выбранные в договоре услуги
                string queryАкт = "select * from УслугиПоДоговору where id_договор = " + this.договор.id_договор + " ";
                DataTable tableAct = ТаблицаБД.GetTable(queryАкт, ConnectionDB.ConnectionString(), "УслугиПоДоговору");

                this.gvАкт.DataSource = tableAct;
                this.gvАкт.Columns["id_услугиДоговор"].Visible = false;
                this.gvАкт.Columns["id_договор"].Visible = false;

                //установим порядок столбцов
                this.gvАкт.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.gvАкт.Columns["НомерПоПеречню"].ReadOnly = true;

                this.gvАкт.Columns["НаименованиеУслуги"].DisplayIndex = 1;
                this.gvАкт.Columns["НаименованиеУслуги"].ReadOnly = true;

                this.gvАкт.Columns["Цена"].DisplayIndex = 2;
                this.gvАкт.Columns["Цена"].ReadOnly = true;

                this.gvАкт.Columns["Количество"].DisplayIndex = 3;


                //string queryАкт = "select * from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
                //DataTable tableAct = ТаблицаБД.GetTable(queryАкт, sCon, "УслугиПоДоговору");

            }

            //Развернём на максимальное значение форму
            this.WindowState = FormWindowState.Maximized;


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.label2.Visible = true;
                this.dateTimePicker1.Visible = true;
                this.button2.Text = "Сохранить и распечатать";
                //this.button2.Text = "Сохранить";
                //this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom )));
                this.button2.Location = new Point(this.Width-300, this.Height - 74);
                //this.button2.Location = new Point(310,264);
                //this.button2.Location = new Point(365, 264); 
                this.button2.Width = 160;
                //this.button2.Width = 88;

            }
            else
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.label2.Visible = false;
                    this.dateTimePicker1.Visible = false;
                    this.button2.Text = "Сохранить";
                    //this.button2.Location = new Point(377, 264); 
                    this.button2.Location = new Point(this.Width-195, this.Height - 74);
                    this.button2.Width = 88;
                }
                else
                {
                    this.label2.Visible = false;
                    this.dateTimePicker1.Visible = false;
                    this.button2.Text = "Сохранить";
                    //this.button2.Location = new Point(377, 264);
                    //this.button2.Location = new Point(this.button1.Location.X + 100, 264);
                    this.button2.Location = new Point(this.Width - 190, this.Height - 74);
                    this.button2.Width = 88;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sCon = ConnectionDB.ConnectionString();
            //нужно проверить есть ли такой акт в таблице если есть то update если нет то insert
            string queryCountAkt = "select count(НомерАкта) from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            DataTable tabCount = ТаблицаБД.GetTable(queryCountAkt, sCon, "АктВыполнненныхРабот");

            DataRow rC = tabCount.Rows[0];
            int count = Convert.ToInt32(rC[0]);

            if (count != 0)
            {
                Receiver recivaer = new Receiver();

                //узнаем количество строк
                int iCountRow = this.gvАкт.Rows.Count;

                //счётчик
                int iCount = 1;

                //акт подписан значит вносим изменение в БД
                //так как у нас 1 договор 1 акт то ориентируемся на id-договора
                foreach (DataGridViewRow r in this.gvАкт.Rows)
                {
                    if (iCount <= iCountRow - 1)
                    {
                        UpdateАкт akt = new UpdateАкт(this.договор.id_договор, this.dateTimePicker1.Value);
                        recivaer.Action(akt);
                    }

                    iCount++;
                }
            }

            int iCountRow1 = this.gvАкт.Rows.Count;

            //счётчик
            //int iCount1 = 1;

            //Создадим список
            List<АктНазваниеУслуги> list = new List<АктНазваниеУслуги>();

            //заполним шапку
            АктНазваниеУслуги шапка = new АктНазваниеУслуги();
            шапка.Номер = "№ п.п";
            шапка.НомерУслуги = "№ услуги в справочнике";
            шапка.НазваниеУслуги = "Наименование услуги";
            шапка.КоличествоУсуг = "Количество";
            шапка.СуммаУслуг = "Сумма, руб.";

            list.Add(шапка);

            string queryRowsAct = "select * from АктВыполнненныхРабот where id_договор = " + this.договор.id_договор + " ";
            DataTable tabAct = ТаблицаБД.GetTable(queryRowsAct, sCon, "АктВыполнненныхРабот");

            decimal сумма = 0.0m;

            //счётчик
            int iCount2 = 1;
            foreach (DataRow row in tabAct.Rows)
            {
                АктНазваниеУслуги акт = new АктНазваниеУслуги();
                акт.Номер = iCount2.ToString();
                акт.НомерУслуги = row["НомерПоПеречню"].ToString().Replace(',', '.'); ;
                акт.НазваниеУслуги = row["НаименованиеУслуги"].ToString();

                //акт.Номер = r.Cells["НомерПоПеречню"].Value.ToString();
                акт.КоличествоУсуг = row["Количество"].ToString();
                акт.СуммаУслуг = row["Сумма"].ToString();

                сумма = Math.Round(сумма, 2) + Math.Round(Convert.ToDecimal(row["Сумма"]), 2);

                list.Add(акт);
                датаАкта = Convert.ToDateTime(row["ДатаПодписания"]).ToShortDateString();
                iCount2++;
            }

            //создадим и сгенирируеем строчку итого
            АктНазваниеУслуги подвал = new АктНазваниеУслуги();
            подвал.НомерУслуги = "Итого";
            подвал.СуммаУслуг = сумма.ToString("c");
            list.Add(подвал);

            //соберём данные для отчёта
            string номерДоговора = this.договор.НомерДоговора;

            //передадим номер акта
            string номерАкта = this.lblNextNumber.Text;

            string исполнитель = this.НазваниеПоликлинники;
            string потребитель = this.ФиоЛьготника;

            //string queryDate = "select * from АктВыполненныхРабот where ";

            string fName = this.ФиоЛьготника;

            //распечатаем word
            //FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\Акт4.dot");
           // FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\"+fileName+".doc");
            //fnDel.Delete();

            //Скопируем шаблон в папку Документы
            FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Акт4.doc");
            fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);

            string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";

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

            ////Номер договора
            object wdrepl = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt = "номердоговора";
            object newtxt = (object)номерДоговора;
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);

            ////Номер договора
            object wdrepl2 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt2 = "номеракта";
            object newtxt2 = (object)номерАкта;
            //object frwd = true;
            object frwd2 = false;
            doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            ref missing, ref missing);

            ////Номер договора
            object wdrepl3 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt3 = "Больница";
            object newtxt3 = (object)исполнитель;
            //object frwd = true;
            object frwd3 = false;
            doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            ref missing, ref missing);

            ////Номер договора
            object wdrepl4 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt4 = "Льготник";
            object newtxt4 = (object)потребитель;
            //object frwd = true;
            object frwd4 = false;
            doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            ref missing, ref missing);

            //выведим дату договора
            string датаДоговора = this.договор.ДатаДоговора.ToShortDateString();

            ////Номер договора
            object wdrepl5 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt5 = "датадоговора";
            object newtxt5 = (object)датаДоговора;
            //object frwd = true;
            object frwd5 = false;
            doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
            ref missing, ref missing);

            ////Номер договора
            object wdrepl6 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt6 = "датаакта";
            object newtxt6 = (object)датаАкта;
            //object frwd = true;
            object frwd6 = false;
            doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
            ref missing, ref missing);

            //ФИО глав врача Главврач
            object wdrepl7 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt7 = "Главврач";
            object newtxt7 = (object)this.ФиоГлаВрач;
            //object frwd = true;
            object frwd7 = false;
            doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
            ref missing, ref missing);

            string summa = Валюта.Рубли.Пропись(сумма);

            //Выведим сумму
            object wdrepl8 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt8 = "сумпрописью";
            object newtxt8 = (object)summa;
            //object frwd = true;
            object frwd8 = false;
            doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
            ref missing, ref missing);




            //Вставить таблицу
            object bookNaziv = "таблица";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 5, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 40;
            table.Columns[2].Width = 80;
            table.Columns[2].AutoFit();
            table.Columns[3].Width = 180;
            table.Columns[4].Width = 60;
            table.Columns[5].Width = 100;
            table.Borders.Enable = 1; // Рамка - сплошная линия
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 10;
            //счётчик строк
            int i = 1;
            
            //запишем данные в таблицу
            foreach (АктНазваниеУслуги item in list)
            {
                table.Cell(i, 1).Range.Text = item.Номер;

                table.Cell(i, 2).Range.Text = item.НомерУслуги;

                table.Cell(i, 3).Range.Text = item.НазваниеУслуги;
                table.Cell(i, 4).Range.Text = item.КоличествоУсуг;
                table.Cell(i, 5).Range.Text = item.СуммаУслуг;

                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                i++;
            }
            table.Rows[i].Delete();

            //


            app.Visible = true;


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //string stest = Валюта.Рубли.Пропись(4578.44m);
            
        }
    }
}