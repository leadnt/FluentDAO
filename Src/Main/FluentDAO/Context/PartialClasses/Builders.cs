﻿using System.Dynamic;

namespace LeadNT.FluentDAO
{
	public partial class DbContext
	{
		public ISelectBuilder<TEntity> Select<TEntity>(string sql)
		{
			return new SelectBuilder<TEntity>(CreateCommand).Select(sql);
		}

		public IInsertBuilder Insert(string tableName)
		{
			return new InsertBuilder(CreateCommand, tableName);
		}

		public IInsertBuilder<T> Insert<T>(string tableName, T item)
		{
			return new InsertBuilder<T>(CreateCommand, tableName, item);
		}

		public IInsertBuilderDynamic Insert(string tableName, ExpandoObject item)
		{
			return new InsertBuilderDynamic(CreateCommand, tableName, item);
		}

		public IUpdateBuilder Update(string tableName)
		{
			return new UpdateBuilder(Data.FluentDAOProvider, CreateCommand, tableName);
		}

		public IUpdateBuilder<T> Update<T>(string tableName, T item)
		{
			return new UpdateBuilder<T>(Data.FluentDAOProvider, CreateCommand, tableName, item);
		}

		public IUpdateBuilderDynamic Update(string tableName, ExpandoObject item)
		{
			return new UpdateBuilderDynamic(Data.FluentDAOProvider, CreateCommand, tableName, item);
		}

		public IDeleteBuilder Delete(string tableName)
		{
			return new DeleteBuilder(CreateCommand, tableName);
		}

		public IDeleteBuilder<T> Delete<T>(string tableName, T item)
		{
			return new DeleteBuilder<T>(CreateCommand, tableName, item);
		}

		private void VerifyStoredProcedureSupport()
		{
			if (!Data.FluentDAOProvider.SupportsStoredProcedures)
				throw new FluentDAOException("The selected database does not support stored procedures.");
		}

		public IStoredProcedureBuilder StoredProcedure(string storedProcedureName)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilder(CreateCommand, storedProcedureName);
		}

		public IStoredProcedureBuilder<T> StoredProcedure<T>(string storedProcedureName, T item)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilder<T>(CreateCommand, storedProcedureName, item);
		}

		public IStoredProcedureBuilderDynamic StoredProcedure(string storedProcedureName, ExpandoObject item)
		{
			VerifyStoredProcedureSupport();
			return new StoredProcedureBuilderDynamic(CreateCommand, storedProcedureName, item);
		}
	}
}
