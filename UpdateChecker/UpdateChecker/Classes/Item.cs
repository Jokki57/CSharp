using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UpdateChecker.Classes
{
    internal abstract class Item
    {
		internal abstract string Path { get; set; }
		internal abstract int Link { get; set; }
		internal abstract TextBox LinkTextBox { get; }
		internal abstract TextBox PathTextBox { get; }

		internal abstract void UnsubscribeFromEvents();
    }
}
