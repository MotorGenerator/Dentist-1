using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.Globalization;
using System.Threading;

namespace Стамотология
{
    public partial class FormУслуги : Form
    {
        private int id_поликлинника;
        //private DataTable dtCount; //количество сторк в таблице
        //List<ВидУслуг> lCount; //количесвто элементов с коллекции

        public FormУслуги()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormКлассификатор form = new FormКлассификатор();
            form.TopMost = true;
            form.ShowDialog();

            if (form.DialogResult == DialogResult.Cancel)
            {
                //обнулим список для обновления
                this.comboBox1.DataSource = null;

                //загрузим раскрывающийся список
                string queryКласс = "select id_кодУслуги,КлассификаторУслуги from КлассификаторУслуги";
                DataTable tab = ТаблицаБД.GetTable(queryКласс, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                this.comboBox1.DataSource = tab;
                this.comboBox1.ValueMember = "id_кодУслуги";
                this.comboBox1.DisplayMember = "КлассификаторУслуги";
            }

        }

        private void FormУслуги_Load(object sender, EventArgs e)
        {
            //загрузим раскрывающийся список
            string queryКласс = "select id_кодУслуги,КлассификаторУслуги from КлассификаторУслуги";
            DataTable tab = ТаблицаБД.GetTable(queryКласс, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

            this.comboBox1.DataSource = tab;
            this.comboBox1.ValueMember = "id_кодУслуги";
            this.comboBox1.DisplayMember = "КлассификаторУслуги";

            //Отобразим оказываемые услуги
            string queryУслуг = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(this.comboBox1.SelectedValue) + " ";

            //отобразим вид услуг
            this.dataGridView1.DataSource = ДанныеПредставление.GetListПредставление(queryУслуг, "ВидУслуг");

            ////скроем от пользователя не нужную информацию
            this.dataGridView1.Columns["id_услуги"].Visible = false;
            //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

            //установим поррядок отображения
            this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
            this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;
            this.dataGridView1.Columns["НомерПоПеречню"].Width = 80;

            this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
            this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;
            this.dataGridView1.Columns["ВидУслуги"].Width = 350;


            this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
            this.dataGridView1.Columns["Цена"].ReadOnly = true;
            this.dataGridView1.Columns["Цена"].Width = 90;


            this.dataGridView1.Columns["Выбрать"].Visible = false;
            this.dataGridView1.Columns["Количество"].Visible = false;

            //получим id_поликлинника
            string queryYjspt = "select id_поликлинника from Поликлинника";
            if (ТаблицаБД.GetTable(queryYjspt, ConnectionDB.ConnectionString(), "Поликлинника").Rows.Count != 0)
            {
                id_поликлинника = Convert.ToInt32(ТаблицаБД.GetTable(queryYjspt, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);
            }

            //Загрузим номера постановлений
            string queryPost = "select flag from Flag";
            DataTable tabPost = ТаблицаБД.GetTable(queryPost, ConnectionDB.ConnectionString(), "Flag");

            List<string> sList = new List<string>();

            //Запишем в коллекцию элменты кроме первого так как первый пункт это flag конфигурации
            int iCount = 0;
            foreach (DataRow r in tabPost.Rows)
            {
                if (iCount != 0)
                {
                    sList.Add(r[0].ToString().Trim());
                }
                iCount++;
            }

            //уберём первый пункт из списка 
            this.cmbPost.DataSource = sList;

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            //Установим в текущей культуре в качестве разделителя точку "."
            CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCInfo.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCInfo;


            
            if (this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.comboBox1.Text != "")
            {

                //string insertQuery = "insert into ВидУслуги(ВидУслуги,Цена,id_поликлинника,НомерПоПеречню,Выбрать,id_кодУслуги)values('" + this.textBox1.Text + "'," + this.textBox2.Text + "," + id_поликлинника + ",'" + this.textBox3.Text + "',False," + Convert.ToInt32(this.comboBox1.SelectedValue) + ")";
                string insertQuery = "insert into ВидУслуги(ВидУслуги,Цена,id_поликлинника,НомерПоПеречню,Выбрать,id_кодУслуги,ТехЛист)values('" + this.textBox1.Text + "'," + this.textBox2.Text + "," + id_поликлинника + ",'" + this.textBox3.Text + "',False," + Convert.ToInt32(this.comboBox1.SelectedValue) + ","+0+")";

                Query.Execute(insertQuery, ConnectionDB.ConnectionString());



                //обновим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(this.comboBox1.SelectedValue) + " ";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                //присоединим данные
                this.dataGridView1.DataSource = tab;

                ////скроем от пользователя не нужную информацию
                this.dataGridView1.Columns["id_услуги"].Visible = false;
                //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

                //установим поррядок отображения
                this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;
                this.dataGridView1.Columns["НомерПоПеречню"].Width = 80;

                this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
                this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;
                this.dataGridView1.Columns["ВидУслуги"].Width = 350;


                this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                this.dataGridView1.Columns["Цена"].ReadOnly = true;
                this.dataGridView1.Columns["Цена"].Width = 90;

                //скроем лишную информацию от пользователя
                this.dataGridView1.Columns["Выбрать"].Visible = false;
                this.dataGridView1.Columns["id_поликлинника"].Visible = false;
                this.dataGridView1.Columns["id_кодУслуги"].Visible = false;


                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";

            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению");
                this.textBox1.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "" && this.textBox2.Text.Trim() != "" && this.textBox3.Text.Trim() != "")
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);

                //UpdateБухгалтер bux = new UpdateБухгалтер(id, this.textBox1.Text);
                //Receiver rec = new Receiver();
                //rec.Action(bux);

                string queryUpdate = "update ВидУслуги "+
                                     "set ВидУслуги = '"+ this.textBox1.Text +"' " +
                                     ",Цена = " + Convert.ToDecimal(this.textBox2.Text) + " " +
                                     ",id_поликлинника = " + id_поликлинника + " " +
                                     ",НомерПоПеречню = '" + this.textBox3.Text + "' " +
                                     ",id_кодУслуги = " + Convert.ToInt32(this.comboBox1.SelectedValue) + " " +
                                     ",Выбрать = False " +
                                     ",ТехЛист = 0 " +
                                     "where id_услуги = "+ id +" ";

                //выполним запрос
                Query.Execute(queryUpdate, ConnectionDB.ConnectionString());



                //обновим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по услугам
                string query = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(this.comboBox1.SelectedValue) + " ";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                //присоединим данные
                this.dataGridView1.DataSource = tab;

                ////скроем от пользователя не нужную информацию
                this.dataGridView1.Columns["id_услуги"].Visible = false;
                //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

                //установим поррядок отображения
                this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;
                this.dataGridView1.Columns["НомерПоПеречню"].Width = 80;

                this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
                this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;
                this.dataGridView1.Columns["ВидУслуги"].Width = 350;


                this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                this.dataGridView1.Columns["Цена"].ReadOnly = true;
                this.dataGridView1.Columns["Цена"].Width = 90;

                //скроем лишную информацию от пользователя
                this.dataGridView1.Columns["Выбрать"].Visible = false;
                this.dataGridView1.Columns["id_поликлинника"].Visible = false;
                this.dataGridView1.Columns["id_кодУслуги"].Visible = false;


                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";

            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению");
                this.textBox1.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_услуги"].Value != DBNull.Value)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);
                string query = "select * from ВидУслуги where id_услуги = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ВидУслуги").Rows[0];

                this.textBox1.Text = row["ВидУслуги"].ToString();
                this.textBox2.Text = row["Цена"].ToString();
                this.textBox3.Text = row["НомерПоПеречню"].ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{
                //dtCount = (DataTable)this.dataGridView1.DataSource;
            //}
            //catch
            //{
            //    lCount = (List<ВидУслуг>)this.dataGridView1.DataSource;
            //}

            //if (dtCount.Rows.Count != 0 || this.dataGridView1.Rows.Count != 0)
            if (this.dataGridView1.Rows.Count != 0 && (this.textBox1.Text.Trim() != "" && this.textBox2.Text.Trim() != "" && this.textBox3.Text.Trim() != ""))
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);
                string query = "delete from ВидУслуги where id_услуги = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обновим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по услугам
                string queryUp = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(this.comboBox1.SelectedValue) + " ";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                //присоединим данные
                this.dataGridView1.DataSource = tab;

                ////скроем от пользователя не нужную информацию
                this.dataGridView1.Columns["id_услуги"].Visible = false;
                //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

                //установим поррядок отображения
                this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;
                this.dataGridView1.Columns["НомерПоПеречню"].Width = 80;

                this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
                this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;
                this.dataGridView1.Columns["ВидУслуги"].Width = 350;


                this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                this.dataGridView1.Columns["Цена"].ReadOnly = true;
                this.dataGridView1.Columns["Цена"].Width = 90;

                //скроем лишную информацию от пользователя
                this.dataGridView1.Columns["Выбрать"].Visible = false;
                this.dataGridView1.Columns["id_поликлинника"].Visible = false;
                this.dataGridView1.Columns["id_кодУслуги"].Visible = false;


                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("Не выбраны записи");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Заменим запятую на точку
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            //Разрешим ввести только числа и знак "."
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.')))// && (textBox2.Text.IndexOf(".") == -1) && (textBox2.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }

            ////Разобъём строку на массив строк
            //string[] array = this.textBox2.Text.Split('.');

            ////определяем количество элементов в массиве
            //int count = array.Length;

            ////Все символы кроме символа забой
            //if (e.KeyChar != (char)Keys.Back)
            //{
            //    //если длинна второй подстроки >= 2 то запрещаем вод следующего символа
            //    if (count >= 2)
            //    {
            //        if (array[1].Length >= 2)
            //        {
            //            e.Handled = true;
            //        }
            //    }
            //}

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedValue is int)
            {
                string id = this.comboBox1.SelectedValue.ToString();

                //Отобразим оказываемые услуги
                string queryУслуг = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(id) + " ";// order by asc";

                //отобразим вид услуг
                this.dataGridView1.DataSource = ДанныеПредставление.GetListПредставление(queryУслуг, "ВидУслуг");

                ////скроем от пользователя не нужную информацию
                this.dataGridView1.Columns["id_услуги"].Visible = false;
                //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

                //установим поррядок отображения
                this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;

                this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
                this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;

                this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                this.dataGridView1.Columns["Цена"].ReadOnly = true;

                this.dataGridView1.Columns["Количество"].DisplayIndex = 3;


            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Заменим запятую на точку
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }

            //Разрешим ввести только числа и знак "."
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.')))// && (textBox2.Text.IndexOf(".") == -1) && (textBox2.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }

        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            FormResolution fr = new FormResolution();
            fr.ShowDialog();
        }
    }
}