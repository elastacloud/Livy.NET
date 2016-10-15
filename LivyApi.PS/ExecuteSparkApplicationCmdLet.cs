using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Elastacloud.LivyApi.AppList;

namespace Elastacloud.LivyApi.PS
{
    [Cmdlet("Execute", "SparkApplication")]
    public class ExecuteSparkApplicationCmdLet : Cmdlet
    {
        private LivyApi _api;
        private appList _appList;

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
            _api = new LivyApi(new LivySettings(Username, Password, ClusterName));
        }

        protected override void ProcessRecord()
        {
            var colour = Console.ForegroundColor;
            var executeResponse =_api.Execute(new SparkSettings()
            {
                args = Args,
                className = ClassName,
                file = SparkJar
            }).Result;
            var state = executeResponse.JobState;
            while (state == SparkJobState.Running || state == SparkJobState.Starting)
            {
                state =  _api.GetJobState(executeResponse.id).Result;
                WriteObject($"Processing record with state {state}");
            }
        }

        protected override void EndProcessing()
        {
            WriteObject($"{_appList.total} apps running on the Spark cluster");
        }
    }
}
