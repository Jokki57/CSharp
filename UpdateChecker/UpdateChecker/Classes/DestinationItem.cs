using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;

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
        internal int Link { get; set; }
        internal TextBox PathTextBox { get { return pathTextBox; } }
        internal TextBox LinkTextBox { get { return linkTextBox; } }

        private string path;
        private TextBox linkTextBox;
        private TextBox pathTextBox;

        internal event EventHandler LinkChanged;
        internal event EventHandler PathChanged;
        internal event EventHandler NeedUpdate;

        internal DestinationItem(string path, TextBox linkTextBox, TextBox pathTextBox)
        {
            this.linkTextBox = linkTextBox;
            this.pathTextBox = pathTextBox;
            this.linkTextBox.TextChanged += linkTextBox_TextChanged;
            this.pathTextBox.TextChanged += pathTextBox_TextChanged;
            Path = path;
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
