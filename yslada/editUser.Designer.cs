﻿
namespace yslada
{
    partial class editUser
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
            this.roleCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.passwdTB = new System.Windows.Forms.TextBox();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.surnameTB = new System.Windows.Forms.TextBox();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.patronymicTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.surnameLB = new System.Windows.Forms.Label();
            this.numberTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // roleCB
            // 
            this.roleCB.FormattingEnabled = true;
            this.roleCB.Location = new System.Drawing.Point(518, 44);
            this.roleCB.Name = "roleCB";
            this.roleCB.Size = new System.Drawing.Size(277, 33);
            this.roleCB.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(513, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 25);
            this.label1.TabIndex = 59;
            this.label1.Text = "Роль";
            // 
            // editBtn
            // 
            this.editBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.editBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBtn.Location = new System.Drawing.Point(17, 259);
            this.editBtn.Margin = new System.Windows.Forms.Padding(6);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(231, 40);
            this.editBtn.TabIndex = 58;
            this.editBtn.Text = "Изменить";
            this.editBtn.UseVisualStyleBackColor = false;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.closeBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(558, 262);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(6);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(231, 40);
            this.closeBtn.TabIndex = 57;
            this.closeBtn.Text = "Выход в меню ";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // passwdTB
            // 
            this.passwdTB.Location = new System.Drawing.Point(518, 200);
            this.passwdTB.Name = "passwdTB";
            this.passwdTB.Size = new System.Drawing.Size(277, 33);
            this.passwdTB.TabIndex = 56;
            // 
            // loginTB
            // 
            this.loginTB.Location = new System.Drawing.Point(518, 126);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(277, 33);
            this.loginTB.TabIndex = 55;
            // 
            // surnameTB
            // 
            this.surnameTB.Location = new System.Drawing.Point(197, 29);
            this.surnameTB.Name = "surnameTB";
            this.surnameTB.Size = new System.Drawing.Size(211, 33);
            this.surnameTB.TabIndex = 54;
            this.surnameTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_KeyPress);
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(197, 80);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(211, 33);
            this.nameTB.TabIndex = 53;
            this.nameTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_KeyPress);
            // 
            // patronymicTB
            // 
            this.patronymicTB.Location = new System.Drawing.Point(197, 140);
            this.patronymicTB.Name = "patronymicTB";
            this.patronymicTB.Size = new System.Drawing.Size(211, 33);
            this.patronymicTB.TabIndex = 52;
            this.patronymicTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(513, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 25);
            this.label6.TabIndex = 51;
            this.label6.Text = "Пароль";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(513, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 25);
            this.label5.TabIndex = 50;
            this.label5.Text = "Логин";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 25);
            this.label4.TabIndex = 48;
            this.label4.Text = "Номер телефона";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 47;
            this.label3.Text = "Отчество";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 46;
            this.label2.Text = "Имя";
            // 
            // surnameLB
            // 
            this.surnameLB.AutoSize = true;
            this.surnameLB.BackColor = System.Drawing.Color.Transparent;
            this.surnameLB.Location = new System.Drawing.Point(12, 32);
            this.surnameLB.Name = "surnameLB";
            this.surnameLB.Size = new System.Drawing.Size(98, 25);
            this.surnameLB.TabIndex = 45;
            this.surnameLB.Text = "Фамилия ";
            // 
            // numberTB
            // 
            this.numberTB.Location = new System.Drawing.Point(197, 200);
            this.numberTB.MaxLength = 12;
            this.numberTB.Name = "numberTB";
            this.numberTB.Size = new System.Drawing.Size(211, 33);
            this.numberTB.TabIndex = 61;
            // 
            // editUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yslada.Properties.Resources.AddEditUserBG;
            this.ClientSize = new System.Drawing.Size(814, 310);
            this.Controls.Add(this.numberTB);
            this.Controls.Add(this.roleCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.passwdTB);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.surnameTB);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.patronymicTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.surnameLB);
            this.Font = new System.Drawing.Font("Nirmala UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "editUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "editUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox roleCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox passwdTB;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox surnameTB;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.TextBox patronymicTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label surnameLB;
        private System.Windows.Forms.TextBox numberTB;
    }
}