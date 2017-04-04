using System.Threading.Tasks;
using Elastacloud.LivyApi.AppList;
using System;

namespace Elastacloud.LivyApi
{
   /// <summary>
   /// The cross-cutting concern library that will get data from livy for am HDInsight cluster
   /// </summary>
   public interface ILivyApi
   {
      /// <summary>
      /// Lists all running jobs on a cluster
      /// </summary>
      Task<LivyBatchListResponse> ListAsync();

      /// <summary>
      /// Executes a Livy job
      /// </summary>
      /// <param name="batch">Batch to execute</param>
      /// <param name="wait">When true the call will scan and wait until the task is complete</param>
      Task<LivyBatchResponse> ExecuteAsync(LivyBatchRequest batch);

      /// <summary>
      /// Executes a Livy job and waits for completion
      /// </summary>
      /// <param name="batch">Batch to execute</param>
      /// <param name="waitTime">Time to wait until the job times out</param>
      /// <returns></returns>
      Task<LivyBatchResponse> ExecuteWorkflowAsync(LivyBatchRequest batch, TimeSpan waitTime);

      /// <summary>
      /// Checks to see whether a job is running
      /// </summary>
      Task<LivyBatchResponse> GetBatchStateAsync(int sessionId);
   }
}
