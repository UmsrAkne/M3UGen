using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using M3UGen.Models;
using Prism.Mvvm;

namespace M3UGen.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private ObservableCollection<ExtendFileInfo> files = new ObservableCollection<ExtendFileInfo>();

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<ExtendFileInfo> Files
        {
            get => files;
            set => SetProperty(ref files, value);
        }

        public void AddFiles(IEnumerable<FileInfo> fileInfoList)
        {
            foreach (FileInfo f in fileInfoList)
            {
                Files.Add(new ExtendFileInfo(f));
            }
        }
    }
}