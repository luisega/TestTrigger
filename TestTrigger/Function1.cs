using MessageHub.Functions.Extensions.Trigger;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using U4.MessageHub.ConnectorSdk;

namespace TestTrigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run(
            [MessageHubTrigger()] IU4Message req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            await req.CompleteAsync();

            ////string name = req.Query["name"];

            ////string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";
        }
    }
}
