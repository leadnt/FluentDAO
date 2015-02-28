using System;

namespace LeadNT.FluentDAO
{
	internal partial class DbCommand
	{
		public T ExecuteReturnLastId<T>(string identityColumnName = null)
		{
			if (Data.Context.Data.FluentDAOProvider.RequiresIdentityColumn && string.IsNullOrEmpty(identityColumnName))
				throw new FluentDAOException("The identity column must be given");

			var value = Data.Context.Data.FluentDAOProvider.ExecuteReturnLastId<T>(this, identityColumnName);
			T lastId;

			if (value.GetType() == typeof(T))
				lastId = (T)value;
			else
				lastId = (T)Convert.ChangeType(value, typeof(T));

			return lastId;
		}
	}
}
