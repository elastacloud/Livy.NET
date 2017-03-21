using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Elastacloud.LivyApi
{
   public class LivyBatchResponse
   {
      /// <summary>
      /// Session ID
      /// </summary>
      [JsonProperty("id")]
      public int SessionId { get; set; }

      /// <summary>
      /// Application ID
      /// </summary>
      [JsonProperty("appId")]
      public string ApplicationId { get; set; }

      //appInfo (key-value)
      //public ? ApplicationInfo { get;set; }

      /// <summary>
      /// Log lines if present
      /// </summary>
      [JsonProperty("log")]
      public string[] LogLines { get; set; }
      
      /// <summary>
      /// Get log joined in one string
      /// </summary>
      [JsonIgnore]
      public string Log
      {
         get
         {
            if (LogLines == null) return null;

            return string.Join(Environment.NewLine, LogLines);
         }
      }

      /// <summary>
      /// The batch state
      /// </summary>
      [JsonProperty("state")]
      public SparkJobState State { get; set; }

      [JsonProperty("appInfo")]
      public Dictionary<string, string> AppInfo { get; set; }

      public override string ToString()
      {
         return $"session: {SessionId}, state: {State}";
      }
   }
}