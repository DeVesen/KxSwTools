using KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention;
using KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using EnvDTE80;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.Forms
{
    public partial class GpSpecialDocFrm : Form
    {
        private readonly DTE2 m_envDte;
        private readonly DirectoryInfo m_gpDocDir;


        public GpSpecialDocFrm(DTE2 envDte)
        {
            InitializeComponent();

            this.m_envDte = envDte;

            var _envDteDir = EnvSolutionDev.GetDirectory(this.m_envDte);

            if (_envDteDir != null && _envDteDir.Exists &&
                _envDteDir.Parent != null && _envDteDir.Parent.Exists)
            {
                this.m_gpDocDir = new DirectoryInfo(PathHelper.Combine(EnvSolutionDev.GetDirectory(this.m_envDte).Parent, "GlobalPic.Doc"));
            }
        }

        private void GpSpecialDocFrm_Shown(object sender, EventArgs e)
        {
            if (this.m_gpDocDir != null)
            {
                var _files = this.m_gpDocDir.GetFiles("*.doc*");

                this.m_existingDocLst.BeginUpdate();
                foreach (var _file in _files)
                {
                    // ReSharper disable once RedundantAssignment
                    var _iconForFile = Icon.ExtractAssociatedIcon(_file.FullName);

                    if (!this.m_existingDocImgLst.Images.ContainsKey(_file.Extension))
                    {
                        // If not, add the image to the image list.
                        _iconForFile = Icon.ExtractAssociatedIcon(_file.FullName);
                        if (_iconForFile != null)
                            this.m_existingDocImgLst.Images.Add(_file.Extension, _iconForFile.ToBitmap());
                    }

                    var _imageIndex = this.m_existingDocImgLst.Images.IndexOfKey(_file.Extension);

                    this.m_existingDocLst.Items.Add(new ExistingFileLvi(_file, _imageIndex));
                }
                this.m_existingDocLst.EndUpdate();
            }
        }

        private void OnNewBtnClicked(object sender, EventArgs e)
        {
            if (sender == this.m_newDocGerBtn)
            {
                this.OpenWordDocument(@"\\gss-online.com\mnt\corp\services\templates\office\Project_organisation\Sonderbeschreibung.dotx");
                this.Close();
            }
            else if (sender == this.m_newDocEngBtn)
            {
                this.OpenWordDocument(@"\\gss-online.com\mnt\corp\services\templates\office\Project_organisation\SpecialDescription.dotx");
                this.Close();
            }
        }

        private void m_existingDocLst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.m_existingDocLst.SelectedItems.Count != 1) return;

            if (!(this.m_existingDocLst.SelectedItems[0] is ExistingFileLvi)) return;

            EnvSourceControlDev.CheckOut_Files_Direct(((ExistingFileLvi)this.m_existingDocLst.SelectedItems[0]).FileInfoObj.FullName);

            this.OpenWordDocument(((ExistingFileLvi)this.m_existingDocLst.SelectedItems[0]).FileInfoObj.FullName);

            this.Close();
        }



        private void OpenWordDocument(string wordDocPath)
        {
            object _oMissing = System.Reflection.Missing.Value;
            object _oTemplate = wordDocPath;

            _Application _oWordApp = new Microsoft.Office.Interop.Word.Application();
            _oWordApp.Visible = true;

            _Document _oDoc = _oWordApp.Documents.Add(ref _oTemplate, ref _oMissing, ref _oMissing, ref _oMissing);

            // ReSharper disable once UnusedVariable
            var _fields = _oDoc.FormFields;

            _oWordApp.Visible = true;

            _oDoc.Activate();
            _oWordApp.Activate();
        }
    }

    internal class ExistingFileLvi : ListViewItem
    {
        public FileInfo FileInfoObj { get; private set; }

        public ExistingFileLvi(FileInfo fileInfo, int imageIndex)
        {
            this.Text = fileInfo.Name;
            this.ImageIndex = imageIndex;
            this.FileInfoObj = fileInfo;
        }
    }
}
