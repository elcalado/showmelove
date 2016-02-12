using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Threading.Tasks;

namespace ShowMeLove.Business.Managers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;


        public ExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }


        public async Task RunActionAsync(Func<Task> unsafeAction)
        {
            try
            {
                await unsafeAction.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }


        public async Task<T> RunFunctionAsync<T>(Func<Task<T>> unsafeFunction)
        {
            try
            {
                return await unsafeFunction.Invoke();
            }
            catch(Exception ex)
            {
                _logger.LogException(ex);
            }
            return default(T);
        }
    }
}
