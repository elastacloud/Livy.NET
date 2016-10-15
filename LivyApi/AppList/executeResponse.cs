using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastacloud.LivyApi.AppList
{
    public class executeResponse
    {
        // {"id":0,"state":"starting","log":[]}* 
        public int id { get; set; }
        public string state { get; set; }
        public string[] log { get; set; }

        public SparkJobState JobState
        {
            get
            {
                SparkJobState jobState;
                switch (state)
                {
                    case "running":
                        jobState = SparkJobState.Running;
                        break;
                    case "failed":
                        jobState = SparkJobState.Failed;
                        break;
                    case "starting":
                        jobState = SparkJobState.Starting;
                        break;
                    default:
                        jobState = SparkJobState.Success;
                        break;
                }
                return jobState;
            }
        }
    }
}
