using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace FunctionApp
{
    public class TimerTrigger
    {
        private readonly IConfiguration configuration;
        public TimerTrigger(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [FunctionName("TimerTrigger")]
        [return: Table()]
        public void Run([TimerTrigger("0 */1 * * * *", RunOnStartup = true)] TimerInfo myTimer, [Blob("test/demo.txt",
            System.IO.FileAccess.Read,
            Connection = "AzureWebJobsStorage")] Stream blob, ILogger log)
        {
            var blobContent = "Book My Show Movie Detils";
            TestEntity newEntity = new TestEntity()
            {
                blobContent = blobContent,
        };
            var config = this.configuration["AzureWebJobsStorage"];
            log.LogInformation($"{config}");
        }
    }

    public class TestEntity : TableEntity
    {
        public TestEntity()
        {
            PartitionKey = "TODO";
            RowKey = Guid.NewGuid().ToString();
        }

        public string blobContent { get; set; }
    }
}
