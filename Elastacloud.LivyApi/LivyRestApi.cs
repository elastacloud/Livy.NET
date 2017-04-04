using System;
using System.Net;
using System.Threading.Tasks;
using Elastacloud.LivyApi.AppList;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace Elastacloud.LivyApi
{
   /// <summary>
   /// The cross-cutting concern library that will get data from livy for am HDInsight cluster
   /// </summary>
   public class LivyRestApi : ILivyApi
   {
      private readonly HttpClient _client;

      public LivyRestApi(LivySettings settings)
      {
         Settings = settings;
         _client = new HttpClient();
         _client.DefaultRequestHeaders.Accept.Clear();
         _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            (settings.Username + ":" + settings.Password).Base64Encode());

         _serviceUri = string.Format(ServiceUriTemplate, settings.ClusterName);
      }
  
      public LivyRestApi(NetworkCredential credential) : this(new LivySettings(credential.UserName, credential.Password, credential.Domain))
      {

      }

      public LivySettings Settings { get; private set; }

      private const string ServiceUriTemplate = "https://{0}.azurehdinsight.net/livy/";

      private readonly string _serviceUri;

      /// <summary>
      /// Lists all running jobs on a cluster
      /// </summary>
      public async Task<LivyBatchListResponse> ListAsync()
      {
         return await Get<LivyBatchListResponse>("batches");
      }
      /// <summary>
      /// Executes a Livy job using a storage jar
      /// </summary>
      public async Task<LivyBatchResponse> ExecuteAsync(LivyBatchRequest batch)
      {
         LivyBatchResponse response = await Post<LivyBatchRequest, LivyBatchResponse>("batches", batch);
         return response;
      }

      public async Task<LivyBatchResponse> ExecuteWorkflowAsync(LivyBatchRequest batch, TimeSpan waitTime)
      {
         LivyBatchResponse response = await Post<LivyBatchRequest, LivyBatchResponse>("batches", batch);

         DateTime startTime = DateTime.UtcNow;

         while (response.State != SparkJobState.Dead && response.State != SparkJobState.Error && response.State != SparkJobState.Success)
         {
            TimeSpan length = DateTime.UtcNow - startTime;

            Debug.WriteLine("session id: {0}, state: {1}, length: {2}", response.SessionId, response.State, length);

            if (length > waitTime)
            {
               throw new TimeoutException("the workflow has timed out");
            }

            await Task.Delay(TimeSpan.FromSeconds(10));

            response = await GetBatchStateAsync(response.SessionId);
         }

         return response;
      }

      /// <summary>
      /// Checks to see whether a job is running
      /// </summary>
      public async Task<LivyBatchResponse> GetBatchStateAsync(int id)
      {
         return await Get<LivyBatchResponse>("batches/" + id);
      }

      private async Task<TResponse> Get<TResponse>(string url)
         where TResponse: class, new()
      {
         HttpResponseMessage response = await _client.GetAsync(_serviceUri + url);
         response.EnsureSuccessStatusCode();

         string json = await response.Content.ReadAsStringAsync();
         return json.AsJsonObject<TResponse>();
      }

      private async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
      {
         HttpResponseMessage response = await _client.PostAsync(_serviceUri + url, new StringContent(request.ToJsonString()));
         response.EnsureSuccessStatusCode();

         string json = await response.Content.ReadAsStringAsync();
         return json.AsJsonObject<TResponse>();
      }

   }
}
