using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Linq;
using KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public class EnvSolutionDev
    {
        private EnvSolutionDev()
        {

        }



        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.ToolWindows != null &&
                envDte.ToolWindows.SolutionExplorer != null &&
                envDte.ToolWindows.SolutionExplorer.UIHierarchyItems != null && envDte.ToolWindows.SolutionExplorer.UIHierarchyItems.Count > 0)
            {
                return envDte.ActiveWindow != null;
            }

            return false;
        }



        public static bool Exist(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (EnvSolutionExplorerDev.Node_GetSolution(envDte) != null)
            {
                return !string.IsNullOrEmpty(EnvSolutionExplorerDev.Node_GetSolution(envDte).Name);
            }

            return false;
        }

        public static void Close(bool saveBeforClose = true, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null)
            {
                envDte.Solution.Close(saveBeforClose);
            }
        }

        public static string GetName(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (EnvSolutionExplorerDev.Node_GetSolution(envDte) != null)
            {
                return EnvSolutionExplorerDev.Node_GetSolution(envDte).Name;
            }

            return null;
        }

        public static bool IsActiveConfiguration(string configName, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
            
            if (envDte != null &&
                envDte.Solution != null)
            {
                if (envDte.Solution.SolutionBuild != null && envDte.Solution.SolutionBuild.ActiveConfiguration != null)
                {
                    return envDte.Solution.SolutionBuild.ActiveConfiguration.Name.IsEqual(configName);
                }
            }

            return false;
        }
        public static bool SetActiveConfiguration(string configName, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null)
            {
                if (!EnvSolutionDev.IsActiveConfiguration(configName, envDte))
                {
                    var _config = EnvSolutionDev.GetConfiguration(configName, envDte);
                    if (_config != null)
                    {
                        _config.Activate();
                    }
                }
            }

            return false;
        }
        public static EnvDTE80.SolutionConfiguration2 GetConfiguration(string configName, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null)
            {
                foreach (EnvDTE80.SolutionConfiguration2 _solutionConfiguration in envDte.Solution.SolutionBuild.SolutionConfigurations)
                {
                    if (_solutionConfiguration != null && _solutionConfiguration.Name.ToUpper() == configName.ToUpper())
                    {
                        return _solutionConfiguration;
                    }
                }
            }

            return null;
        }

        public static FileInfo GetFile(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return null;

            return EnvSolutionExplorerDev.Node_GetSolution(envDte) != null ? (new FileInfo(envDte.Solution.FullName)) : null;
        }
        public static DirectoryInfo GetDirectory(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return null;

            return EnvSolutionExplorerDev.Node_GetSolution(envDte) != null ? (new FileInfo(envDte.Solution.FullName)).Directory : null;
        }



        public static void Rebuild(bool waitForBuildToFinish = false, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null &&
                envDte.Solution.SolutionBuild != null)
            {
                //_envDTE.ExecuteCommand("Build.RebuildSolution");

                envDte.Solution.SolutionBuild.SolutionConfigurations.Item(1).Activate();
                //_envDTE.Solution.SolutionBuild.Clean(true);
                envDte.Solution.SolutionBuild.Build(waitForBuildToFinish);
            }
        }
        public static void Rebuild(System.Threading.ManualResetEvent mre, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null &&
                envDte.Solution.SolutionBuild != null)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    envDte.Solution.SolutionBuild.SolutionConfigurations.Item(1).Activate();
                    envDte.Solution.SolutionBuild.Build(true);
                    mre.Set();
                });
            }
        }

        public static void SetStartupProjects(string[] projectNames, EnvDTE80.DTE2 envDte = null)
        {
            if (projectNames != null && projectNames.Length > 0)
            {
                envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

                if (envDte != null &&
                    envDte.Solution != null &&
                    envDte.Solution.SolutionBuild != null)
                {
                    envDte.Solution.SolutionBuild.StartupProjects = projectNames.Cast<object>().ToArray();
                }
            }
        }
    }
}
