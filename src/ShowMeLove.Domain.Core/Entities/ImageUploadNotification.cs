using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace ShowMeLove.Domain.Core.Entities
{
    [DataContract]
    public class ImageUploadNotification
    {
        [DataMember]
        [JsonProperty("attendeeId")]
        public string UserId { get; set; }

        [DataMember]
        [JsonProperty("blobUrl")]
        public string BlobUrl { get; set; }

        [DataMember]
        [JsonProperty("speakerId")]
        public int SpeakerId { get; set; }

        [DataMember]
        [JsonProperty("sessionId")]
        public int SessionId { get; set; }

        [DataMember]
        [JsonProperty("anger")]
        public decimal Anger { get; set; }

        [DataMember]
        [JsonProperty("contempt")]
        public decimal Contempt { get; set; }

        [DataMember]
        [JsonProperty("disgust")]
        public decimal Disgust { get; set; }

        [DataMember]
        [JsonProperty("fear")]
        public decimal Fear { get; set; }

        [DataMember]
        [JsonProperty("happiness")]
        public decimal Happiness { get; set; }

        [DataMember]
        [JsonProperty("neutral")]
        public decimal Neutral { get; set; }

        [DataMember]
        [JsonProperty("sadness")]
        public decimal Sadness { get; set; }

        [DataMember]
        [JsonProperty("surprise")]
        public decimal Surprise { get; set; }

        [DataMember]
        [JsonProperty("moment")]
        public DateTime Moment { get; set; }

        [DataMember]
        [JsonProperty("age")]
        public int Age { get; set; }

        [DataMember]
        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}
