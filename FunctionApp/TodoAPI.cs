using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using FunctionApp.Model;
using FunctionApp.Model.ViewModel;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace FunctionApp
{
    public class TodoAPI
    {
        private readonly IConfiguration configuration;
        public TodoAPI(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        static List<TodoItem> items = new List<TodoItem>();

        [FunctionName("ListItems")]
        public  async Task<IActionResult> ListItems(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "items")] HttpRequest req,
            ILogger log)
        {
           
            return new OkObjectResult(items.Where(x => x.IsCompleted == false));
        }

        [FunctionName("CreateItem")]
        public static async Task<IActionResult> CreateItem(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "items")] HttpRequest req,
    ILogger log)
        {
            
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreateRequest>(reqBody);

            TodoItem newItem = new TodoItem(input.Description);
            newItem.Id = Guid.NewGuid().ToString();
            newItem.IsCompleted = false;

            items.Add(newItem);
            return new OkObjectResult(newItem);
        }

        [FunctionName("CompleteItem")]
        public static async Task<IActionResult> CompleteItem(
[HttpTrigger(AuthorizationLevel.Function, "put", Route = "items/{id}")] HttpRequest req,
ILogger log, string id)
        {
            var currentItem = items.Where(x => x.Id == id).FirstOrDefault();
            if(currentItem == null)
            {
                return new NotFoundObjectResult($"Item not present with id {id}");
            }
            currentItem.IsCompleted = true;
            return new OkObjectResult(currentItem);
        }
    }
}
