using System;

namespace FluentDAO
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
