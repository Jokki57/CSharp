using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateChecker.Classes
{
    class LinkAlreadyExistException : Exception
    {
		public override string Message
		{
			get
			{
				return "This link already exist in sources";
			}
		}
    }
}
