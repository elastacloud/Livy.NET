using System.Threading.Tasks;
using Elastacloud.LivyApi.AppList;

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
      /// Executes a Livy job using a storage jar
      /// </summary>
      Task<LivyBatchResponse> ExecuteAsync(LivyBatchRequest batch);
      /// <summary>
      /// Checks to see whether a job is running
      /// </summary>
      Task<SparkJobState> GetJobState(int id);
   }
}
