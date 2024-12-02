
namespace yslada
{
    partial class editDishes
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
            this.costTB = new System.Windows.Forms.TextBox();
            this.costLB = new System.Windows.Forms.Label();
            this.categoryCB = new System.Windows.Forms.ComboBox();
            this.categoryLB = new System.Windows.Forms.Label();
            this.descriptionTB = new System.Windows.Forms.TextBox();
            this.descriptionLB = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.nameLB = new System.Windows.Forms.Label();
            this.editBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.dishesPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dishesPB)).BeginInit();
            this.SuspendLayout();
            // 
            // costTB
            // 
            this.costTB.Location = new System.Drawing.Point(12, 375);
            this.costTB.Name = "costTB";
            this.costTB.Size = new System.Drawing.Size(236, 33);
            this.costTB.TabIndex = 95;
            // 
            // costLB
            // 
            this.costLB.AutoSize = true;
            this.costLB.BackColor = System.Drawing.Color.Transparent;
            this.costLB.Location = new System.Drawing.Point(7, 347);
            this.costLB.Name = "costLB";
            this.costLB.Size = new System.Drawing.Size(174, 25);
            this.costLB.TabIndex = 94;
            this.costLB.Text = "Стоимость блюда";
            // 
            // categoryCB
            // 
            this.categoryCB.FormattingEnabled = true;
            this.categoryCB.Location = new System.Drawing.Point(12, 297);
            this.categoryCB.Name = "categoryCB";
            this.categoryCB.Size = new System.Drawing.Size(236, 33);
            this.categoryCB.TabIndex = 93;
            // 
            // categoryLB
            // 
            this.categoryLB.AutoSize = true;
            this.categoryLB.BackColor = System.Drawing.Color.Transparent;
            this.categoryLB.Location = new System.Drawing.Point(7, 269);
            this.categoryLB.Name = "categoryLB";
            this.categoryLB.Size = new System.Drawing.Size(107, 25);
            this.categoryLB.TabIndex = 92;
            this.categoryLB.Text = "Категория";
            // 
            // descriptionTB
            // 
            this.descriptionTB.Location = new System.Drawing.Point(291, 119);
            this.descriptionTB.Multiline = true;
            this.descriptionTB.Name = "descriptionTB";
            this.descriptionTB.Size = new System.Drawing.Size(228, 289);
            this.descriptionTB.TabIndex = 91;
            // 
            // descriptionLB
            // 
            this.descriptionLB.AutoSize = true;
            this.descriptionLB.BackColor = System.Drawing.Color.Transparent;
            this.descriptionLB.Location = new System.Drawing.Point(286, 91);
            this.descriptionLB.Name = "descriptionLB";
            this.descriptionLB.Size = new System.Drawing.Size(102, 25);
            this.descriptionLB.TabIndex = 90;
            this.descriptionLB.Text = "Описание";
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(291, 40);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(228, 33);
            this.nameTB.TabIndex = 89;
            // 
            // nameLB
            // 
            this.nameLB.AutoSize = true;
            this.nameLB.BackColor = System.Drawing.Color.Transparent;
            this.nameLB.Location = new System.Drawing.Point(286, 12);
            this.nameLB.Name = "nameLB";
            this.nameLB.Size = new System.Drawing.Size(110, 25);
            this.nameLB.TabIndex = 88;
            this.nameLB.Text = "Имя блюда";
            // 
            // editBtn
            // 
            this.editBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.editBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editBtn.Location = new System.Drawing.Point(12, 430);
            this.editBtn.Margin = new System.Windows.Forms.Padding(6);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(233, 40);
            this.editBtn.TabIndex = 87;
            this.editBtn.Text = "Изменить";
            this.editBtn.UseVisualStyleBackColor = false;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.LightSalmon;
            this.closeBtn.Font = new System.Drawing.Font("Nirmala UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.Location = new System.Drawing.Point(291, 430);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(6);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(238, 40);
            this.closeBtn.TabIndex = 86;
            this.closeBtn.Text = "Выход в меню ";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // dishesPB
            // 
            this.dishesPB.BackColor = System.Drawing.Color.LightSalmon;
            this.dishesPB.Location = new System.Drawing.Point(12, 12);
            this.dishesPB.Name = "dishesPB";
            this.dishesPB.Size = new System.Drawing.Size(236, 233);
            this.dishesPB.TabIndex = 85;
            this.dishesPB.TabStop = false;
            // 
            // editDishes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yslada.Properties.Resources.AddDishesBG;
            this.ClientSize = new System.Drawing.Size(544, 481);
            this.Controls.Add(this.costTB);
            this.Controls.Add(this.costLB);
            this.Controls.Add(this.categoryCB);
            this.Controls.Add(this.categoryLB);
            this.Controls.Add(this.descriptionTB);
            this.Controls.Add(this.descriptionLB);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.nameLB);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.dishesPB);
            this.Font = new System.Drawing.Font("Nirmala UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "editDishes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "editDishes";
            this.Load += new System.EventHandler(this.editDishes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dishesPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox costTB;
        private System.Windows.Forms.Label costLB;
        private System.Windows.Forms.ComboBox categoryCB;
        private System.Windows.Forms.Label categoryLB;
        private System.Windows.Forms.TextBox descriptionTB;
        private System.Windows.Forms.Label descriptionLB;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label nameLB;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.PictureBox dishesPB;
    }
}