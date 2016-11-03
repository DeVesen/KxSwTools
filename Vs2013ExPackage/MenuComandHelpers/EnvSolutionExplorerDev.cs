using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Linq;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public class EnvSolutionExplorerDev
    {
        private EnvSolutionExplorerDev()
        {

        }



        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (envDte != null &&
                envDte.ToolWindows != null &&
                envDte.ToolWindows.SolutionExplorer != null)
            {
                return true;
            }

            return false;
        }



        public static int GetUiHierarchyItemsCount(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return 0;

            if (EnvSolutionExplorerDev.IsValid(envDte) &&
                envDte.ToolWindows.SolutionExplorer.UIHierarchyItems != null && envDte.ToolWindows.SolutionExplorer.UIHierarchyItems.Count > 0)
            {
                return envDte.ToolWindows.SolutionExplorer.UIHierarchyItems.Count;
            }

            return 0;
        }



        public static void Node_ExpandedAll(bool expanded, bool alsoDeeperAsProjects = false, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            UIHierarchyItem _nodeItem = EnvSolutionExplorerDev.Node_GetSolution(envDte);
            if (_nodeItem != null)
            {
                if (_nodeItem.UIHierarchyItems.Count > 0)
                {
                    foreach (UIHierarchyItem _subItem in _nodeItem.UIHierarchyItems)
                    {
                        EnvSolutionExplorerDev.Node_ExpandedAll(_subItem, expanded, alsoDeeperAsProjects);
                    }
                }
            }
        }
        public static void Node_ExpandedAll(UIHierarchyItem parentItem, bool expanded, bool alsoDeeperAsProjects = false)
        {
            if (parentItem != null)
            {
                if (!parentItem.UIHierarchyItems.Expanded)
                {
                    parentItem.UIHierarchyItems.Expanded = true;
                }
                else if (parentItem.UIHierarchyItems.Expanded && parentItem.UIHierarchyItems.Count <= 0)
                {
                    parentItem.UIHierarchyItems.Expanded = false;
                    parentItem.UIHierarchyItems.Expanded = true;
                }

                if (parentItem.UIHierarchyItems.Count > 0)
                {
                    var _isProject = parentItem.Object is Project;

                    if (!_isProject || alsoDeeperAsProjects || !expanded)
                    {
                        foreach (UIHierarchyItem _subItem in parentItem.UIHierarchyItems)
                        {
                            EnvSolutionExplorerDev.Node_ExpandedAll(_subItem, expanded);
                        }
                    }
                }

                parentItem.UIHierarchyItems.Expanded = expanded;
            }
        }

        public static UIHierarchyItem Node_SetFocus(string projectSolPath, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            if (EnvSolutionExplorerDev.IsValid(envDte))
            {
                UIHierarchyItem _nodeItem = EnvSolutionExplorerDev.Node_GetSubHierachyItem(projectSolPath, envDte);

                if (_nodeItem != null)
                {
                    _nodeItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
                    return _nodeItem;
                }
            }
            return null;
        }
        public static UIHierarchyItem Node_StartNewInstance(string projectSolPath, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return null;
            if (!EnvSolutionExplorerDev.IsValid(envDte)) return null;

            var _nodeItem = EnvSolutionExplorerDev.Node_SetFocus(projectSolPath, envDte);
            if (_nodeItem == null) return null;

            envDte.ToolWindows.SolutionExplorer.Parent.Activate();

            envDte.ToolWindows.SolutionExplorer.DTE.ExecuteCommand("ClassViewContextMenus.ClassViewProject.Debug.Startnewinstance", string.Empty);

            return _nodeItem;
        }

        public static UIHierarchyItem Node_GetSolution(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (envDte == null) return null;

            if (EnvSolutionExplorerDev.IsValid(envDte) &&
                envDte.ToolWindows.SolutionExplorer.UIHierarchyItems != null && envDte.ToolWindows.SolutionExplorer.UIHierarchyItems.Count > 0)
            {
                return envDte.ToolWindows.SolutionExplorer.UIHierarchyItems.Item(1);
            }

            return null;
        }
        public static UIHierarchyItem Node_GetSubHierachyItem(string projectSolPath, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            var _currentSolutionNode = EnvSolutionExplorerDev.Node_GetSolution(envDte);
            if (_currentSolutionNode != null)
            {
                var _result = EnvSolutionExplorerDev.Node_GetSubHierachyItem(projectSolPath, _currentSolutionNode, envDte);
                return _result;
            }

            return null;
        }
        public static UIHierarchyItem Node_GetSubHierachyItem(string projectSolPath, UIHierarchyItem parentItem, EnvDTE80.DTE2 envDte = null)
        {
            if (parentItem == null)
                return null;

            string[] _projPath = EnvSolutionExplorerDev.SplitProjectSolPath(projectSolPath);
            if (_projPath.Length <= 0)
                return null;

            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            if (EnvSolutionExplorerDev.Node_GetSolution(envDte) != null)
            {
                for (int _index = 0; _index < _projPath.Length && parentItem != null; _index++)
                {
                    parentItem = EnvSolutionExplorerDev.Node_GetSubHierachyItem(parentItem, _projPath[_index]);
                    if (parentItem != null && _index + 1 >= _projPath.Length)
                    {
                        return parentItem;
                    }
                }
            }

            return null;
        }
        public static UIHierarchyItem Node_GetSubHierachyItem(UIHierarchyItem parentItem, string subItemName)
        {
            if (parentItem == null)
                return null;

            if (!parentItem.UIHierarchyItems.Expanded)
            {
                parentItem.UIHierarchyItems.Expanded = true;
            }
            else if (parentItem.UIHierarchyItems.Expanded && parentItem.UIHierarchyItems.Count <= 0)
            {
                parentItem.UIHierarchyItems.Expanded = false;
                parentItem.UIHierarchyItems.Expanded = true;
            }

            if (parentItem.UIHierarchyItems.Count <= 0)
                return null;

            if (string.IsNullOrEmpty(subItemName))
                return null;

            return parentItem.UIHierarchyItems.Cast<UIHierarchyItem>().ToArray().FirstOrDefault(p => string.Compare(p.Name, subItemName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public static bool CommandArguments_Set(string projectSolPath, string commandLineArgument, EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;

            UIHierarchyItem _projectNode = EnvSolutionExplorerDev.Node_SetFocus(projectSolPath, envDte);
            if (_projectNode != null)
            {
                Project _project = _projectNode.Object as Project;
                if (_project != null &&
                    _project.ConfigurationManager != null &&
                    _project.ConfigurationManager.ActiveConfiguration != null)
                {
                    Property _property = _project.ConfigurationManager.ActiveConfiguration.Properties.Item("StartArguments");
                    if (_property != null)
                    {
                        _property.Value = commandLineArgument;

                        return true;
                    }
                }
            }

            return false;
        }



        private static string[] SplitProjectSolPath(string projectSolPath)
        {
            if (!string.IsNullOrEmpty(projectSolPath))
            {
                return projectSolPath.Split(new[] { "\\" }, StringSplitOptions.None);
            }

            return new string[0];
        }
    }
}
