namespace KARDEXSoftwareGmbH.Vs2013ExPackage.Forms
{
    partial class GpSpecialDocFrm
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
            this.components = new System.ComponentModel.Container();
            this.m_newDocGerBtn = new System.Windows.Forms.Button();
            this.m_newDocEngBtn = new System.Windows.Forms.Button();
            this.m_existingDocLst = new System.Windows.Forms.ListView();
            this.m_existingDocImgLst = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // m_newDocGerBtn
            // 
            this.m_newDocGerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_newDocGerBtn.Location = new System.Drawing.Point(347, 12);
            this.m_newDocGerBtn.Name = "m_newDocGerBtn";
            this.m_newDocGerBtn.Size = new System.Drawing.Size(112, 47);
            this.m_newDocGerBtn.TabIndex = 1;
            this.m_newDocGerBtn.Text = "New DEUTSCH";
            this.m_newDocGerBtn.UseVisualStyleBackColor = true;
            this.m_newDocGerBtn.Click += new System.EventHandler(this.OnNewBtnClicked);
            // 
            // m_newDocEngBtn
            // 
            this.m_newDocEngBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_newDocEngBtn.Location = new System.Drawing.Point(347, 65);
            this.m_newDocEngBtn.Name = "m_newDocEngBtn";
            this.m_newDocEngBtn.Size = new System.Drawing.Size(112, 47);
            this.m_newDocEngBtn.TabIndex = 2;
            this.m_newDocEngBtn.Text = "New ENGLISCH";
            this.m_newDocEngBtn.UseVisualStyleBackColor = true;
            this.m_newDocEngBtn.Click += new System.EventHandler(this.OnNewBtnClicked);
            // 
            // m_existingDocLst
            // 
            this.m_existingDocLst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_existingDocLst.FullRowSelect = true;
            this.m_existingDocLst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_existingDocLst.LargeImageList = this.m_existingDocImgLst;
            this.m_existingDocLst.Location = new System.Drawing.Point(12, 12);
            this.m_existingDocLst.MultiSelect = false;
            this.m_existingDocLst.Name = "m_existingDocLst";
            this.m_existingDocLst.Size = new System.Drawing.Size(329, 281);
            this.m_existingDocLst.SmallImageList = this.m_existingDocImgLst;
            this.m_existingDocLst.TabIndex = 3;
            this.m_existingDocLst.UseCompatibleStateImageBehavior = false;
            this.m_existingDocLst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_existingDocLst_MouseDoubleClick);
            // 
            // m_existingDocImgLst
            // 
            this.m_existingDocImgLst.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.m_existingDocImgLst.ImageSize = new System.Drawing.Size(64, 64);
            this.m_existingDocImgLst.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // GpSpecialDocFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(471, 305);
            this.Controls.Add(this.m_existingDocLst);
            this.Controls.Add(this.m_newDocEngBtn);
            this.Controls.Add(this.m_newDocGerBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GpSpecialDocFrm";
            this.Text = "GP Special Documentation";
            this.Shown += new System.EventHandler(this.GpSpecialDocFrm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_newDocGerBtn;
        private System.Windows.Forms.Button m_newDocEngBtn;
        private System.Windows.Forms.ListView m_existingDocLst;
        private System.Windows.Forms.ImageList m_existingDocImgLst;
    }
}