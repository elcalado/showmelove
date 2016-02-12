using System;
using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IExceptionHandler
    {
        void Run(Action unsafeAction);

        T Run<T>(Func<T> unsafeFunction);
    }
}
