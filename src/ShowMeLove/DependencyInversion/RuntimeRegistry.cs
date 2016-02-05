using ShowMeLove.Business.Managers;
using ShowMeLove.Data.Identity;
using StructureMap;

namespace ShowMeLove.DependencyInversion
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            Scan(x =>
            {

                // From the business layer
                x.Assembly("ShowMeLove.Business.Managers");

                // From the data layer
                x.Assembly("ShowMeLove.Data.AzureStorage");
                x.Assembly("ShowMeLove.Data.EventHub");
                x.Assembly("ShowMeLove.Data.Identity");
                x.Assembly("ShowMeLove.Data.Fakes");

                x.WithDefaultConventions();
            });
        }
    }
}
