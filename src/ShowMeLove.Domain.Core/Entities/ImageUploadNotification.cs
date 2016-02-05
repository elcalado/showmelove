using System.Runtime.Serialization;

namespace ShowMeLove.Domain.Core.Entities
{
    [DataContract]
    public class ImageUploadNotification
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string BlobUrl { get; set; }
    }
}
