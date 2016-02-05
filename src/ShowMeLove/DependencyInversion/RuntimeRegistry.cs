using ShowMeLove.Business.Managers;
using ShowMeLove.Data.Identity;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using StructureMap;

namespace ShowMeLove.DependencyInversion
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            For<IImageCapture>().Use<ImageCapture>();

            Scan(x =>
            {

                // From the business layer
                x.Assembly("ShowMeLove.Business.Managers");

                // From the data layer
                x.Assembly("ShowMeLove.Data.AzureStorage");
                x.Assembly("ShowMyLove.Data.EventHub"); // yup, spelling error
                x.Assembly("ShowMeLove.Data.Identity");
                x.Assembly("ShowMeLove.Data.Fakes");

                x.WithDefaultConventions();
            });

        }
    }
}
