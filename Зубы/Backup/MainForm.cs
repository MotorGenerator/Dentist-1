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
using System.Diagnostics;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Xml;

using System.Runtime.Serialization.Formatters.Binary;

//using Стамотология.Classes;
using DantistLibrary;

namespace Стамотология
{
    public partial class MainForm : Form
    {
        private int id_льготник;
        private string фиоЛьготника;
        private int id_льготнойКатегории;
        private int id_льготникУдалить;
        private string фиоЛьготникУдалить;
        private int flag=0;
        private bool flagKeyЗапретСнятьАкт = true;

        ////Бибилиотека для храпнения выбранных договоров
        //Dictionary<string, string> library;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {            
            // Проверим есть ли ключевой файл, а так же разрешено ли в нем работать.
            //ValidateKeyFile();


            //Проверяем наличие таблица в бд FlagForLetter для хранения значения выгрузки проектов 
            //договоров в word-овский файл.
            //Если ее нет, то создаем ее. И ставаим значение по умолчанию 0 (т.е. выгрузку не делаем).
            table();

            // Проверим есть ли в таблице столбец FlagRegion, если его нет тогда добавим его в таблицу.
            //AlterTable();
            
            //проверим флаг в таблице Поликлинника
            string queryFlag = "select top 1 flag from Flag";

            if (ТаблицаБД.GetTable(queryFlag, ConnectionDB.ConnectionString(), "Flag").Rows.Count != 0)
            {
                flag = Convert.ToInt32(ТаблицаБД.GetTable(queryFlag, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);
                //проверим если flag = 1 то можно редактировать тарифы если flag = 0 то нельзя
            }                
            else
            { 
                return;
                //при отсутствии поключения к БД, загружаем пустую форму
            }

            if (flag == 0)
            {
                this.районToolStripMenuItem.Enabled = true;
            }

    
            //отобразим введённых льготников
            string query = "select * from Льготник";

            //добавляем столбец СНИЛС
            string query_add = "ALTER TABLE Льготник ADD  СНИЛС CHAR  NOT NULL";

            string queryAddRegion = "ALTER TABLE Льготник ADD  FlagRaion varchar(255) NULL";
           
            //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");

            DataTable dt = ДанныеПредставление.GetПредставление(query, "Льготник");

            if (!dt.Columns.Contains("FlagRaion"))
            {
                try
                {
                    string sCon = ConnectionDB.ConnectionString();
                    Query.Execute(queryAddRegion, sCon);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Сохраним в DataSet таблицу Льготники.
                dt = ДанныеПредставление.GetПредставление(query, "Льготник");

                this.dataGridView1.DataSource = dt;
            }

            // делаем проверку наличия СНИЛС
            if (!dt.Columns.Contains("СНИЛС"))
            {
                try
                {
                    string sCon = ConnectionDB.ConnectionString();
                    Query.Execute(query, sCon);
                    Query.Execute(query_add, sCon);
                    Query.Execute(queryAddRegion, sCon);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");
            }
            else

                // Если данные в 
                dataGridView1.DataSource = dt;
            
            //скроем не нужные для пользователя столбцы
            this.dataGridView1.Columns["id_льготник"].Visible = false;
            this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
            this.dataGridView1.Columns["id_документ"].Visible = false;
            this.dataGridView1.Columns["id_область"].Visible = false;
            this.dataGridView1.Columns["id_насПункт"].Visible = false;
            this.dataGridView1.Columns["id_район"].Visible = false;
            this.dataGridView1.Columns["FlagRaion"].Visible = false;



            // Запрет на добавление строк
            dataGridView1.AllowUserToAddRows = false;
            // Запрет на удаление строк
            dataGridView1.AllowUserToDeleteRows = false;

            //Проверм существует ли файл установки
            FileStream fs = File.Open("Config.dll",FileMode.OpenOrCreate);
            if (fs.Length != 0)
            {
                //Считываем из файла настройку на выгрузку
                ;
            }
            else
            {
                //Создаём файл и записываем настройку запретим выгрузку
                using (TextWriter writ = new StreamWriter(fs))
                {
                    writ.WriteLine("0");
                }
            }
            fs.Close();            

            this.WindowState = FormWindowState.Maximized;
            this.button5.Location = new System.Drawing.Point(410, 786);

            //Конвертнём базу (проверим id льготных категорий у льготников и в договоре)
            //Для этого создадим таблицу control состоящую из одной ячейки

            try
            {
                string selectFlag = "select Наличие from Control";
                DataTable tab = ТаблицаБД.GetTable(selectFlag, ConnectionDB.ConnectionString(), "ControlTab");

                //ни чего не делаем считаем что конвертировать не надо
            }
            catch
            {
                string createTable = "CREATE TABLE Control(Наличие nchar(10))";
                Query.Execute(createTable, ConnectionDB.ConnectionString());

                //так как таблицы нет значит конвертирования не было
                string queryЛьготник = "select id_льготник,id_льготнойКатегории from Льготник";
                DataTable tabЛьготник = ТаблицаБД.GetTable(queryЛьготник,ConnectionDB.ConnectionString(),"Льготник");

                foreach (DataRow row in tabЛьготник.Rows)
                {
                    string queryUpdate = "update Договор " +
                                         "set id_льготнаяКатегория = "+ Convert.ToInt32(row["id_льготнойКатегории"]) +" " +
                                         "where id_льготник = "+ Convert.ToInt32(row["id_льготник"]) +" " ;

                    Query.Execute(queryUpdate, ConnectionDB.ConnectionString());
                }
            }

            //if (System.Configuration.ConfigurationSettings.AppSettings["OnlySatartov"] == "1")
            if (System.Configuration.ConfigurationManager.AppSettings["OnlySatartov"] == "1")
            {
                foreach (ToolStripItem item in this.menuStrip1.Items)
                {
                    if (item.Text == "Справочник")
                    {
                        item.Visible = true;
                    }
                }
            }
            else 
                //if (System.Configuration.ConfigurationSettings.AppSettings["OnlySatartov"] == "0")
                if (System.Configuration.ConfigurationManager.AppSettings["OnlySatartov"] == "0")
                {
                    foreach (ToolStripItem item in this.menuStrip1.Items)
                    {
                        if (item.Text == "Справочник")
                        {
                            item.Visible = false;
                        }
                    }
                }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //отобразим введённых льготников
            string query = "select * from Льготник where Фамилия like '" + this.txtФИО.Text + "%'";
            this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");

            //скроем не нужные для пользователя столбцы
            this.dataGridView1.Columns["id_льготник"].Visible = false;
            this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
            this.dataGridView1.Columns["id_документ"].Visible = false;
            this.dataGridView1.Columns["id_область"].Visible = false;
            this.dataGridView1.Columns["id_насПункт"].Visible = false;
            this.dataGridView1.Columns["id_район"].Visible = false;
            this.dataGridView1.Columns["FlagRaion"].Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
       }

        private void button1_Click(object sender, EventArgs e)
        {
            FormЛьготник льготник = new FormЛьготник();
            //установим флагн на добавление записи
            льготник.FlagUpdate = false;
            //льготник.Show();
            льготник.ShowDialog();
            if (льготник.DialogResult == DialogResult.OK)
            {
                //Обновим данные
                this.dataGridView1.DataSource = null;
                string query = "select * from Льготник";
                this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");

                //скроем не нужные для пользователя столбцы
                this.dataGridView1.Columns["id_льготник"].Visible = false;
                this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
                this.dataGridView1.Columns["id_документ"].Visible = false;
                this.dataGridView1.Columns["id_область"].Visible = false;
                this.dataGridView1.Columns["id_насПункт"].Visible = false;
                this.dataGridView1.Columns["id_район"].Visible = false;
                this.dataGridView1.Columns["FlagRaion"].Visible = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Узнаем есть 

                id_льготник = (int)this.dataGridView1.CurrentRow.Cells["id_льготник"].Value;
                string фамилия = this.dataGridView1.CurrentRow.Cells["Фамилия"].Value.ToString();


                string имя = this.dataGridView1.CurrentRow.Cells["Имя"].Value.ToString();
                string отчество = this.dataGridView1.CurrentRow.Cells["Отчество"].Value.ToString();

                //Сформируем ФИО льготника
                фиоЛьготника = фамилия + " " + имя + " " + отчество;


                //получим id льготной категории 
                if (this.dataGridView1.CurrentRow.Cells["id_льготнойКатегории"].Value != DBNull.Value)
                {
                    id_льготнойКатегории = (int)this.dataGridView1.CurrentRow.Cells["id_льготнойКатегории"].Value;
                }
            }
            catch
            {
                MessageBox.Show("Введите данные по льготнику");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string iTest = ConnectionDB.ConnectionString();

            FormЛьготник льготник = new FormЛьготник();
            //установим флагн на добавление записи
            льготник.FlagUpdate = true;

            //string sTest = this.dataGridView1.CurrentRow.Cells["id_льготник"].Value.ToString();

            //DataGridViewSelectedRowCollection rows = this.dataGridView1.SelectedRows;
            //foreach (DataGridViewRow row in rows)
            //{
            //    string s = row.Cells["id_льготник"].Value.ToString();
            //}
            
            //передадим id льготника для изменения
            //льготник.Id_льготникUpdate = id_льготник;
            int iCountЛьготник  = Convert.ToInt32(ТаблицаБД.GetTable("select Count(*) from Льготник",ConnectionDB.ConnectionString(),"Льготник").Rows[0][0]);

            if (iCountЛьготник != 0)
            {
                льготник.Id_льготникUpdate = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_льготник"].Value);


                льготник.ShowDialog();
                //льготник.Show();
                if (льготник.DialogResult == DialogResult.OK)
                {
                    //Обновим данные
                    this.dataGridView1.DataSource = null;
                    string query = "select * from Льготник";
                    this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");

                    //скроем не нужные для пользователя столбцы
                    this.dataGridView1.Columns["id_льготник"].Visible = false;
                    this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
                    this.dataGridView1.Columns["id_документ"].Visible = false;
                    this.dataGridView1.Columns["id_область"].Visible = false;
                    this.dataGridView1.Columns["id_насПункт"].Visible = false;
                    this.dataGridView1.Columns["id_район"].Visible = false;
                    this.dataGridView1.Columns["FlagRaion"].Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Нет данных по льготнику");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string queryВрач = "select Count(*) from Поликлинника";
                int врач = Convert.ToInt32(ТаблицаБД.GetTable(queryВрач, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);

                string queryТеррОрган = "select Count(*) from Комитет";
                int трОргн = Convert.ToInt32(ТаблицаБД.GetTable(queryТеррОрган, ConnectionDB.ConnectionString(), "Комитет").Rows[0][0]);

                string queryКлассУслуг = "select Count(*) from КлассификаторУслуги";
                int клУслуг = Convert.ToInt32(ТаблицаБД.GetTable(queryКлассУслуг, ConnectionDB.ConnectionString(), "КлассификаторУслуг").Rows[0][0]);

                string queryВидУслуг = "select Count(*) from ВидУслуги";
                int видУслуг = Convert.ToInt32(ТаблицаБД.GetTable(queryВидУслуг, ConnectionDB.ConnectionString(), "КлассификаторУслуг").Rows[0][0]);

                //получим id льготной категории 
                if (this.dataGridView1.CurrentRow.Cells["id_льготнойКатегории"].Value != DBNull.Value)
                {
                    id_льготнойКатегории = (int)this.dataGridView1.CurrentRow.Cells["id_льготнойКатегории"].Value;
                }
            }
            catch
            {
                MessageBox.Show("Не заполнены данные по поликлиннике, территориальному органу или услугам поликлинники");
            }

            //try
            //{
                FormДоговор fDoc = new FormДоговор();
                fDoc.FlagKeyЗапретСнятьАкт = this.flagKeyЗапретСнятьАкт;

                //fDoc.Id_Льготник = id_льготник;
                fDoc.Id_Льготник = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_льготник"].Value);

                if (ValidateRegion(fDoc.Id_Льготник) == true)
                {
                    string фамилия = this.dataGridView1.CurrentRow.Cells["Фамилия"].Value.ToString();
                    string имя = this.dataGridView1.CurrentRow.Cells["Имя"].Value.ToString();
                    string отчество = this.dataGridView1.CurrentRow.Cells["Отчество"].Value.ToString();

                    //Сформируем ФИО льготника
                    фиоЛьготника = фамилия + " " + имя + " " + отчество;

                    fDoc.ФИО_Льготника = фиоЛьготника;
                    fDoc.IdЛьготнойКатегории = id_льготнойКатегории;
                    fDoc.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Задайте у льготника район проживания через кнопку изменить данные польготнику");
                }
            //}
            //catch
            //{
            //    MessageBox.Show("Введите данные по льготнику");
            //}
        }

        private void btnРеестр_Click(object sender, EventArgs e)
        {
            FormReestrPrint formReestr = new FormReestrPrint();
            formReestr.РеестрЗаключенныхДоговоров = true;
            formReestr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReestrPrint formReestr = new FormReestrPrint();
            formReestr.Show();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridViewRow rows = this.dataGridView1.Rows[e.RowIndex];

                //запишем id выбранного льготника

                id_льготникУдалить = Convert.ToInt32(rows.Cells["id_льготник"].Value);
                фиоЛьготникУдалить = rows.Cells["Фамилия"].Value.ToString().Trim();

                this.dataGridView1.ClearSelection();

                if (e.ColumnIndex >= 0)
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;
                }   
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ////if (e.Button == MouseButtons.Right)
            ////{
            //    if (e.RowIndex == -1) return;
            //    int prCurrentRow = e.RowIndex;
            //    if (e.Button.ToString().ToLower().Equals("left")) return;
            //    Point p = MousePosition;
            //    this.dataGridView1.Rows[e.RowIndex].Selected = true;
            //    this.contextMenuStrip1.Show(p.X, p.Y);
            ////}

        }

        /// <summary>
        /// Проверка пользователя на район проживания.
        /// </summary>
        /// <param name="idPerson"></param>
        /// <returns></returns>
        private bool ValidateRegion(int idPerson)
        {
            // Запрос на получение ID района.
            string queryНT = "select FlagRaion from Льготник where id_льготник = " + idPerson + " ";

            DataTable tabRegion = ТаблицаБД.GetTable(queryНT, ConnectionDB.ConnectionString(),"TabRegion");

            string idRegion = Crypto.DeShifrovka(tabRegion.Rows[0][0].ToString().Trim());

            int idResult = 0;

            return int.TryParse(idRegion, out idResult);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
               bool flagError = false;

                FormДоговор fDoc = new FormДоговор();
                fDoc.FlagKeyЗапретСнятьАкт = this.flagKeyЗапретСнятьАкт;

                //fDoc.Id_Льготник = id_льготник;
                fDoc.Id_Льготник = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_льготник"].Value);

                //// Запрос на получение ID района.
                //string queryНT = "select FlagRaion from Льготник where id_льготник = " + fDoc.Id_Льготник + " ";

                //DataTable tabRegion = ТаблицаБД.GetTable(queryНT, ConnectionDB.ConnectionString(),"TabRegion");

                //string idRegion = Crypto.DeShifrovka(tabRegion.Rows[0][0].ToString().Trim());

                //int idResult = 0;

                //if (int.TryParse(idRegion, out idResult) == true)
                if(ValidateRegion(fDoc.Id_Льготник) == true)
                {
                    string фамилия = this.dataGridView1.CurrentRow.Cells["Фамилия"].Value.ToString();
                    string имя = this.dataGridView1.CurrentRow.Cells["Имя"].Value.ToString();
                    string отчество = this.dataGridView1.CurrentRow.Cells["Отчество"].Value.ToString();

                    // Записываем фИО для проверки наличия договора
                    fDoc.familiya = фамилия;
                    fDoc.imya = имя;
                    fDoc.otchestvo = отчество;

                    //Сформируем ФИО льготника
                    фиоЛьготника = фамилия + " " + имя + " " + отчество;

                    fDoc.ФИО_Льготника = фиоЛьготника;
                    fDoc.IdЛьготнойКатегории = id_льготнойКатегории;
                    fDoc.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Задайте у льготника район проживания через кнопку изменить данные польготнику");
                }
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FormReestrPrint formReestr = new FormReestrPrint();
            //Передадим флаг что выводим реестр заключённых договоров
            formReestr.РеестрЗаключенныхДоговоров = false;
            formReestr.Show();
        }

        private void contextMenuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditHospital hospital = new FormEditHospital();
            //hospital.MdiParent = this;
            hospital.WindowState = FormWindowState.Normal;
            hospital.Show();
        }

        private void реквизитыПоликлинникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditHospital hospital = new FormEditHospital();
            //hospital.MdiParent = this;
            hospital.Show();
            hospital.WindowState = FormWindowState.Normal;
        }

        private void населённыйПунктToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormРайон район = new FormРайон();
            район.Show();

        }

        private void населенныйПуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormНаселПункт населённыйПункт = new FormНаселПункт();
            населённыйПункт.Show();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Проверяем, сделан лм клик по льготнику (не в конце талицы, серый фон)
            if (фиоЛьготникУдалить == null)
                return; 

            FormModal modal = new FormModal();
            modal.ФИО = фиоЛьготникУдалить;
            modal.ShowDialog();

            if (modal.DialogResult == DialogResult.OK)
            {
                ////удалим запись
                //string query = "declare @id_договор int " +
                //"select id_договор from Договор where id_льготник = " + id_льготникУдалить + " " +
                string query1 = "delete from УслугиПоДоговору where id_договор in (select id_договор from Договор where id_льготник = " + id_льготникУдалить + " ) ";
                string query2 = "delete from ДопСоглашение where id_договор in (select id_договор from Договор where id_льготник = " + id_льготникУдалить + " ) ";
                string query3 = "delete from АктВыполнненныхРабот where id_договор in (select id_договор from Договор where id_льготник = " + id_льготникУдалить + " ) ";
                string query4 = "delete from Договор where id_льготник = " + id_льготникУдалить + " ";
                string query5 = "delete from Льготник where id_льготник = " + id_льготникУдалить + " ";

                //Acess не понимает большие запросы делаем безумие
                Query.Execute(query1, ConnectionDB.ConnectionString());
                Query.Execute(query2, ConnectionDB.ConnectionString());
                Query.Execute(query3, ConnectionDB.ConnectionString());
                Query.Execute(query4, ConnectionDB.ConnectionString());
                Query.Execute(query5, ConnectionDB.ConnectionString());

                //обновим список льготников
                string queryUpdate = "select * from Льготник";
                this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(queryUpdate, "Льготник");

                //скроем не нужные для пользователя столбцы
                this.dataGridView1.Columns["id_льготник"].Visible = false;
                this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
                this.dataGridView1.Columns["id_документ"].Visible = false;
                this.dataGridView1.Columns["id_область"].Visible = false;
                this.dataGridView1.Columns["id_насПункт"].Visible = false;
                this.dataGridView1.Columns["id_район"].Visible = false;
                this.dataGridView1.Columns["FlagRegion"].Visible = false;
            }
        }

        private void классификаторУслугToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormУслуги услуги = new FormУслуги();
            услуги.Show();
        }

        private void реквизитыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormТеррОрган террОрган = new FormТеррОрган();
            террОрган.Show();
            террОрган.WindowState = FormWindowState.Normal;
        }

        private void подключитьГуглДискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Process ieProcess = Process.Start("IExplore.exe", "https://docs.google.com/folder/d/0Bwz6aPp9jBt8QW54OUxmS2lxdHM/edit?usp=sharing");
            //Process ieProcess = Process.Start("https://docs.google.com/folder/d/0Bwz6aPp9jBt8QW54OUxmS2lxdHM/edit?usp=sharing");
            Process ieProcess = Process.Start("https://drive.google.com/folderview?id=0Bwz6aPp9jBt8QW54OUxmS2lxdHM&usp=sharing");
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            int y = this.Size.Height;
            int x = this.Size.Width;
            this.button5.Location = new Point(x/3,y-58);
        }

        private void button5_Click(object sender, EventArgs e)
        {
             string queryUpdateOn = "update FlagForLetter " +
                                        "set flag = 1 ";

             Query.Execute(queryUpdateOn, ConnectionDB.ConnectionString());

            // Форма со списком льготных категорий.
            FormProjecContract formSelectCategor = new FormProjecContract();
            formSelectCategor.ShowDialog();

            if(formSelectCategor.DialogResult == DialogResult.OK)
            {
                // Форма со списком проектов договоров.
                FormListContract formContract = new FormListContract();
                formContract.IdЛьготнаяКатегория = formSelectCategor.IdЛьготнаяКатегория;
                formContract.Show();
            }
        }

        private void выгрузитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Бибилиотека для храпнения выбранных договоров
            Dictionary<string, string> library = new Dictionary<string, string>();

            //Заполним бибилиоекту liubrare номерами всех договоров
            using (OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString()))
            {
                con.Open();
                OleDbTransaction transact = con.BeginTransaction();

                //Получим номера проектов договоров
                //string queryContr = "select НомерДоговора from Договор where ДатаДоговора IS NULL ";

                //Выгрузим всё
                string queryContr = "select НомерДоговора from Договор";// where ДатаДоговора IS NULL ";

                //string queryContr = "SELECT Договор.НомерДоговора AS НомерДоговора, ДопСоглашение.НомерДопСоглашения AS НомерДопСоглашения " +
                //                    "FROM Договор INNER JOIN ДопСоглашение ON Договор.id_договор = ДопСоглашение.id_договор " +
                //                    "WHERE (((Договор.ДатаДоговора) Is Null) AND ((ДопСоглашение.НомерДопСоглашения) Is Null)); ";

                DataTable tabContr = ТаблицаБД.GetTable(queryContr, "НомераДоговоров", con, transact);

                int countRows = tabContr.Rows.Count;

                //Заполним коллекцию 
                List<ReestrПроектДоговорр> list = new List<ReestrПроектДоговорр>();

                //Счётчик для хранения порядковых номеров
                //int iCount = 1;

                //счётчик циклов для установки номера задублированного договора
                int iCountD = 1;
                foreach (DataRow row in tabContr.Rows)
                {
                    try
                    {
                        //получим нгомер договора
                        string num = row["НомерДоговора"].ToString().Trim();

                        library.Add(num, num);
                    }
                    catch
                    {
                        //получим номер договора дубликат
                        string numW = row["НомерДоговора"].ToString().Trim() + "_" + iCountD.ToString();
                        
                        string num = row["НомерДоговора"].ToString().Trim();
                        library.Add(numW, num);
                        
                        //увеличим на 1
                        iCountD++;
                    }
                }
            }

            UnloadReestr reestr = new UnloadReestr();

            //установим что происходит вся выгрузка договоров
            reestr.FlagВыгрузка = true;
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

                //Получим название поликлинники

                //Получим красивое название файла
                saveFile.FileName = поликлинника + "_Выгрузка_данных_" + дата + ".r";

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

                //Установим для нашей программы текущую директорию для корректного считывания пути к БД
                Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;

                //закроем окно выбора договоров
                this.Close();
            }
        }

        private void webТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWeb web = new FormWeb();
            web.Show();
        }

        private void печатьРеестровДоговоровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPrintContract form = new FormPrintContract();
            form.Show();
        }

        private void врачиПротезистыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Проверим существует ли таблица Врач.
            string queryCreateTable = "CREATE TABLE Врач ( " +
                                      " id_врач   counter(1,1) primary key, " +
                                      " ФИО    CHAR( 50 ) NULL )";

            string querySelectTable = "select * from Врач";

            // Проверим существует ли таблицв Врач
            try
            {
                Query.Execute(querySelectTable, ConnectionDB.ConnectionString());
            }
            catch
            {
                // Если такой таблицы нет значит сосздаём таблицу Врач.
                Query.Execute(queryCreateTable, ConnectionDB.ConnectionString());

                // Вносим изменения в таблицу Договор.
                string queryUpdateTable = "ALTER TABLE Договор  " +
                                          "ADD " +
                                          " id_врач   INT  null, " +
                                          " НомерТехЛиста    CHAR( 50 ) NULL ";

                Query.Execute(queryUpdateTable, ConnectionDB.ConnectionString());

                // Таблица где будут храниться 
                string queryEsculapPaum = "CREATE TABLE ВрачОплата " +
                               "( id  counter(1,1) primary key, " +
                               " idДоговор INT ,  " +
                               " IGuid CHAR( 50 ) NULL, " +
                               " СчётФактура CHAR(30), " +
                               " Дата DATE ) ";

                Query.Execute(queryEsculapPaum, ConnectionDB.ConnectionString());
            }
            FormВрач form = new FormВрач();
            form.Show();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFindContract formFind = new FormFindContract();
            formFind.ShowDialog();

            if (formFind.DialogResult == DialogResult.OK)
            {
                this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(formFind.StringQuery, "Льготник");

                //скроем не нужные для пользователя столбцы
                this.dataGridView1.Columns["id_льготник"].Visible = false;
                this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
                this.dataGridView1.Columns["id_документ"].Visible = false;
                this.dataGridView1.Columns["id_область"].Visible = false;
                this.dataGridView1.Columns["id_насПункт"].Visible = false;
                this.dataGridView1.Columns["id_район"].Visible = false;
                this.dataGridView1.Columns["FlagRaion"].Visible = false;
            }
            else
            {
                return;
            }
        }

        private void ValidateKeyFile()
        {
            string fileName = System.Windows.Forms.Application.StartupPath.ToString() + @"\" + "KeyDantist.k";

            ClassLibrary1.KeyHospital key;// = new KeyHospital();

            //SerializableObject objToSerialize = null;
            //FileStream fstream = File.Open(fileName, FileMode.Open);
            try
            {
                FileStream fstream = File.Open("KeyDantist.k", FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //Получим из файла словарь с договорами
                key = (ClassLibrary1.KeyHospital)binaryFormatter.Deserialize(fstream);

                //Закроем файловый поток
                fstream.Close();

                if (key.Flag == false)
                {
                    //flagKeyЗапретСнятьАкт = false;

                    // Проверим разность записей в key файле и в app.config.
                    //string keyGuid = ConfigurationSettings.AppSettings["keyDantist"].ToString().Trim();
                    string keyGuid = ConfigurationManager.AppSettings["keyDantist"].ToString().Trim();

                    if (keyGuid.Trim() != key.GuidValue.ToString().Trim())
                    {
                        flagKeyЗапретСнятьАкт = false;

                        bool flag = ConfigFile.WriteSetting("keyDantist", key.GuidValue.ToString().Trim());
                        //string шЕуые = "Запишем в app.config файл значение guid";
                    }
                    else
                    {
                        // Запретим редактирование.
                        flagKeyЗапретСнятьАкт = true;
                    }
                }
                else
                {
                    this.button3.Visible = false;

                    // Запретим редактирование.
                    flagKeyЗапретСнятьАкт = true;
                }
            }
            catch
            {
                MessageBox.Show("Отсутствует ключевой файл");
            }
        }

        public static void table()
        {
            try
            {
                string query = "SELECT * FROM FlagForLetter; ";
                ДанныеПредставление.GetПредставление(query, "FlagForLetter");                
            }
            catch (Exception /*e*/)
            {
                //try
                //{
                    string createTable = "CREATE TABLE FlagForLetter(flag int)";
                    Query.Execute(createTable, ConnectionDB.ConnectionString());

                    string query = "insert into FlagForLetter (flag) values( 0 )";
                    string sCon = ConnectionDB.ConnectionString();
                    Query.Execute(query, sCon);
                //}
                //catch (Exception ex)                
                //{
                //    MessageBox.Show("Ошибка подключения к базе данныхю " + ex.Message);
                //}        
            }
        }

        /// <summary>
        /// Вносим измениен в таблицу.
        /// </summary>
        public static void AlterTable()
        {
            try
            {
                // Проверим есть ли в таблице поле с флагом выбора района.
                string query = "select FlagRegion from Льготник";
                ДанныеПредставление.GetПредставление(query, "FlagRegion");  
            }
            catch
            {
                // Если выбрашено исключениетогда добавим в таблицу ещё один столбец.
                string queryAlter = "ALTER TABLE Льготник " +
                                    "ADD FlagRegion varchar(255)";

                string sCon = ConnectionDB.ConnectionString();
                Query.Execute(queryAlter, sCon);
            }

        }

        private void включитьСопроводительноеПисьмоДляВыгрузкиПроектовДоговоровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string MessageOn = "Включить печать сопроводительного письма?";
            string queryUpdateOn = "update FlagForLetter " +
                                        "set flag = 1 ";

            string MessageOff = "Выключить печать сопроводительного письма?";
            string queryUpdateOff = "update FlagForLetter " +
                                        "set flag = 0 ";

            string query = "SELECT flag FROM FlagForLetter; ";
            DataTable dt = new DataTable();
            dt = ДанныеПредставление.GetПредставление(query, "FlagForLetter");
            string test = dt.Rows[0][0].ToString();

            if (test == "0")
            {
                if (MessageBox.Show(MessageOn, "Включение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Query.Execute(queryUpdateOn, ConnectionDB.ConnectionString());
            }
            else
            {
                if (MessageBox.Show(MessageOff, "Отключение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Query.Execute(queryUpdateOff, ConnectionDB.ConnectionString());
            } 
        }

        private void свяжитесьСНамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp form = new FormHelp();
            form.Show();
        }
    }
}