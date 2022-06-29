using MessageHub.Functions.Extensions.Trigger;
using Microsoft.Azure.WebJobs.Host.Config;
using U4.MessageHub.ConnectorSdk;

namespace MessageHub.Functions.Extensions
{
    public class MessageHubExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            // Add trigger first
            var triggerRule = context.AddBindingRule<MessageHubTriggerAttribute>();
            triggerRule.BindToTrigger(new MessageHubTriggerBindingProvider(this));
        }

        public MessageHubTriggerContext CreateContext(MessageHubTriggerAttribute attribute)
        {
            return new MessageHubTriggerContext(attribute,
                new ConnectorFactory(attribute.GetAccessProviderUrl(),
                    attribute.GetIdsAuthority(),
                    attribute.GetClientId(),
                    attribute.GetClientSecret()));
        }
    }
}
