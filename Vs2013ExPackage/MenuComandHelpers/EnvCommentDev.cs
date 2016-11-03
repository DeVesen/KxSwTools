using Microsoft.VisualStudio.Shell;
using System;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public class EnvCommentDev
    {
        private EnvCommentDev()
        {

        }


        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null &&
                envDte.ActiveWindow != null &&
                envDte.ActiveWindow.Selection != null &&
                envDte.ActiveWindow.Selection is EnvDTE.TextSelection &&
                EnvSolutionDev.Exist(envDte))
            {
                return envDte.ActiveWindow != null;
            }

            return false;
        }


        private static string GetCommentBody(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.Solution != null &&
                envDte.ActiveWindow != null &&
                envDte.ActiveWindow.Selection != null &&
                EnvSolutionDev.Exist(envDte))
            {
                return string.Format("{0}_{1}_{2}", EnvSolutionDev.GetName(envDte), Environment.UserName, DateTime.Now.ToString("yyyy-MM-dd_HH:mm"));
            }

            return string.Empty;
        }



        public static void SetLine(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return;
            if (!EnvCommentDev.IsValid(envDte)) return;
            
            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).Insert(string.Format("//{0}", EnvCommentDev.GetCommentBody(envDte)));
        }



        public static void SetBlock(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return;
            if (!EnvCommentDev.IsValid(envDte)) return;
            
            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).Insert(string.Format("//{0}...", EnvCommentDev.GetCommentBody(envDte)));
            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).NewLine();
            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).NewLine();
            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).Insert(string.Format("//...{0}", EnvCommentDev.GetCommentBody(envDte)));

            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).LineUp();
        }

        public static void SetBlockStart(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return;
            if (!EnvCommentDev.IsValid(envDte)) return;

            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).Insert(string.Format("//{0}...", EnvCommentDev.GetCommentBody(envDte)));
        }

        public static void SetBlockEnde(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return;
            if (!EnvCommentDev.IsValid(envDte)) return;

            ((EnvDTE.TextSelection)envDte.ActiveWindow.Selection).Insert(string.Format("//...{0}", EnvCommentDev.GetCommentBody(envDte)));
        }
    }
}
