using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManagementClient.Model
{
    internal class Response
    {
		private int error;

		public int Error
		{
			get { return error; }
			set { error = value; }
		}
		private string message = null!;

		public string Message
		{
			get { return message; }
			set { message = value; }
		}
	}
}
