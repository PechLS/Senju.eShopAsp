namespace eShopAsp.Core.Interfaces;

public interface IAppLogger<T>
{
    void LogInformation(string messages, params object[] args);
    void LogWarning(string messages, params object[] args);
}