using System.Globalization;

namespace SmlApi.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
               "api/sessions/{0}", this.SessionId);
            }
            set { }
        }
    }
}