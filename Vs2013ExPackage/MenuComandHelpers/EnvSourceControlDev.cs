using KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention;
using Microsoft.VisualStudio.Shell;
using System.Linq;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public static class EnvSourceControlDev
    {
        public static void CheckOut_Files(string fileToCheckOut, EnvDTE80.DTE2 envDte = null)
        {
            EnvSourceControlDev.CheckOut_Files(new[] { fileToCheckOut }, envDte);
        }

        public static void CheckOut_Files(string[] fileToCheckOut, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null || envDte.Solution == null || envDte.Solution.SolutionBuild == null ||
                !EnvSolutionDev.GetDirectory(envDte).Exists) return;

            var _fullName =
                    (
                        from _p in fileToCheckOut
                        select PathHelper.Combine(EnvSolutionDev.GetDirectory(envDte), _p)
                    ).ToArray();

            System.IO.FileInfo[] _filtered =
                    (
                        from _p in _fullName
                        where (new System.IO.FileInfo(_p)).Exists
                        select new System.IO.FileInfo(_p)
                    ).ToArray();

            if (_fullName.Length == fileToCheckOut.Length)
            {
                foreach (System.IO.FileInfo _file in _filtered)
                {
                    if (envDte.SourceControl.IsItemUnderSCC(_file.FullName))
                    {
                        if (!envDte.SourceControl.IsItemCheckedOut(_file.FullName))
                        {
                            envDte.SourceControl.CheckOutItem(_file.FullName);
                        }
                    }
                    else
                    {
                        _file.Attributes -= System.IO.FileAttributes.ReadOnly;
                    }
                }
            }
        }


        public static void CheckIn_Files_Direct(string fileToCheckOut, EnvDTE80.DTE2 envDte = null)
        {
            EnvSourceControlDev.CheckIn_Files_Direct(new[] { fileToCheckOut }, envDte);
        }

        public static void CheckIn_Files_Direct(string[] fileToCheckOut, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null || envDte.Solution == null || envDte.Solution.SolutionBuild == null ||
                !EnvSolutionDev.GetDirectory(envDte).Exists) return;

            System.IO.FileInfo[] _filtered =
                    (
                        from _p in fileToCheckOut
                        where (new System.IO.FileInfo(_p)).Exists
                        select new System.IO.FileInfo(_p)
                    ).ToArray();

            foreach (System.IO.FileInfo _file in _filtered)
            {
                try
                {
                    var _isControled = envDte.SourceControl.IsItemUnderSCC(_file.FullName);
                    var _isCheckedOut = envDte.SourceControl.IsItemCheckedOut(_file.FullName);

                    if (!_isControled && !_isCheckedOut)
                    {

                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    if ((_file.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                    {
                        _file.Attributes -= System.IO.FileAttributes.ReadOnly;
                    }
                }
            }
        }


        public static void CheckOut_Files_Direct(string fileToCheckOut, EnvDTE80.DTE2 envDte = null)
        {
            EnvSourceControlDev.CheckOut_Files_Direct(new[] { fileToCheckOut }, envDte);
        }

        public static void CheckOut_Files_Direct(string[] fileToCheckOut, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null || envDte.Solution == null || envDte.Solution.SolutionBuild == null ||
                !EnvSolutionDev.GetDirectory(envDte).Exists) return;

            var _filtered =
                    (
                        from _p in fileToCheckOut
                        where (new System.IO.FileInfo(_p)).Exists
                        select new System.IO.FileInfo(_p)
                    ).ToArray();

            foreach (var _file in _filtered)
            {
                try
                {
                    // ReSharper disable once UnusedVariable
                    var _isControled = envDte.SourceControl.IsItemUnderSCC(_file.FullName);
                    var _isCheckedOut = envDte.SourceControl.IsItemCheckedOut(_file.FullName);

                    if (!_isCheckedOut)
                    {
                        envDte.SourceControl.CheckOutItem(_file.FullName);
                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    if ((_file.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                    {
                        _file.Attributes -= System.IO.FileAttributes.ReadOnly;
                    }
                }
            }
        }
    }
}
