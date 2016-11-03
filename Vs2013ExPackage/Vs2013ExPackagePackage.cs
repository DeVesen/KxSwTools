using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers;
using KARDEXSoftwareGmbH.Vs2013ExPackage.Forms;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVs2013ExPackagePkgString)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [ProvideAutoLoad(UIContextGuids80.EmptySolution)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [ProvideAutoLoad(UIContextGuids80.SolutionBuilding)]
    [ProvideAutoLoad(UIContextGuids80.Debugging)]
    [ProvideAutoLoad(UIContextGuids80.CodeWindow)]
    public sealed class Vs2013ExPackagePackage : Package
    {
        private OleMenuCommandServiceWrapper m_serviceWrapper;


        public Vs2013ExPackagePackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }


        protected override void Initialize()
        {
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService _mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (_mcs == null)
                return;
            this.m_serviceWrapper = new OleMenuCommandServiceWrapper(_mcs);


            #region ----------  Bunny Commands  ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_SourceComments,
                                     (int)PkgCmdIdList.cmSourceCodeComment_Line,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_SourceComments,
                                     (int)PkgCmdIdList.cmSourceCodeComment_Start,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_SourceComments,
                                     (int)PkgCmdIdList.cmSourceCodeComment_Block,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_SourceComments,
                                     (int)PkgCmdIdList.cmSourceCodeComment_End,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  Bunny Commands  ----------


            #region ----------  GP-Start Up  ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartUP,
                                     (int)PkgCmdIdList.cmPreparePPG,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartUP,
                                     (int)PkgCmdIdList.cmPrepareFPG,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  GP-Start Up  ----------


            #region ----------  GP-Start Inst  ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartProjInst,
                                     (int)PkgCmdIdList.cmStartInst_ConfigWizard,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartProjInst,
                                     (int)PkgCmdIdList.cmStartInst_RuleEngine,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartProjInst,
                                     (int)PkgCmdIdList.cmStartInst_MachineControlService,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartProjInst,
                                     (int)PkgCmdIdList.cmStartInst_CrossEnterpriseUnit,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPStartProjInst,
                                     (int)PkgCmdIdList.cmStartInst_UIWin,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  GP-Start Inst  ----------


            #region ----------  GP-Spezial Docu  ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPSpecialDocu,
                                     (int)PkgCmdIdList.cmGPSpecialDescDocu,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  GP-Spezial Docu  ----------


            #region ----------  GP Utils ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPSpecialActions,
                                     (int)PkgCmdIdList.cmGPSpecialActions_GPUpdateWcf,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPSpecialActions,
                                     (int)PkgCmdIdList.cmGPSpecialActions_GPUpdateDB,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  GP Utils  ----------


            #region ----------  GP DB Utils ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPDatabaseActions01,
                                     (int)PkgCmdIdList.cmGPDatabaseActions_ClearDB,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPDatabaseActions02,
                                     (int)PkgCmdIdList.cmGPDatabaseActions_LoadBackup,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );
            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPDatabaseActions02,
                                     (int)PkgCmdIdList.cmGPDatabaseActions_SaveBackup,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );
            
            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPDatabaseActions03,
                                     (int)PkgCmdIdList.GPDatabaseActions_TaskResetStatus,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  GP DB Utils  ----------


            #region ----------  GP Deploy Utils ----------

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPDeployActions01,
                                     (int)PkgCmdIdList.cmGPDeployActions_AverpCustDir,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            this.m_serviceWrapper.AddOleMenuCommand
                                 (
                                     GuidList.guidGroup_GPDeployActions01,
                                     (int)PkgCmdIdList.cmGPDeployActions_DeployGP,
                                     this.MenuItemCallback,
                                     this.OnBeforeQueryStatus
                                 );

            #endregion ----------  GP Deploy Utils  ----------
        }


        private void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            var _envDte = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
            var _menuCommand = sender as OleMenuCommand;
            var _isEnables = true;

            // ReSharper disable once PossibleNullReferenceException
            switch (_menuCommand.CommandID.Guid.ToString().Replace("{", string.Empty).Replace("}", string.Empty).ToUpper())
            {
                case GuidList.guidGroup_SourceCommentsString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmSourceCodeComment_Line:
                            case (int)PkgCmdIdList.cmSourceCodeComment_Start:
                            case (int)PkgCmdIdList.cmSourceCodeComment_Block:
                            case (int)PkgCmdIdList.cmSourceCodeComment_End:
                                {
                                    _isEnables = EnvCommentDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPStartUPString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmPreparePPG:
                            case (int)PkgCmdIdList.cmPrepareFPG:
                                {
                                    // ReSharper disable once PossibleNullReferenceException
                                    _isEnables = _envDte.Mode != EnvDTE.vsIDEMode.vsIDEModeDebug;
                                    _isEnables = _isEnables && EnvGpDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPStartProjInstString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmStartInst_ConfigWizard:
                            case (int)PkgCmdIdList.cmStartInst_RuleEngine:
                            case (int)PkgCmdIdList.cmStartInst_MachineControlService:
                            case (int)PkgCmdIdList.cmStartInst_CrossEnterpriseUnit:
                            case (int)PkgCmdIdList.cmStartInst_UIWin:
                                {
                                    _isEnables = EnvGpDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPSpecialDocuString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPSpecialDescDocu:
                                {
                                    _isEnables = EnvGpDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPSpecialActionsString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPSpecialActions_GPUpdateWcf:
                            case (int)PkgCmdIdList.cmGPSpecialActions_GPUpdateDB:
                                {
                                    // ReSharper disable once PossibleNullReferenceException
                                    _isEnables = _envDte.Mode != EnvDTE.vsIDEMode.vsIDEModeDebug;
                                    _isEnables = _isEnables && EnvGpDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPDatabaseActions01String:
                case GuidList.guidGroup_GPDatabaseActions02String:
                case GuidList.guidGroup_GPDatabaseActions03String:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPDatabaseActions_ClearDB:
                            case (int)PkgCmdIdList.cmGPDatabaseActions_LoadBackup:
                            case (int)PkgCmdIdList.cmGPDatabaseActions_SaveBackup:
                            case (int)PkgCmdIdList.GPDatabaseActions_TaskResetStatus:
                                {
                                    _isEnables = EnvGpDev.IsValid(_envDte);
                                    _isEnables = _isEnables && EnvGpDbBaseDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPDeployActions01String:
                    #region #####     :::::     #####
                    {
                        switch (_menuCommand.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPDeployActions_AverpCustDir:
                                {
                                }
                                break;
                            case (int)PkgCmdIdList.cmGPDeployActions_DeployGP:
                                {
                                    // ReSharper disable once PossibleNullReferenceException
                                    _isEnables = _envDte.Mode != EnvDTE.vsIDEMode.vsIDEModeDebug;
                                    _isEnables = _isEnables && EnvGpDev.IsValid(_envDte);
                                }
                                break;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;
            }

            _menuCommand.Enabled = _isEnables;
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            //Cast
            var _menuCmd = sender as OleMenuCommand;
            if (_menuCmd == null) return;

            //Ist item ID bekannt?
            if (!GuidList.GuidIsKnown(_menuCmd.CommandID.Guid) || !PkgCmdIdList.IdIsKnown(_menuCmd.CommandID.ID))
            {
                string _text = string.Format("ID '{0}' of command '{1}' not known!", _menuCmd.CommandID.ID, _menuCmd.CommandID.Guid);
                this.ShowMsg(_text);
                return;
            }


            //Ausführen...
            switch (_menuCmd.CommandID.Guid.ToString().Replace("{", string.Empty).Replace("}", string.Empty).ToUpper())
            {
                case GuidList.guidGroup_SourceCommentsString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmSourceCodeComment_Line:
                                {
                                    EnvCommentDev.SetLine();
                                }
                                return;
                            case (int)PkgCmdIdList.cmSourceCodeComment_Start:
                                {
                                    EnvCommentDev.SetBlockStart();
                                }
                                return;
                            case (int)PkgCmdIdList.cmSourceCodeComment_Block:
                                {
                                    EnvCommentDev.SetBlock();
                                }
                                return;
                            case (int)PkgCmdIdList.cmSourceCodeComment_End:
                                {
                                    EnvCommentDev.SetBlockEnde();
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPStartUPString:
                    #region #####     :::::     #####
                    {
                        ////Kontrolle:
                        //if (!EnvSolutionDev.IsActiveConfiguration("Debug"))
                        //{
                        //    this.ShowMsg("Bitte erst auf \"DEBUG\" Modus stellen!", "Vs2013ExPackage", OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST, OLEMSGICON.OLEMSGICON_CRITICAL);
                        //    return;
                        //}

                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmPreparePPG:
                                {
                                    EnvGpDev.Prepare_Platform("PP");
                                }
                                return;
                            case (int)PkgCmdIdList.cmPrepareFPG:
                                {
                                    EnvGpDev.Prepare_Platform("FP");
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPStartProjInstString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmStartInst_ConfigWizard:
                                {
                                    EnvGpDev.StartNewInstance(EnvGpDev.GpSolutionElements.ConfigWizard);
                                }
                                return;
                            case (int)PkgCmdIdList.cmStartInst_RuleEngine:
                                {
                                    EnvGpDev.StartNewInstance(EnvGpDev.GpSolutionElements.RuleEnginem);
                                }
                                return;
                            case (int)PkgCmdIdList.cmStartInst_MachineControlService:
                                {
                                    EnvGpDev.StartNewInstance(EnvGpDev.GpSolutionElements.MachineControlService);
                                }
                                return;
                            case (int)PkgCmdIdList.cmStartInst_CrossEnterpriseUnit:
                                {
                                    EnvGpDev.StartNewInstance(EnvGpDev.GpSolutionElements.CrossEnterpriseUnit);
                                }
                                return;
                            case (int)PkgCmdIdList.cmStartInst_UIWin:
                                {
                                    EnvGpDev.StartNewInstance(EnvGpDev.GpSolutionElements.UiWin);
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPSpecialDocuString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPSpecialDescDocu:
                                {
                                    GpSpecialDocFrm _wnd = new GpSpecialDocFrm(Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2);

                                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(_wnd.Handle.ToInt32());
                                    _wnd.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                    _wnd.ShowDialog();

                                    _wnd.Dispose();
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPSpecialActionsString:
                    #region #####     :::::     #####
                    {
                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPSpecialActions_GPUpdateWcf:
                                {
                                    EnvGpDev.Prepare_WcfCodeGen();
                                }
                                return;
                            case (int)PkgCmdIdList.cmGPSpecialActions_GPUpdateDB:
                                {
                                    EnvGpDev.Prepare_UpdateDb();
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPDatabaseActions01String:
                case GuidList.guidGroup_GPDatabaseActions02String:
                case GuidList.guidGroup_GPDatabaseActions03String:
                    #region #####     :::::     #####
                    {
                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPDatabaseActions_ClearDB:
                                {
                                    (new EnvGpDbSqlDev()).ClearAllTables();

                                    ShowMsg("Tables Cleared ...", "Vs2013ExPackage: Clear Tables");
                                }
                                return;
                            case (int)PkgCmdIdList.cmGPDatabaseActions_LoadBackup:
                            {
                                    var _openFileDlg =
                                        new System.Windows.Forms.OpenFileDialog
                                        {
                                            Filter = @"XML files (*.XML)|*.XML|All files (*.*)|*.*",
                                            FilterIndex = 1,
                                            InitialDirectory = EnvSolutionDev.GetDirectory().Parent.Parent.FullName,
                                            RestoreDirectory = true
                                        };

                                    if (_openFileDlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                                    new EnvGpDbSqlDev().BackUp_Restore(_openFileDlg.FileName);

                                    ShowMsg("Backup restored ...", "Vs2013ExPackage: Backup");
                                }
                                return;
                            case (int)PkgCmdIdList.cmGPDatabaseActions_SaveBackup:
                                {
                                    var _saveFileDlg =
                                        new System.Windows.Forms.SaveFileDialog
                                        {
                                            Filter = @"XML files (*.XML)|*.XML|All files (*.*)|*.*",
                                            FilterIndex = 1,
                                            InitialDirectory = EnvSolutionDev.GetDirectory().Parent.Parent.FullName,
                                            RestoreDirectory = true,
                                            FileName =
                                                EnvSolutionDev.GetName() + "_" +
                                                DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                                            DefaultExt = ".xml"
                                        };

                                    if (_saveFileDlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                                    new EnvGpDbSqlDev().BackUp_Create(_saveFileDlg.FileName);

                                    ShowMsg("Backup created ...", "Vs2013ExPackage: Backup");
                                }
                                return;

                            case (int)PkgCmdIdList.GPDatabaseActions_TaskResetStatus:
                                {
                                    //EnvDTE.SolutionConfigurations solutionConfigurations = _envDTE.Solution.SolutionBuild.SolutionConfigurations;

                                    //switch (_envDTE.Debugger.CurrentMode)
                                    //{
                                    //    case EnvDTE.dbgDebugMode.dbgDesignMode:
                                    //        break;
                                    //    case EnvDTE.dbgDebugMode.dbgBreakMode:
                                    //        break;
                                    //    case EnvDTE.dbgDebugMode.dbgRunMode:
                                    //        break;
                                    //}


                                    string _sqlCommand = string.Empty;

                                    _sqlCommand += "UPDATE Task \r\n";
                                    _sqlCommand += "SET Task.StatusType = 10 \r\n";
                                    _sqlCommand += "WHERE Task.StatusType = 20 \r\n";

                                    (new EnvGpDbSqlDev()).Execute(_sqlCommand);

                                    ShowMsg("Command committed ...", "Vs2013ExPackage: Backup");
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;


                case GuidList.guidGroup_GPDeployActions01String:
                    #region #####     :::::     #####
                    {
                        switch (_menuCmd.CommandID.ID)
                        {
                            case (int)PkgCmdIdList.cmGPDeployActions_AverpCustDir:
                                {
                                    EnvAverpDev.CustomerDataDirectory_Open();
                                }
                                return;
                            case (int)PkgCmdIdList.cmGPDeployActions_DeployGP:
                                {
                                    EnvAverpDev.BuildGpSp();
                                }
                                return;
                        }
                    }
                    #endregion #####     :::::     #####
                    break;
            }


            //Not implementet...
            this.ShowMsg(string.Format("'{0}' of '{1}' not implemented!", PkgCmdIdList.Id2String(_menuCmd.CommandID.ID), GuidList.Guid2String(_menuCmd.CommandID.Guid)));
        }


        private void ShowMsg(string pszText, string pszTitle = "Vs2013ExPackage", OLEMSGBUTTON buttons = OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON selButton = OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST, OLEMSGICON image = OLEMSGICON.OLEMSGICON_INFO)
        {
            // Show a Message Box to prove we were here
            var _uiShell = (IVsUIShell)this.GetService(typeof(SVsUIShell));
            var _clsid = Guid.Empty;
            int _result;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(_uiShell.ShowMessageBox(
                       0,
                       ref _clsid,
                       pszTitle,
                       pszText,
                       string.Empty,
                       0,
                       buttons,
                       selButton,
                       image,
                       0,        // false
                       out _result));
        }
    }




    #region ##########  Helpers  ##########

    public class OleMenuCommandServiceWrapper
    {
        private List<MenuCommand> m_menuCommandLst = new List<MenuCommand>();



        public OleMenuCommandService OleMenuCommandService { get; private set; }

        public MenuCommand[] MenuCommands
        {
            get
            {
                return this.m_menuCommandLst.ToArray();
            }
        }


        public MenuCommand this[int commandId]
        {
            get
            {
                return this.m_menuCommandLst.Find(p => p.CommandID.ID == commandId);
            }
        }
        public MenuCommand this[Guid commandId]
        {
            get
            {
                return this.m_menuCommandLst.Find(p => p.CommandID.Guid == commandId);
            }
        }




        public OleMenuCommandServiceWrapper(OleMenuCommandService mcs)
        {
            this.OleMenuCommandService = mcs;
        }




        public MenuCommand AddMenuCommand(Guid menuGroup, int commandId, EventHandler invokeHandler)
        {
            var _menuCommandId = new CommandID(menuGroup, commandId);
            var _menuItem = new MenuCommand(invokeHandler, _menuCommandId);

            this.m_menuCommandLst.Add(_menuItem);
            this.OleMenuCommandService.AddCommand(_menuItem);

            return _menuItem;
        }
        public OleMenuCommand AddOleMenuCommand(Guid menuGroup, int commandId, EventHandler invokeHandler, EventHandler beforeQueryStatus)
        {
            var _menuCommandId = new CommandID(menuGroup, commandId);
            var _menuItem = new OleMenuCommand(invokeHandler, _menuCommandId);

            if (beforeQueryStatus != null)
                _menuItem.BeforeQueryStatus += beforeQueryStatus;

            this.m_menuCommandLst.Add(_menuItem);
            this.OleMenuCommandService.AddCommand(_menuItem);

            return _menuItem;
        }

        public void RemoveCommand(int commandId)
        {
            this.RemoveCommand(this[commandId]);
        }
        public void RemoveCommand(Guid commandId)
        {
            this.RemoveCommand(this[commandId]);
        }
        public void RemoveCommand(MenuCommand command)
        {
            if (command != null)
            {
                this.m_menuCommandLst.Remove(command);
                this.OleMenuCommandService.RemoveCommand(command);
            }
        }

        public void Clear()
        {
            foreach (var _cmdItem in this.m_menuCommandLst)
            {
                this.OleMenuCommandService.RemoveCommand(_cmdItem);
            }
            this.m_menuCommandLst.Clear();
        }
    }

    #endregion ##########  Helpers  ##########
}
