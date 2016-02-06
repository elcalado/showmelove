using ShowMeLove.Domain.Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace ShowMeLove.Domain.Core.Contracts.Repositories
{
    public interface IOxfordClient
    {
        Task<IEnumerable<ProfileResult>> GetProfileFromImageAsync(Stream imageStream);
        Task<IEnumerable<SentimentResult>> GetSentimentsFromImageAsync(Stream imageStream);
    }

}
