using ShowMeLove.Data.Fakes;
using ShowMeLove.Domain.Core.Contracts.Repositories;
using ShowMeLove.ViewModels;
using StructureMap;

namespace ShowMeLove.DependencyInversion
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            // Alternates and non-default conventions
            For<ILogger>().Singleton().Use<FakeLogger>();

            // Scan all assemblies using default conventions, ie: interface IImageHandler is implemented by class ImageHandler when both are found in the assemblies
            Scan(x =>
            {

                // From the business layer
                x.Assembly("ShowMeLove.Business.Managers");

                // From the data layer
                x.Assembly("ShowMeLove.Data.AzureStorage");
                x.Assembly("ShowMyLove.Data.EventHub"); // yup, spelling error
                x.Assembly("ShowMeLove.Data.Identity");
                x.Assembly("ShowMeLove.Data.Http");
                x.Assembly("ShowMeLove.Data.file");
                x.Assembly("ShowMeLove.Data.Imaging");
                x.Assembly("ShowMeLove.Data.Fakes");

                x.WithDefaultConventions();
            });

            // Viewmodels
            AddType(typeof(MainPageViewModel), typeof(MainPageViewModel));
        }
    }
}
