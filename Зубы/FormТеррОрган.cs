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
    public partial class FormТеррОрган : Form
    {
        private bool flagInsert = false;
        private string названиеГорода = string.Empty;
        private string nameThaun = string.Empty;

        public FormТеррОрган()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormШев form = new FormШев();
            form.ShowDialog();

            if (form.DialogResult == DialogResult.Cancel)
            {
                //обнулим список врачей
                this.cmbРуководитель.DataSource = null;

                string queryВрач = "select id_шев,ФИО_Руководитель from ФиоШев";
                DataTable tab = ТаблицаБД.GetTable(queryВрач, ConnectionDB.ConnectionString(), "ФиоШев");
                this.cmbРуководитель.DataSource = tab;

                this.cmbРуководитель.ValueMember = "id_шев";
                this.cmbРуководитель.DisplayMember = "ФИО_Руководитель";
            }
        }

        private void FormТеррОрган_Load(object sender, EventArgs e)
        {

            //Заполним comboBox
            string queryВрач = "select id_шев,ФИО_Руководитель from ФиоШев";
            DataTable tab = ТаблицаБД.GetTable(queryВрач, ConnectionDB.ConnectionString(), "ФиоШев");
            this.cmbРуководитель.DataSource = tab;

            this.cmbРуководитель.ValueMember = "id_шев";
            this.cmbРуководитель.DisplayMember = "ФИО_Руководитель";

            //Узнаем сеть ли записи в таблице и нужно в неё записывать данные или только обновлять
            string queryFlagInsert = "select * from Комитет";
            DataTable dt = ТаблицаБД.GetTable(queryFlagInsert, ConnectionDB.ConnectionString(), "Поликлинника");
            int countRow = dt.Rows.Count;

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


            //Получим данные из таблицы (для ускорения процесса отказался от класса)
            foreach (DataRow row in dt.Rows)
            {
                //наименование терр органа
                this.textBox1.Text = row["НаименованиеКомитета"].ToString();

                //юридический адрес
                this.txtЮрАдрес.Text = row["ЮридическийАдрес"].ToString();

                //укажем текущего руководителя
                string queryШев = "select ФИО_Руководитель from ФиоШев where id_шев in (select id_шев from Комитет)";
                DataTable tabШ = ТаблицаБД.GetTable(queryШев,ConnectionDB.ConnectionString(),"ФиоШев");
                this.cmbРуководитель.Text = tabШ.Rows[0][0].ToString();

                //л/с
                this.maskedTextBox2.Text = row["ЛицевойСчет"].ToString();

                //ИНН
                this.maskedTextBox1.Text = row["ИНН"].ToString();

                //КПП
                this.maskedTextBox3.Text = row["КПП"].ToString();

                //р/с
                this.maskedTextBox4.Text = row["РасчётныйСчёт"].ToString();

                //БИК
                this.maskedTextBox5.Text = row["БИК"].ToString();

                //Наименование банка
                this.textBox2.Text = row["НазваниеБанка"].ToString();

            }

            //Введём в поле наименование территориального органа 
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

                if (ft.DialogResult == DialogResult.OK)
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

            this.textBox3.Text = названиеГорода.Trim();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "" && this.txtЮрАдрес.Text != "" && this.cmbРуководитель.Text != "" && this.maskedTextBox1.Text != "" && this.maskedTextBox2.Text != "" && this.maskedTextBox3.Text != "" && this.maskedTextBox4.Text != "" && this.maskedTextBox5.Text != "" && this.textBox3.Text != "")
            {
                //переменная хранит название города
                nameThaun = this.textBox3.Text.Trim();

                if (flagInsert == true)
                {
                    //InsertHospital hosp = new InsertHospital(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, Convert.ToInt32(this.cmbВрачи.SelectedValue), Convert.ToInt32(this.cmbБух.SelectedValue), "", this.maskedTextBox1.Text, this.maskedTextBox2.Text, this.maskedTextBox3.Text, this.textBox5.Text, this.maskedTextBox4.Text, this.maskedTextBox5.Text, this.textBox6.Text, this.dateTimePicker1.Value.ToShortDateString(), "", this.maskedTextBox8.Text, this.textBox7.Text, this.textBox10.Text, this.maskedTextBox6.Text, this.maskedTextBox7.Text);
                    //Receiver rec = new Receiver();

                    //rec.Action(hosp);

                    string QueryInsert = "insert into Комитет(КодТО,id_шев,ЮридическийАдрес,ИНН,РасчётныйСчёт,НазваниеБанка,КПП,БИК,ЛицевойСчет,НаименованиеКомитета) " +
                                          "values(1," + Convert.ToInt32(this.cmbРуководитель.SelectedValue) + ",'" + this.txtЮрАдрес.Text + "','" + this.maskedTextBox1.Text + "','" + this.maskedTextBox4.Text + "','" + this.textBox2.Text + "','" + this.maskedTextBox3.Text + "','" + this.maskedTextBox5.Text + "','" + this.maskedTextBox2.Text + "','" + this.textBox1.Text + "')";


                    Query.Execute(QueryInsert, ConnectionDB.ConnectionString());
                    this.Close();
                }

                if (flagInsert == false)
                {
                    //string query = "select top 1 id_поликлинника from Поликлинника";
                    //int id = Convert.ToInt32(ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);

                    //UpdateHospital hosp = new UpdateHospital(id, this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, Convert.ToInt32(this.cmbВрачи.SelectedValue), Convert.ToInt32(this.cmbБух.SelectedValue), "", this.maskedTextBox1.Text, this.maskedTextBox2.Text, this.maskedTextBox3.Text, this.textBox5.Text, this.maskedTextBox4.Text, this.maskedTextBox5.Text, this.textBox6.Text, this.dateTimePicker1.Value.ToShortDateString(), "", this.maskedTextBox8.Text, this.textBox7.Text, this.textBox10.Text, this.maskedTextBox6.Text, this.maskedTextBox7.Text);
                    //Receiver rec = new Receiver();

                    string query = "select top 1 id_комитет from Комитет";
                    int id = Convert.ToInt32(ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "Поликлинника").Rows[0][0]);


                    string stringUpdate = "update Комитет " +
                                          "set КодТО = 1 " +
                                          ",id_шев = " + Convert.ToInt32(this.cmbРуководитель.SelectedValue) + " " +
                                          ",ЮридическийАдрес = '" + this.txtЮрАдрес.Text + "' " +
                                          ",ИНН = '" + this.maskedTextBox1.Text + "' " +
                                          ",РасчётныйСчёт = '" + this.maskedTextBox4.Text + "' " +
                                          ",НазваниеБанка = '" + this.textBox2.Text + "' " +
                                          ",КПП = '" + this.maskedTextBox3.Text + "' " +
                                          ",БИК = '" + this.maskedTextBox5.Text + "' " +
                                          ",ЛицевойСчет = '" + this.maskedTextBox2.Text + "' " +
                                          ",НаименованиеКомитета = '" + this.textBox1.Text + "' " +
                                          "where id_комитет = " + id + " ";

                    Query.Execute(stringUpdate, ConnectionDB.ConnectionString());
                    this.Close();

                    FileStream inSream = File.Open("LocalBody.bac", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    inSream.Close();
                    FileInfo f = new FileInfo("LocalBody.bac");

                    StreamWriter w = f.CreateText();
                    w.WriteLine(nameThaun);
                    w.Close();

                }
            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению");
            }

           
        }
    }
}