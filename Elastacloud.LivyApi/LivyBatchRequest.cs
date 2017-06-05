using Newtonsoft.Json;

namespace Elastacloud.LivyApi
{
   public class LivyBatchRequest
   {
      public LivyBatchRequest(string file)
      {
         this.File = file;
      }

      public static LivyBatchRequest FromJar(string file, string className)
      {
         return new LivyBatchRequest(file) { ClassName = className };
      }

      /// <summary>
      /// File path containing the application to execute.
      /// </summary>
      [JsonProperty("file")]
      public string File { get; private set; }

      /// <summary>
      /// Application Java/Spark main class
      /// </summary>
      [JsonProperty("className")]
      public string ClassName { get; set; }

      /// <summary>
      /// Optional job arguments to submit. This array corresponds to what you would type in the command line
      /// separated by spaces
      /// </summary>
      /// <example>
      /// Running a "spark.jar arg1=1 arg2 arg3 will result in json:
      /// <code>
      /// "args" : ["arg1=2", "arg2", "arg3"]
      /// </code>
      /// </example>
      [JsonProperty("args")]
      public string[] Args { get; set; }

      /// <summary>
      /// Amount of memory to use for the driver process
      /// </summary>
      [JsonProperty("driverMemory")]
      public string DriverMemory { get; set; }

      /// <summary>
      /// Number of cores to use for the driver process
      /// </summary>
      [JsonProperty("driverCores")]
      public int DriverCores { get; set; }

      /// <summary>
      /// Amount of memory to use per executor process
      /// </summary>
      [JsonProperty("executorMemory")]
      public string ExecutorMemory { get; set; }

      /// <summary>
      /// Number of cores to use for each executor
      /// </summary>
      [JsonProperty("executorCores")]
      public int ExecutorCores { get; set; }

      /// <summary>
      /// Number of executors to launch for this session
      /// </summary>
      [JsonProperty("numExecutors")]
      public int NumExecutors { get; set; }


   }
}
