using eShopAsp.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace eShopAsp.Infrastructure.Logging;

public class LoggingAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;
    public LoggingAdapter(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<T>();

    public void LogInformation(string messages, params object[] args)
        => _logger.LogInformation(messages, args);

    public void LogWarning(string messages, params object[] args)
        => _logger.LogError(messages, args);
}