using System;
using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IExceptionHandler
    {
        Task RunActionAsync(Func<Task> unsafeAction);

        Task<T> RunFunctionAsync<T>(Func<Task<T>> unsafeFunction);
    }
}
