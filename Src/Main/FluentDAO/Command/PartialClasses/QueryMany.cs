﻿using System;
using System.Collections.Generic;
using System.Data;

namespace LeadNT.FluentDAO
{
	internal partial class DbCommand
	{
        public TList QueryMany<TEntity, TList>(Action<TEntity, IDataReader> customMapper = null)
            where TList : IList<TEntity>
		{
			var items = default(TList);

			Data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				items = new QueryHandler<TEntity>(Data).ExecuteMany<TList>(customMapper, null);
			});

			return items;
		}

	    public TList QueryMany<TEntity, TList>(Action<TEntity, dynamic> customMapper) where TList : IList<TEntity>
		{
			var items = default(TList);

			Data.ExecuteQueryHandler.ExecuteQuery(true, () =>
			{
				items = new QueryHandler<TEntity>(Data).ExecuteMany<TList>(null, customMapper);
			});

			return items;
	    }

	    public List<TEntity> QueryMany<TEntity>(Action<TEntity, IDataReader> customMapper)
		{
			return QueryMany<TEntity, List<TEntity>>(customMapper);
		}

		public List<TEntity> QueryMany<TEntity>(Action<TEntity, dynamic> customMapper)
		{
			return QueryMany<TEntity, List<TEntity>>(customMapper);
		}

		public DataTable QueryManyDataTable()
		{
			var dataTable = new DataTable();

			Data.ExecuteQueryHandler.ExecuteQuery(true, () => dataTable.Load(Data.Reader.InnerReader, LoadOption.OverwriteChanges));

			return dataTable;
		}
	}
}
