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
            Args = Args,
            ClassName = ClassName,
         }).Result;
         var state = executeResponse.State;
         while (state == SparkJobState.Running || state == SparkJobState.Starting)
         {
            state = _api.GetJobState(executeResponse.SessionId).Result;
            WriteObject($"Processing record with state {state}");
         }
      }

      protected override void EndProcessing()
      {
      }
   }
}
