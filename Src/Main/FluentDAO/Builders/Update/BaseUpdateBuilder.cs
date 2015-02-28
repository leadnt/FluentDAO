using System;

namespace LeadNT.FluentDAO
{
	internal abstract class BaseUpdateBuilder
	{
		public BuilderData Data { get; set; }
		protected ActionsHandler Actions { get; set; }

		public BaseUpdateBuilder(IDbProvider provider, IDbCommand command, string name)
		{
			Data =  new BuilderData(command, name);
			Actions = new ActionsHandler(Data);
		}

		public int Execute()
		{
			if (Data.Columns.Count == 0
					   || Data.Where.Count == 0)
				throw new FluentDAOException("Columns or where filter have not yet been added.");

			Data.Command.ClearSql.Sql(Data.Command.Data.Context.Data.FluentDAOProvider.GetSqlForUpdateBuilder(Data));
		
			return Data.Command.Execute();
		}
	}
}
