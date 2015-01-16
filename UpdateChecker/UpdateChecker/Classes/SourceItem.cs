using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;

namespace UpdateChecker.Classes
{
    internal class SourceItem : Item
    {
        internal string Path { get { return path; } 
            set 
            { 
                path = value;
                watcher.Path = Utils.GetPathWithoutFileName(value);
                watcher.Filter = Utils.GetFileName(value);
                pathTextBox.Text = value;
            }
        }
        internal int Link { get; set; }
        internal TextBox PathTextBox { get { return pathTextBox; } }
        internal TextBox LinkTextBox { get { return linkTextBox; } }

        private string path;
        private TextBox linkTextBox;
        private TextBox pathTextBox;
        private FileSystemWatcher watcher;

        internal event EventHandler LinkChanged;
        internal event EventHandler PathChanged;
        internal event EventHandler NeedUpdate;

        internal SourceItem(string path, TextBox linkTextBox, TextBox pathTextBox)
        {
            this.linkTextBox = linkTextBox;
            this.pathTextBox = pathTextBox;
            this.linkTextBox.TextChanged += linkTextBox_TextChanged;
            this.pathTextBox.TextChanged += pathTextBox_TextChanged;
			this.linkTextBox.KeyDown += (object sender, KeyEventArgs args) =>
			{
				System.Windows.Forms.KeysConverter converter = new System.Windows.Forms.KeysConverter();
				if (!Char.IsDigit(converter.ConvertToString(args.Key)[0]))
				{
					args.Handled = true;
				}				
			};
			//this.pathTextBox.Key += 

            watcher = new FileSystemWatcher();
            Path = path;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += watcher_Changed;
        }

        private void watcher_Changed(object sender, FileSystemEventArgs args)
        {
            if (NeedUpdate != null)
            {
                NeedUpdate(this, new EventArgs());
            }
        }

        private void pathTextBox_TextChanged(object sender, TextChangedEventArgs args)
        {
            if (PathChanged != null)
            {
                PathChanged(this, new EventArgs());
            }
        }

        private void linkTextBox_TextChanged(object sender, TextChangedEventArgs args)
        {
            if (LinkChanged != null)
            {
                LinkChanged(this, new EventArgs());
            }
        }
    }
}
