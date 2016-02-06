using ShowMeLove.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.Domain.Core.Contracts.Managers
{
    public interface IImageManager
    {
        event EventHandler<int> OnTimerTick;

        Task<bool> InitializeAsync();

        Task<WriteableBitmap> GetBitmapAsync();

        Task<IEnumerable<SentimentResult>> GetSentimentsAsync(WriteableBitmap bitmap);

        Task TransmitSentimentsAsync(IEnumerable<SentimentResult> sentiments);
    }
}
