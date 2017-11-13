using EnvDTE;
using KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public class EnvGpDev
    {
        public enum GpSolutionElements
        {
            RuleEnginem,
            MachineControlService,
            CrossEnterpriseUnit,
            UiWin,

            WcfCodeGen,
            ConfigWizard
        }

        private EnvGpDev()
        {

        }



        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            var _result = EnvSolutionDev.IsValid(envDte);
            _result = _result && EnvSolutionDev.Exist(envDte);
            _result = _result && EnvSolutionDev.GetFile(envDte).Exists;
            _result = _result && EnvSolutionDev.GetFile(envDte).Directory.FullName.ToLower().EndsWith("Source\\GlobalPic".ToLower())
                              || EnvSolutionDev.GetFile(envDte).Directory.FullName.ToLower().EndsWith("GlobalPic".ToLower());

            _result = _result && EnvGpDev.Node_GetItem(GpSolutionElements.RuleEnginem, envDte) != null;
            _result = _result && EnvGpDev.Node_GetItem(GpSolutionElements.MachineControlService, envDte) != null;
            _result = _result && EnvGpDev.Node_GetItem(GpSolutionElements.CrossEnterpriseUnit, envDte) != null;
            _result = _result && EnvGpDev.Node_GetItem(GpSolutionElements.UiWin, envDte) != null;

            return _result;
        }
        public static string GetPathOf(GpSolutionElements element)
        {
            switch (element)
            {
                case GpSolutionElements.RuleEnginem:
                    return "GP.RuleEngine\\GP.RuleEngine";
                case GpSolutionElements.MachineControlService:
                    return "GP.MachineControlService\\GP.MachineControlService";
                case GpSolutionElements.CrossEnterpriseUnit:
                    return "GP.CrossEnterpriseUnit\\GP.CrossEnterpriseUnit";
                case GpSolutionElements.UiWin:
                    return "GP.UI.Win\\GP.UI.Win";
                case GpSolutionElements.WcfCodeGen:
                    return "_Utilities\\GP.StandardWCFCodeGenerator";
                case GpSolutionElements.ConfigWizard:
                    return "_Utilities\\GP.SetupHelper";
            }
            return null;
        }
        public static string GetPrjPathOf(GpSolutionElements element)
        {
            switch (element)
            {
                case GpSolutionElements.RuleEnginem:
                    return "GP.RuleEngine\\GP.RuleEngine.csproj";
                case GpSolutionElements.MachineControlService:
                    return "GP.MachineControlService\\GP.MachineControlService.csproj";
                case GpSolutionElements.CrossEnterpriseUnit:
                    return "GP.CrossEnterpriseUnit\\GP.CrossEnterpriseUnit.csproj";
                case GpSolutionElements.UiWin:
                    return "GP.UI.Win\\GP.UI.Win.csproj";
                case GpSolutionElements.WcfCodeGen:
                    return "_Utilities\\GP.StandardWCFCodeGenerator.csproj";
            }
            return null;
        }
        public static string GetBinPathOf(GpSolutionElements element)
        {
            switch (element)
            {
                case GpSolutionElements.RuleEnginem:
                    return "bin\\GP.RuleEngine.exe";
                case GpSolutionElements.MachineControlService:
                    return "bin\\GP.MachineControlService.exe";
                case GpSolutionElements.CrossEnterpriseUnit:
                    return "bin\\GP.CrossEnterpriseUnit.exe";
                case GpSolutionElements.UiWin:
                    return "bin\\GP.UI.Win.exe";
                case GpSolutionElements.WcfCodeGen:
                    return "bin\\GP.StandardWCFCodeGenerator.exe";
            }
            return null;
        }



        public static void Prepare_Platform(string platformName, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;


            if (!EnvGpDev.IsValid(envDte))
                return;


            EnvStudio.StatusBar_SetText("Prepare-Platform: Alle Knoten schließen (1) ...", envDte);
            {
                EnvSolutionExplorerDev.Node_ExpandedAll(false, true);
            }


            EnvStudio.StatusBar_SetText("Prepare-Platform: CommandLine setzten ...", envDte);
            {
                EnvSolutionExplorerDev.CommandArguments_Set("_Utilities\\GP.SetupHelper", "/startconfigwizard", envDte);
                EnvSolutionExplorerDev.CommandArguments_Set("GP.RuleEngine\\GP.RuleEngine", "/console", envDte);
                EnvSolutionExplorerDev.CommandArguments_Set("GP.MachineControlService\\GP.MachineControlService", "/console", envDte);
                EnvSolutionExplorerDev.CommandArguments_Set("GP.CrossEnterpriseUnit\\GP.CrossEnterpriseUnit", "/console", envDte);
            }


            EnvStudio.StatusBar_SetText("Prepare-Platform: StartUp Projekte definieren ...", envDte);
            {
                EnvGpDev.SetStartupProjects_UiWin(envDte);
            }


            //EnvStudio.StatusBar_SetText("Prepare-Platform: Solution Kompilieren ...", envDTE);
            //{
            //    EnvSolutionDev.Rebuild(true, envDTE);
            //}


            EnvStudio.StatusBar_SetText("Prepare-Platform: LIB´s kopieren ...", envDte);
            {
                GpLibManager.Prepare(envDte);
            }


            EnvStudio.StatusBar_SetText("Prepare-Platform: Platform kopieren ...", envDte);
            {
                GpPlatformManager.Prepare(platformName, envDte);
            }


            EnvStudio.StatusBar_SetText("Prepare-Platform: Alle Knoten schließen (2) ...", envDte);
            {
                EnvSolutionExplorerDev.Node_ExpandedAll(false, true);
            }


            EnvStudio.StatusBar_SetText("Prepare-Platform: Config-Wizard öffnen ...", envDte);
            {
                EnvOperatingSystem.Clipboard_Set(EnvSolutionDev.GetName(envDte));
                EnvSolutionExplorerDev.Node_SetFocus("_Utilities\\GP.SetupHelper");
                //EnvSolutionExplorerDev.Node_StartNewInstance("_Utilities\\GP.SetupHelper", _envDTE);
            }


            EnvStudio.StatusBar_Clear(envDte);
        }

        public static void Prepare_WcfCodeGen(EnvDTE80.DTE2 envDte = null)
        {
            string[] _files = new string[]
                {
                    @"GP.RuleEngine.Proxy\ServiceModel\GPRuleEngineProxyServices.cs",
                    @"GP.RuleEngine\ServiceModel\GPRuleEngineServices.cs",
                    @"GP.RuleEngine\ServiceModel\GPRuleEngineServiceContracts.cs"
                };

            //Auschecken
            EnvSourceControlDev.CheckOut_Files(_files);

            //Verzeichnis setzten
            if (EnvSolutionDev.GetDirectory() != null)
                Environment.CurrentDirectory = EnvSolutionDev.GetDirectory().FullName;

            //Solution in Zwischenablage
            if (EnvSolutionDev.GetFile() != null)
                EnvOperatingSystem.Clipboard_Set(EnvSolutionDev.GetFile().FullName);

            //Code-Generator-Ausführen
            if (EnvSolutionExplorerDev.Node_SetFocus("_Utilities\\GP.StandardWCFCodeGenerator") != null)
            {
                EnvSolutionExplorerDev.Node_StartNewInstance("_Utilities\\GP.StandardWCFCodeGenerator");
            }
        }

        public static void Prepare_UpdateDb(EnvDTE80.DTE2 envDte = null)
        {
            EnvSolutionDev.Rebuild(true, envDte);

            //Verzeichnis setzten
            if (EnvSolutionDev.GetDirectory(envDte) != null)
            {
                string _ruleEngineExe = PathHelper.Combine(EnvSolutionDev.GetDirectory(envDte).FullName, "Bin", "GP.RuleEngine.exe");
                if (File.Exists(_ruleEngineExe))
                {
                    EnvOperatingSystem.StartProcess(_ruleEngineExe, "/updatedb");
                }
            }
        }


        public static void SetStartupProjects_UiWin(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService, GpSolutionElements.UiWin };
            EnvGpDev.SetStartupProjects(_array, envDte);
        }
        public static void SetStartupProjects_UiWinExt(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService, GpSolutionElements.CrossEnterpriseUnit, GpSolutionElements.UiWin };
            EnvGpDev.SetStartupProjects(_array, envDte);
        }
        public static void SetStartupProjects_Services(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService };
            EnvGpDev.SetStartupProjects(_array, envDte);
        }
        public static void SetStartupProjects_ServicesExt(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService, GpSolutionElements.CrossEnterpriseUnit };
            EnvGpDev.SetStartupProjects(_array, envDte);
        }
        public static void SetStartupProjects(GpSolutionElements element, EnvDTE80.DTE2 envDte = null)
        {
            EnvGpDev.SetStartupProjects(new[] { element }, envDte);
        }
        public static void SetStartupProjects(GpSolutionElements[] elements, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            var _startUpProjects = new List<string>();
            foreach(var _element in elements)
            {
                if (!string.IsNullOrEmpty(EnvGpDev.GetPrjPathOf(_element)))
                    _startUpProjects.Add(EnvGpDev.GetPrjPathOf(_element));
            }
            EnvSolutionDev.SetStartupProjects(_startUpProjects.ToArray(), envDte);
        }


        public static void StartNewInstance_UiWin(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService, GpSolutionElements.UiWin };
            EnvGpDev.StartNewInstance(_array, envDte);
        }
        public static void StartNewInstance_UiWinExt(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService, GpSolutionElements.CrossEnterpriseUnit, GpSolutionElements.UiWin };
            EnvGpDev.StartNewInstance(_array, envDte);
        }
        public static void StartNewInstance_Services(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService };
            EnvGpDev.StartNewInstance(_array, envDte);
        }
        public static void StartNewInstance_ServicesExt(EnvDTE80.DTE2 envDte = null)
        {
            GpSolutionElements[] _array = new GpSolutionElements[] { GpSolutionElements.RuleEnginem, GpSolutionElements.MachineControlService, GpSolutionElements.CrossEnterpriseUnit };
            EnvGpDev.StartNewInstance(_array, envDte);
        }
        public static void StartNewInstance(GpSolutionElements element, EnvDTE80.DTE2 envDte = null)
        {
            EnvGpDev.StartNewInstance(new[] { element }, envDte);
        }
        public static void StartNewInstance(GpSolutionElements[] elements, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            foreach(var _element in elements)
            {
                if (!string.IsNullOrEmpty(EnvGpDev.GetPathOf(_element)))
                {
                    EnvSolutionExplorerDev.Node_StartNewInstance(EnvGpDev.GetPathOf(_element), envDte);
                }
            }
        }


        public static void Process_Element(GpSolutionElements element, string arguments = "", EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            var _fileSysSubPath = EnvGpDev.GetBinPathOf(element);

            if (EnvSolutionDev.IsValid(envDte))
            {
                FileInfo _elementFile = new FileInfo(Path.Combine(EnvSolutionDev.GetDirectory(envDte).FullName, _fileSysSubPath));
                if (_elementFile.Exists)
                {
                    var _processInfo = EnvOperatingSystem.StartProcess(_elementFile, arguments);
                    System.Threading.Thread.Sleep(20);
                    EnvOperatingSystem.WindowToFront(_processInfo);
                }
            }
        }


        public static UIHierarchyItem Node_GetSolution(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            return EnvSolutionExplorerDev.Node_GetSolution(envDte);
        }

        public static UIHierarchyItem Node_GetItem(GpSolutionElements element, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            if (!string.IsNullOrEmpty(EnvGpDev.GetPathOf(element)))
            {
                return EnvSolutionExplorerDev.Node_GetSubHierachyItem(EnvGpDev.GetPathOf(element), envDte);
            }
            return null;
        }
    }


    internal class GpFileCollection
    {
        public class FileElement
        {
            public string Source { get; set; }

            public string Destination { get; set; }

            public override string ToString()
            {
                var _fiSource = new FileInfo(this.Source);
                var _fiDest = new FileInfo(this.Destination);
                // ReSharper disable once PossibleNullReferenceException
                return string.Format("{0} ( '{1}' => '{2}'", _fiSource.Name, _fiSource.Directory.Name, _fiDest.Directory.Name);
            }
        }

        public List<FileElement> Files { get; set; }

        public GpFileCollection()
        {
            this.Files = new List<FileElement>();
        }

        public void DoCopyFiles()
        {
            if (this.Files != null && this.Files.Count > 0)
            {
                foreach(var _file in this.Files)
                {
                    try
                    {
                        if (!File.Exists(_file.Source))
                            continue;

                        FileInfo _dest01 = new FileInfo(_file.Destination);
                        if (_dest01.Exists)
                        {
                            _dest01.Attributes -= FileAttributes.ReadOnly;
                            _dest01.Attributes -= FileAttributes.Hidden;
                            _dest01.Delete();
                        }

                        FileHelper.CopyFile(_file.Source, _file.Destination);

                        var _dest02 = new FileInfo(_file.Destination);
                        if (_dest02.Exists)
                        {
                            _dest02.Attributes -= FileAttributes.ReadOnly;
                            _dest01.Attributes -= FileAttributes.Hidden;
                        }
                    }
                    catch(Exception ex)
                    {
                        // ignored
                    }
                }
            }
        }
    }

    internal class GpLibManager
    {
        private GpLibManager()
        {

        }

        public static void Prepare(EnvDTE80.DTE2 envDte = null)
        {
            GpFileCollection _fileCollection = new GpFileCollection();

            //SolutionDirectory
            var _solDir = EnvSolutionDev.GetDirectory(envDte);
            if (!_solDir.Exists) return;

            //Dateien die kopiert werden wüssen
            //Ab 4.4
            {
                // PPG-Bin
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.OEMSupport\lib\GP.OEMSupport.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\GP.OEMSupport.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.OEMSupport\lib\OEMSupportKernel.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\OEMSupportKernel.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.OEMSupport\lib\OEMSupportKernel64.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\OEMSupportKernel64.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.ListLabel\lib\combit.ListLabel21.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\combit.ListLabel21.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.ListLabel\lib\combit.ListLabel21.Web.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\combit.ListLabel21.Web.dll")
                });

                // PPG-WebApps-Bin
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.OEMSupport\lib\GP.OEMSupport.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\webapps\bin\GP.OEMSupport.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.OEMSupport\lib\OEMSupportKernel.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\webapps\bin\OEMSupportKernel.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.OEMSupport\lib\OEMSupportKernel64.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\webapps\bin\OEMSupportKernel64.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.ListLabel\lib\combit.ListLabel21.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\webapps\bin\combit.ListLabel21.dll")
                });
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"packages\Kardex.PowerPick.ListLabel\lib\combit.ListLabel21.Web.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\webapps\bin\combit.ListLabel21.Web.dll")
                });
            }

            //Älter als 4.4
            {
                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"LIB\GP.OEMSupport.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\GP.OEMSupport.dll")
                });

                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"LIB\OEMSupportKernel.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\OEMSupportKernel.dll")
                });

                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"LIB\OEMSupportKernel64.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\OEMSupportKernel64.dll")
                });

                _fileCollection.Files.Add(new GpFileCollection.FileElement()
                {
                    Source = PathHelper.Combine(_solDir, @"LIB\Combit\x64\combit.ListLabel18.dll"),
                    Destination = PathHelper.Combine(_solDir, @"BIN\combit.ListLabel18.dll")
                });
            }

            //Kopieren
            _fileCollection.DoCopyFiles();
        }
    }

    internal class GpPlatformManager
    {
        class PlatformItem
        {
            public DirectoryInfo DirectoryInfo { get; set; }

            public string Key { get { return this.DirectoryInfo.Name; } }

            public string[] SubFiles
            {
                get
                {
                    return DirectoryHelper.GetDirectoryElements(this.DirectoryInfo) ?? new string[0];
                }
            }
        }

        private List<PlatformItem> m_platforms = new List<PlatformItem>();
        private readonly DirectoryInfo m_solutionDirectory;
        private string m_destPlatform;
        private EnvDTE80.DTE2 m_envDTE;

        private GpPlatformManager(string platform, EnvDTE80.DTE2 envDte = null)
        {
            this.m_destPlatform = platform;
            this.m_envDTE = envDte;
            
            //SolutionDirectory
            this.m_solutionDirectory = EnvSolutionDev.GetDirectory(envDte);
            if (!this.m_solutionDirectory.Exists) return;

            //Platformen Verzeichnis
            DirectoryInfo _platformDirectory = new DirectoryInfo(PathHelper.Combine(this.m_solutionDirectory, "platform"));
            if (!_platformDirectory.Exists) return;

            //Platformen Laden
            foreach(var _subDirectory in _platformDirectory.GetDirectories())
            {
                this.m_platforms.Add(new PlatformItem()
                    {
                        DirectoryInfo = _subDirectory
                    });
            }
        }


        public static void Prepare(string platform, EnvDTE80.DTE2 envDte = null)
        {
            GpPlatformManager _inst = new GpPlatformManager(platform, envDte);

            _inst.RemoveAll();


            var _platform = _inst.m_platforms.FirstOrDefault(p => p.Key.ToUpper() == platform);
            if (_platform != null)
            {
                _inst.Copy(_platform);
            }
        }



        private void Copy(PlatformItem platformItem)
        {
            var _destyFolders = new []
            {
                PathHelper.Combine(this.m_solutionDirectory.FullName, "BIN"),
                PathHelper.Combine(this.m_solutionDirectory.FullName, "bin\\webapps\\bin")
            };

            foreach (var _destyFolderPath in _destyFolders)
            {
                foreach (var _sourceItem in platformItem.SubFiles.OrderBy(p => p))
                {
                    var _sourceFi = new FileInfo(_sourceItem);
                    var _partlyPath = _sourceFi.FullName.Substring(platformItem.DirectoryInfo.FullName.Length);
                    var _destyFi = new FileInfo(PathHelper.Combine(_destyFolderPath, _partlyPath));

                    if (_sourceFi.Exists && !_destyFi.Exists)
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        if (!_destyFi.Directory.Exists)
                            _destyFi.Directory.Create();

                        FileHelper.CopyFile(_sourceFi, _destyFi);

                        _destyFi = new FileInfo(_destyFi.FullName);

                        if (!_destyFi.Exists) continue;

                        if ((_destyFi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            _destyFi.Attributes -= FileAttributes.ReadOnly;
                        }
                    }
                }
            }
        }



        private void RemoveAll()
        {
            foreach(var _valueItem in this.m_platforms)
            {
                this.Remove(_valueItem);
            }
        }
        private void Remove(PlatformItem platformItem)
        {
            foreach (var _sourceItem in platformItem.SubFiles.OrderByDescending(p => p))
            {
                var _partlyPath = _sourceItem.Substring(platformItem.DirectoryInfo.FullName.Length);
                var _destyFi = new FileInfo(PathHelper.Combine(this.m_solutionDirectory.FullName, "BIN", _partlyPath));

                if (_destyFi.Exists)
                {
                    if ((_destyFi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        _destyFi.Attributes -= FileAttributes.ReadOnly;
                    }

                    _destyFi.Delete();
                }
            }
        }
    }
}
