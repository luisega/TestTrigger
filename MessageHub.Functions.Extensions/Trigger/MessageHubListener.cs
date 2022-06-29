using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using U4.MessageHub.ConnectorSdk;

namespace MessageHub.Functions.Extensions.Trigger
{
    /// <summary>
    /// Class containing the logic for receiving Message Hub messages.
    /// </summary>
    public class MessageHubListener : IListener
    {
        private readonly ITriggeredFunctionExecutor _executor;
        private readonly MessageHubTriggerContext _context;

        private IU4Receiver _receiver;

        public MessageHubListener(ITriggeredFunctionExecutor executor, MessageHubTriggerContext context)
        {
            _executor = executor;
            _context = context;
        }

        public void Cancel()
        {
            if (_context == null || _context.ConnectorFactory == null || _receiver == null)
            {
                return;
            }

            _receiver.StopReceivingAsync().Wait();
            _receiver.CloseConnectionAsync().Wait();
        }

        public void Dispose()
        {
            _receiver = null;
            _context.ConnectorFactory = null;
        }

        /// <summary>
        /// Starts the listener, connecting to the provided Tenant Subscription Event Type.
        /// Every time a message is received, callback is executed and dunction triggered.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                IU4MessagePumpConfiguration messagePump = new U4MessagePumpConfiguration(async (receivedMessage) =>
                {
                    TriggeredFunctionData data = new TriggeredFunctionData
                    {
                        TriggerValue = receivedMessage
                    };
                    await _executor.TryExecuteAsync(data, CancellationToken.None);
                }, (exception) =>
                {
                    Console.WriteLine($"Exception! {exception}");
                    return null;
                });

                _receiver = _context.ConnectorFactory.GetReceiver(_context.TriggerAttribute., "testtrigger", "a2161ca7-7b09-4eaa-9f42-f36762c3f771", messagePump);

                ////return _context.Client.Subscribe(_context.TriggerAttribute.Channel, stream => stream.Subscribe(msg => {
                //var receiver = _context.ConnectorFactory.GetReceiver("testluisega", "testluisega", "testluisega", messagePump);
                //receiver.StartReceiving();

                //return Task.CompletedTask;
                _receiver.StartReceiving();
            });
        }

        /// <summary>
        /// Stops current listening operation.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _receiver.StopReceivingAsync();
        }
    }
}
