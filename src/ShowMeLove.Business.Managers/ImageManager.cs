﻿using System;
using ShowMeLove.Domain.Core.Contracts.Managers;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System.Threading.Tasks;

namespace ShowMeLove.Business.Managers
{
    public class ImageManager : IImageManager
    {
        private object idManager;
        private readonly IUserIdManager _userIdManager;
        private readonly IImageCapture _imageCapture;
        private readonly IImageRepository _imageRepository;
        private readonly IMessageTransmitter _messageTransmitter;


        public ImageManager(IUserIdManager userIdManager, IImageRepository imageRepository, IImageCapture imageCapture, IMessageTransmitter messageTransmitter)
        {
            _userIdManager      = userIdManager;
            _imageRepository    = imageRepository;
            _imageCapture       = imageCapture;
            _messageTransmitter = messageTransmitter;
        }


        public async Task<bool> UploadImageAsync()
        {
            // Grab ID of the user
            var userId = await _userIdManager.GetAsync();
            if (userId == null)
                return false;

            // Create BLOB name
            var blobName = userId.ToString() + Guid.NewGuid().ToString();

            // Capture image
            byte[] imageBytes = _imageCapture.CaptureJpegImage();

            // Upload the image
            var blobUrl = await _imageRepository.SaveAsync(imageBytes, blobName);

            var imageUploadNotification = new ImageUploadNotification{
                UserId = userId, 
                BlobUrl = blobUrl
            };

            // When the image is uploaded, send a message on the event hub with the ID and blob name
            await _messageTransmitter.TransmitImageSavedAsync(userId, blobName);

            return true;
        }
    }
}
