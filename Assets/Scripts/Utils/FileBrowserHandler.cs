using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Assets.Scripts.Localization;
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
        private const string FileExtensionFilterTemplate = "\" {0} | *.es\" ";
        private const string ExportFileExtensionFilterTemplate = "\" {0} | *.zip\" ";

        private const string LoadWindowTitleKey = "LoadWindowTitle";
        private const string SaveWindowTitleKey = "SaveWindowTitle";
        private const string ExportWindowTitleKey = "ExportWindowTitle";
        private const string EdusimProjectFileDescriptionKey = "EdusimProjectFileDescription";
        private const string EdusimExportFileDescriptionKey = "EdusimExportFileDescription";

        private Persistance _persistanceScript;
        private ExportHTML _exportScript;
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

        // Setter for the export script
        public ExportHTML ExportScript
        {
            set
            {
                if (_exportScript == null)
                {
                    _exportScript = value;
                }
            }
        }

        private FileBrowserHandler()
        {
        }

        public void LoadFile(string rootBrowsingFolder = null, string fileExtension = FileExtensionFilterTemplate)
        {
            if (_currentRunningProcess != null)
            {
                throw new Exception("Only one file browsing process can be run at a time");
            }

            string loadWindowTitle = ResourceReader.Instance.GetResource(LoadWindowTitleKey);
            string fileExtensionDescription = ResourceReader.Instance.GetResource(EdusimProjectFileDescriptionKey);

            _currentRunningProcess = new Process();
            _currentRunningProcess.StartInfo.Arguments = BuildArguments(true, loadWindowTitle, rootBrowsingFolder,
                string.Format(fileExtension, fileExtensionDescription), Path.GetFileName(_persistanceScript.LastFileName));
            _currentRunningProcess.OutputDataReceived += HandleOnLoadOutputDataReceived;

            ConfigureAndStartProcess();
        }

        public void SaveFile(string rootBrowsingFolder = null, string fileExtension = FileExtensionFilterTemplate)
        {
            if (string.IsNullOrEmpty(_persistanceScript.LastFileName))
            {
                SaveAsFile(rootBrowsingFolder, fileExtension);
            }
            else
            {
                _persistanceScript.Save(null);
            }
        }

        public void SaveAsFile(string rootBrowsingFolder = null, string fileExtension = FileExtensionFilterTemplate)
        {
            if (_currentRunningProcess != null)
            {
                throw new Exception("Only one file browsing process can be run at a time");
            }

            string saveWindowTitle = ResourceReader.Instance.GetResource(SaveWindowTitleKey);
            string fileExtensionDescription = ResourceReader.Instance.GetResource(EdusimProjectFileDescriptionKey);

            _currentRunningProcess = new Process();
            _currentRunningProcess.StartInfo.Arguments = BuildArguments(false, saveWindowTitle, rootBrowsingFolder,
                string.Format(fileExtension, fileExtensionDescription), Path.GetFileName(_persistanceScript.LastFileName));
            _currentRunningProcess.OutputDataReceived += HandleOnSaveOutputDataReceived;
            
            ConfigureAndStartProcess();
        }

        // Opens file browser and passes the path to the handler
        public void SaveExport(string rootBrowsingFolder = null, string fileExtension = ExportFileExtensionFilterTemplate) 
        {
            if (_currentRunningProcess != null)
            {
                throw new Exception("Only one file browsing process can be run at a time");
            }

            string saveWindowTitle = ResourceReader.Instance.GetResource(ExportWindowTitleKey);
            string fileExtensionDescription = ResourceReader.Instance.GetResource(EdusimExportFileDescriptionKey);

            _currentRunningProcess = new Process();
            _currentRunningProcess.StartInfo.Arguments = BuildArguments(false, saveWindowTitle, rootBrowsingFolder,
                string.Format(fileExtension, fileExtensionDescription), "EdusimExport");
            _currentRunningProcess.OutputDataReceived += HandleOnExportOutputDataReceived;

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

        // When user chooses path for export, call exporting function
        private void HandleOnExportOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            string fileName = dataReceivedEventArgs.Data;
            if (!string.IsNullOrEmpty(fileName))
            {
                Dispatcher.Invoke(() =>
                {
                    _exportScript.MakeHtmlExport(fileName);
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
