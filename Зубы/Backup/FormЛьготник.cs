using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

//Для отправки по сети
using System.Net;
using System.IO;
//using EncryptMessage;


using Стамотология.Classes;

namespace Стамотология
{
    public partial class FormЛьготник : Form
    {
        //флаг обновления льготника
        private bool flagUpdate = false;
        private int _id_льготникUpdate;

        //номер поликлиники
        private string inn;

        //объявим счётчик загрузки формы
        private int счётчикЗагрузки = 0;

        // Переменная для хранения состояния редактирования СНИЛС.
        private bool flagEditSnils = false;

        /// <summary>
        /// Хранимт id выбранного льготника
        /// </summary>
        public int Id_льготникUpdate
        {
            get
            {
                return _id_льготникUpdate;
            }
            set
            {
                _id_льготникUpdate = value;
            }
        }

        /// <summary>
        /// false = форма работает на добавление, true = на обновление
        /// </summary>
        public bool FlagUpdate
        {
            get
            {
                return flagUpdate;
            }
            set
            {
                flagUpdate = value;
            }
        }

        //переменная хранит id льготника для обновления записи
        //private int id_льготникUpdate;

        public FormЛьготник()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strInput = this.number_snils.Text.Trim();
            //string snils = "нет записи";
            //if (RemoveSpacesDash(number_snils.Text.ToString()) != "")
            //    snils = RemoveSpacesDash(number_snils.Text.ToString());

            int iRegionTest = Convert.ToInt16(this.cmbРайон.SelectedValue);

            string flagRaion = Crypto.Shifrovka(this.cmbРайон.SelectedValue.ToString());

            //string pattern = @"\d{3}-\d{3}-\d{3} \d{2}";
            if (this.FlagUpdate == false)// )
            {
                //if (this.txtФИО.Text != "" && this.textBox1.Text.Length  != 0 && this.textBox2.Text != "" && this.textBox6.Text != "" && this.textBox5.Text != "" && this.maskedTextBox1.Text != "" && this.maskedTextBox2.Text != "" && this.textBox9.Text != "" && this.maskedTextBox3.Text != "" && this.maskedTextBox4.Text != "" && this.comboBox1.Text.Trim() != "" && this.comboBox2.Text.Trim() != "" && ((this.cmbНП.Text.Trim() != "" || this.cmbРайон.Text.Trim() != "")) || (this.cmbНП.Text.Trim() != "" && this.cmbРайон.Text.Trim() != ""))
                if ((this.txtФИО.Text.Trim().Length != 0) && (this.textBox1.Text.Trim().Length != 0) &&
                    (this.textBox2.Text.Trim().Length != 0) && (this.textBox6.Text.Trim().Length != 0) &&
                    (this.textBox5.Text.Trim().Length != 0) && (this.maskedTextBox1.Text.Trim().Length != 0) &&
                    (this.maskedTextBox2.Text.Trim().Length != 0) && (this.textBox9.Text.Trim().Length != 0) &&
                    (this.maskedTextBox3.Text.Trim().Length != 0) && (this.maskedTextBox4.Text.Trim().Length != 0) &&
                    (this.comboBox1.Text.Trim().Length != 0) && (this.comboBox2.Text.Trim().Length != 0) /*&& this.number_snils.Text != "" */&&
                    ((this.cmbНП.Text.Trim().Length != 0 || this.cmbРайон.Text.Trim().Length != 0)) || (this.cmbНП.Text.Trim().Length != 0 && this.cmbРайон.Text.Trim().Length != 0))
                {

                    //Внесём льготника в БД
                    InsertЛьготник льготник = new InsertЛьготник(this.txtФИО.Text, this.textBox1.Text, 
                        this.textBox6.Text, Convert.ToDateTime(this.maskedTextBox3.Text), this.textBox2.Text, 
                        this.textBox5.Text, this.textBox4.Text, this.textBox3.Text, this.maskedTextBox1.Text, 
                        this.maskedTextBox2.Text, Convert.ToDateTime(this.maskedTextBox4.Text), 
                        this.textBox8.Text,Convert.ToInt32(this.comboBox1.SelectedValue), 
                        Convert.ToInt32(this.comboBox2.SelectedValue), this.textBox10.Text, 
                        this.textBox9.Text, Convert.ToDateTime(this.maskedTextBox5.Text), 
                        this.textBox11.Text,1,Convert.ToInt32(cmbРайон.SelectedValue),
                        Convert.ToInt32(cmbНП.SelectedValue), RemoveSpacesDash(number_snils.Text.ToString()), this.checkBox1.Checked, flagRaion/*, 
                        snils*/
                               );

                    //внесём льготника в БД
                    Receiver recivar = new Receiver();
                    recivar.Action(льготник);

                    //обновим содержание DataGridView
                    //string query = "select * from Льготник";

                    this.txtФИО.Text = "";
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    this.textBox4.Text = "";
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                    //this.textBox7.Text = "";
                    this.textBox8.Text = "";
                    this.textBox9.Text = "";
                    this.textBox10.Text = "";
                    this.textBox11.Text = "";
                    this.maskedTextBox1.Text = "";
                    this.maskedTextBox2.Text = "";
                    this.maskedTextBox3.Text = "";
                    this.maskedTextBox4.Text = "";
                    this.maskedTextBox5.Text = "";

                    this.number_snils.Text = "";

                    //установим flag обновления в false
                    flagUpdate = false;
                }
                else
                {
                    MessageBox.Show("Вы не заполнили обязательные поля");
                }
            }
            
            // Форма работает на обновление льготника в БД.
            if (this.FlagUpdate == true)
            {
                //if (Regex.IsMatch(this.number_snils.Text, pattern) == false)
                //{
                //    MessageBox.Show("Введите корректный СНИЛС");
                //    return;
                //}

                if (this.txtФИО.Text != "" && this.textBox1.Text != "" && this.textBox2.Text != "" && 
                    this.textBox6.Text != "" && this.textBox5.Text != "" && this.maskedTextBox1.Text != "" &&
                    this.maskedTextBox2.Text != "" && this.textBox9.Text != "" && this.maskedTextBox3.Text != "" &&
                    this.maskedTextBox4.Text != "" && this.comboBox1.Text.Trim() != "" &&
                    this.comboBox2.Text.Trim() != "" /*&& this.number_snils.Text != ""*/ &&
                    ((this.cmbНП.Text.Trim() != "" || this.cmbРайон.Text.Trim() != "")) || (this.cmbНП.Text.Trim() != "" && this.cmbРайон.Text.Trim() != ""))
                {
                    //int Test = id_льготникUpdate;

                    UpdateЛьготник льготник = new UpdateЛьготник(Id_льготникUpdate, this.txtФИО.Text,
                        this.textBox1.Text, this.textBox6.Text, Convert.ToDateTime(this.maskedTextBox3.Text),
                        this.textBox2.Text, this.textBox5.Text, this.textBox4.Text, this.textBox3.Text,
                        this.maskedTextBox1.Text, this.maskedTextBox2.Text,
                        Convert.ToDateTime(this.maskedTextBox4.Text), this.textBox8.Text,
                        Convert.ToInt32(this.comboBox1.SelectedValue), Convert.ToInt32(this.comboBox2.SelectedValue),
                        this.textBox10.Text, this.textBox9.Text, Convert.ToDateTime(this.maskedTextBox5.Text),
                        this.textBox11.Text, 1, Convert.ToInt32(cmbРайон.SelectedValue),
                        Convert.ToInt32(cmbНП.SelectedValue), this.checkBox1.Checked, RemoveSpacesDash(number_snils.Text.ToString()), flagRaion);
                    //изменим льготника в БД
                    Receiver recivar = new Receiver();
                    recivar.Action(льготник);

                    //обновим содержание DataGridView
                    //string query = "select * from Льготник";

                    this.txtФИО.Text = "";
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    this.textBox4.Text = "";
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                    //this.textBox7.Text = "";
                    this.textBox8.Text = "";
                    this.textBox9.Text = "";
                    this.textBox10.Text = "";
                    this.textBox11.Text = "";
                    this.maskedTextBox1.Text = "";
                    this.maskedTextBox2.Text = "";
                    this.maskedTextBox3.Text = "";
                    this.maskedTextBox4.Text = "";
                    this.maskedTextBox5.Text = "";
                    this.number_snils.Text = "";

                    //установим flag обновления в false
                    flagUpdate = false;
                }
                else
                {
                    MessageBox.Show("Вы не заполнили обязательные поля");
                }
            }
        }

        private void FormЛьготник_Load(object sender, EventArgs e)
        {
            //получим строку подключения
            string sCon = ConnectionDB.ConnectionString();

            // Запрос на получение кода поликлинники.
            string queryHosp = "select ИНН from Поликлинника";

            DataTable tabHosp = Поликлинника.GetПоликлинники(queryHosp, sCon);
            inn = tabHosp.Rows[0]["ИНН"].ToString().Trim();           
     
            //заполним данными список льготных категорий
            ЛьготнаяКатегория лк = new ЛьготнаяКатегория();
            this.comboBox1.DataSource = лк.GetЛьготнаяКатегория(sCon);
            this.comboBox1.ValueMember  = "id_льготнойКатегории";
            this.comboBox1.DisplayMember = "ЛьготнаяКатегория";
            this.comboBox1.Text = "";
            //this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;


            //заполним данными список тип документа
            ТипДокумента тд = new ТипДокумента();
            this.comboBox2.DataSource = тд.GetТипДокумента(sCon);
            this.comboBox2.ValueMember = "id_документ";
            this.comboBox2.DisplayMember = "НаименованиеТипаДокумента";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // При обновлении данных по льготнику.
            if (this.FlagUpdate == true)
            {
                Льготник льготник = GetЛьготник.GetRow(this.Id_льготникUpdate);
                this.txtФИО.Text = льготник.Фамилия;

                //отобразим льготную категорию
                string query = "select ЛьготнаяКатегория from ЛьготнаяКатегория where id_льготнойКатегории in (select id_льготнойКатегории from Льготник where id_льготник = " + this.Id_льготникUpdate + ")";
                DataTable tabRR = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ЛьготнаяКатегория");
                DataRow r = tabRR.Rows[0];

                string nameLK = r["ЛьготнаяКатегория"].ToString();
                comboBox1.Text = nameLK;

                this.textBox1.Text = льготник.Имя;
                this.textBox6.Text = льготник.Отчество;

                this.maskedTextBox3.Text = льготник.ДатаРождения.ToShortDateString();
                this.textBox2.Text = льготник.улица;
                this.textBox5.Text = льготник.НомерДома;
                this.textBox4.Text = льготник.корпус;
                this.textBox3.Text = льготник.НомерКвартиры;
                this.maskedTextBox1.Text = льготник.СерияПаспорта;
                this.maskedTextBox2.Text = льготник.НомерПаспорта;
                this.maskedTextBox4.Text = льготник.ДатаВыдачиПаспорта.ToShortDateString();
                this.textBox8.Text = льготник.КемВыданПаспорт;
                this.textBox10.Text = льготник.СерияДокумента;
                this.textBox9.Text = льготник.НомерДокумента;
                this.maskedTextBox5.Text = льготник.ДатаВыдачиДокумента.ToShortDateString();
                this.textBox11.Text = льготник.КемВыданДокумент;
                this.number_snils.Text = льготник.SNILS;

                //отобразим состояние checkbox - а
                string sTest = льготник.FlagRaion;

                //if (льготник.id_район == -1)
                if (льготник.FlagRaion == null)
                {
                    //this.checkBox1.Checked = true;
                    MessageBox.Show("Выберите район проживания льготника");

                    this.button1.Enabled = false;

                    this.cmbРайон.Visible = true;
                }
                //else
                //{
                //    this.checkBox1.Checked = false;
                //}

                // отобразим наименование района
                //string queryР = "select id_район, РайонОбласти from НаименованиеРайона where id_район in (select id_район from Льготник where id_льготник = " + this.Id_льготникUpdate + " )";

                
                //DataTable tab = ДанныеПредставление.GetПредставление(queryР, "Льготник");

                ListRegion listRegion = new ListRegion();

                // Получим список районов в Саратовской области.
                DataTable tab = ConvertTo.DataRowsArray(listRegion.Regions());

                if (tab.Rows.Count != 0)
                {
                    //отобразим район 
                    string queryРайон = "select id_район,РайонОбласти from НаименованиеРайона";
                    DataTable tabРайон = ТаблицаБД.GetTable(queryРайон, ConnectionDB.ConnectionString(), "НаименованиеРайона");

                    this.cmbРайон.DataSource = tab;// tabРайон;
                    this.cmbРайон.ValueMember = "id_район";
                    this.cmbРайон.DisplayMember = "РайонОбласти";
                    this.cmbРайон.DropDownStyle = ComboBoxStyle.DropDownList;

                    // Получим наименование района в котором прооживает льготник.
                    string queryР = "select FlagRaion from Льготник where id_льготник = " + this.Id_льготникUpdate + " ";

                    DataTable tabRegion = ТаблицаБД.GetTable(queryР, ConnectionDB.ConnectionString(), "ФлагРегион");

                    if (tabRegion.Rows[0]["FlagRaion"] == DBNull.Value)
                    {
                        // Так как не могу создать метод расширения, тогда проверяем на длинну строки.
                        if (tab.Rows[0][0].ToString().Trim().Length == 1)
                        {
                            this.cmbРайон.SelectedValue = 0;
                        }
                    }
                    else
                    {
                        // Расшифруем содержимое ячейки.
                        string idRs = Crypto.DeShifrovka(tabRegion.Rows[0][0].ToString().Trim());

                        this.cmbРайон.SelectedValue = idRs;
                    }

                    //отобразим район который прописан в БД
                    //this.cmbРайон.Text = tab.Rows[0][0].ToString();
                    //this.cmbРайон.SelectedValue = tab.Rows[0][0].ToString();
                }
                else
                {
                    /*
                    //отобразим район 
                    string queryРайон = "select id_район,РайонОбласти from НаименованиеРайона";
                    DataTable tabРайон = ТаблицаБД.GetTable(queryРайон, ConnectionDB.ConnectionString(), "НаименованиеРайона");

                    this.cmbРайон.DataSource = tabРайон;
                    this.cmbРайон.ValueMember = "id_район";
                    this.cmbРайон.DisplayMember = "РайонОбласти";
                    this.cmbРайон.DropDownStyle = ComboBoxStyle.DropDownList;
                    */
                }

                //отобразим название населённого пункта
                string queryН = "select Наименование from НаселенныйПункт where id_насПункт in (select id_насПункт from Льготник where id_льготник = " + this.Id_льготникUpdate + " )";
                DataTable tabН = ДанныеПредставление.GetПредставление(queryН, "Льготник");

                string queryНT = "select id_насПункт from Льготник where id_льготник = " + this.Id_льготникUpdate + " ";
                DataTable tabНT = ДанныеПредставление.GetПредставление(queryНT, "Льготник");

                if (tabН.Rows.Count != 0)
                {
                    //населённый пункт
                    string queryНП = "select id_насПункт,Наименование from НаселенныйПункт";
                    DataTable tabНП = ТаблицаБД.GetTable(queryНП, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                    this.cmbНП.DataSource = tabНП;
                    this.cmbНП.ValueMember = "id_насПункт";
                    this.cmbНП.DisplayMember = "Наименование";
                    this.cmbНП.DropDownStyle = ComboBoxStyle.DropDownList;


                    //отобразим населённый пункет в котором прописан текущий льготник
                    this.cmbНП.Text = tabН.Rows[0][0].ToString();
                }
                else
                {
                    //населённый пункт
                    string queryНП = "select id_насПункт,Наименование from НаселенныйПункт";
                    DataTable tabНП = ТаблицаБД.GetTable(queryНП, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                    this.cmbНП.DataSource = tabНП;
                    this.cmbНП.ValueMember = "id_насПункт";
                    this.cmbНП.DisplayMember = "Наименование";
                    this.cmbНП.DropDownStyle = ComboBoxStyle.DropDownList;

                }

               // Проверим записан корректный СНИЛС или нет.
//////if (Regex.IsMatch(this.number_snils.Text, @"\d{3}-\d{3}-\d{3} \d{2}") == false)
//////{
//////    this.button1.Enabled = false;
//////}

                // Данные в форму загрузились.
                flagEditSnils = true;
            }
            else
            {
                // При создании новой карточки для льготника.
                //отобразим район 
                //string queryРайон = "select id_район,РайонОбласти from НаименованиеРайона";
                //DataTable tabРайон = ТаблицаБД.GetTable(queryРайон, ConnectionDB.ConnectionString(), "НаименованиеРайона");

                // Сформируем список районов области.
                ListRegion listRegion = new ListRegion();

                // Получим список районов в Саратовской области.
                DataTable tabРайон = ConvertTo.DataRowsArray(listRegion.Regions());

                this.cmbРайон.DataSource = tabРайон;
                this.cmbРайон.ValueMember = "id_район";
                this.cmbРайон.DisplayMember = "РайонОбласти";
                this.cmbРайон.Text = "";

                //населённый пункт
                string queryНП = "select id_насПункт,Наименование from НаселенныйПункт";
                DataTable tabНП = ТаблицаБД.GetTable(queryНП, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                this.cmbНП.DataSource = tabНП;
                this.cmbНП.ValueMember = "id_насПункт";
                this.cmbНП.DisplayMember = "Наименование";
                this.cmbНП.Text = "";
                //this.cmbНП.DropDownStyle = ComboBoxStyle.DropDownList;

                //установим поля редактирования по умолчанию в запрет вводить информацию и установим цвет в системный
                this.txtФИО.Enabled = false;
                this.txtФИО.BackColor = Color.FromName("Control");

                this.textBox1.Enabled = false;
                this.textBox1.BackColor = Color.FromName("Control");

                this.textBox6.Enabled = false;
                this.textBox6.BackColor = Color.FromName("Control");

                this.maskedTextBox3.Enabled = false;
                this.maskedTextBox3.BackColor = Color.FromName("Control");

                this.textBox2.Enabled = false;
                this.textBox2.BackColor = Color.FromName("Control");

                this.textBox5.Enabled = false;
                this.textBox5.BackColor = Color.FromName("Control");

                this.maskedTextBox1.Enabled = false;
                this.maskedTextBox1.BackColor = Color.FromName("Control");

                this.maskedTextBox2.Enabled = false;
                this.maskedTextBox2.BackColor = Color.FromName("Control");

                this.maskedTextBox4.Enabled = false;
                this.maskedTextBox4.BackColor = Color.FromName("Control");

                this.textBox8.Enabled = false;
                this.textBox8.BackColor = Color.FromName("Control");

                this.textBox9.Enabled = false;
                this.textBox9.BackColor = Color.FromName("Control");

                this.maskedTextBox5.Enabled = false;
                this.maskedTextBox5.BackColor = Color.FromName("Control");

                this.textBox11.Enabled = false;
                this.textBox11.BackColor = Color.FromName("Control");

                this.number_snils.Enabled = false;
                this.number_snils.BackColor = Color.FromName("Control");

                this.button1.Enabled = false;

                this.cmbНП.Enabled = false;

                // Данные в форму загрузились.
                flagEditSnils = true;
            }

            //увеличим счётчик загрузки на 1
            счётчикЗагрузки = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox6.Text.Length != 0)
            {
                this.maskedTextBox3.Enabled = true;
                this.maskedTextBox3.BackColor = Color.FromName("Window");
            }
            else
            {
                this.maskedTextBox3.Enabled = false;
                this.maskedTextBox3.BackColor = Color.FromName("Control");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////получим id льготника
            //int id_льготник = (int)this.dataGridView1.CurrentRow.Cells["id_льготник"].Value;

            ////присвоим id льготника переменной для хранения данного id применяемого при обновлении записи
            //id_льготникUpdate = id_льготник;

            //Льготник льготник = GetЛьготник.GetRow(id_льготник);
            //this.txtФИО.Text = льготник.Фамилия;

            //this.textBox1.Text = льготник.Имя;
            //this.textBox6.Text = льготник.Отчество;

            //this.maskedTextBox3.Text = льготник.ДатаРождения.ToShortDateString();
            //this.textBox2.Text = льготник.улица;
            //this.textBox5.Text = льготник.НомерДома;
            //this.textBox4.Text = льготник.корпус;
            //this.textBox3.Text = льготник.НомерКвартиры;
            //this.maskedTextBox1.Text = льготник.СерияПаспорта;
            //this.maskedTextBox2.Text = льготник.НомерПаспорта;
            //this.maskedTextBox4.Text = льготник.ДатаВыдачиПаспорта.ToShortDateString();
            //this.textBox8.Text = льготник.КемВыданПаспорт;
            //this.textBox10.Text = льготник.СерияДокумента;
            //this.textBox9.Text = льготник.НомерДокумента;
            //this.maskedTextBox5.Text = льготник.ДатаВыдачиДокумента.ToShortDateString();
            //this.textBox11.Text = льготник.КемВыданДокумент;


            // //получим id льготнойКатегории
            //int id_льготнойКатегории = (int)this.dataGridView1.CurrentRow.Cells["id_льготнойКатегории"].Value;

            ////обновим содержание DataGridView
            ////string query = "select * from ЛьготнаяКатегория where id_льготнойКатегории = " + id_льготнойКатегории + " ";

            ////получим строку подключения
            //string sCon = ConnectionDB.ConnectionString();

            ////заполним данными список льготных категорий
            //ЛьготнаяКатегория лк = new ЛьготнаяКатегория();
            //this.comboBox1.DataSource = лк.GetCurrentЛьготнаяКатегория(sCon, id_льготнойКатегории);
            //this.comboBox1.ValueMember = "id_льготнойКатегории";
            //this.comboBox1.DisplayMember = "ЛьготнаяКатегория";


            ////получим id типа документа
            //int id_документ = (int)this.dataGridView1.CurrentRow.Cells["id_документ"].Value;

            //////обновим содержание DataGridView
            ////string queryДок = "select * from ТипДокумента where id_документ = " + id_документ + " ";
            //////this.dataGridView1.DataSource = null;
            ////this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(queryДок, "ТипДокумента");
            //ТипДокумента тд = new ТипДокумента();
            //this.comboBox2.DataSource = тд.GetCurrentТипДокумента(sCon, id_документ);
            //this.comboBox2.ValueMember = "id_документ";
            //this.comboBox2.DisplayMember = "НаименованиеТипаДокумента";

            ////установим flagUpdate в true
            //flagUpdate = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //получим id льготника
            //int id_льготник = (int)this.dataGridView1.CurrentRow.Cells["id_льготник"].Value;

            //DeleteЛьготник льготник = new DeleteЛьготник(id_льготник);
            ////изменим льготника в БД
            //Receiver recivar = new Receiver();
            //recivar.Action(льготник);

            //обновим содержание DataGridView
            //string query = "select * from Льготник";
            //this.dataGridView1.DataSource = null;
            //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");


            ////скроем не нужные для пользователя столбцы
            //this.dataGridView1.Columns["id_льготник"].Visible = false;
            //this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
            //this.dataGridView1.Columns["id_документ"].Visible = false;

            this.txtФИО.Text = "";
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
            //this.textBox7.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.textBox11.Text = "";
            this.maskedTextBox1.Text = "";
            this.maskedTextBox2.Text = "";
            this.maskedTextBox3.Text = "";
            this.maskedTextBox4.Text = "";
            this.maskedTextBox5.Text = "";
            this.number_snils.Text = "";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //отобразим введённых льготников
            //string query = "select * from Льготник where Фамилия like '"+ this.textBox7.Text +"%'";
            //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormРайон район = new FormРайон();
            район.ShowDialog();

            if (район.DialogResult == DialogResult.Cancel)
            {
                string query = "select id_район,РайонОбласти from НаименованиеРайона";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаименованиеРайона");

                this.cmbРайон.DataSource = tab;
                this.cmbРайон.ValueMember = "id_район";
                this.cmbРайон.DisplayMember = "РайонОбласти";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormНаселПункт нп = new FormНаселПункт();
            нп.ShowDialog();

            if (нп.DialogResult == DialogResult.Cancel)
            {
                string query = "select id_насПункт,Наименование from НаселенныйПункт";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                this.cmbНП.DataSource = tab;
                this.cmbНП.ValueMember = "id_насПункт";
                this.cmbНП.DisplayMember = "Наименование";
            }
        }

        private void cmbНП_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (Regex.IsMatch(this.cmbНП.Text, "Саратов") == true)
            //{
            //    this.groupBox2.Visible = false;
            //}
            //else
            //{
            //    this.groupBox2.Visible = true;
            //}
            

            //if (this.cmbНП.Text == "г. Саратов")
            //{
            //    this.groupBox2.Visible = false;
            //}
            //else
            //{
            //    this.groupBox2.Visible = true;
            //}
            
        }

        private void cmbРайон_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object val = this.cmbРайон.SelectedValue;

            if (val != null)
            {
                if ((this.cmbРайон.Text.ToLower().Trim() != "--Район не выбран--".ToLower().Trim()))// || (this.cmbРайон.Text.ToLower().Trim() != "".ToLower().Trim()))
                {
                    this.cmbНП.Enabled = true;
                    this.txtФИО.Enabled = true;
                    this.txtФИО.BackColor = Color.FromName("Window");

                    if (this.FlagUpdate == true)
                    {
                        this.button1.Enabled = true;
                    }

                }
                else if (this.cmbРайон.Text.ToLower().Trim() == "--Район не выбран--".ToLower().Trim() || (this.cmbРайон.Text.ToLower().Trim() != "".ToLower().Trim()))
                {
                    this.cmbНП.Enabled = false;
                    this.txtФИО.Enabled = false;
                    this.txtФИО.BackColor = Color.FromName("Control");

                    if (this.FlagUpdate == true)
                    {
                        this.button1.Enabled = false;
                    }
                }
            }
            else
            {
                this.cmbНП.Enabled = false;
                this.txtФИО.Enabled = false;
                this.txtФИО.BackColor = Color.FromName("Control");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.groupBox2.Visible = false;
                this.cmbНП.Enabled = true;
            }
            else
            {
                this.groupBox2.Visible = true;
            }
        }

        private void cmbРайон_TextChanged(object sender, EventArgs e)
        {
            //не разрешим вносить изменеиия в раскрывающийся список с районом (обнулим текст пусть его выберают)
            if (this.счётчикЗагрузки != 0)
            {
                this.cmbРайон.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void cmbНП_TextChanged(object sender, EventArgs e)
        {
            ////Сотрём всё что попытался ввести пользователь пусть выберают из предлогаемого
            if (this.счётчикЗагрузки != 0)
            {
               this.cmbНП.DropDownStyle = ComboBoxStyle.DropDownList;
            }            
        }

        private void txtФИО_TextChanged(object sender, EventArgs e)
        {
            //откроем для редактирования поле ввода Имени
            if (this.txtФИО.Text.Length != 0)
            {
                this.textBox1.Enabled = true;
                this.textBox1.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox1.Enabled = false;
                this.textBox1.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length != 0)
            {
                this.textBox6.Enabled = true;
                this.textBox6.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox6.Enabled = false;
                this.textBox6.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.maskedTextBox3.Text.Length != 0)
            {
                this.textBox2.Enabled = true;
                this.textBox2.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox2.Enabled = false;
                this.textBox2.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Length != 0)
            {
                this.textBox5.Enabled = true;
                this.textBox5.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox5.Enabled = false;
                this.textBox5.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox5.Text.Length != 0)
            {
                this.maskedTextBox1.Enabled = true;
                this.maskedTextBox1.BackColor = Color.FromName("Window");
            }
            else
            {
                this.maskedTextBox1.Enabled = false;
                this.maskedTextBox1.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.maskedTextBox1.Text.Length != 0)
            {
                this.maskedTextBox2.Enabled = true;
                this.maskedTextBox2.BackColor = Color.FromName("Window");
            }
            else
            {
                this.maskedTextBox2.Enabled = false;
                this.maskedTextBox2.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.maskedTextBox2.Text.Length != 0)
            {
                this.maskedTextBox4.Enabled = true;
                this.maskedTextBox4.BackColor = Color.FromName("Window");
            }
            else
            {
                this.maskedTextBox4.Enabled = false;
                this.maskedTextBox4.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (this.maskedTextBox4.Text.Length != 0)
            {
                this.textBox8.Enabled = true;
                this.textBox8.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox8.Enabled = false;
                this.textBox8.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox8.Text.Length != 0)
            {
                this.textBox9.Enabled = true;
                this.textBox9.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox9.Enabled = false;
                this.textBox9.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox9.Text.Length != 0)
            {
                this.maskedTextBox5.Enabled = true;
                this.maskedTextBox5.BackColor = Color.FromName("Window");
            }
            else
            {
                this.maskedTextBox5.Enabled = false;
                this.maskedTextBox5.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void maskedTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (this.maskedTextBox5.Text.Length != 0)
            {
                this.textBox11.Enabled = true;
                this.textBox11.BackColor = Color.FromName("Window");
            }
            else
            {
                this.textBox11.Enabled = false;
                this.textBox11.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox11.Text.Length != 0)
            {
                this.number_snils.Enabled = true;
                this.number_snils.BackColor = Color.FromName("Window");
                this.button1.Enabled = true;
            }
            else
            {
                this.number_snils.Enabled = false;
                this.number_snils.BackColor = Color.FromName("Control");
                this.button1.Enabled = false;
            }        
        }

        private void number_snils_TextChanged(object sender, EventArgs e)
        {
             //Событие срабатывает только после загрузки данных в форму.
            if (flagEditSnils == true)
            {
                if (Regex.IsMatch(this.number_snils.Text, @"\d{3}-\d{3}-\d{3} \d{2}"))
                {
                    this.button1.Enabled = true;
                }
            }

            //if (this.number_snils.Text.Length != 0)
            //{
            //    this.button1.Enabled = true;
            //}
            //else if(this.number_snils.Text.Length == 11)
            //{
            //    if (Regex.IsMatch(this.number_snils.Text, @"\d{3}-\d{3}-\d{3} \d{2}"))
            //    {
            //        this.button1.Enabled = false;
            //    }
            //}
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.счётчикЗагрузки != 0)
            {
                this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

     
        // проверяемая дата, минимальная, максимальная
        private void control_date (string tb, int min, int max)
        {
            try
            {
                DateTime dt = DateTime.Today;
                dt = dt.AddYears(-60);
                dt = Convert.ToDateTime(tb);                
                DateTime year_min = DateTime.Today;
                year_min = year_min.AddYears(-min); //
                year_min = year_min.AddDays(1); //Прибавляем один день, чтобы учитывался сегоднешний.
                DateTime year_max= DateTime.Today;
                year_max = year_max.AddYears(-max);

                //MessageBox.Show(year_min.ToString());

                if (dt <= year_max || dt >= year_min)
                {
                    MessageBox.Show("Не верная дата.");
                }
            }
                //Обработка исключения не правильной даты.
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода даты.");
                //maskedTextBox3.Clear();
            }
        }        
        
        private void maskedTextBox3_Leave(object sender, EventArgs e)
        {
            control_date(maskedTextBox3.Text.ToString(), 55, 120);
        }
        private void maskedTextBox5_Leave(object sender, EventArgs e)
        {
            control_date(maskedTextBox5.Text.ToString(), 0, (Convert.ToInt32(DateTime.Today.Year) - 1989));//1989 чтобы весь 90-й год входил
        }

        private void maskedTextBox4_Leave(object sender, EventArgs e)
        {
            control_date(maskedTextBox4.Text.ToString(), 0, 75);// полагаем что максимальный возраст 120 лет
        }


        private void txtФИО_KeyPress(object sender, KeyPressEventArgs e)
        {
           ////Заменим Ё на Е
           // if (e.KeyChar == 'Ё' )
           // {
           //     e.KeyChar = 'Е';
           // }

           // //Заменим ё на е
           // if (e.KeyChar == 'ё')
           // {
           //     e.KeyChar = 'е';
           // }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////Заменим Ё на Е
            //if (e.KeyChar == 'Ё' )
            //{
            //    e.KeyChar = 'Е';
            //}

            ////Заменим ё на е
            //if (e.KeyChar == 'ё')
            //{
            //    e.KeyChar = 'е';
            //}
        }
        
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////Заменим Ё на Е
            //if (e.KeyChar == 'Ё' )
            //{
            //    e.KeyChar = 'Е';
            //}

            ////Заменим ё на е
            //if (e.KeyChar == 'ё')
            //{
            //    e.KeyChar = 'е';
            //}
        }

        //удаляем пробелы и тире
        private string RemoveSpacesDash(string inputString)
        {
            inputString = inputString.Replace("  ", string.Empty);
            inputString = inputString.Replace("-", string.Empty);
            inputString = inputString.Trim().Replace(" ", string.Empty);
            return inputString;
        }

        private void chech_lgotnik_Click(object sender, EventArgs e)
        {
            //данные для проверки; серия, номер документа, СНИЛС
            //string check = RemoveSpacesDash(textBox10.Text) + " " + RemoveSpacesDash(textBox9.Text) + " " 
            //               + RemoveSpacesDash(number_snils.Text);

            ListRegion lr = new ListRegion();
            string nameRegionTest = lr.FindRegion(5);


            string idTest = Crypto.Shifrovka("26");

            string stest = Crypto.DeShifrovka(idTest);

            if (RemoveSpacesDash(number_snils.Text) == "")
                MessageBox.Show("Заполните СНИЛС.","Предупреждение");
            else
                if (RemoveSpacesDash(number_snils.Text).Length < 11)
                    MessageBox.Show("Введите правильно СНИЛС.", "Предупреждение");
                else
                {
                    try
                    {
                        // Внутир конторы.
                        //WebRequest request = WebRequest.Create("https://10.159.102.10:8091/Home/PostData");

                        // Стучимся через внешний порторганизации.
                        WebRequest request = WebRequest.Create("http://217.23.79.222:3570/Home/PostData");

                        //WebRequest request = WebRequest.Create("http://localhost:3276/Home/PostData");
                        request.Method = "POST";
                       
                        // данные для отправки
                        string data = RemoveSpacesDash(this.number_snils.Text) + "_" + inn;
                        //string data = this.txtФИО.Text;

                        // ========Шифрование===============

                        // Получим ключ по текущей дате.
                        string sTest = DateTime.Today.ToShortDateString();

                        // Получим ключ.
                        byte[] dataKey = EncryptMessage.EncryptMessage.GetKey(sTest);//DateTime.Today.ToShortDateString()  //"12.04.1961"

                        // Создаем объект алгоритма симметричного ключа.
                        SymmetricAlgorithm myAlg = new RijndaelManaged();

                        // Запишем ключ.
                        myAlg.Key = dataKey;
                        myAlg.IV = dataKey;

                        // Получим зашифрованный массив с данными.
                        byte[] byteArrayEncrypt = EncryptMessage.EncryptMessage.Encrypt(data, myAlg.Key, myAlg.IV);

                        //===============================

                        // Преобразуем массив битов в строку.
                        StringBuilder builder = new StringBuilder();

                        for (int i = 0; i < byteArrayEncrypt.Length; i++)
                        {
                            builder.Append(byteArrayEncrypt[i].ToString("x2"));
                        }

                        // Присвоим переменной зашифрованные данные.
                        string dataText = "sName=" + builder.ToString() + " ";

                        // преобразуем данные в массив байтов
                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(dataText);

                        // устанавливаем тип содержимого - параметр ContentType
                        request.ContentType = "application/x-www-form-urlencoded";

                        // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
                        request.ContentLength = byteArray.Length;

                        //записываем данные в поток запроса
                        using (Stream dataStream = request.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);
                        }

                        WebResponse response = request.GetResponse();

                        using (Stream stream = response.GetResponseStream())
                        {

                            // Старый алгоритм действия.
                            using (StreamReader reader = new StreamReader(stream))
                            {                                
                                LgotnikReply newForm = new LgotnikReply();
                                newForm.label_reply.Text = reader.ReadToEnd();
                                newForm.ShowDialog();
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;

                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                            MessageBox.Show(httpResponse.StatusCode.ToString());
                        }
                    }
                }   
        }
    }
}