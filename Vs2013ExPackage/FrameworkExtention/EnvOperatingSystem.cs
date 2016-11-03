using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention
{
    public static class EnvOperatingSystem
    {
        public static void Clipboard_Set(string textValue)
        {
            if (textValue != null)
            {
                System.Windows.Forms.Clipboard.SetDataObject(textValue);
            }
        }


        public static Process StartProcess(string fileName)
        {
            return Process.Start(fileName);
        }
        public static Process StartProcess(string fileName, string arguments)
        {
            return Process.Start(fileName, arguments);
        }
        public static Process StartProcess(string fileName, string userName, System.Security.SecureString password, string domain)
        {
            return Process.Start(fileName, userName, password, domain);
        }
        public static Process StartProcess(string fileName, string arguments, string userName, System.Security.SecureString password, string domain)
        {
            return Process.Start(fileName, arguments, userName, password, domain);
        }


        public static Process StartProcess(FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo != null && fileSystemInfo.Exists)
            {
                return Process.Start(fileSystemInfo.FullName);
            }
            return null;
        }
        public static Process StartProcess(FileSystemInfo fileSystemInfo, string arguments)
        {
            if (fileSystemInfo != null && fileSystemInfo.Exists)
            {
                return Process.Start(fileSystemInfo.FullName, arguments);
            }
            return null;
        }
        public static Process StartProcess(FileSystemInfo fileSystemInfo, string userName, System.Security.SecureString password, string domain)
        {
            if (fileSystemInfo != null && fileSystemInfo.Exists)
            {
                return Process.Start(fileSystemInfo.FullName, userName, password, domain);
            }
            return null;
        }
        public static Process StartProcess(FileSystemInfo fileSystemInfo, string arguments, string userName, System.Security.SecureString password, string domain)
        {
            if (fileSystemInfo != null && fileSystemInfo.Exists)
            {
                return Process.Start(fileSystemInfo.FullName, arguments, userName, password, domain);
            }
            return null;
        }



        public static bool CheckPingTo(string destination, int msTimeout = 3000)
        {
            try
            {
                var _ping = new System.Net.NetworkInformation.Ping();
                var _reply = _ping.Send(destination, msTimeout);

                return _reply != null && _reply.Status == System.Net.NetworkInformation.IPStatus.Success;
            }
            catch
            {
                // ignored
            }

            return false;
        }



        public class ExecuteCommandResult
        {
            public string Command { get; set; }

            public string DirectoryToProcessIn { get; set; }


            public bool WaitForExit { get; set; }
            public bool CreateNoWindow { get; set; }
            public bool UseShellExecute { get; set; }
            public bool RedirectStandardError { get; set; }
            public bool RedirectStandardOutput { get; set; }


            public string Output { get; set; }
            public string Error { get; set; }
            public int ExitCode { get; set; }
        }
        public static ExecuteCommandResult ExecuteCommand(FileInfo fileInfo, DirectoryInfo directoryToProcessIn,
                                                          bool waitForExit = true,
                                                          bool createNoWindow = true, bool useShellExecute = false,
                                                          bool redirectStandardError = true, bool redirectStandardOutput = true)
        {
            if (!fileInfo.Exists)
                return null;

            return EnvOperatingSystem.ExecuteCommand(fileInfo.FullName, directoryToProcessIn,
                                                   waitForExit,
                                                   createNoWindow, useShellExecute,
                                                   redirectStandardError, redirectStandardOutput);
        }
        public static ExecuteCommandResult ExecuteCommand(string command, DirectoryInfo directoryToProcessIn,
                                                          bool waitForExit = true,
                                                          bool createNoWindow = true, bool useShellExecute = false,
                                                          bool redirectStandardError = true, bool redirectStandardOutput = true)
        {
            ExecuteCommandResult _result = new ExecuteCommandResult()
            {
                Command = command,
                DirectoryToProcessIn = directoryToProcessIn != null ? directoryToProcessIn.FullName : null,
                CreateNoWindow = createNoWindow,
                UseShellExecute = useShellExecute,
                RedirectStandardError = redirectStandardError,
                RedirectStandardOutput = redirectStandardOutput,
            };

            ProcessStartInfo _processInfo = new ProcessStartInfo("cmd.exe", "/c \"" + command + "\"")
            {
                CreateNoWindow = createNoWindow,
                UseShellExecute = useShellExecute,
                // *** Redirect the output ***
                RedirectStandardError = redirectStandardError,
                RedirectStandardOutput = redirectStandardOutput,
            };

            if (directoryToProcessIn != null && directoryToProcessIn.Exists)
            {
                Environment.CurrentDirectory = directoryToProcessIn.FullName;
            }

            var _process = Process.Start(_processInfo);

            if (waitForExit && _process != null)
            {
                _process.WaitForExit();
                {
                    // *** Read the streams ***
                    if (redirectStandardOutput)
                        _result.Output = _process.StandardOutput.ReadToEnd();
                    if (redirectStandardError)
                        _result.Error = _process.StandardError.ReadToEnd();
                    _result.ExitCode = _process.ExitCode;
                }

                _process.Close();
            }

            return _result;
        }



        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hwnd, uint windowStyle);
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool WindowToFront(Process process)
        {
            if (process == null) return false;

            try
            {
                return EnvOperatingSystem.SetForegroundWindow(process.MainWindowHandle);
            }
            catch
            {
                // ignored
            }

            return false;
        }
    }


    public static class PathHelper
    {
        public static string Combine(DirectoryInfo directory, params string[] elements)
        {
            List<string> _pathItems = new List<string>();

            if (directory != null)
                _pathItems.Add(directory.FullName);

            if (elements != null && elements.Length > 0)
                _pathItems.AddRange(elements);

            return PathHelper.Combine(_pathItems.ToArray());
        }

        public static string Combine(params string[] elements)
        {
            string _result = null;

            if (elements != null && elements.Length > 0)
            {
                _result = elements[0];

                for (int _index = 1; _index < elements.Length; _index++)
                {
                    string _tmp = elements[_index];

                    if (_tmp.StartsWith("\\"))
                        _tmp = _tmp.Substring(1);

                    _result = Path.Combine(_result, _tmp);
                }
            }

            return _result;
        }
    }


    public static class FileHelper
    {
        public static void CopyFile(FileInfo source, FileInfo dest, bool overrideIfExist = true)
        {
            FileHelper.CopyFile(source.FullName, dest.FullName, overrideIfExist);
        }

        public static void CopyFile(string source, string dest, bool overrideIfExist = true)
        {
            FileInfo _source = new FileInfo(source);
            FileInfo _dest = new FileInfo(dest);

            if (_dest.Exists && overrideIfExist)
            {
                _dest.Attributes -= FileAttributes.ReadOnly;
                _dest.Delete();
            }

            _source.CopyTo(_dest.FullName, overrideIfExist);
        }
    }


    public static class DirectoryHelper
    {
        public static string[] GetDirectoryElements(DirectoryInfo parentDirectory)
        {
            List<string> _lst = new List<string>();

            if (parentDirectory != null && parentDirectory.Exists)
            {
                var _fileArray = parentDirectory.GetFiles();

                if (_fileArray.Length > 0)
                {
                    _lst.AddRange(_fileArray.Select(p => p.FullName));
                }

                var _directoryArray = parentDirectory.GetDirectories();
                if (_directoryArray.Length > 0)
                {
                    foreach (var _directoryItem in _directoryArray)
                    {
                        _lst.Add(_directoryItem.FullName);
                        _lst.AddRange(DirectoryHelper.GetDirectoryElements(_directoryItem));
                    }
                }
            }

            return _lst.ToArray();
        }
    }
}
