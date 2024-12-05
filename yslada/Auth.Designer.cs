
namespace yslada
{
    partial class Auth
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
            this.loginTB = new System.Windows.Forms.TextBox();
            this.passwdTB = new System.Windows.Forms.TextBox();
            this.authBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fieldTB = new System.Windows.Forms.TextBox();
            this.captchaPB = new System.Windows.Forms.PictureBox();
            this.checkBtn = new System.Windows.Forms.Button();
            this.updateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPB)).BeginInit();
            this.SuspendLayout();
            // 
            // loginTB
            // 
            this.loginTB.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginTB.Location = new System.Drawing.Point(106, 130);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(201, 33);
            this.loginTB.TabIndex = 0;
            // 
            // passwdTB
            // 
            this.passwdTB.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwdTB.Location = new System.Drawing.Point(106, 206);
            this.passwdTB.Name = "passwdTB";
            this.passwdTB.PasswordChar = '*';
            this.passwdTB.Size = new System.Drawing.Size(202, 33);
            this.passwdTB.TabIndex = 1;
            // 
            // authBtn
            // 
            this.authBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.authBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authBtn.Location = new System.Drawing.Point(12, 299);
            this.authBtn.Name = "authBtn";
            this.authBtn.Size = new System.Drawing.Size(296, 47);
            this.authBtn.TabIndex = 2;
            this.authBtn.Text = "Авторизоваться";
            this.authBtn.UseVisualStyleBackColor = false;
            this.authBtn.Click += new System.EventHandler(this.authBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(76, 79);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(165, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Авторизация";
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.LightSalmon;
            this.Close.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(12, 375);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(296, 34);
            this.Close.TabIndex = 4;
            this.Close.Text = "Выход";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Логин:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Пароль:";
            // 
            // fieldTB
            // 
            this.fieldTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTB.Location = new System.Drawing.Point(405, 281);
            this.fieldTB.Margin = new System.Windows.Forms.Padding(2);
            this.fieldTB.Name = "fieldTB";
            this.fieldTB.Size = new System.Drawing.Size(261, 32);
            this.fieldTB.TabIndex = 23;
            // 
            // captchaPB
            // 
            this.captchaPB.BackColor = System.Drawing.Color.LightSalmon;
            this.captchaPB.Location = new System.Drawing.Point(405, 97);
            this.captchaPB.Margin = new System.Windows.Forms.Padding(2);
            this.captchaPB.Name = "captchaPB";
            this.captchaPB.Size = new System.Drawing.Size(261, 152);
            this.captchaPB.TabIndex = 22;
            this.captchaPB.TabStop = false;
            // 
            // checkBtn
            // 
            this.checkBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.checkBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBtn.Location = new System.Drawing.Point(405, 373);
            this.checkBtn.Margin = new System.Windows.Forms.Padding(2);
            this.checkBtn.Name = "checkBtn";
            this.checkBtn.Size = new System.Drawing.Size(261, 36);
            this.checkBtn.TabIndex = 21;
            this.checkBtn.Text = "Проверить";
            this.checkBtn.UseVisualStyleBackColor = false;
            this.checkBtn.Click += new System.EventHandler(this.checkBtn_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.updateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateBtn.Location = new System.Drawing.Point(405, 333);
            this.updateBtn.Margin = new System.Windows.Forms.Padding(2);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(261, 36);
            this.updateBtn.TabIndex = 20;
            this.updateBtn.Text = "Обновить";
            this.updateBtn.UseVisualStyleBackColor = false;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // Auth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::yslada.Properties.Resources.MenuBG;
            this.ClientSize = new System.Drawing.Size(323, 421);
            this.Controls.Add(this.fieldTB);
            this.Controls.Add(this.captchaPB);
            this.Controls.Add(this.checkBtn);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.authBtn);
            this.Controls.Add(this.passwdTB);
            this.Controls.Add(this.loginTB);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Auth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Кафе \"Услада\"";
            ((System.ComponentModel.ISupportInitialize)(this.captchaPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox passwdTB;
        private System.Windows.Forms.Button authBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fieldTB;
        private System.Windows.Forms.PictureBox captchaPB;
        private System.Windows.Forms.Button checkBtn;
        private System.Windows.Forms.Button updateBtn;
    }
}

