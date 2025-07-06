namespace CoffeeCafeProject
{
    partial class FrmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.groupbox1 = new System.Windows.Forms.GroupBox();
            this.pbMenuImage = new System.Windows.Forms.PictureBox();
            this.tbMenuName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMenuPrice = new System.Windows.Forms.TextBox();
            this.btSelectMenuImage = new System.Windows.Forms.Button();
            this.lvShowAllMenu = new System.Windows.Forms.ListView();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMenuId = new System.Windows.Forms.TextBox();
            this.groupbox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SaddleBrown;
            this.label1.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(160, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 68);
            this.label1.TabIndex = 31;
            this.label1.Text = "จัดการข้อมูลเมนู";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupbox1
            // 
            this.groupbox1.Controls.Add(this.pbMenuImage);
            this.groupbox1.Location = new System.Drawing.Point(49, 129);
            this.groupbox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox1.Name = "groupbox1";
            this.groupbox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupbox1.Size = new System.Drawing.Size(193, 178);
            this.groupbox1.TabIndex = 34;
            this.groupbox1.TabStop = false;
            // 
            // pbMenuImage
            // 
            this.pbMenuImage.Location = new System.Drawing.Point(16, 18);
            this.pbMenuImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbMenuImage.Name = "pbMenuImage";
            this.pbMenuImage.Size = new System.Drawing.Size(160, 148);
            this.pbMenuImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMenuImage.TabIndex = 0;
            this.pbMenuImage.TabStop = false;
            // 
            // tbMenuName
            // 
            this.tbMenuName.Location = new System.Drawing.Point(360, 225);
            this.tbMenuName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMenuName.Name = "tbMenuName";
            this.tbMenuName.Size = new System.Drawing.Size(240, 22);
            this.tbMenuName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(356, 193);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 28);
            this.label2.TabIndex = 35;
            this.label2.Text = "ชื่อเมนู";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(356, 251);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 28);
            this.label3.TabIndex = 36;
            this.label3.Text = "ราคา (บาท)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbMenuPrice
            // 
            this.tbMenuPrice.Location = new System.Drawing.Point(360, 283);
            this.tbMenuPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMenuPrice.Name = "tbMenuPrice";
            this.tbMenuPrice.Size = new System.Drawing.Size(240, 22);
            this.tbMenuPrice.TabIndex = 37;
            this.tbMenuPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMenuPrice_KeyPress);
            // 
            // btSelectMenuImage
            // 
            this.btSelectMenuImage.Location = new System.Drawing.Point(261, 265);
            this.btSelectMenuImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btSelectMenuImage.Name = "btSelectMenuImage";
            this.btSelectMenuImage.Size = new System.Drawing.Size(47, 43);
            this.btSelectMenuImage.TabIndex = 38;
            this.btSelectMenuImage.Text = "...";
            this.btSelectMenuImage.UseVisualStyleBackColor = true;
            this.btSelectMenuImage.Click += new System.EventHandler(this.btSelectMenuImage_Click);
            // 
            // lvShowAllMenu
            // 
            this.lvShowAllMenu.HideSelection = false;
            this.lvShowAllMenu.Location = new System.Drawing.Point(37, 334);
            this.lvShowAllMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvShowAllMenu.Name = "lvShowAllMenu";
            this.lvShowAllMenu.Size = new System.Drawing.Size(563, 267);
            this.lvShowAllMenu.TabIndex = 39;
            this.lvShowAllMenu.UseCompatibleStateImageBehavior = false;
            this.lvShowAllMenu.ItemActivate += new System.EventHandler(this.lvShowAllMenu_ItemActivate);
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = global::CoffeeCafeProject.Properties.Resources.cofffeelogo;
            this.pictureBox12.Location = new System.Drawing.Point(645, 27);
            this.pictureBox12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(73, 68);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 33;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::CoffeeCafeProject.Properties.Resources.cofffeelogo;
            this.pictureBox11.Location = new System.Drawing.Point(37, 27);
            this.pictureBox11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(73, 68);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 32;
            this.pictureBox11.TabStop = false;
            // 
            // btCancel
            // 
            this.btCancel.Image = global::CoffeeCafeProject.Properties.Resources.reset;
            this.btCancel.Location = new System.Drawing.Point(627, 427);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(116, 84);
            this.btCancel.TabIndex = 64;
            this.btCancel.Text = "ยกเลิก";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btClose
            // 
            this.btClose.Image = global::CoffeeCafeProject.Properties.Resources.cancel;
            this.btClose.Location = new System.Drawing.Point(627, 518);
            this.btClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(116, 84);
            this.btClose.TabIndex = 63;
            this.btClose.Text = "ปิด";
            this.btClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btDelete
            // 
            this.btDelete.Image = global::CoffeeCafeProject.Properties.Resources.delete;
            this.btDelete.Location = new System.Drawing.Point(627, 334);
            this.btDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(116, 84);
            this.btDelete.TabIndex = 62;
            this.btDelete.Text = "ลบ";
            this.btDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Image = global::CoffeeCafeProject.Properties.Resources.update;
            this.btUpdate.Location = new System.Drawing.Point(627, 242);
            this.btUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(116, 84);
            this.btUpdate.TabIndex = 61;
            this.btUpdate.Text = "แก้ไข";
            this.btUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // btSave
            // 
            this.btSave.Image = global::CoffeeCafeProject.Properties.Resources.save;
            this.btSave.Location = new System.Drawing.Point(627, 151);
            this.btSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(116, 84);
            this.btSave.TabIndex = 60;
            this.btSave.Text = "บันทึก";
            this.btSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(356, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 28);
            this.label4.TabIndex = 66;
            this.label4.Text = "รหัสเมนู";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbMenuId
            // 
            this.tbMenuId.Location = new System.Drawing.Point(360, 167);
            this.tbMenuId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMenuId.Name = "tbMenuId";
            this.tbMenuId.ReadOnly = true;
            this.tbMenuId.Size = new System.Drawing.Size(240, 22);
            this.tbMenuId.TabIndex = 65;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 633);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMenuId);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.lvShowAllMenu);
            this.Controls.Add(this.btSelectMenuImage);
            this.Controls.Add(this.tbMenuPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMenuName);
            this.Controls.Add(this.groupbox1);
            this.Controls.Add(this.pictureBox12);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu - SAU Coffee Cafe V.1.0";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.groupbox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupbox1;
        private System.Windows.Forms.TextBox tbMenuName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMenuPrice;
        private System.Windows.Forms.Button btSelectMenuImage;
        private System.Windows.Forms.ListView lvShowAllMenu;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMenuId;
        private System.Windows.Forms.PictureBox pbMenuImage;
    }
}