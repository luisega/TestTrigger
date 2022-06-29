using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using U4.MessageHub.ConnectorSdk;

namespace MessageHub.Functions.Extensions.Trigger
{
    /// <summary>
    /// Class for initializing the Message Hub listener and binding trigger data to it.
    /// </summary>
    public class MessageHubTriggerBinding : ITriggerBinding
    {
        private readonly MessageHubTriggerContext _context;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="context">Message Hub trigger context.</param>
        public MessageHubTriggerBinding(MessageHubTriggerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Value type for Message Hub trigger.
        /// </summary>
        public Type TriggerValueType => typeof(IU4Message);

        /// <summary>
        /// Binding data contract for the trigger.
        /// </summary>
        public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>();

        /// <summary>
        /// Binds an object value 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
        {
            return Task.FromResult<ITriggerData>(new TriggerData(new MessageHubValueBinder(value), new Dictionary<string, object>()));
        }

        /// <summary>
        /// Creates a Message Hub listener object.
        /// </summary>
        /// <param name="context">Listener factory context.</param>
        /// <returns><see cref="Task"/> containing a <see cref="IListener"/> object.</returns>
        public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        {
            return Task.FromResult<IListener>(new MessageHubListener(context.Executor, _context));
        }

        /// <summary>
        /// Gets the parameter binding description.
        /// </summary>
        /// <returns>A <see cref="ParameterDescriptor"/> containing the binding description.</returns>
        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = "MessageHub",
                DisplayHints = new ParameterDisplayHints
                {
                    Prompt = "MessageHub",
                    Description = "Message Hub message trigger"
                }
            };
        }
    }
}
