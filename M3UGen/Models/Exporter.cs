using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace M3UGen.Models
{
    public class Exporter : BindableBase
    {
        private bool containsCommentLine = true;

        public bool ContainsCommentLine
        {
            get => containsCommentLine;
            set => SetProperty(ref containsCommentLine, value);
        }

        public string Export(IEnumerable<ExtendFileInfo> extendFileInfos)
        {
            var sb = new StringBuilder();
            foreach (ExtendFileInfo f in extendFileInfos)
            {
                if (!ContainsCommentLine && f.IsCommentOut)
                {
                    continue;
                }

                sb.AppendLine($"\"{f.DisplayName}\"");
            }

            return sb.ToString();
        }
    }
}