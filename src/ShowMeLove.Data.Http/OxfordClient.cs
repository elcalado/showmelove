using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.Domain.Core.Entities;
using System;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace ShowMeLove.Data.Http
{
    public class OxfordClient : IOxfordClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private const string OxfordClientSubscriptionKey = "5df0154542ef485abe98bd10347cecd0";
        private readonly EmotionServiceClient _emotionServiceClient;


        public OxfordClient()
        {
            _emotionServiceClient = new EmotionServiceClient(_httpClient, OxfordClientSubscriptionKey);
        }


        public async Task<IEnumerable<SentimentResult>> GetSentimentsFromImageAsync(Stream imageStream)
        {
            var emotions = await _emotionServiceClient.RecognizeAsync(imageStream);

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
