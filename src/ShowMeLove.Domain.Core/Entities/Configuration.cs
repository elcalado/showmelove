using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMeLove.Domain.Core.Entities
{
    class Configuration
    {
        public OxfordConfiguration Oxford{ get; set;}

        public EventHubConfiguration EventHub { get; set; }
    }

    public class EventHubConfiguration
    {
        public string ConnectionString { get; set; }

        public string PathName { get; set; }
    }


    public class OxfordConfiguration
    {
        public string ClientSubscriptionKey { get; set; }

        public string FaceRecognitionKey { get; set; }
    }
}
