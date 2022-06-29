using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using U4.MessageHub.ConnectorSdk;

namespace MessageHub.Functions.Extensions
{
    public static class MessageHubFunctionsBuilderExtensions
    {
        public static IWebJobsBuilder AddMessageHubExtension(this IWebJobsBuilder builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException();
            }

            builder.AddExtension<MessageHubExtensionConfigProvider>();
/*            builder.Services.AddSingleton<IConnectorFactory>(new ConnectorFactory("https://u4mh-sandbox-accessprovider.u4pp.com", "https://u4ids-sandbox.u4pp.com/identity", "triggerclient", "3b8d9260-36ac-4ada-9e06-42d3654fa151"))*/;

            return builder;
        }
    }
}
