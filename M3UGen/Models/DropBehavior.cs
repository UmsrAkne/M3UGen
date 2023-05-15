using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using M3UGen.ViewModels;
using Microsoft.Xaml.Behaviors;

namespace M3UGen.Models
{
    public class DropBehavior : Behavior<Window>
    {

        public bool IsSoundFile(string path)
        {
            return
                Path.GetExtension(path).ToLower() == ".mp3" ||
                Path.GetExtension(path).ToLower() == ".wav" ||
                Path.GetExtension(path).ToLower() == ".ogg";
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            // ファイルをドラッグしてきて、コントロール上に乗せた際の処理
            AssociatedObject.PreviewDragOver += AssociatedObject_PreviewDragOver;

            // ファイルをドロップした際の処理
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewDragOver -= AssociatedObject_PreviewDragOver;
            AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            // ファイルパスの一覧の配列
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null)
            {
                return;
            }

            var fileInfos = new List<FileInfo>();

            foreach (string p in files)
            {
                if (string.IsNullOrWhiteSpace(p))
                {
                    continue;
                }

                if (Directory.Exists(p))
                {
                    fileInfos.AddRange(Directory.GetFiles(p).Select(fp => new FileInfo(fp)));
                }
                else
                {
                    fileInfos.Add(new FileInfo(p));
                }
            }

            if (sender is Window window)
            {
                ((MainWindowViewModel)window.DataContext).AddFiles(fileInfos);
            }
        }

        private void AssociatedObject_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
        }
    }
}