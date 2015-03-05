using System.Configuration;

namespace FluentDAO
{
	public partial class DbContext
	{
		public IDbContext ConnectionString(string connectionString, IDbProvider FluentDAOProvider, string providerName = null)
		{
			if(string.IsNullOrEmpty(providerName))
				providerName = FluentDAOProvider.ProviderName;
			var adoNetProvider = System.Data.Common.DbProviderFactories.GetFactory(providerName);
			return ConnectionString(connectionString, FluentDAOProvider, adoNetProvider);
		}

		public IDbContext ConnectionString(string connectionString, IDbProvider FluentDAOProvider, System.Data.Common.DbProviderFactory adoNetProviderFactory)
		{
			Data.ConnectionString = connectionString;
			Data.FluentDAOProvider = FluentDAOProvider;
			Data.AdoNetProvider = adoNetProviderFactory;
			return this;
		}

		public IDbContext ConnectionStringName(string connectionstringName, IDbProvider dbProvider)
		{
			var settings = ConfigurationManager.ConnectionStrings[connectionstringName];
			if(settings == null)
				throw new FluentDAOException("A connectionstring with the specified name was not found in the .config file");
			
			ConnectionString(settings.ConnectionString, dbProvider, settings.ProviderName);
			return this;
		}
	}
}
