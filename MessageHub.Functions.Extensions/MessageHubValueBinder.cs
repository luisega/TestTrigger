using Microsoft.Azure.WebJobs.Host.Bindings;

namespace MessageHub.Functions.Extensions
{
    public class MessageHubValueBinder : IValueBinder
    {
        private object _value;

        public MessageHubValueBinder(object value)
        {
            _value = value;
        }

        public Type Type => typeof(string);

        public Task<object> GetValueAsync()
        {
            return Task.FromResult(_value);
        }

        public Task SetValueAsync(object value, CancellationToken cancellationToken)
        {
            _value = value;
            return Task.CompletedTask;
        }

        public string ToInvokeString()
        {
            return _value?.ToString() ?? string.Empty;
        }
    }
}
