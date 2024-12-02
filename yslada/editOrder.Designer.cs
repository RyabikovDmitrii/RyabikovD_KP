
namespace yslada
{
    partial class editOrder
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
            this.statusCB = new System.Windows.Forms.ComboBox();
            this.editBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.fioCB = new System.Windows.Forms.ComboBox();
            this.tableCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.structureDGV = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.countBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.structureDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // statusCB
            // 
            this.statusCB.FormattingEnabled = true;
            this.statusCB.Location = new System.Drawing.Point(460, 200);
            this.statusCB.Name = "statusCB";
            this.statusCB.Size = new System.Drawing.Size(374, 33);
            this.statusCB.TabIndex = 76;
            // 
            // editBtn
            // 
            this.editBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.editBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBtn.Location = new System.Drawing.Point(460, 388);
            this.editBtn.Margin = new System.Windows.Forms.Padding(6);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(176, 37);
            this.editBtn.TabIndex = 74;
            this.editBtn.Text = "Изменить";
            this.editBtn.UseVisualStyleBackColor = false;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.closeBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(674, 388);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(6);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(162, 37);
            this.closeBtn.TabIndex = 73;
            this.closeBtn.Text = "Выход в меню ";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(455, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 65;
            this.label4.Text = "Статус заказа";
            // 
            // fioCB
            // 
            this.fioCB.FormattingEnabled = true;
            this.fioCB.Location = new System.Drawing.Point(460, 38);
            this.fioCB.Name = "fioCB";
            this.fioCB.Size = new System.Drawing.Size(374, 33);
            this.fioCB.TabIndex = 77;
            // 
            // tableCB
            // 
            this.tableCB.FormattingEnabled = true;
            this.tableCB.Location = new System.Drawing.Point(460, 120);
            this.tableCB.Name = "tableCB";
            this.tableCB.Size = new System.Drawing.Size(374, 33);
            this.tableCB.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(455, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 25);
            this.label2.TabIndex = 79;
            this.label2.Text = "Номер стола";
            // 
            // structureDGV
            // 
            this.structureDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.structureDGV.BackgroundColor = System.Drawing.Color.LightSalmon;
            this.structureDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.structureDGV.Location = new System.Drawing.Point(12, 10);
            this.structureDGV.Name = "structureDGV";
            this.structureDGV.ReadOnly = true;
            this.structureDGV.Size = new System.Drawing.Size(437, 415);
            this.structureDGV.TabIndex = 80;
            this.structureDGV.SelectionChanged += new System.EventHandler(this.structureDGV_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(455, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 25);
            this.label3.TabIndex = 81;
            this.label3.Text = "ФИО сотрудника";
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.addBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.Location = new System.Drawing.Point(460, 315);
            this.addBtn.Margin = new System.Windows.Forms.Padding(6);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(176, 61);
            this.addBtn.TabIndex = 82;
            this.addBtn.Text = "Добавить блюдо  в заказ";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.deleteBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteBtn.Location = new System.Drawing.Point(674, 315);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(6);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(162, 61);
            this.deleteBtn.TabIndex = 83;
            this.deleteBtn.Text = "Удалить блюдо из заказа ";
            this.deleteBtn.UseVisualStyleBackColor = false;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // countBtn
            // 
            this.countBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.countBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countBtn.Location = new System.Drawing.Point(460, 242);
            this.countBtn.Margin = new System.Windows.Forms.Padding(6);
            this.countBtn.Name = "countBtn";
            this.countBtn.Size = new System.Drawing.Size(176, 61);
            this.countBtn.TabIndex = 84;
            this.countBtn.Text = "Изменить количество";
            this.countBtn.UseVisualStyleBackColor = false;
            this.countBtn.Click += new System.EventHandler(this.countBtn_Click);
            // 
            // editOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yslada.Properties.Resources.MenuBG2;
            this.ClientSize = new System.Drawing.Size(849, 438);
            this.Controls.Add(this.countBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.structureDGV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableCB);
            this.Controls.Add(this.fioCB);
            this.Controls.Add(this.statusCB);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Nirmala UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "editOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "editOrder";
            ((System.ComponentModel.ISupportInitialize)(this.structureDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox statusCB;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox fioCB;
        private System.Windows.Forms.ComboBox tableCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView structureDGV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button countBtn;
    }
}