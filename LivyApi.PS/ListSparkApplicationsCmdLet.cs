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
        private LivyApi _api;
        private appList _appList;
         
        [Parameter(Mandatory = true)]
        public string ClusterName { get; set; }

        [Parameter(Mandatory = true)]
        public string Username { get; set; }

        [Parameter(Mandatory = true)]
        public string Password { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject($"Connecting to HDInsight cluster {ClusterName}");
            _api = new LivyApi(new LivySettings(Username, Password, ClusterName));
        }

        protected override void ProcessRecord()
        {
            var colour = Console.ForegroundColor;
            _appList = _api.List().Result;
            var sessions = _appList.sessions;
            foreach (var session in sessions)
            {
                if (session.state != "running" && session.state != "started") continue;
                Console.ForegroundColor = ConsoleColor.Cyan;
                WriteObject($"{session.appId} is in a {session.state} state");
                Console.ForegroundColor = colour;
            }
        }

        protected override void EndProcessing()
        {
            WriteObject($"{_appList.total} apps running on the Spark cluster");
        }
    }
}
