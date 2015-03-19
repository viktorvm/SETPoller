namespace COMPort
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.startBtn = new System.Windows.Forms.Button();
            this.trafficTBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.portNameTBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.baudRateTBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.timeOutTBox = new System.Windows.Forms.TextBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.K2uCBox = new System.Windows.Forms.CheckBox();
            this.KumfCBox = new System.Windows.Forms.CheckBox();
            this.UfCBox = new System.Windows.Forms.CheckBox();
            this.SCBox = new System.Windows.Forms.CheckBox();
            this.TCBox = new System.Windows.Forms.CheckBox();
            this.K0uCBox = new System.Windows.Forms.CheckBox();
            this.UmfCBox = new System.Windows.Forms.CheckBox();
            this.ICBox = new System.Windows.Forms.CheckBox();
            this.QCBox = new System.Windows.Forms.CheckBox();
            this.FCBox = new System.Windows.Forms.CheckBox();
            this.U1_1CBox = new System.Windows.Forms.CheckBox();
            this.KufCBox = new System.Windows.Forms.CheckBox();
            this.CosPhCBox = new System.Windows.Forms.CheckBox();
            this.PCBox = new System.Windows.Forms.CheckBox();
            this.pollPeriodTBox = new System.Windows.Forms.TextBox();
            this.devIDsTBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServNameTBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DBNameTBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(13, 288);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Старт";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // trafficTBox
            // 
            this.trafficTBox.Location = new System.Drawing.Point(6, 330);
            this.trafficTBox.Multiline = true;
            this.trafficTBox.Name = "trafficTBox";
            this.trafficTBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.trafficTBox.Size = new System.Drawing.Size(416, 229);
            this.trafficTBox.TabIndex = 1;
            this.trafficTBox.TextChanged += new System.EventHandler(this.trafficTBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Traffic monitor";
            // 
            // portNameTBox
            // 
            this.portNameTBox.Location = new System.Drawing.Point(60, 17);
            this.portNameTBox.Name = "portNameTBox";
            this.portNameTBox.Size = new System.Drawing.Size(71, 20);
            this.portNameTBox.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Порт";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Скорость";
            // 
            // baudRateTBox
            // 
            this.baudRateTBox.Location = new System.Drawing.Point(61, 42);
            this.baudRateTBox.Name = "baudRateTBox";
            this.baudRateTBox.Size = new System.Drawing.Size(71, 20);
            this.baudRateTBox.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(140, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Четность";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(278, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Стоп бит";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(206, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(71, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Нет";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(339, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(278, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Бит данных";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(140, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Таймаут,мс";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(339, 17);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(71, 20);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "8";
            // 
            // timeOutTBox
            // 
            this.timeOutTBox.Location = new System.Drawing.Point(206, 17);
            this.timeOutTBox.Name = "timeOutTBox";
            this.timeOutTBox.Size = new System.Drawing.Size(71, 20);
            this.timeOutTBox.TabIndex = 8;
            // 
            // stopBtn
            // 
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(345, 288);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 0;
            this.stopBtn.Text = "Стоп";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.timeOutTBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.baudRateTBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.portNameTBox);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(6, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 66);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки порта:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.K2uCBox);
            this.groupBox2.Controls.Add(this.KumfCBox);
            this.groupBox2.Controls.Add(this.UfCBox);
            this.groupBox2.Controls.Add(this.SCBox);
            this.groupBox2.Controls.Add(this.TCBox);
            this.groupBox2.Controls.Add(this.K0uCBox);
            this.groupBox2.Controls.Add(this.UmfCBox);
            this.groupBox2.Controls.Add(this.ICBox);
            this.groupBox2.Controls.Add(this.QCBox);
            this.groupBox2.Controls.Add(this.FCBox);
            this.groupBox2.Controls.Add(this.U1_1CBox);
            this.groupBox2.Controls.Add(this.KufCBox);
            this.groupBox2.Controls.Add(this.CosPhCBox);
            this.groupBox2.Controls.Add(this.PCBox);
            this.groupBox2.Controls.Add(this.pollPeriodTBox);
            this.groupBox2.Controls.Add(this.devIDsTBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(6, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 142);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки устройства:";
            // 
            // K2uCBox
            // 
            this.K2uCBox.AutoSize = true;
            this.K2uCBox.Checked = true;
            this.K2uCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.K2uCBox.Location = new System.Drawing.Point(170, 114);
            this.K2uCBox.Name = "K2uCBox";
            this.K2uCBox.Size = new System.Drawing.Size(45, 17);
            this.K2uCBox.TabIndex = 6;
            this.K2uCBox.Text = "K2u";
            this.K2uCBox.UseVisualStyleBackColor = true;
            // 
            // KumfCBox
            // 
            this.KumfCBox.AutoSize = true;
            this.KumfCBox.Checked = true;
            this.KumfCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KumfCBox.Location = new System.Drawing.Point(116, 114);
            this.KumfCBox.Name = "KumfCBox";
            this.KumfCBox.Size = new System.Drawing.Size(50, 17);
            this.KumfCBox.TabIndex = 6;
            this.KumfCBox.Text = "Kumf";
            this.KumfCBox.UseVisualStyleBackColor = true;
            // 
            // UfCBox
            // 
            this.UfCBox.AutoSize = true;
            this.UfCBox.Checked = true;
            this.UfCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UfCBox.Location = new System.Drawing.Point(53, 114);
            this.UfCBox.Name = "UfCBox";
            this.UfCBox.Size = new System.Drawing.Size(37, 17);
            this.UfCBox.TabIndex = 6;
            this.UfCBox.Text = "Uf";
            this.UfCBox.UseVisualStyleBackColor = true;
            // 
            // SCBox
            // 
            this.SCBox.AutoSize = true;
            this.SCBox.Checked = true;
            this.SCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SCBox.Location = new System.Drawing.Point(10, 114);
            this.SCBox.Name = "SCBox";
            this.SCBox.Size = new System.Drawing.Size(33, 17);
            this.SCBox.TabIndex = 6;
            this.SCBox.Text = "S";
            this.SCBox.UseVisualStyleBackColor = true;
            // 
            // TCBox
            // 
            this.TCBox.AutoSize = true;
            this.TCBox.Checked = true;
            this.TCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TCBox.Location = new System.Drawing.Point(228, 91);
            this.TCBox.Name = "TCBox";
            this.TCBox.Size = new System.Drawing.Size(33, 17);
            this.TCBox.TabIndex = 6;
            this.TCBox.Text = "T";
            this.TCBox.UseVisualStyleBackColor = true;
            // 
            // K0uCBox
            // 
            this.K0uCBox.AutoSize = true;
            this.K0uCBox.Checked = true;
            this.K0uCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.K0uCBox.Location = new System.Drawing.Point(170, 91);
            this.K0uCBox.Name = "K0uCBox";
            this.K0uCBox.Size = new System.Drawing.Size(45, 17);
            this.K0uCBox.TabIndex = 6;
            this.K0uCBox.Text = "K0u";
            this.K0uCBox.UseVisualStyleBackColor = true;
            // 
            // UmfCBox
            // 
            this.UmfCBox.AutoSize = true;
            this.UmfCBox.Checked = true;
            this.UmfCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UmfCBox.Location = new System.Drawing.Point(116, 91);
            this.UmfCBox.Name = "UmfCBox";
            this.UmfCBox.Size = new System.Drawing.Size(45, 17);
            this.UmfCBox.TabIndex = 6;
            this.UmfCBox.Text = "Umf";
            this.UmfCBox.UseVisualStyleBackColor = true;
            // 
            // ICBox
            // 
            this.ICBox.AutoSize = true;
            this.ICBox.Checked = true;
            this.ICBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ICBox.Location = new System.Drawing.Point(53, 91);
            this.ICBox.Name = "ICBox";
            this.ICBox.Size = new System.Drawing.Size(29, 17);
            this.ICBox.TabIndex = 6;
            this.ICBox.Text = "I";
            this.ICBox.UseVisualStyleBackColor = true;
            // 
            // QCBox
            // 
            this.QCBox.AutoSize = true;
            this.QCBox.Checked = true;
            this.QCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.QCBox.Location = new System.Drawing.Point(10, 91);
            this.QCBox.Name = "QCBox";
            this.QCBox.Size = new System.Drawing.Size(34, 17);
            this.QCBox.TabIndex = 6;
            this.QCBox.Text = "Q";
            this.QCBox.UseVisualStyleBackColor = true;
            // 
            // FCBox
            // 
            this.FCBox.AutoSize = true;
            this.FCBox.Checked = true;
            this.FCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FCBox.Location = new System.Drawing.Point(228, 68);
            this.FCBox.Name = "FCBox";
            this.FCBox.Size = new System.Drawing.Size(32, 17);
            this.FCBox.TabIndex = 6;
            this.FCBox.Text = "F";
            this.FCBox.UseVisualStyleBackColor = true;
            // 
            // U1_1CBox
            // 
            this.U1_1CBox.AutoSize = true;
            this.U1_1CBox.Checked = true;
            this.U1_1CBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.U1_1CBox.Location = new System.Drawing.Point(170, 68);
            this.U1_1CBox.Name = "U1_1CBox";
            this.U1_1CBox.Size = new System.Drawing.Size(52, 17);
            this.U1_1CBox.TabIndex = 6;
            this.U1_1CBox.Text = "U1(1)";
            this.U1_1CBox.UseVisualStyleBackColor = true;
            // 
            // KufCBox
            // 
            this.KufCBox.AutoSize = true;
            this.KufCBox.Checked = true;
            this.KufCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KufCBox.Location = new System.Drawing.Point(116, 68);
            this.KufCBox.Name = "KufCBox";
            this.KufCBox.Size = new System.Drawing.Size(42, 17);
            this.KufCBox.TabIndex = 6;
            this.KufCBox.Text = "Kuf";
            this.KufCBox.UseVisualStyleBackColor = true;
            // 
            // CosPhCBox
            // 
            this.CosPhCBox.AutoSize = true;
            this.CosPhCBox.Checked = true;
            this.CosPhCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CosPhCBox.Location = new System.Drawing.Point(53, 68);
            this.CosPhCBox.Name = "CosPhCBox";
            this.CosPhCBox.Size = new System.Drawing.Size(57, 17);
            this.CosPhCBox.TabIndex = 6;
            this.CosPhCBox.Text = "CosPh";
            this.CosPhCBox.UseVisualStyleBackColor = true;
            // 
            // PCBox
            // 
            this.PCBox.AutoSize = true;
            this.PCBox.Checked = true;
            this.PCBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PCBox.Location = new System.Drawing.Point(10, 68);
            this.PCBox.Name = "PCBox";
            this.PCBox.Size = new System.Drawing.Size(33, 17);
            this.PCBox.TabIndex = 6;
            this.PCBox.Text = "P";
            this.PCBox.UseVisualStyleBackColor = true;
            // 
            // pollPeriodTBox
            // 
            this.pollPeriodTBox.Location = new System.Drawing.Point(339, 19);
            this.pollPeriodTBox.Name = "pollPeriodTBox";
            this.pollPeriodTBox.Size = new System.Drawing.Size(71, 20);
            this.pollPeriodTBox.TabIndex = 0;
            // 
            // devIDsTBox
            // 
            this.devIDsTBox.Location = new System.Drawing.Point(28, 19);
            this.devIDsTBox.Name = "devIDsTBox";
            this.devIDsTBox.Size = new System.Drawing.Size(142, 20);
            this.devIDsTBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Опрашиваемые параметры:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Период опроса,мс";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID";
            // 
            // ServNameTBox
            // 
            this.ServNameTBox.Location = new System.Drawing.Point(107, 12);
            this.ServNameTBox.Name = "ServNameTBox";
            this.ServNameTBox.Size = new System.Drawing.Size(146, 20);
            this.ServNameTBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Имя SQL-сервера";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Имя БД";
            // 
            // DBNameTBox
            // 
            this.DBNameTBox.Location = new System.Drawing.Point(107, 38);
            this.DBNameTBox.Name = "DBNameTBox";
            this.DBNameTBox.Size = new System.Drawing.Size(99, 20);
            this.DBNameTBox.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 616);
            this.Controls.Add(this.DBNameTBox);
            this.Controls.Add(this.ServNameTBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trafficTBox);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Name = "Form1";
            this.Text = "Опрос счетчиков СЭТ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox trafficTBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portNameTBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox baudRateTBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox timeOutTBox;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox devIDsTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pollPeriodTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox PCBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox QCBox;
        private System.Windows.Forms.CheckBox SCBox;
        private System.Windows.Forms.CheckBox UfCBox;
        private System.Windows.Forms.CheckBox ICBox;
        private System.Windows.Forms.CheckBox CosPhCBox;
        private System.Windows.Forms.CheckBox KumfCBox;
        private System.Windows.Forms.CheckBox UmfCBox;
        private System.Windows.Forms.CheckBox KufCBox;
        private System.Windows.Forms.CheckBox K2uCBox;
        private System.Windows.Forms.CheckBox K0uCBox;
        private System.Windows.Forms.CheckBox U1_1CBox;
        private System.Windows.Forms.CheckBox TCBox;
        private System.Windows.Forms.CheckBox FCBox;
        private System.Windows.Forms.TextBox ServNameTBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox DBNameTBox;
    }
}

