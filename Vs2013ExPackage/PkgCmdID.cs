// PkgCmdID.cs
// MUST match PkgCmdID.h

namespace KARDEXSoftwareGmbH.Vs2013ExPackage
{
    internal static class PkgCmdIdList
    {
        //Gruppe - Quellcode Kommentare
        //Gruppe - Quellcode Kommentare
        //Gruppe - Quellcode Kommentare
        public const uint cmSourceCodeComment_Line = 0x1101;
        public const uint cmSourceCodeComment_Start = 0x1102;
        public const uint cmSourceCodeComment_Block = 0x1103;
        public const uint cmSourceCodeComment_End = 0x1104;


        //Gruppe - GP Start UP
        //Gruppe - GP Start UP
        //Gruppe - GP Start UP
        public const uint cmPreparePPG = 0x1301;
        public const uint cmPrepareFPG = 0x1302;

        public const uint cmStartInst_ConfigWizard = 0x1351;
        public const uint cmStartInst_RuleEngine = 0x1352;
        public const uint cmStartInst_MachineControlService = 0x1353;
        public const uint cmStartInst_CrossEnterpriseUnit = 0x1354;
        public const uint cmStartInst_UIWin = 0x1355;


        //Menü - GP Special Docu
        //Menü - GP Special Docu
        //Menü - GP Special Docu
        public const uint cmGPSpecialDescDocu = 0x1401;


        //Menü - GP Special Actions
        //Menü - GP Special Actions
        //Menü - GP Special Actions
        public const uint cmGPSpecialActions_GPUpdateWcf = 0x1501;
        public const uint cmGPSpecialActions_GPUpdateDB = 0x1502;


        //Menü - GP Database Actions - 01
        //Menü - GP Database Actions - 01
        //Menü - GP Database Actions - 01
        public const uint cmGPDatabaseActions_ClearDB = 0x1601;
        //Menü - GP Database Actions - 02
        //Menü - GP Database Actions - 02
        //Menü - GP Database Actions - 02
        public const uint cmGPDatabaseActions_LoadBackup = 0x1701;
        public const uint cmGPDatabaseActions_SaveBackup = 0x1702;
        //Menü - GP Database Actions - 03
        //Menü - GP Database Actions - 03
        //Menü - GP Database Actions - 03
        public const uint GPDatabaseActions_TaskResetStatus = 0x1801;


        //Menü - GP Deploy Actions - 01
        //Menü - GP Deploy Actions - 01
        //Menü - GP Deploy Actions - 01
        public const uint cmGPDeployActions_AverpCustDir = 0x1901;
        public const uint cmGPDeployActions_DeployGP = 0x1902;






        public static bool IdIsKnown(int? id)
        {
            bool _result = false;

            _result = _result || id == PkgCmdIdList.cmSourceCodeComment_Line;
            _result = _result || id == PkgCmdIdList.cmSourceCodeComment_Start;
            _result = _result || id == PkgCmdIdList.cmSourceCodeComment_Block;
            _result = _result || id == PkgCmdIdList.cmSourceCodeComment_End;

            _result = _result || id == PkgCmdIdList.cmPreparePPG;
            _result = _result || id == PkgCmdIdList.cmPrepareFPG;

            _result = _result || id == PkgCmdIdList.cmStartInst_ConfigWizard;
            _result = _result || id == PkgCmdIdList.cmStartInst_RuleEngine;
            _result = _result || id == PkgCmdIdList.cmStartInst_MachineControlService;
            _result = _result || id == PkgCmdIdList.cmStartInst_CrossEnterpriseUnit;
            _result = _result || id == PkgCmdIdList.cmStartInst_UIWin;

            _result = _result || id == PkgCmdIdList.cmGPSpecialDescDocu;

            _result = _result || id == PkgCmdIdList.cmGPSpecialActions_GPUpdateWcf;
            _result = _result || id == PkgCmdIdList.cmGPSpecialActions_GPUpdateDB;

            _result = _result || id == PkgCmdIdList.cmGPDatabaseActions_ClearDB;

            _result = _result || id == PkgCmdIdList.cmGPDatabaseActions_LoadBackup;
            _result = _result || id == PkgCmdIdList.cmGPDatabaseActions_SaveBackup;

            _result = _result || id == PkgCmdIdList.GPDatabaseActions_TaskResetStatus;

            _result = _result || id == PkgCmdIdList.cmGPDeployActions_AverpCustDir;
            _result = _result || id == PkgCmdIdList.cmGPDeployActions_DeployGP;

            return _result;
        }
        public static string Id2String(int? id)
        {
            if (id == PkgCmdIdList.cmSourceCodeComment_Line)
            {
                return "Comment Line";
            }
            else if (id == PkgCmdIdList.cmSourceCodeComment_Start)
            {
                return "Comment Start";
            }
            else if (id == PkgCmdIdList.cmSourceCodeComment_Block)
            {
                return "Comment Block";
            }
            else if (id == PkgCmdIdList.cmSourceCodeComment_End)
            {
                return "Comment Ende";
            }

            else if (id == PkgCmdIdList.cmPreparePPG)
            {
                return "Prepare PPG";
            }
            else if (id == PkgCmdIdList.cmPrepareFPG)
            {
                return "Prepare FPG";
            }

            else if (id == PkgCmdIdList.cmGPSpecialActions_GPUpdateWcf)
            {
                return "GP Update Wcf";
            }
            else if (id == PkgCmdIdList.cmGPSpecialActions_GPUpdateDB)
            {
                return "GP Update DB";
            }

            else if (id == PkgCmdIdList.cmGPDatabaseActions_ClearDB)
            {
                return "GP Clear DB";
            }
            else if (id == PkgCmdIdList.cmGPDatabaseActions_LoadBackup)
            {
                return "GP Load Backup";
            }
            else if (id == PkgCmdIdList.cmGPDatabaseActions_SaveBackup)
            {
                return "GP Save Backup";
            }
            else if (id == PkgCmdIdList.GPDatabaseActions_TaskResetStatus)
            {
                return "GP Task Reset Status";
            }

            else if (id == PkgCmdIdList.cmGPDeployActions_AverpCustDir)
            {
                return "GP Averp Customer Directory";
            }
            else if (id == PkgCmdIdList.cmGPDeployActions_DeployGP)
            {
                return "GP Averp Deploy GP";
            }

            return "<N/A>";
        }
    };
}