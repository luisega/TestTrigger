using Microsoft.Azure.WebJobs.Host.Triggers;
using System.Reflection;
using U4.MessageHub.ConnectorSdk;

namespace MessageHub.Functions.Extensions.Trigger
{
    public class MessageHubTriggerBindingProvider : ITriggerBindingProvider
    {
        private MessageHubExtensionConfigProvider _provider;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="provider"></param>
        public MessageHubTriggerBindingProvider(MessageHubExtensionConfigProvider provider)
        {
            _provider = provider;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            var parameter = context.Parameter;
            var attribute = parameter.GetCustomAttribute<MessageHubTriggerAttribute>(false);

            if (attribute == null)
            {
                return Task.FromResult<ITriggerBinding>(null);
            }

            if (parameter.ParameterType == typeof(IU4Message))
            {
                return Task.FromResult<ITriggerBinding>(new MessageHubTriggerBinding(_provider.CreateContext(attribute)));
            }

            throw new NotImplementedException();
        }
    }
}
