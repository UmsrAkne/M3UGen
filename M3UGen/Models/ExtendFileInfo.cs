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
                return string.Empty;
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