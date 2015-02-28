using System;

namespace LeadNT.FluentDAO
{
	public class FluentDAOException : Exception
	{
		public FluentDAOException(string message)
			: base(message)
		{
		}
		public FluentDAOException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
