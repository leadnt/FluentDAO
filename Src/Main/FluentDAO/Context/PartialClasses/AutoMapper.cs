using System;

namespace LeadNT.FluentDAO
{
	public partial class DbContext
	{
		public IDbContext IgnoreIfAutoMapFails(bool ignoreIfAutoMapFails)
		{
			Data.IgnoreIfAutoMapFails = true;
			return this;
		}
	}
}
