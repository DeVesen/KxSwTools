namespace KARDEXSoftwareGmbH.Vs2013ExPackage.Forms
{
    partial class MsgCreateAbortFrm
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
            this.m_cancelBtn = new System.Windows.Forms.Button();
            this.m_createBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_customerNumTb = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_cancelBtn
            // 
            this.m_cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelBtn.Location = new System.Drawing.Point(310, 95);
            this.m_cancelBtn.Name = "m_cancelBtn";
            this.m_cancelBtn.Size = new System.Drawing.Size(79, 47);
            this.m_cancelBtn.TabIndex = 0;
            this.m_cancelBtn.Text = "Abbruch";
            this.m_cancelBtn.UseVisualStyleBackColor = true;
            this.m_cancelBtn.Click += new System.EventHandler(this.m_cancelBtn_Click);
            // 
            // m_createBtn
            // 
            this.m_createBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_createBtn.Location = new System.Drawing.Point(204, 95);
            this.m_createBtn.Name = "m_createBtn";
            this.m_createBtn.Size = new System.Drawing.Size(98, 47);
            this.m_createBtn.TabIndex = 1;
            this.m_createBtn.Text = "Anlegen";
            this.m_createBtn.UseVisualStyleBackColor = true;
            this.m_createBtn.Click += new System.EventHandler(this.m_createBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Das AVERP - Verzeichnis für den Kunden mit der Nummer\r\n";
            // 
            // m_customerNumTb
            // 
            this.m_customerNumTb.BackColor = System.Drawing.Color.Linen;
            this.m_customerNumTb.Font = new System.Drawing.Font("Calibri", 12F);
            this.m_customerNumTb.Location = new System.Drawing.Point(169, 31);
            this.m_customerNumTb.Mask = "00000000";
            this.m_customerNumTb.Name = "m_customerNumTb";
            this.m_customerNumTb.Size = new System.Drawing.Size(72, 27);
            this.m_customerNumTb.TabIndex = 3;
            this.m_customerNumTb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F);
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Soll das Verzeichnis erstellt werden?";
            // 
            // MsgCreateAbortFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(401, 154);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_customerNumTb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_createBtn);
            this.Controls.Add(this.m_cancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgCreateAbortFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_cancelBtn;
        private System.Windows.Forms.Button m_createBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox m_customerNumTb;
        private System.Windows.Forms.Label label2;
    }
}