using Newtonsoft.Json;

namespace Elastacloud.LivyApi.AppList
{
   public class LivyBatchListResponse
   {
      [JsonProperty("from")]
      public int From { get; set; }

      [JsonProperty("total")]
      public int Total { get; set; }

      [JsonProperty("sessions")]
      public LivyBatchResponse[] Sessions { get; set; }
   }
}
