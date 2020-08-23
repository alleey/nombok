namespace Nombok.Shared
{
    public interface IFactory<TInterface, TOptions>
    {
        TInterface Create(TOptions options);
    }
}