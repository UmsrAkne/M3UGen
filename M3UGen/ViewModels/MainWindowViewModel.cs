using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Prism.Mvvm;

namespace M3UGen.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private ObservableCollection<FileInfo> files = new ObservableCollection<FileInfo>();

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<FileInfo> Files
        {
            get => files;
            set => SetProperty(ref files, value);
        }

        public void AddFiles(IEnumerable<FileInfo> fileInfoList)
        {
            foreach (FileInfo f in fileInfoList)
            {
                Files.Add(f);
            }
        }
    }
}