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
    internal class DestinationItem : Item
    {
        internal override string Path
        {
            get { return path; }
            set
            {
                path = value;
                pathTextBox.Text = value;
            }
        }
        internal override int Link { get; set; }
        internal override TextBox PathTextBox { get { return pathTextBox; } }
        internal override TextBox LinkTextBox { get { return linkTextBox; } }

        private string path;
        private TextBox linkTextBox;
        private TextBox pathTextBox;

        internal event EventHandler LinkChanged;
        internal event EventHandler PathChanged;

        internal DestinationItem(string path, TextBox linkTextBox, TextBox pathTextBox)
        {
            this.linkTextBox = linkTextBox;
            this.pathTextBox = pathTextBox;
            this.linkTextBox.TextChanged += linkTextBox_TextChanged;
            this.pathTextBox.TextChanged += pathTextBox_TextChanged;
			this.linkTextBox.PreviewTextInput -= linkTextBox_PreviewTextInput;
            Path = path;
        }

		internal override void UnsubscribeFromEvents()
		{
			this.linkTextBox.TextChanged -= linkTextBox_TextChanged;
			this.pathTextBox.TextChanged -= pathTextBox_TextChanged;
			this.linkTextBox.PreviewTextInput -= linkTextBox_PreviewTextInput;
		}
		private void linkTextBox_PreviewTextInput(object sender, TextCompositionEventArgs args)
		{
			args.Handled = "0123456789".IndexOf(args.Text) == -1;
		}

        private void pathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathChanged != null)
            {
                PathChanged(this, new EventArgs());
            }
        }

        private void linkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LinkChanged != null)
            {
                LinkChanged(this, new EventArgs());
            }
        }
    }
}
