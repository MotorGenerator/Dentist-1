namespace Стамотология
{
    partial class FormЛьготнаяКатегория
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
            System.Windows.Forms.Label льготнаяКатегорияLabel;
            this.db1DataSet = new Стамотология.db1DataSet();
            this.льготнаяКатегорияBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.льготнаяКатегорияTableAdapter = new Стамотология.db1DataSetTableAdapters.ЛьготнаяКатегорияTableAdapter();
            this.льготнаяКатегорияDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.льготнаяКатегорияTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            льготнаяКатегорияLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.db1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.льготнаяКатегорияBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.льготнаяКатегорияDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // льготнаяКатегорияLabel
            // 
            льготнаяКатегорияLabel.AutoSize = true;
            льготнаяКатегорияLabel.Location = new System.Drawing.Point(15, 263);
            льготнаяКатегорияLabel.Name = "льготнаяКатегорияLabel";
            льготнаяКатегорияLabel.Size = new System.Drawing.Size(114, 13);
            льготнаяКатегорияLabel.TabIndex = 2;
            льготнаяКатегорияLabel.Text = "Льготная Категория:";
            // 
            // db1DataSet
            // 
            this.db1DataSet.DataSetName = "db1DataSet";
            this.db1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // льготнаяКатегорияBindingSource
            // 
            this.льготнаяКатегорияBindingSource.DataMember = "ЛьготнаяКатегория";
            this.льготнаяКатегорияBindingSource.DataSource = this.db1DataSet;
            this.льготнаяКатегорияBindingSource.CurrentChanged += new System.EventHandler(this.льготнаяКатегорияBindingSource_CurrentChanged);
            // 
            // льготнаяКатегорияTableAdapter
            // 
            this.льготнаяКатегорияTableAdapter.ClearBeforeFill = true;
            // 
            // льготнаяКатегорияDataGridView
            // 
            this.льготнаяКатегорияDataGridView.AutoGenerateColumns = false;
            this.льготнаяКатегорияDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.льготнаяКатегорияDataGridView.DataSource = this.льготнаяКатегорияBindingSource;
            this.льготнаяКатегорияDataGridView.Location = new System.Drawing.Point(12, 28);
            this.льготнаяКатегорияDataGridView.Name = "льготнаяКатегорияDataGridView";
            this.льготнаяКатегорияDataGridView.Size = new System.Drawing.Size(512, 220);
            this.льготнаяКатегорияDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id_льготнойКатегории";
            this.dataGridViewTextBoxColumn1.HeaderText = "id_льготнойКатегории";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ЛьготнаяКатегория";
            this.dataGridViewTextBoxColumn2.HeaderText = "ЛьготнаяКатегория";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 465;
            // 
            // льготнаяКатегорияTextBox
            // 
            this.льготнаяКатегорияTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.льготнаяКатегорияBindingSource, "ЛьготнаяКатегория", true));
            this.льготнаяКатегорияTextBox.Location = new System.Drawing.Point(135, 260);
            this.льготнаяКатегорияTextBox.Name = "льготнаяКатегорияTextBox";
            this.льготнаяКатегорияTextBox.Size = new System.Drawing.Size(389, 20);
            this.льготнаяКатегорияTextBox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(443, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 286);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(362, 286);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Отмена";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormЛьготнаяКатегория
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 330);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(льготнаяКатегорияLabel);
            this.Controls.Add(this.льготнаяКатегорияTextBox);
            this.Controls.Add(this.льготнаяКатегорияDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormЛьготнаяКатегория";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormЛьготнаяКатегория";
            this.Load += new System.EventHandler(this.FormЛьготнаяКатегория_Load);
            ((System.ComponentModel.ISupportInitialize)(this.db1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.льготнаяКатегорияBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.льготнаяКатегорияDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private db1DataSet db1DataSet;
        private System.Windows.Forms.BindingSource льготнаяКатегорияBindingSource;
        private Стамотология.db1DataSetTableAdapters.ЛьготнаяКатегорияTableAdapter льготнаяКатегорияTableAdapter;
        private System.Windows.Forms.DataGridView льготнаяКатегорияDataGridView;
        private System.Windows.Forms.TextBox льготнаяКатегорияTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}