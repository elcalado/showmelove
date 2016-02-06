using ShowMeLove.Domain.Core.Contracts.Managers;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;


namespace ShowMeLove.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private readonly IImageManager _imageManager;

        // Visible assets, bindables
        private string _userName;
        private string _timeLeft;
        private WriteableBitmap _lastImage;
        private RelayCommand _pauseCommand;
        private string _pauseButtonTitle;
        private bool _isRunning;

        public MainPageViewModel(IImageManager imageManager)
        {
            _imageManager = imageManager;
            _pauseButtonTitle = "Paused";
            _isRunning = false;
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


        public WriteableBitmap LastImage
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

        public string PauseButtonTitle
        {
            get { return _pauseButtonTitle;  }
            set
            {
                if (_pauseButtonTitle == value)
                    return;

                _pauseButtonTitle = value;
                ShoutAbout("PauseButtonTitle");
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


        private async void DoPause()
        {
            if(!_isRunning)
            {
                PauseButtonTitle = "Active";
                _isRunning = true;
                LastImage = await _imageManager.GetBitmapAsync();

                // Send it to Oxford
                var sentiments = await _imageManager.GetSentimentsAsync(LastImage);
            }
            else
            {
                _isRunning = false;
                PauseButtonTitle = "Paused";
            }
        }





        public async Task InitializeAsync()
        {
            var result = await _imageManager.InitializeAsync();


            //if (result == false)
            //    throw new InvalidProgramException("Failed to initialize. oh crap!");
        }
    }
}