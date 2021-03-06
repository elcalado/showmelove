﻿using ShowMeLove.Domain.Core.Contracts.Managers;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using System.Linq;
using ShowMeLove.Domain.Core.Entities;
using System.Collections.Generic;

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
        private List<SentimentResult> _sentiments;

        public MainPageViewModel(IImageManager imageManager)
        {
            _imageManager = imageManager;
            _pauseButtonTitle = "Paused";
            _isRunning = false;
            _imageManager.OnTimerTick += _imageManager_OnTimerTick;
        }

        private async void _imageManager_OnTimerTick(object sender, int e)
        {
            TimeLeft = e.ToString();

            if (e == 0)
            {
                await GetImageAndInvokeOxford();
            }
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
                await GetImageAndInvokeOxford();
            }
            else
            {
                _isRunning = false;
                PauseButtonTitle = "Paused";
            }
        }

        private async Task GetImageAndInvokeOxford()
        {
            LastImage = await _imageManager.GetBitmapAsync();

            // Send it to Oxford
            var sentiments = (await _imageManager.GetSentimentsAsync(LastImage)).ToList();
            var profile = (await _imageManager.GetProfileAsync(LastImage)).ToList();

            foreach (var sentiment in sentiments)
            {
                sentiment.Age = profile.First().Age;
                sentiment.Gender = profile.First().Gender;
            }

            // Put it on the event hub
            await _imageManager.TransmitSentimentsAsync(sentiments);
        }

        public List<SentimentResult> Sentiments
        {
            get { return _sentiments; }
            set
            {
                _sentiments = new List<SentimentResult>(value);
                ShoutAbout("Sentiments");
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