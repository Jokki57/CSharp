using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateChecker.Classes
{
    public static class Utils
    {
        public static string GetFileName(string fullPath)
        {
            return fullPath.Substring(fullPath.LastIndexOf('\\') + 1);
        }

        public static string GetPathWithoutFileName(string fullPath)
        {
            string fileName = fullPath.Substring(fullPath.LastIndexOf('\\') + 1);
            return fullPath.Substring(0, fullPath.Length - fileName.Length);
        }
    }
}
