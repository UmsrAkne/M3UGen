using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using M3UGen.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace M3UGen.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<ExtendFileInfo> files = new ObservableCollection<ExtendFileInfo>();
        private bool relativePathMode;
        private string baseDirectoryPath = new FileInfo("/").FullName;
        private string outputDestPath;

        public MainWindowViewModel()
        {
            RelativePathMode = true;
        }

        public bool RelativePathMode
        {
            get => relativePathMode;
            set
            {
                SetProperty(ref relativePathMode, value);

                foreach (ExtendFileInfo exf in Files)
                {
                    exf.DisplayingRelativePath = value;
                }
            }
        }

        public string BaseDirectoryPath
        {
            get => baseDirectoryPath;
            set
            {
                if (!value.EndsWith(@"\"))
                {
                    value += @"\";
                }

                SetProperty(ref baseDirectoryPath, value);

                foreach (ExtendFileInfo extendFileInfo in Files)
                {
                    extendFileInfo.BaseOfRelativePath = value;
                }
            }
        }

        public ObservableCollection<ExtendFileInfo> Files
        {
            get => files;
            set => SetProperty(ref files, value);
        }

        public Exporter Exporter { get; } = new Exporter();

        public DelegateCommand ExportToClipBoardCommand => new DelegateCommand(() =>
        {
            Clipboard.SetDataObject(Exporter.Export(Files.ToList()));
        });

        public DelegateCommand ExportToM3UFileCommand => new DelegateCommand(() =>
        {
            var directoryName = Files.FirstOrDefault()?.DirectoryName;
            using var sw = File.CreateText($@"{BaseDirectoryPath}\{directoryName}.m3u");
            sw.Write(Exporter.Export(Files.ToList()));
        });

        public DelegateCommand ResetListCommand => new DelegateCommand(() =>
        {
            Files.Clear();
        });

        public void AddFiles(IEnumerable<FileInfo> fileInfoList)
        {
            int index = 0;
            foreach (FileInfo f in fileInfoList)
            {
                Files.Add(new ExtendFileInfo(f)
                {
                    BaseOfRelativePath = BaseDirectoryPath,
                    DisplayingRelativePath = RelativePathMode,
                    Index = ++index,
                });
            }
        }
    }
}