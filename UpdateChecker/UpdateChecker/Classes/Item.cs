using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UpdateChecker.Classes
{
    interface Item
    {
        internal string Path { get; set; }
        internal string Link { get; set; }
        internal TextBlock linkTextBlock { get; }
        internal TextBlock pathTextBlock { get; }
    }
}
