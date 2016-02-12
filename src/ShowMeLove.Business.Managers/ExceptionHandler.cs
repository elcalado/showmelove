using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;

namespace ShowMeLove.Business.Managers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }


        public void Run(Action unsafeAction)
        {
            try
            {
                unsafeAction.Invoke();
            }
            catch(Exception ex)
            {
                _logger.LogException(ex);
            }
        }


        public T Run<T>(Func<T> unsafeFunction)
        {
            try
            {
                return unsafeFunction.Invoke();
            }
            catch(Exception ex)
            {
                _logger.LogException(ex);

            }
            return default(T);
        }
    }
}
