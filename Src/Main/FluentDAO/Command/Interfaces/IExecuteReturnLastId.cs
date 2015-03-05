namespace FluentDAO
{
    public interface IExecuteReturnLastId
    {
        T ExecuteReturnLastId<T>(string identityColumnName = null);        
    }
}