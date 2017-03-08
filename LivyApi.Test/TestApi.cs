using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Elastacloud.LivyApi.Test
{
    // {"from":0,"total":2,"sessions":[{"id":34,"state":"running","appId":"application_1470388360412_0038","appInfo":{"driverLogUrl":"https://flightaware-dev.azurehdinsight.net/yarnui/10.0.0.4/node/containerlogs/container_1470388360412_0038_01_000001/spark","sparkUiUrl":"https://flightaware-dev.azurehdinsight.net/yarnui/hn/proxy/application_1470388360412_0038/"},"log":["16/08/24 16:34:17 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:18 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:19 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:20 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:21 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:22 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:23 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:24 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:25 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)","16/08/24 16:34:26 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)"]},{"id":21,"state":"running","appId":"application_1470388360412_0025","appInfo":{"driverLogUrl":"https://flightaware-dev.azurehdinsight.net/yarnui/10.0.0.4/node/containerlogs/container_1470388360412_0025_01_000001/spark","sparkUiUrl":"https://flightaware-dev.azurehdinsight.net/yarnui/hn/proxy/application_1470388360412_0025/"},"log":["16/08/24 16:34:16 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:17 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:18 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:19 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:20 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:21 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:22 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:23 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:24 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)","16/08/24 16:34:25 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)"]}]}
    
    public class TestApi
    {
        private const string ListResponse = "{\"from\":0,\"total\":2,\"sessions\":[{\"id\":34,\"state\":\"running\",\"appId\":\"application_1470388360412_0038\",\"appInfo\":{\"driverLogUrl\":\"https://flightaware-dev.azurehdinsight.net/yarnui/10.0.0.4/node/containerlogs/container_1470388360412_0038_01_000001/spark\",\"sparkUiUrl\":\"https://flightaware-dev.azurehdinsight.net/yarnui/hn/proxy/application_1470388360412_0038/\"},\"log\":[\"16/08/24 16:34:17 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:18 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:19 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:20 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:21 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:22 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:23 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:24 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:25 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\",\"16/08/24 16:34:26 INFO Client: Application report for application_1470388360412_0038 (state: RUNNING)\"]},{\"id\":21,\"state\":\"running\",\"appId\":\"application_1470388360412_0025\",\"appInfo\":{\"driverLogUrl\":\"https://flightaware-dev.azurehdinsight.net/yarnui/10.0.0.4/node/containerlogs/container_1470388360412_0025_01_000001/spark\",\"sparkUiUrl\":\"https://flightaware-dev.azurehdinsight.net/yarnui/hn/proxy/application_1470388360412_0025/\"},\"log\":[\"16/08/24 16:34:16 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:17 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:18 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:19 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:20 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:21 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:22 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:23 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:24 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\",\"16/08/24 16:34:25 INFO Client: Application report for application_1470388360412_0025 (state: RUNNING)\"]}]}";
        private const string ExecuteResponse = "{\"id\":123,\"state\":\"starting\",\"log\":[]}";
        private const string IsRunningResponse = "{\"id\":123,\"state\":\"success\",\"log\":[]}";

        [Fact]
        public async Task TestList()
        {
            var settings = new LivySettings("azurecoder", "M!crosoft123", "flightaware-dev");
            var api = new Mock<LivyRestApi>(settings);
            api.Protected()
                .Setup<Task<string>>("MakeRequest", ItExpr.IsAny<string>(), ItExpr.IsAny<string>(), ItExpr.IsAny<string>())
                .Returns(Task.FromResult(ListResponse));

            var applications = await api.Object.ListAsync();
            Assert.Equal(2, applications.Total);
        }

        [Fact]
        public async Task TestExecute()
        {
            var settings = new LivySettings("azurecoder", "M!crosoft123", "flightaware-dev");
            var sparkSettings = new LivyBatchRequest(null);
            var api = new Mock<LivyRestApi>(settings);
            api.Protected()
                .Setup<Task<string>>("MakeRequest", ItExpr.IsAny<string>(), ItExpr.IsAny<string>(), ItExpr.IsAny<string>())
                .Returns(Task.FromResult(ExecuteResponse));

            var application = await api.Object.ExecuteAsync(sparkSettings);
            Assert.Equal(123, application.SessionId);
            Assert.Equal(SparkJobState.Starting, application.State);
        }

        [Fact]
        public async Task TestIsRunning_Starting()
        {
            var settings = new LivySettings("azurecoder", "M!crosoft123", "flightaware-dev");
            var sparkSettings = new SparkSettings();
            var api = new Mock<LivyRestApi>(settings);
            api.Protected()
                .Setup<Task<string>>("MakeRequest", ItExpr.IsAny<string>(), ItExpr.IsAny<string>(), ItExpr.IsAny<string>())
                .Returns(Task.FromResult(ExecuteResponse));

            var isRunning = await api.Object.GetBatchStateAsync(123);;
            Assert.Equal(SparkJobState.Starting, isRunning.State);
        }

        [Fact]
        public async Task TestIsRunning_Running()
        {
            var settings = new LivySettings("azurecoder", "M!crosoft123", "flightaware-dev");
            var sparkSettings = new SparkSettings();
            var api = new Mock<LivyRestApi>(settings);
            api.Protected()
                .Setup<Task<string>>("MakeRequest", ItExpr.IsAny<string>(), ItExpr.IsAny<string>(), ItExpr.IsAny<string>())
                .Returns(Task.FromResult(IsRunningResponse));

            var isRunning = await api.Object.GetBatchStateAsync(123); ;
            Assert.Equal(SparkJobState.Success, isRunning.State);
        }

    }
}
