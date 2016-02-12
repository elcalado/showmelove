using System;

namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface ILogger
    {
        void LogException(Exception ex, string message = "");
    }
}
