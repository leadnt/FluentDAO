﻿using System.Collections.Generic;
using System.Dynamic;

namespace FluentDAO
{
	internal class StoredProcedureBuilderDynamic : BaseStoredProcedureBuilder, IStoredProcedureBuilderDynamic
	{
		internal StoredProcedureBuilderDynamic(IDbCommand command, string name, ExpandoObject item)
			: base(command, name)
		{
			Data.Item = (IDictionary<string, object>) item;
		}

		public IStoredProcedureBuilderDynamic Parameter(string name, object value, DataTypes parameterType, int size)
		{
			Actions.ColumnValueAction(name, value, parameterType, size);
			return this;
		}

		public IStoredProcedureBuilderDynamic AutoMap(params string[] ignoreProperties)
		{
			Actions.AutoMapDynamicTypeColumnsAction(ignoreProperties);
			return this;
		}

		public IStoredProcedureBuilderDynamic ParameterOut(string name, DataTypes parameterType, int size = 0)
		{
			Actions.ParameterOutputAction(name, parameterType, size);
			return this;
		}

		public IStoredProcedureBuilderDynamic UseMultiResult(bool useMultipleResultsets)
		{
			Data.Command.UseMultiResult(useMultipleResultsets);
			return this;
		}
	}
}
