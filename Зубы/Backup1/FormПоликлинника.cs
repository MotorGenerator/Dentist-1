using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;

namespace Стамотология
{
    public partial class FormПоликлинника : Form
    {
        public FormПоликлинника()
        {
            InitializeComponent();
        }

        private void поликлинникаBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.поликлинникаBindingSource.EndEdit();
            this.поликлинникаTableAdapter.Update(this.db1DataSet.Поликлинника);

        }

        private void FormПоликлинника_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db1DataSet.Поликлинника' table. You can move, or remove it, as needed.
            this.поликлинникаTableAdapter.Fill(this.db1DataSet.Поликлинника);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //db1DataSet.ПоликлинникаRow row = this.db1DataSet.Поликлинника.NewПоликлинникаRow();
            //row.НаименованиеПоликлинники = наименованиеПоликлинникиTextBox.Text;
            //row.КодПоликлинники = кодПоликлинникиTextBox.Text;
            //row.ЮредическийАдрес = юредическийАдресTextBox.Text;
            //row.ФактическийАдрес = фактическийАдресTextBox.Text;
            //row.ФИО_ГлавВрач = фИО_ГлавВрачTextBox.Text;
            //row.ФИО_ГлавБух = фИО_ГлавБухTextBox.Text;
            //row.СвидетельствоРегистрации = свидетельствоРегистрацииTextBox.Text;
            //row.ИНН = иННTextBox.Text;
            //row.КПП = кодПоликлинникиTextBox.Text;
            //row.БИК = бИКTextBox.Text;
            //row.НаименованиеБанка = наименованиеБанкаTextBox.Text;
            //row.РасчётныйСчёт = расчётныйСчётTextBox.Text;
            //row.НаименованиеКлиента = наименованиеКлиентаTextBox.Text;
            //row.НомерЛицении = номерЛиценииTextBox.Text;
            //row.ДатаРегистрацииЛицении = датаРегистрацииЛиценииDateTimePicker.Value;
            //row.ОРГН = оРГНTextBox.Text;
            //row.СвидетельствоРегистрацииЕГРЮЛ = свидетельствоРегистрацииЕГРЮЛTextBox.Text;
            //row.ОрганВыдавшийСвидетельство = органВыдавшийСвидетельствоTextBox.Text;

            //this.db1DataSet.Поликлинника.AddПоликлинникаRow(row);
            //this.поликлинникаTableAdapter.Update(this.db1DataSet.Поликлинника);

            Receiver receiver = new Receiver();
            InsertПоликлинника hospital = new InsertПоликлинника(наименованиеПоликлинникиTextBox.Text, кодПоликлинникиTextBox.Text, юредическийАдресTextBox.Text, фактическийАдресTextBox.Text, фИО_ГлавВрачTextBox.Text, фИО_ГлавБухTextBox.Text, свидетельствоРегистрацииTextBox.Text, иННTextBox.Text, this.кППTextBox.Text, бИКTextBox.Text, наименованиеБанкаTextBox.Text, расчётныйСчётTextBox.Text, номерЛиценииTextBox.Text, датаРегистрацииЛиценииDateTimePicker.Value.ToShortDateString(), оРГНTextBox.Text, свидетельствоРегистрацииTextBox.Text, органВыдавшийСвидетельствоTextBox.Text);
            receiver.Action(hospital);

            //Обновим данные
            Update();

        }
    }
}