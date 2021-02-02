using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.Configuration;

using System.IO;
using Microsoft.Office.Interop.Word;
using System.Runtime.Serialization.Formatters.Binary;

using DantistLibrary;

namespace Стамотология
{
    public partial class FormListReestr : Form
    {
        private List<Реестр> listR;
        private DateTime _dtStart;
        private DateTime _dtEnd;
        private string льготнаяКатегория;
        private bool _РеестрЗаключенныхДоговоров;
        private int index = -10; // для отсеивания дублей

        //Сформируем данные для отчёта
        List<Реестр> list = new List<Реестр>();

        //Переменная хранит сумму выделенных договоров
        private decimal sum = 0.0m;

        //Счётчик порядковых номеров
        private int numOrdinal = 0;

        private bool flagUnLoad = false;

        /// <summary>
        /// Хранит флаг что разрешена вфгрузка реестров заключённых договоров
        /// </summary>
        public bool РеестрЗаключенныхДоговоров
        {
            get
            {
                return _РеестрЗаключенныхДоговоров;
            }
            set
            {
                _РеестрЗаключенныхДоговоров = value;
            }
        }

        /// <summary>
        /// Начало отчётного периода
        /// </summary>
        public DateTime dtStart
        {
            get
            {
                return _dtStart;
            }
            set
            {
                _dtStart = value;
            }
        }

        /// <summary>
        /// Конец отчётного периода
        /// </summary>
        public DateTime dtEnd
        {
            get
            {
                return _dtEnd;
            }
            set
            {
                _dtEnd = value;
            }
        }

        /// <summary>
        /// Хранит список актов
        /// </summary>
        public List<Реестр> РеестрАктов
        {
            get
            {
                return listR;
            }
            set
            {
                listR = value;
            }
        }

        /// <summary>
        /// Хранит льготную категорию
        /// </summary>
        public string ЛьготнаяКатегория
        {
            get
            {
                return льготнаяКатегория;
            }
            set
            {
                льготнаяКатегория = value;
            }
        }

        public FormListReestr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormListReestr_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.РеестрАктов;

            this.dataGridView1.Columns["Id_акт"].HeaderText = "Id_акт";
            this.dataGridView1.Columns["Id_акт"].DisplayIndex = 0;
            this.dataGridView1.Columns["Id_акт"].Visible = false;

            this.dataGridView1.Columns["Категория"].HeaderText = "Категория";
            this.dataGridView1.Columns["Категория"].DisplayIndex = 1;
            this.dataGridView1.Columns["Категория"].Visible = false;

            this.dataGridView1.Columns["НомерПорядковый"].HeaderText = "№ п.п.";
            this.dataGridView1.Columns["НомерПорядковый"].DisplayIndex = 2;

            this.dataGridView1.Columns["ФИО"].HeaderText = "ФИО";
            this.dataGridView1.Columns["ФИО"].DisplayIndex = 3;

            this.dataGridView1.Columns["НомерДатаДоговора"].HeaderText = "№ и дата договора на предоставление услуг";
            this.dataGridView1.Columns["НомерДатаДоговора"].DisplayIndex = 4;

            this.dataGridView1.Columns["НомерДатаАкта"].HeaderText = "Номер и дата акта выполненных работ";
            this.dataGridView1.Columns["НомерДатаАкта"].DisplayIndex = 5;

            this.dataGridView1.Columns["СерияДатаВыдачиДокумента"].HeaderText = "Серия № и дата документа о праве на льготу";
            this.dataGridView1.Columns["СерияДатаВыдачиДокумента"].DisplayIndex = 6;

            this.dataGridView1.Columns["СтоимсотьУслуги"].HeaderText = "Стоимость услуг";
            this.dataGridView1.Columns["СтоимсотьУслуги"].DisplayIndex = 7;

            this.dataGridView1.Columns["ФлагАктРеестр"].HeaderText = "Выбрать";
            this.dataGridView1.Columns["ФлагАктРеестр"].DisplayIndex = 8;

            // Делаем запрет на редактирование таблицы кроме, флага выбора
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].ReadOnly = true;
            dataGridView1.Columns["ФлагАктРеестр"].ReadOnly = false;

            //формируем шапку таблицы отчёта в word 
            Реестр реестр = new Реестр();
            реестр.НомерПорядковый = "№ п.п";
            реестр.ФИО = "Ф.И.О.";
            реестр.НомерДатаДоговора = "№ и дата договора на предоставление услуг";
            реестр.НомерДатаАкта = "№ и дата акта выполненных работ";
            реестр.СерияДатаВыдачиДокумента = "Серия, № и дата документа о праве на льготу";
            реестр.СтоимсотьУслуги = "Стоимость услуги, руб";
            list.Add(реестр);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {           
            ////формируем шапку таблицы отчёта в word 
            //Реестр реестр = new Реестр();
            //реестр.НомерПорядковый = "№ п.п";

            //реестр.ФИО = "Ф.И.О.";
            //реестр.НомерДатаДоговора = "№ и дата договора на предоставление услуг";

            //реестр.НомерДатаАкта = "№ и дата акта выполненных работ";
            //реестр.СерияДатаВыдачиДокумента = "Серия, № и дата документа о праве на льготу";
            //реестр.СтоимсотьУслуги = "Стоимость услуги, руб";
            //list.Add(реестр);

            this.dtEnd =  this.dtEnd.AddDays(1);
            //  this.dtStart.Value = this.dtStart.Value.AddDays(-1);

            string endYear = this.dtEnd.Year.ToString();
            //отними 1
            //string endDay = this.dtEnd.Value.Day.ToString();
            string endDay = Convert.ToString(this.dtEnd.Day);
            string endMonth = this.dtEnd.Month.ToString();

            string strYear = this.dtStart.Year.ToString();
            //прибавим 1
            //string strDay = this.dtStart.Value.Day.ToString();
            string strDay = Convert.ToString(this.dtStart.Day);
            string strMonth = this.dtStart.Month.ToString();

            string endДата = "#" + endMonth + "/" + endDay + "/" + endYear + "#";
            string strДата = "#" + strMonth + "/" + strDay + "/" + strYear + "#";
            this.dtEnd = this.dtEnd.AddDays(-1);
            
            //////Пройдёмся по DataGridView и добавим в коллекцию отмеченные акты
            //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            //{
            //    if (Convert.ToBoolean(row.Cells["ФлагАктРеестр"].Value) == true)
            //    {
            //        Реестр реестр1 = new Реестр();
            //        реестр1.НомерПорядковый = row.Cells["НомерПорядковый"].Value.ToString();
            //        реестр1.Id_акт = Convert.ToInt32(row.Cells["Id_акт"].Value);

            //        реестр1.ФИО = row.Cells["ФИО"].Value.ToString();
            //        реестр1.НомерДатаДоговора = row.Cells["НомерДатаДоговора"].Value.ToString();

            //        реестр1.НомерДатаАкта = row.Cells["НомерДатаАкта"].Value.ToString();
            //        реестр1.СерияДатаВыдачиДокумента = row.Cells["СерияДатаВыдачиДокумента"].Value.ToString();
            //        реестр1.СтоимсотьУслуги = row.Cells["СтоимсотьУслуги"].Value.ToString();
            //        list.Add(реестр1);

            //        //sum = Math.Round(Math.Round(sum, 2) + Math.Round(Convert.ToDecimal(row.Cells["СтоимсотьУслуги"].Value), 2), 2);

            //        ////Запишем Update для таблицы АктВыполненныхРаборт
            //        //string queryUpdate = "update АктВыполнненныхРабот set НомерПоПеречню = 'True' where id_акт = " + реестр1.Id_акт + " ";

            //        ////Выполним обновление таблицы АктВыполнненныхРабот
            //        //Query.Execute(queryUpdate.ToString().Trim(), ConnectionDB.ConnectionString());
            //    }
            //}

            //Не буду искать где ошибка сделаем суму пустой
            sum = 0.0m;            
            List<Реестр> listSum = new List<Реестр>();

            //int iCountItem = 0;
            ////Заполним данными список listSum
            //foreach (Реестр р in list)
            //{
            //    if (iCountItem > 0)
            //    {
            //        listSum.Add(р);
            //    }
            //    iCountItem++;
            //}
            
            // Список для хранения id договоров.
            List<РеестрВрачТехЛист> listEsc = new List<РеестрВрачТехЛист>();
            string keyGuid = Guid.NewGuid().ToString().Trim();
            int iCountItem = 0;
            
            //Заполним данными список listSum
            foreach (Реестр р in list)
            {
                // Пропустим первый пункт так как он содержит наименование название столбцов и не несет информационной нагрузки, а служит только для отображения названия столбцов.
                if (iCountItem > 0)
                {
                    listSum.Add(р);

                   // if (ConfigurationSettings.AppSettings["OnlySatartov"].ToString().Trim() == "1")
                      if (ConfigurationManager.AppSettings["OnlySatartov"].ToString().Trim() == "1")
                    {
                        // Получим id акт.
                        string queryAct = "select id_договор from АктВыполнненныхРабот where id_акт = " + р.Id_акт + " ";

                        int id_договор = Convert.ToInt32(ТаблицаБД.GetTable(queryAct, ConnectionDB.ConnectionString(), "ActIdContract").Rows[0]["id_договор"]);

                        if (chkBoxНомерСФ.Checked == true)
                        {
                            string queryInsertEsc = " insert into ВрачОплата (idДоговор,IGuid,СчётФактура,Дата) values(" + id_договор + ", '" + keyGuid.Trim() + "','" + this.txtНомерСчётФактуры.Text.Trim() + "','" + this.dateTimePicker1.Value + "' ) ";
                            Query.Execute(queryInsertEsc, ConnectionDB.ConnectionString());
                        }
                        else
                        {
                            string queryInsertEsc = " insert into ВрачОплата (idДоговор,IGuid) values(" + id_договор + ", '" + keyGuid.Trim() + "' ) ";
                            Query.Execute(queryInsertEsc, ConnectionDB.ConnectionString());
                        }
                    }
                }
                iCountItem++;
            }

            foreach (Реестр р in listSum)
            {
                sum = Math.Round(Math.Round(sum, 2) + Math.Round(decimal.Parse(р.СтоимсотьУслуги), 2), 2);
            }

            //Запишем в данные строкчку итого
            Реестр реестр2 = new Реестр();
            реестр2.ФИО = "Всего по реестру";
            реестр2.СтоимсотьУслуги = sum.ToString("c");
            list.Add(реестр2);

            //сформируем word
            //string fName = "Реестр " + this.dtStart.Value.ToShortDateString().Replace('.', '_') + " " + this.dtEnd.Value.ToShortDateString().Replace('.', '_');
            string fName = "Реестр " + this.dtStart.ToShortDateString().Replace('.', '_').Replace('/', '_') + " " + this.dtEnd.ToShortDateString().Replace('.', '_').Replace('/', '_');

            ////Выполним обновление таблицы АктВыполнненныхРабот
            //Query.Execute(builder.ToString().Trim(), ConnectionDB.ConnectionString());

            if (list.Count > 2)
            {
                try
                {
                    //Скопируем шаблон в папку Документы
                    FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Реестр.doc");
                    fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
                }
                catch
                {
                    MessageBox.Show("Документ с таки именем уже существует");
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

                //зададим левый отступ
                doc.PageSetup.LeftMargin = 40f;

                ////Номер договора
                object wdrepl = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt = "категория";
                object newtxt = (object)this.ЛьготнаяКатегория.Trim();
                //object frwd = true;
                object frwd = false;
                doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                ref missing, ref missing);

                //Вставить таблицу
                object bookNaziv = "таблица";
                Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;
                
                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                table.Range.ParagraphFormat.SpaceAfter = 6;
                table.Columns[1].Width = 40;
                table.Columns[2].Width = 140;
                table.Columns[3].Width = 80;
                table.Columns[4].Width = 110;//ширина столбца с номером акта
                table.Columns[5].Width = 70;
                table.Columns[6].Width = 80;
                table.Borders.Enable = 1; // Рамка - сплошная линия
                table.Range.Font.Name = "Times New Roman";
                //table.Range.Font.Size = 10;
                table.Range.Font.Size = 8;
                //счётчик строк
                int i = 1;    

                //запишем данные в таблицу
                foreach (Реестр item in list)
                {
                    table.Cell(i, 1).Range.Text = item.НомерПорядковый; 
                    table.Cell(i, 2).Range.Text = item.ФИО;
                    table.Cell(i, 3).Range.Text = item.НомерДатаДоговора;
                    table.Cell(i, 4).Range.Text = item.НомерДатаАкта;
                    table.Cell(i, 5).Range.Text = item.СерияДатаВыдачиДокумента;
                    table.Cell(i, 6).Range.Text = item.СтоимсотьУслуги;

                    //doc.Words.Count.ToString();
                    Object beforeRow1 = Type.Missing;
                    table.Rows.Add(ref beforeRow1);

                    i++;
                }
                                
                // Ставим порядковый номер, (по порядку), т.к. при снятии галки порядковый номер не меняется 
                // (строка в списке удаляется) идет следующий номер по порядку
                int pn = 1;
                for (int j = 2; j < list.Count; j++)
                {
                    table.Cell(j, 1).Range.Text = pn++.ToString();
                }                   

                    table.Rows[i].Delete();

                //выведим ФИО главврача 
                string глВрач = "select ФИО_ГлавВрач from ГлавВрач where id_главВрач in (select id_главВрач from Поликлинника)";
                DataTable dtГлавВрач = ТаблицаБД.GetTable(глВрач, ConnectionDB.ConnectionString(), "Поликлинника");
                string главВрач = dtГлавВрач.Rows[0][0].ToString();

                //выведим ФИО глав буха
                string глБух = "select ФИО_ГлавБух from ГлавБух where id_главБух in (select id_главБух from Поликлинника)";
                DataTable dtГлавБух = ТаблицаБД.GetTable(глБух, ConnectionDB.ConnectionString(), "Поликлинника");
                string главБух = dtГлавБух.Rows[0][0].ToString();

                ////Номер договора
                object wdrepl2 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt2 = "главврач";
                object newtxt2 = (object)главВрач;
                //object frwd = true;
                object frwd2 = false;
                doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                ref missing, ref missing);

                ////Номер договора
                object wdrepl3 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt3 = "главбух";
                object newtxt3 = (object)главБух;
                //object frwd = true;
                object frwd3 = false;
                doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
                ref missing, ref missing);

                //Должность на подписи
                string queryДолжность = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                object wdrepl4 = WdReplace.wdReplaceAll;//39
                //object searchtxt = "GreetingLine";
                object searchtxt4 = "Predsedatel";
                object newtxt4 = (object)Predsedatel;
                //object frwd = true;
                object frwd4 = false;
                doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
                ref missing, ref missing);

                //должность руководителя ТО
                string quyeryTO = "select Должность, ФИО_Руководитель from ФиоШев where id_шев in (select id_шев from Комитет)";
                DataTable tabРуковод = ТаблицаБД.GetTable(quyeryTO, ConnectionDB.ConnectionString(), "Руководитель");

                //получим должность
                string должность = tabРуковод.Rows[0]["Должность"].ToString();

                object wdrepl5 = WdReplace.wdReplaceAll;//39
                //object searchtxt = "GreetingLine";
                object searchtxt5 = "должность";
                object newtxt5 = (object)должность;
                //object frwd = true;
                object frwd5 = false;
                doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                ref missing, ref missing);

                //получим ФИО руководителя ТО
                string руководитель = tabРуковод.Rows[0]["ФИО_Руководитель"].ToString();

                object wdrepl6 = WdReplace.wdReplaceAll;//39
                //object searchtxt = "GreetingLine";
                object searchtxt6 = "руководитель";
                object newtxt6 = (object)руководитель;
                //object frwd = true;
                object frwd6 = false;
                doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
                ref missing, ref missing);

                app.Visible = true;

                //закроем окно
                this.Close();

                //Выгрузить реестр
                //UnloadDate unloadDate = new UnloadDate(strДата, endДата, this.cmbЛьготнаяКатегория.Text);

                //string sTest = "Test";

                if (this.РеестрЗаключенныхДоговоров == true)
                {
                    //Уберём из списка 1-ый элемент
                    list.RemoveAt(0);

                    //Узнаем оставшееся количество элементов в списке
                    int countItem = list.Count;

                    //Удалим последний элемент
                    list.RemoveAt(countItem-1);

                    //Тест контроль
                    List<Реестр> iList = list;

                    UnloadDate unloadDate = new UnloadDate(strДата, endДата, this.ЛьготнаяКатегория,list);
                    List<Unload> unload = unloadDate.Выгрузка();

                    //закроем окно
                    this.Close();

                    //Проверим в файле конфигурации Config.dll разрешена выгрузка реестра в файл или нет
                    using (FileStream fs = File.OpenRead("Config.dll"))
                    using (TextReader read = new StreamReader(fs))
                    {
                        string sConfig = read.ReadLine();
                        if (sConfig == "1")
                        {
                            //Разрешаем выгрузку реестра в файл
                            flagUnLoad = true;
                        }
                        else
                        {
                            //запрещаем выгрузку реестра в файл
                            flagUnLoad = false;
                        }
                    }

                    if (flagUnLoad == true)
                    {
                        //Проверим если список List<Unload> не пустой
                        if (unload.Count != 0)
                        {

                            //получим путь к файлу
                            //SaveFileDialog saveFile = new SaveFileDialg();
                            SaveFileDialog saveFile = new SaveFileDialog();
                            saveFile.DefaultExt = string.Empty;
                            saveFile.Filter = "All files (*.*)|*.*";

                            //Получим красивое название файла
                            //string arDateStart = strДата.Replace('#', '_');
                            ////string arrDateStart = arDateStart.Replace('/', '_');

                            //string arrDateStart = arDateStart.Replace('/', '.');
                            ////string fileNameBg = arrDateStart;

                            //string arEndДата = "_" + this.dtEnd.Value.ToShortDateString();// .Replace('#', '_');
                            ////string arrEndДата = arEndДата.Replace('/', '_');

                            //string arrEndДата = arEndДата.Replace('/', '.');
                            ////string fileNameEnd = arrEndДата;

                            string arrDateStart = Convert.ToDateTime(this.dtStart).ToShortDateString().Replace('/', '_');
                            string arrEndДата = Convert.ToDateTime(this.dtEnd).ToShortDateString().Replace('/', '_');

                            saveFile.FileName = arrDateStart + "_" + this.ЛьготнаяКатегория + "_" + arrEndДата + ".r";
                            //saveFile.ShowDialog();

                            string fileBinaryName = string.Empty;
                            
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

                            //Установим для проги текущую директорию для корректного считывания пути к БД
                            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;
                        }
                    }
                }
            } 
            else
            {
                MessageBox.Show("Выберите данные для реестра");
            }

            //Сбросим счётчик порядковых номеров
            numOrdinal = 0;

            //Обнулим все переменные
            list.Clear(); 

            Реестр реестр = new Реестр();
            реестр.НомерПорядковый = "№ п.п";
            реестр.ФИО = "Ф.И.О.";
            реестр.НомерДатаДоговора = "№ и дата договора на предоставление услуг";
            реестр.НомерДатаАкта = "№ и дата акта выполненных работ";
            реестр.СерияДатаВыдачиДокумента = "Серия, № и дата документа о праве на льготу";
            реестр.СтоимсотьУслуги = "Стоимость услуги, руб";
            list.Add(реестр);

            //Переменная хранит сумму выделенных договоров
            sum = 0.0m;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                //Получим значение флага
                bool flag = Convert.ToBoolean(this.dataGridView1.CurrentRow.Cells["ФлагАктРеестр"].Value);
                //if (index != -10) //отсеиваем дубли
                //{
                //если пользователь выбрал текущий акт
                if (flag == true)
                {
                    foreach (Реестр row in list)
                    {
                        if (
                            row.Id_акт == Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Id_акт"].Value) &
                            row.ФИО == this.dataGridView1.CurrentRow.Cells["ФИО"].Value.ToString() &
                            row.НомерДатаДоговора == this.dataGridView1.CurrentRow.Cells["НомерДатаДоговора"].Value.ToString() &
                            row.НомерДатаАкта == this.dataGridView1.CurrentRow.Cells["НомерДатаАкта"].Value.ToString() &
                            row.СерияДатаВыдачиДокумента == this.dataGridView1.CurrentRow.Cells["СерияДатаВыдачиДокумента"].Value.ToString() &
                            row.СтоимсотьУслуги == this.dataGridView1.CurrentRow.Cells["СтоимсотьУслуги"].Value.ToString()
                            )
                            return;
                    }
                    
                    numOrdinal++;

                    Реестр реестр1 = new Реестр();
                    реестр1.НомерПорядковый = numOrdinal.ToString();//this.dataGridView1.CurrentRow.Cells["НомерПорядковый"].Value.ToString();
                    реестр1.Id_акт = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Id_акт"].Value);

                    реестр1.ФИО = this.dataGridView1.CurrentRow.Cells["ФИО"].Value.ToString();
                    реестр1.НомерДатаДоговора = this.dataGridView1.CurrentRow.Cells["НомерДатаДоговора"].Value.ToString();

                    реестр1.НомерДатаАкта = this.dataGridView1.CurrentRow.Cells["НомерДатаАкта"].Value.ToString();
                    реестр1.СерияДатаВыдачиДокумента = this.dataGridView1.CurrentRow.Cells["СерияДатаВыдачиДокумента"].Value.ToString();
                    реестр1.СтоимсотьУслуги = this.dataGridView1.CurrentRow.Cells["СтоимсотьУслуги"].Value.ToString();

                    //Проверим есть ли в списке такой реестр с таким id
                    foreach (Реестр rCompare in list)
                    {
                        if (rCompare.Id_акт != реестр1.Id_акт)
                        {
                            list.Add(реестр1);

                            sum = Math.Round(Math.Round(sum, 2) + Math.Round(Convert.ToDecimal(this.dataGridView1.CurrentRow.Cells["СтоимсотьУслуги"].Value), 2), 2);

                            //Запишем Update для таблицы АктВыполненныхРаборт
                            string queryUpdate = "update АктВыполнненныхРабот set НомерПоПеречню = 'True' where id_акт = " + реестр1.Id_акт + " ";

                            //Выполним запрос на обновление 
                            Query.Execute(queryUpdate, ConnectionDB.ConnectionString());

                            break;
                        }
                    }

                    //list.Add(реестр1);

                    //sum = Math.Round(Math.Round(sum, 2) + Math.Round(Convert.ToDecimal(this.dataGridView1.CurrentRow.Cells["СтоимсотьУслуги"].Value), 2), 2);

                    ////Запишем Update для таблицы АктВыполненныхРаборт
                    //string queryUpdate = "update АктВыполнненныхРабот set НомерПоПеречню = 'True' where id_акт = " + реестр1.Id_акт + " ";

                    ////Выполним запрос на обновление 
                    //Query.Execute(queryUpdate, ConnectionDB.ConnectionString());
                }
            //    index = -10;
            //}

                //если пользователь снял флажок
                if (flag == false)
                {
                    if (list.Count > 1)
                    {
                        for (int i = 0; i <= list.Count - 1; i++)
                        {
                            Реестр реестр = list[i];
                            if (реестр.Id_акт == Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Id_акт"].Value))
                            {
                                if (list.Count != 1)
                                {
                                    list.Remove(реестр);
                                }
                            }
                        }
                    }

                    int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Id_акт"].Value);

                    //Запишем Update для таблицы АктВыполненныхРаборт
                    string queryUpdate = "update АктВыполнненныхРабот set НомерПоПеречню = ''  where id_акт = " + id + " ";

                    //Выполним запрос на обновление 
                    Query.Execute(queryUpdate, ConnectionDB.ConnectionString());
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Проставим везде галочки
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.Cells["ФлагАктРеестр"].Value = false;
            }

            // Обнулим список выделенных договоров.ъ
            //list.Clear();

            list.Clear();
            Реестр реестр = new Реестр();
            реестр.НомерПорядковый = "№ п.п";
            реестр.ФИО = "Ф.И.О.";
            реестр.НомерДатаДоговора = "№ и дата договора на предоставление услуг";
            реестр.НомерДатаАкта = "№ и дата акта выполненных работ";
            реестр.СерияДатаВыдачиДокумента = "Серия, № и дата документа о праве на льготу";
            реестр.СтоимсотьУслуги = "Стоимость услуги, руб";
            list.Add(реестр);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }
        
        // ==========Поиск============
        private void search(string s2, string ind)
        {
            int k = 0;  //обнуляем счетчмк
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string s1 = dataGridView1.Rows[i].Cells[ind].Value.ToString().ToLower().Trim();                
                int index = s1.IndexOf(s2);
                dataGridView1.Rows[i].Selected = false;//обнуляем выделения таблицы
                
                if (index >= 0)
                {
                    dataGridView1.Rows[i].Selected = true;
                    k++;
                    //прокрутка списк до первой выделенноой строки
                    if (k == 1)
                        dataGridView1.FirstDisplayedScrollingRowIndex = i;
                }
                label_sovpad.Text = "Кол-во совпадений - " + k.ToString();  // обновляем label
            }

            // обнуление выделения и label-a при очистки поля поиска
            if (s2 == null || s2 == String.Empty)
            {
                label_sovpad.Text = "Кол-во совпадений - ";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Selected = false;
            }
        }

         //прокрутка выделенного списка по нажатию на Enter 
        public void enter(int ind)
        {
            ind++;
            for (int i = ind ; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                    return; 
                }
            }
        }

        private void tb_search_fio_TextChanged(object sender, EventArgs e)
        {
            search(tb_search_fio.Text.ToLower().Trim().ToString(), "ФИО");
        }

        private void tb_search_dogovor_TextChanged(object sender, EventArgs e)
        {
            search(tb_search_dogovor.Text.ToLower().Trim().ToString(), "НомерДатаДоговора");
        }

        private void tb_search_fio_Click(object sender, EventArgs e)
        {
            tb_search_dogovor.Clear();
        }

        private void tb_search_dogovor_Click(object sender, EventArgs e)
        {
            tb_search_fio.Clear();
        }

        //нажатие кнопки Enter
        private void tb_search_fio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)                               //параметром является индекс
                enter(dataGridView1.FirstDisplayedScrollingRowIndex);  //отображаемой первой строки
        }

        private void tb_search_dogovor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                enter(dataGridView1.FirstDisplayedScrollingRowIndex); 
        }

        //записываем номер строки где происходит клик для отсеивания дублей
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = dataGridView1.CurrentRow.Index;
            label_sovpad.Text = "Кол-во совпадений - ";
        }      
    }
}