namespace Стамотология
{
    partial class FormReestrPrint
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
            this.grpЛьготнаяКатегория = new System.Windows.Forms.GroupBox();
            this.cmbЛьготнаяКатегория = new System.Windows.Forms.ComboBox();
            this.grpПериод = new System.Windows.Forms.GroupBox();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.grpЛьготнаяКатегория.SuspendLayout();
            this.grpПериод.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpЛьготнаяКатегория
            // 
            this.grpЛьготнаяКатегория.Controls.Add(this.cmbЛьготнаяКатегория);
            this.grpЛьготнаяКатегория.Location = new System.Drawing.Point(12, 12);
            this.grpЛьготнаяКатегория.Name = "grpЛьготнаяКатегория";
            this.grpЛьготнаяКатегория.Size = new System.Drawing.Size(324, 54);
            this.grpЛьготнаяКатегория.TabIndex = 0;
            this.grpЛьготнаяКатегория.TabStop = false;
            this.grpЛьготнаяКатегория.Text = "Льготная категория";
            // 
            // cmbЛьготнаяКатегория
            // 
            this.cmbЛьготнаяКатегория.FormattingEnabled = true;
            this.cmbЛьготнаяКатегория.Location = new System.Drawing.Point(7, 20);
            this.cmbЛьготнаяКатегория.Name = "cmbЛьготнаяКатегория";
            this.cmbЛьготнаяКатегория.Size = new System.Drawing.Size(306, 21);
            this.cmbЛьготнаяКатегория.TabIndex = 0;
            this.cmbЛьготнаяКатегория.Layout += new System.Windows.Forms.LayoutEventHandler(this.cmbЛьготнаяКатегория_Layout);
            // 
            // grpПериод
            // 
            this.grpПериод.Controls.Add(this.dtEnd);
            this.grpПериод.Controls.Add(this.label2);
            this.grpПериод.Controls.Add(this.dtStart);
            this.grpПериод.Controls.Add(this.label1);
            this.grpПериод.Location = new System.Drawing.Point(13, 67);
            this.grpПериод.Name = "grpПериод";
            this.grpПериод.Size = new System.Drawing.Size(323, 53);
            this.grpПериод.TabIndex = 1;
            this.grpПериод.TabStop = false;
            this.grpПериод.Text = "Период";
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(186, 20);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(128, 20);
            this.dtEnd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "по";
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(27, 20);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(128, 20);
            this.dtStart.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "с";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Печать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(177, 126);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormReestrPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 162);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpПериод);
            this.Controls.Add(this.grpЛьготнаяКатегория);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormReestrPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать реестра";
            this.Load += new System.EventHandler(this.FormReestrPrint_Load);
            this.grpЛьготнаяКатегория.ResumeLayout(false);
            this.grpПериод.ResumeLayout(false);
            this.grpПериод.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpЛьготнаяКатегория;
        private System.Windows.Forms.ComboBox cmbЛьготнаяКатегория;
        private System.Windows.Forms.GroupBox grpПериод;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}