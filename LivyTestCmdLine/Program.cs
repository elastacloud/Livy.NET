using Elastacloud.LivyApi;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;


namespace LivyTestCmdLine
{
   class Program
   {
      static async Task Main(string[] args)
      {
         Console.WriteLine("Getting build params ...");
         IConfiguration config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true, true)
          .Build();

         Console.WriteLine("Starting EMR test ...");
         Console.WriteLine("Getting batches ...");
         var livy = new LivyRestApi(config["Host"]);
         var batches = await livy.ListAsync();
         Console.WriteLine("Finished getting batches. There are {0}...", batches.Total);
         Console.WriteLine("Invoking async software ...");
         var response = await livy.ExecuteAsync(new LivyBatchRequest(config["File"])
         {
            ClassName = config["Class"],
            NumExecutors = 2,
            DriverCores = 2,
            ExecutorCores = 2
         });
         Console.WriteLine("Software async response is {0}", response.SessionId);
         Console.WriteLine("Invoking sync software ...");
         var responseSync = await livy.ExecuteWorkflowAsync(new LivyBatchRequest(config["File"])
         {
            ClassName = config["Class"],
            NumExecutors = 2,
            DriverCores = 2,
            ExecutorCores = 2
         }, TimeSpan.FromMinutes(1));
         Console.WriteLine("Software async response is {0}", response.SessionId);
         Console.WriteLine("Press ENTER to finish ...");
         Console.ReadLine();
      }
   }
}
