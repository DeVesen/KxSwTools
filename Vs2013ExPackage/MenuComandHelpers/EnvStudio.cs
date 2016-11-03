using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Linq;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public class EnvStudio
    {
        public static readonly Guid PaneDebug = new Guid("FC076020-078A-11D1-A7DF-00A0C9110051");

        public static readonly Guid PaneExtention = new Guid("5FBC1899-0461-4FAA-ADEB-8113ACAF3E9B");


        private EnvStudio()
        {

        }


        public static string OutputWnd_NameById(Guid id)
        {
            if (id == EnvStudio.PaneDebug)
                return "Debug";
            else if (id == EnvStudio.PaneExtention)
                return "KX: Studio Ex";

            return null;
        }

        
        public static void OutputWnd_WriteLine(string text, Guid paneId, EnvDTE80.DTE2 envDte = null)
        {
            EnvStudio.OutputWnd_WriteText(text + "\r\n", paneId, envDte);
        }
        public static void OutputWnd_WriteText(string text, Guid paneId, EnvDTE80.DTE2 envDte = null)
        {
            //Pane holen
            var _pane = EnvStudio.OutputWnd_Activate(paneId, envDte);

            if (_pane != null)
            {
                EnvStudio.DoEnvents(10, envDte);

                //Schreiben
                _pane.OutputString(text);

                EnvStudio.DoEnvents(10, envDte);
            }
        }
        public static void OutputWnd_Clear(Guid paneId, EnvDTE80.DTE2 envDte = null)
        {
            //Pane holen
            var _pane = EnvStudio.OutputWnd_GetPane(paneId, envDte);

            if (_pane != null)
            {
                _pane.Clear();
            }
        }

        public static OutputWindowPane OutputWnd_GetPane(Guid paneId, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return null;

            //OutputWindowPanes Casten
            var _panes = (from _p in envDte.ToolWindows.OutputWindow.OutputWindowPanes.Cast<object>().ToArray()
                          where _p is OutputWindowPane
                          select _p as OutputWindowPane).ToArray();

            //Debug - Pane finden     
            var _pane = _panes.FirstOrDefault(p => Guid.Parse(p.Guid) == paneId || string.Compare(p.Name, EnvStudio.OutputWnd_NameById(paneId), StringComparison.OrdinalIgnoreCase) == 0);

            //Ist er bekannt
            if (_pane == null)
            {
                if (paneId == EnvStudio.PaneExtention)
                {
                    _pane = envDte.ToolWindows.OutputWindow.OutputWindowPanes.Add("KX: Studio Ex");
                }
            }

            //Zurückgeben...
            return _pane;
        }
        public static OutputWindowPane OutputWnd_Activate(Guid paneId, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return null;

            var _pane = EnvStudio.OutputWnd_GetPane(paneId, envDte);
            if (_pane != null)
            {
                envDte.ToolWindows.OutputWindow.Parent.Activate();
                _pane.Activate();
            }

            return _pane;
        }


        public static void StatusBar_SetText(string text, EnvDTE80.DTE2 envDte = null)
        {
            //EnvDTE80.DTE2 _envDTE = envDTE ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            //_envDTE.StatusBar.Text = text;

            //EnvStudio.DoEnvents(_envDTE);
        }
        public static void StatusBar_SetProgress(bool inProgress, string label = "", int amountCompleted = 0, int total = 0, EnvDTE80.DTE2 envDte = null)
        {
            //EnvDTE80.DTE2 _envDTE = envDTE ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            //_envDTE.StatusBar.Progress(inProgress, label, amountCompleted, (int)Math.Max(amountCompleted, total));

            //EnvStudio.DoEnvents(_envDTE);
        }
        public static void StatusBar_SetTextAndProgress(string text, string label = "", int amountCompleted = 0, int total = 0, EnvDTE80.DTE2 envDte = null)
        {
            //EnvDTE80.DTE2 _envDTE = envDTE ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            //EnvStudio.StatusBar_SetText(text, _envDTE);
            //EnvStudio.StatusBar_SetProgress(true, label, amountCompleted, total, _envDTE);

            //EnvStudio.DoEnvents(_envDTE);
        }

        public static void StatusBar_Clear(EnvDTE80.DTE2 envDte = null)
        {
            //EnvDTE80.DTE2 _envDTE = envDTE ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            //_envDTE.StatusBar.Text = string.Empty;
            //_envDTE.StatusBar.Clear();
        }


        public static void DoEnvents(EnvDTE80.DTE2 envDte = null)
        {
            EnvStudio.DoEnvents(10, envDte);
        }
        public static void DoEnvents(int sleep = 10, EnvDTE80.DTE2 envDte = null)
        {
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(sleep <= 10 ? 10 : sleep);
        }
    }
}
