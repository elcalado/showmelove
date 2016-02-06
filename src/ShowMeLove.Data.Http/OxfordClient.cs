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
            // var fileStream = await Task.Run( ()  => File.OpenRead(@"C:\Users\pedias\OneDrive\Bilder\1997\Bilde05.jpg"));

            var faceSomething = await _faceServiceClient.DetectAsync(imageStream, false, true);
            var emotions      = await _emotionServiceClient.RecognizeAsync(imageStream);

            return ConvertEmotionsToSentimentResults(emotions);
        }


        private static IEnumerable<SentimentResult> ConvertEmotionsToSentimentResults(Emotion[] emotions)
        {
            foreach (var emotion in emotions)
            {
                yield return new SentimentResult
                {
                    Anger     = (decimal)emotion.Scores.Anger,
                    Contempt  = (decimal)emotion.Scores.Contempt,
                    Disgust   = (decimal)emotion.Scores.Disgust,
                    Fear      = (decimal)emotion.Scores.Fear,
                    Happiness = (decimal)emotion.Scores.Happiness,
                    Neutral   = (decimal)emotion.Scores.Neutral,
                    Sadness   = (decimal)emotion.Scores.Sadness,
                    Surprise  = (decimal)emotion.Scores.Surprise,
                };
            }
        }
    }
}
