using ShowMeLove.Domain.Core.Contracts.Repositories;
using System;
using System.Diagnostics;

namespace ShowMeLove.Data.Fakes
{
    public class FakeLogger : ILogger
    {
        public void LogException(Exception ex, string message = "")
        {
            Debug.WriteLine($">>EXCEPTION: {message ?? string.Empty}\n\n{ex.Message}\n{ex.StackTrace.ToString()}");
        }
    }
}
