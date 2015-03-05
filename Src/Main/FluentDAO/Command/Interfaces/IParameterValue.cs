namespace FluentDAO
{
    public interface IParameterValue
    {
        TParameterType ParameterValue<TParameterType>(string outputParameterName);        
    }
}