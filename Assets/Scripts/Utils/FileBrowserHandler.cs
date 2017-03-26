using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityToolbag;

namespace Assets.Scripts.Utils
{
    public class FileBrowserHandler
    {
        private static readonly string FileBrowserProgramName = Path.Combine(Application.streamingAssetsPath, "EdusimFileBrowser.exe");
        private const string WindowTitleOptionTemplate = " -t {0} ";
        private const string RootBrowsingFolderOptionTemplate = " -r {0} ";
        private const string FileExtensionOptionTemplate = " -e {0} ";
        private const string OperationModeOptionTemplate = " -o {0} ";
        private const string FileNameOptionTemplate = " -f {0} ";
        private const string LoadWindowTitle = " \"_load Es File\" ";
        private const string SaveWindowTitle = " \"_save Es File\" ";
        private const string EdusimProjectFileDescription = "EduSim project files(*.es)";
        private const string FileExtensionFilter = "\"" + EdusimProjectFileDescription + "| *.es\" ";

        private Persistance _persistanceScript;
        private Process _currentRunningProcess;
        private static FileBrowserHandler _instance;

        public static FileBrowserHandler Instance
        {
            get { return _instance ?? (_instance = new FileBrowserHandler()); }
        }

        public Persistance PersistanceScript
        {
            set
            {
                if (_persistanceScript == null)
                {
                    _persistanceScript = value;
                }
            }
        }

        private FileBrowserHandler()
        {
        }

        public void LoadFile(string rootBrowsingFolder = null, string fileExtension = FileExtensionFilter)
        {
            if (_currentRunningProcess != null)
            {
                throw new Exception("Only one file browsing process can be run at a time");
            }

            _currentRunningProcess = new Process();
            _currentRunningProcess.StartInfo.Arguments = BuildArguments(true, LoadWindowTitle, rootBrowsingFolder,
                fileExtension, Path.GetFileName(_persistanceScript.LastFileName));
            _currentRunningProcess.OutputDataReceived += HandleOnLoadOutputDataReceived;

            ConfigureAndStartProcess();
        }

        public void SaveFile(string rootBrowsingFolder = null, string fileExtension = FileExtensionFilter)
        {
            if (string.IsNullOrEmpty(_persistanceScript.LastFileName))
            {
                SaveAsFile();
            }
            else
            {
                _persistanceScript.Save(null);
            }
        }

        public void SaveAsFile(string rootBrowsingFolder = null, string fileExtension = FileExtensionFilter)
        {
            if (_currentRunningProcess != null)
            {
                throw new Exception("Only one file browsing process can be run at a time");
            }

            _currentRunningProcess = new Process();
            _currentRunningProcess.StartInfo.Arguments = BuildArguments(false, SaveWindowTitle, rootBrowsingFolder,
                fileExtension, Path.GetFileName(_persistanceScript.LastFileName));
            _currentRunningProcess.OutputDataReceived += HandleOnSaveOutputDataReceived;
            
            ConfigureAndStartProcess();
        }

        private void HandleOnLoadOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            string fileName = dataReceivedEventArgs.Data;
            if (!string.IsNullOrEmpty(fileName))
            {
                Dispatcher.Invoke(() =>
                {
                    _persistanceScript.Load(fileName);
                });
            }
        }

        private void HandleOnSaveOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            string fileName = dataReceivedEventArgs.Data;
            if (!string.IsNullOrEmpty(fileName))
            {
                Dispatcher.Invoke(() =>
                {
                    _persistanceScript.Save(fileName);
                });
            }
        }

        private void HandleOnProcessExited(object sender, EventArgs eventArgs)
        {
            _currentRunningProcess = null;
        }

        private void ConfigureAndStartProcess()
        {
            _currentRunningProcess.StartInfo.FileName = FileBrowserProgramName;
            _currentRunningProcess.StartInfo.RedirectStandardOutput = true;
            _currentRunningProcess.StartInfo.UseShellExecute = false;
            _currentRunningProcess.EnableRaisingEvents = true;

            _currentRunningProcess.Exited += HandleOnProcessExited;
            _currentRunningProcess.Start();
            _currentRunningProcess.BeginOutputReadLine();
        }

        private static string BuildArguments(bool isLoading, string windowTitle, string rootBrowsingFolder,
            string fileExtension, string fileName)
        {
            StringBuilder arguments = new StringBuilder();

            arguments.Append(string.Format(WindowTitleOptionTemplate, windowTitle));
            if (!string.IsNullOrEmpty(rootBrowsingFolder))
            {
                arguments.Append(string.Format(RootBrowsingFolderOptionTemplate, rootBrowsingFolder));
            }
            if (!string.IsNullOrEmpty(fileExtension))
            {
                arguments.Append(string.Format(FileExtensionOptionTemplate, fileExtension));
            }
            if (isLoading)
            {
                arguments.Append(string.Format(OperationModeOptionTemplate, true));
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                arguments.Append(string.Format(FileNameOptionTemplate, fileName));
            }
            return arguments.ToString();
        }
    }
}
