using U4.MessageHub.ConnectorSdk;

namespace MessageHub.Functions.Extensions.Trigger
{
    public class MessageHubTriggerContext
    {

        public MessageHubTriggerAttribute TriggerAttribute;

        public IConnectorFactory ConnectorFactory;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="factory"></param>
        public MessageHubTriggerContext(MessageHubTriggerAttribute attribute, IConnectorFactory factory)
        {
            TriggerAttribute = attribute;
            ConnectorFactory = factory;
            //Factory = new ConnectorFactory(TriggerAttribute.AccessProviderUrl,
            //    TriggerAttribute.IdsAuthority, TriggerAttribute.ClientId, TriggerAttribute.ClientSecret);
        }
    }
}
