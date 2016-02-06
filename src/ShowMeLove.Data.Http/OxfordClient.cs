using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.ProjectOxford.Face;

namespace ShowMeLove.Data.Http
{
    public class OxfordClient : IOxfordClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private const string OxfordClientSubscriptionKey = "5df0154542ef485abe98bd10347cecd0";
        private const string OxfordFaceRecognitionKey = "69e72f4404934e9c877f3e1643ba66a4";

        private readonly EmotionServiceClient _emotionServiceClient;
        private readonly FaceServiceClient _faceServiceClient;

        public OxfordClient()
        {
            _emotionServiceClient = new EmotionServiceClient(_httpClient, OxfordClientSubscriptionKey);
            _faceServiceClient    = new FaceServiceClient(OxfordFaceRecognitionKey);
        }


        public async Task<IEnumerable<SentimentResult>> GetSentimentsFromImageAsync(Stream imageStream)
        {
            var emotions      = await _emotionServiceClient.RecognizeAsync(imageStream);   
            return ConvertEmotionsToSentimentResults(emotions);
        }

        public async Task<IEnumerable<ProfileResult>> GetProfileFromImageAsync(Stream imageStream)
        {
            FaceAttributeType[] attributes = new FaceAttributeType[2];
            attributes[0] = FaceAttributeType.Age;
            attributes[1] = FaceAttributeType.Gender;
            var profile = await _faceServiceClient.DetectAsync(imageStream, true, false, attributes );
            return ConvertProfileToProfileResults(profile);
        }

        private static IEnumerable<ProfileResult> ConvertProfileToProfileResults(Microsoft.ProjectOxford.Face.Contract.Face[] faces)
        {
            foreach (var face in faces)
            {
                yield return new ProfileResult
                {
                    Age = (int)face.FaceAttributes.Age,
                    Gender = face.FaceAttributes.Gender
                };
            }
        }

        private static IEnumerable<SentimentResult> ConvertEmotionsToSentimentResults(Emotion[] emotions)
        {
            foreach (var emotion in emotions)
            {
                yield return new SentimentResult
                {
                    Anger = Math.Round((decimal)emotion.Scores.Anger * 1000,0),
                    Contempt = Math.Round((decimal)emotion.Scores.Contempt * 1000,0),
                    Disgust = Math.Round((decimal)emotion.Scores.Disgust * 1000,0),
                    Fear = Math.Round((decimal)emotion.Scores.Fear * 1000,0),
                    Happiness = Math.Round((decimal)emotion.Scores.Happiness * 1000,0),
                    Neutral = Math.Round((decimal)emotion.Scores.Neutral * 1000,0),
                    Sadness = Math.Round((decimal)emotion.Scores.Sadness * 1000,0),
                    Surprise = Math.Round((decimal)emotion.Scores.Surprise * 1000,0),
                    Moment = DateTime.Now,
                    Age = 22,
                    Gender = "M"
                };
            }
        }
    }
}
