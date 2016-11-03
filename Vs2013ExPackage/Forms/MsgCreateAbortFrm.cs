using System;
using System.Windows.Forms;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.Forms
{
    public partial class MsgCreateAbortFrm : Form
    {
        public int CustomerNumber { get; private set; }

        public MsgCreateAbortFrm(int customerNumber)
        {
            InitializeComponent();

            this.m_customerNumTb.Text = customerNumber.ToString();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.m_createBtn.Text = string.Empty;
            this.m_createBtn.Image = Resources.OKImg;

            this.m_cancelBtn.Text = string.Empty;
            this.m_cancelBtn.Image = Resources.AbortImg;
        }


        private void m_createBtn_Click(object sender, EventArgs e)
        {
            int _customerNumber;

            if (!int.TryParse(this.m_customerNumTb.Text, out _customerNumber)) return;

            this.CustomerNumber = _customerNumber;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void m_cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}
