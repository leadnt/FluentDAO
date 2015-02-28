using System;

namespace LeadNT.FluentDAO
{
	public interface IEntityFactory
	{
		object Create(Type type);
	}
}
