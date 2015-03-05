using System;

namespace FluentDAO
{
	public class CommandEventArgs : EventArgs
	{
		public System.Data.IDbCommand Command { get; private set; }

		public CommandEventArgs(System.Data.IDbCommand command)
		{
			Command = command;
		}
	}
}
