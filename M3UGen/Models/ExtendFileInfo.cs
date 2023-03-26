using System;
using System.IO;
using Prism.Mvvm;

namespace M3UGen.Models
{
    public class ExtendFileInfo : BindableBase
    {
        private string baseOfRelativePath = new FileInfo("/").FullName;
        private bool displayingRelativePath;
        private string displayName = string.Empty;

        public ExtendFileInfo(FileInfo fi)
        {
            FileInfo = fi;
            DisplayName = FullPath;
        }

        private FileInfo FileInfo { get; set; }

        private string FullPath => FileInfo.FullName;

        public string DisplayName
        {
            get => displayName;
            set => SetProperty(ref displayName, value);
        }

        public string RelativePath
        {
            get
            {
                Uri u1 = new Uri(BaseOfRelativePath);
                Uri u2 = new Uri(FullPath);

                Uri relativeUri = u1.MakeRelativeUri(u2);
                return relativeUri.ToString().Replace('/', '\\');
            }
        }

        public string BaseOfRelativePath
        {
            get => baseOfRelativePath;
            set
            {
                SetProperty(ref baseOfRelativePath, value);
                DisplayingRelativePath = displayingRelativePath;
            }
        }

        public bool DisplayingRelativePath
        {
            get => displayingRelativePath;
            set
            {
                SetProperty(ref displayingRelativePath, value);
                DisplayName = value ? RelativePath : FullPath;
            }
        }
    }
}