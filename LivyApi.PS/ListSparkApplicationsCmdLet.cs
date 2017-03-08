using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Elastacloud.LivyApi.AppList;

namespace Elastacloud.LivyApi.PS
{
    [Cmdlet(VerbsCommunications.Read, "SparkApplications")]
    public class ListSparkApplicationsCmdLet : Cmdlet
    {
        private LivyRestApi _api;
        private LivyBatchListResponse _appList;
         
        [Parameter(Mandatory = true)]
        public string ClusterName { get; set; }

        [Parameter(Mandatory = true)]
        public string Username { get; set; }

        [Parameter(Mandatory = true)]
        public string Password { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject($"Connecting to HDInsight cluster {ClusterName}");
            _api = new LivyRestApi(new LivySettings(Username, Password, ClusterName));
        }

        protected override void ProcessRecord()
        {
            var colour = Console.ForegroundColor;
            _appList = _api.ListAsync().Result;
            var sessions = _appList.Sessions;
            foreach (var session in sessions)
            {
                if (session.State != SparkJobState.Running && session.State != SparkJobState.Starting) continue;
                Console.ForegroundColor = ConsoleColor.Cyan;
                WriteObject($"{session.ApplicationId} is in a {session.State} state");
                Console.ForegroundColor = colour;
            }
        }

        protected override void EndProcessing()
        {
            WriteObject($"{_appList.Total} apps running on the Spark cluster");
        }
    }
}
