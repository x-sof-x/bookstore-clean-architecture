using Microsoft.Extensions.Caching.Memory;
using BookStore.Domain.Exceptions;
using BookStore.Application.Interfaces;
namespace BookStore.Infrastructure.Logging
{
    public class LogsService : ILogsService
    {
        private readonly IMemoryCache _cache;
        private const int MaxAllowedCalls = 3;

        public LogsService(IMemoryCache cache)
        {
            _cache = cache;
        }

        private void CheckCallCountAndThrow<TException>(string key, string message) where TException : Exception
        {
            int currentCount = _cache.GetOrCreate(key, entry => 0);
            currentCount++;
            _cache.Set(key, currentCount);

            if (currentCount > MaxAllowedCalls)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message)!;
            }
        }

        public bool TriggerInfo()
        {
            CheckCallCountAndThrow<InfoExceptions>("count_info", "Вичерпано 3 виклики: залоговано як Information");
            return true;
        }

        public bool TriggerDebug()
        {
            CheckCallCountAndThrow<DebugException>("count_debug", "Вичерпано 3 виклики: залоговано як Debug");
            return true;
        }

        public bool TriggerWarning()
        {
            CheckCallCountAndThrow<WarningException>("count_warning", "Вичерпано 3 виклики: залоговано як Warning");
            return true;
        }

        public bool TriggerError()
        {
            CheckCallCountAndThrow<CustomErrorException>("count_error", "Вичерпано 3 виклики: залоговано як Error");
            return true;
        }
    }
}
