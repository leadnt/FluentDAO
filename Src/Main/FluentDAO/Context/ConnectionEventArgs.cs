using System;
using System.Data;

namespace FluentDAO
{
	public class ConnectionEventArgs : EventArgs
	{
		public IDbConnection Connection { get; private set; }

		public ConnectionEventArgs(System.Data.IDbConnection connection)
		{
			Connection = connection;
		}
	}
}
