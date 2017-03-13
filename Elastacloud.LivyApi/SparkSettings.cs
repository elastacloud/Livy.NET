using Newtonsoft.Json;
using Elastacloud.LivyApi.AppList;

namespace Elastacloud.LivyApi
{
    /// <summary>
    /// Contains the settings needed to execute a Livy job
    /// </summary>
    public class SparkSettings
    {
        // { "file":"wasbs:///example/jars/SparkSimpleApp.jar", "className":"com.microsoft.spark.example.WasbIOTest" }
        public string file { get; set; }
        public string className { get; set; }
        public string args { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}