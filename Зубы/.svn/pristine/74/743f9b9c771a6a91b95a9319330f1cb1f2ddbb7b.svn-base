using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;

namespace Стамотология
{
    public partial class FormЛьготнаяКатегория : Form
    {
        public FormЛьготнаяКатегория()
        {
            InitializeComponent();
        }

        private void льготнаяКатегорияBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
           
        }

        private void FormЛьготнаяКатегория_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db1DataSet.ЛьготнаяКатегория' table. You can move, or remove it, as needed.
            this.льготнаяКатегорияTableAdapter.Fill(this.db1DataSet.ЛьготнаяКатегория);

            this.льготнаяКатегорияDataGridView.DataSource = TableЛьготнаяКатегория.GetDateTable();
        }

        private void UpdateDGV()
        {
            this.льготнаяКатегорияDataGridView.DataSource = null;
            this.льготнаяКатегорияDataGridView.DataSource = TableЛьготнаяКатегория.GetDateTable();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Receiver receiver = new Receiver();
            InsertЛьготьнаяКатегория лк = new InsertЛьготьнаяКатегория(this.льготнаяКатегорияTextBox.Text);
            receiver.Action(лк);

            //Обновим данные
            UpdateDGV();

            this.льготнаяКатегорияTextBox.Text = "";
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            int id = (int)this.льготнаяКатегорияDataGridView.CurrentRow.Cells[0].Value;

            Receiver reciver = new Receiver();
            DeleteЛьготнаяКатегория лк = new DeleteЛьготнаяКатегория(id);
            reciver.Action(лк);

            UpdateDGV();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void льготнаяКатегорияBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}