namespace Стамотология
{
    partial class FormПоликлинника
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label наименованиеПоликлинникиLabel;
            System.Windows.Forms.Label кодПоликлинникиLabel;
            System.Windows.Forms.Label юредическийАдресLabel;
            System.Windows.Forms.Label фактическийАдресLabel;
            System.Windows.Forms.Label фИО_ГлавВрачLabel;
            System.Windows.Forms.Label фИО_ГлавБухLabel;
            System.Windows.Forms.Label свидетельствоРегистрацииLabel;
            System.Windows.Forms.Label иННLabel;
            System.Windows.Forms.Label кППLabel;
            System.Windows.Forms.Label бИКLabel;
            System.Windows.Forms.Label наименованиеБанкаLabel;
            System.Windows.Forms.Label расчётныйСчётLabel;
            System.Windows.Forms.Label номерЛиценииLabel;
            System.Windows.Forms.Label датаРегистрацииЛиценииLabel;
            System.Windows.Forms.Label оРГНLabel;
            System.Windows.Forms.Label свидетельствоРегистрацииЕГРЮЛLabel;
            System.Windows.Forms.Label органВыдавшийСвидетельствоLabel;
            this.db1DataSet = new Стамотология.db1DataSet();
            this.поликлинникаBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.поликлинникаTableAdapter = new Стамотология.db1DataSetTableAdapters.ПоликлинникаTableAdapter();
            this.наименованиеПоликлинникиTextBox = new System.Windows.Forms.TextBox();
            this.кодПоликлинникиTextBox = new System.Windows.Forms.TextBox();
            this.юредическийАдресTextBox = new System.Windows.Forms.TextBox();
            this.фактическийАдресTextBox = new System.Windows.Forms.TextBox();
            this.фИО_ГлавВрачTextBox = new System.Windows.Forms.TextBox();
            this.фИО_ГлавБухTextBox = new System.Windows.Forms.TextBox();
            this.свидетельствоРегистрацииTextBox = new System.Windows.Forms.TextBox();
            this.иННTextBox = new System.Windows.Forms.TextBox();
            this.кППTextBox = new System.Windows.Forms.TextBox();
            this.бИКTextBox = new System.Windows.Forms.TextBox();
            this.наименованиеБанкаTextBox = new System.Windows.Forms.TextBox();
            this.расчётныйСчётTextBox = new System.Windows.Forms.TextBox();
            this.номерЛиценииTextBox = new System.Windows.Forms.TextBox();
            this.датаРегистрацииЛиценииDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.оРГНTextBox = new System.Windows.Forms.TextBox();
            this.свидетельствоРегистрацииЕГРЮЛTextBox = new System.Windows.Forms.TextBox();
            this.органВыдавшийСвидетельствоTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            наименованиеПоликлинникиLabel = new System.Windows.Forms.Label();
            кодПоликлинникиLabel = new System.Windows.Forms.Label();
            юредическийАдресLabel = new System.Windows.Forms.Label();
            фактическийАдресLabel = new System.Windows.Forms.Label();
            фИО_ГлавВрачLabel = new System.Windows.Forms.Label();
            фИО_ГлавБухLabel = new System.Windows.Forms.Label();
            свидетельствоРегистрацииLabel = new System.Windows.Forms.Label();
            иННLabel = new System.Windows.Forms.Label();
            кППLabel = new System.Windows.Forms.Label();
            бИКLabel = new System.Windows.Forms.Label();
            наименованиеБанкаLabel = new System.Windows.Forms.Label();
            расчётныйСчётLabel = new System.Windows.Forms.Label();
            номерЛиценииLabel = new System.Windows.Forms.Label();
            датаРегистрацииЛиценииLabel = new System.Windows.Forms.Label();
            оРГНLabel = new System.Windows.Forms.Label();
            свидетельствоРегистрацииЕГРЮЛLabel = new System.Windows.Forms.Label();
            органВыдавшийСвидетельствоLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.db1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.поликлинникаBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // наименованиеПоликлинникиLabel
            // 
            наименованиеПоликлинникиLabel.AutoSize = true;
            наименованиеПоликлинникиLabel.Location = new System.Drawing.Point(23, 23);
            наименованиеПоликлинникиLabel.Name = "наименованиеПоликлинникиLabel";
            наименованиеПоликлинникиLabel.Size = new System.Drawing.Size(163, 13);
            наименованиеПоликлинникиLabel.TabIndex = 3;
            наименованиеПоликлинникиLabel.Text = "Наименование Поликлинники:";
            // 
            // кодПоликлинникиLabel
            // 
            кодПоликлинникиLabel.AutoSize = true;
            кодПоликлинникиLabel.Location = new System.Drawing.Point(23, 49);
            кодПоликлинникиLabel.Name = "кодПоликлинникиLabel";
            кодПоликлинникиLabel.Size = new System.Drawing.Size(106, 13);
            кодПоликлинникиLabel.TabIndex = 5;
            кодПоликлинникиLabel.Text = "Код Поликлинники:";
            // 
            // юредическийАдресLabel
            // 
            юредическийАдресLabel.AutoSize = true;
            юредическийАдресLabel.Location = new System.Drawing.Point(23, 75);
            юредическийАдресLabel.Name = "юредическийАдресLabel";
            юредическийАдресLabel.Size = new System.Drawing.Size(112, 13);
            юредическийАдресLabel.TabIndex = 7;
            юредическийАдресLabel.Text = "Юредический Адрес:";
            // 
            // фактическийАдресLabel
            // 
            фактическийАдресLabel.AutoSize = true;
            фактическийАдресLabel.Location = new System.Drawing.Point(23, 101);
            фактическийАдресLabel.Name = "фактическийАдресLabel";
            фактическийАдресLabel.Size = new System.Drawing.Size(113, 13);
            фактическийАдресLabel.TabIndex = 9;
            фактическийАдресLabel.Text = "Фактический Адрес:";
            // 
            // фИО_ГлавВрачLabel
            // 
            фИО_ГлавВрачLabel.AutoSize = true;
            фИО_ГлавВрачLabel.Location = new System.Drawing.Point(23, 127);
            фИО_ГлавВрачLabel.Name = "фИО_ГлавВрачLabel";
            фИО_ГлавВрачLabel.Size = new System.Drawing.Size(91, 13);
            фИО_ГлавВрачLabel.TabIndex = 11;
            фИО_ГлавВрачLabel.Text = "ФИО Глав Врач:";
            // 
            // фИО_ГлавБухLabel
            // 
            фИО_ГлавБухLabel.AutoSize = true;
            фИО_ГлавБухLabel.Location = new System.Drawing.Point(23, 153);
            фИО_ГлавБухLabel.Name = "фИО_ГлавБухLabel";
            фИО_ГлавБухLabel.Size = new System.Drawing.Size(84, 13);
            фИО_ГлавБухLabel.TabIndex = 13;
            фИО_ГлавБухLabel.Text = "ФИО Глав Бух:";
            // 
            // свидетельствоРегистрацииLabel
            // 
            свидетельствоРегистрацииLabel.AutoSize = true;
            свидетельствоРегистрацииLabel.Location = new System.Drawing.Point(23, 179);
            свидетельствоРегистрацииLabel.Name = "свидетельствоРегистрацииLabel";
            свидетельствоРегистрацииLabel.Size = new System.Drawing.Size(155, 13);
            свидетельствоРегистрацииLabel.TabIndex = 15;
            свидетельствоРегистрацииLabel.Text = "Свидетельство Регистрации:";
            // 
            // иННLabel
            // 
            иННLabel.AutoSize = true;
            иННLabel.Location = new System.Drawing.Point(23, 205);
            иННLabel.Name = "иННLabel";
            иННLabel.Size = new System.Drawing.Size(34, 13);
            иННLabel.TabIndex = 17;
            иННLabel.Text = "ИНН:";
            // 
            // кППLabel
            // 
            кППLabel.AutoSize = true;
            кППLabel.Location = new System.Drawing.Point(23, 231);
            кППLabel.Name = "кППLabel";
            кППLabel.Size = new System.Drawing.Size(33, 13);
            кППLabel.TabIndex = 19;
            кППLabel.Text = "КПП:";
            // 
            // бИКLabel
            // 
            бИКLabel.AutoSize = true;
            бИКLabel.Location = new System.Drawing.Point(23, 257);
            бИКLabel.Name = "бИКLabel";
            бИКLabel.Size = new System.Drawing.Size(32, 13);
            бИКLabel.TabIndex = 21;
            бИКLabel.Text = "БИК:";
            // 
            // наименованиеБанкаLabel
            // 
            наименованиеБанкаLabel.AutoSize = true;
            наименованиеБанкаLabel.Location = new System.Drawing.Point(23, 283);
            наименованиеБанкаLabel.Name = "наименованиеБанкаLabel";
            наименованиеБанкаLabel.Size = new System.Drawing.Size(120, 13);
            наименованиеБанкаLabel.TabIndex = 23;
            наименованиеБанкаLabel.Text = "Наименование Банка:";
            // 
            // расчётныйСчётLabel
            // 
            расчётныйСчётLabel.AutoSize = true;
            расчётныйСчётLabel.Location = new System.Drawing.Point(23, 309);
            расчётныйСчётLabel.Name = "расчётныйСчётLabel";
            расчётныйСчётLabel.Size = new System.Drawing.Size(91, 13);
            расчётныйСчётLabel.TabIndex = 25;
            расчётныйСчётLabel.Text = "Расчётный Счёт:";
            // 
            // номерЛиценииLabel
            // 
            номерЛиценииLabel.AutoSize = true;
            номерЛиценииLabel.Location = new System.Drawing.Point(23, 334);
            номерЛиценииLabel.Name = "номерЛиценииLabel";
            номерЛиценииLabel.Size = new System.Drawing.Size(97, 13);
            номерЛиценииLabel.TabIndex = 29;
            номерЛиценииLabel.Text = "Номер Лицензии:";
            // 
            // датаРегистрацииЛиценииLabel
            // 
            датаРегистрацииЛиценииLabel.AutoSize = true;
            датаРегистрацииЛиценииLabel.Location = new System.Drawing.Point(23, 361);
            датаРегистрацииЛиценииLabel.Name = "датаРегистрацииЛиценииLabel";
            датаРегистрацииЛиценииLabel.Size = new System.Drawing.Size(157, 13);
            датаРегистрацииЛиценииLabel.TabIndex = 31;
            датаРегистрацииЛиценииLabel.Text = "Дата Регистрации Лицензии:";
            // 
            // оРГНLabel
            // 
            оРГНLabel.AutoSize = true;
            оРГНLabel.Location = new System.Drawing.Point(23, 386);
            оРГНLabel.Name = "оРГНLabel";
            оРГНLabel.Size = new System.Drawing.Size(39, 13);
            оРГНLabel.TabIndex = 33;
            оРГНLabel.Text = "ОРГН:";
            // 
            // свидетельствоРегистрацииЕГРЮЛLabel
            // 
            свидетельствоРегистрацииЕГРЮЛLabel.AutoSize = true;
            свидетельствоРегистрацииЕГРЮЛLabel.Location = new System.Drawing.Point(23, 412);
            свидетельствоРегистрацииЕГРЮЛLabel.Name = "свидетельствоРегистрацииЕГРЮЛLabel";
            свидетельствоРегистрацииЕГРЮЛLabel.Size = new System.Drawing.Size(195, 13);
            свидетельствоРегистрацииЕГРЮЛLabel.TabIndex = 35;
            свидетельствоРегистрацииЕГРЮЛLabel.Text = "Свидетельство Регистрации ЕГРЮЛ:";
            // 
            // органВыдавшийСвидетельствоLabel
            // 
            органВыдавшийСвидетельствоLabel.AutoSize = true;
            органВыдавшийСвидетельствоLabel.Location = new System.Drawing.Point(23, 438);
            органВыдавшийСвидетельствоLabel.Name = "органВыдавшийСвидетельствоLabel";
            органВыдавшийСвидетельствоLabel.Size = new System.Drawing.Size(177, 13);
            органВыдавшийСвидетельствоLabel.TabIndex = 37;
            органВыдавшийСвидетельствоLabel.Text = "Орган Выдавший Свидетельство:";
            // 
            // db1DataSet
            // 
            this.db1DataSet.DataSetName = "db1DataSet";
            this.db1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // поликлинникаBindingSource
            // 
            this.поликлинникаBindingSource.DataMember = "Поликлинника";
            this.поликлинникаBindingSource.DataSource = this.db1DataSet;
            // 
            // поликлинникаTableAdapter
            // 
            this.поликлинникаTableAdapter.ClearBeforeFill = true;
            // 
            // наименованиеПоликлинникиTextBox
            // 
            this.наименованиеПоликлинникиTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "НаименованиеПоликлинники", true));
            this.наименованиеПоликлинникиTextBox.Location = new System.Drawing.Point(224, 20);
            this.наименованиеПоликлинникиTextBox.Name = "наименованиеПоликлинникиTextBox";
            this.наименованиеПоликлинникиTextBox.Size = new System.Drawing.Size(200, 20);
            this.наименованиеПоликлинникиTextBox.TabIndex = 4;
            // 
            // кодПоликлинникиTextBox
            // 
            this.кодПоликлинникиTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "КодПоликлинники", true));
            this.кодПоликлинникиTextBox.Location = new System.Drawing.Point(224, 46);
            this.кодПоликлинникиTextBox.Name = "кодПоликлинникиTextBox";
            this.кодПоликлинникиTextBox.Size = new System.Drawing.Size(200, 20);
            this.кодПоликлинникиTextBox.TabIndex = 6;
            // 
            // юредическийАдресTextBox
            // 
            this.юредическийАдресTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ЮредическийАдрес", true));
            this.юредическийАдресTextBox.Location = new System.Drawing.Point(224, 72);
            this.юредическийАдресTextBox.Name = "юредическийАдресTextBox";
            this.юредическийАдресTextBox.Size = new System.Drawing.Size(200, 20);
            this.юредическийАдресTextBox.TabIndex = 8;
            // 
            // фактическийАдресTextBox
            // 
            this.фактическийАдресTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ФактическийАдрес", true));
            this.фактическийАдресTextBox.Location = new System.Drawing.Point(224, 98);
            this.фактическийАдресTextBox.Name = "фактическийАдресTextBox";
            this.фактическийАдресTextBox.Size = new System.Drawing.Size(200, 20);
            this.фактическийАдресTextBox.TabIndex = 10;
            // 
            // фИО_ГлавВрачTextBox
            // 
            this.фИО_ГлавВрачTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ФИО_ГлавВрач", true));
            this.фИО_ГлавВрачTextBox.Location = new System.Drawing.Point(224, 124);
            this.фИО_ГлавВрачTextBox.Name = "фИО_ГлавВрачTextBox";
            this.фИО_ГлавВрачTextBox.Size = new System.Drawing.Size(200, 20);
            this.фИО_ГлавВрачTextBox.TabIndex = 12;
            // 
            // фИО_ГлавБухTextBox
            // 
            this.фИО_ГлавБухTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ФИО_ГлавБух", true));
            this.фИО_ГлавБухTextBox.Location = new System.Drawing.Point(224, 150);
            this.фИО_ГлавБухTextBox.Name = "фИО_ГлавБухTextBox";
            this.фИО_ГлавБухTextBox.Size = new System.Drawing.Size(200, 20);
            this.фИО_ГлавБухTextBox.TabIndex = 14;
            // 
            // свидетельствоРегистрацииTextBox
            // 
            this.свидетельствоРегистрацииTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "СвидетельствоРегистрации", true));
            this.свидетельствоРегистрацииTextBox.Location = new System.Drawing.Point(224, 176);
            this.свидетельствоРегистрацииTextBox.Name = "свидетельствоРегистрацииTextBox";
            this.свидетельствоРегистрацииTextBox.Size = new System.Drawing.Size(200, 20);
            this.свидетельствоРегистрацииTextBox.TabIndex = 16;
            // 
            // иННTextBox
            // 
            this.иННTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ИНН", true));
            this.иННTextBox.Location = new System.Drawing.Point(224, 202);
            this.иННTextBox.Name = "иННTextBox";
            this.иННTextBox.Size = new System.Drawing.Size(200, 20);
            this.иННTextBox.TabIndex = 18;
            // 
            // кППTextBox
            // 
            this.кППTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "КПП", true));
            this.кППTextBox.Location = new System.Drawing.Point(224, 228);
            this.кППTextBox.Name = "кППTextBox";
            this.кППTextBox.Size = new System.Drawing.Size(200, 20);
            this.кППTextBox.TabIndex = 20;
            // 
            // бИКTextBox
            // 
            this.бИКTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "БИК", true));
            this.бИКTextBox.Location = new System.Drawing.Point(224, 254);
            this.бИКTextBox.Name = "бИКTextBox";
            this.бИКTextBox.Size = new System.Drawing.Size(200, 20);
            this.бИКTextBox.TabIndex = 22;
            // 
            // наименованиеБанкаTextBox
            // 
            this.наименованиеБанкаTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "НаименованиеБанка", true));
            this.наименованиеБанкаTextBox.Location = new System.Drawing.Point(224, 280);
            this.наименованиеБанкаTextBox.Name = "наименованиеБанкаTextBox";
            this.наименованиеБанкаTextBox.Size = new System.Drawing.Size(200, 20);
            this.наименованиеБанкаTextBox.TabIndex = 24;
            // 
            // расчётныйСчётTextBox
            // 
            this.расчётныйСчётTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "РасчётныйСчёт", true));
            this.расчётныйСчётTextBox.Location = new System.Drawing.Point(224, 306);
            this.расчётныйСчётTextBox.Name = "расчётныйСчётTextBox";
            this.расчётныйСчётTextBox.Size = new System.Drawing.Size(200, 20);
            this.расчётныйСчётTextBox.TabIndex = 26;
            // 
            // номерЛиценииTextBox
            // 
            this.номерЛиценииTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "НомерЛицении", true));
            this.номерЛиценииTextBox.Location = new System.Drawing.Point(224, 331);
            this.номерЛиценииTextBox.Name = "номерЛиценииTextBox";
            this.номерЛиценииTextBox.Size = new System.Drawing.Size(200, 20);
            this.номерЛиценииTextBox.TabIndex = 30;
            // 
            // датаРегистрацииЛиценииDateTimePicker
            // 
            this.датаРегистрацииЛиценииDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.поликлинникаBindingSource, "ДатаРегистрацииЛицении", true));
            this.датаРегистрацииЛиценииDateTimePicker.Location = new System.Drawing.Point(224, 357);
            this.датаРегистрацииЛиценииDateTimePicker.Name = "датаРегистрацииЛиценииDateTimePicker";
            this.датаРегистрацииЛиценииDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.датаРегистрацииЛиценииDateTimePicker.TabIndex = 32;
            // 
            // оРГНTextBox
            // 
            this.оРГНTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ОРГН", true));
            this.оРГНTextBox.Location = new System.Drawing.Point(224, 383);
            this.оРГНTextBox.Name = "оРГНTextBox";
            this.оРГНTextBox.Size = new System.Drawing.Size(200, 20);
            this.оРГНTextBox.TabIndex = 34;
            // 
            // свидетельствоРегистрацииЕГРЮЛTextBox
            // 
            this.свидетельствоРегистрацииЕГРЮЛTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "СвидетельствоРегистрацииЕГРЮЛ", true));
            this.свидетельствоРегистрацииЕГРЮЛTextBox.Location = new System.Drawing.Point(224, 409);
            this.свидетельствоРегистрацииЕГРЮЛTextBox.Name = "свидетельствоРегистрацииЕГРЮЛTextBox";
            this.свидетельствоРегистрацииЕГРЮЛTextBox.Size = new System.Drawing.Size(200, 20);
            this.свидетельствоРегистрацииЕГРЮЛTextBox.TabIndex = 36;
            // 
            // органВыдавшийСвидетельствоTextBox
            // 
            this.органВыдавшийСвидетельствоTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.поликлинникаBindingSource, "ОрганВыдавшийСвидетельство", true));
            this.органВыдавшийСвидетельствоTextBox.Location = new System.Drawing.Point(224, 435);
            this.органВыдавшийСвидетельствоTextBox.Name = "органВыдавшийСвидетельствоTextBox";
            this.органВыдавшийСвидетельствоTextBox.Size = new System.Drawing.Size(200, 20);
            this.органВыдавшийСвидетельствоTextBox.TabIndex = 38;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 39;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormПоликлинника
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 499);
            this.Controls.Add(this.button1);
            this.Controls.Add(наименованиеПоликлинникиLabel);
            this.Controls.Add(this.наименованиеПоликлинникиTextBox);
            this.Controls.Add(кодПоликлинникиLabel);
            this.Controls.Add(this.кодПоликлинникиTextBox);
            this.Controls.Add(юредическийАдресLabel);
            this.Controls.Add(this.юредическийАдресTextBox);
            this.Controls.Add(фактическийАдресLabel);
            this.Controls.Add(this.фактическийАдресTextBox);
            this.Controls.Add(фИО_ГлавВрачLabel);
            this.Controls.Add(this.фИО_ГлавВрачTextBox);
            this.Controls.Add(фИО_ГлавБухLabel);
            this.Controls.Add(this.фИО_ГлавБухTextBox);
            this.Controls.Add(свидетельствоРегистрацииLabel);
            this.Controls.Add(this.свидетельствоРегистрацииTextBox);
            this.Controls.Add(иННLabel);
            this.Controls.Add(this.иННTextBox);
            this.Controls.Add(кППLabel);
            this.Controls.Add(this.кППTextBox);
            this.Controls.Add(бИКLabel);
            this.Controls.Add(this.бИКTextBox);
            this.Controls.Add(наименованиеБанкаLabel);
            this.Controls.Add(this.наименованиеБанкаTextBox);
            this.Controls.Add(расчётныйСчётLabel);
            this.Controls.Add(this.расчётныйСчётTextBox);
            this.Controls.Add(номерЛиценииLabel);
            this.Controls.Add(this.номерЛиценииTextBox);
            this.Controls.Add(датаРегистрацииЛиценииLabel);
            this.Controls.Add(this.датаРегистрацииЛиценииDateTimePicker);
            this.Controls.Add(оРГНLabel);
            this.Controls.Add(this.оРГНTextBox);
            this.Controls.Add(свидетельствоРегистрацииЕГРЮЛLabel);
            this.Controls.Add(this.свидетельствоРегистрацииЕГРЮЛTextBox);
            this.Controls.Add(органВыдавшийСвидетельствоLabel);
            this.Controls.Add(this.органВыдавшийСвидетельствоTextBox);
            this.Name = "FormПоликлинника";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormПоликлинника";
            this.Load += new System.EventHandler(this.FormПоликлинника_Load);
            ((System.ComponentModel.ISupportInitialize)(this.db1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.поликлинникаBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private db1DataSet db1DataSet;
        private System.Windows.Forms.BindingSource поликлинникаBindingSource;
        private Стамотология.db1DataSetTableAdapters.ПоликлинникаTableAdapter поликлинникаTableAdapter;
        private System.Windows.Forms.TextBox наименованиеПоликлинникиTextBox;
        private System.Windows.Forms.TextBox кодПоликлинникиTextBox;
        private System.Windows.Forms.TextBox юредическийАдресTextBox;
        private System.Windows.Forms.TextBox фактическийАдресTextBox;
        private System.Windows.Forms.TextBox фИО_ГлавВрачTextBox;
        private System.Windows.Forms.TextBox фИО_ГлавБухTextBox;
        private System.Windows.Forms.TextBox свидетельствоРегистрацииTextBox;
        private System.Windows.Forms.TextBox иННTextBox;
        private System.Windows.Forms.TextBox кППTextBox;
        private System.Windows.Forms.TextBox бИКTextBox;
        private System.Windows.Forms.TextBox наименованиеБанкаTextBox;
        private System.Windows.Forms.TextBox расчётныйСчётTextBox;
        private System.Windows.Forms.TextBox номерЛиценииTextBox;
        private System.Windows.Forms.DateTimePicker датаРегистрацииЛиценииDateTimePicker;
        private System.Windows.Forms.TextBox оРГНTextBox;
        private System.Windows.Forms.TextBox свидетельствоРегистрацииЕГРЮЛTextBox;
        private System.Windows.Forms.TextBox органВыдавшийСвидетельствоTextBox;
        private System.Windows.Forms.Button button1;
    }
}