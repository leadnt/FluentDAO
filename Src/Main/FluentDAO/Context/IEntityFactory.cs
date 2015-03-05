using System;

namespace FluentDAO
{
	public interface IEntityFactory
	{
		object Create(Type type);
	}
}
