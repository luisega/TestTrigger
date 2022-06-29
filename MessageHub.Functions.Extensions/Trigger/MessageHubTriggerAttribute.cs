using Microsoft.Azure.WebJobs.Description;

namespace MessageHub.Functions.Extensions.Trigger
{
    /// <summary>
    /// Attribute class for Message Hub trigger, containing parameters and configuration values for it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class MessageHubTriggerAttribute : Attribute
    {
        //public string ReceivingSourceSystem { get; set; } = ""
        public string IdsAuthority { get; set; } = "IdsAuthority";
        public string AccessProviderUrl { get; set; } = "AccessProviderUrl";
        public string ClientId { get; set; } = "ClientId";
        public string ClientSecret { get; set; } = "ClientSecret";

        // <siummary>
        // Helper method for getting ClientId from environment variables
        // </summary>
        public string GetClientId()
        {
            return Environment.GetEnvironmentVariable(ClientId) ?? throw new ArgumentException("Client Id is not valid.");
        }

        // <siummary>
        // Helper method for getting ClientSecret from environment variables
        // </summary>
        public string GetClientSecret()
        {
            return Environment.GetEnvironmentVariable(ClientSecret) ?? throw new ArgumentException("Client Secret is not valid.");
        }

        // <siummary>
        // Helper method for getting ClientSecret from environment variables
        // </summary>
        public string GetIdsAuthority()
        {
            return Environment.GetEnvironmentVariable(IdsAuthority) ?? throw new ArgumentException("Ids Authority is not valid.");
        }

        // <siummary>
        // Helper method for getting ClientSecret from environment variables
        // </summary>
        public string GetAccessProviderUrl()
        {
            return Environment.GetEnvironmentVariable(AccessProviderUrl) ?? throw new ArgumentException("Access Provider URL is not valid.");
        }
    }
}