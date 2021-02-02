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
using System.Configuration;
using System.Text.RegularExpressions;

namespace Стамотология
{
    public partial class FormPrintТехЛист : Form
    {
        private List<ТаблицаДоговор> listDate = new List<ТаблицаДоговор>();
        private List<ТаблицаДоговор> listPrint = new List<ТаблицаДоговор>();
        private string _ФИО;

        /// <summary>
        /// Свойство хранит данные для отображения услуг по текущему договору
        /// </summary>
        public List<ТаблицаДоговор> УслугиДоговора
        {
            get
            {
                return listDate;
            }
            set
            {
                listDate = value;
            }
        }

        /// <summary>
        /// Хранит ФИО Льготника 
        /// </summary>
        public string ФИО_Льготника
        {
            get
            {
                return _ФИО;
            }
            set
            {
                _ФИО = value;
            }
        }

        private int _id_льготник;

        /// <summary>
        /// Хранит ID льготника
        /// </summary>
        public int Id_Льготник
        {
            get
            {
                return _id_льготник;
            }
            set
            {
                _id_льготник = value;
            }
        }



        public FormPrintТехЛист()
        {
            InitializeComponent();
        }

        private void FormPrintТехЛист_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.УслугиДоговора;
            this.dataGridView1.Columns["ПорядковыНомер"].Visible = false;
            this.dataGridView1.Columns["ПорядковыНомер"].ReadOnly = true;

            this.dataGridView1.Columns["Наименование"].Visible = true;
            this.dataGridView1.Columns["Наименование"].DisplayIndex = 0;
            this.dataGridView1.Columns["Наименование"].ReadOnly = true;

            this.dataGridView1.Columns["НомерУслуги"].Visible = true;
            this.dataGridView1.Columns["НомерУслуги"].DisplayIndex = 1;
            this.dataGridView1.Columns["НомерУслуги"].HeaderText = "Номер услуги";
            this.dataGridView1.Columns["НомерУслуги"].ReadOnly = true;

            this.dataGridView1.Columns["Цена"].Visible = true;
            this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
            this.dataGridView1.Columns["Цена"].ReadOnly = true;

            this.dataGridView1.Columns["Количество"].Visible = true;
            this.dataGridView1.Columns["Количество"].DisplayIndex = 3;
            this.dataGridView1.Columns["Количество"].ReadOnly = true;

            this.dataGridView1.Columns["Стоимость"].Visible = true;
            this.dataGridView1.Columns["Стоимость"].DisplayIndex = 4;
            this.dataGridView1.Columns["Стоимость"].ReadOnly = true;

            this.dataGridView1.Columns["FlagPrint"].Visible = true;
            this.dataGridView1.Columns["FlagPrint"].DisplayIndex = 5;
            this.dataGridView1.Columns["FlagPrint"].HeaderText = "Печатать в тех листе";
            this.dataGridView1.Columns["FlagPrint"].ReadOnly = false;

            this.dataGridView1.Columns["FlagSelect"].Visible = false;




        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //опросим dataGridView и узнаем что выбрали пользователи

            List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();

            //заполгним первую строку
            ТаблицаДоговор шапка = new ТаблицаДоговор();
            //шапка.ПорядковыНомер = "№ п/п";
            шапка.НомерУслуги = "№ усл в справ";
            шапка.Наименование = "Наименование";
            //шапка.Цена = "Цена, руб";
            шапка.Количество = "Кол";
            шапка.Цена = "Стоимость";
            list.Add(шапка);

            //подсчитаем сумму выполненных работ в тех листе
            decimal суммаРабот = 0m;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["FlagPrint"].Value) == true && Convert.ToBoolean(row.Cells["FlagSelect"].Value) != true)
                {
                    //string iTest = row.Cells["Наименование"].Value.ToString().Trim();

                    ТаблицаДоговор item = new ТаблицаДоговор();
                    item.НомерУслуги = row.Cells["НомерУслуги"].Value.ToString();

                    item.Наименование = row.Cells["Наименование"].Value.ToString();
                    item.Количество = row.Cells["Количество"].Value.ToString();

                    item.Цена = row.Cells["Стоимость"].Value.ToString();

                    string стоим = row.Cells["Стоимость"].Value.ToString().Replace('р',' ').Replace('Р',' ').Trim();
                    string стоимостString = стоим.Replace('.', ' ').Trim();

                    //суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(row.Cells["Стоимость"].Value), 2);
                    суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(стоимостString), 2);

                    list.Add(item);
                   
                    //укажем что данная строка была отправлена на печать
                    row.Cells["FlagSelect"].Value = true;
                }
            }

            //сформируем строку ИТОГО
            ТаблицаДоговор sumТехЛист = new ТаблицаДоговор();
            sumТехЛист.Наименование = "Итого: ";
            sumТехЛист.Цена = суммаРабот.ToString("c");

            //добавим в список list нижную строку с суммой ИТОГО
            list.Add(sumТехЛист);

            List<ТаблицаДоговор> iApple = list;
            
            //string iTest = "Test";

            //Распечатаем технический лист
            string fName = "Технический лист " + this.ФИО_Льготника;


            try
            {
                //Скопируем шаблон в папку Документы
                FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ТехЛист.doc");
                fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
            }
            catch
            {
                MessageBox.Show("Возможно у вас уже открыт договор с этим льготником. Закройте этот договор.");
            }

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

            object wdrepl = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt = "Льготник";
            object newtxt = (object)this.ФИО_Льготника;
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);

            //узнаем адрес
            string queryАдрес = "select * from Льготник where id_льготник = " + this.Id_Льготник + " ";
            DataRow rowЛьготник = ТаблицаБД.GetTable(queryАдрес, ConnectionDB.ConnectionString(), "Льготник").Rows[0];

            //получим название района 
            string queryР = "select РайонОбласти from НаименованиеРайона where id_район in (select id_район from Льготник where id_льготник = " + this.Id_Льготник + " )";
            DataTable tab = ДанныеПредставление.GetПредставление(queryР, "Льготник");

            //переменная хранит название района
            string названиеРайона = string.Empty;

            if (tab.Rows.Count != 0)
            {
                //отобразим район который прописан в БД
                названиеРайона = tab.Rows[0][0].ToString();
            }

            //получим название населённого пункта
            string queryН = "select Наименование from НаселенныйПункт where id_насПункт in (select id_насПункт from Льготник where id_льготник = " + this.Id_Льготник + " )";
            DataTable tabН = ДанныеПредставление.GetПредставление(queryН, "Льготник");

            //переменная хранит название населённого пункта
            string населённыйПункт = string.Empty;

            if (tabН.Rows.Count != 0)
            {
                //отобразим населённый пункет в котором прописан текущий льготник
                населённыйПункт = tabН.Rows[0][0].ToString();
            }

            //если населённый пункт = Саратов 
            if (Regex.IsMatch(населённыйПункт, "Саратов") == true)
            {
                названиеРайона = "";
            }



            //улица
            string улица = rowЛьготник["улица"].ToString();
            string улицаPrint = string.Empty;

            if (улица != "")
            {
                улицаPrint = " ул " + улица;
            }

            //номер дома
            string numHous = rowЛьготник["НомерДома"].ToString();
            string numHousPrint = string.Empty;

            if (numHous != "")
            {
                numHousPrint = " д " + numHous;
            }


            //номер корпуса
            string numSubHous = rowЛьготник["корпус"].ToString();
            string numSubHousPrint = string.Empty;

            if (numSubHous != "")
            {
                numSubHousPrint = " корп. " + numSubHous;
            }

            //номер кв
            string numEpartment = rowЛьготник["НомерКвартиры"].ToString();
            string numEpartmentPrint = string.Empty;

            if (numEpartment != "")
            {
                numEpartmentPrint = " кв. " + numEpartment;
            }

            //полный адрес
            //string адрес = улица + " " + numHous + " "  + numSubHous + " " + numEpartment;
            string адрес = названиеРайона + " " + населённыйПункт + " " + улицаPrint + numHousPrint + numSubHousPrint + numEpartmentPrint;

            object wdrepl2 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt2 = "местопроживания";
            object newtxt2 = (object)адрес;
            //object frwd = true;
            object frwd2 = false;
            doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            ref missing, ref missing);

            //вычислим количество полных лет
            //получим дату рождения
            DateTime др = Convert.ToDateTime(rowЛьготник["ДатаРождения"]);

            //год рождения
            int yearR = др.Year;
            //int yearR = 1973;

            //месяц рождения 
            int montchR = др.Month;
            //int montchR = 09;

            //текущую дату
            DateTime data = DateTime.Today;

            // текущий год
            int yearT = data.Year;

            //текущий месяц
            int montchT = data.Month;

            int rez = (montchT > montchR) ? (yearT - yearR) : (yearT == yearR) ? 0 : (yearT - yearR - 1);
            string возрост = rez.ToString();

            object wdrepl3 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt3 = "скольколет";
            object newtxt3 = (object)возрост;
            //object frwd = true;
            object frwd3 = false;
            doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            ref missing, ref missing);

            // List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();

            // //заполгним первую строку
            // ТаблицаДоговор шапка = new ТаблицаДоговор();
            // //шапка.ПорядковыНомер = "№ п/п";
            // шапка.НомерУслуги = "№ усл в справ";
            // шапка.Наименование = "Наименование";
            // //шапка.Цена = "Цена, руб";
            // шапка.Количество = "Кол";
            // шапка.Цена  = "Стоимость";
            // list.Add(шапка);


            // //сформируем таблицу из услуг указанных в gvВидУслуг
            // //узнаем колько строк в гриде
            // int iCountR = this.gvВидУслуг.Rows.Count;

            // //откитнем последную пустую строку
            // int iCountRow = iCountR - 1;

            // int i = 0;

            // //подсчитаем сумму выполненных работ в тех листе
            // decimal суммаРабот = 0m;

            // foreach (DataGridViewRow row in this.gvВидУслуг.Rows)
            // {
            //     if (i != iCountRow)
            //     {
            //         ТаблицаДоговор item = new ТаблицаДоговор();
            //         item.НомерУслуги = row.Cells["НомерПоПеречню"].Value.ToString();

            //         item.Наименование = row.Cells["НаименованиеУслуги"].Value.ToString();
            //         item.Количество = row.Cells["'Кол-во'"].Value.ToString();

            //         item.Цена = row.Cells["Сумма"].Value.ToString();

            //         суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(row.Cells["Сумма"].Value), 2);

            //         list.Add(item);
            //     }

            //     i++;
            // }

            // ТаблицаДоговор sumТехЛист = new ТаблицаДоговор();
            // sumТехЛист.Наименование = "Итого: ";
            // sumТехЛист.Цена = суммаРабот.ToString("c");

            // //добавим в список list нижную строку с суммой ИТОГО
            // list.Add(sumТехЛист);

            //Вставить таблицу
            object bookNaziv = "таблица";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 3;

            table.Columns[1].Width = 150;// Было 50
            table.Columns[2].Width = 240;// было 340
            table.Columns[3].Width = 35;
            table.Columns[4].Width = 60;

            table.Borders.Enable = 1; // Рамка - сплошная линия
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 8;

            //счётчик строк
            int ic = 1;

            //запишем данные в таблицу
            foreach (ТаблицаДоговор item in list)
            {
                table.Cell(ic, 1).Range.Text = item.НомерУслуги;
                table.Cell(ic, 2).Range.Text = item.Наименование;

                table.Cell(ic, 3).Range.Text = item.Количество;
                table.Cell(ic, 4).Range.Text = item.Цена;

                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                ic++;
            }

            //удалим последную строку
            table.Rows[ic].Delete();

            //откроем получившейся документ
            app.Visible = true;
        }

        private void btnPrintBalanc_Click(object sender, EventArgs e)
        {
            //опросим dataGridView и узнаем что выбрали пользователи

            List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();

            //заполгним первую строку
            ТаблицаДоговор шапка = new ТаблицаДоговор();
            //шапка.ПорядковыНомер = "№ п/п";
            шапка.НомерУслуги = "№ усл в справ";
            шапка.Наименование = "Наименование";
            //шапка.Цена = "Цена, руб";
            шапка.Количество = "Кол";
            шапка.Цена = "Стоимость";
            list.Add(шапка);

            //подсчитаем сумму выполненных работ в тех листе
            decimal суммаРабот = 0m;

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["FlagSelect"].Value) != true)
                {
                    //string iTest = row.Cells["Наименование"].Value.ToString().Trim();

                    ТаблицаДоговор item = new ТаблицаДоговор();
                    item.НомерУслуги = row.Cells["НомерУслуги"].Value.ToString();

                    item.Наименование = row.Cells["Наименование"].Value.ToString();
                    item.Количество = row.Cells["Количество"].Value.ToString();

                    item.Цена = row.Cells["Стоимость"].Value.ToString();

                    string стоим = row.Cells["Стоимость"].Value.ToString().Replace('р', ' ').Trim();
                    string стоимостString = стоим.Replace('.', ' ').Trim();

                    //суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(row.Cells["Стоимость"].Value), 2);
                    суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(стоимостString), 2);

                    list.Add(item);

                    //укажем что данная строка была отправлена на печать
                    row.Cells["FlagSelect"].Value = true;
                    
                    //Установим флаг в true
                    row.Cells["FlagPrint"].Value = true;
                }
            }

            //сформируем строку ИТОГО
            ТаблицаДоговор sumТехЛист = new ТаблицаДоговор();
            sumТехЛист.Наименование = "Итого: ";
            sumТехЛист.Цена = суммаРабот.ToString("c");

            //добавим в список list нижную строку с суммой ИТОГО
            list.Add(sumТехЛист);

            List<ТаблицаДоговор> iApple = list;

            //string iTest = "Test";

            //Распечатаем технический лист
            string fName = "Технический лист " + this.ФИО_Льготника;


            try
            {
                //Скопируем шаблон в папку Документы
                FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ТехЛист.doc");
                fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
            }
            catch
            {
                MessageBox.Show("Возможно у вас уже открыт договор с этим льготником. Закройте этот договор.");
            }

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

            object wdrepl = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt = "Льготник";
            object newtxt = (object)this.ФИО_Льготника;
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);

            //узнаем адрес
            string queryАдрес = "select * from Льготник where id_льготник = " + this.Id_Льготник + " ";
            DataRow rowЛьготник = ТаблицаБД.GetTable(queryАдрес, ConnectionDB.ConnectionString(), "Льготник").Rows[0];

            //получим название района 
            string queryР = "select РайонОбласти from НаименованиеРайона where id_район in (select id_район from Льготник where id_льготник = " + this.Id_Льготник + " )";
            DataTable tab = ДанныеПредставление.GetПредставление(queryР, "Льготник");

            //переменная хранит название района
            string названиеРайона = string.Empty;

            if (tab.Rows.Count != 0)
            {
                //отобразим район который прописан в БД
                названиеРайона = tab.Rows[0][0].ToString();
            }

            //получим название населённого пункта
            string queryН = "select Наименование from НаселенныйПункт where id_насПункт in (select id_насПункт from Льготник where id_льготник = " + this.Id_Льготник + " )";
            DataTable tabН = ДанныеПредставление.GetПредставление(queryН, "Льготник");

            //переменная хранит название населённого пункта
            string населённыйПункт = string.Empty;

            if (tabН.Rows.Count != 0)
            {
                //отобразим населённый пункет в котором прописан текущий льготник
                населённыйПункт = tabН.Rows[0][0].ToString();
            }

            //если населённый пункт = Саратов 
            if (Regex.IsMatch(населённыйПункт, "Саратов") == true)
            {
                названиеРайона = "";
            }



            //улица
            string улица = rowЛьготник["улица"].ToString();
            string улицаPrint = string.Empty;

            if (улица != "")
            {
                улицаPrint = " ул " + улица;
            }

            //номер дома
            string numHous = rowЛьготник["НомерДома"].ToString();
            string numHousPrint = string.Empty;

            if (numHous != "")
            {
                numHousPrint = " д " + numHous;
            }


            //номер корпуса
            string numSubHous = rowЛьготник["корпус"].ToString();
            string numSubHousPrint = string.Empty;

            if (numSubHous != "")
            {
                numSubHousPrint = " корп. " + numSubHous;
            }

            //номер кв
            string numEpartment = rowЛьготник["НомерКвартиры"].ToString();
            string numEpartmentPrint = string.Empty;

            if (numEpartment != "")
            {
                numEpartmentPrint = " кв. " + numEpartment;
            }

            //полный адрес
            //string адрес = улица + " " + numHous + " "  + numSubHous + " " + numEpartment;
            string адрес = названиеРайона + " " + населённыйПункт + " " + улицаPrint + numHousPrint + numSubHousPrint + numEpartmentPrint;

            object wdrepl2 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt2 = "местопроживания";
            object newtxt2 = (object)адрес;
            //object frwd = true;
            object frwd2 = false;
            doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            ref missing, ref missing);

            //вычислим количество полных лет
            //получим дату рождения
            DateTime др = Convert.ToDateTime(rowЛьготник["ДатаРождения"]);

            //год рождения
            int yearR = др.Year;
            //int yearR = 1973;

            //месяц рождения 
            int montchR = др.Month;
            //int montchR = 09;

            //текущую дату
            DateTime data = DateTime.Today;

            // текущий год
            int yearT = data.Year;

            //текущий месяц
            int montchT = data.Month;

            int rez = (montchT > montchR) ? (yearT - yearR) : (yearT == yearR) ? 0 : (yearT - yearR - 1);
            string возрост = rez.ToString();

            object wdrepl3 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt3 = "скольколет";
            object newtxt3 = (object)возрост;
            //object frwd = true;
            object frwd3 = false;
            doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            ref missing, ref missing);

            // List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();

            // //заполгним первую строку
            // ТаблицаДоговор шапка = new ТаблицаДоговор();
            // //шапка.ПорядковыНомер = "№ п/п";
            // шапка.НомерУслуги = "№ усл в справ";
            // шапка.Наименование = "Наименование";
            // //шапка.Цена = "Цена, руб";
            // шапка.Количество = "Кол";
            // шапка.Цена  = "Стоимость";
            // list.Add(шапка);


            // //сформируем таблицу из услуг указанных в gvВидУслуг
            // //узнаем колько строк в гриде
            // int iCountR = this.gvВидУслуг.Rows.Count;

            // //откитнем последную пустую строку
            // int iCountRow = iCountR - 1;

            // int i = 0;

            // //подсчитаем сумму выполненных работ в тех листе
            // decimal суммаРабот = 0m;

            // foreach (DataGridViewRow row in this.gvВидУслуг.Rows)
            // {
            //     if (i != iCountRow)
            //     {
            //         ТаблицаДоговор item = new ТаблицаДоговор();
            //         item.НомерУслуги = row.Cells["НомерПоПеречню"].Value.ToString();

            //         item.Наименование = row.Cells["НаименованиеУслуги"].Value.ToString();
            //         item.Количество = row.Cells["'Кол-во'"].Value.ToString();

            //         item.Цена = row.Cells["Сумма"].Value.ToString();

            //         суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(row.Cells["Сумма"].Value), 2);

            //         list.Add(item);
            //     }

            //     i++;
            // }

            // ТаблицаДоговор sumТехЛист = new ТаблицаДоговор();
            // sumТехЛист.Наименование = "Итого: ";
            // sumТехЛист.Цена = суммаРабот.ToString("c");

            // //добавим в список list нижную строку с суммой ИТОГО
            // list.Add(sumТехЛист);

            //Вставить таблицу
            object bookNaziv = "таблица";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 3;

            table.Columns[1].Width = 150;// Было 50
            table.Columns[2].Width = 240;// было 340
            table.Columns[3].Width = 35;
            table.Columns[4].Width = 60;

            table.Borders.Enable = 1; // Рамка - сплошная линия
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 8;

            //счётчик строк
            int ic = 1;

            //запишем данные в таблицу
            foreach (ТаблицаДоговор item in list)
            {
                table.Cell(ic, 1).Range.Text = item.НомерУслуги;
                table.Cell(ic, 2).Range.Text = item.Наименование;

                table.Cell(ic, 3).Range.Text = item.Количество;
                table.Cell(ic, 4).Range.Text = item.Цена;

                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                ic++;
            }

            //удалим последную строку
            table.Rows[ic].Delete();

            //откроем получившейся документ
            app.Visible = true;
        }
    }
}