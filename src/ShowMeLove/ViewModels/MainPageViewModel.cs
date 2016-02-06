using ShowMeLove.Domain.Core.Contracts.Managers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace ShowMeLove.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private readonly IImageManager _imageManager;

        // Visible assets, bindables
        private string _userName;
        private string _timeLeft;
        private BitmapImage _lastImage;
        private RelayCommand _pauseCommand;

        public MainPageViewModel(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }


        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                    return;

                _userName = value;
                ShoutAbout("UserName");
            }
        }


        public string TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                if (_timeLeft == value)
                    return;

                _timeLeft = value;
                ShoutAbout("TimeLeft");
            }
        }


        public BitmapImage LastImage
        {
            get { return _lastImage; }
            set
            {
                if (_lastImage == value)
                    return;

                _lastImage = value;
                ShoutAbout("LastImage");
            }
        }

        public ICommand PauseCommand
        {
            get
            {
                if (_pauseCommand == null)
                    _pauseCommand = new RelayCommand(DoPause);

                return _pauseCommand;
            }
        }


        private void DoPause()
        {
            throw new NotImplementedException();
        }


        public async Task InitializeAsync()
        {
            var result = await _imageManager.InitializeAsync();

            if (!result)
                throw new InvalidProgramException("Failed to initialize. oh crap!");
        }
    }
}