using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.IO;

namespace Стамотология
{
    public partial class FormEditHospital : Form
    {
        private bool flagInsert = false;
        public FormEditHospital()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEditHospital_Load(object sender, EventArgs e)
        {
            //Узнаем данные в таблицу поликлитнника нужно записывать или только обновлять
            string queryFlagInsert = "select * from Поликлинника";
            DataTable dt = ТаблицаБД.GetTable(queryFlagInsert, ConnectionDB.ConnectionString(), "Поликлинника");

            // Узнаем есть ли в таблице колонка номер телефона и e-mail.
            if (dt.Columns.Count == 24)
            {
                string queryUpdate = "ALTER TABLE Поликлинника ADD COLUMN НомерТелефона TEXT(50), COLUMN email TEXT(50),COLUMN Исполнитель TEXT(50) ";

                // Внесем изменения в структуру таблицы.
                ТаблицаБД.AlterTableПоликлинника(queryUpdate, ConnectionDB.ConnectionString(), "Поликлинника");

                // Не очень красиво , но получим таблицу с новой структурой.
                dt = ТаблицаБД.GetTable(queryFlagInsert, ConnectionDB.ConnectionString(), "Поликлинника");
            }
            
            int countRow = dt.Rows.Count;
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["НачальныйНомерДоговора"].ToString() != "")
                {
                    this.txtNumDog.Text = dt.Rows[0]["НачальныйНомерДоговора"].ToString();
                }
                else
                {
                    this.txtNumDog.Text = "0";
                }
            }
            else
            {
                this.txtNumDog.Text = "0";
            }
            //заполним combo box-ы
            string queryВрачи = "select id_главВрач,ФИО_ГлавВрач from ГлавВрач";
            this.cmbВрачи.DataSource = ТаблицаБД.GetTable(queryВрачи, ConnectionDB.ConnectionString(), "ГлавВрач");
            this.cmbВрачи.ValueMember = "id_главВрач";
            this.cmbВрачи.DisplayMember = "ФИО_ГлавВрач";

            string queryБух = "select id_главБух,ФИО_ГлавБух from ГлавБух";
            this.cmbБух.DataSource = ТаблицаБД.GetTable(queryБух, ConnectionDB.ConnectionString(), "ГлавВрач");
            this.cmbБух.ValueMember = "id_главБух";
            this.cmbБух.DisplayMember = "ФИО_ГлавБух";


            //узнаем добавлять записи или только обновлять 
            if (countRow == 0)
            {
                //записей в таблице нет значит добьавляем новую запись
                flagInsert = true;
            }
            else
            {
                //записи в таблице есть значит форма работает на обновление записи
                flagInsert = false;
            }

            //Получим реквизиты поликлинники
            Hospital hospital = new Hospital();

           foreach(DataRow row in dt.Rows)
           {
               hospital.НаименованиеПоликлинники = row["НаименованиеПоликлинники"].ToString();
               hospital.КодПоликлинники = row["КодПоликлинники"].ToString();
               hospital.ЮридическийАдрес = row["ЮридическийАдрес"].ToString();

               hospital.ФактическийАдрес = row["ФактическийАдрес"].ToString();
               hospital.id_главВрач = Convert.ToInt32(row["id_главВрач"]);
               hospital.id_главБух = Convert.ToInt32(row["id_главБух"]);

               hospital.СвидетельствоРегистрации = row["СвидетельствоРегистрации"].ToString();
               hospital.ИНН = row["ИНН"].ToString();
               hospital.КПП = row["КПП"].ToString();
               hospital.БИК = row["БИК"].ToString();

               hospital.НаименованиеБанка = row["НаименованиеБанка"].ToString();
               hospital.РасчётСчёт = row["РасчётныйСчёт"].ToString();
               hospital.ЛицевойСчёт = row["ЛицевойСчёт"].ToString();
               hospital.НомерЛицензии = row["НомерЛицензии"].ToString();

               hospital.ДатаРегистрацииЛицензии = row["ДатаРегистрацииЛицензии"].ToString();
               hospital.ОГРН = row["ОГРН"].ToString();
               hospital.СвидетельствоРегистрацииЕГРЮЛ = row["СвидетельствоРегистрацииЕГРЮЛ"].ToString();
               //hospital.ОрганВыдавшийСвидетельство = row["ОрганВыдавшийЛицензию"].ToString();
               hospital.ОрганВыдавшийЛицензию  = row["ОрганВыдавшийЛицензию"].ToString();

               hospital.Постановление = row["Постановление"].ToString();
               hospital.ОКПО = row["ОКПО"].ToString();
               hospital.ОКАТО = row["ОКАТО"].ToString();

               //string s1 = row["НомерТелефона"].ToString();
               //string s2 = row["email"].ToString();

               hospital.Phone = row["НомерТелефона"].ToString();
               hospital.Email = row["email"].ToString();

               hospital.Исполнитель = row["Исполнитель"].ToString();
           }

            //отобразим данные на форме
            this.textBox1.Text = hospital.НаименованиеПоликлинники;
            this.textBox2.Text = hospital.КодПоликлинники;
            this.textBox3.Text = hospital.ЮридическийАдрес;
            this.textBox4.Text = hospital.ФактическийАдрес;

            //отобразим combo box-ы
            string qГЛавВрач = "select ФИО_ГлавВрач from ГлавВрач where id_главВрач = " + hospital.id_главВрач + " ";

            DataTable tabHosp = ТаблицаБД.GetTable(qГЛавВрач, ConnectionDB.ConnectionString(), "ГлавВрач");

            //переменная для хранения ФИО глав врача
            string главВрач = string.Empty;
            if (tabHosp.Rows.Count != 0)
            {
                //string главВрач = ТаблицаБД.GetTable(qГЛавВрач, ConnectionDB.ConnectionString(), "ГлавВрач").Rows[0][0].ToString().Trim();
                главВрач = tabHosp.Rows[0][0].ToString().Trim();
            }
            this.cmbВрачи.Text = главВрач;


            string qГЛавБух = "select ФИО_ГлавБух from ГлавБух where id_главБух = " + hospital.id_главБух + " ";

            DataTable tabBux = ТаблицаБД.GetTable(qГЛавБух, ConnectionDB.ConnectionString(), "ГлавВрач");

            //переменная для хранения ФИО глав буха
            string главБух = string.Empty;

            if (tabBux.Rows.Count != 0)
            {
                главБух = tabBux.Rows[0][0].ToString().Trim();
                //string главБух = ТаблицаБД.GetTable(qГЛавБух, ConnectionDB.ConnectionString(), "ГлавВрач").Rows[0][0].ToString().Trim();
            }
            this.cmbБух.Text = главБух;

            this.maskedTextBox1.Text = hospital.ИНН;
            this.maskedTextBox8.Text = hospital.ОГРН;

            this.maskedTextBox6.Text = hospital.ОКПО;
            this.maskedTextBox7.Text = hospital.ОКАТО;

            this.textBox6.Text = hospital.НомерЛицензии;

            if (hospital.ДатаРегистрацииЛицензии != null)
            {
                this.dateTimePicker1.Value = Convert.ToDateTime(hospital.ДатаРегистрацииЛицензии);
            }

            this.textBox7.Text = hospital.ОрганВыдавшийЛицензию;
            this.textBox5.Text = hospital.НаименованиеБанка;

            this.maskedTextBox4.Text = hospital.РасчётСчёт;
            
            // В связи с изменениями в лицевом счёте (появились буквы).
            //this.maskedTextBox5.Text = hospital.ЛицевойСчёт;
            this.txtЛицевойСчёт.Text = hospital.ЛицевойСчёт;


            this.maskedTextBox2.Text = hospital.КПП;
            this.maskedTextBox3.Text = hospital.БИК;

            this.textBox10.Text = hospital.Постановление;

            // Отобразим номер телефона.
            this.maskedTextBox5.Text = hospital.Phone.Trim();

            // От образим e-mail.
            this.textBox8.Text = hospital.Email.Trim();

            this.textBox9.Text = hospital.Исполнитель.Trim();
           
            //Прочитаем файл конфигурации Config.dll (для конфигурирования выгрузки реестра в файл)
            using (FileStream fs = File.OpenRead("Config.dll"))
            using (TextReader read = new StreamReader(fs))
            {
                string sConfig = read.ReadLine();
                if (sConfig == "1")
                {
                    //Разрешаем выгрузку реестра в файл
                    this.chkВыгрузки.Checked = true;
                }
                else
                {
                    //запрещаем выгрузку реестра в файл
                    this.chkВыгрузки.Checked = false;
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormEditEsculap esculap = new FormEditEsculap();
            esculap.ShowDialog();

            //обонвим по закрытию список врачей
            if (esculap.DialogResult == DialogResult.Cancel)
            {
                //обнулим список врачей
                this.cmbВрачи.DataSource = null;

                string queryВрач = "select id_главВрач,ФИО_ГлавВрач from ГлавВрач";
                DataTable tab = ТаблицаБД.GetTable(queryВрач,ConnectionDB.ConnectionString(),"ГлавВрач");
                this.cmbВрачи.DataSource = tab;

                this.cmbВрачи.ValueMember = "id_главВрач";
                this.cmbВрачи.DisplayMember = "ФИО_ГлавВрач";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormБухгалтер fБух = new FormБухгалтер();
            fБух.ShowDialog();

            if (fБух.DialogResult == DialogResult.Cancel)
            {
                //обнулим список врачей
                this.cmbБух.DataSource = null;

                string queryБух = "select id_главБух,ФИО_ГлавБух from ГлавБух";
                DataTable tab = ТаблицаБД.GetTable(queryБух, ConnectionDB.ConnectionString(), "ГлавБух");
                this.cmbБух.DataSource = tab;

                this.cmbБух.ValueMember = "id_главБух";
                this.cmbБух.DisplayMember = "ФИО_ГлавБух";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text != "" && this.textBox1.Text != "" && this.textBox3.Text != "" && this.cmbВрачи.Text != "" && this.cmbБух.Text != "" && this.maskedTextBox1.Text != "" && this.textBox6.Text != "" && this.textBox7.Text != "" && this.txtNumDog.Text != "" && this.cmbБух.Text.Trim() != "" && this.cmbВрачи.Text.Trim() != "")
            {
                if (flagInsert == true)
                {
                    //// Проверим длинну лицевого счёта.
                    //int lenЛС = this.txtЛицевойСчёт.Text.Length;

                    //if (lenЛС == 15)
                    //{
                        InsertHospital hosp = new InsertHospital(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, Convert.ToInt32(this.cmbВрачи.SelectedValue), Convert.ToInt32(this.cmbБух.SelectedValue), "", this.maskedTextBox1.Text, this.maskedTextBox2.Text, this.maskedTextBox3.Text, this.textBox5.Text, this.maskedTextBox4.Text, this.txtЛицевойСчёт.Text, this.textBox6.Text, this.dateTimePicker1.Value.ToShortDateString(), "", this.maskedTextBox8.Text, this.textBox7.Text, this.textBox10.Text, this.maskedTextBox6.Text, this.maskedTextBox7.Text, Convert.ToInt32(this.txtNumDog.Text));
                        Receiver rec = new Receiver();

                    

                        rec.Action(hosp);
                        this.Close();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Проверьте количество символов в лицевом счёте");
                    //}
                }

                if (flagInsert == false)
                {
                    string query = "select top 1 id_поликлинника from Поликлинника";
                    int id = Convert.ToInt32(ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);

                    //// Проверим длинну лицевого счёта.
                    //int lenЛС = this.txtЛицевойСчёт.Text.Length;

                    //if (lenЛС == 15)
                    //{
                        UpdateHospital hosp = new UpdateHospital(id, this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, Convert.ToInt32(this.cmbВрачи.SelectedValue), Convert.ToInt32(this.cmbБух.SelectedValue), "", this.maskedTextBox1.Text, this.maskedTextBox2.Text, this.maskedTextBox3.Text, this.textBox5.Text, this.maskedTextBox4.Text, this.txtЛицевойСчёт.Text, this.textBox6.Text, this.dateTimePicker1.Value.ToShortDateString(), "", this.maskedTextBox8.Text, this.textBox7.Text, this.textBox10.Text, this.maskedTextBox6.Text, this.maskedTextBox7.Text, Convert.ToInt32(this.txtNumDog.Text));
                        Receiver rec = new Receiver();

                    // Запишем телефон и email поликлинники.
                        hosp.Phone = this.maskedTextBox5.Text.Trim();
                        hosp.Email = this.textBox8.Text.Trim();
                        hosp.Исполнитель = this.textBox9.Text.Trim();

                        rec.Action(hosp);
                        this.Close();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Проверьте количество символов в лицевом счёте");
                    //}
                }

                //Запишем изменения в файл конфигурации выгрузки Config.dll
                using(FileStream fs = File.OpenWrite("Config.dll"))
                using(TextWriter writ =new StreamWriter(fs))
                {
                    if(this.chkВыгрузки.Checked == true)
                    {
                        writ.WriteLine("1");
                    }
                    else
                    {
                        writ.WriteLine("0");
                    }
                }
            }
            else
            {
                MessageBox.Show("Не заполнены обязательные поля");
            }
           
        }

        private void txtNumDog_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////Заменим запятую на точку
            //if (e.KeyChar == ',')
            //{
            //    e.KeyChar = '.';
            //}

            //Разрешим ввести только числа и знак "."
            if (!(Char.IsDigit(e.KeyChar)))// && !((e.KeyChar == '.')))// && (textBox2.Text.IndexOf(".") == -1) && (textBox2.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
    }
}