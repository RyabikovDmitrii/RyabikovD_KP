
namespace yslada
{
    partial class InportData
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
            this.tableCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InportBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.recoverBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableCB
            // 
            this.tableCB.FormattingEnabled = true;
            this.tableCB.Location = new System.Drawing.Point(12, 145);
            this.tableCB.Name = "tableCB";
            this.tableCB.Size = new System.Drawing.Size(364, 33);
            this.tableCB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Импорт данных в таблицу ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Location = new System.Drawing.Point(7, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(372, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выберите таблику для импортирования";
            // 
            // InportBtn
            // 
            this.InportBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.InportBtn.Location = new System.Drawing.Point(12, 261);
            this.InportBtn.Name = "InportBtn";
            this.InportBtn.Size = new System.Drawing.Size(364, 71);
            this.InportBtn.TabIndex = 3;
            this.InportBtn.Text = "Импортирование ";
            this.InportBtn.UseVisualStyleBackColor = false;
            this.InportBtn.Click += new System.EventHandler(this.InportBtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSalmon;
            this.button2.Location = new System.Drawing.Point(12, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(364, 71);
            this.button2.TabIndex = 4;
            this.button2.Text = "Выход";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // recoverBtn
            // 
            this.recoverBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.recoverBtn.Location = new System.Drawing.Point(12, 338);
            this.recoverBtn.Name = "recoverBtn";
            this.recoverBtn.Size = new System.Drawing.Size(364, 71);
            this.recoverBtn.TabIndex = 5;
            this.recoverBtn.Text = "Восстановление";
            this.recoverBtn.UseVisualStyleBackColor = false;
            this.recoverBtn.Click += new System.EventHandler(this.recoverBtn_Click);
            // 
            // InportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yslada.Properties.Resources.AddDishesBG;
            this.ClientSize = new System.Drawing.Size(388, 498);
            this.Controls.Add(this.recoverBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.InportBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableCB);
            this.Font = new System.Drawing.Font("Nirmala UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "InportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InportData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox tableCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button InportBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button recoverBtn;
    }
}