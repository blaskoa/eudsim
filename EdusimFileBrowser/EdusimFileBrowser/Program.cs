using System;
using System.Drawing;
using System.Windows.Forms;
using CommandLine;

namespace EdusimFileBrowser
{
    public class Program
    {
        public const string IconFilePath = "test";
        [STAThread]
        public static void Main(string[] args)
        {
            
            using (var owner = new Form
            {
                Width = 0,
                Height = 0,
                StartPosition = FormStartPosition.Manual,
                Location = new Point(-999999999,-999999999),
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                Text = "Browse for Folder"
            })
            {
                Options options = new Options();
                Parser.Default.ParseArguments(args, options);
                FileDialog fileDialog;
                if (options.IsOpeningMode)
                {
                    fileDialog = new OpenFileDialog();
                }
                else
                {
                    fileDialog = new SaveFileDialog();
                }

                if (!string.IsNullOrWhiteSpace(options.Root))
                {
                    fileDialog.InitialDirectory = options.Root;
                }

                if (!string.IsNullOrWhiteSpace(options.ExtensionMask))
                {
                    fileDialog.Filter = options.ExtensionMask;
                }

                fileDialog.Title = options.Title;
                fileDialog.FileName = options.FileName;
                owner.Show();
                owner.SendToBack();

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Console.Out.WriteLine(fileDialog.FileName);
                }
            }
        }
    }
}
