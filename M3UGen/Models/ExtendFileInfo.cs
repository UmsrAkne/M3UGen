using System;
using System.IO;
using Prism.Mvvm;

namespace M3UGen.Models
{
    public class ExtendFileInfo : BindableBase
    {
        private string baseOfRelativePath;

        public ExtendFileInfo(FileInfo fi)
        {
            FileInfo = fi;
        }

        private FileInfo FileInfo { get; set; }

        public string FullPath => FileInfo.FullName;

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
                RaisePropertyChanged(nameof(RelativePath));
            }
        }
    }
}