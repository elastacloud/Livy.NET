using Newtonsoft.Json;

namespace Elastacloud.LivyApi
{
   public class LivyBatchRequest
   {
      public LivyBatchRequest(string file)
      {
         this.File = file;
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

      [JsonProperty("args")]
      public string Args { get; set; }
   }
}
