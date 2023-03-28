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
        private string title = "Prism Application";

        private ObservableCollection<ExtendFileInfo> files = new ObservableCollection<ExtendFileInfo>();
        private bool relativePathMode;
        private string baseDirectoryPath = new FileInfo("/").FullName;

        public MainWindowViewModel()
        {
            RelativePathMode = true;
        }

        public string Title { get => title; set => SetProperty(ref title, value); }

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

        public DelegateCommand ResetListCommand => new DelegateCommand(() =>
        {
            Files.Clear();
        });

        public void AddFiles(IEnumerable<FileInfo> fileInfoList)
        {
            foreach (FileInfo f in fileInfoList)
            {
                Files.Add(new ExtendFileInfo(f)
                {
                    BaseOfRelativePath = BaseDirectoryPath,
                    DisplayingRelativePath = RelativePathMode,
                });
            }
        }
    }
}