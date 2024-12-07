
namespace yslada
{
    partial class MenuDishes
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
            this.DishesDGW = new System.Windows.Forms.DataGridView();
            this.SearchTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SortCB = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.FilterCB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.forwardBtn = new System.Windows.Forms.Button();
            this.countRowLB = new System.Windows.Forms.Label();
            this.page1LB = new System.Windows.Forms.Label();
            this.page2LB = new System.Windows.Forms.Label();
            this.page3LB = new System.Windows.Forms.Label();
            this.colorSquareLabel2 = new yslada.ColorSquareLabel();
            this.colorSquareLabel1 = new yslada.ColorSquareLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DishesDGW)).BeginInit();
            this.SuspendLayout();
            // 
            // DishesDGW
            // 
            this.DishesDGW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DishesDGW.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DishesDGW.BackgroundColor = System.Drawing.Color.DarkSalmon;
            this.DishesDGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DishesDGW.Location = new System.Drawing.Point(16, 93);
            this.DishesDGW.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.DishesDGW.Name = "DishesDGW";
            this.DishesDGW.ReadOnly = true;
            this.DishesDGW.Size = new System.Drawing.Size(1304, 570);
            this.DishesDGW.TabIndex = 0;
            this.DishesDGW.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DishesDGW_CellFormatting);
            // 
            // SearchTB
            // 
            this.SearchTB.BackColor = System.Drawing.SystemColors.Control;
            this.SearchTB.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTB.Location = new System.Drawing.Point(17, 48);
            this.SearchTB.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.SearchTB.Name = "SearchTB";
            this.SearchTB.Size = new System.Drawing.Size(655, 33);
            this.SearchTB.TabIndex = 3;
            this.SearchTB.TextChanged += new System.EventHandler(this.SearchTB_TextChanged);
            this.SearchTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchTB_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Поиск по наименованию";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1293, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 5;
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.closeBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(1087, 673);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(231, 40);
            this.closeBtn.TabIndex = 23;
            this.closeBtn.Text = "Выход в меню ";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(715, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 25);
            this.label3.TabIndex = 27;
            this.label3.Text = "Сортировка цены";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1592, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 25);
            this.label4.TabIndex = 30;
            this.label4.Text = "Фильтрация по:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1919, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 25);
            this.label5.TabIndex = 28;
            // 
            // SortCB
            // 
            this.SortCB.BackColor = System.Drawing.SystemColors.Control;
            this.SortCB.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SortCB.FormattingEnabled = true;
            this.SortCB.Location = new System.Drawing.Point(720, 48);
            this.SortCB.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.SortCB.Name = "SortCB";
            this.SortCB.Size = new System.Drawing.Size(276, 33);
            this.SortCB.TabIndex = 31;
            this.SortCB.SelectedIndexChanged += new System.EventHandler(this.SortCB_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(1597, 48);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(189, 33);
            this.comboBox2.TabIndex = 32;
            // 
            // FilterCB
            // 
            this.FilterCB.BackColor = System.Drawing.SystemColors.Control;
            this.FilterCB.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterCB.FormattingEnabled = true;
            this.FilterCB.Location = new System.Drawing.Point(1043, 48);
            this.FilterCB.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.FilterCB.Name = "FilterCB";
            this.FilterCB.Size = new System.Drawing.Size(276, 33);
            this.FilterCB.TabIndex = 34;
            this.FilterCB.SelectedIndexChanged += new System.EventHandler(this.FilterCB_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1039, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(226, 25);
            this.label6.TabIndex = 33;
            this.label6.Text = "Фильтрация категории ";
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.Transparent;
            this.backBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.backBtn.FlatAppearance.BorderSize = 0;
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.Image = global::yslada.Properties.Resources.left;
            this.backBtn.Location = new System.Drawing.Point(16, 668);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(46, 48);
            this.backBtn.TabIndex = 35;
            this.backBtn.UseVisualStyleBackColor = false;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // forwardBtn
            // 
            this.forwardBtn.BackColor = System.Drawing.Color.Transparent;
            this.forwardBtn.FlatAppearance.BorderSize = 0;
            this.forwardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.forwardBtn.Image = global::yslada.Properties.Resources.right;
            this.forwardBtn.Location = new System.Drawing.Point(75, 668);
            this.forwardBtn.Name = "forwardBtn";
            this.forwardBtn.Size = new System.Drawing.Size(46, 48);
            this.forwardBtn.TabIndex = 36;
            this.forwardBtn.UseVisualStyleBackColor = false;
            this.forwardBtn.Click += new System.EventHandler(this.forwardBtn_Click);
            // 
            // countRowLB
            // 
            this.countRowLB.AutoSize = true;
            this.countRowLB.BackColor = System.Drawing.Color.Transparent;
            this.countRowLB.Location = new System.Drawing.Point(270, 680);
            this.countRowLB.Name = "countRowLB";
            this.countRowLB.Size = new System.Drawing.Size(20, 25);
            this.countRowLB.TabIndex = 38;
            this.countRowLB.Text = "*";
            // 
            // page1LB
            // 
            this.page1LB.AutoSize = true;
            this.page1LB.BackColor = System.Drawing.Color.Transparent;
            this.page1LB.Location = new System.Drawing.Point(170, 680);
            this.page1LB.Name = "page1LB";
            this.page1LB.Size = new System.Drawing.Size(22, 25);
            this.page1LB.TabIndex = 39;
            this.page1LB.Text = "1";
            // 
            // page2LB
            // 
            this.page2LB.AutoSize = true;
            this.page2LB.BackColor = System.Drawing.Color.Transparent;
            this.page2LB.Location = new System.Drawing.Point(198, 680);
            this.page2LB.Name = "page2LB";
            this.page2LB.Size = new System.Drawing.Size(22, 25);
            this.page2LB.TabIndex = 40;
            this.page2LB.Text = "2";
            // 
            // page3LB
            // 
            this.page3LB.AutoSize = true;
            this.page3LB.BackColor = System.Drawing.Color.Transparent;
            this.page3LB.Location = new System.Drawing.Point(226, 680);
            this.page3LB.Name = "page3LB";
            this.page3LB.Size = new System.Drawing.Size(22, 25);
            this.page3LB.TabIndex = 41;
            this.page3LB.Text = "3";
            // 
            // colorSquareLabel2
            // 
            this.colorSquareLabel2.BackColor = System.Drawing.Color.LightSalmon;
            this.colorSquareLabel2.DisplayText = " - недоступно";
            this.colorSquareLabel2.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorSquareLabel2.Location = new System.Drawing.Point(768, 694);
            this.colorSquareLabel2.Name = "colorSquareLabel2";
            this.colorSquareLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colorSquareLabel2.Size = new System.Drawing.Size(149, 20);
            this.colorSquareLabel2.SquareColor = System.Drawing.Color.Red;
            this.colorSquareLabel2.TabIndex = 43;
            this.colorSquareLabel2.Text = " - недоступно";
            // 
            // colorSquareLabel1
            // 
            this.colorSquareLabel1.BackColor = System.Drawing.Color.LightSalmon;
            this.colorSquareLabel1.DisplayText = " - доступно";
            this.colorSquareLabel1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorSquareLabel1.Location = new System.Drawing.Point(768, 668);
            this.colorSquareLabel1.Name = "colorSquareLabel1";
            this.colorSquareLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colorSquareLabel1.Size = new System.Drawing.Size(149, 20);
            this.colorSquareLabel1.SquareColor = System.Drawing.Color.Green;
            this.colorSquareLabel1.TabIndex = 42;
            this.colorSquareLabel1.Text = " - доступно";
            // 
            // MenuDishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::yslada.Properties.Resources.MenuBG1;
            this.ClientSize = new System.Drawing.Size(1334, 723);
            this.Controls.Add(this.colorSquareLabel2);
            this.Controls.Add(this.colorSquareLabel1);
            this.Controls.Add(this.page3LB);
            this.Controls.Add(this.page2LB);
            this.Controls.Add(this.page1LB);
            this.Controls.Add(this.countRowLB);
            this.Controls.Add(this.forwardBtn);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.FilterCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.SortCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchTB);
            this.Controls.Add(this.DishesDGW);
            this.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "MenuDishes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuDishes";
            this.Load += new System.EventHandler(this.MenuDishes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DishesDGW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DishesDGW;
        private System.Windows.Forms.TextBox SearchTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox SortCB;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox FilterCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button forwardBtn;
        private System.Windows.Forms.Label countRowLB;
        private System.Windows.Forms.Label page1LB;
        private System.Windows.Forms.Label page2LB;
        private System.Windows.Forms.Label page3LB;
        private ColorSquareLabel colorSquareLabel1;
        private ColorSquareLabel colorSquareLabel2;
    }
}