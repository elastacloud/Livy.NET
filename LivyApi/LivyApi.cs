using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Elastacloud.LivyApi.AppList;

namespace Elastacloud.LivyApi
{
    /// <summary>
    /// The cross-cutting concern library that will get data from livy for am HDInsight cluster
    /// </summary>
    public class LivyApi : ILivyApi
    {
        public LivyApi(LivySettings settings)
        {
            Settings = settings;
        }

        public LivySettings Settings { get; private set; }

        private const string BatchUri = "https://{0}.azurehdinsight.net/livy/batches";
        /// <summary>
        /// Lists all running jobs on a cluster
        /// </summary>
        public async Task<appList> List()
        {
            var response = await MakeRequest(BatchUri, "GET");
            return JsonConvert.DeserializeObject<appList>(response);
        }
        /// <summary>
        /// Executes a Livy job using a storage jar
        /// </summary>
        public async Task<executeResponse> Execute(SparkSettings settings)
        {
            var response = await MakeRequest(BatchUri, "POST", settings.ToString());
            return JsonConvert.DeserializeObject<executeResponse>(response);
        }
        /// <summary>
        /// Checks to see whether a job is running
        /// </summary>
        public async Task<SparkJobState> GetJobState(int id)
        {
            var response = await MakeRequest(BatchUri, "GET", Convert.ToString(id));
            var executeResponse = JsonConvert.DeserializeObject<executeResponse>(response);
            SparkJobState state;
            switch (executeResponse.state.ToLower())
            {
                case "starting":
                    state = SparkJobState.Starting;
                    break;
                case "running":
                    state = SparkJobState.Running;
                    break;
                case "success":
                    state = SparkJobState.Success;
                    break;
                case "failed":
                case "dead":
                default:
                    state = SparkJobState.Failed;
                    break;        
            }
            return state;
        }
        /// <summary>
        /// Builds the request to the Livy API
        /// </summary>
        protected virtual async Task<string> MakeRequest(string uri, string method, string body = null)
        {
            string sparkLivyUri = String.Format(uri, Settings.ClusterName);
            if (method == "GET" && body != null)
            {
                sparkLivyUri += $"/{body}";
            }
            var request = (HttpWebRequest)WebRequest.Create(sparkLivyUri);
            request.Headers.Add(HttpRequestHeader.Authorization, $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(Settings.Username + ":" + Settings.Password))}");
            request.Method = method;
            request.Accept = "application/json";
            if (method == "POST")
            {
                request.ContentType = "application/json";
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    await writer.WriteLineAsync(body);
                }
            }
            
            string jsonResponse = null;
            var response = await request.GetResponseAsync();
            var responseStream = response.GetResponseStream();

            using (var reader = new StreamReader(responseStream))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }
            
            return jsonResponse;
        }
    }
}
