using System;

namespace LeadNT.FluentDAO
{
	public interface IInsertBuilder : IExecute, IExecuteReturnLastId
	{
		BuilderData Data { get; }
		IInsertBuilder Column(string columnName, object value, DataTypes parameterType = DataTypes.Object, int size = 0);
		IInsertBuilder Fill(Action<IInsertUpdateBuilder> fillMethod);
	}
}