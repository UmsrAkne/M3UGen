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
        private bool isCommentOut;

        public ExtendFileInfo(FileInfo fi)
        {
            FileInfo = fi;
            DisplayName = FileInfo.FullName;
        }

        public string DisplayName
        {
            get => displayName;
            private set => SetProperty(ref displayName, value);
        }

        public string RelativePath
        {
            get
            {
                Uri u1 = new Uri(BaseOfRelativePath);
                Uri u2 = new Uri(FileInfo.FullName);

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
                DisplayName = value ? RelativePath : FileInfo.FullName;
            }
        }

        public bool IsCommentOut { get => isCommentOut; set => SetProperty(ref isCommentOut, value); }

        private FileInfo FileInfo { get; set; }
    }
}