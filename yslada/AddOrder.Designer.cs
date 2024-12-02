
namespace yslada
{
    partial class AddOrder
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
            this.button1 = new System.Windows.Forms.Button();
            this.orderDGV = new System.Windows.Forms.DataGridView();
            this.addOrderBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timeOrder = new System.Windows.Forms.Label();
            this.tableCB = new System.Windows.Forms.ComboBox();
            this.sumOrder = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.employeeTB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.orderDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSalmon;
            this.button1.Location = new System.Drawing.Point(684, 342);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Назад к меню";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // orderDGV
            // 
            this.orderDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.orderDGV.BackgroundColor = System.Drawing.Color.LightSalmon;
            this.orderDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderDGV.Location = new System.Drawing.Point(12, 12);
            this.orderDGV.Name = "orderDGV";
            this.orderDGV.Size = new System.Drawing.Size(437, 371);
            this.orderDGV.TabIndex = 1;
            // 
            // addOrderBtn
            // 
            this.addOrderBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.addOrderBtn.Location = new System.Drawing.Point(470, 342);
            this.addOrderBtn.Name = "addOrderBtn";
            this.addOrderBtn.Size = new System.Drawing.Size(166, 41);
            this.addOrderBtn.TabIndex = 2;
            this.addOrderBtn.Text = "Создать заказ";
            this.addOrderBtn.UseVisualStyleBackColor = false;
            this.addOrderBtn.Click += new System.EventHandler(this.addOrderBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(465, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Номер столика";
            // 
            // timeOrder
            // 
            this.timeOrder.AutoSize = true;
            this.timeOrder.BackColor = System.Drawing.Color.Transparent;
            this.timeOrder.Location = new System.Drawing.Point(465, 304);
            this.timeOrder.Name = "timeOrder";
            this.timeOrder.Size = new System.Drawing.Size(20, 25);
            this.timeOrder.TabIndex = 10;
            this.timeOrder.Text = "*";
            // 
            // tableCB
            // 
            this.tableCB.FormattingEnabled = true;
            this.tableCB.Location = new System.Drawing.Point(626, 48);
            this.tableCB.Name = "tableCB";
            this.tableCB.Size = new System.Drawing.Size(131, 33);
            this.tableCB.TabIndex = 11;
            // 
            // sumOrder
            // 
            this.sumOrder.AutoSize = true;
            this.sumOrder.BackColor = System.Drawing.Color.Transparent;
            this.sumOrder.Location = new System.Drawing.Point(766, 304);
            this.sumOrder.Name = "sumOrder";
            this.sumOrder.Size = new System.Drawing.Size(20, 25);
            this.sumOrder.TabIndex = 12;
            this.sumOrder.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(679, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Итого:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(465, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Сотрудник ";
            // 
            // employeeTB
            // 
            this.employeeTB.AutoSize = true;
            this.employeeTB.BackColor = System.Drawing.Color.Transparent;
            this.employeeTB.Location = new System.Drawing.Point(465, 181);
            this.employeeTB.Name = "employeeTB";
            this.employeeTB.Size = new System.Drawing.Size(20, 25);
            this.employeeTB.TabIndex = 18;
            this.employeeTB.Text = "*";
            // 
            // AddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yslada.Properties.Resources.MenuBG2;
            this.ClientSize = new System.Drawing.Size(849, 393);
            this.Controls.Add(this.employeeTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sumOrder);
            this.Controls.Add(this.tableCB);
            this.Controls.Add(this.timeOrder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addOrderBtn);
            this.Controls.Add(this.orderDGV);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AddOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление продуктов";
            this.Load += new System.EventHandler(this.AddOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.orderDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView orderDGV;
        private System.Windows.Forms.Button addOrderBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timeOrder;
        private System.Windows.Forms.ComboBox tableCB;
        private System.Windows.Forms.Label sumOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label employeeTB;
    }
}