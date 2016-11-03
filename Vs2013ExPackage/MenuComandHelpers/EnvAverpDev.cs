using KARDEXSoftwareGmbH.Vs2013ExPackage.Forms;
using KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention;
using Microsoft.VisualStudio.Shell;
using System.IO;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public class EnvAverpDev
    {
        private EnvAverpDev()
        {

        }


        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            //_result = _result && EnvSolutionDev.IsValid(envDte);
            //_result = _result && EnvSolutionDev.Exist(envDte);
            //_result = _result && EnvSolutionDev.GetFile(envDte).Exists;

            return true;
        }


        public static void CustomerDataDirectory_Open(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            var _averpCustDirBasePath = EnvAverpDev.CustomerDataDirectory_CheckAndCreate(EnvSolutionDev.GetName(envDte));
            if (!string.IsNullOrEmpty(_averpCustDirBasePath))
            {
                EnvOperatingSystem.StartProcess(_averpCustDirBasePath);
            }
        }

        public static void BuildGpSp(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (!EnvSolutionDev.IsValid(envDte) || !EnvGpDev.IsValid(envDte))
                return;

            //Verzeichnisse sammeln (001) ...
            var _solutionDirectory = EnvSolutionDev.GetDirectory(envDte);
            var _sourceDirectory = _solutionDirectory.Parent;
            var _teamprojectDirectory = _sourceDirectory.Parent;

            //Verzeichnisse sammeln (002) ...
            var _setupInnoDirectory = new DirectoryInfo(PathHelper.Combine(_sourceDirectory, "GlobalPic.Setup\\Inno"));
            var _docuDirectory = new DirectoryInfo(PathHelper.Combine(_sourceDirectory, "GlobalPic.Doc"));
            var _teamBuildDirectory = new DirectoryInfo(PathHelper.Combine(_teamprojectDirectory, "TeamBuildTypes\\GP Local Build"));

            //Dateien sammeln...
            FileInfo _localBuildCmd = new FileInfo(PathHelper.Combine(_teamBuildDirectory, "local_build.cmd"));

            //Verzeichniss prüfen
            if (!_setupInnoDirectory.Exists || !_teamBuildDirectory.Exists || !_localBuildCmd.Exists || !_docuDirectory.Exists)
                return;


            //Kundenverzeichnis errechnen...
            var _averpCustDirBasePath = EnvAverpDev.CustomerDataDirectory_CheckAndCreate(EnvSolutionDev.GetName(envDte));
            if (_averpCustDirBasePath == null || !Directory.Exists(_averpCustDirBasePath))
                return;


            //Solution schließen...
            EnvSolutionDev.Close(true, envDte);
            EnvStudio.DoEnvents(10, envDte);


            //Local-Build ausführen...
            EnvOperatingSystem.ExecuteCommand(_localBuildCmd, _localBuildCmd.Directory, true, false, false, false, false);


            //AVERP Kundenverzeichnis öffnen...
            EnvOperatingSystem.StartProcess(_averpCustDirBasePath);
            EnvStudio.DoEnvents(10, envDte);

            //GP DOKU Verzeichnis...
            EnvOperatingSystem.StartProcess(_docuDirectory.FullName);
            EnvStudio.DoEnvents(10, envDte);

            //GP SETUP Verzeichnis...
            EnvOperatingSystem.StartProcess(_setupInnoDirectory.FullName);
            EnvStudio.DoEnvents(10, envDte);
        }



        public static string CustomerDataDirectory_CheckAndCreate(string solutionName)
        {
            var _averpCustDirBasePath = @"\\gss-online.com\mnt\corp\services\averp\data";

            if (string.IsNullOrEmpty(solutionName) || string.IsNullOrEmpty(solutionName.Trim()))
                return _averpCustDirBasePath;

            var _soluionNameParts = solutionName.Trim().Split('_');

            if (_soluionNameParts.Length <= 0) return _averpCustDirBasePath;

            for (var _i = 2; _i < _soluionNameParts.Length; _i++)
            {
                int _customerNumber;
                if (!int.TryParse(_soluionNameParts[_i], out _customerNumber) || _customerNumber <= 0) continue;

                _averpCustDirBasePath = PathHelper.Combine(_averpCustDirBasePath, _customerNumber.ToString());

                if (!Directory.Exists(_averpCustDirBasePath))
                {
                    var _frm = new MsgCreateAbortFrm(_customerNumber);

                    switch (_frm.ShowDialog())
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            {
                                _averpCustDirBasePath = PathHelper.Combine(_averpCustDirBasePath, _frm.CustomerNumber.ToString());
                                if (!Directory.Exists(_averpCustDirBasePath))
                                    Directory.CreateDirectory(_averpCustDirBasePath);
                            }
                            break;
                        case System.Windows.Forms.DialogResult.Abort:
                            {

                            }
                            return null;
                    }
                }

                _averpCustDirBasePath = PathHelper.Combine(_averpCustDirBasePath, "customerdata");
                if (!Directory.Exists(_averpCustDirBasePath))
                    Directory.CreateDirectory(_averpCustDirBasePath);

                break;
            }

            return _averpCustDirBasePath;
        }
    }
}
