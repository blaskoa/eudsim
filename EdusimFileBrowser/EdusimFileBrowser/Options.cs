using CommandLine;

namespace EdusimFileBrowser
{
    class Options
    {
        [Option('r', "root", Required = false, HelpText = "Root folder of the file browser")]
        public string Root { get; set; }

        [Option('t', "title", Required = true, HelpText = "Title of the file browser widnow")]
        public string Title { get; set; }

        [Option('e', "extension", Required = false, HelpText = "Extension mask for specific file browsing")]
        public string ExtensionMask { get; set; }

        [Option('f', "filename", Required = false, HelpText = "Default file name for saving/loading")]
        public string FileName { get; set; }

        [Option('o', "open", Required = false, HelpText = "Mode of file browsing. Set to false for saving")]
        public bool IsOpeningMode { get; set; }
    }
}
