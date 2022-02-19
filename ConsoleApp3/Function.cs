using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webjobs
{
    public class Function
    {
        public static void ExecuteWebJob([QueueTrigger("test")] Order order, ILogger logger)
        {
            logger.LogInformation("Webjob Executed");
        }

        public static void TimerTrigger([TimerTrigger("0 */2 * * * *", RunOnStartup = true) ] TimerInfo info, ILogger logger)
        {
            Console.WriteLine($"Executed At {DateTime.Now}");
        }
    }


    public class Order
    {
        public string ResturantName { get; set; }
        public bool IsDelievered { get; set; }
    }
}
