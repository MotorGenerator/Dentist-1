using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary1;

// библиотеки Excel.
using ExcelLibrary;
using ExcelLibrary.SpreadSheet;


namespace Стамотология
{
    public partial class FormДоговор : Form
    {
        // ФИО для проверки наличия договора
        public string familiya;
        public string imya;
        public string otchestvo;


        //private bool отметкаВыполнения = false;
        //private bool добавитьЗапись = false;

        //хранит id договора
        private int id_дог;

        // Ключ что нельзя снимать акт.
        private bool flagKeyЗапретСнятьАкт = true;

        //строка запрса на insert договора
        private string queryУслугиF;

        //флаг указывает что форма с актом доп соглашения 
        private bool flagАктДопСоглашение = false;

        private bool flagPrintДопСоглашение = false;

        //если флаг = true то gvДопСоглашения отображаются данные
        private string flagПодписьДопСоглашение = string.Empty;

        //флаг наличия у договора акта выполненных работ для договора
        //private bool flagДоговорValidate = false;

        private int idДоговор;

        //хранит номер договора
        string номерДоговора = string.Empty;

        //хранит номер доп соглашения
        string номерДопСоглашения = string.Empty;

        //хранит дату договора
        //DateTime датаДоговора;

        //хранит id выбранной услуги
        private int id_услуга;

        //хранит id льготной категории
        private int id_льготнойКатегории;

        //хранит id поликлинники
        private int id_поликлинники;

        private string insertПервоеЯнваря = string.Empty;
        private string insertТридцатьПервое = string.Empty;

        private int id_договор;
        private string _ФИО;

        //хранит ФИО врача в именительном падеже
        private string фиоВрача;

        //хранит ФИО врача в родительнмо падеже
        private string фиоВрачаРодПадеж = string.Empty;

        /// <summary>
        /// Флаг указывает о наличии доп соглашения true = доп соглашение есть, false = нет
        /// </summary>
        //private bool flagДопСоглашение = false;

        private string кодКодПоликлинники;

        //строка хранит запрос на определение номер договора в АКТ
        private string queryNum = string.Empty;

        //порядковый номер акта АКТ
        private string порядковыйНомер = string.Empty;

        //строка для хранения номера договора АКТ
        private string номер = string.Empty;
        
        /// <summary>
        /// Флаг указывает что по gvДоговор был CellClck
        /// </summary>
        //private bool flagCellClicДоговор = false;

        //переменная для хранения ud комитета
        private int id_комитет = 0;

        //переменная для хранения количества договоров с предыдущей версии
        private int countДоговор = 0;

        /// <summary>
        /// Переменная хранит название города в котором находиться территориальный орган управления
        /// </summary>
        private string названиеГорода = string.Empty;
      
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

        /// <summary>
        /// Хранит id льготной категории
        /// </summary>
        public int IdЛьготнойКатегории
        {
            get
            {
                return id_льготнойКатегории;
            }
            set
            {
                id_льготнойКатегории = value;
            }
        }

        /// <summary>
        /// Свойство запрещает (состояние - true) снимать акты.
        /// </summary>
        public bool FlagKeyЗапретСнятьАкт
        {
            get
            {
                return flagKeyЗапретСнятьАкт;
            }
            set
            {
                flagKeyЗапретСнятьАкт = value;
            }
        }

        //private DateTime date;

        public FormДоговор()
        {
            InitializeComponent();

            try
            {
                //string фиоВрач = "select ФИО_ГлавВрач from Поликлинника";
                //фиоВрача = ТаблицаБД.GetTable(фиоВрач, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0].ToString();

                string queryФиоВрач = "select ФИО_ГлавВрач,ФИО_РодительныйПадеж from ГлавВрач where id_главВрач in (select id_главВрач from Поликлинника)";
                фиоВрача = ТаблицаБД.GetTable(queryФиоВрач, ConnectionDB.ConnectionString(), "ГлавВрач").Rows[0][0].ToString();

                DataTable dt = ТаблицаБД.GetTable(queryФиоВрач, ConnectionDB.ConnectionString(), "ГлавВрач");

                //получим ФИО врача в именительном падеже
                фиоВрача = dt.Rows[0][0].ToString();

                //получим ФИО врача в родительном падеже
                фиоВрачаРодПадеж = dt.Rows[0][1].ToString();

                string queryCountDogovor = "select НачальныйНомерДоговора from Поликлинника";
                countДоговор = Convert.ToInt32(ТаблицаБД.GetTable(queryCountDogovor, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);
            }
            catch
            {
                MessageBox.Show("Нет данных по поликлиннике");
            }
        }

        private void FormДоговор_Load(object sender, EventArgs e)
        {
            // Запрещаем редактирование dataGrid
            gvДопСоглашение.ReadOnly = true;

            string fileName = System.Windows.Forms.Application.StartupPath.ToString() + @"\" + "KeyDantist.k";

            KeyHospital key;// = new KeyHospital();


            //SerializableObject objToSerialize = null;
            //FileStream fstream = File.Open(fileName, FileMode.Open);
            try
            {
                FileStream fstream = File.Open("KeyDantist.k", FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //Получим из файла словарь с договорами
                key = (KeyHospital)binaryFormatter.Deserialize(fstream);

                //Закроем файловый поток
                fstream.Close();

                if (this.FlagKeyЗапретСнятьАкт == false)
                {
                    if (key.Flag == false)
                    {

                        flagKeyЗапретСнятьАкт = false;
                    }
                    else
                    {
                        this.button3.Visible = false;
                    }
                }
                else
                {
                    this.button3.Visible = false;
                }
            }
            catch
            {
                //this.button3.Visible = false;
                //MessageBox.Show("Отсутствует ключевой файл");
            }


            //if (ConfigurationSettings.AppSettings["OnlySatartov"].Trim() == "1")
            if (ConfigurationManager.AppSettings["OnlySatartov"].Trim() == "1")
            {
                this.label1.Visible = true;
                this.cmbEsculap.Visible = true;
                this.label4.Visible = true;
                this.txtTechSheet.Visible = true;

                this.button4.Enabled = true;

                // Загрузим данными из раскрывающегося списка.
                string queryEsculap = "select id_врач, ФИО from Врач";
                DataTable tabРайон = ТаблицаБД.GetTable(queryEsculap, ConnectionDB.ConnectionString(), "ВрачиПротезисты");

                this.cmbEsculap.DataSource = tabРайон;
                this.cmbEsculap.ValueMember = "id_врач";
                this.cmbEsculap.DisplayMember = "ФИО";
                this.cmbEsculap.DropDownStyle = ComboBoxStyle.DropDownList;


            }
            else
            {
                this.label1.Visible = false;
                this.cmbEsculap.Visible = false;
                this.label4.Visible = false;
                this.txtTechSheet.Visible = false;
            }

            //получим id комитета
            string queryКомитет = "select * from Комитет";
            id_комитет = Convert.ToInt32(ТаблицаБД.GetTable(queryКомитет, ConnectionDB.ConnectionString(), "Комитет").Rows[0][0]);
            
            //Отобразим номера договоров
            
            //Получим текущую дату
            DateTime dt = DateTime.Today;
            
            //получим текущий год
            int Year = dt.Year;

            //получим первое января
            string первоеЯнваря = "01.01." + Year.ToString();// +"0101";

            insertПервоеЯнваря = первоеЯнваря;

            string тридцатьПервоеДекабря = "31.12." + Year.ToString();// +"3112";
            insertТридцатьПервое = тридцатьПервоеДекабря;

            //строка подключения
            string sCon = ConnectionDB.ConnectionString();
            string queryКод = "select КодПоликлинники from Поликлинника";

            кодКодПоликлинники = ДоговорTable.GetNumberДоговор(queryКод, sCon);

            //Выведим номер договора
            //string queryNumber = "select COUNT(НомерДоговора) + 1 from Договор";

            string querynum = "select COUNT(НомерДоговора) as 'NUM' from Договор ";
            DataTable tabNumber = ТаблицаБД.GetTable(querynum, ConnectionDB.ConnectionString(), "НомерДоговора");

            int num = Convert.ToInt32(tabNumber.Rows[0]["'NUM'"]);
            string queryNumber = string.Empty;
            if (num == 0)
            {
                queryNumber = "select НомерДоговора from Договор order by НомерДоговора desc";
            }
            else
            {
                queryNumber = "select НомерДоговора from Договор where id_договор = (select MAX(id_договор) from Договор)";
            }
            DataTable tabNum = ТаблицаБД.GetTable(queryNumber, ConnectionDB.ConnectionString(), "НомерДоговора");


            //получим количество строк в таблице
            int iCountRow = tabNum.Rows.Count-1;

            string sNum = string.Empty;
            string[] sArr;

            int numContr;

            if (iCountRow < 0)
            {
                //sNum = "0";
                numContr = 1;
            }
            else
            {
                //sNum = tabNum.Rows[iCountRow]["НомерДоговора"].ToString().Trim();
                //sNum = tabNum.Rows[Convert.ToInt32(num - 1)]["НомерДоговора"].ToString();
                sNum = tabNum.Rows[0]["НомерДоговора"].ToString();
                
               
                //Получим номер последнего договора
                sArr = sNum.Split('/');
                numContr = Convert.ToInt32(sArr[1]) + 1;

            }

            //Получим последную строку из таблицы
            //string sNum = tabNum.Rows[iCountRow]["НомерДоговора"].ToString().Trim();

            //int numContr;

            //Получим номер последнего договора
            //string[] sArr = sNum.Split('/');
            //numContr = Convert.ToInt32(sArr[1]) + 1;
      
            //выведим номер следующего договора
            this.label3.Text = кодКодПоликлинники + "/" + numContr.ToString();


             //Выведим ФИО льготника
             
            this.lblFIO.Text = this.ФИО_Льготника;

            //if(nДогValid != "1")
            //{
             //Отобразми договоры для данного льготника
             string query = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения,ФлагНаличияАкта from Договор where id_льготник = " + Id_Льготник + " order by НомерДоговора desc";

             this.gvДоговор.DataSource = DisplayДоговор.ДанныеДоговор(query, sCon, "Договор");
            //}
            //else
            //{
            //                 //Отобразми договоры для данного льготника
            // string query = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения,ФлагНаличияАкта from Договор where id_льготник = " + Id_Льготник + " ";
            // //this.gvДоговор.DataSource = ДанныеПредставление.GetПредставление(query, "Договор");
            // this.gvДоговор.DataSource = DisplayДоговор.ДанныеДоговор(query, sCon, "Договор");

            //}

            ////получить каким то образом из запроса что верхная строчка имееет поле ФлагДопСоглашения = в True
            // string queryДопСоглашение = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения from Договор where id_льготник = " + Id_Льготник + " order by НомерДоговора asc";

             
             this.gvДоговор.Columns["id_договор"].Visible = false;
             this.gvДоговор.Columns["ФлагНаличияДоговора"].Visible = false;
             
             this.gvДоговор.Columns["ФлагДопСоглашения"].Visible = false;
             this.gvДоговор.Columns["ФлагНаличияАкта"].Visible = false;
             //ФлагДопСоглашения

             try
             {
                 //проверим если договор не подписан
                 if (Convert.ToBoolean(this.gvДоговор.Rows[0].Cells["ФлагНаличияДоговора"].Value) == false)
                 {
                     this.btnАкт.Enabled = false;
                     this.button2.Enabled = true;

                     //не дадим пользователою заключить доп соглашение
                    this.btnДопСоглашение.Enabled = false;
                    this.btnДатаДоп.Enabled = false;

                     //отобразим кнопки добавить и удалить услугу
                    this.btnDelete.Enabled = true;
                    this.btnAdd.Enabled = true;
                 }
                 else
                 {
                     //договор подписан
                     this.btnАкт.Enabled = true;
                     this.button2.Enabled = false;


                     //Разрешим пользователю заключить доп соглашение
                     this.btnДопСоглашение.Enabled = true;
                     this.btnДатаДоп.Enabled = true;

                     //проверим есть ли для данного договора доп соглашения 
                     string queryДопСоглашения = "select distinct top 1 * from ДопСоглашение where id_договор = " + id_договор + " ";
                     DataTable tabНомерСоглашения = ТаблицаБД.GetTable(queryДопСоглашения, sCon, "ДопСоглашение");

                     if (tabНомерСоглашения.Rows.Count != 0)
                     {
                         this.gvДопСоглашение.DataSource = tabНомерСоглашения;
                         

                         //поставить флаг что доп соглашение есть
                     }

                     //скроем кнопки
                     this.btnDelete.Enabled = false;
                     this.btnAdd.Enabled = false;


                 }
                 //}
                 //catch
                 //{
                 //    this.btnАкт.Enabled = false;
                 //}

                 //установим размеры
                 //this.gvДоговор.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                 this.gvДоговор.Columns["НомерДоговора"].Width = 200;
                 this.gvДоговор.Columns["ДатаДоговора"].Width = 200;

                 //Получим id первой записи (у нас первым стоит последная запись)
                 DataGridViewRow row = this.gvДоговор.Rows[0];
                 int id = Convert.ToInt32(row.Cells[0].Value);

                 idДоговор = id;

                 string queryУслуги = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество as 'Кол-во',НомерПоПеречню,Сумма from УслугиПоДоговору where id_договор = " + id + " ";
                 this.gvВидУслуг.DataSource = ДанныеПредставление.GetПредставление(queryУслуги, "УслугиПоДоговору");
                 
                 //this.gvВидУслуг.Columns["Количество"].Visible = false;
                 this.gvВидУслуг.Columns["id_услугиДоговор"].Visible = false;
                 this.gvВидУслуг.Columns["НомерПоПеречню"].Visible = false;
                 this.gvВидУслуг.Columns["'Кол-во'"].Width = 50;


                 //проверим есть ли открытые договоры
                 string queryOpen = "select ФлагНаличияДоговора from Договор where id_льготник = " + this.Id_Льготник + " ";
                 bool flagCloseDogovor = ДоговорTable.CloseДоговоры(queryOpen);

                 string queryДоговор = "select count(НомерДоговора) from Договор where id_льготник = " + this.Id_Льготник + " ";
                 int flagRowДоговор = ДоговорTable.GetIdДоговор(queryДоговор, sCon);

                 if (flagRowДоговор != 0)
                 {
                     if (flagCloseDogovor == false)
                     {
                         this.button4.Enabled = false;


                         //if (flagRowДоговор != 0)
                         //{
                         //   this.button4.Enabled = true;
                         //}
                         //else
                         //{
                         //    this.button4.Enabled = false;
                         //}
                     }

                     //string заполним = "Test";
                 }
                 else
                 {
                     this.button4.Enabled = true;

                 }
             }
             catch
             {
                 this.btnАкт.Enabled = false;
             }


            ////получим есть ли доп соглашения
             string queryНомер = "select id_допСоглашение,id_договор,НомерДопСоглашения,Дата from ДопСоглашение where id_договор = " + idДоговор + " ";// order by  НомерДопСоглашения desc";
             DataTable tabНСоглашения = ТаблицаБД.GetTable(queryНомер, sCon, "ДопСоглашение");

             this.gvДопСоглашение.DataSource = tabНСоглашения;
             this.gvДопСоглашение.Columns["id_допСоглашение"].Visible = false;
             this.gvДопСоглашение.Columns["id_договор"].Visible = false;

             //string номерДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();

            //проверим закрыто доп соглавшение для данного договора или нет
             //string queryList = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения from Договор where id_договор = " + idДоговор + " ";
             //DataTable dtДоговор = ТаблицаБД.GetTable(queryList, sCon, "Договор");

             string queryList = string.Empty;

             //if (ConfigurationSettings.AppSettings["OnlySatartov"] == "0")
               if (ConfigurationManager.AppSettings["OnlySatartov"] == "0")
             {
                 queryList = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения from Договор where id_договор = " + idДоговор + " ";
             }
             else
                   //if (ConfigurationSettings.AppSettings["OnlySatartov"] == "1")
                   if (ConfigurationManager.AppSettings["OnlySatartov"] == "1")
             {
                 queryList = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения,id_врач,НомерТехЛиста from Договор where id_договор = " + idДоговор + " ";
             }


             DataTable dtДоговор = ТаблицаБД.GetTable(queryList, sCon, "Договор");

             //if (dtДоговор.Rows.Count != 0)
             //{
             //    DataRow rДог = dtДоговор.Rows[0];
             //    //если договор открыт кнопка не активная
             //    if (rДог["ФлагДопСоглашения"].ToString() == "True")
             //    {
             //        this.btnДопСоглашение.Enabled = false;
             //    }

             //    if (rДог["ФлагДопСоглашения"].ToString() == "False")
             //    {
             //        this.btnДопСоглашение.Enabled = true;
             //    }
             //}

             if (dtДоговор.Rows.Count != 0)
             {
                 DataRow rДог = dtДоговор.Rows[0];
                 //если договор открыт кнопка не активная
                 if (rДог["ФлагДопСоглашения"].ToString() == "True")
                 {
                     this.btnДопСоглашение.Enabled = false;
                 }

                 if (rДог["ФлагДопСоглашения"].ToString() == "False")
                 {
                     this.btnДопСоглашение.Enabled = true;
                 }

                 //if (ConfigurationSettings.AppSettings["OnlySatartov"] == "1")
                 if (ConfigurationManager.AppSettings["OnlySatartov"] == "1")
                 {
                     // Получим id врача и номер договора.
                     if (dtДоговор.Rows[0]["id_врач"] != DBNull.Value)
                     {
                         // Отобразим номер тех листа.
                         this.txtTechSheet.Text = dtДоговор.Rows[0]["НомерТехЛиста"].ToString().Trim();

                         // Отобразим ФИО врача в раскрывающемся списке.
                         string queryEsculap = "select ФИО from Врач where id_врач = " + Convert.ToInt32(dtДоговор.Rows[0]["id_врач"]) + " ";
                         DataTable dtEsculap = ТаблицаБД.GetTable(queryEsculap, sCon, "SelectРач");

                         // Отобразм врача который обслуживал льготника по текущему договору.
                         string strTemp = dtEsculap.Rows[0]["ФИО"].ToString().Trim();
                         //this.cmbEsculap.Text = strTemp.Trim();

                         this.cmbEsculap.SelectedIndex = cmbEsculap.FindString(strTemp);
                     }
                 }
             }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //отобразим введённых льготников
            //string query = "select * from Льготник where Фамилия like '" + this.txtФИО.Text + "%'";
            //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //отметкаВыполнения = this.dataGridView2.CurrentRow.Cells["Выбрать"].Selected;

            //if (отметкаВыполнения == true)
            //{
            //    добавитьЗапись = true;
            //}
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //отследим изменения в DatatGridView
            DataGridViewCell col = this.gvВидУслуг.CurrentCell;

            //Определим тип выбранной ячейки
            Type t = col.GetType();

            if (t.ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
            {

            }
        }        

        private void button2_Click(object sender, EventArgs e)
        {            
            //Проверим существует файл территориальный орган.bac
            string read = null;
            try
            {
                //StreamReader s = File.Open("LocalBody.bac", FileMode.Open, FileAccess.ReadWrite);
                StreamReader s = File.OpenText("LocalBody.bac");
                while ((read = s.ReadLine()) != null)
                {
                    названиеГорода = read;
                }
                s.Close();
            }
            catch
            {
                //если файл не существует то создадим его и заполним

                FormТеррОргн ft = new FormТеррОргн();
                ft.ShowDialog();

                if(ft.DialogResult == DialogResult.OK)
                {
                    if (ft.Город != "")
                    {
                        FileStream inSream = File.Open("LocalBody.bac", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        inSream.Close();
                        FileInfo f = new FileInfo("LocalBody.bac");

                        StreamWriter w = f.CreateText();
                        w.WriteLine(ft.Город);
                        w.Close();

                        названиеГорода = ft.Город;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            ////Проверим подписан акт или нет
            //string queryДоговорWrit = "select ФлагНаличияДоговора from Договор where id_договор = " + this.gvДоговор.CurrentRow.Cells["id_договор"].Value.ToString().Trim() + " ";
            //bool aktFlag = Convert.ToBoolean(ТаблицаБД.GetTable(queryДоговорWrit, ConnectionDB.ConnectionString(), "Договор").Rows[0][0]);

            //if (aktFlag != true)
            //{

                //this.button2.Enabled = false;
                bool iTest = this.flagPrintДопСоглашение;

                if (this.flagPrintДопСоглашение == false)
                {
                    //прочтём ФИО глав врача в родительном падеже
                    //FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"ФИО.txt");
                    //TextWriter tw = fn.

                    //string родПадежГлВрач = string.Empty;

                    //StreamReader sr = File.OpenText(System.Windows.Forms.Application.StartupPath + @"\ФИО.txt");

                    //while ((родПадежГлВрач = sr.ReadLine()) != null)
                    //{
                    //   MessageBox.Show(System.Text.ASCIIEncoding.UTF8 родПадежГлВрач.Trim());
                    //}


                    //using (StreamReader reader = File.OpenText(System.Windows.Forms.Application.StartupPath + @"/ФИО.txt"))
                    //    while (reader.Peek() > -1)
                    //    {
                    //        родПадежГлВрач = reader.ReadLine().ToString().Trim();
                    //    }





                    //===================Сформируем название документа================

                    string sCon = ConnectionDB.ConnectionString();
                    //Получим код поликлинники
                    string queryКод = "select * from Поликлинника";
                    DataTable tabПоликлинника = Поликлинника.GetПоликлинники(queryКод, sCon);

                    DataRow row = tabПоликлинника.Rows[0];

                    //наименование поликлинники
                    string наименованиеПоликлинники = row["НаименованиеПоликлинники"].ToString();

                    //ФИО глав врач
                    //string фиоГлавврач = row["ФИО_ГлавВрач"].ToString();
                    string фиоГлавврач = фиоВрача;

                    //Номер лицензии
                    string номерЛицензии = row["НомерЛицензии"].ToString();
                    //№ договора 
                    //string numДоговора = номерДоговора; - не стал его исплльзовать т.к. он есть в переменной

                    //дата регистрации
                    string датаРегистрации = Convert.ToDateTime(row["ДатаРегистрацииЛицензии"]).ToShortDateString();

                    //наименованиеучреждения
                    string наименованиеУчреждения = row["ОрганВыдавшийЛицензию"].ToString();

                    //=======================Данные о льготнике================
                    string queryЛьгот = "select * from Льготник where id_льготник = " + this.Id_Льготник + "";
                    DataTable tabЛьготник = ТаблицаБД.GetTable(queryЛьгот, sCon, "Льготник");

                    DataRow rowL = tabЛьготник.Rows[0];
                    //получим ФИО гражданина
                    string Фамилия = rowL["Фамилия"].ToString();

                    string Имя = rowL["Имя"].ToString();

                    string Отчество = rowL["Отчество"].ToString();

                    string гражданин = Фамилия + " " + Имя + " " + Отчество;

                    //Номер Паспорт
                    string номер = rowL["НомерПаспорта"].ToString();

                    //Серия паспорта
                    string серияПаспорта = rowL["СерияПаспорта"].ToString();

                    //Дата выдачи
                    string датаВыдачи = Convert.ToDateTime(rowL["ДатаВыдачиПаспорта"]).ToShortDateString();

                    //кем выдан 
                    string кемВыдан = rowL["КемВыданПаспорт"].ToString();

                    //===========================Получим льготную категорию=======================
                    string queryЛК = "select * from ЛьготнаяКатегория where id_льготнойКатегории in (select id_льготнойКатегории from Льготник where id_льготник = " + this.Id_Льготник + ")";

                    DataTable tabLK = ТаблицаБД.GetTable(queryЛК, sCon, "ЛьготнаяКатегория");
                    DataRow rowLK = tabLK.Rows[0];

                    //Льготная категория 
                    string льготняКатегория = rowLK["ЛьготнаяКатегория"].ToString();


                    //FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\Договор.doc");
                    //fnDel.Delete();

                    string fName = this.ФИО_Льготника;

                //int idProcessWord = 0;

                    try
                    {
                        //Скопируем шаблон в папку Документы
                        FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Договор.doc");
                        fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);

                    //idProcessWord = System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc").Id;
                }
                    catch
                    {

                    //idProcessWord = System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc").Id;

                        MessageBox.Show("Возможно у вас уже открыт договор с этим льготником. Закройте этот договор.");
                        return;
                    }

                    string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";



                    //System.Diagnostics.Process.Start("C:/asdasd.xls");

                    //Создаём новый Word.Application
                    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                    //Загружаем документ
                    Microsoft.Office.Interop.Word.Document doc = null;

                    object fileName = filName;
                    object falseValue = false;
                    object trueValue = true;
                    object missing = Type.Missing;
                    object writePasswordDocument = "12A86Asd";

                    //1

                    //старая рабочая реализация 
                    //doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    //ref missing, ref missing, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing);

                    doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    ref missing, ref missing, ref missing, ref missing, ref writePasswordDocument,
                    ref missing, ref missing, ref missing, ref missing, ref trueValue,
                    ref missing, ref missing, ref missing);



                    //Object template = Type.Missing;
                    //Object newTemplate = Type.Missing;
                    //Object documentType = Type.Missing;
                    //Object visible = Type.Missing;

                    //doc = app.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                    //получим номер договора
                    //string queryДог = "select НомерДоговора from Договор where id_льготник = "+ this._id_льготник  +" ";
                    //DataTable tabДог = ТаблицаБД.GetTable(queryДог, sCon, "Договор");
                    //DataRow rowДог = tabДог.Rows[0];

                    //string номерДоговора = rowДог["НомерДоговора"].ToString().Trim();
                    string номерДоговора;// = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();
                    try
                    {
                        номерДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();
                    }
                    catch
                    {
                        MessageBox.Show("Нет договора.", "Ошибка");
                        return;
                    }

                    ////Номер договора
                    object wdrepl = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt = "НОМЕРДОКУМЕНТА";
                    object newtxt = (object)номерДоговора;
                    //object frwd = true;
                    object frwd = false;
                    doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                    ref missing, ref missing);

                    ////Наименование поликлинники
                    object wdrepl1 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt1 = "НаименованиеПоликлинники";
                    object newtxt1 = (object)наименованиеПоликлинники;
                    //object frwd = true;
                    object frwd1 = false;
                    doc.Content.Find.Execute(ref searchtxt1, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd1, ref missing, ref missing, ref newtxt1, ref wdrepl1, ref missing, ref missing,
                    ref missing, ref missing);

                    //ФИО глав врача в род падеже
                    //string фиоГлавВрач = ConfigurationSettings.AppSettings["pad"].ToString();  

                    object wdrepl37 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt37 = "Рпфиоглавврач";
                    //object newtxt37 = (object)фиоГлавВрач;фиоВрачаРодПадеж
                    object newtxt37 = (object)фиоВрачаРодПадеж;
                    //object frwd = true;
                    object frwd37 = false;
                    doc.Content.Find.Execute(ref searchtxt37, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd37, ref missing, ref missing, ref newtxt37, ref wdrepl37, ref missing, ref missing,
                    ref missing, ref missing);

                    //ФИО глав врач
                    object wdrepl3 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt3 = "ГлавВрачФИО";
                    object newtxt3 = (object)фиоГлавврач;
                    //object frwd = true;
                    object frwd3 = false;
                    doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
                    ref missing, ref missing);

                    ////Номер лицензии
                    object wdrepl4 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt4 = "номерлицензии";
                    object newtxt4 = (object)номерЛицензии;
                    //object frwd = true;
                    object frwd4 = false;
                    doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
                    ref missing, ref missing);

                    ///Дата регистрации
                    object wdrepl5 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt5 = "датарегистрации";
                    object newtxt5 = (object)датаРегистрации;
                    //object frwd = true;
                    object frwd5 = false;
                    doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                    ref missing, ref missing);

                    ///Организация выдавшая свидетельство
                    object wdrepl6 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt6 = "наименованиеучреждения";
                    object newtxt6 = (object)наименованиеУчреждения;
                    //object frwd = true;
                    object frwd6 = false;
                    doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
                    ref missing, ref missing);

                    ///Гражданин
                    object wdrepl7 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt7 = "Льготник";
                    object newtxt7 = (object)гражданин;
                    //object frwd = true;
                    object frwd7 = false;
                    doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
                    ref missing, ref missing);

                    ///Номер паспрта
                    object wdrepl8 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt8 = "номер";
                    object newtxt8 = (object)номер;
                    //object frwd = true;
                    object frwd8 = false;
                    doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
                    ref missing, ref missing);

                    ///серия паспорта
                    object wdrepl10 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    //object searchtxt10 = "датавыдачипаспорта"; 
                    object searchtxt10 = "dataexstandpassword";
                    object newtxt10 = (object)датаВыдачи;
                    //object frwd = true;
                    object frwd10 = false;
                    doc.Content.Find.Execute(ref searchtxt10, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd10, ref missing, ref missing, ref newtxt10, ref wdrepl10, ref missing, ref missing,
                    ref missing, ref missing);

                    ///дата выдачи паспорта
                    object wdrepl9 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt9 = "серияпаспорта";
                    object newtxt9 = (object)серияПаспорта;
                    //object frwd = true;
                    object frwd9 = false;
                    doc.Content.Find.Execute(ref searchtxt9, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd9, ref missing, ref missing, ref newtxt9, ref wdrepl9, ref missing, ref missing,
                    ref missing, ref missing);


                    ///кем выдан паспорт
                    object wdrepl11 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt11 = "кемвыдано";
                    object newtxt11 = (object)кемВыдан;
                    //object frwd = true;
                    object frwd11 = false;
                    doc.Content.Find.Execute(ref searchtxt11, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd11, ref missing, ref missing, ref newtxt11, ref wdrepl11, ref missing, ref missing,
                    ref missing, ref missing);

                    ///кем выдан паспорт
                    object wdrepl12 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt12 = "льготнаякатегория";
                    object newtxt12 = (object)льготняКатегория;
                    //object frwd = true;
                    object frwd12 = false;
                    doc.Content.Find.Execute(ref searchtxt12, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd12, ref missing, ref missing, ref newtxt12, ref wdrepl12, ref missing, ref missing,
                    ref missing, ref missing);

                    //Вставить таблицу
                    object bookNaziv = "таблица";
                    Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                    object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                    object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


                    Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                    table.Range.ParagraphFormat.SpaceAfter = 6;

                    table.Columns[1].Width = 30;
                    table.Columns[2].Width = 50;
                    table.Columns[3].Width = 250;
                    table.Columns[4].Width = 60;
                    table.Columns[5].Width = 30;
                    table.Columns[6].Width = 60;
                    table.Borders.Enable = 1; // Рамка - сплошная линия
                    table.Range.Font.Name = "Times New Roman";
                    table.Range.Font.Size = 10;


                    if (this.id_договор != 0)
                    {
                        id_дог = this.id_договор;
                    }
                    else
                    {
                        id_дог = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);
                    }

                    //получим данные для таблицы
                    string selectQuery = "select * from УслугиПоДоговору where id_договор = " + id_дог + " ";
                    DataTable улугиДоговор = ТаблицаБД.GetTable(selectQuery, sCon, "УслугиПоДоговору");

                    List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();
                    //заполгним первую строку
                    ТаблицаДоговор шапка = new ТаблицаДоговор();
                    шапка.ПорядковыНомер = "№ п/п";
                    шапка.НомерУслуги = "№ усл в справ";
                    шапка.Наименование = "Наименование";
                    шапка.Цена = "Цена, руб";
                    шапка.Количество = "Кол";
                    шапка.Стоимость = "Стоимость";
                    list.Add(шапка);

                    //установим счётчик
                    int iCount = 1;

                    //счётчик суммы итого
                    decimal суммИтого = 0m;

                    //заполним коллекцию классов для таблицы
                    foreach (DataRow r in улугиДоговор.Rows)
                    {
                        ТаблицаДоговор str = new ТаблицаДоговор();
                        str.ПорядковыНомер = iCount.ToString();

                        str.НомерУслуги = r["НомерПоПеречню"].ToString().Replace(',', '.');
                        str.Наименование = r["НаименованиеУслуги"].ToString();

                        str.Цена = r["Цена"].ToString();
                        str.Количество = r["Количество"].ToString();

                        str.Стоимость = r["Сумма"].ToString();
                        list.Add(str);

                        //if (i != 1)
                        //{
                        суммИтого = Math.Round(суммИтого + Convert.ToDecimal(str.Стоимость), 2);
                        //}


                        //увеличем на 1
                        iCount++;
                    }

                    //создадим строку итого 
                    ТаблицаДоговор итого = new ТаблицаДоговор();
                    итого.НомерУслуги = "Итого:";
                    итого.Стоимость = суммИтого.ToString("c");

                    list.Add(итого);

                    //счётчик строк
                    int i = 1;

                    //запишем данные в таблицу
                    foreach (ТаблицаДоговор item in list)
                    {
                        //table.Cell(i, 1).Column.Width = 10f;
                        table.Cell(i, 1).Range.Text = item.ПорядковыНомер;
                        table.Cell(i, 2).Range.Text = item.НомерУслуги;
                        //table.Cell(i, 2).Range.FormattedText.HorizontalInVertical = 

                        table.Cell(i, 3).Range.Text = item.Наименование;
                        table.Cell(i, 4).Range.Text = item.Цена;

                        table.Cell(i, 5).Range.Text = item.Количество;
                        table.Cell(i, 6).Range.Text = item.Стоимость;

                        //doc.Words.Count.ToString();
                        Object beforeRow1 = Type.Missing;
                        table.Rows.Add(ref beforeRow1);

                        i++;
                    }


                    //удалим последную строку
                    table.Rows[i].Delete();

                    //Сформируем подписи
                    //string sCon = ConnectionDB.ConnectionString();
                    //Получим код поликлинники
                    //string queryПол = "select * from Поликлинника";
                    DataTable tabПоликлин = Поликлинника.GetПоликлинники(queryКод, sCon);

                    DataRow rПол = tabПоликлин.Rows[0];
                    //наименование поликлинники
                    string названиеПоликлинники = rПол["НаименованиеПоликлинники"].ToString();

                    ///Наименование поликлинники
                    object wdrepl13 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt13 = "НаименованиеПоликлинники";
                    object newtxt13 = (object)названиеПоликлинники;
                    //object frwd = true;
                    object frwd13 = false;
                    doc.Content.Find.Execute(ref searchtxt13, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd13, ref missing, ref missing, ref newtxt13, ref wdrepl13, ref missing, ref missing,
                    ref missing, ref missing);

                    string ЮридическийАдрес = rПол["ЮридическийАдрес"].ToString();

                    ///Юридичексий адрес
                    object wdrepl14 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt14 = "ЮридическийАдрес";
                    object newtxt14 = (object)ЮридическийАдрес;
                    //object frwd = true;
                    object frwd14 = false;
                    doc.Content.Find.Execute(ref searchtxt14, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd14, ref missing, ref missing, ref newtxt14, ref wdrepl14, ref missing, ref missing,
                    ref missing, ref missing);

                    //ФактическийАдрес
                    string ФактическийАдрес = rПол["ФактическийАдрес"].ToString();

                    ///Юридичексий адрес
                    object wdrepl33 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt33 = "ФактическийАдрес";
                    object newtxt33 = (object)ФактическийАдрес;
                    //object frwd = true;
                    object frwd33 = false;
                    doc.Content.Find.Execute(ref searchtxt33, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd33, ref missing, ref missing, ref newtxt33, ref wdrepl33, ref missing, ref missing,
                    ref missing, ref missing);

                    //Расчётный счёт
                    string РасчётныйСчёт = rПол["РасчётныйСчёт"].ToString();

                    object wdrepl15 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt15 = "РасчётныйСчёт";
                    object newtxt15 = (object)РасчётныйСчёт;
                    //object frwd = true;
                    object frwd15 = false;
                    doc.Content.Find.Execute(ref searchtxt15, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd15, ref missing, ref missing, ref newtxt15, ref wdrepl15, ref missing, ref missing,
                    ref missing, ref missing);

                    //Лицевой счёт ЛИЦЕВОЙСЧЁТИСПОЛНИТЕЛЬ
                    string ЛИЦЕВОЙСЧЁТИСПОЛНИТЕЛЬ = rПол["ЛицевойСчёт"].ToString();

                    ///Юридичексий адрес
                    object wdrepl34 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt34 = "ЛИЦЕВОЙСЧЁТИСПОЛНИТЕЛЬ";
                    object newtxt34 = (object)ЛИЦЕВОЙСЧЁТИСПОЛНИТЕЛЬ;
                    //object frwd = true;
                    object frwd34 = false;
                    doc.Content.Find.Execute(ref searchtxt34, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd34, ref missing, ref missing, ref newtxt34, ref wdrepl34, ref missing, ref missing,
                    ref missing, ref missing);

                    //Наименование банка
                    string НаименованиеБанка = rПол["НаименованиеБанка"].ToString();

                    object wdrepl16 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt16 = "НаименованиеБанка";
                    object newtxt16 = (object)НаименованиеБанка;
                    //object frwd = true;
                    object frwd16 = false;
                    doc.Content.Find.Execute(ref searchtxt16, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd16, ref missing, ref missing, ref newtxt16, ref wdrepl16, ref missing, ref missing,
                    ref missing, ref missing);

                    //Расчётный счёт
                    string ИННПОЛЕ = rПол["ИНН"].ToString();

                    object wdrepl17 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt17 = "ИННПОЛЕ";
                    object newtxt17 = (object)ИННПОЛЕ;
                    //object frwd = true;
                    object frwd17 = false;
                    doc.Content.Find.Execute(ref searchtxt17, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd17, ref missing, ref missing, ref newtxt17, ref wdrepl17, ref missing, ref missing,
                    ref missing, ref missing);

                    //Расчётный счёт
                    string КПППОЛЕ = rПол["КПП"].ToString();

                    object wdrepl18 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt18 = "КПППОЛЕ";
                    object newtxt18 = (object)КПППОЛЕ;
                    //object frwd = true;
                    object frwd18 = false;
                    doc.Content.Find.Execute(ref searchtxt18, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd18, ref missing, ref missing, ref newtxt18, ref wdrepl18, ref missing, ref missing,
                    ref missing, ref missing);

                    //Расчётный счёт
                    string ОРГНПОЛЕ = rПол["ОГРН"].ToString();

                    object wdrepl19 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt19 = "ОРГНПОЛЕ";
                    object newtxt19 = (object)ОРГНПОЛЕ;
                    //object frwd = true;
                    object frwd19 = false;
                    doc.Content.Find.Execute(ref searchtxt19, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd19, ref missing, ref missing, ref newtxt19, ref wdrepl19, ref missing, ref missing,
                    ref missing, ref missing);


                    //Расчётный счёт
                    string БИКПОЛЕ = rПол["БИК"].ToString();

                    object wdrepl20 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt20 = "БИКПОЛЕ";
                    object newtxt20 = (object)БИКПОЛЕ;
                    //object frwd = true;
                    object frwd20 = false;
                    doc.Content.Find.Execute(ref searchtxt20, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd20, ref missing, ref missing, ref newtxt20, ref wdrepl20, ref missing, ref missing,
                    ref missing, ref missing);


                    //Расчётный счёт
                    string ОКПОПОЛЕ = rПол["ОКПО"].ToString();

                    object wdrepl21 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt21 = "ОКПОПОЛЕ";
                    object newtxt21 = (object)ОКПОПОЛЕ;
                    //object frwd = true;
                    object frwd21 = false;
                    doc.Content.Find.Execute(ref searchtxt21, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd21, ref missing, ref missing, ref newtxt21, ref wdrepl21, ref missing, ref missing,
                    ref missing, ref missing);


                    //Расчётный счёт
                    string ОКАТОПОЛЕ = rПол["ОКАТО"].ToString();

                    object wdrepl22 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt22 = "ОКАТОПОЛЕ";
                    object newtxt22 = (object)ОКАТОПОЛЕ;
                    //object frwd = true;
                    object frwd22 = false;
                    doc.Content.Find.Execute(ref searchtxt22, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd22, ref missing, ref missing, ref newtxt22, ref wdrepl22, ref missing, ref missing,
                    ref missing, ref missing);

                    //Заполним потребитель
                    string queryЛ = "select * from Льготник where id_льготник = " + this.Id_Льготник + " ";
                    DataTable tabП = ТаблицаБД.GetTable(queryЛьгот, sCon, "Льготник"); //Поликлинника.GetПоликлинники(queryКод, sCon);

                    DataRow rP = tabП.Rows[0];

                    //получим название района 
                    //string queryР = "select РайонОбласти from НаименованиеРайона where id_район in (select id_район from Льготник where id_льготник = " + this.Id_Льготник + " )";
                    string queryР = "select id_район from Льготник where id_льготник = " + this.Id_Льготник + " ";
                    DataTable tab = ДанныеПредставление.GetПредставление(queryР, "Льготник");

                    //переменная хранит название района
                    string названиеРайона = string.Empty;

                    if (tab.Rows.Count != 0)
                    {
                        int idRegion = Convert.ToInt16(tab.Rows[0][0]);

                        ListRegion listR = new ListRegion();
                        string nameRegion = listR.FindRegion(idRegion);

                        //отобразим район который прописан в БД
                        //названиеРайона = tab.Rows[0][0].ToString() + " р-он";
                        названиеРайона = nameRegion + " р-он";
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


                    string ПолеАдреса = string.Empty;

                    string улица = string.Empty;

                    //string[] arrStrit = rP["улица"].ToString().Replace("  "," ").Split(' ');

                    //if(arrStrit.Length > 1)
                    //{
                    //    foreach(var str in arrStrit)
                    //    {
                    //        if(str.Trim().ToLower() == "Жассминный".Trim().ToLower() || str.ToLower().Trim() == "Дачный".Trim().ToLower() || str.ToLower().Trim() == "Сокол".Trim().ToLower() || str.ToLower().Trim() == "Соклоловый".Trim().ToLower() || str.ToLower().Trim() == "Сторожовка".Trim().ToLower())
                    //        {
                    //            string f = "";
                    //        }    
                    //    }
                    //}
                    //else
                    //{

                    //}

                    //Адрес
                    улица = rP["улица"].ToString();
                    string дом = rP["НомерДома"].ToString();
                    string корпус = rP["корпус"].ToString();
                    //string номерКв = rP["НомерКвартиры"].ToString();

                    //переменная хранит номер квартиры
                    string номерКв = string.Empty;

                    if (rP["НомерКвартиры"].ToString().Length != 0)
                    {
                        номерКв = "кв. " + rP["НомерКвартиры"].ToString();
                    }
                    else
                    {
                        номерКв = "";
                    }

                    bool flagStreet = false;

                    // Узнаем есть ли в адресе сокращение слова ул.
                    if(улица.IndexOf("ул") > 0)
                    {
                          flagStreet = true;
                    }

                    if (корпус != "")
                    {
                        if (flagStreet == false)
                        {
                            ПолеАдреса = названиеРайона + " " + населённыйПункт + " ул. " + улица + " " + "д. " + дом + " " + "корп. " + корпус + " " + номерКв;
                        }
                        else
                        {
                            ПолеАдреса = названиеРайона + " " + населённыйПункт + " " + улица + " " + "д. " + дом + " " + "корп. " + корпус + " " + номерКв;
                        }
                    }
                    else
                    {
                        if (flagStreet == false)
                        {
                            ПолеАдреса = названиеРайона + " " + населённыйПункт + " ул. " + улица + " " + "д. " + дом + " " + номерКв;
                        }
                        else
                        {
                            ПолеАдреса = названиеРайона + " " + населённыйПункт + " " + улица + " " + "д. " + дом + " " + номерКв;
                        }
                    }

                    object wdrepl23 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt23 = "ПолеАдреса";
                    object newtxt23 = (object)ПолеАдреса;
                    //object frwd = true;
                    object frwd23 = false;
                    doc.Content.Find.Execute(ref searchtxt23, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd23, ref missing, ref missing, ref newtxt23, ref wdrepl23, ref missing, ref missing,
                    ref missing, ref missing);

                    //Кем выдан
                    string КемВыданПаспорт = rP["КемВыданПаспорт"].ToString();

                    object wdrepl24 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt24 = "КемВыданПаспорт";
                    object newtxt24 = (object)КемВыданПаспорт;
                    //object frwd = true;
                    object frwd24 = false;
                    doc.Content.Find.Execute(ref searchtxt24, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd24, ref missing, ref missing, ref newtxt24, ref wdrepl24, ref missing, ref missing,
                    ref missing, ref missing);

                    //=====================Заказчик====================================
                    string queryКомитет = "select * from Комитет";
                    DataTable tabК = ТаблицаБД.GetTable(queryКомитет, sCon, "Комитет"); //Поликлинника.GetПоликлинники(queryКод, sCon);

                    DataRow rКомитет = tabК.Rows[0];
                    string НаименованиеКомитета = rКомитет["НаименованиеКомитета"].ToString();

                    object wdrepl25 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt25 = "НаименованиеКомитета";
                    object newtxt25 = (object)НаименованиеКомитета;
                    //object frwd = true;
                    object frwd25 = false;
                    doc.Content.Find.Execute(ref searchtxt25, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd25, ref missing, ref missing, ref newtxt25, ref wdrepl25, ref missing, ref missing,
                    ref missing, ref missing);

                    //ЮредическийАдрес
                    string ЮридическийАдресКомитета = rКомитет["ЮридическийАдрес"].ToString();

                    object wdrepl26 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt26 = "КомитетЮрАдрес";
                    object newtxt26 = (object)ЮридическийАдресКомитета;
                    //object frwd = true;
                    object frwd26 = false;
                    doc.Content.Find.Execute(ref searchtxt26, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd26, ref missing, ref missing, ref newtxt26, ref wdrepl26, ref missing, ref missing,
                    ref missing, ref missing);

                    //Лицевой счёт комитета
                    string ЛИЦЕВОЙСЧЁТКОМИТЕТА = rКомитет["ЛицевойСчет"].ToString();

                    object wdrepl27 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt27 = "ЛИЦЕВОЙСЧЁТКОМИТЕТА";
                    object newtxt27 = (object)ЛИЦЕВОЙСЧЁТКОМИТЕТА;
                    //object frwd = true;
                    object frwd27 = false;
                    doc.Content.Find.Execute(ref searchtxt27, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd27, ref missing, ref missing, ref newtxt27, ref wdrepl27, ref missing, ref missing,
                    ref missing, ref missing);

                    //ИНН  
                    string полеИНН = rКомитет["ИНН"].ToString();

                    object wdrepl28 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt28 = "ИННКОМИТЕТА";
                    object newtxt28 = (object)полеИНН;
                    //object frwd = true;
                    object frwd28 = false;
                    doc.Content.Find.Execute(ref searchtxt28, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd28, ref missing, ref missing, ref newtxt28, ref wdrepl28, ref missing, ref missing,
                    ref missing, ref missing);

                    //КПП КПППОЛЕ
                    string полеКПП = rКомитет["КПП"].ToString();

                    object wdrepl29 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt29 = "КППКОМИТЕТА";
                    object newtxt29 = (object)полеКПП;
                    //object frwd = true;
                    object frwd29 = false;
                    doc.Content.Find.Execute(ref searchtxt29, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd29, ref missing, ref missing, ref newtxt29, ref wdrepl29, ref missing, ref missing,
                    ref missing, ref missing);

                    //РасчётныйСчёт
                    string рс = rКомитет["РасчётныйСчёт"].ToString();

                    object wdrepl30 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    //object searchtxt30 = "рсКом"; 
                    object searchtxt30 = "rsKom"; 
                    object newtxt30 = (object)рс;
                    //object frwd = true;
                    object frwd30 = false;
                    doc.Content.Find.Execute(ref searchtxt30, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd30, ref missing, ref missing, ref newtxt30, ref wdrepl30, ref missing, ref missing,
                    ref missing, ref missing);

                    //НазваниеБанка
                    string НаваниеБанка = rКомитет["НазваниеБанка"].ToString();

                    object wdrepl31 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt31 = "НазваниеБанка";
                    object newtxt31 = (object)НаваниеБанка;
                    //object frwd = true;
                    object frwd31 = false;
                    doc.Content.Find.Execute(ref searchtxt31, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd31, ref missing, ref missing, ref newtxt31, ref wdrepl31, ref missing, ref missing,
                    ref missing, ref missing);

                    //Руководитель
                    string queryРуководитель = "select ФИО_Руководитель from ФиоШев where id_шев = " + Convert.ToInt32(rКомитет["id_шев"]) + " ";
                    string Руководитель = ТаблицаБД.GetTable(queryРуководитель, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();
                    //string Руководитель = rКомитет["Руководитель"].ToString();
                    
                    object wdrepl32 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt32 = "Руководитель";
                    object newtxt32 = (object)Руководитель;
                    //object frwd = true;
                    object frwd32 = false;
                    doc.Content.Find.Execute(ref searchtxt32, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd32, ref missing, ref missing, ref newtxt32, ref wdrepl32, ref missing, ref missing,
                    ref missing, ref missing);

                    //БИК БИККОМИТЕТА
                    string бикКомитета = rКомитет["БИК"].ToString();

                    object wdrepl35 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt35 = "БИККОМИТЕТА";
                    object newtxt35 = (object)бикКомитета;
                    //object frwd = true;
                    object frwd35 = false;
                    doc.Content.Find.Execute(ref searchtxt35, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd35, ref missing, ref missing, ref newtxt35, ref wdrepl35, ref missing, ref missing,
                    ref missing, ref missing);

                    //добавим сумму прописью ниже таблицы
                    //обавим сумму пропсиью
                    string summa = Валюта.Рубли.Пропись(суммИтого);

                    object wdrepl36 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt36 = "прописьсумм";
                    object newtxt36 = (object)summa;
                    //object frwd = true;
                    object frwd36 = false;
                    doc.Content.Find.Execute(ref searchtxt36, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd36, ref missing, ref missing, ref newtxt36, ref wdrepl36, ref missing, ref missing,
                    ref missing, ref missing);

                    //выведим должность руководителя в родительском падеже
                    string queryРук = "select ДолжностьРодПадеж from ФиоШев where id_шев = " + Convert.ToInt32(rКомитет["id_шев"]) + " ";
                    string ДолжностьРодПадеж = ТаблицаБД.GetTable(queryРук, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl38 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt38 = "должрпадеж";
                    object newtxt38 = (object)ДолжностьРодПадеж;
                    //object frwd = true;
                    object frwd38 = false;
                    doc.Content.Find.Execute(ref searchtxt38, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd38, ref missing, ref missing, ref newtxt38, ref wdrepl38, ref missing, ref missing,
                    ref missing, ref missing);

                    //выведим ФИО руководителя в Родительском падеже
                    string queryФиоРуководитель = "select ФИО_РодПадеж from ФиоШев where id_шев = " + Convert.ToInt32(rКомитет["id_шев"]) + " ";
                    string ФИО_РодПадеж = ТаблицаБД.GetTable(queryФиоРуководитель, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();


                    object wdrepl39 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt39 = "Родпадруковод";
                    object newtxt39 = (object)ФИО_РодПадеж;
                    //object frwd = true;
                    object frwd39 = false;
                    doc.Content.Find.Execute(ref searchtxt39, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd39, ref missing, ref missing, ref newtxt39, ref wdrepl39, ref missing, ref missing,
                    ref missing, ref missing);


                    //основание 
                    string queryУстав = "select Основание from ФиоШев where id_шев = " + Convert.ToInt32(rКомитет["id_шев"]) + " ";
                    string Основание = ТаблицаБД.GetTable(queryУстав, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();


                    object wdrepl40 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt40 = "устав";
                    object newtxt40 = (object)Основание;
                    //object frwd = true;
                    object frwd40 = false;
                    doc.Content.Find.Execute(ref searchtxt40, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd40, ref missing, ref missing, ref newtxt40, ref wdrepl40, ref missing, ref missing,
                    ref missing, ref missing);


                    //Должность Должность 
                    string queryДолжность = "select Должность from ФиоШев where id_шев = " + Convert.ToInt32(rКомитет["id_шев"]) + " ";
                    string Должность = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl41 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt41 = "Должность";
                    object newtxt41 = (object)Должность;
                    //object frwd = true;
                    object frwd41 = false;
                    doc.Content.Find.Execute(ref searchtxt41, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd41, ref missing, ref missing, ref newtxt41, ref wdrepl41, ref missing, ref missing,
                    ref missing, ref missing);


                    //Наименование комитета
                    string queryНазваниеКомитет = "select НаименованиеКомитета from Комитет";
                    string НаименовКомитета = ТаблицаБД.GetTable(queryНазваниеКомитет, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl42 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt42 = "террорган";
                    object newtxt42 = (object)НаименовКомитета;
                    //object frwd = true;
                    object frwd42 = false;
                    doc.Content.Find.Execute(ref searchtxt42, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd42, ref missing, ref missing, ref newtxt42, ref wdrepl42, ref missing, ref missing,
                    ref missing, ref missing);

                    //Устав поликлинники устполиклиник
                    string queryУставПоликлинник = "select Основание from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string ОснованиеПоликлинника = ТаблицаБД.GetTable(queryУставПоликлинник, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl43 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt43 = "устполиклиник";
                    object newtxt43 = (object)ОснованиеПоликлинника;
                    //object frwd = true;
                    object frwd43 = false;
                    doc.Content.Find.Execute(ref searchtxt43, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd43, ref missing, ref missing, ref newtxt43, ref wdrepl43, ref missing, ref missing,
                    ref missing, ref missing);

                    //должность врача в род падеже 
                    string queryRodPad = "select ДолжностьРодПадеж from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string RodPad = ТаблицаБД.GetTable(queryRodPad, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl44 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt44 = "должrodpadej";
                    object newtxt44 = (object)RodPad;
                    //object frwd = true;
                    object frwd44 = false;
                    doc.Content.Find.Execute(ref searchtxt44, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd44, ref missing, ref missing, ref newtxt44, ref wdrepl44, ref missing, ref missing,
                    ref missing, ref missing);

                    //Должность врача DoljnostEsculap
                    string queryDoljnostEsculap = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string DoljnostEsculap = ТаблицаБД.GetTable(queryDoljnostEsculap, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl45 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt45 = "DoljnostEsculap";
                    object newtxt45 = (object)DoljnostEsculap;
                    //object frwd = true;
                    object frwd45 = false;
                    doc.Content.Find.Execute(ref searchtxt45, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd45, ref missing, ref missing, ref newtxt45, ref wdrepl45, ref missing, ref missing,
                    ref missing, ref missing);

                    //серия и номер удостоверения справки льготника
                    string queryСПРАВКА = "select * from Льготник where id_льготник = " + this.Id_Льготник + " ";
                    DataRow rowЛьготник = ТаблицаБД.GetTable(queryСПРАВКА, ConnectionDB.ConnectionString(), "Льготник").Rows[0];

                    //впишем серию документа
                    string SERIA = rowЛьготник["СерияДокумента"].ToString();

                    object wdrepl46 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt46 = "SERIA";
                    object newtxt46 = (object)SERIA;
                    //object frwd = true;
                    object frwd46 = false;
                    doc.Content.Find.Execute(ref searchtxt46, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd46, ref missing, ref missing, ref newtxt46, ref wdrepl46, ref missing, ref missing,
                    ref missing, ref missing);

                    //впишем номер документа
                    string НОМСПРАВКА = rowЛьготник["НомерДокумента"].ToString();

                    object wdrepl47 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt47 = "НОМСПРАВКА";
                    object newtxt47 = (object)НОМСПРАВКА;
                    //object frwd = true;
                    object frwd47 = false;
                    doc.Content.Find.Execute(ref searchtxt47, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd47, ref missing, ref missing, ref newtxt47, ref wdrepl47, ref missing, ref missing,
                    ref missing, ref missing);

                    //кем ывыдан документ ISSUEDBY
                    string кемВыданДокумент = rowЛьготник["КемВыданДокумент"].ToString();

                    object wdrepl48 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt48 = "ISSUEDBY";
                    object newtxt48 = (object)кемВыданДокумент;
                    //object frwd = true;
                    object frwd48 = false;
                    doc.Content.Find.Execute(ref searchtxt48, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd48, ref missing, ref missing, ref newtxt48, ref wdrepl48, ref missing, ref missing,
                    ref missing, ref missing);

                    //============район город где проживает льготник РАЙОНГОРОД
                    //получим район где проживает льготик
                    string id_Район = rowЛьготник["id_район"].ToString();
                    string queryРайон = string.Empty;
                    string названиеРайонаОбласти = string.Empty;
                    //if (id_Район != "-1")
                    if (id_Район.ToLower().Trim() != "0".ToLower().Trim())
                    {
                        ListRegion lr = new ListRegion();
                        названиеРайонаОбласти = lr.FindRegion(Convert.ToInt16(id_Район));
                        //queryРайон = "select РайонОбласти from НаименованиеРайона where id_район = " + id_Район + " ";
                        //названиеРайонаОбласти = ТаблицаБД.GetTable(queryРайон, ConnectionDB.ConnectionString(), "НаименованиеРайона").Rows[0][0].ToString() + " р-он";
                    }
                    else
                    {
                        названиеРайонаОбласти = string.Empty;
                    }

                    //получим название населённого пункта
                    string id_насПункт = rowЛьготник["id_насПункт"].ToString();
                    string queryГород = "select Наименование from НаселенныйПункт where id_насПункт = " + id_насПункт + " ";

                    string насПункт = ТаблицаБД.GetTable(queryГород, ConnectionDB.ConnectionString(), "НаселённыйПункт").Rows[0][0].ToString();
                    string РАЙОНГОРОД = названиеРайонаОбласти + " " + насПункт;

                    object wdrepl49 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt49 = "РАЙОНГОРОД";
                    object newtxt49 = (object)РАЙОНГОРОД;
                    //object frwd = true;
                    object frwd49 = false;
                    doc.Content.Find.Execute(ref searchtxt49, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd49, ref missing, ref missing, ref newtxt49, ref wdrepl49, ref missing, ref missing,
                    ref missing, ref missing);

                    //дата выдачи справки date  ДатаВыдачиДокумента

                    string date = Convert.ToDateTime(rowЛьготник["ДатаВыдачиДокумента"]).ToShortDateString();

                    object wdrepl50 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt50 = "date";
                    object newtxt50 = (object)date;
                    //object frwd = true;
                    object frwd50 = false;
                    doc.Content.Find.Execute(ref searchtxt50, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd50, ref missing, ref missing, ref newtxt50, ref wdrepl50, ref missing, ref missing,
                    ref missing, ref missing);

                    //введём название города где расположен территориальный орган 
                    object wdrepl51 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt51 = "tbody";
                    object newtxt51 = (object)названиеГорода.Trim();
                    //object frwd = true;
                    object frwd51 = false;
                    doc.Content.Find.Execute(ref searchtxt51, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd51, ref missing, ref missing, ref newtxt51, ref wdrepl51, ref missing, ref missing,
                    ref missing, ref missing);

                // Данные по реквизитам.
                    string queryЛкУфк = "select * from Реквизиты2021";

                try
                {
                    DataTable tabМинистерство = Поликлинника.GetПоликлинники(queryЛкУфк, sCon);

                    if (tabМинистерство != null && tabМинистерство.Rows != null && tabМинистерство.Rows.Count > 0)
                    {
                        DataRow rowMin = tabМинистерство.Rows[0];

                        //наименование поликлинники
                        string лсУфк = rowMin["txtShortHospital"].ToString();

                        string eks = rowMin["ЕКС"].ToString();

                        string ОКТМО = rowMin["ОКТМО"].ToString();

                        // personalaccaunt 
                        // Выведим лс УФК по Саратовской области.
                        object wdrepl52 = WdReplace.wdReplaceAll;
                        //object searchtxt = "GreetingLine";
                        object searchtxt52 = "txtShortHospital";
                        object newtxt52 = (object)лсУфк.Trim();
                        //object frwd = true;
                        object frwd52 = false;
                        doc.Content.Find.Execute(ref searchtxt52, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd52, ref missing, ref missing, ref newtxt52, ref wdrepl52, ref missing, ref missing,
                        ref missing, ref missing);

                        object wdrepl53 = WdReplace.wdReplaceAll;
                        //object searchtxt = "GreetingLine";
                        object searchtxt53 = "eks";
                        object newtxt53 = (object)eks.Trim();
                        //object frwd = true;
                        object frwd53 = false;
                        doc.Content.Find.Execute(ref searchtxt53, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd53, ref missing, ref missing, ref newtxt53, ref wdrepl53, ref missing, ref missing,
                        ref missing, ref missing);

                        object wdrepl54 = WdReplace.wdReplaceAll;
                        //object searchtxt = "GreetingLine";
                        object searchtxt54 = "ОКТМОПОЛЕ";
                        object newtxt54 = (object)ОКТМО.Trim();
                        //object frwd = true;
                        object frwd54 = false;
                        doc.Content.Find.Execute(ref searchtxt54, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd54, ref missing, ref missing, ref newtxt54, ref wdrepl54, ref missing, ref missing,
                        ref missing, ref missing);

                    }

                }
                catch
                {
                    MessageBox.Show("Установите реквизиты поликлинники");

                    //var idTest = idProcessWord;

                    //System.Diagnostics.Process process1 = System.Diagnostics.Process.GetProcessById(idProcessWord);

                    //process1.Kill();

                    //foreach (var process in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                    //{
                    //    process.Kill();
                    //}

                    // Закроем форму.
                    this.Close();

                    return;
                }

               


                //откроем получившейся документ
                app.Visible = true;

                }

                //====================================Распечатаем Договор доп соглашения
                if (this.flagPrintДопСоглашение == true)
                {
                    string sCon = ConnectionDB.ConnectionString();
                    //Получим код поликлинники
                    string queryКод = "select * from Поликлинника";
                    DataTable tabПоликлинника = Поликлинника.GetПоликлинники(queryКод, sCon);

                    DataRow row = tabПоликлинника.Rows[0];

                    //наименование поликлинники
                    string наименованиеПоликлинники = row["НаименованиеПоликлинники"].ToString();

                    //ФИО глав врача



                    string fName = "Дополнительное соглашение " + this.ФИО_Льготника;

                    try
                    {
                        //Скопируем шаблон в папку Документы
                        FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ДопСоглашение.doc");
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

                    //2

                    //doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    //ref missing, ref missing, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing);

                    doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                    //получим номер документа
                    //номерДопСоглашения
                    //номердокумента
                    object wdrepl = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt = "номердокумента";
                    object newtxt = (object)номерДопСоглашения;
                    //object frwd = true;
                    object frwd = false;
                    doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                    ref missing, ref missing);

                    //наименование поликолинники
                    object wdrepl1 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt1 = "наименованиеполиклинники";
                    object newtxt1 = (object)наименованиеПоликлинники;
                    //object frwd = true;
                    object frwd1 = false;
                    doc.Content.Find.Execute(ref searchtxt1, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd1, ref missing, ref missing, ref newtxt1, ref wdrepl1, ref missing, ref missing,
                    ref missing, ref missing);


                    //льготник
                    object wdrepl2 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt2 = "льготник";
                    object newtxt2 = (object)this.ФИО_Льготника;
                    //object frwd = true;
                    object frwd2 = false;
                    doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                    ref missing, ref missing);

                    //
                    //ФИО глав врач
                    //string фиоГлавврач = row["ФИО_ГлавВрач"].ToString();


                    object wdrepl3 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt3 = "Фиоглавврач";
                    object newtxt3 = (object)фиоВрача;
                    //object frwd = true;
                    object frwd3 = false;
                    doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
                    ref missing, ref missing);

                    //номер договора

                    string queryНомерДоговора = "select НомерДоговора from Договор where id_договор in (select id_договор from ДопСоглашение where НомерДопСоглашения = '" + this.номерДопСоглашения + "')";
                    DataTable tabДоговор = ТаблицаБД.GetTable(queryНомерДоговора, ConnectionDB.ConnectionString(), "Договор");

                    string номерДоговора = string.Empty;

                    if (tabДоговор.Rows.Count != 0)
                        номерДоговора = tabДоговор.Rows[0][0].ToString();

                    //string test = this.label3.Text;
                    object wdrepl4 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt4 = "номердоговора";
                    object newtxt4 = (object)номерДоговора;// this.label3.Text;
                    //object frwd = true;
                    object frwd4 = false;
                    doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
                    ref missing, ref missing);

                    //получим дату договора
                    string датаДоговора = this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value.ToString();
                    object wdrepl5 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt5 = "датадоговора";
                    object newtxt5 = (object)датаДоговора;
                    //object frwd = true;
                    object frwd5 = false;
                    doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                    ref missing, ref missing);

                    //объявим коллекцию классов для хранения данных таблицы в доп соглашении
                    List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();

                    //Соберём данные для таблицы
                    ТаблицаДоговор шапка = new ТаблицаДоговор();
                    шапка.ПорядковыНомер = "№ п/п";
                    шапка.НомерУслуги = "№ усл в справ";
                    шапка.Наименование = "Наименование";
                    шапка.Цена = "Цена, руб";
                    шапка.Количество = "Кол";
                    шапка.Стоимость = "Стоимость";
                    list.Add(шапка);

                    //заполним list
                    //получим id договора
                    int id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                    string queryДоговор = "select * from УслугиПоДоговору where id_договор = " + id_договор + " ";
                    DataTable tab = ТаблицаБД.GetTable(queryДоговор, ConnectionDB.ConnectionString(), "Договор");

                    //установим счётчик
                    int iCount = 1;

                    //счётчик суммы итого
                    decimal суммИтого = 0m;

                    //заполним коллекцию классов для таблицы
                    foreach (DataRow r in tab.Rows)
                    {
                        ТаблицаДоговор str = new ТаблицаДоговор();
                        str.ПорядковыНомер = iCount.ToString();

                        str.НомерУслуги = r["НомерПоПеречню"].ToString().Replace(',', '.');
                        str.Наименование = r["НаименованиеУслуги"].ToString();

                        str.Цена = r["Цена"].ToString();
                        str.Количество = r["Количество"].ToString();

                        str.Стоимость = r["Сумма"].ToString();
                        list.Add(str);

                        //if (i != 1)
                        //{
                        суммИтого = Math.Round(суммИтого + Convert.ToDecimal(str.Стоимость), 2);
                        //}


                        //увеличем на 1
                        iCount++;
                    }

                    ////заполним коллекцию классов для таблицы
                    //foreach (DataRow r in tab.Rows)
                    //{
                    //    ТаблицаДоговор str = new ТаблицаДоговор();
                    //    str.ПорядковыНомер = iCount.ToString();

                    //    str.НомерУслуги = r["НомерПоПеречню"].ToString().Replace(',', '.');
                    //    str.Наименование = r["НаименованиеУслуги"].ToString();

                    //    str.Цена = r["Цена"].ToString();
                    //    list.Add(str);

                    //    //подсчитаем сумму итого
                    //    суммИтого = Math.Round(суммИтого + Convert.ToDecimal(str.Стоимость), 2);

                    //    //увеличем на 1
                    //    iCount++;
                    //}


                    //создадим строку итого 
                    ТаблицаДоговор итого = new ТаблицаДоговор();
                    итого.НомерУслуги = "Итого:";
                    итого.Стоимость = суммИтого.ToString("c");

                    list.Add(итого);


                    //счётчик строк
                    int i = 1;

                    //Вставить таблицу
                    object bookNaziv = "таблица";
                    Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                    object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                    object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


                    Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                    table.Range.ParagraphFormat.SpaceAfter = 6;

                    table.Columns[1].Width = 30;
                    table.Columns[2].Width = 50;
                    table.Columns[3].Width = 250;
                    table.Columns[4].Width = 60;
                    table.Columns[5].Width = 30;
                    table.Columns[6].Width = 60;

                    table.Borders.Enable = 1; // Рамка - сплошная линия
                    table.Range.Font.Name = "Times New Roman";
                    table.Range.Font.Size = 10;

                    //запишем данные в таблицу
                    foreach (ТаблицаДоговор item in list)
                    {
                        //table.Cell(i, 1).Column.Width = 10f;
                        table.Cell(i, 1).Range.Text = item.ПорядковыНомер;
                        table.Cell(i, 2).Range.Text = item.НомерУслуги;
                        //table.Cell(i, 2).Range.FormattedText.HorizontalInVertical = 

                        table.Cell(i, 3).Range.Text = item.Наименование;
                        table.Cell(i, 4).Range.Text = item.Цена;

                        table.Cell(i, 5).Range.Text = item.Количество;
                        table.Cell(i, 6).Range.Text = item.Стоимость;

                        //doc.Words.Count.ToString();
                        Object beforeRow1 = Type.Missing;
                        table.Rows.Add(ref beforeRow1);

                        i++;
                    }

                    //удалим последную строку
                    table.Rows[i].Delete();

                    //ФИО глав врач
                    //string фиоГлавВрач = row["ФИО_ГлавВрач"].ToString();

                    //получим дату договора
                    //string датаДоговора = this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value.ToString();
                    object wdrepl6 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt6 = "Главврач";
                    object newtxt6 = (object)фиоВрачаРодПадеж;
                    //object frwd = true;
                    object frwd6 = false;
                    doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
                    ref missing, ref missing);

                    //=============ФИО льготника для подписи
                    //Разобьём строку наименованиеПоликлинники массив строк
                    string[] FIO = this.ФИО_Льготника.Split(' ');

                    StringBuilder builder = new StringBuilder();

                    int iCountFio = 0;

                    //отметём пустые строки
                    foreach (string fio in FIO)
                    {
                        if (fio != "")
                        {
                            if (iCountFio == 0)
                            {
                                builder.Append(fio + " ");
                            }
                            else
                            {
                                builder.Append(ShortString.Short(fio) + "." + " ");
                            }

                            iCountFio++;
                        }
                    }

                    //получим строку в формате Фамилия И.О.
                    //string льготникПодпись = FIO[0] + " " + ShortString.Short(FIO[1]) + "." + " " + ShortString.Short(FIO[2]) + ".";

                    string льготникПодпись = builder.ToString();

                    object wdrepl7 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt7 = "Льготподпись";
                    object newtxt7 = (object)льготникПодпись;
                    //object frwd = true;
                    object frwd7 = false;
                    doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
                    ref missing, ref missing);

                    //добавим сумму прописью ниже таблицы
                    //обавим сумму пропсиью
                    string summa = Валюта.Рубли.Пропись(суммИтого);

                    object wdrepl36 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt36 = "прописьсумм";
                    object newtxt36 = (object)summa;
                    //object frwd = true;
                    object frwd36 = false;
                    doc.Content.Find.Execute(ref searchtxt36, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd36, ref missing, ref missing, ref newtxt36, ref wdrepl36, ref missing, ref missing,
                    ref missing, ref missing);


                    //Наименование комитета
                    string queryНазваниеКомитет = "select НаименованиеКомитета from Комитет";
                    string НаименовКомитета = ТаблицаБД.GetTable(queryНазваниеКомитет, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl37 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt37 = "террорган";
                    object newtxt37 = (object)НаименовКомитета;
                    //object frwd = true;
                    object frwd37 = false;
                    doc.Content.Find.Execute(ref searchtxt37, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd37, ref missing, ref missing, ref newtxt37, ref wdrepl37, ref missing, ref missing,
                    ref missing, ref missing);

                    //Должность врача врачдолжн 
                    string queryДолжнВрача = "select ДолжностьРодПадеж from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string ДолжностьРодПадеж = ТаблицаБД.GetTable(queryДолжнВрача, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl38 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt38 = "врачдолжн";
                    object newtxt38 = (object)ДолжностьРодПадеж;
                    //object frwd = true;
                    object frwd38 = false;
                    doc.Content.Find.Execute(ref searchtxt38, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd38, ref missing, ref missing, ref newtxt38, ref wdrepl38, ref missing, ref missing,
                    ref missing, ref missing);

                    //основание 
                    string queryОВ = "select Основание from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string osnova = ТаблицаБД.GetTable(queryОВ, ConnectionDB.ConnectionString(), "ГлавВрач").Rows[0][0].ToString();

                    object wdrepl39 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt39 = "ustav";
                    object newtxt39 = (object)osnova;
                    //object frwd = true;
                    object frwd39 = false;
                    doc.Content.Find.Execute(ref searchtxt39, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd39, ref missing, ref missing, ref newtxt39, ref wdrepl39, ref missing, ref missing,
                    ref missing, ref missing);


                    //выведим должность руководителя в родительском падеже
                    string queryРук = "select ДолжностьРодПадеж from ФиоШев where id_шев = (select id_шев from Комитет) ";
                    string дрп = ТаблицаБД.GetTable(queryРук, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl40 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt40 = "должрпадеж";
                    object newtxt40 = (object)дрп;
                    //object frwd = true;
                    object frwd40 = false;
                    doc.Content.Find.Execute(ref searchtxt40, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd40, ref missing, ref missing, ref newtxt40, ref wdrepl40, ref missing, ref missing,
                    ref missing, ref missing);


                    //ФИО начальника терр органа Директтр
                    string queryДиректтр = "select ФИО_РодПадеж from ФиоШев where id_шев = (select id_шев from Комитет) ";
                    string ФИО_РодПадеж = ТаблицаБД.GetTable(queryДиректтр, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();


                    object wdrepl41 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt41 = "Директтр";
                    object newtxt41 = (object)ФИО_РодПадеж;
                    //object frwd = true;
                    object frwd41 = false;
                    doc.Content.Find.Execute(ref searchtxt41, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd41, ref missing, ref missing, ref newtxt41, ref wdrepl41, ref missing, ref missing,
                    ref missing, ref missing);

                    //Основание osnovanie
                    //Основание Руководителя
                    string queryУстав = "select Основание from ФиоШев where id_шев = (select id_шев from Комитет) ";
                    string Основание = ТаблицаБД.GetTable(queryУстав, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();


                    object wdrepl42 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt42 = "osnovanie";
                    object newtxt42 = (object)Основание;
                    //object frwd = true;
                    object frwd42 = false;
                    doc.Content.Find.Execute(ref searchtxt42, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd42, ref missing, ref missing, ref newtxt42, ref wdrepl42, ref missing, ref missing,
                    ref missing, ref missing);

                    //Должность ГлавВрач DoljnostEsculap
                    string queryDoljnostEsculap = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string DoljnostEsculap = ТаблицаБД.GetTable(queryDoljnostEsculap, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl43 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt43 = "DoljnostEsculap";
                    object newtxt43 = (object)DoljnostEsculap;
                    //object frwd = true;
                    object frwd43 = false;
                    doc.Content.Find.Execute(ref searchtxt43, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd43, ref missing, ref missing, ref newtxt43, ref wdrepl43, ref missing, ref missing,
                    ref missing, ref missing);


                    //Должность передседателя на подписи Predsedatel
                    string queryДолжность = "select Должность from ФиоШев where id_шев = (select id_шев from Комитет)";
                    string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl44 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt44 = "Predsedatel";
                    object newtxt44 = (object)Predsedatel;
                    //object frwd = true;
                    object frwd44 = false;
                    doc.Content.Find.Execute(ref searchtxt44, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd44, ref missing, ref missing, ref newtxt44, ref wdrepl44, ref missing, ref missing,
                    ref missing, ref missing);

                    //Подпись ФИО пердседатель Fiopredsedatel
                    string queryFIO = "select ФИО_Руководитель from ФиоШев where id_шев = (select id_шев from Комитет)";
                    string Fiopredsedatel = ТаблицаБД.GetTable(queryFIO, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl45 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt45 = "Fio";
                    object newtxt45 = (object)Fiopredsedatel;
                    //object frwd = true;
                    object frwd45 = false;
                    doc.Content.Find.Execute(ref searchtxt45, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd45, ref missing, ref missing, ref newtxt45, ref wdrepl45, ref missing, ref missing,
                    ref missing, ref missing);


                    //откроем получившейся документ
                    app.Visible = true;

                    //doc.Close(ref falseValue, ref falseValue, ref falseValue);

                    ////откроем документ
                    //doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                    //ref missing, ref missing, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing, ref missing, ref missing,
                    //ref missing, ref missing, ref missing);

                    //app.Visible = true;

                }


            //Object saveChanges = Type.Missing;
            //Object originalFormat = Type.Missing;
            //Object routeDocument = Type.Missing;
            //doc.Close(ref saveChanges,
            // ref originalFormat, ref routeDocument);

            //this.TopMost = true;

        }

   
        private void button4_Click(object sender, EventArgs e)
        {
            //получим id поликлинники
            string query = "select id_поликлинника from Поликлинника";
            string sCon = ConnectionDB.ConnectionString();

            //получим id поликлинники
            id_поликлинники = Поликлинника.GetIdПоликлинники(query, sCon);

            //Receiver recever = new Receiver();
            //InsertДоговор insertДоговор = new InsertДоговор(this.label3.Text, this.IdЛьготнойКатегории, id_поликлинники, this.Id_Льготник);
            //recever.Action(insertДоговор);

            //получим id договора
            string queryId = "select id_договор from Договор where id_льготник = " + this.Id_Льготник + "";
            int id_договор = ДоговорTable.GetIdДоговор(queryId, sCon);

            Receiver recever = new Receiver();
            InsertДоговор insertДоговор = new InsertДоговор(this.label3.Text, this.IdЛьготнойКатегории, id_поликлинники, this.Id_Льготник, id_комитет);
            recever.Action(insertДоговор);


            //обновим таблицу с отображением договоров
            string queryДоговор = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения,ФлагНаличияАкта from Договор where id_льготник = " + this.Id_Льготник + " order by НомерДоговора desc";
            //this.gvДоговор.DataSource = ДанныеПредставление.GetПредставление(queryДоговор, "Договор");
            this.gvДоговор.DataSource = DisplayДоговор.ДанныеДоговор(queryДоговор, sCon, "Договор");
           

            this.gvДоговор.Columns["id_договор"].Visible = false;
            this.gvДоговор.Columns["ФлагНаличияДоговора"].Visible = false;
            this.gvДоговор.Columns["ФлагДопСоглашения"].Visible = false;
            this.gvДоговор.Columns["ФлагНаличияАкта"].Visible = false;
            
            
            //ФлагДопСоглашения

            //делаем не активной кнопку
            this.button4.Enabled = false;

            //сделаем не активной кнопки доп соглашений
            this.btnДопСоглашение.Enabled = false;
            this.btnДатаДоп.Enabled = false;

            //скинем всё в гриде доп соглашений
            this.gvДопСоглашение.DataSource = null;
        }

        private void gvДоговор_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //обнулим DataSet для GridView
            this.gvДопСоглашение.DataSource = null;

            //установим флаг печати доп соглашения в false
            this.flagPrintДопСоглашение = false;

            //узнаем содержимое текущего договора

            // Проверка пустого грида, клик по названию столбца
            try { if (this.gvДоговор.CurrentRow.Cells["id_договор"].Value != DBNull.Value) { } }
            catch { return; }

            if (this.gvДоговор.CurrentRow.Cells["id_договор"].Value != DBNull.Value)
            {
                id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                //узнаем закрыт ли акт
                //проверим есть ли у данного договора акт
                string queryFlag = "select ФлагНаличияАкта from Договор where id_договор = " + id_договор + " ";
                bool flagАкт = Convert.ToBoolean(ТаблицаБД.GetTable(queryFlag, ConnectionDB.ConnectionString(), "Договор").Rows[0][0]);
                
                //bool flagАкт = Convert.ToBoolean(this.gvДоговор.CurrentRow.Cells["ФлагНаличияАкта"].Value);
                if (flagАкт == true)
                {
                    //поставим флаг flagПодписьДопСоглашение в TRUE тем самым запретим создание нового доп соглашения
                    flagПодписьДопСоглашение = "True";
                    this.btnДопСоглашение.Enabled = false;
                }

                string queryУслуги = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество as 'Кол-во',НомерПоПеречню,Сумма from УслугиПоДоговору where id_договор = " + id_договор + " ";
                this.gvВидУслуг.DataSource = ДанныеПредставление.GetПредставление(queryУслуги, "УслугиПоДоговору");

                //this.gvВидУслуг.Columns["Количество"].Visible = false;
                this.gvВидУслуг.Columns["НомерПоПеречню"].Visible = false;
                this.gvВидУслуг.Columns["id_услугиДоговор"].Visible = false;
                this.gvВидУслуг.Columns["'Кол-во'"].Width = 50;

                //сохраним номер договора
                this.номерДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();

                //узнаем открыто ли доп соглашение
                string доп = this.gvДоговор.CurrentRow.Cells["ФлагДопСоглашения"].Value.ToString();
                if (доп == "True")
                {
                    this.btnДопСоглашение.Enabled = false;
                    this.btnДатаДоп.Enabled = true;
                }
                
                if (доп == "False")
                {
                    this.btnДопСоглашение.Enabled = true;
                    this.btnДатаДоп.Enabled = false;
                }
                

                //    try
                //    {
                //        //для List
                //        //сохраним дату договора 
                //        if (this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value != "")
                //        {
                //            this.датаДоговора = Convert.ToDateTime(this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value);
                //        }
                //    }
                //    catch
                //    {
                //        if (this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value != DBNull.Value)
                //        {
                //            this.датаДоговора = Convert.ToDateTime(this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value);
                //        }
                //    }

                //если договор НЕ ПОДПИСАН
                if (Convert.ToBoolean(this.gvДоговор.CurrentRow.Cells["ФлагНаличияДоговора"].Value) == false)
                {
                    this.btnАкт.Enabled = false;
                    //this.button2.Enabled = false;

                    //не дадим пользователою заключить доп соглашение
                    this.btnДопСоглашение.Enabled = false;
                    this.btnДатаДоп.Enabled = false;

                    //проверим есть ли доп соглашения у первого в гриде договора если есть то отобразим его в гриде
                    int id_договорCurrent = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                    //отобразим кнопки редактирования
                    this.btnAdd.Enabled = true;//*************
                    this.btnDelete.Enabled = true;//************

                    //откроем кнопку добавления даты
                    this.btnDate.Enabled = true;
                    
                    //откроем кнопку печать на договор
                    this.button2.Enabled = true;

                }
                else
                {
                    //если договор ПОДПИСАН
                    if (flagАкт == true)
                    {
                        this.btnАкт.Enabled = false;
                    }
                    else
                    {
                        this.btnАкт.Enabled = true;
                    }
                    //this.button2.Enabled = false;

                    //разрешим пользователю заключить доп соглашение
                   // this.btnДопСоглашение.Enabled = true;
                    this.btnДатаДоп.Enabled = true;

                    //скроем кнопки
                    this.btnAdd.Enabled = false; //****************
                    this.btnDelete.Enabled = false;//**************

                    //закроем кнопку добавления даты
                    this.btnDate.Enabled = false;

                    //скроем кнопку печать договора
                    this.button2.Enabled = false;

                    //проверим есть ли доп соглашения у первого в гриде договора если есть то отобразим его в гриде
                    int id_договорCurrent = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                    //проверим есть ли для данного договора доп соглашения 
                    string queryДопСоглашения = "select * from ДопСоглашение where id_договор = " + id_договор + " ";
                    DataTable tabНомерСоглашения = ТаблицаБД.GetTable(queryДопСоглашения, ConnectionDB.ConnectionString(), "ДопСоглашение");

                    //если есть доп соглашения
                    if (tabНомерСоглашения.Rows.Count != 0)
                    {
                        this.gvДопСоглашение.DataSource = tabНомерСоглашения;
                        this.gvДопСоглашение.Columns["id_договор"].Visible = false;
                        this.gvДопСоглашение.Columns["id_допСоглашение"].Visible = false;

                        //флаг наличия доп соглашения 
                        //this.flagДопСоглашение = true;
                        //this.flagPrintДопСоглашение = true;

                        //если доп соглашение открыто
                        if (flagПодписьДопСоглашение == "True")
                        {
                            //скроем кнопку 
                            this.btnАкт.Enabled = false;

                            //откроем кнопки редактирования услуг
                            this.btnAdd.Enabled = false;
                            this.btnDelete.Enabled = false;
                            this.btnДопСоглашение.Enabled = true;

                            //this.flagPrintДопСоглашение = true;

                            this.button2.Text = "Печать";
                        }
                        
                        //если доп соглашение закрыто
                        if (flagПодписьДопСоглашение == "False")
                        {
                            //скроем кнопку 
                            this.btnАкт.Enabled = true;

                            //откроем кнопки редактирования услуг
                            this.btnAdd.Enabled = true;
                            this.btnDelete.Enabled = true;
                            //this.btnДопСоглашение.Enabled = false;

                            //this.flagPrintДопСоглашение = false;

                            this.button2.Text = "Печать договора";
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //проверим есть ли по данному договору улусги
            string currentУслуги = "select count(НаименованиеУслуги) from УслугиПоДоговору where id_договор = "+ this.id_договор +" ";
            bool flagУслуги = УслугиДоговор.ValidateУслуги(currentУслуги);

            if (flagУслуги == true)
            {
                FormSetDate setDate = new FormSetDate();
                setDate.ShowDialog();


                if (setDate.DialogResult == DialogResult.OK)
                {
                    DateTime selectDate = setDate.DateTimeValue;

                    Receiver recivar = new Receiver();
                    UpdateДоговор updateДоговор = new UpdateДоговор(id_договор, selectDate, true);
                    recivar.Action(updateДоговор);

                    //Отобразми договоры для данного льготника
                    string query = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения,ФлагНаличияАкта from Договор where id_льготник = " + Id_Льготник + " order by НомерДоговора desc";
                    //this.gvДоговор.DataSource = ДанныеПредставление.GetПредставление(query, "Договор");

                    this.gvДоговор.DataSource = DisplayДоговор.ДанныеДоговор(query, ConnectionDB.ConnectionString(), "Договор");
                    this.gvДоговор.Columns["id_договор"].Visible = false;
                    this.gvДоговор.Columns["ФлагНаличияДоговора"].Visible = false;
                    this.gvДоговор.Columns["ФлагДопСоглашения"].Visible = false;
                    this.gvДоговор.Columns["ФлагНаличияАкта"].Visible = false;
                    
                    //ФлагДопСоглашения

                    this.btnAdd.Enabled = false;
                    this.btnDelete.Enabled = false;

                    this.button4.Enabled = true;

                    //скроем кнопку распечатать договор
                    this.button2.Enabled = false;
                    
                    //отобразим кнопку доп соглашений
                    this.btnДопСоглашение.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Услуги не выбраны");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //отобразим форму выбора вида услуг
            FormВидУслуги видУслуг = new FormВидУслуги();

            //получим id договора
            //int id
            if (id_договор != 0)
            {
                видУслуг.IdДоговор = id_договор;
            }
            else
            {
                //если пользователь сразу нажал кнопку добавить
                id_дог = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);
                видУслуг.IdДоговор = id_дог;

            }
            видУслуг.ShowDialog();

            if (видУслуг.DialogResult == DialogResult.OK)
            {
                if (id_договор != 0)
                {
                    queryУслугиF = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество as 'Кол-во',НомерПоПеречню,Сумма from УслугиПоДоговору where id_договор = " + id_договор + " ";
                }
                else
                {
                    queryУслугиF = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество  as 'Кол-во',НомерПоПеречню,Сумма from УслугиПоДоговору where id_договор = " + id_дог + " ";
                }

                this.gvВидУслуг.DataSource = ДанныеПредставление.GetПредставление(queryУслугиF, "УслугиПоДоговору");

                this.gvВидУслуг.Columns["НомерПоПеречню"].Visible = false;
                //this.gvВидУслуг.Columns["Количество"].Visible = false;
                this.gvВидУслуг.Columns["'Кол-во'"].Width = 50;

                string queryДоговор = "select id_договор,НомерДоговора,ДатаДоговора,ФлагНаличияДоговора,ФлагДопСоглашения from Договор where id_льготник = " + this.Id_Льготник + " order by НомерДоговора desc";
                //this.gvДоговор.DataSource = ДанныеПредставление.GetПредставление(queryДоговор, "Договор");
                //this.gvДоговор.DataSource = ДанныеПредставление.GetПредставление(queryДоговор, "Договор");
                this.gvДоговор.DataSource = DisplayДоговор.ДанныеДоговор(queryДоговор, ConnectionDB.ConnectionString(), "Договор");
                this.gvДоговор.Columns["id_договор"].Visible = false;
                this.gvДоговор.Columns["ФлагНаличияДоговора"].Visible = false;
                this.gvДоговор.Columns["ФлагДопСоглашения"].Visible = false;

                

            }
        }

        private void gvВидУслуг_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.gvВидУслуг.CurrentRow.Cells["id_услугиДоговор"].Value != DBNull.Value)
            {
                id_услуга = Convert.ToInt32(this.gvВидУслуг.CurrentRow.Cells["id_услугиДоговор"].Value);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Receiver reciver = new Receiver();
            DeleteУслугиПоДоговору услуга = new DeleteУслугиПоДоговору(id_услуга);
            reciver.Action(услуга);

            //старая реализация
            //string queryУслуги = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество,ФлагНаличияДоговора from УслугиПоДоговору where id_договор = " + this.id_договор + " ";
            //string queryУслуги = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество from УслугиПоДоговору where id_договор = " + this.id_договор + " ";
            //this.gvВидУслуг.DataSource = ДанныеПредставление.GetПредставление(queryУслуги, "УслугиПоДоговору");
            
            ////this.gvДоговор.DataSource = DisplayДоговор.ДанныеДоговор(queryУслуги, ConnectionDB.ConnectionString(), "Договор");
            //this.gvВидУслуг.Columns["Количество"].Visible = false;
            //this.gvВидУслуг.Columns["id_услугиДоговор"].Visible = false;

            this.id_дог = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

            string delQuery = "select id_услугиДоговор,НаименованиеУслуги,Цена,Количество  as 'Кол-во',НомерПоПеречню,Сумма from УслугиПоДоговору where id_договор = " + id_дог + " ";
            this.gvВидУслуг.DataSource = ДанныеПредставление.GetПредставление(delQuery, "УслугиПоДоговору");

            this.gvВидУслуг.Columns["НомерПоПеречню"].Visible = false;
                //this.gvВидУслуг.Columns["Количество"].Visible = false;
            this.gvВидУслуг.Columns["'Кол-во'"].Width = 50;

            

        }

        private void btnАкт_Click(object sender, EventArgs e)
        {
            //распечатаем акт
            PrintAct();
        }

 
        private void btnДопСоглашение_Click(object sender, EventArgs e)
        {
            /*
             * В таблице Договор поле ФлагДопСоглашения = False - доп соглашение есть и оно закрыто
             * если
             * В таблице Договор поле ФлагДопСоглашения = True - доп соглашение есть и оно не закрыто 
             */


            //строка подключения
            string sCon = ConnectionDB.ConnectionString();

            int id_договор;
            try
            {
                //получим id договора
                id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Токого договора не сушествует");
                return;
            }

            //проверим есть ли у данного договора акт
            string queryFlag = "select ФлагНаличияАкта from Договор where id_договор = " + id_договор + " ";
            bool flagАкт = Convert.ToBoolean(ТаблицаБД.GetTable(queryFlag, ConnectionDB.ConnectionString(), "Договор").Rows[0][0]);

            if (flagАкт == false)
            {
                string номерДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();

                //Получим услуги по договору
                string queryУсулгиДоговор = "select * from УслугиПоДоговору where id_договор = " + id_договор + " ";
                DataTable tabУслДог = ТаблицаБД.GetTable(queryУсулгиДоговор, sCon, "УслугиПоДоговору");

                int id_услугиДоговор = Convert.ToInt32(tabУслДог.Rows[0][0]);

                //получим номер доп соглашения
                //string queryНомер = "select НомерДопСоглашения from ДопСоглашение where id_услугиДоговор = " + id_услугиДоговор + " order by  НомерДопСоглашения desc";
                string queryНомер = "select Count(НомерДопСоглашения) from ДопСоглашение where id_договор = " + id_договор + " ";// order by  НомерДопСоглашения desc";
                DataTable tabНомерСоглашения = ТаблицаБД.GetTable(queryНомер, sCon, "ДопСоглашение");

                string номДоп = string.Empty;
                if (tabНомерСоглашения.Rows.Count != 0)
                {
                    DataRow row = tabНомерСоглашения.Rows[0];

                    //получим порядковый номер доп соглашения
                    номДоп = Convert.ToString(Convert.ToInt32(row[0]) + 1);
                }
                else
                {
                    номДоп = "1";
                }


                //получим номер доп соглашения
                string номерДопСоглашения = номерДоговора + "/" + номДоп;
                int id_услугиПоДоговору = Convert.ToInt32(tabУслДог.Rows[0][0]);

                //заполним таблицу доп соглашений
                //string queryInsert = "insert into ДопСоглашение(id_услугиДоговор,НомерДопСоглашения)values(" + id_услугиПоДоговору + ",'" + номерДопСоглашения + "')";
                string queryInsert = "insert into ДопСоглашение(id_договор,НомерДопСоглашения)values(" + id_договор + ",'" + номерДопСоглашения + "')";
                Query.Execute(queryInsert, sCon);


                //Обновим содержимое DataGridView
                string queryNum = "select id_допСоглашение,id_договор,НомерДопСоглашения,Дата from ДопСоглашение where id_договор = " + id_договор + " order by  НомерДопСоглашения desc";
                DataTable tabNumСоглашения = ТаблицаБД.GetTable(queryNum, sCon, "ДопСоглашение");

                this.gvДопСоглашение.DataSource = tabNumСоглашения;

                this.gvДопСоглашение.Columns["id_допСоглашение"].Visible = false;
                this.gvДопСоглашение.Columns["id_договор"].Visible = false;

                //установим флаг в таблицу Договор
                string insertДоговор = "update Договор set ФлагДопСоглашения = 'True' where id_договор = " + id_договор + " ";
                Query.Execute(insertДоговор, sCon);

                //закроем кнопку
                this.btnДопСоглашение.Enabled = false;
            }
            else
            {
                MessageBox.Show("Невозможно заключить доп. соглашение. Так как договор уже закрыт.");
            }
        }

  

        private void gvДопСоглашение_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (this.gvДопСоглашение.CurrentRow.Cells["id_допСоглашение"].Value != DBNull.Value)
            {
                int id_допСоглашения = Convert.ToInt32(this.gvДопСоглашение.CurrentRow.Cells["id_допСоглашение"].Value);

                //получим id договора 
                int id_договор = Convert.ToInt32(this.gvДопСоглашение.CurrentRow.Cells["id_договор"].Value);

                //если id есть
                //if (id_договор != null)
                if (this.gvДопСоглашение.CurrentRow.Cells["id_договор"].Value != null)
                {
                    this.button2.Text = "Печать";

                    this.flagPrintДопСоглашение = true;

                    номерДопСоглашения = this.gvДопСоглашение.CurrentRow.Cells["НомерДопСоглашения"].Value.ToString();

                    string queryOpenДопСоглашение = "select ФлагДопСоглашения from Договор where id_договор = " + id_договор + " ";
                    DataTable tabFlag = ТаблицаБД.GetTable(queryOpenДопСоглашение, ConnectionDB.ConnectionString(), "Договор");

                    //узнаем доп соглашение открыто или закрыто или его вообще нет
                    string flagOpen = tabFlag.Rows[0]["ФлагДопСоглашения"].ToString();

                    //доп соглашение закрыто 
                    if (flagOpen == "False")// || flagOpen == "")
                    {
                        btnAdd.Enabled = false;
                        btnDelete.Enabled = false;

                        //скроем кнопку печати доп соглашения
                        this.button2.Enabled = false;

                        //укажем что у договора есть допсоглашение
                        flagАктДопСоглашение = true;
                    }

                    //если доп соглашение открыто
                    if (flagOpen == "True")
                    {
                        btnAdd.Enabled = true;
                        btnDelete.Enabled = true;

                        this.btnДатаДоп.Enabled = true;
                        //укажем что у договора есть допс соглашение
                        //flagАктДопСоглашение = true;

                        //установим флаг flagАктДопСоглашение в true
                        this.flagАктДопСоглашение = true;

                        //откроем кнопку печати допсоглашения
                        this.button2.Enabled = true;
                    }
                }
                else
                {
                    this.button2.Text = "Печать договора";
                    this.flagPrintДопСоглашение = false;
                }
            }
        }

        private void btnДатаДоп_Click(object sender, EventArgs e)
        {
            //if (gvДопСоглашение.RowCount == 0)
            //    return;
            int id_допСоглашение;
            try
            {
                id_допСоглашение = Convert.ToInt32(this.gvДопСоглашение.CurrentRow.Cells["id_допСоглашение"].Value);
                int id_договор = Convert.ToInt32(this.gvДопСоглашение.CurrentRow.Cells["id_договор"].Value);
            }
            catch /*(Exception ex)*/
            {
                //MessageBox.Show("Создайте дополнительное соглашение.", "Ошибка");
                //MessageBox.Show("Создайте дополнительное соглашение. \n\n" + ex.Message, "Ошибка");
                return;
            }
            
            //int id_допСоглашение = Convert.ToInt32(this.gvДопСоглашение.CurrentRow.Cells["id_допСоглашение"].Value);
            //int id_договор = Convert.ToInt32(this.gvДопСоглашение.CurrentRow.Cells["id_договор"].Value);

            FormSetDate setDate = new FormSetDate();
            setDate.ShowDialog();

            if (setDate.DialogResult == DialogResult.OK)
            {
                //запишем дату в таблицу ДопСоглашение и False в таблицу Договор
                //string updateДопСоглашение = "update ДопСоглашение set Дата = '" + DateTime.Today.ToShortDateString() + "' where id_допСоглашение = " + id_допСоглашение + " ";
                string updateДопСоглашение = "update ДопСоглашение set Дата = '" + setDate.DateTimeValue.ToShortDateString() + "' where id_допСоглашение = " + id_допСоглашение + " ";
                Query.Execute(updateДопСоглашение, ConnectionDB.ConnectionString());

                string updateДоговор = "update Договор  set ФлагДопСоглашения = 'False' where id_договор = " + id_договор + " ";
                Query.Execute(updateДоговор, ConnectionDB.ConnectionString());

                //обновим содержимое gvДопСоглашения
                //проверим есть ли доп соглашения у первого в гриде договора если есть то отобразим его в гриде
                //int id_договорCurrent = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                //проверим есть ли для данного договора доп соглашения 
                string queryДопСоглашения = "select * from ДопСоглашение where id_договор = " + id_договор + " ";
                DataTable tabНомерСоглашения = ТаблицаБД.GetTable(queryДопСоглашения, ConnectionDB.ConnectionString(), "ДопСоглашение");

                this.gvДопСоглашение.DataSource = tabНомерСоглашения;
                this.gvДопСоглашение.Columns["id_допСоглашение"].Visible = false;
                this.gvДопСоглашение.Columns["id_договор"].Visible = false;

                //скроем кнопки редактирваония улуг
                this.btnDelete.Enabled = false;
                this.btnAdd.Enabled = false;

                //скроем кнопку распечатать доп соглашение
                this.button2.Enabled = false;
            }

        }

        private void PrintAct()
        {
            bool iTest = this.flagАктДопСоглашение;

            //хранит переменную дату акта
            string датаАкта = string.Empty;

            #region кликнули по договору
            //если по гриду договор кликнули
            if (this.flagАктДопСоглашение == false)
            {

                FormSetDate setDate = new FormSetDate();
                setDate.ShowDialog();

                if (setDate.DialogResult == DialogResult.OK)
                {
                    //проверим подписан ли договор
                    int id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                    ////получим номер договора
                    string нумДог = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString().Trim();

                    //проверим есть ли у договора акт
                    string queryValidate = "select ФлагНаличияАкта from Договор where id_договор = " + id_договор + " ";
                    bool flagДоговорValidate = Convert.ToBoolean(ТаблицаБД.GetTable(queryValidate, ConnectionDB.ConnectionString(), "Договор").Rows[0][0]);

                    if (flagДоговорValidate == false)
                    {
                        //установим в таблице договор флаг о том что в договоре распечатан акт
                        string updateДоговор = "update Договор " +
                                                "set ФлагНаличияАкта = True " +
                                               "where id_договор = " + id_договор + " ";

                        //выполним обновление таблицы Договор
                        Query.Execute(updateДоговор, ConnectionDB.ConnectionString());

                        //получим порядковый номер акта
                        queryNum = "select Count(НомерАкта) +1 from АктВыполнненныхРабот";

                        DataTable dtNum = ТаблицаБД.GetTable(queryNum, ConnectionDB.ConnectionString(), "АткВыполненныхРабот");
                        порядковыйНомер = dtNum.Rows[0][0].ToString();

                        номер = нумДог + "/" + порядковыйНомер;

                        string queryInsertAct = "insert into АктВыполнненныхРабот(НомерАкта,id_договор,ФлагПодписания,ДатаПодписания)values('" + номер + "'," + id_договор + ",'True','" + setDate.DateTimeValue.ToShortDateString() + "')";
                        Query.Execute(queryInsertAct, ConnectionDB.ConnectionString());

                        //установим дату для отображения в акте 
                        датаАкта = setDate.DateTimeValue.ToShortDateString();

                    }
                    else
                    {
                        //получим порядковый номер акта
                        queryNum = "select НомерАкта,ДатаПодписания from АктВыполнненныхРабот where id_договор = " + id_договор + " ";

                        DataTable dtNum = ТаблицаБД.GetTable(queryNum, ConnectionDB.ConnectionString(), "АткВыполненныхРабот");
                        порядковыйНомер = dtNum.Rows[0]["НомерАкта"].ToString();

                        номер = порядковыйНомер;
                        датаАкта = Convert.ToDateTime(dtNum.Rows[0]["ДатаПодписания"]).ToShortDateString();
                        
                    }

                    //список для хранения данных для таблицы в акте
                    List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();
                    //заполгним первую строку

                    ТаблицаДоговор шапка = new ТаблицаДоговор();
                    шапка.ПорядковыНомер = "№ п/п";
                    шапка.НомерУслуги = "№ усл в справ";
                    шапка.Наименование = "Наименование";
                    шапка.Цена = "Цена, руб";
                    шапка.Количество = "Кол";
                    шапка.Стоимость = "Стоимость";
                    list.Add(шапка);

                    //получим данные для таблицы
                    string selectQuery = "select * from УслугиПоДоговору where id_договор = " + id_договор + " ";
                    DataTable улугиДоговор = ТаблицаБД.GetTable(selectQuery, ConnectionDB.ConnectionString(), "УслугиПоДоговору");

                    //установим счётчик
                    int iCount = 1;

                    //счётчик суммы итого
                    decimal суммИтого = 0m;

                    //заполним коллекцию классов для таблицы
                    foreach (DataRow r in улугиДоговор.Rows)
                    {
                        ТаблицаДоговор str = new ТаблицаДоговор();
                        str.ПорядковыНомер = iCount.ToString();

                        str.НомерУслуги = r["НомерПоПеречню"].ToString().Replace(',', '.');
                        str.Наименование = r["НаименованиеУслуги"].ToString();

                        str.Цена = r["Цена"].ToString();
                        str.Количество = r["Количество"].ToString();

                        str.Стоимость = r["Сумма"].ToString();
                        list.Add(str);

                        //if (i != 1)
                        //{
                        суммИтого = Math.Round(суммИтого + Convert.ToDecimal(str.Стоимость), 2);
                        //}

                        //увеличем на 1
                        iCount++;
                    }

                    //создадим строку итого 
                    ТаблицаДоговор итого = new ТаблицаДоговор();
                    итого.НомерУслуги = "Итого:";
                    итого.Стоимость = суммИтого.ToString("c");

                    list.Add(итого);

                    //создадим документ WORD
                    string fName = this.ФИО_Льготника;

                    //распечатаем word
                    //FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\Акт4.dot");
                    // FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\"+fileName+".doc");
                    //fnDel.Delete();

                    //Скопируем шаблон в папку Документы
                    try
                    {
                        FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Акт4.doc");
                        fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Документ уже открыт.\n"  + e.Message ,"Ошибка");
                        return;
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


                    ////Номер договора
                    object wdrepl = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt = "номердоговора";
                    //object newtxt = (object)номерДоговора;
                    object newtxt = (object)нумДог;
                    //object frwd = true;
                    object frwd = false;
                    doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                    ref missing, ref missing);

                    //получим номер акта
                    string номДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();

                    string номерАкта = номер;


                    ////Номер договора
                    object wdrepl2 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt2 = "номеракта";
                    object newtxt2 = (object)номерАкта;
                    //object frwd = true;
                    object frwd2 = false;
                    doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                    ref missing, ref missing);

                    string qHosp = "select * from Поликлинника";
                    DataTable tabHosp = ТаблицаБД.GetTable(qHosp, ConnectionDB.ConnectionString(), "Поликлинника");
                    DataRow rH = tabHosp.Rows[0];

                    //присвоим наименование поликлинники
                    string исполнитель = rH["НаименованиеПоликлинники"].ToString();

                    //вычислим потребителя
                    string потребитель = this.ФИО_Льготника;


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
                    string датаДоговора = this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value.ToString();

                    //Номер договора
                    object wdrepl5 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt5 = "датадоговора";
                    object newtxt5 = (object)датаДоговора;
                    //object frwd = true;
                    object frwd5 = false;
                    doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                    ref missing, ref missing);

                    //выведим дату договора
                    //string датаАкта = setDate.DateTimeValue.ToShortDateString();
                    //string датаАкта = 


                    //Номер договора
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
                    object newtxt7 = (object)фиоВрача;
                    //object frwd = true;
                    object frwd7 = false;
                    doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
                    ref missing, ref missing);

                    string summa = Валюта.Рубли.Пропись(суммИтого);

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


                    Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                    table.Range.ParagraphFormat.SpaceAfter = 6;

                    //выставим ширину столбцов
                    table.Columns[1].Width = 40;
                    table.Columns[2].Width = 50;
                    table.Columns[3].Width = 250;
                    table.Columns[4].Width = 60;
                    table.Columns[5].Width = 40;
                    table.Columns[6].Width = 70;
                    table.Borders.Enable = 1; // Рамка - сплошная линия
                    table.Range.Font.Name = "Times New Roman";
                    table.Range.Font.Size = 10;
                    //счётчик строк
                    int i = 1;

                    //запишем данные в таблицу
                    foreach (ТаблицаДоговор item in list)
                    {
                        table.Cell(i, 1).Range.Text = item.ПорядковыНомер;
                        table.Cell(i, 2).Range.Text = item.НомерУслуги;

                        table.Cell(i, 3).Range.Text = item.Наименование;
                        table.Cell(i, 4).Range.Text = item.Цена;

                        table.Cell(i, 5).Range.Text = item.Количество;
                        table.Cell(i, 6).Range.Text = item.Стоимость;

                        //doc.Words.Count.ToString();
                        Object beforeRow1 = Type.Missing;
                        table.Rows.Add(ref beforeRow1);

                        i++;
                    }
                    table.Rows[i].Delete();

                    //Должность на подписи
                    string queryДолжность = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl9 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt9 = "Predsedatel";
                    object newtxt9 = (object)Predsedatel;
                    //object frwd = true;
                    object frwd9 = false;
                    doc.Content.Find.Execute(ref searchtxt9, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd9, ref missing, ref missing, ref newtxt9, ref wdrepl9, ref missing, ref missing,
                    ref missing, ref missing);


                    app.Visible = true;
                }//конец выбора даты
            }
            #endregion

            if (this.flagАктДопСоглашение == true)
            {
                ////закроем актом договор из под доп соглашения

                //                FormSetDate setDate = new FormSetDate();
                //setDate.ShowDialog();

                //if (setDate.DialogResult == DialogResult.OK)
                //{
                //    //проверим подписан ли договор
                //    int id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);


                //}

                  FormSetDate setDate = new FormSetDate();
                  setDate.ShowDialog();

                if (setDate.DialogResult == DialogResult.OK)
                {
                    //проверим подписан ли договор
                    int id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value);

                    ////получим номер договора
                    string нумДог = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString().Trim();

                    //проверим есть ли у договора акт
                    string queryValidate = "select ФлагНаличияАкта from Договор where id_договор = " + id_договор + " ";
                    bool flagДоговорValidate = Convert.ToBoolean(ТаблицаБД.GetTable(queryValidate, ConnectionDB.ConnectionString(), "Договор").Rows[0][0]);

                    if (flagДоговорValidate == false)
                    {
                        //установим в таблице договор флаг о том что в договоре распечатан акт
                        string updateДоговор = "update Договор " +
                                                "set ФлагНаличияАкта = True " +
                                               "where id_договор = " + id_договор + " ";

                        //выполним обновление таблицы Договор
                        Query.Execute(updateДоговор, ConnectionDB.ConnectionString());

                        //получим порядковый номер акта
                        queryNum = "select Count(НомерАкта) +1 from АктВыполнненныхРабот";

                        DataTable dtNum = ТаблицаБД.GetTable(queryNum, ConnectionDB.ConnectionString(), "АткВыполненныхРабот");
                        порядковыйНомер = dtNum.Rows[0][0].ToString();

                        номер = нумДог + "/" + порядковыйНомер;

                        string queryInsertAct = "insert into АктВыполнненныхРабот(НомерАкта,id_договор,ФлагПодписания,ДатаПодписания)values('" + номер + "'," + id_договор + ",'True','" + setDate.DateTimeValue.ToShortDateString() + "')";
                        Query.Execute(queryInsertAct, ConnectionDB.ConnectionString());

                    }
                    else
                    {
                        //получим порядковый номер акта
                        queryNum = "select НомерАкта from АктВыполнненныхРабот where id_договор = " + id_договор + " ";

                        DataTable dtNum = ТаблицаБД.GetTable(queryNum, ConnectionDB.ConnectionString(), "АткВыполненныхРабот");
                        порядковыйНомер = dtNum.Rows[0][0].ToString();

                        номер = порядковыйНомер;
                    }

                    //список для хранения данных для таблицы в акте
                    List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();
                    //заполгним первую строку

                    ТаблицаДоговор шапка = new ТаблицаДоговор();
                    шапка.ПорядковыНомер = "№ п/п";
                    шапка.НомерУслуги = "№ усл в справ";
                    шапка.Наименование = "Наименование";
                    шапка.Цена = "Цена, руб";
                    шапка.Количество = "Кол";
                    шапка.Стоимость = "Стоимость";
                    list.Add(шапка);

                    //получим данные для таблицы
                    string selectQuery = "select * from УслугиПоДоговору where id_договор = " + id_договор + " ";
                    DataTable улугиДоговор = ТаблицаБД.GetTable(selectQuery, ConnectionDB.ConnectionString(), "УслугиПоДоговору");

                    //установим счётчик
                    int iCount = 1;

                    //счётчик суммы итого
                    decimal суммИтого = 0m;

                    //заполним коллекцию классов для таблицы
                    foreach (DataRow r in улугиДоговор.Rows)
                    {
                        ТаблицаДоговор str = new ТаблицаДоговор();
                        str.ПорядковыНомер = iCount.ToString();

                        str.НомерУслуги = r["НомерПоПеречню"].ToString().Replace(',', '.');
                        str.Наименование = r["НаименованиеУслуги"].ToString();

                        str.Цена = r["Цена"].ToString();
                        str.Количество = r["Количество"].ToString();

                        str.Стоимость = r["Сумма"].ToString();
                        list.Add(str);

                        //if (i != 1)
                        //{
                        суммИтого = Math.Round(суммИтого + Convert.ToDecimal(str.Стоимость), 2);
                        //}

                        //увеличем на 1
                        iCount++;
                    }

                    //создадим строку итого 
                    ТаблицаДоговор итого = new ТаблицаДоговор();
                    итого.НомерУслуги = "Итого:";
                    итого.Стоимость = суммИтого.ToString("c");

                    list.Add(итого);

                    //создадим документ WORD
                    string fName = this.ФИО_Льготника;

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
                    //object newtxt = (object)номерДоговора; //нумДог
                    object newtxt = (object)нумДог;
                    //object frwd = true;
                    object frwd = false;
                    doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                    ref missing, ref missing);

                    //получим номер акта
                    string номДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();

                    string номерАкта = номер;


                    ////Номер договора
                    object wdrepl2 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt2 = "номеракта";
                    object newtxt2 = (object)номерАкта;
                    //object frwd = true;
                    object frwd2 = false;
                    doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                    ref missing, ref missing);

                    string qHosp = "select * from Поликлинника";
                    DataTable tabHosp = ТаблицаБД.GetTable(qHosp, ConnectionDB.ConnectionString(), "Поликлинника");
                    DataRow rH = tabHosp.Rows[0];

                    //присвоим наименование поликлинники
                    string исполнитель = rH["НаименованиеПоликлинники"].ToString();

                    //вычислим потребителя
                    string потребитель = this.ФИО_Льготника;


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
                    string датаДоговора = this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value.ToString();

                    //Номер договора
                    object wdrepl5 = WdReplace.wdReplaceAll;
                    //object searchtxt = "GreetingLine";
                    object searchtxt5 = "датадоговора";
                    object newtxt5 = (object)датаДоговора;
                    //object frwd = true;
                    object frwd5 = false;
                    doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                    ref missing, ref missing);

                    //выведим дату договора
                    //string датаАкта = setDate.DateTimeValue.ToShortDateString();
                    датаАкта = setDate.DateTimeValue.ToShortDateString();

                    //Номер договора
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
                    object newtxt7 = (object)фиоВрача;
                    //object frwd = true;
                    object frwd7 = false;
                    doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
                    ref missing, ref missing);

                    string summa = Валюта.Рубли.Пропись(суммИтого);

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


                    Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                    table.Range.ParagraphFormat.SpaceAfter = 6;

                    //выставим ширину столбцов
                    table.Columns[1].Width = 40;
                    table.Columns[2].Width = 50;
                    table.Columns[3].Width = 250;
                    table.Columns[4].Width = 60;
                    table.Columns[5].Width = 40;
                    table.Columns[6].Width = 70;
                    table.Borders.Enable = 1; // Рамка - сплошная линия
                    table.Range.Font.Name = "Times New Roman";
                    table.Range.Font.Size = 10;
                    //счётчик строк
                    int i = 1;

                    //запишем данные в таблицу
                    foreach (ТаблицаДоговор item in list)
                    {
                        table.Cell(i, 1).Range.Text = item.ПорядковыНомер;
                        table.Cell(i, 2).Range.Text = item.НомерУслуги;

                        table.Cell(i, 3).Range.Text = item.Наименование;
                        table.Cell(i, 4).Range.Text = item.Цена;

                        table.Cell(i, 5).Range.Text = item.Количество;
                        table.Cell(i, 6).Range.Text = item.Стоимость;

                        //doc.Words.Count.ToString();
                        Object beforeRow1 = Type.Missing;
                        table.Rows.Add(ref beforeRow1);

                        i++;
                    }

                    table.Rows[i].Delete();

                    //Должность на подписи
                    string queryДолжность = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                    string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                    object wdrepl9 = WdReplace.wdReplaceAll;//39
                    //object searchtxt = "GreetingLine";
                    object searchtxt9 = "Predsedatel";
                    object newtxt9 = (object)Predsedatel;
                    //object frwd = true;
                    object frwd9 = false;
                    doc.Content.Find.Execute(ref searchtxt9, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd9, ref missing, ref missing, ref newtxt9, ref wdrepl9, ref missing, ref missing,
                    ref missing, ref missing);

                    app.Visible = true;

                }
            }
        }

        private void btnТехническийЛист_Click(object sender, EventArgs e)
        {

            //Получим данные для содержащие вид работ
            List<ТаблицаДоговор> listТех = new List<ТаблицаДоговор>();

            //подсчитаем сумму выполненных работ в тех листе
            decimal суммаРабот = 0m;

            int iCountRow = this.gvВидУслуг.Rows.Count-1;

            int i = 0;

            foreach (DataGridViewRow row in this.gvВидУслуг.Rows)
            {
                if (i != iCountRow)
                {
                    ТаблицаДоговор item = new ТаблицаДоговор();
                    item.НомерУслуги = row.Cells["НомерПоПеречню"].Value.ToString();

                    item.Наименование = row.Cells["НаименованиеУслуги"].Value.ToString();
                    item.Количество = row.Cells["'Кол-во'"].Value.ToString();

                    //item.Стоимость = row.Cells["Сумма"].Value.ToString();
                    item.Цена = Convert.ToDecimal(row.Cells["Цена"].Value).ToString("c");

                    суммаРабот = Math.Round(суммаРабот + Convert.ToDecimal(row.Cells["Сумма"].Value), 2);

                    item.Стоимость = Convert.ToDecimal(Math.Round(Convert.ToDecimal(row.Cells["Цена"].Value) * Convert.ToInt32(row.Cells["'Кол-во'"].Value), 2)).ToString("c");

                    listТех.Add(item);
                }

                i++;
            }

            List<ТаблицаДоговор> lTest = listТех;

            FormPrintТехЛист fpint = new FormPrintТехЛист();

            //передадим в форму услуги по текущему договору
            fpint.УслугиДоговора = listТех;

            //передадим в форму ФИО льготника и его ID
            fpint.ФИО_Льготника = this.ФИО_Льготника;
            fpint.Id_Льготник = this.Id_Льготник;

            fpint.Show();
            
            
           // //Распечатаем технический лист
           // string fName = "Технический лист " + this.ФИО_Льготника;


           // try
           // {
           //     //Скопируем шаблон в папку Документы
           //     FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ТехЛист.doc");
           //     fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
           // }
           // catch
           // {
           //     MessageBox.Show("Возможно у вас уже открыт договор с этим льготником. Закройте этот договор.");
           // }

           // string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";

           // //Создаём новый Word.Application
           // Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

           // //Загружаем документ
           // Microsoft.Office.Interop.Word.Document doc = null;

           // object fileName = filName;
           // object falseValue = false;
           // object trueValue = true;
           // object missing = Type.Missing;

           // doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
           // ref missing, ref missing, ref missing, ref missing, ref missing,
           // ref missing, ref missing, ref missing, ref missing, ref missing,
           // ref missing, ref missing, ref missing);

           // object wdrepl = WdReplace.wdReplaceAll;
           // //object searchtxt = "GreetingLine";
           // object searchtxt = "Льготник";
           // object newtxt = (object)this.ФИО_Льготника;
           // //object frwd = true;
           // object frwd = false;
           // doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
           // ref missing, ref missing);

           // //узнаем адрес
           // string queryАдрес = "select * from Льготник where id_льготник = "+ this.Id_Льготник +" ";
           // DataRow rowЛьготник = ТаблицаБД.GetTable(queryАдрес, ConnectionDB.ConnectionString(), "Льготник").Rows[0];

           // //получим название района 
           // string queryР = "select РайонОбласти from НаименованиеРайона where id_район in (select id_район from Льготник where id_льготник = " + this.Id_Льготник + " )";
           // DataTable tab = ДанныеПредставление.GetПредставление(queryР, "Льготник");

           // //переменная хранит название района
           // string названиеРайона = string.Empty;

           // if (tab.Rows.Count != 0)
           // {
           //     //отобразим район который прописан в БД
           //     названиеРайона = tab.Rows[0][0].ToString();
           // }

           // //получим название населённого пункта
           // string queryН = "select Наименование from НаселенныйПункт where id_насПункт in (select id_насПункт from Льготник where id_льготник = " + this.Id_Льготник + " )";
           // DataTable tabН = ДанныеПредставление.GetПредставление(queryН, "Льготник");

           // //переменная хранит название населённого пункта
           // string населённыйПункт = string.Empty;

           // if (tabН.Rows.Count != 0)
           // {
           //     //отобразим населённый пункет в котором прописан текущий льготник
           //     населённыйПункт = tabН.Rows[0][0].ToString();
           // }

           // //если населённый пункт = Саратов 
           // if (Regex.IsMatch(населённыйПункт, "Саратов") == true)
           // {
           //     названиеРайона = "";
           // }



           // //улица
           // string улица = rowЛьготник["улица"].ToString();
           // string улицаPrint = string.Empty;

           // if (улица != "")
           // {
           //     улицаPrint = " ул " + улица;
           // }

           // //номер дома
           // string numHous = rowЛьготник["НомерДома"].ToString();
           // string numHousPrint = string.Empty;

           // if (numHous != "")
           // {
           //     numHousPrint = " д " + numHous;
           // }


           // //номер корпуса
           // string numSubHous = rowЛьготник["корпус"].ToString();
           // string numSubHousPrint = string.Empty;

           // if (numSubHous != "")
           // {
           //     numSubHousPrint = " корп. " + numSubHous;
           // }

           // //номер кв
           // string numEpartment = rowЛьготник["НомерКвартиры"].ToString();
           // string numEpartmentPrint = string.Empty;

           // if (numEpartment != "")
           // {
           //     numEpartmentPrint = " кв. " + numEpartment;
           // }

           ////полный адрес
           // //string адрес = улица + " " + numHous + " "  + numSubHous + " " + numEpartment;
           // string адрес = названиеРайона + " " + населённыйПункт + " " +  улицаPrint + numHousPrint + numSubHousPrint + numEpartmentPrint;

           // object wdrepl2 = WdReplace.wdReplaceAll;
           // //object searchtxt = "GreetingLine";
           // object searchtxt2 = "местопроживания";
           // object newtxt2 = (object)адрес;
           // //object frwd = true;
           // object frwd2 = false;
           // doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
           // ref missing, ref missing);

           // //вычислим количество полных лет
           // //получим дату рождения
           // DateTime др = Convert.ToDateTime(rowЛьготник["ДатаРождения"]);

           // //год рождения
           // int yearR = др.Year;
           // //int yearR = 1973;

           // //месяц рождения 
           // int montchR = др.Month;
           // //int montchR = 09;

           // //текущую дату
           // DateTime data = DateTime.Today;

           // // текущий год
           // int yearT = data.Year;

           // //текущий месяц
           // int montchT = data.Month;

           // int rez = (montchT > montchR) ? (yearT - yearR) : (yearT == yearR) ? 0 : (yearT - yearR - 1);
           // string возрост = rez.ToString();

           // object wdrepl3 = WdReplace.wdReplaceAll;
           // //object searchtxt = "GreetingLine";
           // object searchtxt3 = "скольколет";
           // object newtxt3 = (object)возрост;
           // //object frwd = true;
           // object frwd3 = false;
           // doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
           // ref missing, ref missing);

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

           // //Вставить таблицу
           // object bookNaziv = "таблица";
           // Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

           // object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
           // object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;

           // Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 4, ref behavior, ref autobehavior);
           // table.Range.ParagraphFormat.SpaceAfter = 3;

           // table.Columns[1].Width = 50;
           // table.Columns[2].Width = 340;
           // table.Columns[3].Width = 35;
           // table.Columns[4].Width = 60;

           // table.Borders.Enable = 1; // Рамка - сплошная линия
           // table.Range.Font.Name = "Times New Roman";
           // table.Range.Font.Size = 8;

           // //счётчик строк
           // int ic = 1;

           // //запишем данные в таблицу
           // foreach (ТаблицаДоговор item in list)
           // {
           //     table.Cell(ic, 1).Range.Text = item.НомерУслуги;
           //     table.Cell(ic, 2).Range.Text = item.Наименование;
                
           //     table.Cell(ic, 3).Range.Text = item.Количество;
           //     table.Cell(ic, 4).Range.Text = item.Цена;

           //     //doc.Words.Count.ToString();
           //     Object beforeRow1 = Type.Missing;
           //     table.Rows.Add(ref beforeRow1);

           //     ic++;
           // }

           // //удалим последную строку
           // table.Rows[ic].Delete();

           // //откроем получившейся документ
           // app.Visible = true;

        }

        private void gvДопСоглашение_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelContract_Click(object sender, EventArgs e)
        {
            int i_договор = id_договор;

            //string iTest = "Test";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //FormPassword fPass = new FormPassword();
            //fPass.ShowDialog();

            //if (fPass.DialogResult == DialogResult.OK)
            //{
            //    if (fPass.FlagValidate == true)
            //    {
                    string connectionString = ConnectionDB.ConnectionString();

                        // Получим текущюую строку.
                         DataGridViewRow row = this.gvДоговор.CurrentRow;

                        // Получим id договора.
                        int id_contract = Convert.ToInt32(row.Cells[0].Value);

                        // Снимим флаг о наличии акта выполненных работ.
                        string queryUpdate = "update Договор " +
                                             " set ФлагНаличияАкта = 0 " +
                                             " where id_договор = "+ id_contract +" ";

                        Query.Execute(queryUpdate, connectionString);

                        // Удалим акт выполненных работ.
                        //string queryDelete = "delete АктВыполнненныхРабот " +
                        //                     " where id_договор = " + id_contract + " ";

                        string queryDelete = "delete from АктВыполнненныхРабот where id_договор = " + id_contract + " ";

                        Query.Execute(queryDelete, connectionString);

                        MessageBox.Show("Изменения внесены");

                        this.Close();

                //}
                //else
                //{
                //    this.Close();
                //}
            //}


            ////// Получим номер договора.
            ////DataGridViewSelectedRowCollection selRow = this.gvДоговор.SelectedRows;
            ////string номерДоговора = selRow[1].Cells[1].Value.ToString();
            ////string датаДоговора = selRow[0].Cells[1].Value.ToString();

            //DataGridViewRow row = this.gvДоговор.CurrentRow;
            //string номерДоговора = row.Cells[1].Value.ToString().Trim();

            //int id_contract = Convert.ToInt32(row.Cells[0].Value);

            //string query = "select id_договор from Договор where НомерДоговора = '" + номерДоговора + "' ";
            //DataTable tabID = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ВременнаяТаблицаIDДоговор");


            //int idДоговор = Convert.ToInt32(tabID.Rows[0]["id_договор"]);

            //// Скроем форму выбора даты для акта.
            ////FormКалендарь fk = new FormКалендарь();
            ////fk.ShowDialog();

            //// Переменная для хранения данных.
            //DataTable tabTemp;
            
            //string qDate = "select ДатаПодписания from АктВыполнненныхРабот where id_договор = "+ idДоговор +" ";
            //DataTable tabДата = ТаблицаБД.GetTable(qDate, ConnectionDB.ConnectionString(), "ВременнаяАктВыполнненныхРабот");
            // date = Convert.ToDateTime(tabДата.Rows[0]["ДатаПодписания"]);

            ////if (fk.DialogResult == DialogResult.OK)
            ////{
            ////    date = fk.Date;

            //    // Преобразуем в удобочитаемый формат.


            //    //// Изменим номер договора.
            //    //string queryUpdate = "update АктВыполнненныхРабот " +
            //    //                     "set ДатаПодписания = '" + date.ToShortDateString() + "' " +
            //    //                     "where id_договор = " + idДоговор + " ";

            //    //// Сохраним зменения.
            //    ////выполним обновление таблицы Договор
            //    //Query.Execute(queryUpdate, ConnectionDB.ConnectionString());

            //string selQuery = "select * from АктВыполнненныхРабот " +
            //                  "where id_договор = " + idДоговор + " ";

            //tabTemp = ТаблицаБД.GetTable(selQuery, ConnectionDB.ConnectionString(), "ВременнаяТаблица");

            //// Номер акта.
            //string numАкт = tabTemp.Rows[0][1].ToString().Trim();
            //string queryНомерДог = "select * from Договор where id_договор = " + idДоговор + " ";;

            //DataTable tabTempДог = ТаблицаБД.GetTable(queryНомерДог, ConnectionDB.ConnectionString(), "ВременнаяТаблицаДог");

            //string номерДог = tabTempДог.Rows[0][1].ToString().Trim();


            ////список для хранения данных для таблицы в акте
            //List<ТаблицаДоговор> list = new List<ТаблицаДоговор>();
            ////заполгним первую строку

            //ТаблицаДоговор шапка = new ТаблицаДоговор();
            //шапка.ПорядковыНомер = "№ п/п";
            //шапка.НомерУслуги = "№ усл в справ";
            //шапка.Наименование = "Наименование";
            //шапка.Цена = "Цена, руб";
            //шапка.Количество = "Кол";
            //шапка.Стоимость = "Стоимость";
            //list.Add(шапка);

            ////получим данные для таблицы
            //string selectQuery = "select * from УслугиПоДоговору where id_договор = " + idДоговор + " ";
            //DataTable улугиДоговор = ТаблицаБД.GetTable(selectQuery, ConnectionDB.ConnectionString(), "УслугиПоДоговору");

            ////установим счётчик
            //int iCount = 1;

            ////счётчик суммы итого
            //decimal суммИтого = 0m;

            ////заполним коллекцию классов для таблицы
            //foreach (DataRow r in улугиДоговор.Rows)
            //{
            //    ТаблицаДоговор str = new ТаблицаДоговор();
            //    str.ПорядковыНомер = iCount.ToString();

            //    str.НомерУслуги = r["НомерПоПеречню"].ToString().Replace(',', '.');
            //    str.Наименование = r["НаименованиеУслуги"].ToString();

            //    str.Цена = r["Цена"].ToString();
            //    str.Количество = r["Количество"].ToString();

            //    str.Стоимость = r["Сумма"].ToString();
            //    list.Add(str);

            //    //if (i != 1)
            //    //{
            //    суммИтого = Math.Round(суммИтого + Convert.ToDecimal(str.Стоимость), 2);
            //    //}

            //    //увеличем на 1
            //    iCount++;
            //}

            ////создадим строку итого 
            //ТаблицаДоговор итого = new ТаблицаДоговор();
            //итого.НомерУслуги = "Итого:";
            //итого.Стоимость = суммИтого.ToString("c");

            //list.Add(итого);

            ////создадим документ WORD
            //string fName = this.ФИО_Льготника;

            ////распечатаем word
            ////FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\Акт4.dot");
            //// FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\"+fileName+".doc");
            ////fnDel.Delete();

            ////Скопируем шаблон в папку Документы
            //FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Акт4.doc");
            //fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);

            //string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";

            ////Создаём новый Word.Application
            //Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            ////Загружаем документ
            //Microsoft.Office.Interop.Word.Document doc = null;

            //object fileName = filName;
            //object falseValue = false;
            //object trueValue = true;
            //object missing = Type.Missing;

            //doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            //ref missing, ref missing, ref missing, ref missing, ref missing,
            //ref missing, ref missing, ref missing, ref missing, ref missing,
            //ref missing, ref missing, ref missing);


            //////Номер договора
            //object wdrepl = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt = "номердоговора";
            ////object newtxt = (object)номерДоговора; //нумДог
            //object newtxt = (object)номерДог;
            ////object frwd = true;
            //object frwd = false;
            //doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            //ref missing, ref missing);

            ////получим номер акта
            ////string номДоговора = this.gvДоговор.CurrentRow.Cells["НомерДоговора"].Value.ToString();

            //string номерАкта = numАкт;


            //////Номер договора
            //object wdrepl2 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt2 = "номеракта";
            //object newtxt2 = (object)номерАкта;
            ////object frwd = true;
            //object frwd2 = false;
            //doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            //ref missing, ref missing);

            //string qHosp = "select * from Поликлинника";
            //DataTable tabHosp = ТаблицаБД.GetTable(qHosp, ConnectionDB.ConnectionString(), "Поликлинника");
            //DataRow rH = tabHosp.Rows[0];

            ////присвоим наименование поликлинники
            //string исполнитель = rH["НаименованиеПоликлинники"].ToString();

            ////вычислим потребителя
            //string потребитель = this.ФИО_Льготника;


            //////Номер договора
            //object wdrepl3 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt3 = "Больница";
            //object newtxt3 = (object)исполнитель;
            ////object frwd = true;
            //object frwd3 = false;
            //doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            //ref missing, ref missing);

            //////Номер договора
            //object wdrepl4 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt4 = "Льготник";
            //object newtxt4 = (object)потребитель;
            ////object frwd = true;
            //object frwd4 = false;
            //doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            //ref missing, ref missing);

            ////выведим дату договора
            //string датаДоговора = this.gvДоговор.CurrentRow.Cells["ДатаДоговора"].Value.ToString();

            ////Номер договора
            //object wdrepl5 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt5 = "датадоговора";
            //object newtxt5 = (object)датаДоговора;
            ////object frwd = true;
            //object frwd5 = false;
            //doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
            //ref missing, ref missing);

            ////выведим дату договора
            ////string датаАкта = setDate.DateTimeValue.ToShortDateString();
            ////датаАкта = setDate.DateTimeValue.ToShortDateString();
            

            ////Номер договора
            //object wdrepl6 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt6 = "датаакта";
            //object newtxt6 = (object)date.ToShortDateString();
            ////object frwd = true;
            //object frwd6 = false;
            //doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
            //ref missing, ref missing);

            ////ФИО глав врача Главврач
            //object wdrepl7 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt7 = "Главврач";
            //object newtxt7 = (object)фиоВрача;
            ////object frwd = true;
            //object frwd7 = false;
            //doc.Content.Find.Execute(ref searchtxt7, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd7, ref missing, ref missing, ref newtxt7, ref wdrepl7, ref missing, ref missing,
            //ref missing, ref missing);

            //string summa = Валюта.Рубли.Пропись(суммИтого);

            ////Выведим сумму
            //object wdrepl8 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt8 = "сумпрописью";
            //object newtxt8 = (object)summa;
            ////object frwd = true;
            //object frwd8 = false;
            //doc.Content.Find.Execute(ref searchtxt8, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd8, ref missing, ref missing, ref newtxt8, ref wdrepl8, ref missing, ref missing,
            //ref missing, ref missing);

            ////Вставить таблицу
            //object bookNaziv = "таблица";
            //Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            //object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            //object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            //Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
            //table.Range.ParagraphFormat.SpaceAfter = 6;

            ////выставим ширину столбцов
            //table.Columns[1].Width = 40;
            //table.Columns[2].Width = 50;
            //table.Columns[3].Width = 250;
            //table.Columns[4].Width = 60;
            //table.Columns[5].Width = 40;
            //table.Columns[6].Width = 70;
            //table.Borders.Enable = 1; // Рамка - сплошная линия
            //table.Range.Font.Name = "Times New Roman";
            //table.Range.Font.Size = 10;
            ////счётчик строк
            //int i = 1;

            ////запишем данные в таблицу
            //foreach (ТаблицаДоговор item in list)
            //{
            //    table.Cell(i, 1).Range.Text = item.ПорядковыНомер;
            //    table.Cell(i, 2).Range.Text = item.НомерУслуги;

            //    table.Cell(i, 3).Range.Text = item.Наименование;
            //    table.Cell(i, 4).Range.Text = item.Цена;

            //    table.Cell(i, 5).Range.Text = item.Количество;
            //    table.Cell(i, 6).Range.Text = item.Стоимость;

            //    //doc.Words.Count.ToString();
            //    Object beforeRow1 = Type.Missing;
            //    table.Rows.Add(ref beforeRow1);

            //    i++;
            //}

            //table.Rows[i].Delete();

            ////Должность на подписи
            //string queryДолжность = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
            //string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

            //object wdrepl9 = WdReplace.wdReplaceAll;//39
            ////object searchtxt = "GreetingLine";
            //object searchtxt9 = "Predsedatel";
            //object newtxt9 = (object)Predsedatel;
            ////object frwd = true;
            //object frwd9 = false;
            //doc.Content.Find.Execute(ref searchtxt9, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd9, ref missing, ref missing, ref newtxt9, ref wdrepl9, ref missing, ref missing,
            //ref missing, ref missing);

            //app.Visible = true;
        //}
        }

        private void привязатьВрачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { id_договор = Convert.ToInt32(this.gvДоговор.CurrentRow.Cells["id_договор"].Value); }
            catch
            { }     //MessageBox.Show("Заключите договор.","Нет договора"); }
            

            if (this.txtTechSheet.Text.Length > 0)
            {
                // Получим id врача.
                string queryEsc = "select id_врач from Врач " +
                                  "where ФИО = '" + this.cmbEsculap.Text.Trim() + "' ";

                // id врача - протезиста.
                int idEsculap = Convert.ToInt32(ТаблицаБД.GetTable(queryEsc, ConnectionDB.ConnectionString(), "ВрачПротезист").Rows[0]["id_врач"]);

                 //string hValue = this.cmbEsculap.SelectedItem.HiddenValue; 

                string queryUpdate = "update Договор " +
                                     " set id_врач = " + idEsculap + ", " +
                                     " НомерТехЛиста = '" + this.txtTechSheet.Text.Trim() + "' " +
                                     " where id_договор = " + id_договор + " ";

                Query.Execute(queryUpdate, ConnectionDB.ConnectionString());

                MessageBox.Show("Технический лист и Фио врача привязаны к договору");

                this.txtTechSheet.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Укажите номер технического листа");
            }
        }

        
       
        

    }
}