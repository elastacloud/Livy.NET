using System;
using System.Management.Automation;
using Elastacloud.LivyApi.AppList;

namespace Elastacloud.LivyApi.PS
{
   [Cmdlet("Execute", "SparkApplication")]
   public class ExecuteSparkApplicationCmdLet : Cmdlet
   {
      private LivyRestApi _api;

      [Parameter(Mandatory = true)]
      public string ClusterName { get; set; }

      [Parameter(Mandatory = true)]
      public string Username { get; set; }

      [Parameter(Mandatory = true)]
      public string Password { get; set; }

      public string Args { get; set; }
      public string ClassName { get; set; }
      public string SparkJar { get; set; }

      protected override void BeginProcessing()
      {
         WriteObject($"Connecting to HDInsight cluster {ClusterName}");
         _api = new LivyRestApi(new LivySettings(Username, Password, ClusterName));
      }

      protected override void ProcessRecord()
      {
         var colour = Console.ForegroundColor;
         var executeResponse = _api.ExecuteAsync(new LivyBatchRequest(SparkJar)
         {
            Args = new string[] { Args },
            ClassName = ClassName,
         }).Result;
         var state = executeResponse.State;
         while (state == SparkJobState.Busy || state == SparkJobState.Starting)
         {
            var state2 = _api.GetBatchStateAsync(executeResponse.SessionId).Result;
            WriteObject($"Processing record with state {state2.State}");
         }
      }

      protected override void EndProcessing()
      {
      }
   }
}
