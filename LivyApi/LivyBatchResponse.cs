using Newtonsoft.Json;

namespace Elastacloud.LivyApi
{
   public class LivyBatchResponse
   {
      [JsonProperty("id")]
      public int SessionId { get; set; }

      [JsonProperty("appId")]
      public string ApplicationId { get; set; }

      //appInfo (key-value)
      //public ? ApplicationInfo { get;set; }

      //
      //public string[] LogLines { get; set; }

      /// <summary>
      /// The batch state
      /// </summary>
      public SparkJobState State { get; set; }

   }
}