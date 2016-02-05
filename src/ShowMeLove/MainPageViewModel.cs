using ShowMeLove.Domain.Core.Contracts.Managers;
using System;
using System.Threading.Tasks;

namespace ShowMeLove
{
    internal class MainPageViewModel
    {
        private readonly IImageManager _imageManager;

        public MainPageViewModel(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }


        public async Task InitializeAsync()
        {
            var result = await _imageManager.InitializeAsync();

            if (!result)
                throw new InvalidProgramException("Failed to initialize. oh crap!");
        }
    }
}