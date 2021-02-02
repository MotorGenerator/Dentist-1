using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.Configuration;

namespace Стамотология
{
    public partial class FormВидУслуги : Form
    {
        private int id_договор;
        private List<ВидУслуг> list;
        private Dictionary<int, ВидУслуг> listLibrary;

        /// <summary>
        /// Хранит id договора
        /// </summary>
        public int IdДоговор
        {
            get
            {
                return id_договор;
            }
            set
            {
                id_договор = value;
            }
        }

        public FormВидУслуги()
        {
            InitializeComponent();
        }

        private void FormВидУслуги_Load(object sender, EventArgs e)
        {
            ////отобразим введённых льготников
            //string query = "select * from Льготник where id_льготник = " + Id_Льготник + " ";
            //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(query, "Льготник");

            ////скроем не нужные для пользователя столбцы
            //this.dataGridView1.Columns["id_льготник"].Visible = false;
            //this.dataGridView1.Columns["id_льготнойКатегории"].Visible = false;
            //this.dataGridView1.Columns["id_документ"].Visible = false;

            //Заполним классификатор услуг
            string queryКласс = "select * from КлассификаторУслуги";
            this.comboBox1.DataSource = ДанныеПредставление.GetПредставление(queryКласс, "КлассификаторУслуги");
            this.comboBox1.ValueMember = "id_кодУслуги";
            this.comboBox1.DisplayMember = "КлассификаторУслуги";


            //Отобразим оказываемые услуги
            string queryУслуг = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(this.comboBox1.SelectedValue) + " ";

            //отобразим вид услуг
            this.dataGridView1.DataSource = ДанныеПредставление.GetListПредставление(queryУслуг, "ВидУслуг");
            
            //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(queryУслуг, "ВидУслуг");

            //this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;
            //this.dataGridView1.Columns["Цена"].ReadOnly = true;
            //this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;

            //this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
            //this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
            //this.dataGridView1.Columns["Цена"].DisplayIndex = 2;

            ////скроем от пользователя не нужную информацию
            this.dataGridView1.Columns["id_услуги"].Visible = false;
            //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

            //bool flagElse = Convert.ToBoolean(ConfigurationSettings.AppSettings["orderEngls"]);
            bool flagElse = Convert.ToBoolean(ConfigurationManager.AppSettings["orderEngls"]);

            if (flagElse == true)
            {
                //// Для Энгелься========
                //установим поррядок отображения
                this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;

                this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 3;
                this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;

                this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                this.dataGridView1.Columns["Цена"].ReadOnly = true;

                this.dataGridView1.Columns["Количество"].DisplayIndex = 1;

                this.dataGridView1.Columns["Выбрать"].DisplayIndex = 4;
                this.dataGridView1.Columns["Выбрать"].Visible = false;
            }
            else
            {

                //=====================

                //установим поррядок отображения
                this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;

                this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
                this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;

                this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                this.dataGridView1.Columns["Цена"].ReadOnly = true;

                this.dataGridView1.Columns["Выбрать"].DisplayIndex = 4;
                this.dataGridView1.Columns["Выбрать"].Visible = false;

                this.dataGridView1.Columns["Количество"].ReadOnly = false;
                this.dataGridView1.Columns["Количество"].DisplayIndex = 3;
            }

            

            //создадим список для хранения выделенных элементов
            list = new List<ВидУслуг>();
            listLibrary = new Dictionary<int, ВидУслуг>();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (ВидУслуг видУслуг in listLibrary.Values)
            {
                //string queryInsert = "insert into УслугиПоДоговору(НаименованиеУслуги,Цена,Количество,id_договор,НомерПоПеречню,Сумма)values('" + видУслуг.ВидУслуги + "','" + видУслуг.Цена + "'," + видУслуг.Количество + "," + this.IdДоговор + ",'" + видУслуг.НомерПоПеречню + "'," + Convert.ToString(Math.Round(видУслуг.Цена * видУслуг.Количество,2)).Replace(',','.') + ");";
                string queryInsert = "insert into УслугиПоДоговору(НаименованиеУслуги,Цена,Количество,id_договор,НомерПоПеречню,Сумма,ТехЛист)values('" + видУслуг.ВидУслуги + "','" + видУслуг.Цена + "'," + видУслуг.Количество + "," + this.IdДоговор + ",'" + видУслуг.НомерПоПеречню + "'," + Convert.ToString(Math.Round(видУслуг.Цена * видУслуг.Количество, 2)).Replace(',', '.') + ","+ 0 +");";
                string sCon = ConnectionDB.ConnectionString();
                Query.Execute(queryInsert, sCon);
            }
        }

 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox1.SelectedValue is int)
            {
                string id = this.comboBox1.SelectedValue.ToString();

                //Отобразим оказываемые услуги
                string queryУслуг = "select * from ВидУслуги where id_кодУслуги = " + Convert.ToInt32(id) + " ";// order by asc";

                //отобразим вид услуг
                this.dataGridView1.DataSource = ДанныеПредставление.GetListПредставление(queryУслуг, "ВидУслуг");

                //this.dataGridView1.DataSource = ДанныеПредставление.GetПредставление(queryУслуг, "ВидУслуг");

                //this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;
                //this.dataGridView1.Columns["Цена"].ReadOnly = true;
                //this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;

                //this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                //this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 1;
                //this.dataGridView1.Columns["Цена"].DisplayIndex = 2;

                ////скроем от пользователя не нужную информацию
                //this.dataGridView1.Columns["id_услуги"].Visible = false;
                //this.dataGridView1.Columns["id_поликлинника"].Visible = false;
                //this.dataGridView1.Columns["id_кодУслуги"].Visible = false;

                ////скроем от пользователя не нужную информацию
                this.dataGridView1.Columns["id_услуги"].Visible = false;
                //this.dataGridView1.Columns["id_поликлинника"].Visible = false;

                 //bool flagElse = Convert.ToBoolean(ConfigurationSettings.AppSettings["orderEngls"]);
                bool flagElse = Convert.ToBoolean(ConfigurationManager.AppSettings["orderEngls"]);

                 if (flagElse == true)
                 {

                     // Для Энгелься========
                     //установим поррядок отображения
                     this.dataGridView1.Columns["НомерПоПеречню"].DisplayIndex = 0;
                     this.dataGridView1.Columns["НомерПоПеречню"].ReadOnly = true;

                     this.dataGridView1.Columns["ВидУслуги"].DisplayIndex = 3;
                     this.dataGridView1.Columns["ВидУслуги"].ReadOnly = true;

                     this.dataGridView1.Columns["Цена"].DisplayIndex = 2;
                     this.dataGridView1.Columns["Цена"].ReadOnly = true;

                     this.dataGridView1.Columns["Количество"].DisplayIndex = 1;

                     this.dataGridView1.Columns["Выбрать"].DisplayIndex = 4;
                     this.dataGridView1.Columns["Выбрать"].Visible = false;
                 }
                 else
                 {
                     //==================
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
        }

     
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////отследим изменения в DatatGridView
            //DataGridViewCell col = this.dataGridView1.CurrentCell;

            ////Определим тип выбранной ячейки
            //Type t = col.GetType();

            ////if(t.ToSting())

            ////если пользователь нажал checkbox 
            //if (t.ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
            //{
            //    //================================пометсим полученное значение в словарь
            //    //получим id записи
            //    int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);

            //    ВидУслуг видУлуг = new ВидУслуг();
            //    видУлуг.ВидУслуги = this.dataGridView1.CurrentRow.Cells["ВидУслуги"].Value.ToString();

            //    видУлуг.НомерПоПеречню = this.dataGridView1.CurrentRow.Cells["НомерПоПеречню"].Value.ToString();
            //    видУлуг.Цена = Convert.ToDecimal(this.dataGridView1.CurrentRow.Cells["Цена"].Value);

            //    видУлуг.Количество = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Количество"].Value);

            //    //запишем в словарь
            //    try
            //    {
            //        //проверим 
            //        if (видУлуг.Количество != 0)
            //        {
            //            listLibrary.Add(id, видУлуг);
            //        }
            //        else
            //        {
            //            //если флажок выбрали но число не выставили тогда флажок сбросим
            //            col.Value = null;
            //        }
            //    }
            //    catch
            //    {
            //        //если пользователь нажал ещё раз выбрал выделенное поле и ключ не записался значит пользователь хочет снять флажок
            //        listLibrary.Remove(id);
            //    }

            //}
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = (TextBox)e.Control;
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            }

        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(Char.IsDigit(e.KeyChar)) && (e.KeyChar != '.'))
            //{
            //    if (e.KeyChar != (char)Keys.Back)
            //    { e.Handled = true; }
            //}


            if (!(Char.IsNumber(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                { e.Handled = true; }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //отследим изменения в DatatGridView
            DataGridViewCell col = this.dataGridView1.CurrentCell;

            //Определим тип выбранной ячейки
            Type t = col.GetType();

            //if(t.ToSting())

            //если пользователь нажал checkbox 
            //if (t.ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
            if (col == this.dataGridView1.CurrentRow.Cells["Количество"] && Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Количество"].Value) != 0)
            {
                //================================пометсим полученное значение в словарь

                //получим id записи
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);

                ВидУслуг видУлуг = new ВидУслуг();
                видУлуг.ВидУслуги = this.dataGridView1.CurrentRow.Cells["ВидУслуги"].Value.ToString();

                видУлуг.НомерПоПеречню = this.dataGridView1.CurrentRow.Cells["НомерПоПеречню"].Value.ToString();
                видУлуг.Цена = Convert.ToDecimal(this.dataGridView1.CurrentRow.Cells["Цена"].Value);

                видУлуг.Количество = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Количество"].Value);

                //запишем в словарь
                try
                {
                    //проверим 
                    if (видУлуг.Количество != 0)
                    {
                        listLibrary.Add(id, видУлуг);
                    }
                    else
                    {
                        //если флажок выбрали но число не выставили тогда флажок сбросим
                        col.Value = null;
                    }
                }
                catch
                {
                    //Если пользователь изменил ранее введённое значение значит мы удаляем ранее введённую строку 
                    listLibrary.Remove(id);

                    //Собираем новые данные и записываем их по новому
                    ВидУслуг видУлугН = new ВидУслуг();
                    видУлугН.ВидУслуги = this.dataGridView1.CurrentRow.Cells["ВидУслуги"].Value.ToString();

                    видУлугН.НомерПоПеречню = this.dataGridView1.CurrentRow.Cells["НомерПоПеречню"].Value.ToString();
                    видУлугН.Цена = Convert.ToDecimal(this.dataGridView1.CurrentRow.Cells["Цена"].Value);

                    видУлугН.Количество = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Количество"].Value);

                    listLibrary.Add(id, видУлугН);
                }

            }

            //если поставили ноль
            if (col == this.dataGridView1.CurrentRow.Cells["Количество"] && Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Количество"].Value) == 0)
            {
                //получим id записи
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);

                //Если пользователь ввёл 0
                listLibrary.Remove(id);
            }


            //==============================================Старая рабочая версия===============
            ////отследим изменения в DatatGridView
            //DataGridViewCell col = this.dataGridView1.CurrentCell;

            ////Определим тип выбранной ячейки
            //Type t = col.GetType();

            ////if(t.ToSting())

            ////если пользователь нажал checkbox 
            //if (t.ToString() == "System.Windows.Forms.DataGridViewCheckBoxCell")
            //{
            //    //================================пометсим полученное значение в словарь
            //    //получим id записи
            //    int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_услуги"].Value);

            //    ВидУслуг видУлуг = new ВидУслуг();
            //    видУлуг.ВидУслуги = this.dataGridView1.CurrentRow.Cells["ВидУслуги"].Value.ToString();

            //    видУлуг.НомерПоПеречню = this.dataGridView1.CurrentRow.Cells["НомерПоПеречню"].Value.ToString();
            //    видУлуг.Цена = Convert.ToDecimal(this.dataGridView1.CurrentRow.Cells["Цена"].Value);

            //    видУлуг.Количество = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["Количество"].Value);

            //    //запишем в словарь
            //    try
            //    {
            //        //проверим 
            //        if (видУлуг.Количество != 0)
            //        {
            //            listLibrary.Add(id, видУлуг);
            //        }
            //        else
            //        {
            //            //если флажок выбрали но число не выставили тогда флажок сбросим
            //            col.Value = null;
            //        }
            //    }
            //    catch
            //    {
            //        //если пользователь нажал ещё раз выбрал выделенное поле и ключ не записался значит пользователь хочет снять флажок
            //        listLibrary.Remove(id);
            //    }

            //}



        }

     
   
    }
}