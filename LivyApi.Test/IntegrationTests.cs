using Elastacloud.LivyApi;
using Elastacloud.LivyApi.AppList;
using System.Threading.Tasks;
using Xunit;

namespace ProductFactory.LivyApi.Test
{
   public class IntegrationTests
   {
      private readonly ILivyApi _api;
      private readonly TestSettings _settings;

      public IntegrationTests()
      {
         _settings = new TestSettings();

         _api = new LivyRestApi(_settings.SparkCluster);
      }

      [Fact]
      public async Task I_can_submit_job_and_get_back_its_status()
      {
         LivyBatchResponse response = await _api.ExecuteAsync(LivyBatchRequest.FromJar(_settings.SparkJobFile, _settings.SparkJobClassName));

         Assert.NotNull(response.SessionId);

         LivyBatchResponse batch = await _api.GetBatchStateAsync(response.SessionId);
         Assert.Equal(SparkJobState.Starting, batch.State);
      }


      [Fact]
      public async Task Job_appears_in_the_list_after_executing()
      {
         //todo: submit a job

         LivyBatchListResponse apps = await _api.ListAsync();
      }
   }
}
